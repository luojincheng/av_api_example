package com.greensoftcode.api.ffmpeg;

import com.sun.jna.*;
import com.sun.jna.ptr.IntByReference;
import com.sun.jna.win32.StdCallLibrary.StdCallCallback;

import java.lang.reflect.Field;
import java.lang.reflect.Modifier;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;


/**
 * NetSDK JNA接口封装
 */
public interface HlsLib extends Library {

    // HlsLib INSTANCE = (HlsLib) Native.load(Utils.getLoadLibrary("av_ffhls_Api"), HlsLib.class);
    public   HlsLib INSTANCE = (HlsLib) Native.load("av_ffhls_Api", HlsLib.class);
    //拉流API
    public int doHls(String sn, String cardid,String RtspUrl, String protocol, String outPath, String newTime,
                     String  start_number,
                     String hls_time,
                     String timeout,
                     IntByReference eixfun,
                     Callback callback);

    //Callback
    public interface IOutParamBack extends Callback {
        public void outParamFun(int type,int cur_pkt_size,int sum_Pkt_Size, int stmpTime,int curmarkgop,String folder,
                                String rtspUrl,
                                String outLog);
    }

}

