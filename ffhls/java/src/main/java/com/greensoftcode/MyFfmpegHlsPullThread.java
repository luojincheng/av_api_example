package com.greensoftcode;

import com.greensoftcode.api.ffmpeg.HlsLib;
import com.sun.jna.ptr.IntByReference;

//拉流原子线程
public class MyFfmpegHlsPullThread extends Thread {

    public HlsLib hlsLib ;  //jna
    private   String rtspUrl="rtsp://127.0.0.1:8554/test01";
    private    String realCardIdPath="./win32-x86-64/libs/ffmpeg/x64/cardid.png"; //使用者身份证照片路径。这个路径是相对dll/so路径
    private    String   protocol="TCP"; //指定rtsp通讯协议
    private    String outPath="F:\\temp\\ffhls\\"; //m3u8/ts文件保存路径
    private    String Hlststime="23:59:59";  //按日期新生产m3u8/ts文件保存路径
    private    String  start_number="0";    //ts文件起始编号
    private    String   Hlstimelen="6";      //ts文件时长
    private    String Timeout="5000000";   //tcp/udp连接rtsp服务器或摄像机的超时时间.例如： "10000000"（单位：微秒
    public  IntByReference exitFun=new IntByReference(0);//退出Api函数

    //SN
    private static   String Sn=
            "135b5d73d2166b07a82746b5d444c56236746ce222dba1e0f17dd26824a352396ec996fbe9b38f9890efcbaea1c370ecc46b5d70407187a3a46e585520ec7f0d43c2d8750cfd21948b1b5ad5c4986569ae1c470363998b0ed3153e70442771e79b9d9e81b574b069f6249cd3343406e322245a3fb11dd444c43cbf961aed83ee";

    public MyFfmpegHlsPullThread(){
        String libararyPath="/win32-x86-64/libs/ffmpeg/x64";
        String userPath= System.getProperty("user.dir");
        String dllFolder= userPath+libararyPath;
        //修改jna加载动态库路径
        System.setProperty("jna.library.path",dllFolder);
        hlsLib = HlsLib.INSTANCE;  //jna

    }

    //退出函数
    public void exitThead() {
        this.exitFun.setValue(1);
    }

    @Override
    public void run() {
        super.run();
        int ret = hlsLib.doHls(Sn, realCardIdPath, rtspUrl,
                protocol.toLowerCase(), outPath,
                Hlststime, start_number,  Hlstimelen,
                Timeout,
                exitFun,
                new MyFfmpegHlsOutParamFunImpl());
    }

}
