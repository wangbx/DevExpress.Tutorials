namespace DevExpress.Tutorials
{
    using System;

    public class WhatsThisHintEntry
    {
        private string controlName;
        private string hintText;

        public WhatsThisHintEntry(string controlName, string hintText)
        {
            this.controlName = controlName;
            this.hintText = hintText;
        }

        public string ControlName
        {
            get
            {
                return this.controlName;
            }
        }

        public string HintText
        {
            get
            {
                return this.hintText;
            }
        }
    }
}

