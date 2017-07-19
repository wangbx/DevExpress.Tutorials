namespace DevExpress.DXperience.Demos
{
    using DevExpress.LookAndFeel;
    using DevExpress.XtraBars;
    using System;

    public class CheckBarItemWithStyle : CheckBarItem
    {
        private DevExpress.LookAndFeel.LookAndFeelStyle lfStyle;

        public CheckBarItemWithStyle(BarManager manager, string text, ItemClickEventHandler handler, ActiveLookAndFeelStyle style, DevExpress.LookAndFeel.LookAndFeelStyle lfStyle) : base(manager, text, handler, style)
        {
            this.lfStyle = lfStyle;
        }

        public DevExpress.LookAndFeel.LookAndFeelStyle LookAndFeelStyle
        {
            get
            {
                return this.lfStyle;
            }
        }
    }
}

