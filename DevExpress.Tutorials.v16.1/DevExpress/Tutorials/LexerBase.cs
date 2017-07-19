namespace DevExpress.Tutorials
{
    using System;

    public class LexerBase
    {
        private int currentPos;
        private string currentString;
        private string stringToProcess;

        public LexerBase(string stringToProcess)
        {
            this.stringToProcess = stringToProcess;
            this.currentPos = 0;
            this.currentString = string.Empty;
        }

        private bool ExtractHeadingSeparator(string s)
        {
            foreach (string str in this.GetSeparatorStrings())
            {
                if (s.StartsWith(str))
                {
                    this.currentString = str;
                    this.currentPos += str.Length;
                    return true;
                }
            }
            return false;
        }

        protected virtual string[] GetSeparatorStrings()
        {
            return new string[0];
        }

        public bool ReadNext()
        {
            if (this.currentPos >= (this.stringToProcess.Length - 1))
            {
                return false;
            }
            if (!this.ExtractHeadingSeparator(this.stringToProcess.Substring(this.currentPos)))
            {
                this.ReadNextToken();
            }
            return true;
        }

        private void ReadNextToken()
        {
            int currentPos = this.currentPos;
            while (currentPos < this.stringToProcess.Length)
            {
                if (this.StringStartsWithSeparator(this.stringToProcess.Substring(currentPos)))
                {
                    break;
                }
                currentPos++;
            }
            this.currentString = this.stringToProcess.Substring(this.currentPos, currentPos - this.currentPos);
            this.currentPos = currentPos;
        }

        private bool StringStartsWithSeparator(string s)
        {
            foreach (string str in this.GetSeparatorStrings())
            {
                if (s.StartsWith(str))
                {
                    return true;
                }
            }
            return false;
        }

        public string CurrentString
        {
            get
            {
                return this.currentString;
            }
        }
    }
}

