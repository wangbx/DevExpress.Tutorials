namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ImageShaderHatch : ImageShaderBase
    {
        private HatchStyle style;

        public ImageShaderHatch(HatchStyle style)
        {
            this.style = style;
        }

        protected override void PaintShade(Graphics g, Bitmap original)
        {
            HatchBrush brush = new HatchBrush(this.style, Color.FromArgb(150, SystemColors.ControlDarkDark), Color.Transparent);
            g.FillRectangle(brush, 0, 0, original.Width, original.Height);
        }
    }
}

