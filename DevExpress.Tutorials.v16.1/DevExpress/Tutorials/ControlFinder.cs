namespace DevExpress.Tutorials
{
    using System;
    using System.Windows.Forms;

    public class ControlFinder : ControlIterator
    {
        private string controlName;
        private bool exists;

        public ControlFinder(string controlName, Control startControl) : base(startControl)
        {
            this.controlName = controlName;
            this.exists = false;
        }

        protected override void ProcessControl(Control control)
        {
            if (control.Name == this.controlName)
            {
                this.exists = true;
            }
        }

        public bool Exists
        {
            get
            {
                return this.exists;
            }
        }
    }
}

