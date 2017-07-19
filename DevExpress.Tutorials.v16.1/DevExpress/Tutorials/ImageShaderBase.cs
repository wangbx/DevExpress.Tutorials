namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public class ImageShaderBase
    {
        protected virtual void PaintShade(Graphics g, Bitmap original)
        {
        }

        public Bitmap ShadeBitmap(Bitmap bmp)
        {
            Bitmap image = new Bitmap(bmp);
            Graphics g = Graphics.FromImage(image);
            this.PaintShade(g, bmp);
            return image;
        }
    }
}

