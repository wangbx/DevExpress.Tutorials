namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ImageShaderDisable : ImageShaderBase
    {
        protected override void PaintShade(Graphics g, Bitmap original)
        {
            ControlPaint.DrawImageDisabled(g, original, 0, 0, Color.Transparent);
        }
    }
}

