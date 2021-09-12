// myffPlay.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include <iostream>
 

#include <inttypes.h>
#include <math.h>
#include <limits.h>
#include <signal.h>
#include <stdint.h>

#include "libavutil/avstring.h"
#include "libavutil/eval.h"
#include "libavutil/mathematics.h"
#include "libavutil/pixdesc.h"
#include "libavutil/imgutils.h"
#include "libavutil/dict.h"
#include "libavutil/parseutils.h"
#include "libavutil/samplefmt.h"
#include "libavutil/avassert.h"
#include "libavutil/time.h"
#include "libavformat/avformat.h"
#include "libavdevice/avdevice.h"
#include "libswscale/swscale.h"
#include "libavutil/opt.h"
#include "libavcodec/avfft.h"
#include "libswresample/swresample.h"
 
# include "libavfilter/avfilter.h"
# include "libavfilter/buffersink.h"
# include "libavfilter/buffersrc.h"
 
#include <SDL.h>
#include <SDL_thread.h> 
#include <SDL_main.h>  
#include <assert.h>
 

#pragma comment(lib ,"sdl2.lib")
#pragma comment(lib ,"SDL2main.lib")

#pragma comment(lib ,"avcodec.lib")
#pragma comment(lib ,"avdevice.lib")
#pragma comment(lib ,"avfilter.lib")
#pragma comment(lib ,"avformat.lib")
#pragma comment(lib ,"avutil.lib")
#pragma comment(lib ,"swresample.lib")
#pragma comment(lib ,"swscale.lib")
//

const char program_name[] = "ffplay";
const int program_birth_year = 2003;

#define MAX_QUEUE_SIZE (15 * 1024 * 1024)
#define MIN_FRAMES 25
#define EXTERNAL_CLOCK_MIN_FRAMES 2
#define EXTERNAL_CLOCK_MAX_FRAMES 10

/* Minimum SDL audio buffer size, in samples. */
#define SDL_AUDIO_MIN_BUFFER_SIZE 512
/* Calculate actual buffer size keeping in mind not cause too frequent audio callbacks */
#define SDL_AUDIO_MAX_CALLBACKS_PER_SEC 30

/* Step size for volume control in dB */
#define SDL_VOLUME_STEP (0.75)

/* no AV sync correction is done if below the minimum AV sync threshold */
#define AV_SYNC_THRESHOLD_MIN 0.04
/* AV sync correction is done if above the maximum AV sync threshold */
#define AV_SYNC_THRESHOLD_MAX 0.1
/* If a frame duration is longer than this, it will not be duplicated to compensate AV sync */
#define AV_SYNC_FRAMEDUP_THRESHOLD 0.1
/* no AV correction is done if too big error */
#define AV_NOSYNC_THRESHOLD 10.0

/* maximum audio speed change to get correct sync */
#define SAMPLE_CORRECTION_PERCENT_MAX 10

/* external clock speed adjustment constants for realtime sources based on buffer fullness */
#define EXTERNAL_CLOCK_SPEED_MIN  0.900
#define EXTERNAL_CLOCK_SPEED_MAX  1.010
#define EXTERNAL_CLOCK_SPEED_STEP 0.001

/* we use about AUDIO_DIFF_AVG_NB A-V differences to make the average */
#define AUDIO_DIFF_AVG_NB   20

/* polls for possible required screen refresh at least this often, should be less than 1/fps */
#define REFRESH_RATE 0.01

/* NOTE: the size must be big enough to compensate the hardware audio buffersize size */
/* TODO: We assume that a decoded and resampled frame fits into this buffer */
#define SAMPLE_ARRAY_SIZE (8 * 65536)

#define CURSOR_HIDE_DELAY 1000000

#define USE_ONEPASS_SUBTITLE_RENDER 1

static unsigned sws_flags = SWS_BICUBIC;

typedef struct MyAVPacketList {
    AVPacket pkt;
    struct MyAVPacketList* next;
    int serial;
} MyAVPacketList;

typedef struct PacketQueue {
    MyAVPacketList* first_pkt, * last_pkt;
    int nb_packets;
    int size;
    int64_t duration;
    int abort_request;
    int serial;
    SDL_mutex* mutex;
    SDL_cond* cond;
} PacketQueue;

#define VIDEO_PICTURE_QUEUE_SIZE 3
#define SUBPICTURE_QUEUE_SIZE 16
#define SAMPLE_QUEUE_SIZE 9
#define FRAME_QUEUE_SIZE FFMAX(SAMPLE_QUEUE_SIZE, FFMAX(VIDEO_PICTURE_QUEUE_SIZE, SUBPICTURE_QUEUE_SIZE))

typedef struct AudioParams {
    int freq;
    int channels;
    int64_t channel_layout;
    enum AVSampleFormat fmt;
    int frame_size;
    int bytes_per_sec;
} AudioParams;

