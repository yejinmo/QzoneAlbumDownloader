using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialSkin.Animations;
using System.Drawing.Drawing2D;
using MaterialSkin;
using System.ComponentModel;

/// <summary>
/// QQ空间相册控件
/// </summary>
namespace QzoneAlbumDownloader.Controls
{
    public class AlbumControl : Control, IMaterialControl
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

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }


        public AlbumControl()
        {
            Padding = new Padding(10);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            animationManager.OnAnimationProgress += sender => Invalidate();

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;

                    animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;

                Invalidate();
            };

        }

        #endregion

        #region 绘制

        private readonly AnimationManager animationManager;

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
            //Draw Ripple
            if (animationManager.IsAnimating())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = animationManager.GetProgress(i);
                    var animationSource = animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), Color.Black)))
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                g.SmoothingMode = SmoothingMode.None;
            }
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
            return;
        }

        #endregion

    }
}
