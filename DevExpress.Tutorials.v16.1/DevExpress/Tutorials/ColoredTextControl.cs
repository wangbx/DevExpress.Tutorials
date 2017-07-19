namespace DevExpress.Tutorials
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class ColoredTextControl : Control
    {
        private int bestFitMaxWidth;
        private bool hintBorderVisible;
        private string lexemProcessorKind;
        private string lexerKind;
        private int textPadding;
        private ColoredTextControlViewInfo viewInfo;
        private bool viewInfoValid = false;
        private bool wordWrap;

        public ColoredTextControl()
        {
            this.viewInfo = new ColoredTextControlViewInfo(this);
            this.lexerKind = "CSharp";
            this.lexemProcessorKind = "CSharp";
            this.Text = string.Empty;
            this.textPadding = 8;
            this.wordWrap = false;
            this.hintBorderVisible = true;
            this.bestFitMaxWidth = 300;
            base.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
        }

        public void BestFit()
        {
            base.Size = this.CalcBestFit(this.bestFitMaxWidth);
        }

        public Size CalcBestFit(int maxWidth)
        {
            return this.viewInfo.CalcBestFit(maxWidth);
        }

        public void CalculateAndInvalidate()
        {
            this.viewInfoValid = false;
            this.viewInfo.Calculate();
            base.Invalidate();
        }

        private void DrawHintBorder(Graphics g)
        {
            g.DrawLine(Pens.White, new Point(0, 0), new Point(base.Width - 2, 0));
            g.DrawLine(Pens.White, new Point(0, 0), new Point(0, base.Height - 2));
            g.DrawLine(Pens.Black, new Point(base.Width - 1, 0), new Point(base.Width - 1, base.Height));
            g.DrawLine(Pens.Black, new Point(0, base.Height - 1), new Point(base.Width, base.Height - 1));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.viewInfoValid)
            {
                this.viewInfo.Calculate();
                this.viewInfoValid = true;
            }
            this.viewInfo.Paint(e.Graphics);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.CalculateAndInvalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.CalculateAndInvalidate();
        }

        [DefaultValue(300), Browsable(true)]
        public int BestFitMaxWidth
        {
            get
            {
                return this.bestFitMaxWidth;
            }
            set
            {
                this.bestFitMaxWidth = value;
            }
        }

        [Browsable(true)]
        public bool HintBorderVisible
        {
            get
            {
                return this.hintBorderVisible;
            }
            set
            {
                if (value != this.hintBorderVisible)
                {
                    this.hintBorderVisible = value;
                    base.Invalidate();
                }
            }
        }

        [Browsable(true)]
        public string LexemProcessorKind
        {
            get
            {
                return this.lexemProcessorKind;
            }
            set
            {
                if (this.lexemProcessorKind != value)
                {
                    this.lexemProcessorKind = value;
                    this.CalculateAndInvalidate();
                }
            }
        }

        [Browsable(true)]
        public string LexerKind
        {
            get
            {
                return this.lexerKind;
            }
            set
            {
                if (this.lexerKind != value)
                {
                    this.lexerKind = value;
                    this.CalculateAndInvalidate();
                }
            }
        }

        [Browsable(true), DefaultValue(4)]
        public int TextPadding
        {
            get
            {
                return this.textPadding;
            }
            set
            {
                if (this.textPadding != value)
                {
                    this.textPadding = value;
                    this.CalculateAndInvalidate();
                }
            }
        }

        public ColoredTextControlViewInfo ViewInfo
        {
            get
            {
                return this.viewInfo;
            }
        }

        [Browsable(true), DefaultValue(false)]
        public bool WordWrap
        {
            get
            {
                return this.wordWrap;
            }
            set
            {
                if (this.wordWrap != value)
                {
                    this.wordWrap = value;
                    this.CalculateAndInvalidate();
                }
            }
        }
    }
}

