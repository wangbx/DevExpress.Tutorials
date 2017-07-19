namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public abstract class LexemProcessorBase
    {
        protected LexemProcessorBase()
        {
        }

        public virtual string CorrectString(string s)
        {
            return s;
        }

        public abstract TextFormatInfo GetStringFormatInfo(string s, int markCount);
        public virtual bool Init(string s)
        {
            return true;
        }

        public virtual void ProcessAuxiliaryToken(string s)
        {
        }

        public abstract Font DefaultFont { get; }

        public virtual bool InNotesComment
        {
            get
            {
                return false;
            }
        }
    }
}

