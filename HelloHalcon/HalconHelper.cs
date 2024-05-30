using HalconDotNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HelloHalcon
{
    public static class HalconHelper
    {
        // format Default value: 'tiff'
        // List of values: 'bigtiff alpha', 'bigtiff deflate 9 alpha', 'bigtiff deflate 9', 'bigtiff jpeg 90',
        //                 'bigtiff lzw alpha ', 'bigtiff lzw', 'bigtiff mask', 'bigtiff packbits', 'bigtiff',
        //                 'bmp', 'hobj', 'ima', 'jp2 20', 'jp2 30', 'jp2 40', 'jp2 50', 'jp2', 'jpeg 100', 'jpeg 20',
        //                 'jpeg 40', 'jpeg 60', 'jpeg 80', 'jpeg', 'jpegxr 20', 'jpegxr 30', 'jpegxr 40', 'jpegxr 50', 'jpegxr',
        //                 'png best', 'png fastest', 'png none', 'png',
        //                 'tiff alpha', 'tiff deflate 9 alpha', 'tiff deflate 9', 'tiff jpeg 90', 'tiff lzw alpha ',
        //                 'tiff lzw', 'tiff mask', 'tiff packbits', 'tiff'
        public static void SaveImage(HObject image, string imageFilePath, string format)
        {
            HOperatorSet.WriteImage(image, format, 0, imageFilePath);
        }

        public static void SaveImage(HObject image, string imageFilePath)
        {
            string format = GetFormatByExtension(imageFilePath);
            HOperatorSet.WriteImage(image, format, 0, imageFilePath);
        }

        public static HObject ReadImage(string imageFilePath)
        {
            HOperatorSet.ReadImage(out HObject hImage, imageFilePath);
            return hImage;
        }

        public static bool GetImageInfo(HObject image, out int width, out int height, out int channels, out int bitDepth, out IntPtr dataPtr)
        {
            if (image != null)
            {
                try
                {
                    // 获取图像的基本信息
                    HOperatorSet.GetImagePointer1(image, out HTuple hPointer, out HTuple hType, out HTuple hWidth, out HTuple hHeight);
                    //Console.WriteLine($"图像宽度: {width}");
                    //Console.WriteLine($"图像高度: {height}");
                    //Console.WriteLine($"图像类型: {type}");
                    width = hWidth.I;
                    height = hHeight.I;

                    dataPtr = hPointer.IP;

                    // 获取图像的通道数
                    HOperatorSet.CountChannels(image, out HTuple hChannels);
                    //Console.WriteLine($"图像通道数: {channels}");
                    channels = hChannels.I;

                    // 获取图像的位深（根据图像类型判断）
                    bitDepth = GetBitDepth(hType);
                    //Console.WriteLine($"图像位深: {bitDepth}");

                    return true;
                }
                catch (Exception)
                {
                }
            }
            width = 0;
            height = 0;
            channels = 0;
            bitDepth = 0;
            dataPtr = IntPtr.Zero;
            return false;
        }

        private static int GetBitDepth(HTuple type)
        {
            // 根据图像类型判断位深
            switch (type.S)
            {
                case "byte":
                case "uint1":
                    return 8;
                case "int2":
                case "uint2":
                    return 16; // HALCON does not have a direct uint2 type, usually it's 10-bit packed.
                case "int4":
                case "uint4":
                    return 32; // HALCON does not have a direct uint4 type, usually it's 12-bit packed.
                case "real":
                    return 32;
                default:
                    return -1; // 未知类型
            }
        }
        public static string GetImageInfoAsString(HObject image)
        {
            if (HalconHelper.GetImageInfo(image, out int imgWidth, out int imgHeight, out int imgChannels, out int imgBigDepth, out _))
            {
                var info = $"Width: {imgWidth}, Height: {imgHeight}, Channels: {imgChannels}, imgBigDepth: {imgBigDepth}";
                return info;
            }
            return "";
        }

        /// <summary>
        /// 获取图像的颜色空间
        /// </summary>
        /// <param name="image">输入的HObject对象</param>
        /// <returns>颜色空间的字符串表示（例如 "Gray", "RGB", "HSV", "YUV", "LAB", "CMYK"）</returns>
        /// 对于具有三个通道的图像，可能是 RGB、HSV、YUV、LAB 
        static string GetColorSpace(HObject image)
        {
            HOperatorSet.CountChannels(image, out HTuple channelCount);

            int iChannelCount = channelCount.I;

            if (channelCount == 1)
            {
                return "Gray";
            }
            else if (channelCount == 3)
            {
                // 由于 Halcon 没有直接的方法来确定颜色空间，假设为 RGB
                // 如果知道图像来源或经过转换，可以更准确地确定
                return "RGB";
            }
            else if (channelCount == 4)
            {
                // 一些图像可能有四个通道，例如 CMYK
                return "CMYK";
            }
            else
            {
                // 其他通道数（如 2 或 5 及以上），可能表示未知或特定的颜色空间
                return "Unknown";
            }
        }

        /// <summary>
        /// 获取图像的基本信息，包括宽度、高度、通道数、最小灰度值和最大灰度值
        /// </summary>
        /// <param name="image">输入的HObject对象</param>
        /// <returns>包含宽度、高度、通道数、最小灰度值和最大灰度值的元组</returns>
        static (double, double) GetMinMaxGray(HObject image)
        {
            // 获取图像的最小和最大灰度值
            HTuple minGray, maxGray;
            HOperatorSet.MinMaxGray(image, image, 0, out minGray, out maxGray, out _);

            return (minGray.D, maxGray.D);
        }

        /// <summary>
        /// 根据文件扩展名匹配图像格式
        /// </summary>
        /// <param name="extension">文件扩展名</param>
        /// <returns>图像格式字符串</returns>
        public static string GetFormatByExtension(string imageFilePath)
        {
            string extension = Path.GetExtension(imageFilePath).ToLower();
            switch (extension)
            {
                case ".tiff":
                case ".tif":
                    return "tiff";
                case ".jpeg":
                case ".jpg":
                    return "jpeg";
                case ".png":
                    return "png";
                case ".bmp":
                    return "bmp";
                case ".jp2":
                    return "jp2";
                case ".hobj":
                    return "hobj";
                // 添加更多需要支持的格式
                default:
                    return null;
            }
        }

        public static HObject OverlayImage2(List<HObject> images16bit)
        {
            if (images16bit == null || images16bit.Count == 0)
            {
                throw new ArgumentException("图像列表为空或未提供图像。");
            }

            // 获取第一张图像的尺寸和类型作为参考
            HObject image0 = images16bit[0];
            HTuple imgWidth, imgHeight;
            HOperatorSet.GetImageSize(image0, out imgWidth, out imgHeight);
            HOperatorSet.GetImageType(image0, out HTuple imgType);

            // 检查所有图像是否具有相同的尺寸和类型
            for (int i = 1; i < images16bit.Count; i++)
            {
                HObject img = images16bit[i];
                HTuple hWidth, hHeight;
                HOperatorSet.GetImageSize(img, out hWidth, out hHeight);
                HOperatorSet.GetImageType(img, out HTuple hType);
                if (hWidth.D != imgWidth.D || hHeight.D != imgHeight.D || hType.S != imgType.S)
                {
                    throw new ArgumentException("所有图像必须具有相同的尺寸和类型。");
                }
            }

            // 创建累积图像
            HOperatorSet.GenImageConst(out HObject overlayImage, "uint2", imgWidth, imgHeight);

            int queueNumber = images16bit.Count;
            for (int i = 0; i < queueNumber; i++)
            {
                HTuple multCrose = 1.0d / queueNumber;
                HObject imageTampe = images16bit[i];
                HOperatorSet.ScaleImage(imageTampe, out HObject imageScaled, multCrose, 0);
                HOperatorSet.AddImage(overlayImage, imageScaled, out HObject imageResult, 1.0d, 0);
                imageScaled.Dispose();
                overlayImage.Dispose();
                overlayImage = imageResult;
            }

            return overlayImage;
        }

        public static HObject OverlayImage3(List<HObject> images16bit)
        {
            if (images16bit == null || images16bit.Count == 0)
            {
                throw new ArgumentException("图像列表为空或未提供图像。");
            }

            // 获取第一张图像的尺寸和类型作为参考
            HObject image0 = images16bit[0];
            HTuple imgWidth, imgHeight;
            HOperatorSet.GetImageSize(image0, out imgWidth, out imgHeight);
            HOperatorSet.GetImageType(image0, out HTuple imgType);

            // 检查所有图像是否具有相同的尺寸和类型
            for (int i = 1; i < images16bit.Count; i++)
            {
                HObject img = images16bit[i];
                HTuple hWidth, hHeight;
                HOperatorSet.GetImageSize(img, out hWidth, out hHeight);
                HOperatorSet.GetImageType(img, out HTuple hType);
                if (hWidth.D != imgWidth.D || hHeight.D != imgHeight.D || hType.S != imgType.S)
                {
                    throw new ArgumentException("所有图像必须具有相同的尺寸和类型。");
                }
            }

            // 创建累积图像
            HOperatorSet.GenImageConst(out HObject overlayImage, "int4", imgWidth, imgHeight);

            int queueNumber = images16bit.Count;
            for (int i = 0; i < queueNumber; i++)
            {
                HObject imageTampe = images16bit[i];
                HOperatorSet.ConvertImageType(imageTampe, out HObject imgFloat, "int4");
                HOperatorSet.AddImage(overlayImage, imgFloat, out HObject imageResult, 1.0d, 0);
                imgFloat.Dispose();
                overlayImage.Dispose();
                overlayImage = imageResult;
            }

            // 计算平均图像
            //HObject averagedImage = overlayImage / images16bit.Count;
            HOperatorSet.ScaleImage(overlayImage, out HObject averagedImage, 1.0d / images16bit.Count, 0);

            // 将平均图像转换回16位以查看或保存
            HOperatorSet.ConvertImageType(averagedImage, out HObject averagedImage16bit, "uint2");

            averagedImage.Dispose();
            overlayImage.Dispose();

            return averagedImage16bit;
        }

        public static HObject WLWWTransferWithFull(HObject sourceImg)
        {
            HOperatorSet.ScaleImageMax(sourceImg, out HObject grayImage8);
            return grayImage8;
        }

        public static HObject WLWWTransferWithParallel(HObject sourceImg, int wl, int ww)
        {
            var imageCopy = sourceImg.Clone();
            HOperatorSet.GetImagePointer1(imageCopy, out HTuple ptrImage, out HTuple hType, out HTuple hWidth, out HTuple hHeight);

            short[] imgData = new short[hWidth.I * hHeight.I];
            Marshal.Copy(ptrImage.IP, imgData, 0, imgData.Length);
            SetImageWL(ptrImage.IP, imgData, hWidth.I, hHeight.I, out HObject hImageSetWL, wl, ww);
            imageCopy.Dispose();
            return hImageSetWL;
        }

        // 设置窗宽窗位
        private static void SetImageWL(IntPtr ptrImage, short[] img_data, int imgWidth, int img_height, out HObject hImageSetWL, int wl, int ww)
        {
            var min = wl - ww / 2.0;
            var max = wl + ww / 2.0;
            var len = imgWidth * img_height;
            short[] data = new short[img_data.Length];

            Parallel.ForEach(Partitioner.Create(0, len), (range) =>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    var val = (ushort)img_data[i];

                    if (val < min) val = 0;
                    else if (val > max) val = 255;
                    else val = (ushort)((val - min) / ww * 255);
                    data[i] = (short)val;
                }
            });
            Marshal.Copy(data, 0, ptrImage, img_data.Length);
            HOperatorSet.GenImage1(out hImageSetWL, "uint2", imgWidth, img_height, ptrImage);
        }

        public static HObject Gray16ToRgb8(HObject grayImage16)
        {
            // 将16位灰度图像归一化为8位灰度图像
            HOperatorSet.ScaleImageMax(grayImage16, out HObject grayImage8);
            HOperatorSet.Compose3(grayImage8, grayImage8, grayImage8, out HObject rgbImage8);
            grayImage8.Dispose();

            return rgbImage8;
        }

        public static void DispImageFit(HObject image, HWindowControl hWindowControl)
        {
            if (image == null) return;
            try
            {
                HOperatorSet.GetImageSize(image, out HTuple img_width, out HTuple img_height);
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

                HOperatorSet.ClearWindow(hWindowControl.HalconWindow);

                HOperatorSet.SetPart(hWindowControl.HalconWindow, row1, column1, row2, column2);

                HOperatorSet.DispObj(image, hWindowControl.HalconWindow);
            }
            catch (Exception)
            {
                //LogHelper.Error(ex.Message);
            }
        }

        public static string PaintImage(HObject image, List<HObject> paintObjects, out HObject outputImage)
        {
            outputImage = null;
            try
            {
                // 初始化 HALCON 窗口
                // 获取图像宽度和高度
                HOperatorSet.GetImagePointer1(image, out HTuple hPointer, out HTuple hType, out HTuple hWidth, out HTuple hHeight);

                // 创建虚拟 HALCON 窗口并设置为图像大小
                HTuple windowID;
                HOperatorSet.OpenWindow(0, 0, hWidth, hHeight, "root", "buffer", "", out windowID);

                // 设置绘制颜色和模式
                //HOperatorSet.SetColor(windowID, "green");
                HOperatorSet.SetDraw(windowID, "fill");

                // 绘制多个矩形
                for (int i = 0; i < paintObjects.Count; i++)
                {
                    var paintObject = paintObjects[i];
                    HOperatorSet.DispObj(paintObject, windowID);
                }

                // 从虚拟窗口获取绘制后的图像
                HOperatorSet.DumpWindowImage(out outputImage, windowID);

                HOperatorSet.CloseWindow(windowID);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

    public static class HalconHelperTester
    {
        // 画
        public static void DrawPaintDisp()
        {
            HOperatorSet.ReadImage(out HObject image, @"E:\solder-ball-tif\Org_11AS.tif");

            // 测量执行时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // 初始化 HALCON 窗口
            // 获取图像宽度和高度
            HOperatorSet.GetImageSize(image, out HTuple width, out HTuple height);

            // 创建虚拟 HALCON 窗口并设置为图像大小
            HTuple windowID;
            HOperatorSet.OpenWindow(0, 0, width, height, "root", "buffer", "", out windowID);
            HDevWindowStack.Push(windowID);

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

            // 保存最终的图像
            HOperatorSet.WriteImage(outputImage, "jpg", 0, "output_image.jpg");

            // 关闭虚拟窗口并清理资源
            HOperatorSet.CloseWindow(windowID);
            image.Dispose();
            outputImage.Dispose();
        }

        // HObject转HImage 图像转换
        public static void HObjectToHImage()
        {
            // 创建一个示例图像
            HImage image = new HImage();
            image.ReadImage(@"E:\solder-ball-tif\Org_11AS.tif"); // 替换为实际的图像路径

            // 测量执行时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                // 将图像转换为 HObject
                HObject hObject = image;

                // 再从 HObject 转换回 HImage
                HImage convertedImage = new HImage(hObject);

                // 检查转换后的图像是否有效
                //Console.WriteLine(convertedImage);
                // 打印通道数、位深、Size、获取图像的最小和最大灰度值、获取图像的颜色空间
                if (i % 100 == 0)
                {
                    Console.WriteLine($"{HalconHelper.GetImageInfoAsString(image)}");
                    Console.WriteLine($"{HalconHelper.GetImageInfoAsString(convertedImage)}");
                }

                // 注意！！！注意！！！注意！！！
                // hObject 不可以释放
                // hObject.Dispose(); // 会导致内存泄漏 // new HImage(hObject) 这样是引用了HObject

                // convertedImage 可以释放
                convertedImage.Dispose();
            }

            stopwatch.Stop();
            Console.WriteLine($"图像转换耗时: {stopwatch.ElapsedMilliseconds} 毫秒"); // 16毫秒

            image.Dispose();
        }

        public static void OverlayImage()
        {
            // 加载图像列表
            HOperatorSet.ReadImage(out HObject img1, @"E:\\solder-ball-tif\\Org_14AS.tif");
            HOperatorSet.ReadImage(out HObject img2, @"E:\\solder-ball-tif\\Org_15AS.tif");
            HOperatorSet.ReadImage(out HObject img3, @"E:\\solder-ball-tif\\Org_16AS.tif");
            HOperatorSet.ReadImage(out HObject img4, @"E:\\solder-ball-tif\\Org_17AS.tif");
            HOperatorSet.ReadImage(out HObject img5, @"E:\\solder-ball-tif\\Org_18AS.tif");
            List<HObject> images16bit = new List<HObject>
            {
                img1,
                img2,
                img3,
                img4,
                img5,
            };

            // 测量执行时间
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            // 调用叠加函数
            HObject resultImage = HalconHelper.OverlayImage3(images16bit);

            stopwatch.Stop();
            Console.WriteLine($"图像叠加耗时: {stopwatch.ElapsedMilliseconds} 毫秒");

            // 保存结果图像
            HOperatorSet.WriteImage(resultImage, "tiff", 0, "averaged_image.tif");

            // 释放资源
            resultImage.Dispose();
            foreach (var img in images16bit)
            {
                img.Dispose();
            }
        }
    }

}
