using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace QzoneAlbumDownloader
{
    public partial class Form_ImageViewer : Form
    {

        #region global

        ImageInfo ImageInfo = null;

        #endregion

        #region UI

        bool FORM_SHOULD_CLOSE = false;

        const int Guying_HTLEFT = 10;
        const int Guying_HTRIGHT = 11;
        const int Guying_HTTOP = 12;
        const int Guying_HTTOPLEFT = 13;
        const int Guying_HTTOPRIGHT = 14;
        const int Guying_HTBOTTOM = 15;
        const int Guying_HTBOTTOMLEFT = 0x10;
        const int Guying_HTBOTTOMRIGHT = 17;

        public Form_ImageViewer(ImageInfo II)
        {
            ImageInfo = II;
            m_aeroEnabled = false;
            FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
        }

        private void Form_ImageViewer_Load(object sender, EventArgs e)
        {
            Opacity = 0.0;
            Timer_Fade.Start();
        }

        private void Timer_Fade_Tick(object sender, EventArgs e)
        {
            double d = 0.05;
            if (!FORM_SHOULD_CLOSE)
            {
                if (Opacity + d >= 1.0)
                {
                    Opacity = 1.0;
                    Timer_Fade.Stop();
                }
                else
                {
                    Opacity += d;
                }
            }
            else
            {
                if (Opacity - d <= 0.0)
                {
                    Opacity = 0.0;
                    Timer_Fade.Stop();
                    Close();
                }
                else
                {
                    Opacity -= d;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc(ref m);
                    Point vPoint = new Point((int)m.LParam & 0xFFFF,
                        (int)m.LParam >> 16 & 0xFFFF);
                    vPoint = PointToClient(vPoint);
                    if (vPoint.X <= 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPLEFT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMLEFT;
                        else m.Result = (IntPtr)Guying_HTLEFT;
                    else if (vPoint.X >= ClientSize.Width - 5)
                        if (vPoint.Y <= 5)
                            m.Result = (IntPtr)Guying_HTTOPRIGHT;
                        else if (vPoint.Y >= ClientSize.Height - 5)
                            m.Result = (IntPtr)Guying_HTBOTTOMRIGHT;
                        else m.Result = (IntPtr)Guying_HTRIGHT;
                    else if (vPoint.Y <= 5)
                        m.Result = (IntPtr)Guying_HTTOP;
                    else if (vPoint.Y >= ClientSize.Height - 5)
                        m.Result = (IntPtr)Guying_HTBOTTOM;
                    break;
                case 0x0201:                //鼠标左键按下的消息   
                    m.Msg = 0x00A1;         //更改消息为非客户区按下鼠标   
                    m.LParam = IntPtr.Zero; //默认值   
                    m.WParam = new IntPtr(2);//鼠标放在标题栏内   
                    base.WndProc(ref m);
                    break;
                case WM_NCPAINT:                        // box shadow
                    if (m_aeroEnabled)
                    {
                        var v = 2;
                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
                        MARGINS margins = new MARGINS()
                        {
                            bottomHeight = 1,
                            leftWidth = 1,
                            rightWidth = 1,
                            topHeight = 1
                        };
                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
                m.Result = (IntPtr)HTCAPTION;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                m_aeroEnabled = CheckAeroEnabled();

                CreateParams cp = base.CreateParams;

                if (!m_aeroEnabled)
                    cp.ClassStyle |= CS_DROPSHADOW;

                const int WS_MINIMIZEBOX = 0x00020000;  // Winuser.h中定义
                cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许最小化操作
                return cp;
            }
        }

        #region shadow

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
    (
       int nLeftRect, // x-coordinate of upper-left corner
       int nTopRect, // y-coordinate of upper-left corner
       int nRightRect, // x-coordinate of lower-right corner
       int nBottomRect, // y-coordinate of lower-right corner
       int nWidthEllipse, // height of ellipse
       int nHeightEllipse // width of ellipse
    );

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        private bool m_aeroEnabled;                     // variables for box shadow
        private const int CS_DROPSHADOW = 0x00020000;
        private const int WM_NCPAINT = 0x0085;
        private const int WM_ACTIVATEAPP = 0x001C;

        public struct MARGINS                           // struct for box shadow
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        private const int HTCLIENT = 0x1;
        private const int HTCAPTION = 0x2;

        private bool CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                DwmIsCompositionEnabled(ref enabled);
                return (enabled == 1) ? true : false;
            }
            return false;
        }

        #endregion

        #endregion

        #region control

        /// <summary>
        /// 关闭窗体
        /// </summary>
        private void CloseForm()
        {
            if (FORM_SHOULD_CLOSE)
                return;
            FORM_SHOULD_CLOSE = true;
            Timer_Fade.Start();
        }

        /// <summary>
        /// 加载图片
        /// </summary>
        public void LoadImage()
        {
            if (ImageInfo == null || string.IsNullOrEmpty(ImageInfo.OriginURL))
                return;
            ProcessBar_LoadImage.Visible = true;
            new Thread(new ThreadStart(delegate
            {
                try
                {
                    var img = AlbumHelper.GetImageByURL(ImageInfo.OriginURL);
                    Invoke((EventHandler)delegate
                    {
                        ProcessBar_LoadImage.Visible = false;
                        BackgroundImage = img;
                    });
                }
                catch { }
            }))
            {
                IsBackground = true
            }.Start();
        }

        private void Button_Control_Close_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void Form_ImageViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                CloseForm();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        #endregion

        #region mouse move

        private void Timer_GetCurPos_Tick(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            if (x >= Left && x <= Left + Width && y >= Top && y <= Top + Height)
            {
                MouseMoveNeedShowPanel = true;
                Timer_Panel.Start();
            }
            else
            {
                MouseMoveNeedShowPanel = false;
                Timer_Panel.Start();
            }
        }

        #endregion

        #region panel

        bool MouseMoveNeedShowPanel = false;

        private void Timer_Panel_Tick(object sender, EventArgs e)
        {
            if (Opacity < 0.5)
                return;
            int offset = 6;
            if (MouseMoveNeedShowPanel)
            {
                if (Button_Control_Close.Top + offset >= 0)
                    Button_Control_Close.Top = 0;
                else
                    Button_Control_Close.Top += offset;
                if (Panel_Control.Top - offset <= Height - Panel_Control.Height)
                {
                    Panel_Control.Top = Height - Panel_Control.Height;
                    MouseMoveNeedShowPanel = false;
                    Timer_Panel.Stop();
                }
                else
                    Panel_Control.Top -= offset;
            }
            else
            {
                if (Button_Control_Close.Top - offset <= -50)
                    Button_Control_Close.Top = -50;
                else
                    Button_Control_Close.Top -= offset;
                if (Panel_Control.Top + offset >= Height)
                {
                    Panel_Control.Top = Height;
                    Timer_Panel.Stop();
                }
                else
                    Panel_Control.Top += offset;
            }
        }

        #endregion

    }
}
