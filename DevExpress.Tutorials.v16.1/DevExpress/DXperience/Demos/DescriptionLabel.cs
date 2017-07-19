namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraEditors;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class DescriptionLabel : LabelControl
    {
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if ((base.AutoSizeMode == LabelAutoSizeMode.Vertical) && ((this.Dock == DockStyle.Right) || (this.Dock == DockStyle.Bottom)))
            {
                Size preferredSize = this.GetPreferredSize(new Size(width, height));
                if (this.Dock == DockStyle.Right)
                {
                    x = (x + width) - preferredSize.Width;
                }
                if (this.Dock == DockStyle.Bottom)
                {
                    y = (y + height) - preferredSize.Height;
                }
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }
    }
}

