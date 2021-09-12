using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using myffplay.Localparam;

namespace myffplay
{


    public partial class Form1 : Form
    {




        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern int doPlay(string filename, IntPtr handler);   //API：播放 


        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern double seekPos(double newPos);   //API：调整进度(单位s) 

        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern double getPos();   //API:进度位置(单位s)


        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern ulong playLen(string filename); //API:播放时长(单位s)


        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern void playFast(int speed);  //API：快播  >10 例如：30  正常：10


        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern void playSlow(int speed);    //API：慢播 >10 例如：30  正常：10


        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern void playPause();    //API：暂停  


        [DllImport("..\\..\\..\\..\\win32-x86-64\\libs\\ffmpeg\\x64\\av_ffplay_Api.dll")]
        public static extern void doExit(); //API：退出



        #region "变量"
        private Thread m_thDoPlay = null;
        private Thread m_thPos = null;
        //string m_filename = "F:\\temp\\video\\ring.mp4";
        string m_filename = "F:\\temp\\video\\tzys.mp3";
        Param m_param = new Param(); 
        private bool m_exit=false;

        #endregion
        public Form1()
        {
            InitializeComponent();
        }
     
        private void cc_playbtn_Click(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedIndex==0)
            this.m_filename = this.cc_openfiledlg.FileName;
            else
            this.m_filename = this.cc_playurl.Text;
            //播放(利用线程)
            if (m_param.status == PlayStatus.stop)  //播放
            {
                m_param.status = PlayStatus.paly;
                m_exit = false;
                this.cc_playbtn.Text = "暂停";
                this.cc_tackbar.Value = 0;
                this.cc_lb_02.Text = "当前播放:0s";
                if (m_thDoPlay != null)
                {
                    doExit();
                    m_thDoPlay.Abort();
                    m_thDoPlay = null;
                }
                m_param.handle = this.cc_panel.Handle;
                m_thDoPlay = new Thread(new ParameterizedThreadStart(thDoPlayFun));//ParameterizedThreadStart 是委托，参数是函数名
                m_thDoPlay.Start(m_param);//Svtart的参数类型是 ob  
                m_thPos = new Thread(PosFun); //进度及播放位置
                m_thPos.Start(); 
            }
            else
            {
                if (m_param.status == PlayStatus.paly)  //暂停播放
                {
                    m_param.status = PlayStatus.pause;
                    this.cc_playbtn.Text = "播放";
                    playPause();
                }
                else if (m_param.status == PlayStatus.pause)  //继续播放
                {
                    m_param.status = PlayStatus.paly;
                    this.cc_playbtn.Text = "暂停";
                    playPause();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.m_param.status = PlayStatus.stop;
            this.cc_playbtn.Enabled = false;
            this.cc_fastbtn.Enabled = false;
            this.cc_slowbtn.Enabled = false;
            this.cc_stopbtn.Enabled = false;
        }

        //播放线程
        private void thDoPlayFun(Object p)
        {
           
            IntPtr handle = ((Param)p).handle;
            doPlay(m_filename, handle);

        }
        //获得进度条大小,改变位置
        private delegate void PosDeleg();
        private void PosFun()
        {
            int len = (int)playLen(m_filename) ;
            PosDeleg maxDelegate = delegate ()
            {
                if (this.IsHandleCreated)
                {
                    this.cc_tackbar.Maximum = len;
                    this.cc_lb_01.Text = "总时长:" + len.ToString() + "s";
                }
            }; 
            try { this.Invoke(maxDelegate); } catch { }; //异常   
            int pos =0; 
            while (len > pos && !m_exit)
            {
                if (this.m_exit) break;
                Thread.Sleep(100);
                if (this.m_param.movePos) continue;
                pos = (int)getPos();  //异常
                PosDeleg setDelegate = delegate ()
                {
                    if (this.IsHandleCreated)
                    {
                        this.cc_tackbar.Value = pos;
                        this.cc_lb_02.Text = "当前播放:" + pos.ToString() + "s";
                    }
                };
                try { this.Invoke(setDelegate); } catch { }; //异常   
            };
         
            PosDeleg mDelegate = delegate ()
            {
                if (this.IsHandleCreated)
                {
                    doExit();
                    m_param.status = PlayStatus.stop; 
                    this.cc_tackbar.Value = 0;
                    this.cc_lb_02.Text = "当前播放:0s";
                    this.cc_playbtn.Text = "播放";
                }
            };
            try { this.Invoke(mDelegate); }  catch { }; //异常   

        }


 

        private void cc_tackbar_MouseCaptureChanged(object sender, EventArgs e)
        {
            this.m_param.movePos = true;
            double curVal = this.cc_tackbar.Value;
            this.cc_lb_02.Text = curVal.ToString();
            double f = seekPos(curVal);
            this.m_param.movePos = false;

        } 
        private void cc_tackbar_MouseEnter(object sender, EventArgs e)
        {
            this.m_param.movePos = true;
        }

        private void cc_tackbar_MouseLeave(object sender, EventArgs e)
        {
            this.m_param.movePos = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.m_exit = true;
            doExit();
        }

        private void cc_fastbtn_Click(object sender, EventArgs e)
        {
            if (m_param.speed == PlaySpeed.normal)
            {
                m_param.speed = PlaySpeed.fast;
                playFast(30); ;  //值越大越快
                this.cc_fastbtn.Text = "正常速度";
            }
            else
            {
                m_param.speed = PlaySpeed.normal;
                playFast(10); //正常播放
                this.cc_fastbtn.Text = "快速播放";
            }
          
        }

        private void cc_slowbtn_Click(object sender, EventArgs e)
        {
            if (m_param.speed == PlaySpeed.normal)
            {
                m_param.speed = PlaySpeed.slow;
                playSlow(30);  //值越大越慢
                this.cc_slowbtn.Text = "正常速度";
            }
            else
            {
                m_param.speed = PlaySpeed.normal;
                playSlow(10);//正常播放
                this.cc_slowbtn.Text = "慢速播放";
            }
        }

        private void cc_openfilebtn_Click(object sender, EventArgs e)
        {
            if (this.cc_openfiledlg.ShowDialog() == DialogResult.OK)
            {
                this.m_filename = this.cc_openfiledlg.FileName;
                this.cc_playbtn.Enabled = true;
                this.cc_fastbtn.Enabled = true;
                this.cc_slowbtn.Enabled = true;
                this.cc_stopbtn.Enabled = true;
            }
        }

        private void cc_stopbtn_Click(object sender, EventArgs e)
        {
            m_exit = true;
            doExit();
        }
    }
}
