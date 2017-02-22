using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
            InitializeComponent();
        }

        private void Form_ImageViewer_Load(object sender, EventArgs e)
        {
            Opacity = 0.0; 
            Timer_Fade.Start(); 
        }

        private void Timer_Fade_Tick(object sender, EventArgs e)
        {
            double d = 0.10;
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
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region control

        private void Button_Control_Close_Click(object sender, EventArgs e)
        {
            if (FORM_SHOULD_CLOSE)
                return;
            FORM_SHOULD_CLOSE = true;
            Timer_Fade.Start();
        }

        #endregion

        #region event

        public void LoadImage()
        {
            if (ImageInfo == null || string.IsNullOrEmpty(ImageInfo.OriginURL))
                return;
            ProcessBar_LoadImage.Visible = true;
            ThreadPool.QueueUserWorkItem(new WaitCallback(delegate
            {
                var img = AlbumHelper.GetImageByURL(ImageInfo.PreviewImagePath);
                Invoke((EventHandler)delegate
                {
                    ProcessBar_LoadImage.Visible = false;
                    BackgroundImage = img;
                });
            }));
        }

        #endregion

    }
}
