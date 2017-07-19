namespace DevExpress.Tutorials
{
    using DevExpress.XtraEditors;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ModuleWhatsThis : XtraUserControl
    {
        private IContainer components;
        private WhatsThisController controller;
        private Control hotTrackedControl;
        private ControlHotTrackPainterBase hotTrackPainter;
        private bool parentFormActive;

        public ModuleWhatsThis(WhatsThisController controller)
        {
            this.InitializeComponent();
            this.controller = controller;
            this.hotTrackedControl = null;
            base.SetStyle(ControlStyles.DoubleBuffer, true);
            this.parentFormActive = true;
            this.hotTrackPainter = new ControlHotTrackPainterHint(this);
        }

        private void AssignParentFormHandlers()
        {
            base.ParentForm.Activated += new EventHandler(this.ParentFormActivated);
            base.ParentForm.Deactivate += new EventHandler(this.ParentFormDeactivate);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void HotTrackControlChanging(Control prevHotControl, Control currHotControl)
        {
            this.InvalidateControl(prevHotControl);
            this.InvalidateControl(currHotControl);
        }

        private void InitializeComponent()
        {
            this.Cursor = Cursors.Help;
            base.Name = "ModuleWhatsThis";
            base.Click += new EventHandler(this.ModuleWhatsThis_Click);
            base.Load += new EventHandler(this.ModuleWhatsThis_Load);
            base.Paint += new PaintEventHandler(this.ModuleWhatsThis_Paint);
            base.MouseMove += new MouseEventHandler(this.ModuleWhatsThis_MouseMove);
            base.MouseLeave += new EventHandler(this.ModuleWhatsThis_MouseLeave);
        }

        private void InvalidateControl(Control control)
        {
            if (control != null)
            {
                foreach (WhatsThisControlEntry entry in this.controller.RegisteredVisibleControls)
                {
                    if (entry.Control == control)
                    {
                        Region invalidateRegion = this.HotTrackPainter.GetInvalidateRegion(entry);
                        base.Invalidate(invalidateRegion);
                        break;
                    }
                }
            }
        }

        private void ModuleWhatsThis_Click(object sender, EventArgs e)
        {
            if (this.hotTrackedControl != null)
            {
                this.controller.TryToShowWhatsThis(this.hotTrackedControl);
                this.InvalidateControl(this.hotTrackedControl);
            }
        }

        private void ModuleWhatsThis_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.AssignParentFormHandlers();
            }
        }

        private void ModuleWhatsThis_MouseLeave(object sender, EventArgs e)
        {
            this.HotTrackedControl = null;
        }

        private void ModuleWhatsThis_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.parentFormActive)
            {
                bool flag = false;
                foreach (WhatsThisControlEntry entry in this.controller.RegisteredVisibleControls)
                {
                    if (base.RectangleToClient(entry.ControlScreenRect).Contains(e.X, e.Y))
                    {
                        this.HotTrackedControl = entry.Control;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    this.HotTrackedControl = null;
                }
            }
        }

        private void ModuleWhatsThis_Paint(object sender, PaintEventArgs e)
        {
            if (!base.DesignMode && ((this.controller != null) && (this.controller.WhatsThisModuleBitmap != null)))
            {
                using (Bitmap bitmap = new Bitmap(this.controller.WhatsThisModuleBitmap))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        foreach (WhatsThisControlEntry entry in this.controller.RegisteredVisibleControls)
                        {
                            if (entry.Control == this.HotTrackedControl)
                            {
                                this.HotTrackPainter.DrawHotTrackSign(graphics, entry);
                            }
                        }
                        e.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
                    }
                }
            }
        }

        private void ParentFormActivated(object sender, EventArgs e)
        {
            this.parentFormActive = true;
        }

        private void ParentFormDeactivate(object sender, EventArgs e)
        {
            this.parentFormActive = false;
            this.HotTrackedControl = null;
        }

        public void SetHotTrackPainter(string painterName)
        {
            string str = painterName;
            if (str != null)
            {
                if (str != "Hint")
                {
                    if (str != "Frame")
                    {
                        return;
                    }
                }
                else
                {
                    this.hotTrackPainter = new ControlHotTrackPainterHint(this);
                    return;
                }
                this.hotTrackPainter = new ControlHotTrackPainterFrame(this);
            }
        }

        private Control HotTrackedControl
        {
            get
            {
                return this.hotTrackedControl;
            }
            set
            {
                if (this.hotTrackedControl != value)
                {
                    this.HotTrackControlChanging(this.hotTrackedControl, value);
                    this.hotTrackedControl = value;
                }
            }
        }

        public ControlHotTrackPainterBase HotTrackPainter
        {
            get
            {
                return this.hotTrackPainter;
            }
        }
    }
}

