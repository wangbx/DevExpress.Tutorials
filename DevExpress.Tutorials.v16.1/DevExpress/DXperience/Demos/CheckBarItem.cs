namespace DevExpress.DXperience.Demos
{
    using DevExpress.LookAndFeel;
    using DevExpress.XtraBars;
    using System;

    public class CheckBarItem : OptionBarItem
    {
        private ActiveLookAndFeelStyle style;

        public CheckBarItem(BarManager manager, string text, ItemClickEventHandler handler) : this(manager, text, handler, ActiveLookAndFeelStyle.Flat)
        {
        }

        public CheckBarItem(BarManager manager, string text, ItemClickEventHandler handler, ActiveLookAndFeelStyle style) : base(manager, text, handler, false)
        {
            this.style = style;
        }

        public ActiveLookAndFeelStyle Style
        {
            get
            {
                return this.style;
            }
        }
    }
}

