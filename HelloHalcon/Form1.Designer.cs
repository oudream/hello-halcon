namespace HelloHalcon
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.autoWLWWRadioButton = new System.Windows.Forms.RadioButton();
            this.noneRadioButton = new System.Windows.Forms.RadioButton();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fitSmallestCircleButton = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openImageButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.wwLabel = new System.Windows.Forms.Label();
            this.wlLabel = new System.Windows.Forms.Label();
            this.WLWWCheckBox = new System.Windows.Forms.CheckBox();
            this.wlTrackBar = new System.Windows.Forms.TrackBar();
            this.wwTrackBar = new System.Windows.Forms.TrackBar();
            this.drawCenterCrossRadioButton = new System.Windows.Forms.CheckBox();
            this.drawSmallestCircleCheckBox = new System.Windows.Forms.CheckBox();
            this.findCircleAreaRadioButton = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wlTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wwTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.drawSmallestCircleCheckBox);
            this.panel1.Controls.Add(this.drawCenterCrossRadioButton);
            this.panel1.Controls.Add(this.fitSmallestCircleButton);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.openImageButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1400, 100);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.findCircleAreaRadioButton);
            this.groupBox1.Controls.Add(this.autoWLWWRadioButton);
            this.groupBox1.Controls.Add(this.noneRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(1054, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(334, 66);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "画框的作用";
            // 
            // autoWLWWRadioButton
            // 
            this.autoWLWWRadioButton.AutoSize = true;
            this.autoWLWWRadioButton.Enabled = false;
            this.autoWLWWRadioButton.Location = new System.Drawing.Point(115, 33);
            this.autoWLWWRadioButton.Name = "autoWLWWRadioButton";
            this.autoWLWWRadioButton.Size = new System.Drawing.Size(141, 22);
            this.autoWLWWRadioButton.TabIndex = 0;
            this.autoWLWWRadioButton.Text = "自动窗宽窗位";
            this.autoWLWWRadioButton.UseVisualStyleBackColor = true;
            // 
            // noneRadioButton
            // 
            this.noneRadioButton.AutoSize = true;
            this.noneRadioButton.Checked = true;
            this.noneRadioButton.Location = new System.Drawing.Point(15, 33);
            this.noneRadioButton.Name = "noneRadioButton";
            this.noneRadioButton.Size = new System.Drawing.Size(69, 22);
            this.noneRadioButton.TabIndex = 0;
            this.noneRadioButton.TabStop = true;
            this.noneRadioButton.Text = "NONE";
            this.noneRadioButton.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(779, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 82);
            this.button5.TabIndex = 2;
            this.button5.Text = "临时";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(699, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(79, 82);
            this.button4.TabIndex = 2;
            this.button4.Text = "清除框";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(619, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(79, 82);
            this.button3.TabIndex = 2;
            this.button3.Text = "画中框";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(524, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 82);
            this.button2.TabIndex = 2;
            this.button2.Text = "适应大小";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(446, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(72, 82);
            this.button1.TabIndex = 2;
            this.button1.Text = "1:1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fitSmallestCircleButton
            // 
            this.fitSmallestCircleButton.Location = new System.Drawing.Point(859, 12);
            this.fitSmallestCircleButton.Name = "fitSmallestCircleButton";
            this.fitSmallestCircleButton.Size = new System.Drawing.Size(79, 82);
            this.fitSmallestCircleButton.TabIndex = 2;
            this.fitSmallestCircleButton.Text = "外接圆";
            this.fitSmallestCircleButton.UseVisualStyleBackColor = true;
            this.fitSmallestCircleButton.Click += new System.EventHandler(this.fitSmallestCircleButton_Click_1);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(340, 12);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 28);
            this.textBox3.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(234, 12);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 28);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(128, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 28);
            this.textBox1.TabIndex = 1;
            // 
            // openImageButton
            // 
            this.openImageButton.Location = new System.Drawing.Point(12, 12);
            this.openImageButton.Name = "openImageButton";
            this.openImageButton.Size = new System.Drawing.Size(110, 82);
            this.openImageButton.TabIndex = 0;
            this.openImageButton.Text = "打开图像";
            this.openImageButton.UseVisualStyleBackColor = true;
            this.openImageButton.Click += new System.EventHandler(this.openImageButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1400, 642);
            this.panel2.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1400, 642);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.hWindowControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1392, 610);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(3, 3);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(1386, 604);
            this.hWindowControl.TabIndex = 1;
            this.hWindowControl.WindowSize = new System.Drawing.Size(1386, 604);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1392, 610);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1386, 604);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.WLWWCheckBox);
            this.panel3.Controls.Add(this.wlTrackBar);
            this.panel3.Controls.Add(this.wwTrackBar);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 742);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1400, 100);
            this.panel3.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.wwLabel);
            this.panel4.Controls.Add(this.wlLabel);
            this.panel4.Location = new System.Drawing.Point(987, 17);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(377, 67);
            this.panel4.TabIndex = 29;
            // 
            // wwLabel
            // 
            this.wwLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wwLabel.ForeColor = System.Drawing.Color.Tomato;
            this.wwLabel.Location = new System.Drawing.Point(187, 18);
            this.wwLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.wwLabel.Name = "wwLabel";
            this.wwLabel.Size = new System.Drawing.Size(154, 35);
            this.wwLabel.TabIndex = 23;
            this.wwLabel.Text = "ww";
            this.wwLabel.Visible = false;
            // 
            // wlLabel
            // 
            this.wlLabel.ForeColor = System.Drawing.Color.Tomato;
            this.wlLabel.Location = new System.Drawing.Point(19, 18);
            this.wlLabel.Name = "wlLabel";
            this.wlLabel.Size = new System.Drawing.Size(127, 30);
            this.wlLabel.TabIndex = 34;
            this.wlLabel.Text = "wl";
            this.wlLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.wlLabel.Visible = false;
            // 
            // WLWWCheckBox
            // 
            this.WLWWCheckBox.AutoSize = true;
            this.WLWWCheckBox.Checked = true;
            this.WLWWCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.WLWWCheckBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WLWWCheckBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.WLWWCheckBox.Location = new System.Drawing.Point(241, 19);
            this.WLWWCheckBox.Name = "WLWWCheckBox";
            this.WLWWCheckBox.Size = new System.Drawing.Size(60, 28);
            this.WLWWCheckBox.TabIndex = 28;
            this.WLWWCheckBox.Text = "WL";
            this.WLWWCheckBox.UseVisualStyleBackColor = true;
            this.WLWWCheckBox.Visible = false;
            // 
            // wlTrackBar
            // 
            this.wlTrackBar.LargeChange = 500;
            this.wlTrackBar.Location = new System.Drawing.Point(284, 15);
            this.wlTrackBar.Margin = new System.Windows.Forms.Padding(4);
            this.wlTrackBar.Maximum = 50000;
            this.wlTrackBar.Minimum = 100;
            this.wlTrackBar.Name = "wlTrackBar";
            this.wlTrackBar.Size = new System.Drawing.Size(200, 69);
            this.wlTrackBar.SmallChange = 100;
            this.wlTrackBar.TabIndex = 24;
            this.wlTrackBar.Value = 10000;
            this.wlTrackBar.Visible = false;
            // 
            // wwTrackBar
            // 
            this.wwTrackBar.LargeChange = 500;
            this.wwTrackBar.Location = new System.Drawing.Point(613, 17);
            this.wwTrackBar.Margin = new System.Windows.Forms.Padding(4);
            this.wwTrackBar.Maximum = 20000;
            this.wwTrackBar.Minimum = 30;
            this.wwTrackBar.Name = "wwTrackBar";
            this.wwTrackBar.Size = new System.Drawing.Size(200, 69);
            this.wwTrackBar.SmallChange = 100;
            this.wwTrackBar.TabIndex = 25;
            this.wwTrackBar.Value = 10000;
            this.wwTrackBar.Visible = false;
            // 
            // drawCenterCrossRadioButton
            // 
            this.drawCenterCrossRadioButton.AutoSize = true;
            this.drawCenterCrossRadioButton.Location = new System.Drawing.Point(128, 55);
            this.drawCenterCrossRadioButton.Name = "drawCenterCrossRadioButton";
            this.drawCenterCrossRadioButton.Size = new System.Drawing.Size(88, 22);
            this.drawCenterCrossRadioButton.TabIndex = 4;
            this.drawCenterCrossRadioButton.Text = "十字线";
            this.drawCenterCrossRadioButton.UseVisualStyleBackColor = true;
            this.drawCenterCrossRadioButton.Click += new System.EventHandler(this.drawCenterCrossRadioButton_Click);
            // 
            // drawSmallestCircleCheckBox
            // 
            this.drawSmallestCircleCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.drawSmallestCircleCheckBox.AutoSize = true;
            this.drawSmallestCircleCheckBox.Location = new System.Drawing.Point(222, 56);
            this.drawSmallestCircleCheckBox.Name = "drawSmallestCircleCheckBox";
            this.drawSmallestCircleCheckBox.Size = new System.Drawing.Size(88, 22);
            this.drawSmallestCircleCheckBox.TabIndex = 46;
            this.drawSmallestCircleCheckBox.Text = "外接圆";
            this.drawSmallestCircleCheckBox.UseVisualStyleBackColor = true;
            this.drawSmallestCircleCheckBox.CheckedChanged += new System.EventHandler(this.drawSmallestCircleCheckBox_CheckedChanged);
            this.drawSmallestCircleCheckBox.Click += new System.EventHandler(this.drawSmallestCircleCheckBox_Click);
            // 
            // findCircleAreaRadioButton
            // 
            this.findCircleAreaRadioButton.AutoSize = true;
            this.findCircleAreaRadioButton.Location = new System.Drawing.Point(262, 33);
            this.findCircleAreaRadioButton.Name = "findCircleAreaRadioButton";
            this.findCircleAreaRadioButton.Size = new System.Drawing.Size(69, 22);
            this.findCircleAreaRadioButton.TabIndex = 1;
            this.findCircleAreaRadioButton.Text = "找圆";
            this.findCircleAreaRadioButton.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 842);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.wlTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wwTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button openImageButton;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox WLWWCheckBox;
        private System.Windows.Forms.TrackBar wlTrackBar;
        private System.Windows.Forms.TrackBar wwTrackBar;
        private System.Windows.Forms.Label wwLabel;
        private System.Windows.Forms.Button fitSmallestCircleButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton autoWLWWRadioButton;
        private System.Windows.Forms.RadioButton noneRadioButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label wlLabel;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox drawCenterCrossRadioButton;
        private System.Windows.Forms.CheckBox drawSmallestCircleCheckBox;
        private System.Windows.Forms.RadioButton findCircleAreaRadioButton;
    }
}

