namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraNavBar;
    using DevExpress.XtraNavBar.ViewInfo;

    public class TouchNavBarControl : NavBarControl
    {
        protected override NavBarViewInfo CreateViewInfo()
        {
            NavBarViewInfo info = base.CreateViewInfo();
            info.ScrollBar.TouchMode = true;
            return info;
        }
    }
}

