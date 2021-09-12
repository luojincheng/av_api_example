
namespace myffplay
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cc_panel = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.cc_openfiledlg = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cc_tackbar = new System.Windows.Forms.TrackBar();
            this.cc_openfilebtn = new System.Windows.Forms.Button();
            this.cc_playbtn = new System.Windows.Forms.Button();
            this.cc_stopbtn = new System.Windows.Forms.Button();
            this.cc_fastbtn = new System.Windows.Forms.Button();
            this.cc_slowbtn = new System.Windows.Forms.Button();
            this.cc_lb_02 = new System.Windows.Forms.Label();
            this.cc_lb_01 = new System.Windows.Forms.Label();
            this.cc_playurl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cc_playnetbtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cc_tackbar)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(565, 589);
            this.splitContainer1.SplitterDistance = 367;
            this.splitContainer1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cc_panel);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(565, 367);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "播放窗口";
            // 
            // cc_panel
            // 
            this.cc_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cc_panel.Location = new System.Drawing.Point(3, 17);
            this.cc_panel.Name = "cc_panel";
            this.cc_panel.Size = new System.Drawing.Size(559, 347);
            this.cc_panel.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Font = new System.Drawing.Font("宋体", 1F);
            this.splitContainer2.Size = new System.Drawing.Size(565, 218);
            this.splitContainer2.SplitterDistance = 189;
            this.splitContainer2.TabIndex = 0;
            // 
            // cc_openfiledlg
            // 
            this.cc_openfiledlg.FileName = "openFileDialog1";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(565, 189);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cc_lb_02);
            this.tabPage1.Controls.Add(this.cc_lb_01);
            this.tabPage1.Controls.Add(this.cc_slowbtn);
            this.tabPage1.Controls.Add(this.cc_fastbtn);
            this.tabPage1.Controls.Add(this.cc_stopbtn);
            this.tabPage1.Controls.Add(this.cc_playbtn);
            this.tabPage1.Controls.Add(this.cc_openfilebtn);
            this.tabPage1.Controls.Add(this.cc_tackbar);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(557, 163);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "播放文件";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cc_playnetbtn);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.cc_playurl);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(557, 163);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "播放网络流";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cc_tackbar
            // 
            this.cc_tackbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.cc_tackbar.Location = new System.Drawing.Point(3, 3);
            this.cc_tackbar.Name = "cc_tackbar";
            this.cc_tackbar.Size = new System.Drawing.Size(551, 45);
            this.cc_tackbar.TabIndex = 1;
            this.cc_tackbar.Scroll += new System.EventHandler(this.cc_tackbar_MouseCaptureChanged);
            // 
            // cc_openfilebtn
            // 
            this.cc_openfilebtn.Location = new System.Drawing.Point(18, 102);
            this.cc_openfilebtn.Name = "cc_openfilebtn";
            this.cc_openfilebtn.Size = new System.Drawing.Size(87, 23);
            this.cc_openfilebtn.TabIndex = 11;
            this.cc_openfilebtn.Text = "打开播放文件";
            this.cc_openfilebtn.UseVisualStyleBackColor = true;
            this.cc_openfilebtn.Click += new System.EventHandler(this.cc_openfilebtn_Click);
            // 
            // cc_playbtn
            // 
            this.cc_playbtn.Location = new System.Drawing.Point(183, 102);
            this.cc_playbtn.Name = "cc_playbtn";
            this.cc_playbtn.Size = new System.Drawing.Size(75, 23);
            this.cc_playbtn.TabIndex = 12;
            this.cc_playbtn.Text = "播放";
            this.cc_playbtn.UseVisualStyleBackColor = true;
            this.cc_playbtn.Click += new System.EventHandler(this.cc_playbtn_Click);
            // 
            // cc_stopbtn
            // 
            this.cc_stopbtn.Location = new System.Drawing.Point(276, 102);
            this.cc_stopbtn.Name = "cc_stopbtn";
            this.cc_stopbtn.Size = new System.Drawing.Size(75, 23);
            this.cc_stopbtn.TabIndex = 13;
            this.cc_stopbtn.Text = "停止播放";
            this.cc_stopbtn.UseVisualStyleBackColor = true;
            this.cc_stopbtn.Click += new System.EventHandler(this.cc_stopbtn_Click);
            // 
            // cc_fastbtn
            // 
            this.cc_fastbtn.Location = new System.Drawing.Point(369, 102);
            this.cc_fastbtn.Name = "cc_fastbtn";
            this.cc_fastbtn.Size = new System.Drawing.Size(75, 23);
            this.cc_fastbtn.TabIndex = 14;
            this.cc_fastbtn.Text = "快速播放";
            this.cc_fastbtn.UseVisualStyleBackColor = true;
            this.cc_fastbtn.Click += new System.EventHandler(this.cc_fastbtn_Click);
            // 
            // cc_slowbtn
            // 
            this.cc_slowbtn.Location = new System.Drawing.Point(462, 102);
            this.cc_slowbtn.Name = "cc_slowbtn";
            this.cc_slowbtn.Size = new System.Drawing.Size(75, 23);
            this.cc_slowbtn.TabIndex = 15;
            this.cc_slowbtn.Text = "慢速播放";
            this.cc_slowbtn.UseVisualStyleBackColor = true;
            // 
            // cc_lb_02
            // 
            this.cc_lb_02.AutoSize = true;
            this.cc_lb_02.Location = new System.Drawing.Point(392, 68);
            this.cc_lb_02.Name = "cc_lb_02";
            this.cc_lb_02.Size = new System.Drawing.Size(71, 12);
            this.cc_lb_02.TabIndex = 17;
            this.cc_lb_02.Text = "当前播放:0s";
            // 
            // cc_lb_01
            // 
            this.cc_lb_01.AutoSize = true;
            this.cc_lb_01.Location = new System.Drawing.Point(158, 68);
            this.cc_lb_01.Name = "cc_lb_01";
            this.cc_lb_01.Size = new System.Drawing.Size(59, 12);
            this.cc_lb_01.TabIndex = 16;
            this.cc_lb_01.Text = "总时长:0s";
            // 
            // cc_playurl
            // 
            this.cc_playurl.Location = new System.Drawing.Point(94, 51);
            this.cc_playurl.Name = "cc_playurl";
            this.cc_playurl.Size = new System.Drawing.Size(444, 21);
            this.cc_playurl.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "播放网络地址：";
            // 
            // cc_playnetbtn
            // 
            this.cc_playnetbtn.Location = new System.Drawing.Point(212, 92);
            this.cc_playnetbtn.Name = "cc_playnetbtn";
            this.cc_playnetbtn.Size = new System.Drawing.Size(75, 23);
            this.cc_playnetbtn.TabIndex = 13;
            this.cc_playnetbtn.Text = "播放";
            this.cc_playnetbtn.UseVisualStyleBackColor = true;
            this.cc_playnetbtn.Click += new System.EventHandler(this.cc_playbtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 589);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cc_tackbar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel cc_panel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.OpenFileDialog cc_openfiledlg;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TrackBar cc_tackbar;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label cc_lb_02;
        private System.Windows.Forms.Label cc_lb_01;
        private System.Windows.Forms.Button cc_slowbtn;
        private System.Windows.Forms.Button cc_fastbtn;
        private System.Windows.Forms.Button cc_stopbtn;
        private System.Windows.Forms.Button cc_playbtn;
        private System.Windows.Forms.Button cc_openfilebtn;
        private System.Windows.Forms.Button cc_playnetbtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox cc_playurl;
    }
}

