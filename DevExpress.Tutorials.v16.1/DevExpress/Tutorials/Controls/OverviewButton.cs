namespace DevExpress.Tutorials.Controls
{
    using DevExpress.Utils.Frames;
    using DevExpress.XtraLayout;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class OverviewButton : PictureButton
    {
        public OverviewButton(Control parent, Image normal, Image hover, Image pressed, string processStartLink) : base(parent, normal, hover, pressed, processStartLink)
        {
        }

        protected override void SetActive(Image image, bool active)
        {
            base.Image = image;
            if (base.Parent != null)
            {
                LayoutControl parent = base.Parent.Parent as LayoutControl;
                if (parent != null)
                {
                    LayoutControlItem itemByControl = parent.GetItemByControl(base.Parent);
                    if (itemByControl.TextVisible)
                    {
                        itemByControl.AppearanceItemCaption.Font = new Font(itemByControl.AppearanceItemCaption.Font, active ? FontStyle.Underline : FontStyle.Regular);
                    }
                }
            }
        }
    }
}

