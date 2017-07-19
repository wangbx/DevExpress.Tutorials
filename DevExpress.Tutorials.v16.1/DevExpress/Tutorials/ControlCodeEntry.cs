namespace DevExpress.Tutorials
{
    using System;

    public class ControlCodeEntry
    {
        private string code;
        private string controlName;

        public ControlCodeEntry(string controlName, string code)
        {
            this.controlName = controlName;
            this.code = code;
        }

        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public string ControlName
        {
            get
            {
                return this.controlName;
            }
        }
    }
}

