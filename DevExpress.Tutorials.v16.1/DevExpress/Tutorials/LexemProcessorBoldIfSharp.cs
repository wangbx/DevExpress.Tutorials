namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public class LexemProcessorBoldIfSharp : LexemProcessorBase
    {
        private TextFormatInfo boldInfo = new TextFormatInfo(SystemColors.InfoText, new Font("Tahoma", 8f, FontStyle.Bold));
        private bool currentWordInBold = false;
        private TextFormatInfo regularInfo = new TextFormatInfo(SystemColors.InfoText, new Font("Tahoma", 8f));

        public override string CorrectString(string s)
        {
            if (s.StartsWith("#"))
            {
                return s.Substring(1);
            }
            return s;
        }

        public override TextFormatInfo GetStringFormatInfo(string s, int markCount)
        {
            this.currentWordInBold = false;
            if (s.StartsWith("#"))
            {
                this.currentWordInBold = true;
            }
            return this.InternalGetStringFormatInfo();
        }

        private TextFormatInfo InternalGetStringFormatInfo()
        {
            if (this.currentWordInBold)
            {
                return this.boldInfo;
            }
            return this.regularInfo;
        }

        public override Font DefaultFont
        {
            get
            {
                return this.regularInfo.TextFont;
            }
        }
    }
}

