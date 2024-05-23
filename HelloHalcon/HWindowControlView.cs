using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HelloHalcon
{
    public class HWindowControlView
    {
        public delegate void ShowImageInfoDelegate(int x, int y, int grayscaleValue);
        public ShowImageInfoDelegate ShowImageInfo;

        HWindowControl _hWindowControl;
        CheckBox _winTechnologyCheckBox;
        TrackBar _wlBar;
        TextBox _wlTextBox;
        TrackBar _wwBar;
        TextBox _wwTextBox;
        RadioButton _noneRadioButton;
        RadioButton _autoWLWWRadioButton;

        public HalconWindowTools _hWindowTools;

        public bool mouseLeftButDown;
        private int mouseLeftDownRow;
        private int mouseLeftDownCol;

        private Point startPoint;
        private Rectangle currentRectangle;
        private bool isDrawing;
        public HWindowControlView(HalconWindowTools halconWindowTools, CheckBox winTechnologyCheckBox, TrackBar wlBar, TextBox wlTextBox, TrackBar wwBar, TextBox wwTextBox, RadioButton noneRadioButton, RadioButton autoWLWWRadioButton)
        {
            _hWindowTools = halconWindowTools;

            _winTechnologyCheckBox = winTechnologyCheckBox;
            _wlBar = wlBar;
            _wlTextBox = wlTextBox;
            _wwBar = wwBar;
            _wwTextBox = wwTextBox;
            _noneRadioButton = noneRadioButton;
            _autoWLWWRadioButton = autoWLWWRadioButton;

            _hWindowControl = _hWindowTools.GetHWindowControl();
            _hWindowControl.HMouseDown += HWindowControl_HMouseDown;
            _hWindowControl.HMouseMove += HWindowControl_HMouseMove;
            _hWindowControl.HMouseUp += HWindowControl_HMouseUp;
            _hWindowControl.HMouseWheel += HWindowControl_HMouseWheel;

            _wlBar.Scroll += wlwwBarScroll;
            _wlBar.Scroll += wlwwBarScroll;

            //LoadRectanglesFromConfig("0,1024,3071,1024");
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
            }
            catch (HalconException ex)
            {
                MessageBox.Show("Error loading image: " + ex.Message);
            }
        }


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
                _hWindowTools.DrawAll();
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
        private void wlwwBarScroll(object sender, EventArgs e)
        {
            var wl = _wlBar.Value;
            var ww = _wwBar.Value;
            _wlTextBox.Text = wl.ToString();
            _wwTextBox.Text = ww.ToString();
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
                if (wl < _wlBar.Minimum)
                {
                    wl = _wlBar.Minimum;
                }
                else if (wl > _wlBar.Maximum)
                {
                    wl = _wlBar.Maximum;
                }
                _wlTextBox.Text = wl.ToString();
                _wlBar.Value = wl;
            }

            if (ww != -1)
            {
                if (ww < _wwBar.Minimum)
                {
                    ww = _wwBar.Minimum;
                }
                else if (ww > _wwBar.Maximum)
                {
                    ww = _wwBar.Maximum;
                }
                _wwTextBox.Text = ww.ToString();
                _wwBar.Value = ww;
            }
        }

        public (bool, double, double) FitSmallestCircle()
        {
            return _hWindowTools.FitSmallestCircle();
        }
    }
}
