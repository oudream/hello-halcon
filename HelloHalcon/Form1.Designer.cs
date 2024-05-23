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
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.fitSmallestCircleButton = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.openImageButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.winTechnologyCheckBox = new System.Windows.Forms.CheckBox();
            this.wlTextBox = new System.Windows.Forms.TextBox();
            this.wwTextBox = new System.Windows.Forms.TextBox();
            this.wlBar = new System.Windows.Forms.TrackBar();
            this.wwBar = new System.Windows.Forms.TrackBar();
            this.wwLabel = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wlBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wwBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.fitSmallestCircleButton);
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
            this.groupBox1.Controls.Add(this.autoWLWWRadioButton);
            this.groupBox1.Controls.Add(this.noneRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(1034, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 66);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "画框的作用";
            // 
            // autoWLWWRadioButton
            // 
            this.autoWLWWRadioButton.AutoSize = true;
            this.autoWLWWRadioButton.Checked = true;
            this.autoWLWWRadioButton.Location = new System.Drawing.Point(115, 33);
            this.autoWLWWRadioButton.Name = "autoWLWWRadioButton";
            this.autoWLWWRadioButton.Size = new System.Drawing.Size(141, 22);
            this.autoWLWWRadioButton.TabIndex = 0;
            this.autoWLWWRadioButton.TabStop = true;
            this.autoWLWWRadioButton.Text = "自动窗宽窗位";
            this.autoWLWWRadioButton.UseVisualStyleBackColor = true;
            // 
            // noneRadioButton
            // 
            this.noneRadioButton.AutoSize = true;
            this.noneRadioButton.Location = new System.Drawing.Point(15, 33);
            this.noneRadioButton.Name = "noneRadioButton";
            this.noneRadioButton.Size = new System.Drawing.Size(69, 22);
            this.noneRadioButton.TabIndex = 0;
            this.noneRadioButton.Text = "NONE";
            this.noneRadioButton.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(777, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 82);
            this.button3.TabIndex = 2;
            this.button3.Text = "画中框";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(680, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 82);
            this.button2.TabIndex = 2;
            this.button2.Text = "适应大小";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(583, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 82);
            this.button1.TabIndex = 2;
            this.button1.Text = "1:1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // fitSmallestCircleButton
            // 
            this.fitSmallestCircleButton.Location = new System.Drawing.Point(486, 12);
            this.fitSmallestCircleButton.Name = "fitSmallestCircleButton";
            this.fitSmallestCircleButton.Size = new System.Drawing.Size(97, 82);
            this.fitSmallestCircleButton.TabIndex = 2;
            this.fitSmallestCircleButton.Text = "外接圆";
            this.fitSmallestCircleButton.UseVisualStyleBackColor = true;
            this.fitSmallestCircleButton.Click += new System.EventHandler(this.fitSmallestCircleButton_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(377, 26);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 28);
            this.textBox3.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(271, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 28);
            this.textBox2.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(165, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 28);
            this.textBox1.TabIndex = 1;
            // 
            // openImageButton
            // 
            this.openImageButton.Location = new System.Drawing.Point(12, 12);
            this.openImageButton.Name = "openImageButton";
            this.openImageButton.Size = new System.Drawing.Size(147, 82);
            this.openImageButton.TabIndex = 0;
            this.openImageButton.Text = "打开图像";
            this.openImageButton.UseVisualStyleBackColor = true;
            this.openImageButton.Click += new System.EventHandler(this.openImageButton_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.hWindowControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1400, 642);
            this.panel2.TabIndex = 0;
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(0, 0);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(1400, 642);
            this.hWindowControl.TabIndex = 0;
            this.hWindowControl.WindowSize = new System.Drawing.Size(1400, 642);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.winTechnologyCheckBox);
            this.panel3.Controls.Add(this.wlTextBox);
            this.panel3.Controls.Add(this.wwTextBox);
            this.panel3.Controls.Add(this.wlBar);
            this.panel3.Controls.Add(this.wwBar);
            this.panel3.Controls.Add(this.wwLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 742);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1400, 100);
            this.panel3.TabIndex = 0;
            // 
            // winTechnologyCheckBox
            // 
            this.winTechnologyCheckBox.AutoSize = true;
            this.winTechnologyCheckBox.Checked = true;
            this.winTechnologyCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.winTechnologyCheckBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.winTechnologyCheckBox.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.winTechnologyCheckBox.Location = new System.Drawing.Point(241, 19);
            this.winTechnologyCheckBox.Name = "winTechnologyCheckBox";
            this.winTechnologyCheckBox.Size = new System.Drawing.Size(60, 28);
            this.winTechnologyCheckBox.TabIndex = 28;
            this.winTechnologyCheckBox.Text = "WL";
            this.winTechnologyCheckBox.UseVisualStyleBackColor = true;
            // 
            // wlTextBox
            // 
            this.wlTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.wlTextBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wlTextBox.ForeColor = System.Drawing.Color.White;
            this.wlTextBox.Location = new System.Drawing.Point(486, 17);
            this.wlTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.wlTextBox.Name = "wlTextBox";
            this.wlTextBox.Size = new System.Drawing.Size(85, 35);
            this.wlTextBox.TabIndex = 26;
            // 
            // wwTextBox
            // 
            this.wwTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.wwTextBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wwTextBox.ForeColor = System.Drawing.Color.White;
            this.wwTextBox.Location = new System.Drawing.Point(814, 17);
            this.wwTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.wwTextBox.Name = "wwTextBox";
            this.wwTextBox.Size = new System.Drawing.Size(85, 35);
            this.wwTextBox.TabIndex = 27;
            // 
            // wlBar
            // 
            this.wlBar.LargeChange = 500;
            this.wlBar.Location = new System.Drawing.Point(284, 15);
            this.wlBar.Margin = new System.Windows.Forms.Padding(4);
            this.wlBar.Maximum = 50000;
            this.wlBar.Minimum = 100;
            this.wlBar.Name = "wlBar";
            this.wlBar.Size = new System.Drawing.Size(200, 69);
            this.wlBar.SmallChange = 100;
            this.wlBar.TabIndex = 24;
            this.wlBar.Value = 10000;
            // 
            // wwBar
            // 
            this.wwBar.LargeChange = 500;
            this.wwBar.Location = new System.Drawing.Point(613, 17);
            this.wwBar.Margin = new System.Windows.Forms.Padding(4);
            this.wwBar.Maximum = 20000;
            this.wwBar.Minimum = 30;
            this.wwBar.Name = "wwBar";
            this.wwBar.Size = new System.Drawing.Size(200, 69);
            this.wwBar.SmallChange = 100;
            this.wwBar.TabIndex = 25;
            this.wwBar.Value = 10000;
            // 
            // wwLabel
            // 
            this.wwLabel.AutoSize = true;
            this.wwLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.wwLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.wwLabel.Location = new System.Drawing.Point(579, 20);
            this.wwLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.wwLabel.Name = "wwLabel";
            this.wwLabel.Size = new System.Drawing.Size(34, 24);
            this.wwLabel.TabIndex = 23;
            this.wwLabel.Text = "WW";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(874, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 82);
            this.button4.TabIndex = 2;
            this.button4.Text = "清除框";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
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
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.wlBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wwBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.Button openImageButton;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox winTechnologyCheckBox;
        private System.Windows.Forms.TextBox wlTextBox;
        private System.Windows.Forms.TextBox wwTextBox;
        private System.Windows.Forms.TrackBar wlBar;
        private System.Windows.Forms.TrackBar wwBar;
        private System.Windows.Forms.Label wwLabel;
        private System.Windows.Forms.Button fitSmallestCircleButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton autoWLWWRadioButton;
        private System.Windows.Forms.RadioButton noneRadioButton;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