typedef struct Clock {
    double pts;           /* clock base */
    double pts_drift;     /* clock base minus time at which we updated the clock */
    double last_updated;
    double speed;
    int serial;           /* clock is based on a packet with this serial */
    int paused;
    int* queue_serial;    /* pointer to the current packet queue serial, used for obsolete clock detection */
} Clock;

/* Common struct for handling all types of decoded data and allocated render buffers. */
typedef struct Frame {
    AVFrame* frame;
    AVSubtitle sub;
    int serial;
    double pts;           /* presentation timestamp for the frame */
    double duration;      /* estimated duration of the frame */
    int64_t pos;          /* byte position of the frame in the input file */
    int width;
    int height;
    int format;
    AVRational sar;
    int uploaded;
    int flip_v;
} Frame;

typedef struct FrameQueue {
    Frame queue[FRAME_QUEUE_SIZE];
    int rindex;
    int windex;
    int size;
    int max_size;
    int keep_last;
    int rindex_shown;
    SDL_mutex* mutex;
    SDL_cond* cond;
    PacketQueue* pktq;
} FrameQueue;

enum {
    AV_SYNC_AUDIO_MASTER, /* default choice */
    AV_SYNC_VIDEO_MASTER,
    AV_SYNC_EXTERNAL_CLOCK, /* synchronize to an external clock */
};

typedef struct Decoder {
    AVPacket pkt;
    PacketQueue* queue;
    AVCodecContext* avctx;
    int pkt_serial;
    int finished;
    int packet_pending;
    SDL_cond* empty_queue_cond;
    int64_t start_pts;
    AVRational start_pts_tb;
    int64_t next_pts;
    AVRational next_pts_tb;
    SDL_Thread* decoder_tid;
} Decoder;

typedef struct VideoState {
    SDL_Thread* read_tid;
    AVInputFormat* iformat;
    int abort_request;
    int force_refresh;
    int paused;
    int last_paused;
    int queue_attachments_req;
    int seek_req;
    int seek_flags;
    int64_t seek_pos;
    int64_t seek_rel;
    int read_pause_return;
    AVFormatContext* ic;
    int realtime;

    Clock audclk;
    Clock vidclk;
    Clock extclk;

    FrameQueue pictq;
    FrameQueue subpq;
    FrameQueue sampq;

    Decoder auddec;
    Decoder viddec;
    Decoder subdec;

    int audio_stream;

    int av_sync_type;

    double audio_clock;
    int audio_clock_serial;
    double audio_diff_cum; /* used for AV difference average computation */
    double audio_diff_avg_coef;
    double audio_diff_threshold;
    int audio_diff_avg_count;
    AVStream* audio_st;
    PacketQueue audioq;
    int audio_hw_buf_size;
    uint8_t* audio_buf;
    uint8_t* audio_buf1;
    unsigned int audio_buf_size; /* in bytes */
    unsigned int audio_buf1_size;
    int audio_buf_index; /* in bytes */
    int audio_write_buf_size;
    int audio_volume;
    int muted;
    struct AudioParams audio_src;
#if CONFIG_AVFILTER
    struct AudioParams audio_filter_src;
#endif
    struct AudioParams audio_tgt;
    struct SwrContext* swr_ctx;
    int frame_drops_early;
    int frame_drops_late;

    enum ShowMode {
        SHOW_MODE_NONE = -1, SHOW_MODE_VIDEO = 0, SHOW_MODE_WAVES, SHOW_MODE_RDFT, SHOW_MODE_NB
    } show_mode;
    int16_t sample_array[SAMPLE_ARRAY_SIZE];
    int sample_array_index;
    int last_i_start;
    RDFTContext* rdft;
    int rdft_bits;
    FFTSample* rdft_data;
    int xpos;
    double last_vis_time;
    SDL_Texture* vis_texture;
    SDL_Texture* sub_texture;
    SDL_Texture* vid_texture;

    int subtitle_stream;
    AVStream* subtitle_st;
    PacketQueue subtitleq;

    double frame_timer;
    double frame_last_returned_time;
    double frame_last_filter_delay;
    int video_stream;
    AVStream* video_st;
    PacketQueue videoq;
    double max_frame_duration;      // maximum duration of a frame - above this, we consider the jump a timestamp discontinuity
    struct SwsContext* img_convert_ctx;
    struct SwsContext* sub_convert_ctx;
    int eof;

    char* filename;
    int width, height, xleft, ytop;
    int step;

#if CONFIG_AVFILTER
    int vfilter_idx;
    AVFilterContext* in_video_filter;   // the first filter in the video chain
    AVFilterContext* out_video_filter;  // the last filter in the video chain
    AVFilterContext* in_audio_filter;   // the first filter in the audio chain
    AVFilterContext* out_audio_filter;  // the last filter in the audio chain
    AVFilterGraph* agraph;              // audio filter graph
#endif

    int last_video_stream, last_audio_stream, last_subtitle_stream;

    SDL_cond* continue_read_thread;
} VideoState;

