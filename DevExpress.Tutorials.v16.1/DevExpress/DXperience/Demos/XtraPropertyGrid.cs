namespace DevExpress.DXperience.Demos
{
    using DevExpress.LookAndFeel;
    using DevExpress.Utils;
    using DevExpress.Utils.Controls;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraVerticalGrid;
    using DevExpress.XtraVerticalGrid.Events;
    using DevExpress.XtraVerticalGrid.Rows;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class XtraPropertyGrid : XtraUserControl
    {
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private BarCheckItem bciAlphabetical;
        private BarCheckItem bciCategories;
        private BarButtonItem biDescription;
        private Bar bMain;
        private IContainer components;
        private Label lbCaption;
        private PanelControl pncDescription;
        private Panel pnlBottom;
        private NotePanelEx pnlHint;
        private Panel pnlTop;
        private PropertyGridControl propertyGridControl1;
        private RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private RepositoryItemColorEdit repositoryItemColorEdit1;

        public XtraPropertyGrid()
        {
            this.InitializeComponent();
            this.biDescription.Down = true;
            this.bciCategories.Checked = true;
            DevExpress.Utils.ImageCollection images = ImageHelper.CreateImageCollectionFromResources("DevExpress.Tutorials.MainDemo.Properties.png", typeof(XtraPropertyGrid).Assembly, new Size(0x10, 0x10));
            this.barManager1.Images = images;
        }

        private void bci_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            this.propertyGridControl1.OptionsView.ShowRootCategories = this.bciCategories.Checked;
        }

        private void biDescription_DownChanged(object sender, ItemClickEventArgs e)
        {
            this.pncDescription.Visible = this.pnlBottom.Visible = this.biDescription.Down;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(XtraPropertyGrid));
            this.pnlHint = new NotePanelEx();
            this.barManager1 = new BarManager(this.components);
            this.bMain = new Bar();
            this.bciCategories = new BarCheckItem();
            this.bciAlphabetical = new BarCheckItem();
            this.biDescription = new BarButtonItem();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.lbCaption = new Label();
            this.pncDescription = new PanelControl();
            this.propertyGridControl1 = new PropertyGridControl();
            this.repositoryItemCheckEdit1 = new RepositoryItemCheckEdit();
            this.repositoryItemColorEdit1 = new RepositoryItemColorEdit();
            this.pnlTop = new Panel();
            this.pnlBottom = new Panel();
            this.barManager1.BeginInit();
            this.pncDescription.BeginInit();
            this.pncDescription.SuspendLayout();
            this.propertyGridControl1.BeginInit();
            this.repositoryItemCheckEdit1.BeginInit();
            this.repositoryItemColorEdit1.BeginInit();
            base.SuspendLayout();
            manager.ApplyResources(this.pnlHint, "pnlHint");
            this.pnlHint.ForeColor = Color.Black;
            this.pnlHint.MaxRows = 10;
            this.pnlHint.Name = "pnlHint";
            this.pnlHint.TabStop = false;
            this.barManager1.Bars.AddRange(new Bar[] { this.bMain });
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.bciCategories, this.bciAlphabetical, this.biDescription });
            this.barManager1.MaxItemId = 3;
            this.bMain.BarName = "Main";
            this.bMain.DockCol = 0;
            this.bMain.DockRow = 0;
            this.bMain.DockStyle = BarDockStyle.Top;
            this.bMain.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.bciCategories), new LinkPersistInfo(this.bciAlphabetical), new LinkPersistInfo(this.biDescription, true) });
            this.bMain.OptionsBar.AllowDelete = true;
            this.bMain.OptionsBar.AllowQuickCustomization = false;
            this.bMain.OptionsBar.DrawDragBorder = false;
            this.bMain.OptionsBar.UseWholeRow = true;
            manager.ApplyResources(this.bMain, "bMain");
            this.bciCategories.GroupIndex = 1;
            manager.ApplyResources(this.bciCategories, "bciCategories");
            this.bciCategories.Id = 0;
            this.bciCategories.ImageIndex = 0;
            this.bciCategories.Name = "bciCategories";
            this.bciCategories.CheckedChanged += new ItemClickEventHandler(this.bci_CheckedChanged);
            this.bciAlphabetical.GroupIndex = 1;
            manager.ApplyResources(this.bciAlphabetical, "bciAlphabetical");
            this.bciAlphabetical.Id = 1;
            this.bciAlphabetical.ImageIndex = 1;
            this.bciAlphabetical.Name = "bciAlphabetical";
            this.bciAlphabetical.CheckedChanged += new ItemClickEventHandler(this.bci_CheckedChanged);
            this.biDescription.ButtonStyle = BarButtonStyle.Check;
            manager.ApplyResources(this.biDescription, "biDescription");
            this.biDescription.Id = 2;
            this.biDescription.ImageIndex = 2;
            this.biDescription.Name = "biDescription";
            this.biDescription.DownChanged += new ItemClickEventHandler(this.biDescription_DownChanged);
            this.barDockControlTop.CausesValidation = false;
            manager.ApplyResources(this.barDockControlTop, "barDockControlTop");
            this.barDockControlBottom.CausesValidation = false;
            manager.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            this.barDockControlLeft.CausesValidation = false;
            manager.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            this.barDockControlRight.CausesValidation = false;
            manager.ApplyResources(this.barDockControlRight, "barDockControlRight");
            this.lbCaption.BackColor = SystemColors.Info;
            manager.ApplyResources(this.lbCaption, "lbCaption");
            this.lbCaption.Name = "lbCaption";
            this.pncDescription.Appearance.BackColor = (Color) manager.GetObject("pncDescription.Appearance.BackColor");
            this.pncDescription.Appearance.Options.UseBackColor = true;
            this.pncDescription.Controls.Add(this.lbCaption);
            this.pncDescription.Controls.Add(this.pnlHint);
            manager.ApplyResources(this.pncDescription, "pncDescription");
            this.pncDescription.LookAndFeel.Style = LookAndFeelStyle.Flat;
            this.pncDescription.LookAndFeel.UseDefaultLookAndFeel = false;
            this.pncDescription.Name = "pncDescription";
            this.propertyGridControl1.DefaultEditors.AddRange(new DevExpress.XtraVerticalGrid.Rows.DefaultEditor[] { new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(bool), this.repositoryItemCheckEdit1), new DevExpress.XtraVerticalGrid.Rows.DefaultEditor(typeof(Color), this.repositoryItemColorEdit1) });
            manager.ApplyResources(this.propertyGridControl1, "propertyGridControl1");
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.RepositoryItems.AddRange(new RepositoryItem[] { this.repositoryItemCheckEdit1, this.repositoryItemColorEdit1 });
            this.propertyGridControl1.FocusedRowChanged += new FocusedRowChangedEventHandler(this.propertyGridControl1_FocusedRowChanged);
            manager.ApplyResources(this.repositoryItemCheckEdit1, "repositoryItemCheckEdit1");
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            manager.ApplyResources(this.repositoryItemColorEdit1, "repositoryItemColorEdit1");
            this.repositoryItemColorEdit1.Buttons.AddRange(new EditorButton[] { new EditorButton((ButtonPredefines) manager.GetObject("repositoryItemColorEdit1.Buttons")) });
            this.repositoryItemColorEdit1.Name = "repositoryItemColorEdit1";
            this.repositoryItemColorEdit1.TextEditStyle = TextEditStyles.Standard;
            manager.ApplyResources(this.pnlTop, "pnlTop");
            this.pnlTop.Name = "pnlTop";
            manager.ApplyResources(this.pnlBottom, "pnlBottom");
            this.pnlBottom.Name = "pnlBottom";
            base.Controls.Add(this.propertyGridControl1);
            base.Controls.Add(this.pnlBottom);
            base.Controls.Add(this.pnlTop);
            base.Controls.Add(this.pncDescription);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.Name = "XtraPropertyGrid";
            manager.ApplyResources(this, "$this");
            base.Resize += new EventHandler(this.XtraPropertyGrid_Resize);
            this.barManager1.EndInit();
            this.pncDescription.EndInit();
            this.pncDescription.ResumeLayout(false);
            this.propertyGridControl1.EndInit();
            this.repositoryItemCheckEdit1.EndInit();
            this.repositoryItemColorEdit1.EndInit();
            base.ResumeLayout(false);
        }

        private void propertyGridControl1_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            PropertyDescriptor propertyDescriptor = null;
            if (e.Row != null)
            {
                propertyDescriptor = this.propertyGridControl1.GetPropertyDescriptor(e.Row);
            }
            if (propertyDescriptor != null)
            {
                this.lbCaption.Text = propertyDescriptor.Name;
                this.pnlHint.Text = propertyDescriptor.Description;
            }
            else if (e.Row != null)
            {
                this.lbCaption.Text = e.Row.Properties.Caption;
                this.pnlHint.Text = string.Empty;
            }
            else
            {
                this.lbCaption.Text = this.pnlHint.Text = string.Empty;
            }
            this.SetPanelHeight();
        }

        private void SetPanelHeight()
        {
            this.pncDescription.Height = (this.lbCaption.Height + this.pnlHint.Height) + 4;
        }

        private void XtraPropertyGrid_Resize(object sender, EventArgs e)
        {
            this.SetPanelHeight();
        }

        public PropertyGridControl PropertyGrid
        {
            get
            {
                return this.propertyGridControl1;
            }
        }

        [DefaultValue(true)]
        public bool ShowButtons
        {
            get
            {
                return this.bMain.Visible;
            }
            set
            {
                this.bMain.Visible = this.pnlTop.Visible = value;
            }
        }

        [DefaultValue(true)]
        public bool ShowCategories
        {
            get
            {
                return this.bciCategories.Checked;
            }
            set
            {
                if (value)
                {
                    this.bciCategories.Checked = true;
                }
                else
                {
                    this.bciAlphabetical.Checked = true;
                }
            }
        }

        [DefaultValue(true)]
        public bool ShowDescription
        {
            get
            {
                return this.biDescription.Down;
            }
            set
            {
                this.biDescription.Down = value;
            }
        }
    }
}

