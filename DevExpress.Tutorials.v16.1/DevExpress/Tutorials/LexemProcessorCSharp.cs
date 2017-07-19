namespace DevExpress.Tutorials
{
    using System;

    public class LexemProcessorCSharp : LexemProcessorCode
    {
        protected override string[] GetKeywords()
        {
            return new string[] { 
                "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate",
                "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit",
                "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private",
                "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw",
                "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "volatile", "void", "while"
            };
        }

        protected override bool IsEndNotesCommentString(string s)
        {
            return (s.Trim() == "*/");
        }

        protected override bool IsStartCommentString(string s)
        {
            return (s == "//");
        }

        protected override bool IsStartNotesCommentString(string s)
        {
            return (s.Trim() == "/*");
        }
    }
}