/* options specified by the user */
static AVInputFormat* file_iformat;
static const char* input_filename;
static const char* window_title;
static int default_width = 640;
static int default_height = 480;
static int screen_width = 0;
static int screen_height = 0;
static int screen_left = SDL_WINDOWPOS_CENTERED;
static int screen_top = SDL_WINDOWPOS_CENTERED;
static int audio_disable;
static int video_disable;
static int subtitle_disable;
static const char* wanted_stream_spec[AVMEDIA_TYPE_NB] = { 0 };
static int seek_by_bytes = -1;
static float seek_interval = 10;
static int display_disable;
static int borderless;
static int alwaysontop;
static int startup_volume = 100;
static int show_status = 1;
static int av_sync_type = AV_SYNC_AUDIO_MASTER;
static int64_t start_time = AV_NOPTS_VALUE;
static int64_t duration = AV_NOPTS_VALUE;
static int fast = 0;
static int genpts = 0;
static int lowres = 0;
static int decoder_reorder_pts = -1;
static int autoexit;
static int exit_on_keydown;
static int exit_on_mousedown;
static int loop = 1;
static int framedrop = -1;
static int infinite_buffer = -1;
static enum ShowMode show_mode =  SHOW_MODE_NONE;
static const char* audio_codec_name;
static const char* subtitle_codec_name;
static const char* video_codec_name;
double rdftspeed = 0.02;
static int64_t cursor_last_shown;
static int cursor_hidden = 0;
#if CONFIG_AVFILTER
static const char** vfilters_list = NULL;
static int nb_vfilters = 0;
static char* afilters = NULL;
#endif
static int autorotate = 1;
static int find_stream_info = 1;
static int filter_nbthreads = 0;

/* current context */
static int is_full_screen;
static int64_t audio_callback_time;

static AVPacket flush_pkt;

#define FF_QUIT_EVENT    (SDL_USEREVENT + 2)

static SDL_Window* window;
static SDL_Renderer* renderer;
static SDL_RendererInfo renderer_info = { 0 };
static SDL_AudioDeviceID audio_dev;

static const struct TextureFormatEntry {
    enum AVPixelFormat format;
    int texture_fmt;
} sdl_texture_format_map[] = {
    { AV_PIX_FMT_RGB8,           SDL_PIXELFORMAT_RGB332 },
    { AV_PIX_FMT_RGB444,         SDL_PIXELFORMAT_RGB444 },
    { AV_PIX_FMT_RGB555,         SDL_PIXELFORMAT_RGB555 },
    { AV_PIX_FMT_BGR555,         SDL_PIXELFORMAT_BGR555 },
    { AV_PIX_FMT_RGB565,         SDL_PIXELFORMAT_RGB565 },
    { AV_PIX_FMT_BGR565,         SDL_PIXELFORMAT_BGR565 },
    { AV_PIX_FMT_RGB24,          SDL_PIXELFORMAT_RGB24 },
    { AV_PIX_FMT_BGR24,          SDL_PIXELFORMAT_BGR24 },
    { AV_PIX_FMT_0RGB32,         SDL_PIXELFORMAT_RGB888 },
    { AV_PIX_FMT_0BGR32,         SDL_PIXELFORMAT_BGR888 },
    { AV_PIX_FMT_NE(RGB0, 0BGR), SDL_PIXELFORMAT_RGBX8888 },
    { AV_PIX_FMT_NE(BGR0, 0RGB), SDL_PIXELFORMAT_BGRX8888 },
    { AV_PIX_FMT_RGB32,          SDL_PIXELFORMAT_ARGB8888 },
    { AV_PIX_FMT_RGB32_1,        SDL_PIXELFORMAT_RGBA8888 },
    { AV_PIX_FMT_BGR32,          SDL_PIXELFORMAT_ABGR8888 },
    { AV_PIX_FMT_BGR32_1,        SDL_PIXELFORMAT_BGRA8888 },
    { AV_PIX_FMT_YUV420P,        SDL_PIXELFORMAT_IYUV },
    { AV_PIX_FMT_YUYV422,        SDL_PIXELFORMAT_YUY2 },
    { AV_PIX_FMT_UYVY422,        SDL_PIXELFORMAT_UYVY },
    { AV_PIX_FMT_NONE,           SDL_PIXELFORMAT_UNKNOWN },
};


//
#undef  main;
int main()
{
  printf(  "Hello World!\n");
}

// 运行程序: Ctrl + F5 或调试 >“开始执行(不调试)”菜单
// 调试程序: F5 或调试 >“开始调试”菜单

// 入门使用技巧: 
//   1. 使用解决方案资源管理器窗口添加/管理文件
//   2. 使用团队资源管理器窗口连接到源代码管理
//   3. 使用输出窗口查看生成输出和其他消息
//   4. 使用错误列表窗口查看错误
//   5. 转到“项目”>“添加新项”以创建新的代码文件，或转到“项目”>“添加现有项”以将现有代码文件添加到项目
//   6. 将来，若要再次打开此项目，请转到“文件”>“打开”>“项目”并选择 .sln 文件
