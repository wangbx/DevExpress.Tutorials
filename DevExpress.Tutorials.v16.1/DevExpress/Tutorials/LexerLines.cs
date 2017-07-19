namespace DevExpress.Tutorials
{
    using System;

    public class LexerLines : LexerBase
    {
        public LexerLines(string stringToProcess) : base(stringToProcess)
        {
        }

        protected override string[] GetSeparatorStrings()
        {
            return new string[] { "\n", "\r\n", Environment.NewLine };
        }
    }
}

