namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public class ImageShaderPatternBase : ImageShaderBase
    {
        protected virtual Bitmap GetFillPattern()
        {
            Bitmap bitmap = new Bitmap(1, 1);
            bitmap.SetPixel(0, 0, Color.Transparent);
            return bitmap;
        }

        protected override void PaintShade(Graphics g, Bitmap original)
        {
            TextureBrush brush = new TextureBrush(this.GetFillPattern());
            g.FillRectangle(brush, 0, 0, original.Width, original.Height);
        }
    }
}

