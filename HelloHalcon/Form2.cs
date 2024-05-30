using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloHalcon
{
    public partial class Form2 : Form
    {
        HObject smallImage;
        HObject mediumImage;
        HObject largeImage;

        public Form2()
        {
            InitializeComponent();
            // 初始化HWindowControl控件
            //hWindowControl1 = new HWindowControl();
            //hWindowControl1.Dock = DockStyle.Fill;
            //this.Controls.Add(hWindowControl1);

            // 创建不同尺寸和颜色的图像
            smallImage = CreateColoredImage(512, 512, "red"); // 小图像，红色
            mediumImage = CreateColoredImage(1024, 1024, "green"); // 中等图像，绿色
            largeImage = CreateColoredImage(1536, 1536, "blue"); // 大图像，蓝色

            // 最大化窗口
            this.WindowState = FormWindowState.Maximized;
        }

        private HObject CreateColoredImage(int width, int height, string color)
        {
            IntPtr redBuffer = Marshal.AllocHGlobal(width * height);
            IntPtr greenBuffer = Marshal.AllocHGlobal(width * height);
            IntPtr blueBuffer = Marshal.AllocHGlobal(width * height);
           
            byte[] red = new byte[width * height];
            byte[] green = new byte[width * height];
            byte[] blue = new byte[width * height];

            if (color == "red")
            {
                for (int i = 0; i < red.Length; i++) red[i] = 255;
                Marshal.Copy(red, 0, redBuffer, width * height);
            }
            else if (color == "green")
            {
                for (int i = 0; i < green.Length; i++) green[i] = 255;
                Marshal.Copy(green, 0, greenBuffer, width * height);
            }
            else if (color == "blue")
            {
                for (int i = 0; i < blue.Length; i++) blue[i] = 255;
                Marshal.Copy(blue, 0, blueBuffer, width * height);
            }

            HOperatorSet.GenImage3(out HObject image, "byte", width, height, redBuffer, greenBuffer, blueBuffer);
            Marshal.FreeHGlobal(redBuffer);
            Marshal.FreeHGlobal(greenBuffer);
            Marshal.FreeHGlobal(blueBuffer);
            return image;
        }

        private void DisplayImages(HObject img1, HObject img2, HObject img3)
        {
            //hWindowControl1.HalconWindow.SetColor("white");
            //hWindowControl1.HalconWindow.DispObj(new HImage());

            //HTuple width, height;

            //HOperatorSet.GetImageSize(img1, out width, out height);
            // 显示第一张图像
            //HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, height - 1, width - 1);
            hWindowControl1.HalconWindow.DispObj(img1);

            // 显示第二张图像，偏移一定距离
            //HOperatorSet.GetImageSize(img2, out width, out height);
            //HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, height - 1, xOffset + width - 1);
            hWindowControl1.HalconWindow.DispObj(img2);

            // 显示第三张图像，再偏移一定距离
            //HOperatorSet.GetImageSize(img3, out width, out height);
            //HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, height, xOffset + width - 1);
            hWindowControl1.HalconWindow.DispObj(img3);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 释放资源
            base.OnFormClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 清除窗口
            hWindowControl1.HalconWindow.ClearWindow();
            // 设置窗口背景色
            HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, 2048, 2048);
            // 显示图像
            DisplayImages(largeImage, mediumImage, smallImage);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 清除窗口
            hWindowControl1.HalconWindow.ClearWindow();
            // 设置窗口背景色
            HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 256, 256, 1024+256);
            // 显示图像
            DisplayImages(largeImage, mediumImage, smallImage);
        }

        HObject _prodImage = null;

        private void button3_Click(object sender, EventArgs e)
        {
            if (_prodImage == null)
            {
                _prodImage = CreateProbeImage();
            }

            // 清除窗口
            hWindowControl1.HalconWindow.ClearWindow();
            // 设置窗口背景色
            HOperatorSet.SetPart(hWindowControl1.HalconWindow, 0, 0, 1024, 3072);

            hWindowControl1.HalconWindow.DispObj(_prodImage);
        }

        private HObject CreateProbeImage()
        {
            // 创建一个 3072 x 1024 的空白图像
            HImage hImage = new HImage();
            hImage.GenImageConst("byte", 3072, 1024);

            // 创建一个画笔 (绘制圆的灰度值为255)
            HObject hCircle;
            HOperatorSet.GenCircle(out hCircle, 512, 512, 128); // 圆心在(512, 1536)，半径为 256

            // 在图像上绘制圆
            HOperatorSet.PaintRegion(hCircle, hImage, out HObject prodImage, 255, "fill");

            hImage.Dispose();
            hCircle.Dispose();

            return prodImage;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // 加载16位灰度图像
            HImage hImage = new HImage(@"C:\Users\Administrator\Desktop\CxAAC\vision-probe-image.tif"); // 假设图像格式是 TIFF，你可以根据实际情况更改
            HTuple width, height;
            hImage.GetImageSize(out width, out height);

            // 检查图像尺寸是否为3072x3072
            if (width != 3072 || height != 3072)
            {
                Console.WriteLine("图像尺寸不是 3072 x 3072");
                return;
            }

            // 分割图像
            int partHeight = 1024;
            for (int i = 0; i < 3; i++)
            {
                // 提取图像的一部分
                HImage partImage = hImage.CropRectangle1(i * partHeight, 0, (i + 1) * partHeight - 1, 3071);

                // 转换为8位图像
                HObject partImage8Bit = ConvertTo8Bit(partImage);

                // 保存为8位BMP灰度图像
                string outputPath = $"part_{i + 1}.bmp";
                HOperatorSet.WriteImage(partImage8Bit, "bmp", 0, outputPath);

                Console.WriteLine($"部分图像已保存到: {outputPath}");
            }
        }

        static HObject ConvertTo8Bit(HObject image16Bit)
        {
            // 获取图像的最小值和最大值
            HOperatorSet.MinMaxGray(image16Bit, image16Bit, 0, out HTuple minVal, out HTuple maxVal, out _);

            // 线性缩放图像到 8 位
            HOperatorSet.ScaleImage(image16Bit, out HObject image8Bit, 255.0 / (maxVal.D - minVal.D), -minVal.D * 255.0 / (maxVal.D - minVal.D));
            
            // 将图像强制转换为8位
            HOperatorSet.ConvertImageType(image8Bit, out image8Bit, "byte");
            return image8Bit;
        }
    }
}
