using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// QQ空间相册控件
/// </summary>
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

        private Font hintFont = DefaultFont;
        public Font HintFont { get => hintFont; set => hintFont = value; }

        private string hintString = string.Empty;
        public string HintString { get => hintString; set => hintString = value; }

        private Color hintForeColor = DefaultForeColor;
        public Color HintForeColor { get => hintForeColor; set => hintForeColor = value; }

        #endregion

        #region 初始化

        public AlbumControl()
        {
            Padding = new Padding(10);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        #endregion

        #region 绘制

        protected override void OnPaint(PaintEventArgs e)
        {
            ReloadSize();
            int a = Width - Padding.Left - Padding.Right;
            var g = e.Graphics;
            g.Clear(BackColor);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            //Draw Image
            if (Image != null)
                g.DrawImage(Image, new Rectangle(Padding.Left, Padding.Top, a, a));
            //Draw Border
            Pen p = new Pen(Color.FromArgb(68, 69, 70));
            g.DrawLine(p, new Point(Padding.Left - 1, Padding.Top - 1), new Point(Padding.Left + a, Padding.Top - 1));
            g.DrawLine(p, new Point(Padding.Left + a, Padding.Top - 1), new Point(Padding.Left + a, Padding.Top + a));
            g.DrawLine(p, new Point(Padding.Left - 1, Padding.Top + a), new Point(Padding.Left + a, Padding.Top + a));
            g.DrawLine(p, new Point(Padding.Left - 1, Padding.Top + a), new Point(Padding.Left - 1, Padding.Top - 1));
            //Draw Title
            StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            g.DrawString(Title, Font, new SolidBrush(ForeColor),
                new Rectangle(Padding.Left, a + Padding.Top + Padding.Bottom,
                a, (int)Font.Size + Padding.Bottom), sf);
            g.DrawString(HintString, HintFont, new SolidBrush(HintForeColor),
                new Rectangle(Padding.Left, a + Padding.Top + Padding.Bottom * 2 + (int)Font.Size + 2,
                a, (int)HintFont.Size + Padding.Bottom), sf);
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            ReloadSize();
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

        /// <summary>
        /// 重新计算 Size
        /// </summary>
        public void ReloadSize()
        {
            Height = Width + Padding.Top + Padding.Bottom * 2 + (int)Font.Size + (int)HintFont.Size;
        }

        #endregion

    }
}
