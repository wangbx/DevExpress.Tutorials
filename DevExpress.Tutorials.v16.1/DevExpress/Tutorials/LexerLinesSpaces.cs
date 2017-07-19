namespace DevExpress.Tutorials
{
    using System;

    public class LexerLinesSpaces : LexerBase
    {
        public LexerLinesSpaces(string stringToProcess) : base(stringToProcess)
        {
        }

        protected override string[] GetSeparatorStrings()
        {
            return new string[] { "\n", "\r\n", " ", "\t" };
        }
    }
}

