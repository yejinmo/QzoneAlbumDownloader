using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace MaterialSkin.Controls
{
    /// <summary>
    /// Material design-like Process bar
    /// </summary>
    public partial class MaterialProcessBar : ProgressBar, IMaterialControl
    {
        private System.Windows.Forms.Timer RenderTimer = new System.Windows.Forms.Timer() { };

        /// <summary>
        /// Initializes a new instance of the <see cref="MaterialProcessBar"/> class.
        /// </summary>
        public MaterialProcessBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            RenderTimer.Tick += new EventHandler(RenderTimer_Tick);
            RenderTimer.Interval = Interval;
            RenderTimer.Enabled = Processing;
            //IsCreated = true;
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            if (Processing)
            {
                int i = Value;
                i += StepValue;
                if (i >= Width)
                    i = -LengthValue;
                Value = i;
                Invalidate();
            }
        }

        public new int Value;
        private int value
        {
            get
            {
                return Value;
            }

            set
            {
                Value = value;
            }
        }

        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>
        /// The depth.
        /// </value>
        [Browsable(false)]
        public int Depth { get; set; }

        private int stepValue = 5;
        public int StepValue
        {
            get
            {
                return stepValue;
            }

            set
            {
                stepValue = value;
            }
        }

        private int lengthValue = 10;
        public int LengthValue
        {
            get
            {
                return lengthValue;
            }

            set
            {
                lengthValue = value;
            }
        }

        private bool processing = false;
        public bool Processing
        {
            get
            {
                return processing;
            }

            set
            {
                processing = value;
                //Visible = Processing;
                RenderTimer.Enabled = Processing;
            }
        }

        private int interval = 10;
        public int Interval
        {
            get
            {
                return interval;
            }

            set
            {
                interval = value;
                RenderTimer.Interval = Interval;
            }
        }

        /// <summary>
        /// Gets the skin manager.
        /// </summary>
        /// <value>
        /// The skin manager.
        /// </value>
        [Browsable(false)]
        public MaterialSkinManager SkinManager
        {
            get { return MaterialSkinManager.Instance; }
        }

        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>
        /// The state of the mouse.
        /// </value>
        [Browsable(false)]
        public MouseState MouseState { get; set; }

        /// <summary>
        /// Performs the work of setting the specified bounds of this control.
        /// </summary>
        /// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
        /// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
        /// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
        /// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
        /// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, 5, specified);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {

            try
            {
                //draw background
                //e.Graphics.FillRectangle(SkinManager.GetDisabledOrHintBrush(), 0, top_pos, e.ClipRectangle.Width, e.ClipRectangle.Height);
                e.Graphics.FillRectangle(SkinManager.GetDisabledOrHintBrush(), 0, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                //draw process block
                //e.Graphics.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, Value, top_pos, LengthValue, e.ClipRectangle.Height);
                e.Graphics.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, Value, 0, LengthValue, e.ClipRectangle.Height);
                /*
                var doneProgress = (int)(e.ClipRectangle.Width * ((double)Value / Maximum));
                e.Graphics.FillRectangle(SkinManager.ColorScheme.PrimaryBrush, 0, 0, doneProgress, e.ClipRectangle.Height);
                e.Graphics.FillRectangle(SkinManager.GetDisabledOrHintBrush(), doneProgress, 0, e.ClipRectangle.Width, e.ClipRectangle.Height);
                */
            }
            catch { }
        }

        //private bool visible = true;
        //int top_pos = 0;
        //public new bool Visible
        //{
        //    get
        //    {
        //        return visible;
        //    }
        //    set
        //    {
        //        visible = value;
        //        if (visible)
        //        {
        //            top_pos = -Height;
        //            Invalidate();
        //            if (HideThread != null)
        //                HideThread.Abort();
        //            if (ShowThread != null)
        //                ShowThread.Abort();
        //            ShowThread = new Thread(new ThreadStart(ShowSub));
        //            ShowThread.Start();
        //        }
        //        else
        //        {
        //            top_pos = 0;
        //            Invalidate();
        //            if (ShowThread != null)
        //                ShowThread.Abort();
        //            if (HideThread != null)
        //                HideThread.Abort();
        //            HideThread = new Thread(new ThreadStart(HideSub));
        //            HideThread.Start();
        //        }
        //    }
        //}

        //Thread ShowThread;

        //Thread HideThread;

        //private void ShowSub()
        //{
        //    while (true && IsCreated)
        //    {
        //        Thread.Sleep(40);
        //        top_pos++;
        //        Invoke((EventHandler)delegate {
        //            Invalidate();
        //        });
        //        if (top_pos >= 0)
        //            break;
        //    }
        //    Invoke((EventHandler)delegate {
        //        base.Visible = true;
        //    });
        //}

        //private void HideSub()
        //{
        //    while (true && IsCreated)
        //    {
        //        Thread.Sleep(40);
        //        top_pos--;
        //        Invoke((EventHandler)delegate {
        //            Invalidate();
        //        });
        //        if (top_pos <= -Height)
        //            break;
        //    }
        //    Invoke((EventHandler)delegate {
        //        base.Visible = false;
        //    });
        //}

        //private bool IsCreated = false;

    }
}
