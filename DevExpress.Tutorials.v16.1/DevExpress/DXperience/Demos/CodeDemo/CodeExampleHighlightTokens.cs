namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class CodeExampleHighlightTokens : Attribute
    {
        public CodeExampleHighlightTokens(params string[] tokens)
        {
            this.Tokens = tokens.ToList<string>();
        }

        public List<string> Tokens { get; private set; }
    }
}

