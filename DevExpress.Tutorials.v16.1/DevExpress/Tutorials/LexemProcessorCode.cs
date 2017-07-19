namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public class LexemProcessorCode : LexemProcessorBase
    {
        private TextFormatInfo commentInfo;
        private TextFormatInfo defaultInfo;
        private Font font = new Font("Courier", 8.25f);
        private bool inComment = false;
        private bool inNotesComment = false;
        private TextFormatInfo keywordInfo;

        public LexemProcessorCode()
        {
            this.commentInfo = this.GetCommentInfo();
            this.keywordInfo = this.GetKeywordInfo();
            this.defaultInfo = this.GetDefaultInfo();
        }

        public override string CorrectString(string s)
        {
            if (s.StartsWith("~"))
            {
                return s.Substring(1);
            }
            return s;
        }

        protected virtual TextFormatInfo GetCommentInfo()
        {
            return new TextFormatInfo(Color.Green, this.font);
        }

        protected virtual TextFormatInfo GetDefaultInfo()
        {
            return new TextFormatInfo(Color.Black, this.font);
        }

        protected virtual TextFormatInfo GetKeywordInfo()
        {
            return new TextFormatInfo(Color.Blue, this.font);
        }

        protected virtual string[] GetKeywords()
        {
            return new string[0];
        }

        public override TextFormatInfo GetStringFormatInfo(string s, int markCount)
        {
            if (this.inComment)
            {
                return this.commentInfo;
            }
            if (this.IsStartCommentString(s))
            {
                this.inComment = (markCount % 2) == 0;
                return this.commentInfo;
            }
            if (this.IsKeyword(s) && !this.inNotesComment)
            {
                return this.keywordInfo;
            }
            return this.defaultInfo;
        }

        public override bool Init(string s)
        {
            if (s.StartsWith("~") && this.inNotesComment)
            {
                this.inComment = true;
            }
            if (this.IsStartNotesCommentString(s))
            {
                this.inNotesComment = true;
                return false;
            }
            if (this.IsEndNotesCommentString(s))
            {
                this.inNotesComment = false;
                this.inComment = false;
                return false;
            }
            return true;
        }

        protected virtual bool IsEndNotesCommentString(string s)
        {
            return false;
        }

        private bool IsKeyword(string s)
        {
            foreach (string str in this.GetKeywords())
            {
                if (s == str)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsNewLine(string s)
        {
            return (s == "\n");
        }

        protected virtual bool IsStartCommentString(string s)
        {
            return false;
        }

        protected virtual bool IsStartNotesCommentString(string s)
        {
            return false;
        }

        public override void ProcessAuxiliaryToken(string s)
        {
            if (this.IsNewLine(s))
            {
                this.inComment = false;
            }
        }

        public override Font DefaultFont
        {
            get
            {
                return this.defaultInfo.TextFont;
            }
        }

        public override bool InNotesComment
        {
            get
            {
                return this.inNotesComment;
            }
        }
    }
}

