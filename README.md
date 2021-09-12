# av_api_example

#### 介绍
av_ffhls_Api是基于ffmpeg api开发的一款收取流（大华、海康等摄像头或rtsp视频源）完成hls直播、回看API函数。
 
av_ffplay_Api是基于ffplay api开发的一款  支持hls 、http、 file等协议的播放视频api函数。

#### hls拉流api：av_ffhls_Api.so/av_ffhls_Api.dll(主要应用web服务端) 

 1.拉流函数doHls
	     int doHls(const char *sn, const char * cardIdFilePath ,const char *RtspUrl, const char *protocol, const char *outPath, const char *newTime, const  char *start_number, const  char *hls_time, const  char *stimeout,int *exitfun, void (*outParamFun)(int type, int cur_pkt_size, int sum_Pkt_Size, int stmpTime, char *folder, char *rtspUrl, char *outLog));

  参数
     (1)sn：试用版本sn:0d09419934dc4820056c05f6bfb394a994cfb5e9db2ea3761001c0960cf34bd0700cfbd17011060eb2a7aa1152431ebd85227d841c29a7ce3ec6708a7fb95c09f3df88ec002e6a0d6873d9b217a090bcd11b20f244c04f8327ced9a0e7bad9ee71e20352e139f50865976839dd4f484cab11db1743322527a87886b50d35ccdd加官网水印。
 
    (2)cardIdFilePath:cardid存在路径,例如："./win32-x86-64/libs/ffmpeg/x64/cardid.png"

    (3)RtspUrl：rtsp地址,例如："udp""rtsp://127.0.0.1:8554/test01"

    (4)protocol：支持tcp/udp连接。例如："tcp"

    (5)outPath： ts文件保存路径。例如："F:\\temp\\video\\rtsp\\127_0_0_1_8554\\"

    (6)newTime：每天重新生产视频存储路的路径。例如；"23:59:59" 在凌晨重新生成。(说明： 2021-01-01....2021-12-31按日期生产回看文件夹) 

    (7)start_number：生产ts文件编号的起始位置（out0.ts）。例如:  "0"

    (8)hls_time：每个ts文件的时长。例如： "6"

    (9)stimeout：tcp/udp连接rtsp服务器或摄像机的超时时间.例如： "10000000"（单位：微秒 10000000==10s）

    (10)exitfun: 结束函数内循环收流过程标志 0：不退出 1：退出

    (11)void (*outParamFun):日志输出函数

        (11.1) type:返回处理包类型：视频/音频

        (11.2) cur_pkt_size：当前接收包尺寸。（单位：B）

        (11.3) sum_Pkt_Size: 本次执行doHls总共接收包尺寸。（单位：B）

        (11.4) stmpTime：接收包的时间戳（单位:s）

        (11.5) folder：ts保存路径。（说明：与主参数outPath区别，folder==outPath+日期）

        (11.6) rtspUrl：rtsp地址 （说明：与主参数RtspUrl一样，为了在多线程控制台下查看那个地址运行正常)

    返回值

     (1)  0:成功。注意：  永远不会返回，内部程序一直运行失败或完成才会返回。

     (2) -1：验证图片不存在

     (3) -2：验证码错误
 
     (4) -3: 连接rtsp服务器失败

     (5) -4：初始化过滤器失败

     (6) -5: 开始生产m3u8文件失败

     (7) -100:命令退出

     (8) -101:读取视频失败


#### 播放api：av_ffplay_Api.dll(主要应用pc客户端) 

 说明： 支持hls 、http、 file等协议的播放视频api函数

     1. 播放函数：doPlay

        doPlay(char *filename, void *handle, int64_t *duration);

            参数：

             (1) filename:播放视频文件或url地址

             (2) handle：显示窗口句柄

             (3) duration：输出参数，播放总时长

     2. 调整进度(单位s)  double seekPos(double newPos);

     3. 播放位置(单位s)   double getPos();

     4. 快播(speed 10的倍数 20 30...) void playFast(int speed);

     5. 慢播(speed 10的倍数 20 30...) void playSlow(int speed);

     6. 暂停 void playPause();

     7. 退出 void doExit()


#### 下载地址

     1.av_ffhls_Api(拉流调用例子java)

       https://gitee.com/luo-jincheng/av_api_example.git

     2.av_ffplay_Api(播放调用例子c#)

       https://gitee.com/luo-jincheng/av_api_example.git 

#### 使用Api案例：

       https://gitee.com/luo-jincheng/mpegserver.git