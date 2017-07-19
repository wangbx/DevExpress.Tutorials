namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public class ColoredTextControlViewInfo
    {
        private ColoredTextControl control;
        private TextPortionPopulator populator;
        private ScrollBarInfo scrollInfo;
        private TextPortionInfoCollection textPortions = new TextPortionInfoCollection();

        public ColoredTextControlViewInfo(ColoredTextControl control)
        {
            this.populator = new TextPortionPopulator(this);
            this.control = control;
            this.scrollInfo = new ScrollBarInfo(this);
        }

        public Size CalcBestFit(int maxWidth)
        {
            return this.populator.CalcBestFit(maxWidth);
        }

        public void Calculate()
        {
            this.populator.Update();
            this.populator.PopulateText();
            this.scrollInfo.UpdateScrollInfo();
        }

        public void Paint(Graphics g)
        {
            foreach (TextPortionInfo info in this.textPortions)
            {
                this.PaintTextPortion(g, info);
            }
        }

        protected void PaintTextPortion(Graphics g, TextPortionInfo info)
        {
            g.DrawString(info.Text, info.FormatInfo.TextFont, new SolidBrush(info.FormatInfo.TextColor), info.X, info.Y);
        }

        public ColoredTextControl Control
        {
            get
            {
                return this.control;
            }
        }

        public TextPortionPopulator Populator
        {
            get
            {
                return this.populator;
            }
        }

        public ScrollBarInfo ScrollInfo
        {
            get
            {
                return this.scrollInfo;
            }
        }

        public TextPortionInfoCollection TextPortions
        {
            get
            {
                return this.textPortions;
            }
        }
    }
}

