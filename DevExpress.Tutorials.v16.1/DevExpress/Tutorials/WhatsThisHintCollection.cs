namespace DevExpress.Tutorials
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class WhatsThisHintCollection : CollectionBase
    {
        public void Add(string controlName, string hintText)
        {
            base.List.Add(new WhatsThisHintEntry(controlName, hintText));
        }

        public string GetHintTextByControl(string controlName)
        {
            foreach (WhatsThisHintEntry entry in this)
            {
                if (entry.ControlName == controlName)
                {
                    return entry.HintText;
                }
            }
            return string.Empty;
        }

        public WhatsThisHintEntry this[int index]
        {
            get
            {
                return (base.List[index] as WhatsThisHintEntry);
            }
        }
    }
}

