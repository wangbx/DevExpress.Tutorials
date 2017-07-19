namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CodeExampleVersionID : Attribute
    {
        public CodeExampleVersionID(int id)
        {
            this.VersionID = id;
        }

        public int VersionID { get; private set; }
    }
}

