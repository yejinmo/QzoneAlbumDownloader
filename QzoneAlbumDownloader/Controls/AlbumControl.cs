using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QzoneAlbumDownloader.Controls
{
    public class AlbumControl : Control
    {

        #region 属性

        private Bitmap image = null;
        public Bitmap Image
        {
            get
            {
                return image;
            }

            set
            {
                value = GetSquareBitmap(value);
                image = value;
                Invalidate();
            }
        }

        private string imageURL = string.Empty;
        public string ImageURL
        {
            get
            {
                return imageURL;
            }

            set
            {
                imageURL = value;
            }
        }

        private string title = string.Empty;
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
                Invalidate();
            }
        }

        #endregion

        #region 初始化

        public AlbumControl()
        {
            Padding = new Padding(10);
            BackColor = Color.White;
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        #endregion

        #region 绘制

        protected override void OnPaint(PaintEventArgs e)
        {
            Height = Width + Padding.Top + Padding.Bottom + (int)Font.Size;
            int a = Width - Padding.Left - Padding.Right;
            var g = e.Graphics;
            g.Clear(BackColor);
            //Draw Image
            if (Image != null)
                g.DrawImage(Image, new Rectangle(Padding.Left, Padding.Top, a, a));
            //Draw Border
            Pen p = new Pen(ForeColor);
            g.DrawLine(p, new Point(Padding.Left - 1, Padding.Top - 1), new Point(Padding.Left + a, Padding.Top - 1));
            g.DrawLine(p, new Point(Padding.Left + a, Padding.Top - 1), new Point(Padding.Left + a, Padding.Top + a));
            g.DrawLine(p, new Point(Padding.Left - 1, Padding.Top + a), new Point(Padding.Left + a, Padding.Top + a));
            g.DrawLine(p, new Point(Padding.Left - 1, Padding.Top + a), new Point(Padding.Left - 1, Padding.Top - 1));
            //Draw Title
            g.DrawString(Title, Font, new SolidBrush(ForeColor),
                new Rectangle(Padding.Left, a + Padding.Top + Padding.Bottom, a, Height - Padding.Bottom * 2 - a - Padding.Top));
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Height = Width + Padding.Top + Padding.Bottom + (int)Font.Size;
            base.OnSizeChanged(e);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            Invalidate();
            base.OnFontChanged(e);
        }

        #endregion

        #region 函数

        private Bitmap GetSquareBitmap(Bitmap img)
        {
            int temp = 0;
            if (img.Width > img.Height)
            {
                temp = (img.Width - img.Height) / 2;
                return CutImage(img, new Rectangle(temp, 0, img.Height, img.Height));
            }
            else if (img.Width < img.Height)
            {
                temp = (img.Height - img.Width) / 2;
                return CutImage(img, new Rectangle(0, temp, img.Width, img.Width));
            }
            else
                return img;
        }

        private Bitmap CutImage(Bitmap sourceBitmap, Rectangle rc)
        {
            if (rc.Bottom < 0)
                return null;
            Bitmap TempsourceBitmap = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top);
            Graphics gr = Graphics.FromImage(TempsourceBitmap);
            gr.DrawImage(sourceBitmap, 0, 0, new RectangleF(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top), GraphicsUnit.Pixel);
            gr.Dispose();
            return TempsourceBitmap;
        }

        public void ReloadSize()
        {
            Height = Width + Padding.Top + Padding.Bottom + (int)Font.Size;
        }

        #endregion

    }
}
