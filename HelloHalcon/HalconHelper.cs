using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloHalcon
{
    public static class HalconHelper
    {
        public static bool GetImageInfo(HObject image, out int width, out int height, out int channels, out int bitDepth)
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
    }
}
