namespace DevExpress.Tutorials
{
    using System;

    public class TextPortionInfo
    {
        private TextFormatInfo formatInfo;
        private string text;
        private float x;
        private float y;

        public TextPortionInfo(float x, float y, string text, TextFormatInfo formatInfo)
        {
            this.x = x;
            this.y = y;
            this.text = text;
            this.formatInfo = formatInfo;
        }

        public TextFormatInfo FormatInfo
        {
            get
            {
                return this.formatInfo;
            }
        }

        public string Text
        {
            get
            {
                return this.text;
            }
        }

        public float X
        {
            get
            {
                return this.x;
            }
        }

        public float Y
        {
            get
            {
                return this.y;
            }
        }
    }
}

