namespace DevExpress.Tutorials
{
    using System;
    using System.Windows.Forms;

    public class FindControlByNameIterator : ControlIterator
    {
        private Control controlResult;
        private string name;

        public FindControlByNameIterator(string name, Control startControl) : base(startControl)
        {
            this.name = name;
            this.controlResult = null;
        }

        protected override void ProcessControl(Control control)
        {
            if (control.Name == this.name)
            {
                this.controlResult = control;
            }
        }

        public Control ControlResult
        {
            get
            {
                return this.controlResult;
            }
        }
    }
}

