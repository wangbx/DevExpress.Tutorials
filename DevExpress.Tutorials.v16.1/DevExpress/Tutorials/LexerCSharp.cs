namespace DevExpress.Tutorials
{
    using System;

    public class LexerCSharp : LexerBase
    {
        public LexerCSharp(string stringToProcess) : base(stringToProcess)
        {
        }

        protected override string[] GetSeparatorStrings()
        {
            return new string[] { "\n", "\r\n", " ", "(", ")", "}", "{", "[", "]", "//", "\t", ";", "," };
        }
    }
}

