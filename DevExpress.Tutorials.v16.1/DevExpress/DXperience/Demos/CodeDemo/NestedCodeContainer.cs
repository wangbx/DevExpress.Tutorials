namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.Runtime.CompilerServices;

    public class NestedCodeContainer
    {
        public NestedCodeContainer(string Code)
        {
            this.Code = this.UserCode = Code;
        }

        public string Code { get; set; }

        public string UserCode { get; set; }
    }
}

