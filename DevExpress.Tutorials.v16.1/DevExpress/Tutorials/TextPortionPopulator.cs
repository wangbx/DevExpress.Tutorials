namespace DevExpress.Tutorials
{
    using DevExpress.Utils.Drawing;
    using System;
    using System.Drawing;
    using System.Reflection;

    public class TextPortionPopulator
    {
        private double currLineHeight;
        private double currX;
        private double currY;
        private LexemProcessorBase lexemProcessor;
        private LexemProcessorFactory lexemProcessorFactory;
        private LexerBase lexer;
        private LexerFactory lexerFactory;
        private int offsetX;
        private int offsetY;
        private bool skipLine;
        private int totalHeight;
        private int totalWidth;
        private ColoredTextControlViewInfo viewInfo;
        private int wordsInCurrLine;

        public TextPortionPopulator(ColoredTextControlViewInfo viewInfo)
        {
            this.viewInfo = viewInfo;
            this.offsetX = this.offsetY = 0;
            this.lexerFactory = new LexerFactory();
            this.lexemProcessorFactory = new LexemProcessorFactory();
        }

        public Size CalcBestFit(int maxWidth)
        {
            if (!this.viewInfo.Control.WordWrap)
            {
                return new Size(this.totalWidth, this.totalHeight);
            }
            if (maxWidth > this.totalWidth)
            {
                return new Size(this.totalWidth + 2, this.totalHeight + 2);
            }
            TextPortionPopulator populator = new TextPortionPopulator(this.viewInfo);
            populator.Update();
            populator.PopulateText(maxWidth);
            return new Size(populator.TotalWidth + 2, populator.TotalHeight + 2);
        }

        private int GetEmptyLineHeight()
        {
            int num;
            GraphicsInfo info = new GraphicsInfo();
            info.AddGraphics(null);
            try
            {
                num = (int) Math.Ceiling((double) info.Graphics.MeasureString("Qq", this.lexemProcessor.DefaultFont).Height);
            }
            finally
            {
                info.ReleaseGraphics();
            }
            return num;
        }

        private float GetStringWidth(string s)
        {
            float width;
            GraphicsInfo info = new GraphicsInfo();
            info.AddGraphics(null);
            try
            {
                width = info.Graphics.MeasureString(s, this.lexemProcessor.GetStringFormatInfo(s, 0).TextFont).Width;
            }
            finally
            {
                info.ReleaseGraphics();
            }
            return width;
        }

        private void InitLexemProcessor()
        {
            ConstructorInfo constructorByKind = this.lexemProcessorFactory.GetConstructorByKind(this.Control.LexemProcessorKind);
            if (constructorByKind != null)
            {
                this.lexemProcessor = constructorByKind.Invoke(new object[0]) as LexemProcessorBase;
            }
        }

        private void InitLexer()
        {
            ConstructorInfo constructorByKind = this.lexerFactory.GetConstructorByKind(this.Control.LexerKind);
            if (constructorByKind != null)
            {
                this.lexer = constructorByKind.Invoke(new object[] { this.Control.Text }) as LexerBase;
            }
        }

        private bool NeedWrap(string currentString, int maxWidth)
        {
            if (!this.viewInfo.Control.WordWrap)
            {
                return false;
            }
            return (((((this.currX + this.GetStringWidth(currentString)) + this.offsetX) + 4.0) >= maxWidth) && (this.wordsInCurrLine > 1));
        }

        public void PopulateText()
        {
            this.PopulateText(this.viewInfo.Control.Bounds.Width);
        }

        public void PopulateText(int maxWidth)
        {
            this.TextPortions.Clear();
            int markCount = 0;
            while (this.lexer.ReadNext())
            {
                if (this.lexer.CurrentString == "\"")
                {
                    markCount++;
                }
                if (this.lexemProcessor.Init(this.lexer.CurrentString))
                {
                    if (this.ProcessNewLine(this.lexer.CurrentString))
                    {
                        this.UpdateCoordsAfterLine();
                    }
                    else
                    {
                        if (this.NeedWrap(this.lexer.CurrentString, maxWidth))
                        {
                            this.UpdateCoordsAfterLine();
                        }
                        double num2 = this.currX - this.viewInfo.ScrollInfo.ScrollOffsetX;
                        double num3 = this.currY - this.viewInfo.ScrollInfo.ScrollOffsetY;
                        string text = this.lexemProcessor.CorrectString(this.lexer.CurrentString);
                        TextPortionInfo info = new TextPortionInfo((float) num2, (float) num3, text, this.lexemProcessor.GetStringFormatInfo(this.lexer.CurrentString, markCount));
                        this.TextPortions.Add(info);
                        this.UpdateCoordsAfterToken(info);
                    }
                }
                else
                {
                    this.skipLine = this.lexemProcessor.InNotesComment;
                }
            }
            this.UpdateCoordsAfterLine();
            this.UpdateCoordsFinally();
        }

        private bool ProcessNewLine(string currentString)
        {
            if ((currentString != "\n") && (currentString != "\r\n"))
            {
                return false;
            }
            this.lexemProcessor.ProcessAuxiliaryToken("\n");
            return true;
        }

        private void ResetCoords()
        {
            this.totalWidth = 0;
            this.totalHeight = this.offsetY;
            this.currLineHeight = 0.0;
            this.currX = this.offsetX;
            this.currY = this.offsetY;
            this.wordsInCurrLine = 0;
        }

        public void Update()
        {
            this.UpdateOffsets();
            this.ResetCoords();
            this.InitLexer();
            this.InitLexemProcessor();
        }

        private void UpdateCoordsAfterLine()
        {
            if (this.skipLine)
            {
                this.skipLine = false;
            }
            else
            {
                if (this.wordsInCurrLine == 0)
                {
                    this.currLineHeight = this.GetEmptyLineHeight();
                }
                if (this.totalWidth < this.currX)
                {
                    this.totalWidth = (int) Math.Ceiling(this.currX);
                }
                this.currX = this.offsetX;
                this.currY += this.currLineHeight;
                this.currLineHeight = 0.0;
                this.wordsInCurrLine = 0;
            }
        }

        private void UpdateCoordsAfterToken(TextPortionInfo info)
        {
            string text = "w" + info.Text + "w";
            GraphicsInfo info2 = new GraphicsInfo();
            info2.AddGraphics(null);
            try
            {
                SizeF ef = info2.Graphics.MeasureString(text, info.FormatInfo.TextFont);
                SizeF ef2 = info2.Graphics.MeasureString("ww", info.FormatInfo.TextFont);
                if (ef.Height > this.currLineHeight)
                {
                    this.currLineHeight = ef.Height;
                }
                this.currX += Math.Ceiling((double) (ef.Width - ef2.Width));
                this.wordsInCurrLine++;
            }
            finally
            {
                info2.ReleaseGraphics();
            }
        }

        private void UpdateCoordsFinally()
        {
            this.totalHeight = (int) Math.Ceiling(this.currY);
            this.totalHeight += this.offsetY;
            this.totalWidth += this.offsetX + 4;
        }

        private void UpdateOffsets()
        {
            this.offsetX = this.viewInfo.Control.TextPadding;
            this.offsetY = this.viewInfo.Control.TextPadding;
        }

        protected ColoredTextControl Control
        {
            get
            {
                return this.viewInfo.Control;
            }
        }

        protected TextPortionInfoCollection TextPortions
        {
            get
            {
                return this.viewInfo.TextPortions;
            }
        }

        public int TotalHeight
        {
            get
            {
                return this.totalHeight;
            }
        }

        public int TotalWidth
        {
            get
            {
                return this.totalWidth;
            }
        }
    }
}

