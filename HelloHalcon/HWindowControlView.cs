using HalconDotNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace HelloHalcon
{
    public class HalconWindowTools
    {
        private int current_beginRow, current_beginCol, current_endRow, current_endCol;
        private int zoom_beginRow, zoom_beginCol, zoom_endRow, zoom_endCol;
        public HObject origImage;
        public HObject ShowingImage;

        public HWindowControl hw_ctrl;

        public HalconWindowTools(HWindowControl hWindowControl1)
        {
            hw_ctrl = hWindowControl1;

            //设置线条颜色和字体
            HOperatorSet.SetColor(hw_ctrl.HalconWindow, "green");
            HOperatorSet.QueryFont(hw_ctrl.HalconWindow, out HTuple hv_Font);
            HTuple FontWithStyleAndSize = hv_Font.TupleSelect(0) + "-Bold-24";
            HOperatorSet.SetFont(hw_ctrl.HalconWindow, FontWithStyleAndSize);
        }

        /// <summary>
        /// 更新图片考虑窗口技术
        /// </summary>
        public void UpdataImage(HObject image, bool useWLTechnology = false, int wl = 0, int ww = 0)
        {
            if (image == null) return;
            HObject imageByWL = null;
            //HOperatorSet.GenEmptyObj(out HObject imageByWL);
            if (useWLTechnology)
            {
                if (wl == 0 || ww == 0) return;
                //imageByWL.Dispose();
                UseWLForImage(image, out imageByWL, wl, ww);
            }
            var imgShow = imageByWL ?? image;
            ShowingImage = imgShow.Clone();
            ShowImage(imgShow);
        }

        public void UseWLForImage(HObject ho_Image, out HObject ho_ImageSetWL, int wl, int ww)
        {
            ho_ImageSetWL = null;
            //HOperatorSet.GenEmptyObj(out HObject imageCopy);
            //imageCopy.Dispose();
            HOperatorSet.CopyImage(ho_Image, out HObject imageCopy);
            HOperatorSet.GetImagePointer1(imageCopy, out var ptrImage, out HTuple type1, out var img_width, out var img_height);

            short[] img_data = new short[img_width * img_height];
            Marshal.Copy(ptrImage, img_data, 0, img_data.Length);
            SetImageWL(ptrImage, img_data, img_width, img_height, out ho_ImageSetWL, wl, ww);
            imageCopy.Dispose();
        }

        private static void SetImageWL(IntPtr ptrImage, short[] img_data, int img_width, int img_height, out HObject ho_ImageSetWL, int wl, int ww)
        {
            var min = wl - ww / 2.0;
            var max = wl + ww / 2.0;
            var len = img_width * img_height;
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
            HOperatorSet.GenImage1(out ho_ImageSetWL, "uint2", img_width, img_height, ptrImage);
        }

        private void ShowImage(HObject img)
        {
            if (img == null) return;
            HOperatorSet.ClearWindow(hw_ctrl.HalconWindow);
            HOperatorSet.DispObj(img, hw_ctrl.HalconWindow);
        }

        /// <summary>
        /// 自适应显示图片
        /// </summary>
        public void DispImageFit(HObject image)
        {
            if (image == null ) return;
            ShowingImage = image;
            try
            {
                HOperatorSet.GetImageSize(image, out HTuple img_width, out HTuple img_height);
                //int win_width = hw_ctrl.ImagePart.Width;
                //int win_height = hw_ctrl.ImagePart.Height;
                int win_width = hw_ctrl.Size.Width;
                int win_height = hw_ctrl.Size.Height;

                double w_ratio = 1.0 * img_width / win_width;
                double h_ratio = 1.0 * img_height / win_height;
                double ratio = (w_ratio > h_ratio ? w_ratio : h_ratio);
                double column1 = -(ratio * win_width - img_width) / 2.0;
                double row1 = -(ratio * win_height - img_height) / 2.0;
                double column2 = (ratio * win_width - img_width) / 2.0 + img_width;
                double row2 = (ratio * win_height - img_height) / 2.0 + img_height;

                HOperatorSet.SetPart(hw_ctrl.HalconWindow, row1, column1, row2, column2);
                ShowImage(image);
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message);
            }
        }

        /// <summary>
        /// 缩放图片
        /// </summary>
        public void DispImageZoom(HTuple mode)
        {
            if (ShowingImage == null) return;
            hw_ctrl.HalconWindow.GetMpositionSubPix(out var Mouse_row, out var Mouse_col, out _);
            HOperatorSet.GetImageSize(ShowingImage, out var hv_imageWidth, out var hv_imageHeight);
            try
            {
                hw_ctrl.HalconWindow.GetPart(out current_beginRow, out current_beginCol, out current_endRow, out current_endCol);
                if (mode > 0)//图像放大,当窗口过小时可能出现除数为0
                {
                    zoom_beginRow = (int)(current_beginRow + (Mouse_row - current_beginRow) * 0.300d);
                    zoom_beginCol = (int)(current_beginCol + (Mouse_col - current_beginCol) * 0.300d);
                    zoom_endRow = (int)(current_endRow - (current_endRow - Mouse_row) * 0.300d);
                }
                else//图像缩小
                {
                    zoom_beginRow = (int)(Mouse_row - (Mouse_row - current_beginRow) / 0.700d);
                    zoom_beginCol = (int)(Mouse_col - (Mouse_col - current_beginCol) / 0.700d);
                    zoom_endRow = (int)(Mouse_row + (current_endRow - Mouse_row) / 0.700d);
                }

                var hw_width = hw_ctrl.WindowSize.Width;
                var hw_height = hw_ctrl.WindowSize.Height;
                double windowPartRatio = 1.0 * hw_height / hw_width;
                zoom_endCol = (int)((zoom_endRow - zoom_beginRow) / windowPartRatio + zoom_beginCol);

                var _isOutOfArea = zoom_beginRow >= hv_imageHeight || zoom_beginCol >= hv_imageWidth || zoom_endRow <= 0 || zoom_endCol <= 0;
                var _isOutOfSize = (zoom_endRow - zoom_beginRow) > hv_imageHeight * 20 || (zoom_endCol - zoom_beginCol) > hv_imageWidth * 20;//避免像素过小
                var _isOutOfPixel = hw_height / (zoom_endRow - zoom_beginRow) > 400 || hw_width / (zoom_endCol - zoom_beginCol) > 400; //避免像素过大

                if (_isOutOfArea || _isOutOfSize || _isOutOfPixel) return;

                hw_ctrl.HalconWindow.SetPaint(new HTuple("default"));
                hw_ctrl.HalconWindow.SetPart(zoom_beginRow, zoom_beginCol, zoom_endRow, zoom_endCol);

                ShowImage(ShowingImage);
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message);
            }
        }

        /// <summary>
        /// 移动图片
        /// </summary>
        internal void DispImageMove(int btn_down_row, int btn_down_col)
        {
            if (ShowingImage == null) return;
            try
            {
                hw_ctrl.HalconWindow.GetPart(out var current_beginRow, out var current_beginCol, out var current_endRow, out int current_endCol);
                hw_ctrl.HalconWindow.GetMposition(out var mouse_post_row, out var mouse_pose_col, out _);
                hw_ctrl.HalconWindow.SetPaint(new HTuple("default"));
                hw_ctrl.HalconWindow.SetPart(current_beginRow + btn_down_row - mouse_post_row, current_beginCol + btn_down_col - mouse_pose_col, current_endRow + btn_down_row - mouse_post_row, current_endCol + btn_down_col - mouse_pose_col);

                ShowImage(ShowingImage);
            }
            catch (Exception ex)
            {
                //当移动鼠标超出窗口会报错
                //System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public void OpenImage(string imgPath)
        {
            HOperatorSet.ReadImage(out origImage, imgPath);
            DispImageFit(origImage);
        }

    }

}