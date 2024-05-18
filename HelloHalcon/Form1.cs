using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class Form1 : Form
    {
        public HalconWindowTools hWindowTool;

        public bool mouseLeftButDown;
        private int mouseLeftDownRow;
        private int mouseLeftDownCol;

        public Form1()
        {
            InitializeComponent();

            hWindowTool = new HalconWindowTools(hWindowControl);

            this.hWindowControl.HMouseDown += HWindowControl_HMouseDown;
            this.hWindowControl.HMouseMove += HWindowControl_HMouseMove;
            this.hWindowControl.HMouseUp += HWindowControl_HMouseUp;
            this.hWindowControl.HMouseWheel += HWindowControl_HMouseWheel;
        }

        private void HWindowControl_HMouseDown(object sender, HMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//鼠标点下左键
            {
                mouseLeftButDown = true;
                hWindowControl.HalconWindow.GetMposition(out mouseLeftDownRow, out mouseLeftDownCol, out _);
                if (e.Clicks == 2) hWindowTool.DispImageFit(hWindowTool.ShowingImage);
            }
        }


        private void HWindowControl_HMouseUp(object sender, HMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//鼠标左键松开
            {
                mouseLeftButDown = false;
                try
                {
                    hWindowControl.HalconWindow.GetMposition(out _, out _, out _);
                }
                catch { }
            }
        }


        private void HWindowControl_HMouseMove(object sender, HMouseEventArgs e)
        {
            // 鼠标按下时进行移动
            if (mouseLeftButDown)
            {
                hWindowTool.DispImageMove(mouseLeftDownRow, mouseLeftDownCol);
            }
            else
            {
                try
                {
                    if (hWindowTool.ShowingImage == null) return;
                    hWindowControl.HalconWindow.GetMposition(out var mousePostRow, out var mousePostCol, out _);
                    HOperatorSet.GetImageSize(hWindowTool.ShowingImage, out var imageWidth, out var imageHeight);
                    HTuple grayValue = 0;
                    if (mousePostRow > 0 && mousePostCol > 0 && mousePostRow < imageHeight && mousePostCol < imageWidth)
                    {
                        HOperatorSet.GetGrayval(hWindowTool.origImage, mousePostRow, mousePostCol, out grayValue);
                        grayValue = grayValue[0];
                    }
                    {
                        textBox1.Text = mousePostCol.ToString(CultureInfo.InvariantCulture);
                        textBox2.Text = mousePostRow.ToString(CultureInfo.InvariantCulture);
                        textBox3.Text = grayValue.ToString();
                    }
                }
                catch { }
            }
        }


        private void HWindowControl_HMouseWheel(object sender, HMouseEventArgs e)
        {
            HTuple mode = e.Delta;
            hWindowTool.DispImageZoom(mode);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filename = "";
            OpenFileDialog dlg = new OpenFileDialog();
            //dlg.Filter = "Tiff文件|*.tif|Bmp文件|*.bmp|Erdas img文件|*.img|EVNI文件|*.hdr|jpeg文件|*.jpg|raw文件|*.raw|vrt文件|*.vrt|所有文件|*.*";
            //dlg.Filter = "Tiff文件|*.tif|Png文件|*.png|Bmp文件|*.bmp|jpeg文件|*.jpg";
            //dlg.Filter = "图像文件(*.tif;*.png;*.bmp;*.jpg)|*.tif;*.png;*.bmp;*.jpg|所有文件(*.*)|*.*";
            dlg.Filter = "图像文件(*.tif;*.png;*.bmp;*.jpg)|*.tif;*.png;*.bmp;*.jpg";
            dlg.FilterIndex = 0;
            dlg.Title = "打开图像";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                filename = dlg.FileName;
            }
            if (filename == "")
            {
                return;
            }

            try
            {
                // 加载图像
                hWindowTool.OpenImage(filename);

                // 可以添加更多的图像处理代码
            }
            catch (HalconException ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        public int WL;
        public int WW;

        private void WLBar_Scroll(object sender, EventArgs e)
        {
            WL = WLBar.Value;
            WLtextBox.Text = WL.ToString();
            hWindowTool.UpdataImage(hWindowTool.origImage, winTechnologyCheckBox.Checked, WL, WW);
        }

        private void WWBar_Scroll(object sender, EventArgs e)
        {
            WW = WWBar.Value;
            WWtextBox.Text = WW.ToString();
            hWindowTool.UpdataImage(hWindowTool.origImage, winTechnologyCheckBox.Checked, WL, WW);
        }
    }



}
