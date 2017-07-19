namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public class TextFormatInfo
    {
        private Color textColor;
        private Font textFont;

        public TextFormatInfo(Color color, Font font)
        {
            this.textColor = color;
            this.textFont = font;
        }

        public static TextFormatInfo Empty
        {
            get
            {
                return new TextFormatInfo(Color.Black, new Font("Tahoma", 8f));
            }
        }

        public Color TextColor
        {
            get
            {
                return this.textColor;
            }
        }

        public Font TextFont
        {
            get
            {
                return this.textFont;
            }
        }
    }
}

