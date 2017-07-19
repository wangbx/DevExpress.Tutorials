namespace DevExpress.Tutorials.Controls
{
    using DevExpress.Utils.Drawing;
    using DevExpress.XtraEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class OverviewLabel : LabelControl
    {
        private LabelControlAppearanceObject GetFitAppearance(GraphicsCache gc)
        {
            LabelControlAppearanceObject obj2 = new LabelControlAppearanceObject();
            obj2.Assign(base.ViewInfo.PaintAppearance);
            while (obj2.Font.Size > 12f)
            {
                if (obj2.CalcTextSize(gc, this.Text, base.ClientRectangle.Width).Width < base.ClientRectangle.Width)
                {
                    return obj2;
                }
                obj2.Font = new Font(obj2.Font.FontFamily, obj2.Font.Size - 1f);
            }
            return obj2;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!this.IsDesignMode)
            {
                GraphicsCache cache = new GraphicsCache(e.Graphics) {
                    Graphics = { SmoothingMode = SmoothingMode.HighQuality }
                };
                this.GetFitAppearance(cache).DrawString(cache, this.Text, base.ClientRectangle);
            }
        }
    }
}

