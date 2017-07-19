namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.XtraEditors;
    using System;
    using System.Runtime.CompilerServices;

    public class CodeEvaluationEventArgs : EventArgs
    {
        public DevExpress.DXperience.Demos.CodeDemo.CodeExample CodeExample { get; set; }

        public ExampleLanguage Language { get; set; }

        public bool Result { get; set; }

        public XtraUserControl RootUserControl { get; set; }
    }
}

