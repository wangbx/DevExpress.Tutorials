namespace DevExpress.Tutorials
{
    using System.Drawing;

    public class ImageShaderPatternDots : ImageShaderPatternBase
    {
        protected override Bitmap GetFillPattern()
        {
            Bitmap bitmap = new Bitmap(2, 2);
            bitmap.SetPixel(0, 0, Color.FromArgb(150, SystemColors.ControlDarkDark));
            bitmap.SetPixel(0, 1, Color.Transparent);
            bitmap.SetPixel(1, 0, Color.Transparent);
            bitmap.SetPixel(1, 1, Color.FromArgb(150, SystemColors.ControlDarkDark));
            return bitmap;
        }
    }
}

