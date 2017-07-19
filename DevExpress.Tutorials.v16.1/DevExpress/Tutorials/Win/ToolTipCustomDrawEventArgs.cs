namespace DevExpress.Tutorials.Win
{
    using System;
    using System.Windows.Forms;

    public class ToolTipCustomDrawEventArgs : EventArgs
    {
        private bool handled = false;
        private PaintEventArgs paintArgs;

        public ToolTipCustomDrawEventArgs(PaintEventArgs paintArgs)
        {
            this.paintArgs = paintArgs;
        }

        public bool Handled
        {
            get
            {
                return this.handled;
            }
            set
            {
                this.handled = value;
            }
        }

        public PaintEventArgs PaintArgs
        {
            get
            {
                return this.paintArgs;
            }
        }
    }
}

