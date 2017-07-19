namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraBars;
    using System;

    public class ButtonBarItem : BarButtonItem
    {
        public ButtonBarItem(BarManager manager, string text, ItemClickEventHandler handler) : this(manager, text, -1, handler)
        {
        }

        public ButtonBarItem(BarManager manager, string text, int imageIndex, ItemClickEventHandler handler)
        {
            base.Manager = manager;
            this.Caption = text;
            this.ImageIndex = imageIndex;
            base.ItemClick += handler;
        }
    }
}

