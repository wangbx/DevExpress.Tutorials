namespace DevExpress.Tutorials
{
    using DevExpress.DXperience.Demos;
    using DevExpress.LookAndFeel;
    using DevExpress.Tutorials.Controls;
    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    public class FrmMain : FrmMainBase, ITutorialForm, IWhatsThisProvider
    {
        private string applicationStartupPath = "";
        private Bar bar1;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        protected BarManager barManager1;
        private BarCheckItem bciDisable;
        private BarCheckItem bciDots;
        private BarCheckItem bciFrame;
        private BarCheckItem bciHint;
        private BarListItem bliHatch;
        private BarSubItem bsiHatch;
        private BarSubItem bsiHotTrack;
        protected BarSubItem bsiOptions;
        private BarSubItem bsiShader;
        protected SimpleButton btnAbout;
        protected SimpleButton btnWhatsThis;
        private IContainer components;
        private ImageShaderBase currentShader;
        private bool disableWhatsThisOnMove;
        private LabelControl lbFilter;
        private DevExpress.DXperience.Demos.LookAndFeelMenu mainMenu;
        private Panel pnlFilter;
        private Point prevFormLocation;
        private TextEdit tbFilter;
        private DevExpress.Tutorials.WhatsThisController whatsThisController;
        private bool whatsThisEnabled;
        private ModuleWhatsThis whatsThisModule;

        public FrmMain()
        {
            this.InitializeComponent();
            this.applicationStartupPath = Application.StartupPath;
            base.pnlCaption.Controls.Add(this.btnWhatsThis);
            base.pnlCaption.Controls.Add(this.btnAbout);
            this.btnWhatsThis.Top = this.btnAbout.Top = (base.pnlCaption.Height - this.btnWhatsThis.Height) / 2;
            this.whatsThisController = new DevExpress.Tutorials.WhatsThisController(this);
            this.InitWhatsThisModule();
            this.whatsThisEnabled = false;
            this.disableWhatsThisOnMove = true;
            this.prevFormLocation = Point.Empty;
            this.currentShader = new ImageShaderDisable();
        }

        private void bciDisable_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ClearShaderItems(1);
            this.currentShader = new ImageShaderDisable();
        }

        private void bciDots_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ClearShaderItems(1);
            this.currentShader = new ImageShaderPatternDots();
        }

        private void bciFrame_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.whatsThisModule.SetHotTrackPainter(e.Item.Caption);
        }

        private void bciHint_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.whatsThisModule.SetHotTrackPainter(e.Item.Caption);
        }

        private void bliHatch_ItemClick(object sender, ItemClickEventArgs e)
        {
        }

        private void bliHatch_ListItemClick(object sender, ListItemClickEventArgs e)
        {
            this.ClearShaderItems(0);
            HatchStyle style = (HatchStyle) Enum.Parse(typeof(HatchStyle), ((BarListItem) e.Item).Strings[e.Index]);
            this.currentShader = new ImageShaderHatch(style);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (frmAbout about = new frmAbout(this.GetAboutFileName(this.CurrentModule.TutorialInfo.AboutFile), "About " + this.CurrentModule.TutorialInfo.TutorialName, base.FindForm().Icon))
            {
                about.ShowDialog();
            }
        }

        private void btnWhatsThis_Click(object sender, EventArgs e)
        {
            if (this.whatsThisEnabled)
            {
                this.DisableWhatsThis();
            }
            else
            {
                this.EnableWhatsThis();
            }
        }

        private void ClearShaderItems(int groupIndex)
        {
            foreach (BarItemLink link in this.bsiShader.ItemLinks)
            {
                BarCheckItem item = link.Item as BarCheckItem;
                if (item != null)
                {
                    item.GroupIndex = groupIndex;
                    if (groupIndex == 0)
                    {
                        item.Checked = false;
                    }
                    else
                    {
                        this.bliHatch.ItemIndex = -1;
                    }
                }
            }
        }

        protected virtual DevExpress.DXperience.Demos.LookAndFeelMenu CreateMenu(BarManager manager, DefaultLookAndFeel lookAndFeel)
        {
            return new DevExpress.DXperience.Demos.LookAndFeelMenu(manager, lookAndFeel, string.Empty);
        }

        private bool CurrentModuleFitsInScreen()
        {
            return SystemInformation.WorkingArea.Contains(this.CurrentModule.RectangleToScreen(this.CurrentModule.Bounds));
        }

        void ITutorialForm.ResetNavbarSelectedLink()
        {
        }

        void ITutorialForm.ShowModule(string name)
        {
        }

        protected void DisableWhatsThis()
        {
            if (this.whatsThisEnabled)
            {
                this.NotifyModuleWhatsThisStateChange(false);
                this.whatsThisEnabled = false;
                this.HideWhatsThisModule();
                this.UpdateWhatsThisButton();
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

        protected void EnableWhatsThis()
        {
            if (!this.whatsThisEnabled)
            {
                this.NotifyModuleWhatsThisStateChange(true);
                this.whatsThisEnabled = true;
                this.disableWhatsThisOnMove = true;
                if (this.CurrentModuleFitsInScreen())
                {
                    this.disableWhatsThisOnMove = false;
                    this.prevFormLocation = base.Location;
                }
                this.whatsThisController.UpdateRegisteredVisibleControls();
                this.ShowWhatsThisModule();
                this.UpdateWhatsThisButton();
            }
        }

        protected void FillHatchStyles()
        {
            this.bliHatch.Strings.AddRange(Enum.GetNames(typeof(HatchStyle)));
        }

        private string GetAboutFileName(string fileName)
        {
            return FilesHelper.FindingFileName(this.applicationStartupPath, fileName, false);
        }

        public virtual System.Type GetModuleType(string typeName)
        {
            return System.Type.GetType(typeName);
        }

        protected virtual string GetStartModuleName()
        {
            return string.Empty;
        }

        protected override void HideHint()
        {
            if (this.WhatsThisController != null)
            {
                this.WhatsThisController.HideHint();
            }
        }

        public virtual void HideServiceElements()
        {
            this.DisableWhatsThis();
            base.gcDescription.Visible = base.horzSplitter.Visible = false;
            base.gcNavigations.Hide();
            base.pnlCaption.Hide();
        }

        private void HideWhatsThisModule()
        {
            this.whatsThisModule.Visible = false;
            this.CurrentModule.Show();
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmMain));
            this.btnWhatsThis = new SimpleButton();
            this.barManager1 = new BarManager(this.components);
            this.bar1 = new Bar();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.bsiOptions = new BarSubItem();
            this.bsiShader = new BarSubItem();
            this.bciDisable = new BarCheckItem();
            this.bciDots = new BarCheckItem();
            this.bsiHatch = new BarSubItem();
            this.bliHatch = new BarListItem();
            this.bsiHotTrack = new BarSubItem();
            this.bciHint = new BarCheckItem();
            this.bciFrame = new BarCheckItem();
            this.btnAbout = new SimpleButton();
            this.lbFilter = new LabelControl();
            this.tbFilter = new TextEdit();
            this.pnlFilter = new Panel();
            base.gcNavigations.BeginInit();
            base.gcNavigations.SuspendLayout();
            base.gcContainer.BeginInit();
            base.horzSplitter.BeginInit();
            base.gcDescription.BeginInit();
            base.gcDescription.SuspendLayout();
            base.pcMain.BeginInit();
            base.pcMain.SuspendLayout();
            this.barManager1.BeginInit();
            this.tbFilter.Properties.BeginInit();
            this.pnlFilter.SuspendLayout();
            base.SuspendLayout();
            base.pnlHint.Size = new Size(0x1fa, 0x21);
            base.pnlCaption.Location = new Point(0xa6, 0x18);
            base.pnlCaption.Size = new Size(0x20e, 0x33);
            base.pnlCaption.SizeChanged += new EventHandler(this.pnlCaption_SizeChanged);
            base.gcNavigations.Controls.Add(this.pnlFilter);
            base.gcNavigations.Location = new Point(0, 0x18);
            base.gcNavigations.Size = new Size(0xa6, 0x1c3);
            base.gcContainer.BorderStyle = BorderStyles.NoBorder;
            base.gcContainer.Size = new Size(510, 0x153);
            base.horzSplitter.Location = new Point(8, 0x15b);
            base.horzSplitter.Size = new Size(510, 8);
            base.gcDescription.Location = new Point(8, 0x163);
            base.gcDescription.Size = new Size(510, 0x25);
            base.pcMain.Location = new Point(0xa6, 0x4b);
            base.pcMain.Size = new Size(0x20e, 400);
            this.btnWhatsThis.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnWhatsThis.Location = new Point(0xb8, 0x20);
            this.btnWhatsThis.Name = "btnWhatsThis";
            this.btnWhatsThis.Size = new Size(0x60, 0x20);
            this.btnWhatsThis.TabIndex = 0;
            this.btnWhatsThis.Text = "What's This?";
            this.btnWhatsThis.Visible = false;
            this.btnWhatsThis.Click += new EventHandler(this.btnWhatsThis_Click);
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new Bar[] { this.bar1 });
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.bsiOptions, this.bsiShader, this.bsiHotTrack, this.bciDisable, this.bciDots, this.bsiHatch, this.bliHatch, this.bciHint, this.bciFrame });
            this.barManager1.MainMenu = this.bar1;
            this.barManager1.MaxItemId = 9;
            this.bar1.BarName = "Main Menu";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = BarDockStyle.Top;
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Main Menu";
            this.bsiOptions.Caption = "What's This Options";
            this.bsiOptions.Id = 0;
            this.bsiOptions.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.bsiShader), new LinkPersistInfo(this.bsiHotTrack) });
            this.bsiOptions.Name = "bsiOptions";
            this.bsiShader.Caption = "Choose shader";
            this.bsiShader.Id = 1;
            this.bsiShader.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.bciDisable), new LinkPersistInfo(this.bciDots), new LinkPersistInfo(this.bsiHatch) });
            this.bsiShader.Name = "bsiShader";
            this.bciDisable.Caption = "Disable";
            this.bciDisable.Checked = true;
            this.bciDisable.GroupIndex = 1;
            this.bciDisable.Id = 3;
            this.bciDisable.Name = "bciDisable";
            this.bciDisable.ItemClick += new ItemClickEventHandler(this.bciDisable_ItemClick);
            this.bciDots.Caption = "Dots";
            this.bciDots.GroupIndex = 1;
            this.bciDots.Id = 4;
            this.bciDots.Name = "bciDots";
            this.bciDots.ItemClick += new ItemClickEventHandler(this.bciDots_ItemClick);
            this.bsiHatch.Caption = "Hatch";
            this.bsiHatch.Id = 5;
            this.bsiHatch.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.bliHatch) });
            this.bsiHatch.Name = "bsiHatch";
            this.bliHatch.Caption = "barListItem1";
            this.bliHatch.Id = 6;
            this.bliHatch.Name = "bliHatch";
            this.bliHatch.ShowChecks = true;
            this.bliHatch.ListItemClick += new ListItemClickEventHandler(this.bliHatch_ListItemClick);
            this.bliHatch.ItemClick += new ItemClickEventHandler(this.bliHatch_ItemClick);
            this.bsiHotTrack.Caption = "Choose hot-tracker";
            this.bsiHotTrack.Id = 2;
            this.bsiHotTrack.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.bciHint), new LinkPersistInfo(this.bciFrame) });
            this.bsiHotTrack.Name = "bsiHotTrack";
            this.bciHint.Caption = "Hint";
            this.bciHint.Checked = true;
            this.bciHint.GroupIndex = 2;
            this.bciHint.Id = 7;
            this.bciHint.Name = "bciHint";
            this.bciHint.ItemClick += new ItemClickEventHandler(this.bciHint_ItemClick);
            this.bciFrame.Caption = "Frame";
            this.bciFrame.GroupIndex = 2;
            this.bciFrame.Id = 8;
            this.bciFrame.Name = "bciFrame";
            this.bciFrame.ItemClick += new ItemClickEventHandler(this.bciFrame_ItemClick);
            this.btnAbout.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.btnAbout.Image = (Image) manager.GetObject("btnAbout.Image");
            this.btnAbout.Location = new Point(0x130, 0x20);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new Size(80, 0x20);
            this.btnAbout.TabIndex = 9;
            this.btnAbout.Text = "&About";
            this.btnAbout.Visible = false;
            this.btnAbout.Click += new EventHandler(this.btnAbout_Click);
            this.lbFilter.Location = new Point(3, 7);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new Size(0x1c, 13);
            this.lbFilter.TabIndex = 0;
            this.lbFilter.Text = "Filter:";
            this.tbFilter.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbFilter.Location = new Point(0x22, 3);
            this.tbFilter.MenuManager = this.barManager1;
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new Size(0x68, 20);
            this.tbFilter.TabIndex = 1;
            this.pnlFilter.Controls.Add(this.lbFilter);
            this.pnlFilter.Controls.Add(this.tbFilter);
            this.pnlFilter.Dock = DockStyle.Top;
            this.pnlFilter.Location = new Point(0x16, 2);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new Size(0x8e, 0x1b);
            this.pnlFilter.TabIndex = 2;
            this.pnlFilter.Visible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2b4, 0x1db);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            this.MinimumSize = new Size(700, 500);
            base.Name = "FrmMain";
            this.Text = "XtraEditors Tutorials";
            base.TutorialInfo.AboutFile = null;
            base.TutorialInfo.ImagePatternFill = null;
            base.TutorialInfo.ImageWhatsThisButton = (Image) manager.GetObject("FrmMain.TutorialInfo.ImageWhatsThisButton");
            base.TutorialInfo.ImageWhatsThisButtonStop = (Image) manager.GetObject("FrmMain.TutorialInfo.ImageWhatsThisButtonStop");
            base.TutorialInfo.SourceFileComment = null;
            base.TutorialInfo.SourceFileType = SourceFileType.CS;
            base.Load += new EventHandler(this.MainTutorialForm_Load);
            base.Move += new EventHandler(this.MainTutorialForm_Move);
            base.Resize += new EventHandler(this.MainTutorialForm_Resize);
            base.Controls.SetChildIndex(this.barDockControlTop, 0);
            base.Controls.SetChildIndex(this.barDockControlBottom, 0);
            base.Controls.SetChildIndex(this.barDockControlRight, 0);
            base.Controls.SetChildIndex(this.barDockControlLeft, 0);
            base.Controls.SetChildIndex(base.gcNavigations, 0);
            base.Controls.SetChildIndex(base.pnlCaption, 0);
            base.Controls.SetChildIndex(base.pcMain, 0);
            base.gcNavigations.EndInit();
            base.gcNavigations.ResumeLayout(false);
            base.gcContainer.EndInit();
            base.horzSplitter.EndInit();
            base.gcDescription.EndInit();
            base.gcDescription.ResumeLayout(false);
            base.pcMain.EndInit();
            base.pcMain.ResumeLayout(false);
            this.barManager1.EndInit();
            this.tbFilter.Properties.EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            base.ResumeLayout(false);
        }

        protected virtual void InitMenu()
        {
            this.mainMenu = this.CreateMenu(this.barManager1, base.defaultLookAndFeel);
            this.barManager1.BeginUpdate();
            try
            {
                this.SetItems();
            }
            finally
            {
                this.barManager1.EndUpdate();
            }
        }

        protected virtual void InitModuleInformation()
        {
        }

        private void InitWhatsThisModule()
        {
            this.whatsThisModule = new ModuleWhatsThis(this.whatsThisController);
            this.whatsThisModule.Parent = base.gcContainer;
            this.whatsThisModule.Dock = DockStyle.Fill;
            this.whatsThisModule.Visible = false;
        }

        private void MainTutorialForm_Load(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.UpdateWhatsThisButton();
                this.InitMenu();
                this.InitModuleInformation();
            }
        }

        private void MainTutorialForm_Move(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                if (this.whatsThisEnabled && this.disableWhatsThisOnMove)
                {
                    this.DisableWhatsThis();
                }
                if (this.whatsThisEnabled && !this.disableWhatsThisOnMove)
                {
                    this.whatsThisController.Collection.UpdateControlRects(base.Location.X - this.prevFormLocation.X, base.Location.Y - this.prevFormLocation.Y);
                    this.prevFormLocation = base.Location;
                }
            }
        }

        private void MainTutorialForm_Resize(object sender, EventArgs e)
        {
            if (!base.DesignMode)
            {
                this.DisableWhatsThis();
            }
        }

        protected virtual void NotifyModuleWhatsThisStateChange(bool whatsThisStarted)
        {
            if (this.CurrentModule != null)
            {
                if (whatsThisStarted)
                {
                    this.CurrentModule.StartWhatsThis();
                }
                else
                {
                    this.CurrentModule.EndWhatsThis();
                }
                this.MainMenu.EnabledLookFeelMenu(!whatsThisStarted);
                this.bsiOptions.Enabled = !whatsThisStarted;
            }
        }

        private void pnlCaption_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateWhatsThisButton();
        }

        protected void SelectModule(ModuleBase module, bool makeVisible)
        {
            if (module != null)
            {
                this.DisableWhatsThis();
                this.UpdatePanelsWithModuleInfo(module);
                if (makeVisible)
                {
                    DevExpress.Tutorials.ModuleInfoCollection.ShowModule(module, base.gcContainer);
                }
                else
                {
                    DevExpress.Tutorials.ModuleInfoCollection.SetCurrentModule(module);
                }
                this.WhatsThisController.UpdateWhatsThisInfo(module.TutorialInfo.WhatsThisXMLFile, module.TutorialInfo.WhatsThisCodeFile);
                this.btnWhatsThis.Visible = this.WhatsThisController.IsWhatsThisInfoValid();
                this.btnAbout.Visible = this.GetAboutFileName(module.TutorialInfo.AboutFile) != "";
                base.pnlCaption.ShowLogo(!this.WhatsThisController.IsWhatsThisInfoValid() && !this.btnAbout.Visible);
                this.UpdateAboutButton();
            }
        }

        private void SetItems()
        {
            this.FillHatchStyles();
            this.mainMenu.SetTutorialsMenu();
        }

        public void ShowDemoFilter()
        {
        }

        public virtual void ShowServiceElements()
        {
            this.DisableWhatsThis();
            TutorialInfo info = (this.CurrentModule != null) ? this.CurrentModule.TutorialInfo : null;
            base.gcDescription.Visible = base.horzSplitter.Visible = (info != null) && !string.IsNullOrEmpty(info.Description);
            base.gcNavigations.Show();
            base.pnlCaption.Show();
        }

        private void ShowWhatsThisModule()
        {
            this.whatsThisController.UpdateWhatsThisBitmaps();
            this.CurrentModule.Hide();
            this.whatsThisModule.Show();
        }

        private void UpdateAboutButton()
        {
            if (this.btnAbout.Parent != null)
            {
                if (this.btnWhatsThis.Visible)
                {
                    this.btnAbout.Left = (this.btnWhatsThis.Left - this.btnAbout.Width) - 8;
                }
                else
                {
                    this.btnAbout.Left = (this.btnAbout.Parent.Width - this.btnAbout.Width) - 0x10;
                }
            }
        }

        private void UpdatePanelsWithModuleInfo(ModuleBase module)
        {
            base.gcDescription.Parent.SuspendLayout();
            base.pnlCaption.Text = module.TutorialInfo.TutorialName;
            if ((module.TutorialInfo.Description == string.Empty) || (module.TutorialInfo.Description == null))
            {
                base.gcDescription.Visible = false;
                base.horzSplitter.Visible = false;
            }
            else
            {
                base.pnlHint.Text = module.TutorialInfo.Description;
                base.gcDescription.Visible = true;
                base.horzSplitter.Visible = true;
            }
            base.gcDescription.Parent.ResumeLayout();
        }

        private void UpdateWhatsThisButton()
        {
            if (!base.DesignMode)
            {
                Image imageWhatsThisButton = base.TutorialInfo.ImageWhatsThisButton;
                string str = "What's this?";
                if (this.whatsThisEnabled)
                {
                    imageWhatsThisButton = base.TutorialInfo.ImageWhatsThisButtonStop;
                    str = "Stop";
                }
                this.btnWhatsThis.Text = str;
                this.btnWhatsThis.Image = imageWhatsThisButton;
                try
                {
                    GraphicsInfo info = new GraphicsInfo();
                    info.AddGraphics(null);
                    try
                    {
                        this.btnWhatsThis.Width = this.btnWhatsThis.CalcBestFit(info.Graphics).Width;
                        this.btnWhatsThis.Left = (this.btnWhatsThis.Parent.Width - this.btnWhatsThis.Width) - 0x10;
                    }
                    finally
                    {
                        info.ReleaseGraphics();
                    }
                }
                catch
                {
                }
                this.UpdateAboutButton();
            }
        }

        public ModuleBase CurrentModule
        {
            get
            {
                if (base.DesignMode)
                {
                    return null;
                }
                return DevExpress.Tutorials.ModuleInfoCollection.CurrentModule;
            }
        }

        public ImageShaderBase CurrentShader
        {
            get
            {
                return this.currentShader;
            }
        }

        bool ITutorialForm.AllowDemoFilter
        {
            get
            {
                return false;
            }
        }

        bool ITutorialForm.IsDemoFilterVisible
        {
            get
            {
                return false;
            }
        }

        bool ITutorialForm.IsFullMode
        {
            get
            {
                return !base.gcNavigations.Visible;
            }
        }

        UserControl IWhatsThisProvider.CurrentModule
        {
            get
            {
                return this.CurrentModule;
            }
        }

        public DevExpress.DXperience.Demos.LookAndFeelMenu MainMenu
        {
            get
            {
                return this.mainMenu;
            }
        }

        [Browsable(false)]
        public DevExpress.Tutorials.WhatsThisController WhatsThisController
        {
            get
            {
                return this.whatsThisController;
            }
        }

        [Browsable(false)]
        public bool WhatsThisEnabled
        {
            get
            {
                return this.whatsThisEnabled;
            }
        }
    }
}

