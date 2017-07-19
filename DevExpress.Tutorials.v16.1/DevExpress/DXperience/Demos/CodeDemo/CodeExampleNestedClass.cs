namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.ComponentModel;

    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class CodeExampleNestedClass : CategoryAttribute
    {
        public CodeExampleNestedClass() : base(string.Empty)
        {
        }

        public CodeExampleNestedClass(string description) : base(description)
        {
        }
    }
}

