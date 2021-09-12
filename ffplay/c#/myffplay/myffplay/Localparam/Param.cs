using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myffplay.Localparam
{
    public enum PlayStatus {paly, pause, stop};
    public enum PlaySpeed { normal,fast, slow};
    public   class Param
    {
        public IntPtr handle;
        public bool   movePos;//手动滑动改变播放进度 
        public PlayStatus status;//播放状态
        public PlaySpeed speed;  //播放速度
    }

}
