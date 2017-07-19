namespace DevExpress.Tutorials
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class ControlCodes : CollectionBase
    {
        public void AddListener(string controlName)
        {
            if (!this.ContainsListener(controlName))
            {
                base.List.Add(new ControlCodeEntry(controlName, string.Empty));
            }
        }

        private bool ContainsListener(string controlName)
        {
            if (this.IndexOf(controlName) == -1)
            {
                return false;
            }
            return true;
        }

        public string GetCodeByControlName(string controlName)
        {
            int index = this.IndexOf(controlName);
            if (index == -1)
            {
                return string.Empty;
            }
            return this[index].Code;
        }

        private int IndexOf(string controlName)
        {
            for (int i = 0; i < base.Count; i++)
            {
                if (this[i].ControlName == controlName)
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveListener(string controlName)
        {
            if (this.ContainsListener(controlName))
            {
                base.RemoveAt(this.IndexOf(controlName));
            }
        }

        public ControlCodeEntry this[int index]
        {
            get
            {
                return (base.List[index] as ControlCodeEntry);
            }
        }
    }
}

