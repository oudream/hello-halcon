using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HelloHalcon
{
    // 注意，窗宽窗位只能 .tif 图像
    public partial class Form1 : Form
    {
        private HalconWindowTools _hWindowTools;
        private HWindowControlView _hWindowControlView;

        public Form1()
        {
            InitializeComponent();

            _hWindowTools = new HalconWindowTools(this.hWindowControl);

            _hWindowControlView = new HWindowControlView(_hWindowTools,
                winTechnologyCheckBox, wlBar, wlTextBox, wwBar, wwTextBox, noneRadioButton, autoWLWWRadioButton);
            _hWindowControlView.ShowImageInfo += HWindowControlView_ShowImageInfo;
        }

        private void HWindowControlView_ShowImageInfo(int x, int y, int grayscaleValue)
        {
            textBox1.Text = x.ToString();
            textBox2.Text = y.ToString();
            textBox3.Text = grayscaleValue.ToString();
        }

        // 打开图像
        private void openImageButton_Click(object sender, EventArgs e)
        {
            _hWindowControlView.OpenImage();
        }

        // 拟合最小外接圆
        private void fitSmallestCircleButton_Click(object sender, EventArgs e)
        {
            _hWindowTools.FitSmallestCircle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _hWindowTools.DisplayOriginalSizeImage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _hWindowTools.DisplayAdaptiveSizeImage();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _hWindowControlView.LoadRectanglesFromConfig("0,1024,3071,1024");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 设置窗体的 WindowState 属性为 Maximized
            this.WindowState = FormWindowState.Maximized;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _hWindowTools.ClearMinCircle();
        }
    }



}
