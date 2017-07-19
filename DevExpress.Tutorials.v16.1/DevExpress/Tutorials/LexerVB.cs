namespace DevExpress.Tutorials
{
    using System;

    public class LexerVB : LexerBase
    {
        public LexerVB(string stringToProcess) : base(stringToProcess)
        {
        }

        protected override string[] GetSeparatorStrings()
        {
            return new string[] { "\n", "\r\n", " ", "(", ")", "[", "]", "'", "\t", "," };
        }
    }
}

