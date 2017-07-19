namespace DevExpress.Tutorials
{
    using DevExpress.Tutorials.Win;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ColoredHint : ToolTipWindow
    {
        private ColoredTextControl control;

        public ColoredHint()
        {
            this.InitializeComponent();
        }

        private void ColoredHintCalcSize(object sender, ToolTipCalcSizeEventArgs e)
        {
            e.Size = this.control.CalcBestFit(300);
        }

        private void CreateTextControl()
        {
        }

        private void InitializeComponent()
        {
            this.control = new ColoredTextControl();
            base.SuspendLayout();
            this.control.Dock = DockStyle.Left;
            this.control.LexemProcessorKind = "BoldIfSharp";
            this.control.LexerKind = "Lines";
            this.control.Name = "control";
            this.control.Size = new Size(0x328, 180);
            this.control.TabIndex = 0;
            this.control.Text = string.Empty;
            this.control.WordWrap = true;
            this.control.HintBorderVisible = true;
            this.AutoScaleBaseSize = new Size(5, 14);
            base.ClientSize = new Size(320, 180);
            base.Controls.AddRange(new Control[] { this.control });
            base.Name = "ColoredHint";
            base.ToolTipCalcSize += new ToolTipCalcSizeEventHandler(this.ColoredHintCalcSize);
            base.ResumeLayout(false);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            MessageBox.Show("Works");
            base.OnDeactivate(e);
        }

        public void ShowAtControl(Control ctrl)
        {
            base.Size = new Size(10, 10);
            Point bottomPosition = ctrl.PointToScreen(new Point(0, ctrl.Height));
            base.ShowTip(bottomPosition, bottomPosition);
            this.control.Update();
            base.Size = this.control.CalcBestFit(300);
            this.control.Size = base.Size;
        }

        public string ColoredHintText
        {
            get
            {
                return this.control.Text;
            }
            set
            {
                this.control.Text = value;
            }
        }
    }
}

