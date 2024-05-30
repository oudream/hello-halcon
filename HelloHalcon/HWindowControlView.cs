using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows.Forms;

namespace HelloHalcon
{
    public class HWindowControlView
    {
        public delegate void ShowImageInfoDelegate(int x, int y, int grayscaleValue);
        public ShowImageInfoDelegate ShowImageInfo;

        HWindowControl _hWindowControl;
        CheckBox _wlwwCheckBox;
        TrackBar _wlTrackBar;
        Label _wlLabel;
        TrackBar _wwTrackBar;
        Label _wwLabel;
        RadioButton _noneRadioButton;
        RadioButton _autoWLWWRadioButton;

        public HalconWindowTools _hWindowTools;

        public bool mouseLeftButDown;
        private int mouseLeftDownRow;
        private int mouseLeftDownCol;

        private Point startPoint;
        private Rectangle currentRectangle;
        private bool isDrawing;
        private string _currentImageFilePath;

        public HWindowControlView(HalconWindowTools halconWindowTools, CheckBox wlwwCheckBox, TrackBar wlBar, Label wlLabel, TrackBar wwBar, Label wwLabel, RadioButton noneRadioButton, RadioButton autoWLWWRadioButton)
        {
            _hWindowTools = halconWindowTools;

            _wlwwCheckBox = wlwwCheckBox;
            _wlTrackBar = wlBar;
            _wlLabel = wlLabel;
            _wwTrackBar = wwBar;
            _wwLabel = wwLabel;
            _noneRadioButton = noneRadioButton;
            _autoWLWWRadioButton = autoWLWWRadioButton;

            _hWindowControl = _hWindowTools.GetHWindowControl();
            _hWindowControl.HMouseDown += HWindowControl_HMouseDown;
            _hWindowControl.HMouseMove += HWindowControl_HMouseMove;
            _hWindowControl.HMouseUp += HWindowControl_HMouseUp;
            _hWindowControl.HMouseWheel += HWindowControl_HMouseWheel;

            _wlTrackBar.Scroll += wlwwTrackBarScroll;
            _wlTrackBar.Scroll += wlwwTrackBarScroll;

            _wlwwCheckBox.Click += WLWWCheckBox_Click;

            //LoadRectanglesFromConfig("0,1024,3071,1024");
        }

        // 窗宽窗位显示
        private void WLWWCheckBox_Click(object sender, EventArgs e)
        {
            if (_wlwwCheckBox.Checked && _hWindowTools.IsNullImage())
            {
                _wlwwCheckBox.Checked = false;
                return;
            }

            EnableWLWW(_wlwwCheckBox.Checked);
        }

        private void EnableWLWW(bool wlwwEnabled)
        {
            _wlTrackBar.Visible = wlwwEnabled;
            _wwTrackBar.Visible = wlwwEnabled;
            if (wlwwEnabled)
            {
                //AdjustControlsInPanel(wwTrackBar, wlTrackBar, imagePanel);
                _hWindowTools.UpdataImageByWLWW((int)_wlTrackBar.Value, (int)_wwTrackBar.Value);
                _wlLabel.Visible = true;
                _wwLabel.Visible = true;
                _autoWLWWRadioButton.Enabled = true;
                _autoWLWWRadioButton.Checked = true;
            }
            else
            {
                // 重置窗宽窗
                _hWindowTools.UpdataImageByWLWW(0, 0);
                _wlLabel.Visible = false;
                _wwLabel.Visible = false;
                _noneRadioButton.Checked = true;
                _autoWLWWRadioButton.Enabled = false;
            }
        }

        // 固定框
        // 从配置文件加载矩形
        public void LoadRectanglesFromConfig(string configRectangles)
        {
            var rects = new List<Rectangle>();

            // 从配置文件加载矩形框信息
            if (!string.IsNullOrEmpty(configRectangles))
            {
                var rectArray = configRectangles.Split(';');
                foreach (var rect in rectArray)
                {
                    var values = rect.Split(',');
                    if (values.Length == 4)
                    {
                        var rectangle = new Rectangle(
                            int.Parse(values[0]),
                            int.Parse(values[1]),
                            int.Parse(values[2]),
                            int.Parse(values[3])
                        );
                        rects.Add(rectangle);
                    }
                }
            }

            _hWindowTools.LoadFixedRectangles(rects);
        }

        // 打开图像
        public void OpenImage()
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

            OpenImage(filename);
        }

