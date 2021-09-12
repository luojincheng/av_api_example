package com.greensoftcode;

import com.greensoftcode.api.ffmpeg.HlsLib;

public class MyFfmpegHlsOutParamFunImpl implements HlsLib.IOutParamBack {
    @Override
    public void outParamFun(int type, int cur_pkt_size, int sum_Pkt_Size, int stmpTime, int curmarkgop, String folder, String rtspUrl, String outLog) {
        System.out.println("=======返回参数=======>" + type + "," + rtspUrl + "," + folder + ","
                + cur_pkt_size + "," + sum_Pkt_Size + "," + stmpTime + "," +curmarkgop+","+ outLog);  //参数获得错误日志
    }
}
