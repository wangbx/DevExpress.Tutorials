namespace DevExpress.Tutorials
{
    using System;
    using System.Windows.Forms;

    public class ControlIterator
    {
        private Control startControl;

        public ControlIterator(Control startControl)
        {
            this.startControl = startControl;
        }

        protected virtual void InitProcessing()
        {
        }

        protected virtual void ProcessControl(Control control)
        {
        }

        public void ProcessControls()
        {
            this.InitProcessing();
            this.ProcessControls(this.startControl);
        }

        private void ProcessControls(Control startControl)
        {
            foreach (Control control in startControl.Controls)
            {
                this.ProcessControl(control);
                if (control.HasChildren)
                {
                    this.ProcessControls(control);
                }
            }
        }
    }
}

