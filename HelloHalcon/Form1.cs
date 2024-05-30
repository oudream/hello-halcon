using HalconDotNet;
using OpenCvSharp;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

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
                WLWWCheckBox, wlTrackBar, wlLabel, wwTrackBar, wwLabel, noneRadioButton, autoWLWWRadioButton);
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

        private void button5_Click(object sender, EventArgs e)
        {
            //HalconHelperTester.TestOverlayImage();

            //for (int i = 0; i < 100; i++)
            //{
            //    TestGenImage1();
            //}

            //TestContour();

            //HalconHelperTester.HObjectToHImage();

            //TestSave();

            //TestGray2Rgb2();

            HImage image = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            for (int i = 0; i < 100; i++)
            {
                TestPaint3(image);
            }

            //TestHImageToMat();

            //TestBinImage();
        }

        // 生成二值图
        private void TestBinImage()
        {
            // 假设图像数据存储在一个byte数组中，大小为3072*3072
            int width = 3072;
            var height = 3072;
            byte[] imageData = new byte[width * height];

            // 填充imageData数据 (这里只是示例，实际数据需要从文件或其他来源获取)
            for (int i = 0; i < imageData.Length; i++)
            {
                imageData[i] = (byte)(i % 2); // 生成0和1的交替数据
            }
            // 创建一个新的数组用于映射像素值
            byte[] displayData = new byte[width * height];
            for (int i = 0; i < imageData.Length; i++)
            {
                displayData[i] = (byte)(imageData[i] * 255);
            }

            // 将 byte[] 转换为 IntPtr
            IntPtr unmanagedBuffer = Marshal.AllocHGlobal(3072 * 3072);
            Marshal.Copy(displayData, 0, unmanagedBuffer, 3072 * 3072);

            // 创建一个HObject图像
            HObject hObject;
            HOperatorSet.GenImage1(out hObject, "byte", width, height, unmanagedBuffer);
            //HOperatorSet.Threshold(hObject, out HObject binaryImage, 0, 1);

            _hWindowControlView.OpenImage(new HImage(hObject));

            Marshal.FreeHGlobal(unmanagedBuffer);
            hObject.Dispose();
        }

        public void TestGenImage1()
        {
            int imgWidth = 3072;
            int imgHeight = 3072;
            int iLen = imgWidth * imgHeight * sizeof(ushort);

            // 分配内存并填充测试数据
            IntPtr imageData = Marshal.AllocHGlobal(iLen);
            unsafe
            {
                ushort* pImageData = (ushort*)imageData.ToPointer();
                for (int i = 0; i < imgWidth * imgHeight; i++)
                {
                    pImageData[i] = (ushort)(i % 65536);  // 填充一些测试数据
                }
            }

            // 调用测试函数
            ImageDebuggingOne(1, imageData, imgWidth, imgHeight);
        }

        public void ImageDebuggingOne(int frameNo, IntPtr imgData, int imgWidth, int imgHeight)
        {
            HOperatorSet.GenImage1(out HObject image16Grey, "uint2", imgWidth, imgHeight, imgData);

            //HObject image16Grey1 = image16Grey;
            //// 释放传入的内存
            //unsafe
            //{
            //    ushort* pImageData = (ushort*)imgData.ToPointer();
            //    for (int i = 0; i < imgWidth * imgHeight; i++)
            //    {
            //        pImageData[i] = 0;  // 填充一些测试数据
            //    }
            //}
            Marshal.FreeHGlobal(imgData);

            // 尝试访问图像数据以验证图像是否有效
            try
            {
                HOperatorSet.DispObj(image16Grey, this.hWindowControl.HalconWindow);
                Console.WriteLine($"图像帧 {frameNo} 已成功显示。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"图像帧 {frameNo} 无法显示: {ex.Message}");
            }

            // 释放图像对象
            image16Grey.Dispose();
        }

        public void ContourDebuggingOne(HObject img)
        {
            // 从图像中提取轮廓
            HOperatorSet.Threshold(img, out HObject region, 128, 255);
            HOperatorSet.Connection(region, out HObject connectedRegions);
            HOperatorSet.SelectShape(connectedRegions, out HObject selectedRegions, "area", "and", 500, 99999);
            HOperatorSet.GenContourRegionXld(selectedRegions, out HObject contours, "border");


            //// 定义固定区域 (例如：一个矩形区域)
            //HTuple row1 = 100, column1 = 100, row2 = 300, column2 = 300;
            //HOperatorSet.GenRectangle1(out HObject rectangle, row1, column1, row2, column2);

            //// 限制处理区域
            //HOperatorSet.ReduceDomain(img, rectangle, out HObject reducedImg);

            // 删除原图像
            img.Dispose();

            // 尝试访问轮廓数据以验证轮廓是否有效
            try
            {
                HOperatorSet.DispObj(contours, this.hWindowControl.HalconWindow);
                Console.WriteLine("轮廓已成功显示。");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"轮廓无法显示: {ex.Message}");
            }

            // 释放资源
            region.Dispose();
            connectedRegions.Dispose();
            selectedRegions.Dispose();
            contours.Dispose();
        }

        public void TestContour()
        {
            // 加载测试图像
            HOperatorSet.ReadImage(out HObject img, @"E:\image9\1.jpg"); // 替换为实际的图像路径

            // 调用测试函数
            ContourDebuggingOne(img);

            //img.Dispose();
        }

        public void ConvertHObjectToHImage1()
        {
            // 创建一个示例图像
            HImage image = new HImage("byte", 100, 100);

            // 将图像转换为 HObject
            HObject hObject = image;

            // 再从 HObject 转换回 HImage
            HImage convertedImage = new HImage(hObject);

            // 检查转换后的图像是否有效
            Console.WriteLine(convertedImage);
            Console.WriteLine($"{convertedImage.CountChannels()} {image.CountChannels()}");

            // 清理资源
            hObject.Dispose();
            image.Dispose();
            convertedImage.Dispose();
        }

        public void TestSave()
        {
            // 示例用法
            HImage image = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            // 保存图像为不同格式的示例
            SaveImage(image, "output_image.tiff");
            SaveImage(image, "output_image.png");

            // 创建三个通道
            HImage byteImage = image.ScaleImageMax();
            //HImage redChannel, greenChannel, blueChannel;
            //redChannel = byteImage.Clone();
            //greenChannel = byteImage.Clone();
            //blueChannel = byteImage.Clone();

            //// 合并三个通道为一个彩色图像
            //HImage colorImage = new HImage();
            //byteImage.GetImageSize(out int width, out int height);
            //colorImage.GenImage3("byte", width, height, redChannel, greenChannel, blueChannel);

            //var rgbImage = HalconHelper.Gray2Rgb(image);

            SaveImage(byteImage, "output_image.jpeg");

            // 释放资源
            image.Dispose();
        }

        /// <summary>
        /// 保存图像到指定路径，根据文件名自动匹配格式
        /// </summary>
        /// <param name="image">输入的HObject图像对象</param>
        /// <param name="imageFilePath">图像保存路径</param>
        public static void SaveImage(HObject image, string imageFilePath)
        {
            // 根据扩展名匹配格式
            string format = HalconHelper.GetFormatByExtension(imageFilePath);

            // 如果未找到匹配格式，抛出异常
            if (string.IsNullOrEmpty(format))
            {
                throw new ArgumentException($"Unsupported file extension: {format}");
            }

            // 保存图像
            HOperatorSet.WriteImage(image, format, 0, imageFilePath);
        }

        public void TestGray2Rgb()
        {
            // 读取16位灰度图像
            HImage grayImage16 = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            // 获取图像宽度和高度
            HTuple width, height;
            grayImage16.GetImageSize(out width, out height);

            // 将16位灰度图像归一化为8位灰度图像
            HImage grayImage8 = grayImage16.ScaleImageMax();

            // 获取8位灰度图像的数据指针
            HTuple pointer, type;
            pointer = grayImage8.GetImagePointer1(out type, out width, out height);

            // 生成RGB图像的三个通道，初始化为灰度图像数据
            HImage redChannel = new HImage("byte", width, height, pointer);
            HImage greenChannel = new HImage("byte", width, height, pointer);
            HImage blueChannel = new HImage("byte", width, height, pointer);

            // 创建一个RGB图像
            HImage rgbImage = new HImage();
            rgbImage.GenImage3("byte", width, height, redChannel.GetImagePointer1(out type, out _, out _), greenChannel.GetImagePointer1(out type, out _, out _), blueChannel.GetImagePointer1(out type, out _, out _));

            _hWindowControlView.OpenImage(rgbImage);
        }

        public void TestGray2Rgb2()
        {
            // 读取16位灰度图像
            HImage grayImage16 = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            var info = HalconHelper.GetImageInfoAsString(grayImage16);
            Console.WriteLine(info);

            // 将16位灰度图像归一化为8位灰度图像
            HImage grayImage8 = grayImage16.ScaleImageMax();

            info = HalconHelper.GetImageInfoAsString(grayImage8);
            Console.WriteLine(info);

            HImage rgbImage8 = grayImage8.Compose3(grayImage8, grayImage8);

            _hWindowControlView.OpenImage(rgbImage8);

            info = HalconHelper.GetImageInfoAsString(rgbImage8);
            Console.WriteLine(info);

            HOperatorSet.WriteImage(rgbImage8, "jpeg", 0, "temp_rgbImage8.jpg");

            grayImage8.Dispose();
            grayImage16.Dispose();
        }

        public void TestGray2Rgb3()
        {
            // 读取16位灰度图像
            HImage grayImage16 = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            var info = HalconHelper.GetImageInfoAsString(grayImage16);
            Console.WriteLine(info);

            HImage rgbImage8 = grayImage16.Compose3(grayImage16, grayImage16);

            _hWindowControlView.OpenImage(rgbImage8);

            info = HalconHelper.GetImageInfoAsString(rgbImage8);
            Console.WriteLine(info);

            HOperatorSet.WriteImage(rgbImage8, "jpeg", 0, "temp_rgbImage8.jpg");

            grayImage16.Dispose();
        }

        public void TestPaint()
        {
            // 读取图像
            HImage image = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            // 设置显示窗口
            HOperatorSet.OpenWindow(0, 0, 512, 512, 0, "visible", "", out HTuple windowHandle);
            HOperatorSet.SetPart(windowHandle, 0, 0, -1, -1);
            HOperatorSet.DispObj(image, windowHandle);

            // 在图像上创建覆盖层
            HOperatorSet.SetColor(windowHandle, "red");
            HOperatorSet.SetDraw(windowHandle, "margin");
            HOperatorSet.SetLineWidth(windowHandle, 3);
            HOperatorSet.DrawRectangle1(windowHandle, out HTuple row1, out HTuple column1, out HTuple row2, out HTuple column2);
            HOperatorSet.DispRectangle1(windowHandle, row1, column1, row2, column2);

            HOperatorSet.SetTposition(windowHandle, row1 - 10, column1);
            HOperatorSet.WriteString(windowHandle, "这是一个框");

            // 保存带有覆盖层的图像
            HOperatorSet.DumpWindowImage(out HObject imageWithGraphics, windowHandle);
            HOperatorSet.WriteImage(imageWithGraphics, "jpg", 0, "output_with_overlay.jpg");

            // 清理资源
            HOperatorSet.CloseWindow(windowHandle);
            HOperatorSet.ClearObj(image);
            HOperatorSet.ClearObj(imageWithGraphics);
        }

        public void TestPaint2()
        {
            // 初始化HALCON环境
            HImage image = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            // 创建矩形区域
            HOperatorSet.GenRectangle1(out HObject rectangle, 100, 100, 200, 200); // 参数为左上角和右下角的坐标

            // 创建圆形区域
            HOperatorSet.GenCircle(out HObject circle, 300, 300, 50); // 参数为圆心坐标和半径

            // 使用 paint_region 绘制形状
            HOperatorSet.PaintRegion(rectangle, image, out HObject imageWithGraphics, 210, "fill");
            HOperatorSet.PaintRegion(circle, imageWithGraphics, out imageWithGraphics, 110, "fill");

            // 创建文本区域（这里简化处理，实际应用中可能需要复杂的文字处理）
            HOperatorSet.GenRegionPoints(out HObject textRegion, 150, 150);

            // 保存图像
            HOperatorSet.WriteImage(imageWithGraphics, "jpeg", 0, "output_with_graphics.jpg");

            // 清理资源
            HOperatorSet.ClearObj(image);
            HOperatorSet.ClearObj(rectangle);
            HOperatorSet.ClearObj(circle);
            HOperatorSet.ClearObj(textRegion);
            HOperatorSet.ClearObj(imageWithGraphics);
        }

        private void TestPaint3(HImage image)
        {
            // 测量执行时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // 初始化 HALCON 窗口
            // 获取图像宽度和高度
            image.GetImageSize(out HTuple width, out HTuple height);

            // 创建虚拟 HALCON 窗口并设置为图像大小
            HTuple windowID;
            HOperatorSet.OpenWindow(0, 0, width, height, "root", "buffer", "", out windowID);
            //HDevWindowStack.Push(windowID);

            // 显示图像到虚拟窗口
            HOperatorSet.DispObj(image, windowID);

            // 设置绘制颜色和模式
            HOperatorSet.SetColor(windowID, "green");
            HOperatorSet.SetDraw(windowID, "fill");

            // 绘制多个矩形
            for (int i = 0; i < 10; i++)
            {
                HOperatorSet.SetColor(windowID, "green");
                HTuple row1 = 100 + i * 50, col1 = 100 + i * 50, row2 = 150 + i * 50, col2 = 150 + i * 50;
                HOperatorSet.DispRectangle1(windowID, row1, col1, row2, col2);
            }

            // 绘制多个圆形
            for (int i = 0; i < 10; i++)
            {
                HOperatorSet.SetColor(windowID, "blue");
                HTuple row = 300 + i * 50, col = 300 + i * 50, radius = 30;
                HOperatorSet.DispCircle(windowID, row, col, radius);
            }

            // 绘制多个多边形
            for (int i = 0; i < 10; i++)
            {
                HOperatorSet.SetColor(windowID, "yellow");
                HTuple rows = new HTuple(new double[] { 500 + i * 50, 520 + i * 50, 540 + i * 50 });
                HTuple cols = new HTuple(new double[] { 500 + i * 50, 520 + i * 50, 500 + i * 50 });
                HOperatorSet.DispPolygon(windowID, rows, cols);
            }

            // 绘制多个文本
            for (int i = 0; i < 10; i++)
            {
                HOperatorSet.SetColor(windowID, "red");
                HTuple rowText = 50 + i * 30, colText = 50;
                string text = $"Text {i + 1}";
                HOperatorSet.DispText(windowID, text, "image", rowText, colText, "red", "box", "false");
            }

            // 从虚拟窗口获取绘制后的图像
            HOperatorSet.DumpWindowImage(out HObject outputImage, windowID);

            stopwatch.Stop();
            Console.WriteLine($"画图耗时: {stopwatch.ElapsedMilliseconds} 毫秒"); // 36毫秒

            stopwatch.Restart();
            // 保存最终的图像
            HOperatorSet.WriteImage(outputImage, "jpeg 100", 0, "output_image.jpg");
            Console.WriteLine($"写图耗时: {stopwatch.ElapsedMilliseconds} 毫秒"); // 36毫秒

            //_hWindowControlView.OpenImage(new HImage(outputImage));

            // 关闭虚拟窗口并清理资源
            HOperatorSet.CloseWindow(windowID);
            outputImage.Dispose();
        }

        private void TestHImageToMat()
        {
            // 创建一个示例HImage
            HImage hImage = new HImage(@"E:\solder-ball-tif\Org_11AS.tif");

            // 将HImage转换为Mat
            Mat mat = HImageToMat(hImage);
            var dst = new Mat();
            // 对图像进行归一化处理，MinMax相当于把窗宽窗位拉满
            Cv2.Normalize(mat, dst, 0, 255, NormTypes.MinMax, MatType.CV_8UC1);

            var displayImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(dst);
            pictureBox1.Image = displayImage;
            //Console.WriteLine("HImage has been converted to Mat.");

            // 将Mat转换回HImage
            HImage convertedHImage = MatToHImage(mat);
            DispImageFit(convertedHImage);
            this.hWindowControl.HalconWindow.DispObj(convertedHImage);
            //Console.WriteLine("Mat has been converted back to HImage.");
        }
        public void DispImageFit(HImage convertedHImage)
        {
            if (convertedHImage == null) return;
            try
            {
                HOperatorSet.GetImageSize(convertedHImage, out HTuple img_width, out HTuple img_height);
                //int win_width = hw_ctrl.ImagePart.Width;
                //int win_height = hw_ctrl.ImagePart.Height;
                int win_width = hWindowControl.Size.Width;
                int win_height = hWindowControl.Size.Height;

                double w_ratio = 1.0 * img_width / win_width;
                double h_ratio = 1.0 * img_height / win_height;
                double ratio = (w_ratio > h_ratio ? w_ratio : h_ratio);
                double column1 = -(ratio * win_width - img_width) / 2.0;
                double row1 = -(ratio * win_height - img_height) / 2.0;
                double column2 = (ratio * win_width - img_width) / 2.0 + img_width;
                double row2 = (ratio * win_height - img_height) / 2.0 + img_height;

                HOperatorSet.SetPart(hWindowControl.HalconWindow, row1, column1, row2, column2);

                HOperatorSet.ClearWindow(hWindowControl.HalconWindow);
                HOperatorSet.DispObj(convertedHImage, hWindowControl.HalconWindow);
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message);
            }
        }

        static Mat HImageToMat(HImage hImage)
        {
            hImage.GetImageSize(out int width, out int height);
            IntPtr ptr = hImage.GetImagePointer1(out string type, out width, out height);

            if (type != "uint2")
            {
                throw new Exception("The HImage is not a 16-bit single-channel image.");
            }

            Mat mat = new Mat(height, width, MatType.CV_16UC1, ptr);
            return mat.Clone();
        }

        static HImage MatToHImage(Mat mat)
        {
            if (mat.Type() != MatType.CV_16UC1)
            {
                throw new Exception("The Mat is not a 16-bit single-channel image.");
            }

            int width = mat.Width;
            int height = mat.Height;
            IntPtr ptr = mat.Data;

            HImage hImage = new HImage();
            hImage.GenImage1("uint2", width, height, ptr);
            return hImage;
        }

        private void drawCenterCrossRadioButton_Click(object sender, EventArgs e)
        {
            if (_hWindowTools.DrawCenterCross != drawCenterCrossRadioButton.Checked)
            {
                _hWindowTools.DrawCenterCross = drawCenterCrossRadioButton.Checked;
                _hWindowTools.ReShowDrawALL();
            }
        }

        private void drawSmallestCircleCheckBox_Click(object sender, EventArgs e)
        {
            if (drawSmallestCircleCheckBox.Checked)
            {
                _hWindowTools.FitSmallestCircle();
                _hWindowTools.ReShowDrawALL();
            }
            else
            {
                _hWindowTools.ClearMinCircle();
            }
        }

        private void drawSmallestCircleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (drawSmallestCircleCheckBox.Checked && _hWindowTools.IsNullImage())
            {
                drawSmallestCircleCheckBox.Checked = false; // 禁用复选框
            }
        }

        private void fitSmallestCircleButton_Click_1(object sender, EventArgs e)
        {
            _hWindowTools.GetCirclePoint(this.hWindowControl);
        }
    }


}
