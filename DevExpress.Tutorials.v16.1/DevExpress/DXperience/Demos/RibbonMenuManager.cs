namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraBars;
    using System;

    public class RibbonMenuManager
    {
        private RibbonMainForm parentForm;
        private DevExpress.DXperience.Demos.PrintOptions printOptions = new DevExpress.DXperience.Demos.PrintOptions();

        public RibbonMenuManager(RibbonMainForm parentForm)
        {
            this.parentForm = parentForm;
        }

        public void AllowExport(object obj)
        {
            this.parentForm.PrintExportGroup.Visible = obj != null;
        }

        public void ShowReservGroup1(bool show)
        {
            this.parentForm.ReservGroup1.Visible = show;
        }

        public void ShowReservGroup2(bool show)
        {
            this.parentForm.ReservGroup2.Visible = show;
        }

        public BarManager Manager
        {
            get
            {
                return this.parentForm.Ribbon.Manager;
            }
        }

        public DevExpress.DXperience.Demos.PrintOptions PrintOptions
        {
            get
            {
                return this.printOptions;
            }
        }
    }
}

