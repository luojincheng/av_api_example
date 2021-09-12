package com.greensoftcode;


public class ApplicationMain {

    public static void main(String[] args) throws InterruptedException {

        //启动hls收流线程
        MyFfmpegHlsPullThread thread = new MyFfmpegHlsPullThread();
        thread.start();
        //30s后退出收流
        Thread.sleep(1000 * 30);
        thread.exitThead();
        //放在main函数退出
        Thread.sleep(1000 * 86400);

    }


}