        public void OpenImage(string fileName)
        {
            try
            {
                // 加载图像
                _hWindowTools.OpenImage(fileName);
                _currentImageFilePath = fileName;

                if (_hWindowTools.GetImageInfo(out int imgWidth, out int imgHeight, out int imgChannels, out int imgBigDepth))
                {
                    if (imgChannels == 1 && imgBigDepth == 16)
                    {
                        _wlwwCheckBox.Enabled = true;
                        _wlwwCheckBox.Visible = true;
                        _wlwwCheckBox.Checked = true;
                    }
                    else
                    {
                        _wlwwCheckBox.Enabled = false;
                        _wlwwCheckBox.Visible = false;
                        _wlwwCheckBox.Checked = false;
                    }
                    EnableWLWW(_wlwwCheckBox.Checked);
                }
            }
            catch (HalconException ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        public void OpenImage(HImage image)
        {
            try
            {
                // 加载图像
                _hWindowTools.OpenImage(image, 0, 0);
                _currentImageFilePath = "";

                if (_hWindowTools.GetImageInfo(out int imgWidth, out int imgHeight, out int imgChannels, out int imgBigDepth))
                {
                    if (imgChannels == 1 && imgBigDepth == 16)
                    {
                        _wlwwCheckBox.Enabled = true;
                        _wlwwCheckBox.Visible = true;
                        _wlwwCheckBox.Checked = true;
                    }
                    else
                    {
                        _wlwwCheckBox.Enabled = false;
                        _wlwwCheckBox.Visible = false;
                        _wlwwCheckBox.Checked = false;
                    }
                    EnableWLWW(_wlwwCheckBox.Checked);
                }
            }
            catch (HalconException ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }

        public string GetCurrentImageFilePath() => _currentImageFilePath;

        // 鼠标 左键按下
        private void HWindowControl_HMouseDown(object sender, HMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Shift) // 按住 Shift 键时开始绘制矩形
                {
                    if (_noneRadioButton.Checked) return;
                    isDrawing = true;
                    startPoint = new Point((int)e.X, (int)e.Y);
                }
                else
                {
                    mouseLeftButDown = true;
                    _hWindowControl.HalconWindow.GetMposition(out mouseLeftDownRow, out mouseLeftDownCol, out _);
                    if (e.Clicks == 2) _hWindowTools.DispImageFit();
                }
            }
        }

        // 鼠标 左键松开
        private void HWindowControl_HMouseUp(object sender, HMouseEventArgs e)
        {
            //鼠标左键松开
            if (e.Button == MouseButtons.Left)
            {
                if (isDrawing)
                {
                    isDrawing = false;
                    if (_autoWLWWRadioButton.Checked)
                    {
                        bool ret = _hWindowTools.CalcAutoWLWW(currentRectangle, out int wl, out int ww);
                        if (ret)
                        {
                            UpdataWLWW(wl, ww);
                            _hWindowTools.UpdataImageByWLWW(wl, ww);
                        }
                    }
                    else
                    {
                        //hWindowTool.AddUserRectangle(currentRectangle);
                    }
                }
                else
                {
                    mouseLeftButDown = false;
                    try
                    {
                        _hWindowControl.HalconWindow.GetMposition(out _, out _, out _);
                    }
                    catch { }
                }
            }
        }

        // 鼠标 移动
        private void HWindowControl_HMouseMove(object sender, HMouseEventArgs e)
        {
            // 鼠标按下时进行移动
            if (isDrawing)
            {
                currentRectangle = new Rectangle(
                    (int)Math.Min(startPoint.X, e.X),
                    (int)Math.Min(startPoint.Y, e.Y),
                    (int)Math.Abs(startPoint.X - e.X),
                    (int)Math.Abs(startPoint.Y - e.Y)
                );
                _hWindowTools.ReShowDrawALL();
                _hWindowTools.DrawRectangle(currentRectangle);
            }
            else if (mouseLeftButDown)
            {
                _hWindowTools.DispImageMove(mouseLeftDownRow, mouseLeftDownCol);
            }
            else
            {
                var (r, mousePostRow, mousePostCol, grayValue) = _hWindowTools.GetCurrentMousePosAndGrayval();
                if (r)
                {
                    //ShowImageInfo?.Invoke(mousePostCol.ToString(CultureInfo.InvariantCulture), mousePostRow.ToString(CultureInfo.InvariantCulture), grayValue.ToString());
                    ShowImageInfo?.Invoke(mousePostCol, mousePostRow, grayValue);
                }
            }
        }

        // 鼠标 滚轮
        private void HWindowControl_HMouseWheel(object sender, HMouseEventArgs e)
        {
            HTuple mode = e.Delta;
            _hWindowTools.DispImageZoom(mode);
        }

        // 窗宽窗位 滚动
        private void wlwwTrackBarScroll(object sender, EventArgs e)
        {
            var wl = _wlTrackBar.Value;
            var ww = _wwTrackBar.Value;
            _wlLabel.Text = wl.ToString();
            _wwLabel.Text = ww.ToString();
            _hWindowTools.UpdataImageByWLWW(wl, ww);
        }

        // 切换填充模式
        private void button2_Click(object sender, EventArgs e)
        {
            _hWindowTools.IsFilled = !_hWindowTools.IsFilled; // 切换实心和空心模式
            //btnToggleFillMode.Text = hWindowTool.IsFilled ? "切换到空心模式" : "切换到实心模式"; // 更新按钮文本
        }


        // 更新窗宽窗位到界面，-1表示保持原数值
        private void UpdataWLWW(int wl, int ww)
        {
            if (wl != -1)
            {
                if (wl < _wlTrackBar.Minimum)
                {
                    wl = _wlTrackBar.Minimum;
                }
                else if (wl > _wlTrackBar.Maximum)
                {
                    wl = _wlTrackBar.Maximum;
                }
                _wlLabel.Text = wl.ToString();
                _wlTrackBar.Value = wl;
            }

            if (ww != -1)
            {
                if (ww < _wwTrackBar.Minimum)
                {
                    ww = _wwTrackBar.Minimum;
                }
                else if (ww > _wwTrackBar.Maximum)
                {
                    ww = _wwTrackBar.Maximum;
                }
                _wwLabel.Text = ww.ToString();
                _wwTrackBar.Value = ww;
            }
        }

        public (bool, double, double) FitSmallestCircle()
        {
            return _hWindowTools.FitSmallestCircle();
        }
    }
}
