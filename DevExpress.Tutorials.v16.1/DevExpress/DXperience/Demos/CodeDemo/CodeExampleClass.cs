namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.ComponentModel;

    [AttributeUsage(AttributeTargets.Class)]
    public class CodeExampleClass : CategoryAttribute
    {
        private readonly string fileName;

        public CodeExampleClass(string codeExampleName, string fileName) : base(codeExampleName)
        {
            this.fileName = fileName;
        }

        public string FileName
        {
            get
            {
                return this.fileName;
            }
        }
    }
}

