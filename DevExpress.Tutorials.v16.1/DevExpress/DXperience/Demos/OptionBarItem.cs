namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraBars;
    using System;

    public class OptionBarItem : BarCheckItem
    {
        private bool optionItem;

        public OptionBarItem(BarManager manager, string text, ItemClickEventHandler handler) : this(manager, text, handler, null, true)
        {
        }

        public OptionBarItem(BarManager manager, string text, ItemClickEventHandler handler, object tag) : this(manager, text, handler, tag, true)
        {
        }

        public OptionBarItem(BarManager manager, string text, ItemClickEventHandler handler, object tag, bool optionItem)
        {
            this.optionItem = optionItem;
            base.Manager = manager;
            this.Caption = text;
            base.Tag = tag;
            base.ItemClick += handler;
        }

        public bool IsOption
        {
            get
            {
                return this.optionItem;
            }
        }
    }
}

