namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.ComponentModel;

    [AttributeUsage(AttributeTargets.Method)]
    public class CodeExampleCase : CategoryAttribute
    {
        public Type[] Types;

        public CodeExampleCase(string val) : this(val, null)
        {
        }

        public CodeExampleCase(string val, Type[] types) : base(val)
        {
            this.Types = types;
        }
    }
}

