namespace DevExpress.Tutorials.Win
{
    using System;
    using System.Drawing;

    public class ToolTipCalcSizeEventArgs : EventArgs
    {
        private Point bottomPosition;
        private System.Drawing.Size size;
        private Point topPosition;

        public ToolTipCalcSizeEventArgs(Point bottomPosition, Point topPosition, System.Drawing.Size size)
        {
            this.topPosition = topPosition;
            this.bottomPosition = bottomPosition;
            this.size = size;
        }

        public Point BottomPosition
        {
            get
            {
                return this.bottomPosition;
            }
            set
            {
                this.bottomPosition = value;
            }
        }

        public System.Drawing.Size Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public Point TopPosition
        {
            get
            {
                return this.topPosition;
            }
            set
            {
                this.topPosition = value;
            }
        }
    }
}

