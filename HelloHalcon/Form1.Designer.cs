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
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.hWindowControl = new HalconDotNet.HWindowControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.winTechnologyCheckBox = new System.Windows.Forms.CheckBox();
            this.WLtextBox = new System.Windows.Forms.TextBox();
            this.WWtextBox = new System.Windows.Forms.TextBox();
            this.WLBar = new System.Windows.Forms.TrackBar();
            this.WWBar = new System.Windows.Forms.TrackBar();
            this.WWLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WLBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WWBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1140, 100);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(146, 48);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.hWindowControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1140, 642);
            this.panel2.TabIndex = 0;
            // 
            // hWindowControl
            // 
            this.hWindowControl.BackColor = System.Drawing.Color.Black;
            this.hWindowControl.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl.Location = new System.Drawing.Point(12, 6);
            this.hWindowControl.Name = "hWindowControl";
            this.hWindowControl.Size = new System.Drawing.Size(1116, 630);
            this.hWindowControl.TabIndex = 0;
            this.hWindowControl.WindowSize = new System.Drawing.Size(1116, 630);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.winTechnologyCheckBox);
            this.panel3.Controls.Add(this.WLtextBox);
            this.panel3.Controls.Add(this.WWtextBox);
            this.panel3.Controls.Add(this.WLBar);
            this.panel3.Controls.Add(this.WWBar);
            this.panel3.Controls.Add(this.WWLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 742);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1140, 100);
            this.panel3.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(165, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 28);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(271, 26);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 28);
            this.textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(377, 26);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 28);
            this.textBox3.TabIndex = 1;
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
            // WLtextBox
            // 
            this.WLtextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.WLtextBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WLtextBox.ForeColor = System.Drawing.Color.White;
            this.WLtextBox.Location = new System.Drawing.Point(486, 17);
            this.WLtextBox.Margin = new System.Windows.Forms.Padding(4);
            this.WLtextBox.Name = "WLtextBox";
            this.WLtextBox.Size = new System.Drawing.Size(85, 35);
            this.WLtextBox.TabIndex = 26;
            // 
            // WWtextBox
            // 
            this.WWtextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(57)))), ((int)(((byte)(57)))));
            this.WWtextBox.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WWtextBox.ForeColor = System.Drawing.Color.White;
            this.WWtextBox.Location = new System.Drawing.Point(814, 17);
            this.WWtextBox.Margin = new System.Windows.Forms.Padding(4);
            this.WWtextBox.Name = "WWtextBox";
            this.WWtextBox.Size = new System.Drawing.Size(85, 35);
            this.WWtextBox.TabIndex = 27;
            // 
            // WLBar
            // 
            this.WLBar.LargeChange = 500;
            this.WLBar.Location = new System.Drawing.Point(284, 15);
            this.WLBar.Margin = new System.Windows.Forms.Padding(4);
            this.WLBar.Maximum = 50000;
            this.WLBar.Minimum = 100;
            this.WLBar.Name = "WLBar";
            this.WLBar.Size = new System.Drawing.Size(200, 69);
            this.WLBar.SmallChange = 100;
            this.WLBar.TabIndex = 24;
            this.WLBar.Value = 10000;
            this.WLBar.Scroll += new System.EventHandler(this.WLBar_Scroll);
            // 
            // WWBar
            // 
            this.WWBar.LargeChange = 500;
            this.WWBar.Location = new System.Drawing.Point(613, 17);
            this.WWBar.Margin = new System.Windows.Forms.Padding(4);
            this.WWBar.Maximum = 20000;
            this.WWBar.Minimum = 30;
            this.WWBar.Name = "WWBar";
            this.WWBar.Size = new System.Drawing.Size(200, 69);
            this.WWBar.SmallChange = 100;
            this.WWBar.TabIndex = 25;
            this.WWBar.Value = 10000;
            this.WWBar.Scroll += new System.EventHandler(this.WWBar_Scroll);
            // 
            // WWLabel
            // 
            this.WWLabel.AutoSize = true;
            this.WWLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.WWLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.WWLabel.Location = new System.Drawing.Point(579, 20);
            this.WWLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WWLabel.Name = "WWLabel";
            this.WWLabel.Size = new System.Drawing.Size(34, 24);
            this.WWLabel.TabIndex = 23;
            this.WWLabel.Text = "WW";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1140, 842);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WLBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WWBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private HalconDotNet.HWindowControl hWindowControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox winTechnologyCheckBox;
        private System.Windows.Forms.TextBox WLtextBox;
        private System.Windows.Forms.TextBox WWtextBox;
        private System.Windows.Forms.TrackBar WLBar;
        private System.Windows.Forms.TrackBar WWBar;
        private System.Windows.Forms.Label WWLabel;
    }
}

