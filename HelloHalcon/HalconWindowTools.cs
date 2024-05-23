using HalconDotNet;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
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
        private int _currentBeginRow, _currentBeginCol, _currentEndRow, _currentEndCol;
        private int _zoomBeginRow, _zoomBeginCol, _zoomEndRow, _zoomEndCol;
        private HObject _origImage;
        private HObject _showingImage;

        // 固定绘制的矩形（从配置文件加载的矩形）
        private List<Rectangle> _drawFixedRectangles;
        // 用户绘制的矩形
        private List<Rectangle> _drawUserRectangles;
        // 要绘制的对象
        private HObject _drawObjectList;
        private List<string> _drawObjectInfoList;
        // 拟合的最小外接圆
        private HObject _minCircle;

        private HWindowControl _hWindowControl;

        public bool IsFilled { get; set; } = false; // 添加布尔标志以指示当前的绘制模式

        public HWindowControl GetHWindowControl() => _hWindowControl;

        public HalconWindowTools(HWindowControl hWindowControl1)
        {
            //if (HalconAPI.isWindows)
            //    HOperatorSet.SetSystem("use_window_thread", "true");

            _hWindowControl = hWindowControl1;

            _drawFixedRectangles = new List<Rectangle>();
            _drawUserRectangles = new List<Rectangle>();

            //设置线条颜色和字体
            HOperatorSet.SetColor(_hWindowControl.HalconWindow, "green");
            HOperatorSet.QueryFont(_hWindowControl.HalconWindow, out HTuple hv_Font);
            HTuple FontWithStyleAndSize = hv_Font.TupleSelect(0) + "-Bold-24";
            HOperatorSet.SetFont(_hWindowControl.HalconWindow, FontWithStyleAndSize);
            HOperatorSet.SetDraw(_hWindowControl.HalconWindow, "margin");
            //hw_ctrl.HalconWindow.SetDraw("margin");

            HOperatorSet.GenEmptyObj(out _drawObjectList);//空绘图列表
            _drawObjectInfoList = new List<string> { };
        }

        // 打开图像
        public void OpenImage(string imgPath)
        {
            _origImage?.Dispose();
            HOperatorSet.ReadImage(out _origImage, imgPath);
            _showingImage?.Dispose();
            _showingImage = _origImage.Clone();
            DispImageFit();

            //if (HalconHelper.GetImageInfo(_origImage, out int imgWidth, out int imgHeight, out int imgChannels, out int imgDepth))
            //{
            //    Console.WriteLine($"图像宽度：{imgWidth} 图像高度：{imgHeight} 图像通道数：{imgChannels} 图像位深：{imgDepth}");
            //}
        }

        // 通过调整窗宽窗位来更新图像
        public void UpdataImageByWLWW(int wl = 0, int ww = 0)
        {
            if (_origImage == null) return;
            if (wl == 0 || ww == 0) return;

            UseWLForImage(_origImage, out HObject imageByWL, wl, ww);
            _showingImage?.Dispose();
            _showingImage = imageByWL;
           
            // 显示图像
            ShowImage();
           
            // 绘制所有其他对象
            DrawAll();
        }

        // 设置窗宽窗位，返回新的图像 ho_ImageSetWL
        private void UseWLForImage(HObject ho_Image, out HObject ho_ImageSetWL, int wl, int ww)
        {
            HOperatorSet.CopyImage(ho_Image, out HObject imageCopy);
            HOperatorSet.GetImagePointer1(imageCopy, out var ptrImage, out HTuple type1, out var img_width, out var img_height);

            short[] img_data = new short[img_width * img_height];
            Marshal.Copy(ptrImage, img_data, 0, img_data.Length);
            SetImageWL(ptrImage, img_data, img_width, img_height, out ho_ImageSetWL, wl, ww);
            imageCopy.Dispose();
        }

        // 设置窗宽窗位
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

        // 显示图像
        private void ShowImage()
        {
            if (_showingImage == null) return;
            HOperatorSet.ClearWindow(_hWindowControl.HalconWindow);
            HOperatorSet.DispObj(_showingImage, _hWindowControl.HalconWindow);
        }

        // 自适应显示图片
        public void DispImageFit()
        {
            if (_showingImage == null ) return;
            try
            {
                HOperatorSet.GetImageSize(_showingImage, out HTuple img_width, out HTuple img_height);
                //int win_width = hw_ctrl.ImagePart.Width;
                //int win_height = hw_ctrl.ImagePart.Height;
                int win_width = _hWindowControl.Size.Width;
                int win_height = _hWindowControl.Size.Height;

                double w_ratio = 1.0 * img_width / win_width;
                double h_ratio = 1.0 * img_height / win_height;
                double ratio = (w_ratio > h_ratio ? w_ratio : h_ratio);
                double column1 = -(ratio * win_width - img_width) / 2.0;
                double row1 = -(ratio * win_height - img_height) / 2.0;
                double column2 = (ratio * win_width - img_width) / 2.0 + img_width;
                double row2 = (ratio * win_height - img_height) / 2.0 + img_height;

                HOperatorSet.SetPart(_hWindowControl.HalconWindow, row1, column1, row2, column2);
                
                // 显示图像
                ShowImage();

                // 绘制所有其他对象
                DrawAll();
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message);
            }
        }

        // 放大或缩放图片
        public void DispImageZoom(HTuple mode)
        {
            if (_showingImage == null) return;
            _hWindowControl.HalconWindow.GetMpositionSubPix(out var Mouse_row, out var Mouse_col, out _);
            HOperatorSet.GetImageSize(_showingImage, out var hv_imageWidth, out var hv_imageHeight);
            try
            {
                _hWindowControl.HalconWindow.GetPart(out _currentBeginRow, out _currentBeginCol, out _currentEndRow, out _currentEndCol);
                if (mode > 0)//图像放大,当窗口过小时可能出现除数为0
                {
                    _zoomBeginRow = (int)(_currentBeginRow + (Mouse_row - _currentBeginRow) * 0.300d);
                    _zoomBeginCol = (int)(_currentBeginCol + (Mouse_col - _currentBeginCol) * 0.300d);
                    _zoomEndRow = (int)(_currentEndRow - (_currentEndRow - Mouse_row) * 0.300d);
                }
                else//图像缩小
                {
                    _zoomBeginRow = (int)(Mouse_row - (Mouse_row - _currentBeginRow) / 0.700d);
                    _zoomBeginCol = (int)(Mouse_col - (Mouse_col - _currentBeginCol) / 0.700d);
                    _zoomEndRow = (int)(Mouse_row + (_currentEndRow - Mouse_row) / 0.700d);
                }

                var hw_width = _hWindowControl.WindowSize.Width;
                var hw_height = _hWindowControl.WindowSize.Height;
                double windowPartRatio = 1.0 * hw_height / hw_width;
                _zoomEndCol = (int)((_zoomEndRow - _zoomBeginRow) / windowPartRatio + _zoomBeginCol);

                var _isOutOfArea = _zoomBeginRow >= hv_imageHeight || _zoomBeginCol >= hv_imageWidth || _zoomEndRow <= 0 || _zoomEndCol <= 0;
                var _isOutOfSize = (_zoomEndRow - _zoomBeginRow) > hv_imageHeight * 20 || (_zoomEndCol - _zoomBeginCol) > hv_imageWidth * 20;//避免像素过小
                var _isOutOfPixel = hw_height / (_zoomEndRow - _zoomBeginRow) > 400 || hw_width / (_zoomEndCol - _zoomBeginCol) > 400; //避免像素过大

                if (_isOutOfArea || _isOutOfSize || _isOutOfPixel) return;

                _hWindowControl.HalconWindow.SetPaint(new HTuple("default"));
                _hWindowControl.HalconWindow.SetPart(_zoomBeginRow, _zoomBeginCol, _zoomEndRow, _zoomEndCol);

                // 显示图像
                ShowImage();

                // 绘制所有其他对象
                DrawAll();
            }
            catch (Exception ex)
            {
                //LogHelper.Error(ex.Message);
            }
        }

        // 移动图片
        internal void DispImageMove(int btn_down_row, int btn_down_col)
        {
            if (_showingImage == null) return;
            try
            {
                _hWindowControl.HalconWindow.GetPart(out var current_beginRow, out var current_beginCol, out var current_endRow, out int current_endCol);
                _hWindowControl.HalconWindow.GetMposition(out var mouse_post_row, out var mouse_pose_col, out _);
                _hWindowControl.HalconWindow.SetPaint(new HTuple("default"));
                _hWindowControl.HalconWindow.SetPart(current_beginRow + btn_down_row - mouse_post_row, current_beginCol + btn_down_col - mouse_pose_col, current_endRow + btn_down_row - mouse_post_row, current_endCol + btn_down_col - mouse_pose_col);

                // 显示图像
                ShowImage();

                // 绘制所有其他对象
                DrawAll();
            }
            catch (Exception ex)
            {
                //当移动鼠标超出窗口会报错
                //System.Windows.MessageBox.Show(ex.Message);
            }
        }

        // 加载固定矩形
        public void LoadFixedRectangles(List<Rectangle> rects)
        {
            _drawFixedRectangles = rects;
            DrawAll();
        }

        // 添加用户矩形
        public void AddUserRectangle(Rectangle rect)
        {
            _drawUserRectangles.Add(rect);
            DrawAll();
        }

        // 绘制所有
        public void DrawAll()
        {
            if (_showingImage == null) return;

            HOperatorSet.ClearWindow(_hWindowControl.HalconWindow);
            HOperatorSet.DispObj(_showingImage, _hWindowControl.HalconWindow);

            // 绘制固定矩形
            foreach (var rect in _drawFixedRectangles)
            {
                DrawRectangle(rect);
            }

            // 绘制用户矩形
            foreach (var rect in _drawUserRectangles)
            {
                DrawRectangle(rect);
            }

            // 绘制对象
            for (int i = 0; i < _drawObjectInfoList.Count; i++)
            {
                string info = _drawObjectInfoList[i];
                HOperatorSet.SelectObj(_drawObjectList, out HObject object_temp1, i + 1);
                HOperatorSet.SetColor(_hWindowControl.HalconWindow, "red");
                HOperatorSet.DispObj(object_temp1, _hWindowControl.HalconWindow);
            }

            // 绘制最小圆
            if (_minCircle != null)
            {
                HOperatorSet.SetColor(_hWindowControl.HalconWindow, "red");
                HOperatorSet.DispObj(_minCircle, _hWindowControl.HalconWindow);
            }
        }

        // 绘制矩形
        public void DrawRectangle(Rectangle rect)
        {
            //double row1 = rect.Top;
            //double col1 = rect.Left;
            //double row2 = rect.Bottom;
            //double col2 = rect.Right;
            //double row = (row1 + row2) / 2;
            //double col = (col1 + col2) / 2;
            //double phi = 0.0; // 没有旋转角度
            //double length1 = Math.Abs(row2 - row1) / 2;
            //double length2 = Math.Abs(col2 - col1) / 2;

            //HOperatorSet.SetColor(hw_ctrl.HalconWindow, "red");
            //HOperatorSet.GenRectangle2(out HObject rectOut, row, col, phi, length1, length2);
            //HOperatorSet.DispObj(rectOut, hw_ctrl.HalconWindow);

            HOperatorSet.SetColor(_hWindowControl.HalconWindow, "red");
            HOperatorSet.SetDraw(_hWindowControl.HalconWindow, IsFilled ? "fill" : "margin"); // 根据标志设置绘制模式
            HOperatorSet.DispRectangle1(_hWindowControl.HalconWindow, rect.Top, rect.Left, rect.Bottom, rect.Right);

        }

        // 添加绘制对象
        public void AddDrawObjectList(HObject obj, string info = null)
        {
            HOperatorSet.ConcatObj(_drawObjectList, obj, out _drawObjectList);
            _drawObjectInfoList.Add(info);//编号，跟随坐标X，跟随坐标Y，图案类型，图片左上角显示信息，图案颜色
            DrawAll();
        }

        // 拟合最小圆
        // 圆心坐标: X = " + hv_Column.D + ", Y = " + hv_Row.D
        public (bool, double, double) FitSmallestCircle()
        {
            if (_showingImage == null)
            {
                return (false, 0, 0);
            }

            // Local iconic variables 
            HObject ho_Region, ho_RegionClosing, ho_Circle;
            // Local control variables 
            HTuple hv_UsedThreshold = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Radius = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_Circle);

            var halconWindow = _hWindowControl.HalconWindow;
            halconWindow.SetDraw("margin");
            ho_Region.Dispose(); hv_UsedThreshold.Dispose();
            // 二值化图像
            HOperatorSet.BinaryThreshold(_showingImage, out ho_Region, "max_separability", "dark", out hv_UsedThreshold);
            ho_RegionClosing.Dispose();
            // 形态学开运算
            HOperatorSet.OpeningCircle(ho_Region, out ho_RegionClosing, 3.5);
            hv_Row.Dispose(); hv_Column.Dispose(); hv_Radius.Dispose();
            // 拟合最小外接圆
            HOperatorSet.SmallestCircle(ho_RegionClosing, out hv_Row, out hv_Column, out hv_Radius);
            ho_Circle.Dispose();
            // 生成圆对象
            HOperatorSet.GenCircle(out ho_Circle, hv_Row, hv_Column, hv_Radius);
            // 显示圆
            HOperatorSet.DispObj(ho_Circle, halconWindow);
            _minCircle?.Dispose();
            _minCircle = ho_Circle;

            var rX = hv_Column.D;
            var rY = hv_Row.D;
            //System.Console.WriteLine("圆心坐标: X = " + hv_Column.D + ", Y = " + hv_Row.D);

            ho_Region.Dispose();
            ho_RegionClosing.Dispose();
            //ho_Circle.Dispose();
            hv_UsedThreshold.Dispose();
            hv_Row.Dispose();
            hv_Column.Dispose();
            hv_Radius.Dispose();

            return (true, rX, rY);
        }

        // 计算自动窗宽窗位
        public bool CalcAutoWLWW(Rectangle currentRectangle, out int wl, out int ww)
        {
            wl = 0;
            ww = 0;
            if (_origImage == null) return false;
            _hWindowControl.Focus();

            try
            {
                _hWindowControl.HalconWindow.SetDraw("margin");
                double column_1 = currentRectangle.X;
                double row_1 = currentRectangle.Y;
                double column_2 = currentRectangle.X + currentRectangle.Width;
                double row_2 = currentRectangle.Y + currentRectangle.Height;
                //hw_ctrl.HalconWindow.DrawRectangle1(out double row_1, out double column_1, out double row_2, out double column_2);
                HOperatorSet.GenRectangle1(out HObject RectOut, row_1, column_1, row_2, column_2);
                //HOperatorSet.DispObj(RectOut, _hWindowControl.HalconWindow);

                //灰度
                HOperatorSet.Intensity(RectOut, _origImage, out HTuple mean, out HTuple deviation);
                HOperatorSet.MinMaxGray(RectOut, _origImage, 1, out HTuple mingray, out HTuple maxgray, out HTuple rangegray);
                wl = (int)mean.D;
                int min1 = (int)(mean - mingray).D;
                int max1 = (int)(maxgray - mean).D;
                ww = Math.Max(min1, max1);

                RectOut.Dispose();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 一比一显示图像
        public void DisplayOriginalSizeImage()
        {
            if (_showingImage == null) return;

            try
            {
                // 获取图像尺寸
                HOperatorSet.GetImageSize(_showingImage, out HTuple imgWidth, out HTuple imgHeight);

                // 获取窗口的实际尺寸
                int winWidth = _hWindowControl.Size.Width;
                int winHeight = _hWindowControl.Size.Height;

                if (imgWidth > winWidth || imgHeight > winHeight)
                {
                    // 图像尺寸大于窗口尺寸，显示图像中心部分
                    double rowOffset = (imgHeight - winHeight) / 2.0;
                    double colOffset = (imgWidth - winWidth) / 2.0;
                    HOperatorSet.SetPart(_hWindowControl.HalconWindow, rowOffset, colOffset, rowOffset + winHeight - 1, colOffset + winWidth - 1);
                }
                else
                {
                    // 图像尺寸小于窗口尺寸，按比例缩放图像
                    double rowOffset = (winHeight - imgHeight) / 2.0;
                    double colOffset = (winWidth - imgWidth) / 2.0;
                    HOperatorSet.SetPart(_hWindowControl.HalconWindow, -rowOffset, -colOffset, rowOffset + imgHeight - 1, colOffset + imgWidth - 1);
                }

                // 显示图像
                ShowImage();

                // 绘制所有其他对象
                DrawAll();
            }
            catch (Exception ex)
            {
                // 捕捉并记录异常
                // LogHelper.Error(ex.Message);
            }
        }

        // 自适应窗体大小显示图像
        public void DisplayAdaptiveSizeImage()
        {
            if (_showingImage == null) return;

            try
            {
                // 获取图像尺寸
                HOperatorSet.GetImageSize(_showingImage, out HTuple imgWidth, out HTuple imgHeight);

                // 获取窗口尺寸
                int winWidth = _hWindowControl.Size.Width;
                int winHeight = _hWindowControl.Size.Height;

                // 计算宽高比例
                double wRatio = 1.0 * imgWidth / winWidth;
                double hRatio = 1.0 * imgHeight / winHeight;
                double ratio = Math.Max(wRatio, hRatio);

                // 计算适应窗口的显示区域
                double column1 = -(ratio * winWidth - imgWidth) / 2.0;
                double row1 = -(ratio * winHeight - imgHeight) / 2.0;
                double column2 = (ratio * winWidth - imgWidth) / 2.0 + imgWidth;
                double row2 = (ratio * winHeight - imgHeight) / 2.0 + imgHeight;

                // 设置窗口显示的图像区域
                HOperatorSet.SetPart(_hWindowControl.HalconWindow, row1, column1, row2, column2);

                // 显示图像
                ShowImage();

                // 绘制所有其他对象
                DrawAll();
            }
            catch (Exception ex)
            {
                // 捕捉并记录异常
                // LogHelper.Error(ex.Message);
            }
        }

        public bool IsNullImage()
        {
            return _origImage == null || _showingImage == null;
        }

        public (bool, int, int, int) GetCurrentMousePosAndGrayval()
        {
            try
            {
                if (_showingImage == null || _origImage == null)
                {
                    return (false, 0, 0, 0);
                }

                _hWindowControl.HalconWindow.GetMposition(out var mousePostRow, out var mousePostCol, out _);
                HOperatorSet.GetImageSize(_showingImage, out var imageWidth, out var imageHeight);
                HTuple grayValue = 0;
                if (mousePostRow > 0 && mousePostCol > 0 && mousePostRow < imageHeight && mousePostCol < imageWidth)
                {
                    HOperatorSet.GetGrayval(_origImage, mousePostRow, mousePostCol, out grayValue);
                    grayValue = grayValue[0];
                }
                
                return (true, mousePostRow, mousePostCol, grayValue.I);
            }
            catch { }
            return (false, 0, 0, 0);
        }

        // 清除拟合的最小圆
        public void ClearMinCircle()
        {
            _minCircle = null;

            // 绘制所有其他对象
            DrawAll();
        }

        // 清除固定的矩形
        public void ClearFixedRectangles()
        {
            _drawFixedRectangles.Clear();

            // 绘制所有其他对象
            DrawAll();
        }

        // 清除全部框、要绘制的对象
        public void ClearAll()
        {
            // 清除固定的矩形
            _drawFixedRectangles.Clear();

            // 清除 
            _drawUserRectangles.Clear();

            // 清除要绘制的对象 
            _drawObjectList.Dispose();
            HOperatorSet.GenEmptyObj(out _drawObjectList);
            _drawObjectInfoList.Clear();

            // 清除拟合的最小圆
            _minCircle = null;

            // 绘制所有其他对象
            DrawAll();
        }
    }

}