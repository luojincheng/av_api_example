package com.greensoftcode.api.ffmpeg;

import com.sun.jna.Structure;

import java.util.Arrays;
import java.util.List;
//**************ljc******************废弃
public   class OutParam extends Structure {  //注意继续Structure
    public int cur_pkt_size;  //当前包尺寸
    public int sum_Pkt_Size;  //接收包总尺寸
    public int stmpTime;          //当前时间戳
    public byte[] folder;      //存在路径
    public byte[] rtspUrl;     //rtspUrl

    //结构体指针
    public static class ByReference extends OutParam implements Structure.ByReference {
    }
    public static class ByValue extends OutParam implements Structure.ByValue {
    }
    @Override
    protected List getFieldOrder() {
        return Arrays.asList("cur_pkt_size", "sum_Pkt_Size", "stmpTime", "folder", "rtspUrl");
    }
}