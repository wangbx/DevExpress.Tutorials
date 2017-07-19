namespace DevExpress.Tutorials
{
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class ModuleBase : XtraUserControl
    {
        private Container components;
        private bool setManager;
        private DevExpress.Tutorials.TutorialInfo tutorialInfo;

        public ModuleBase()
        {
            base.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            this.tutorialInfo = new DevExpress.Tutorials.TutorialInfo();
            this.InitializeComponent();
        }

        private void AddManager(Control.ControlCollection collection, BarManager manager)
        {
            foreach (Control control in collection)
            {
                this.SetControlManager(control, manager);
                this.AddManager(control.Controls, manager);
            }
        }

        public void AddMenuManager(BarManager manager)
        {
            if (!this.setManager)
            {
                this.AddManager(base.Controls, manager);
                this.setManager = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual void DoVisibleChanged(bool visible)
        {
        }

        public virtual void EndWhatsThis()
        {
        }

        private void InitializeComponent()
        {
            this.Font = new Font("Tahoma", 8.25f);
            base.Name = "ModuleBase";
            base.Size = new Size(0x310, 0x1b0);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            base.VisibleChanged += new EventHandler(this.OnVisibleChanged);
        }

        protected virtual void OnVisibleChanged(object sender, EventArgs e)
        {
            this.DoVisibleChanged(base.Visible);
        }

        protected virtual void SetControlManager(Control ctrl, BarManager manager)
        {
        }

        public virtual void StartWhatsThis()
        {
        }

        public DevExpress.Tutorials.TutorialInfo TutorialInfo
        {
            get
            {
                return this.tutorialInfo;
            }
        }
    }
}

