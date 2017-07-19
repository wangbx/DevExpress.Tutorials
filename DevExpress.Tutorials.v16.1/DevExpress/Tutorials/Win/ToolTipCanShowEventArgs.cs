namespace DevExpress.Tutorials.Win
{
    using System;
    using System.Drawing;

    public class ToolTipCanShowEventArgs : EventArgs
    {
        private Point position;
        private bool show;
        private string text;
        private StringAlignment windowAlignment;

        public ToolTipCanShowEventArgs(bool show, string text, Point position) : this(show, text, position, StringAlignment.Near)
        {
        }

        public ToolTipCanShowEventArgs(bool show, string text, Point position, StringAlignment windowAlignment)
        {
            this.text = text;
            this.show = show;
            this.position = position;
            this.windowAlignment = windowAlignment;
        }

        public Point Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public bool Show
        {
            get
            {
                return this.show;
            }
            set
            {
                this.show = value;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
            set
            {
                this.text = value;
            }
        }

        public StringAlignment WindowAlignment
        {
            get
            {
                return this.windowAlignment;
            }
            set
            {
                this.windowAlignment = value;
            }
        }
    }
}

