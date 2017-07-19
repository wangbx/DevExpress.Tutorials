namespace DevExpress.Tutorials.Controls
{
    using DevExpress.LookAndFeel;
    using DevExpress.Utils.Drawing;
    using DevExpress.XtraEditors;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class AlignmentControl : XtraUserControl
    {
        private Container components;
        private ContentAlignment fAlignment = ContentAlignment.MiddleCenter;
        private AlignmentControlViewInfo fInfo;

        public event EventHandler AlignmentChanged;

        public AlignmentControl()
        {
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            base.TabStop = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            this.Info.CalcPressed(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Info.HotTrackIndex = -1;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Info.CalcHotTrack(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            this.Info.Draw(new GraphicsCache(e), this.LookAndFeel);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Info.CalcBounds();
        }

        public void RaiseAlignmentChanged(ContentAlignment alignment)
        {
            this.Alignment = alignment;
            if (this.AlignmentChanged != null)
            {
                this.AlignmentChanged(this, EventArgs.Empty);
            }
        }

        [DefaultValue(0x20)]
        public ContentAlignment Alignment
        {
            get
            {
                return this.fAlignment;
            }
            set
            {
                this.fAlignment = value;
                this.Info.CalcLocation();
            }
        }

        public AlignmentControlViewInfo Info
        {
            get
            {
                if (this.fInfo == null)
                {
                    this.fInfo = new AlignmentControlViewInfo(this);
                }
                return this.fInfo;
            }
        }

        public class AlignmentControlViewInfo
        {
            private ContentAlignment[] alignments = new ContentAlignment[] { ContentAlignment.TopLeft, ContentAlignment.TopCenter, ContentAlignment.TopRight, ContentAlignment.MiddleLeft, ContentAlignment.MiddleCenter, ContentAlignment.MiddleRight, ContentAlignment.BottomLeft, ContentAlignment.BottomCenter, ContentAlignment.BottomRight };
            private ArrayList args = new ArrayList();
            private AlignmentControl control;
            private int dx = 8;
            private int fHotTrackIndex = -1;

            public AlignmentControlViewInfo(AlignmentControl control)
            {
                this.control = control;
                for (int i = 0; i < this.alignments.Length; i++)
                {
                    this.args.Add(new StyleObjectInfoArgs());
                }
                this.CalcBounds();
                this.CalcLocation();
            }

            public void CalcBounds()
            {
                for (int i = 0; i < 9; i++)
                {
                    this.ObjectInfoByID(i).Bounds = new Rectangle((this.ElementWidht + this.dx) * (i % 3), (this.ElementHeight + this.dx) * (i / 3), this.ElementWidht, this.ElementHeight);
                }
                this.control.Refresh();
            }

            public void CalcHotTrack(MouseEventArgs e)
            {
                Point p = new Point(e.X, e.Y);
                this.HotTrackIndex = this.IndexByPoint(p);
            }

            public void CalcLocation()
            {
                int locationIndex = this.LocationIndex;
                for (int i = 0; i < this.args.Count; i++)
                {
                    this.ObjectInfoByID(i).State = (locationIndex == i) ? ObjectState.Pressed : ObjectState.Normal;
                }
                if ((this.HotTrackIndex > -1) && (this.HotTrackIndex != locationIndex))
                {
                    this.ObjectInfoByID(this.HotTrackIndex).State = ObjectState.Hot;
                }
                if (this.control == null)
                {
                    for (int j = 0; j < this.args.Count; j++)
                    {
                        this.ObjectInfoByID(j).State = ObjectState.Disabled;
                    }
                }
                this.control.Refresh();
            }

            public void CalcPressed(MouseEventArgs e)
            {
                Point p = new Point(e.X, e.Y);
                int index = this.IndexByPoint(p);
                if (index > -1)
                {
                    this.control.RaiseAlignmentChanged(this.alignments[index]);
                    this.CalcLocation();
                }
            }

            public void Draw(GraphicsCache cache, UserLookAndFeel lookAndFeel)
            {
                for (int i = 0; i < this.args.Count; i++)
                {
                    this.ObjectInfoByID(i).Cache = cache;
                    lookAndFeel.Painter.Button.DrawObject(this.ObjectInfoByID(i));
                }
            }

            private int IndexByPoint(Point p)
            {
                for (int i = 0; i < this.args.Count; i++)
                {
                    if (this.ObjectInfoByID(i).Bounds.Contains(p))
                    {
                        return i;
                    }
                }
                return -1;
            }

            private StyleObjectInfoArgs ObjectInfoByID(int index)
            {
                return (this.args[index] as StyleObjectInfoArgs);
            }

            private int ElementHeight
            {
                get
                {
                    return ((this.control.Height - (this.dx * 2)) / 3);
                }
            }

            private int ElementWidht
            {
                get
                {
                    return ((this.control.Width - (this.dx * 2)) / 3);
                }
            }

            public int HotTrackIndex
            {
                get
                {
                    return this.fHotTrackIndex;
                }
                set
                {
                    if (this.fHotTrackIndex != value)
                    {
                        this.fHotTrackIndex = value;
                        this.CalcLocation();
                    }
                }
            }

            private int LocationIndex
            {
                get
                {
                    if (this.control != null)
                    {
                        for (int i = 0; i < this.alignments.Length; i++)
                        {
                            if (this.control.Alignment.Equals(this.alignments[i]))
                            {
                                return i;
                            }
                        }
                    }
                    return -1;
                }
            }
        }
    }
}

