namespace DevExpress.Tutorials
{
    using System;
    using System.Windows.Forms;

    public class VisibleControlFinder : ControlIterator
    {
        private string controlName;
        private Control result;

        public VisibleControlFinder(string controlName, Control startControl) : base(startControl)
        {
            this.controlName = controlName;
            this.result = null;
        }

        protected override void ProcessControl(Control control)
        {
            if (((control.Name == this.controlName) && !ControlUtils.ControlHasInvisibleParent(control)) && control.Visible)
            {
                this.result = control;
            }
        }

        public Control Result
        {
            get
            {
                return this.result;
            }
        }
    }
}

