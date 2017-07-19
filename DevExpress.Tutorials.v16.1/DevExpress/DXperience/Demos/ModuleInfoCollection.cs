namespace DevExpress.DXperience.Demos
{
    using System;
    using System.Collections;
    using System.Reflection;

    internal class ModuleInfoCollection : CollectionBase
    {
        public void Add(ModuleInfo value)
        {
            if (base.List.IndexOf(value) < 0)
            {
                base.List.Add(value);
            }
        }

        public int IndexOf(ModuleInfo value)
        {
            return base.List.IndexOf(value);
        }

        public ModuleInfo this[int index]
        {
            get
            {
                if (base.List.Count > index)
                {
                    return (base.List[index] as ModuleInfo);
                }
                return null;
            }
        }

        public ModuleInfo this[string fName]
        {
            get
            {
                foreach (ModuleInfo info in this)
                {
                    if (info.Name.Equals(fName))
                    {
                        return info;
                    }
                }
                return null;
            }
        }
    }
}

