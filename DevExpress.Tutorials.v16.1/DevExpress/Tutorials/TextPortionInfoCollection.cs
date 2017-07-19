namespace DevExpress.Tutorials
{
    using System;
    using System.Collections;
    using System.Reflection;

    public class TextPortionInfoCollection : CollectionBase
    {
        public void Add(TextPortionInfo info)
        {
            base.List.Add(info);
        }

        public TextPortionInfo this[int index]
        {
            get
            {
                return this[index];
            }
        }
    }
}

