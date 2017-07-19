namespace DevExpress.Tutorials
{
    using DevExpress.XtraEditors;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ScrollBarInfo
    {
        private DevExpress.XtraEditors.HScrollBar horzScrollBar;
        private bool horzScrollBarVisible;
        private Panel pnlBottomRight;
        private int prevControlHeight;
        private int prevControlWidth;
        private Rectangle realClientRect;
        private int scrollOffsetX;
        private int scrollOffsetY;
        private DevExpress.XtraEditors.VScrollBar vertScrollBar;
        private bool vertScrollBarVisible;
        private ColoredTextControlViewInfo viewInfo;

        public ScrollBarInfo(ColoredTextControlViewInfo viewInfo)
        {
            this.viewInfo = viewInfo;
            this.InitScrollBars();
            this.prevControlWidth = viewInfo.Control.Bounds.Width;
            this.prevControlHeight = viewInfo.Control.Bounds.Height;
            this.horzScrollBarVisible = this.vertScrollBarVisible = false;
            this.realClientRect = Rectangle.Empty;
            this.pnlBottomRight = new Panel();
            this.pnlBottomRight.Visible = false;
            this.pnlBottomRight.Parent = viewInfo.Control;
        }

        private void CheckBottomRightPanel()
        {
            Rectangle bounds = this.viewInfo.Control.Bounds;
            if (this.vertScrollBarVisible && this.horzScrollBarVisible)
            {
                this.pnlBottomRight.Bounds = new Rectangle(bounds.Right - SystemInformation.VerticalScrollBarWidth, bounds.Bottom - SystemInformation.HorizontalScrollBarHeight, SystemInformation.VerticalScrollBarWidth, SystemInformation.HorizontalScrollBarHeight);
                this.pnlBottomRight.Visible = true;
            }
            else
            {
                this.pnlBottomRight.Visible = false;
            }
        }

        private void CheckScrollBarsVisible()
        {
            this.UpdateRealClientRect();
            this.HorzScrollBarVisible = this.viewInfo.Populator.TotalWidth > this.realClientRect.Width;
            this.VertScrollBarVisible = this.viewInfo.Populator.TotalHeight > this.realClientRect.Height;
        }

        private void InitScrollBar(ScrollBarBase scrollBar)
        {
            scrollBar.Visible = false;
            scrollBar.ValueChanged += new EventHandler(this.ScrollBarPositionChanged);
            scrollBar.Parent = this.viewInfo.Control;
        }

        private void InitScrollBars()
        {
            this.horzScrollBar = new DevExpress.XtraEditors.HScrollBar();
            this.vertScrollBar = new DevExpress.XtraEditors.VScrollBar();
            this.InitScrollBar(this.horzScrollBar);
            this.InitScrollBar(this.vertScrollBar);
        }

        private bool NeedUpdate()
        {
            bool flag = false;
            if (this.prevControlWidth != this.viewInfo.Control.Bounds.Width)
            {
                this.prevControlWidth = this.viewInfo.Control.Bounds.Width;
                flag = true;
            }
            if (this.prevControlHeight != this.viewInfo.Control.Bounds.Height)
            {
                this.prevControlHeight = this.viewInfo.Control.Bounds.Height;
                flag = true;
            }
            return flag;
        }

        private void ScrollBarPositionChanged(object sender, EventArgs e)
        {
            this.scrollOffsetX = this.horzScrollBar.Value;
            this.scrollOffsetY = this.vertScrollBar.Value;
            this.viewInfo.Control.CalculateAndInvalidate();
        }

        private bool TextOverflowsClientAreaHorz(bool useScrollbar)
        {
            int width = this.viewInfo.Control.Bounds.Width;
            if (useScrollbar)
            {
                width -= SystemInformation.VerticalScrollBarWidth;
            }
            return (this.viewInfo.Populator.TotalWidth > width);
        }

        private bool TextOverflowsClientAreaVert(bool useScrollbar)
        {
            int height = this.viewInfo.Control.Bounds.Height;
            if (useScrollbar)
            {
                height -= SystemInformation.HorizontalScrollBarHeight;
            }
            return (this.viewInfo.Populator.TotalHeight > height);
        }

        private void UpdateRealClientRect()
        {
            bool flag2;
            bool vScrollVisible = flag2 = false;
            if (this.TextOverflowsClientAreaHorz(false))
            {
                flag2 = true;
            }
            if (this.TextOverflowsClientAreaVert(false))
            {
                vScrollVisible = true;
            }
            if (flag2 && this.TextOverflowsClientAreaVert(true))
            {
                vScrollVisible = true;
            }
            if (vScrollVisible && this.TextOverflowsClientAreaHorz(true))
            {
                flag2 = true;
            }
            this.UpdateRealClientRect(flag2, vScrollVisible);
        }

        private void UpdateRealClientRect(bool hScrollVisible, bool vScrollVisible)
        {
            this.realClientRect = this.viewInfo.Control.Bounds;
            if (hScrollVisible)
            {
                this.realClientRect.Height -= SystemInformation.HorizontalScrollBarHeight;
            }
            if (vScrollVisible)
            {
                this.realClientRect.Width -= SystemInformation.VerticalScrollBarWidth;
            }
        }

        private void UpdateScrollBars()
        {
            Rectangle bounds = this.viewInfo.Control.Bounds;
            this.horzScrollBar.Bounds = new Rectangle(bounds.Left, bounds.Bottom - SystemInformation.HorizontalScrollBarHeight, this.realClientRect.Width, SystemInformation.HorizontalScrollBarHeight);
            this.vertScrollBar.Bounds = new Rectangle(bounds.Right - SystemInformation.VerticalScrollBarWidth, bounds.Top, SystemInformation.VerticalScrollBarWidth, this.realClientRect.Height);
            this.horzScrollBar.Visible = this.horzScrollBarVisible;
            this.vertScrollBar.Visible = this.vertScrollBarVisible;
            this.UpdateScrollBarValues();
            this.CheckBottomRightPanel();
        }

        private void UpdateScrollBarValues()
        {
            this.horzScrollBar.Maximum = this.viewInfo.Populator.TotalWidth;
            this.horzScrollBar.LargeChange = this.realClientRect.Width;
            this.vertScrollBar.Maximum = this.viewInfo.Populator.TotalHeight;
            this.vertScrollBar.LargeChange = this.realClientRect.Height;
            if (this.horzScrollBar.Visible)
            {
                this.horzScrollBar.Value = Math.Min(this.horzScrollBar.Value, this.viewInfo.Populator.TotalWidth - this.realClientRect.Width);
            }
            else
            {
                this.horzScrollBar.Value = 0;
            }
            if (this.vertScrollBar.Visible)
            {
                this.vertScrollBar.Value = Math.Min(this.vertScrollBar.Value, this.viewInfo.Populator.TotalHeight - this.realClientRect.Height);
            }
            else
            {
                this.vertScrollBar.Value = 0;
            }
        }

        public void UpdateScrollInfo()
        {
            if (this.NeedUpdate())
            {
                this.CheckScrollBarsVisible();
                this.UpdateScrollBars();
            }
        }

        protected bool HorzScrollBarVisible
        {
            get
            {
                return this.horzScrollBarVisible;
            }
            set
            {
                if (this.horzScrollBarVisible != value)
                {
                    this.horzScrollBarVisible = value;
                    if (this.horzScrollBarVisible)
                    {
                        this.horzScrollBar.Value = 0;
                    }
                }
            }
        }

        public bool ScrollBarVisible
        {
            get
            {
                if (!this.horzScrollBarVisible)
                {
                    return this.vertScrollBarVisible;
                }
                return true;
            }
        }

        public int ScrollOffsetX
        {
            get
            {
                return this.scrollOffsetX;
            }
        }

        public int ScrollOffsetY
        {
            get
            {
                return this.scrollOffsetY;
            }
        }

        protected bool VertScrollBarVisible
        {
            get
            {
                return this.vertScrollBarVisible;
            }
            set
            {
                if (this.vertScrollBarVisible != value)
                {
                    this.vertScrollBarVisible = value;
                    if (this.vertScrollBarVisible)
                    {
                        this.vertScrollBar.Value = 0;
                    }
                }
            }
        }
    }
}

