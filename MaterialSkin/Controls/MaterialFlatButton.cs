using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialSkin.Animations;

namespace MaterialSkin.Controls
{
    public class MaterialFlatButton : Button, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public bool Primary { get; set; }

        private bool drawHoverMode = true;
        /// <summary>
        /// 悬浮提示
        /// </summary>
        [Browsable(true)]
        public bool DrawHoverMode { get => drawHoverMode; set => drawHoverMode = value; }

        private bool drawImageMode = false;
        /// <summary>
        /// 是否为图像模式
        /// </summary>
        public bool DrawImageMode
        {
            get
            {
                return drawImageMode;
            }
            set
            {
                drawImageMode = value;
                ZoomImg = SmallPic((Bitmap)Image, Width, Height);
                Invalidate();
            }
        }

        private Image ZoomImg = null;
        private Image image;
        public new Image Image
        {
            get => image;
            set
            {
                image = value;
                ZoomImg = SmallPic((Bitmap)image, Width, Height);
                Invalidate();
            }
        }

        private readonly AnimationManager animationManager;
        private readonly AnimationManager hoverAnimationManager;

        public MaterialFlatButton()
        {
            Primary = false;

            animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };

            hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            animationManager.OnAnimationProgress += sender => Invalidate();

            Margin = new Padding(4, 6, 4, 6);
            Padding = new Padding(0);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            g.Clear(BackColor);
            //Hover
            if (DrawHoverMode)
            {
                Color c = SkinManager.GetFlatButtonHoverBackgroundColor();
                using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                    g.FillRectangle(b, ClientRectangle);
            }        
            //DrawContent - Image Mode
            if (DrawImageMode)
            {
                if (ZoomImg != null)
                    g.DrawImage(ZoomImg, new Point(0, 0));
            }
            //Ripple
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
            //DrawContent - String Mode
            if (!DrawImageMode)
            {
                g.DrawString(
                    Text, 
                    Font, 
                    Enabled ? new SolidBrush(ForeColor) : SkinManager.GetFlatButtonDisabledTextBrush(), 
                    ClientRectangle,
                    new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
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

        protected override void OnSizeChanged(EventArgs e)
        {
            Invalidate();
            ZoomImg = SmallPic((Bitmap)Image, Width, Height);
            base.OnSizeChanged(e);
        }

        /// <summary>
        /// 缩小图片
        /// </summary>
        /// <param name="strOldPic">源图文件名(包括路径)</param>
        /// <param name="strNewPic">缩小后保存为文件名(包括路径)</param>
        /// <param name="intWidth">缩小至宽度</param>
        /// <param name="intHeight">缩小至高度</param>
        public static Bitmap SmallPic(Bitmap strOldPic, int intWidth, int intHeight)
        {
            if (strOldPic == null)
                return null;
            Bitmap objNewPic;
            try
            {
                objNewPic = new Bitmap(strOldPic, intWidth, intHeight);
                return objNewPic;
            }
            catch (Exception exp) { throw exp; }
        }

    }
}
