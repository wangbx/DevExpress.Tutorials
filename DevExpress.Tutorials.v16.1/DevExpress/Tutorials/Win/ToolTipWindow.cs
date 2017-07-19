namespace DevExpress.Tutorials.Win
{
    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using DevExpress.Utils.Win;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class ToolTipWindow : CustomTopForm
    {
        private Point bottomPosition;
        private bool mouseTransparent = true;
        private string toolTip;
        private StringAlignment toolTipAlignment = StringAlignment.Near;
        private Point topPosition;

        public event DevExpress.Tutorials.Win.ToolTipCalcSizeEventHandler ToolTipCalcSize;

        public event DevExpress.Tutorials.Win.ToolTipCustomDrawEventHandler ToolTipCustomDraw;

        public ToolTipWindow()
        {
            this.Font = SystemInformation.MenuFont;
            this.toolTip = "";
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.ControlBox = false;
            this.BackColor = SystemColors.Info;
            this.ForeColor = SystemColors.InfoText;
            base.SetStyle(ControlStyles.Opaque, true);
        }

        protected virtual Size CalcTipSize()
        {
            Size size;
            GraphicsInfo info = new GraphicsInfo();
            info.AddGraphics(null);
            try
            {
                DevExpress.Tutorials.Win.ToolTipCalcSizeEventArgs e = new DevExpress.Tutorials.Win.ToolTipCalcSizeEventArgs(this.bottomPosition, this.topPosition, Size.Empty) {
                    Size = info.Cache.CalcTextSize(this.ToolTip, this.Font, TextOptions.DefaultStringFormat, 0).ToSize()
                };
                e.Size = new Size(e.Size.Width + 4, e.Size.Height + 4);
                if (this.ToolTipCalcSize != null)
                {
                    this.ToolTipCalcSize(this, e);
                    this.bottomPosition = e.BottomPosition;
                    this.topPosition = e.TopPosition;
                }
                size = e.Size;
            }
            finally
            {
                info.ReleaseGraphics();
            }
            return size;
        }

        public void HideTip()
        {
            base.Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.ToolTipCustomDraw != null)
            {
                DevExpress.Tutorials.Win.ToolTipCustomDrawEventArgs args = new DevExpress.Tutorials.Win.ToolTipCustomDrawEventArgs(e);
                this.ToolTipCustomDraw(this, args);
                if (args.Handled)
                {
                    return;
                }
            }
            Brush brush = new SolidBrush(this.BackColor);
            Brush foreBrush = new SolidBrush(this.ForeColor);
            GraphicsCache cache = new GraphicsCache(e);
            e.Graphics.FillRectangle(brush, base.ClientRectangle);
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(0, base.Height - 1, base.Width, 1));
            e.Graphics.FillRectangle(Brushes.Black, new Rectangle(base.Width - 1, 0, 1, base.Height));
            cache.DrawString(this.ToolTip, this.Font, foreBrush, new Rectangle(2, 2, base.ClientRectangle.Width - 4, base.ClientRectangle.Height - 4), TextOptions.DefaultStringFormat);
            brush.Dispose();
            foreBrush.Dispose();
        }

        public void ShowTip(Point bottomPosition, Point topPosition)
        {
            this.bottomPosition = bottomPosition;
            this.topPosition = topPosition;
            this.ToolTipChanged(true);
            base.Visible = true;
        }

        protected virtual void ToolTipChanged(bool makeVisible)
        {
            if (base.Visible || makeVisible)
            {
                Point bottomPosition = this.bottomPosition;
                Point topPosition = this.topPosition;
                Size popupSize = this.CalcTipSize();
                if ((popupSize.Width < 1) || (popupSize.Height < 1))
                {
                    base.Size = popupSize;
                    this.HideTip();
                }
                else
                {
                    if (this.ToolTipAlignment == StringAlignment.Far)
                    {
                        bottomPosition.X -= popupSize.Width;
                        topPosition.X -= popupSize.Width;
                    }
                    Point point3 = ControlUtils.CalcLocation(bottomPosition, topPosition, popupSize);
                    base.ClientSize = popupSize;
                    base.Location = point3;
                    base.Invalidate();
                    if (makeVisible)
                    {
                        base.Visible = true;
                    }
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if ((m.Msg == 0x84) && this.MouseTransparent)
            {
                m.Result = new IntPtr(-1);
            }
        }

        public virtual bool MouseTransparent
        {
            get
            {
                return this.mouseTransparent;
            }
            set
            {
                this.mouseTransparent = value;
            }
        }

        public string ToolTip
        {
            get
            {
                return this.toolTip;
            }
            set
            {
                if (this.ToolTip != value)
                {
                    this.toolTip = value;
                    this.ToolTipChanged(false);
                }
            }
        }

        public StringAlignment ToolTipAlignment
        {
            get
            {
                return this.toolTipAlignment;
            }
            set
            {
                if ((this.ToolTipAlignment != value) && (value != StringAlignment.Center))
                {
                    this.toolTipAlignment = value;
                    this.ToolTipChanged(false);
                }
            }
        }
    }
}

