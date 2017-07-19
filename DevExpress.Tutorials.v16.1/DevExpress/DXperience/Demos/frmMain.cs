namespace DevExpress.DXperience.Demos
{
    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.Utils;
    using DevExpress.Utils.About;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraNavBar;
    using DevExpress.XtraNavBar.ViewInfo;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class frmMain : XtraForm, ITutorialForm
    {
        protected DevExpress.DXperience.Demos.LookAndFeelMenu appearanceMenu;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private BarStaticItem bsData;
        private BarStaticItem bsUser;
        private IContainer components;
        protected DefaultLookAndFeel defaultLookAndFeel1;
        private NavBarFilter filter;
        private bool fullWindow;
        private GroupControl gcContainer;
        private GroupControl gcDescription;
        private GroupControl gcNavigations;
        private PanelControl horzSplitter;
        private LabelControl lbFilter;
        private Bar mainMenu;
        private int maxHintRows;
        protected NavBarControl navBarControl1;
        private PanelControl panelControl1;
        private PanelControl pcActions;
        protected ApplicationCaption8_1 pnlCaption;
        private Panel pnlFilter;
        private NotePanel8_1 pnlHint;
        private NavBarItemLink prevLink;
        private string prevModuleName;
        private SimpleButton sbDescription;
        private SimpleButton sbRun;
        private string startModule;
        private Bar statusBar;
        private TextEdit tbFilter;

        public frmMain() : this(new string[0])
        {
        }

        public frmMain(string[] arguments)
        {
            this.startModule = string.Empty;
            this.prevModuleName = "";
            this.maxHintRows = 0x19;
            UAlgo.Default.DoEventObject(1, 1, this);
            this.InitializeComponent();
            SkinManager.EnableFormSkins();
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            this.navBarControl1.MenuManager = this.barManager1;
            if (this.ShowStatusBar)
            {
                this.statusBar.Visible = true;
            }
            this.SetFormParam();
            this.filter = new NavBarFilter(this.navBarControl1);
            this.pnlFilter.Visible = this.AllowNavBarFilter;
            MainFormHelper.SetCommandLineArgs(Environment.GetCommandLineArgs(), out this.startModule, out this.fullWindow);
        }

        protected virtual void CreateMenu()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.filter != null)
                {
                    this.filter.Dispose();
                }
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected virtual void FillNavBar()
        {
            ModulesInfo.FillNavBar(this.navBarControl1);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.prevModuleName != "")
            {
                bool flag = ModulesInfo.Instance.CurrentModuleBase.Name == ModulesInfo.GetItem(0).Name;
                if (((e.KeyData == Keys.Escape) && flag) && (this.prevLink != null))
                {
                    this.navBarControl1.SelectedLink = this.prevLink;
                }
            }
        }

        public void HideServiceElements()
        {
            this.gcDescription.Visible = this.horzSplitter.Visible = false;
            this.gcNavigations.Hide();
            this.pnlCaption.Hide();
            this.navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Collapsed;
            this.statusBar.Visible = false;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.gcNavigations = new GroupControl();
            this.navBarControl1 = new NavBarControl();
            this.pnlFilter = new Panel();
            this.lbFilter = new LabelControl();
            this.tbFilter = new TextEdit();
            this.barManager1 = new BarManager(this.components);
            this.mainMenu = new Bar();
            this.statusBar = new Bar();
            this.bsUser = new BarStaticItem();
            this.bsData = new BarStaticItem();
            this.barDockControlTop = new BarDockControl();
            this.barDockControlBottom = new BarDockControl();
            this.barDockControlLeft = new BarDockControl();
            this.barDockControlRight = new BarDockControl();
            this.defaultLookAndFeel1 = new DefaultLookAndFeel(this.components);
            this.panelControl1 = new PanelControl();
            this.gcContainer = new GroupControl();
            this.horzSplitter = new PanelControl();
            this.pcActions = new PanelControl();
            this.sbRun = new SimpleButton();
            this.gcDescription = new GroupControl();
            this.sbDescription = new SimpleButton();
            this.pnlHint = new NotePanel8_1();
            this.pnlCaption = new ApplicationCaption8_1();
            this.gcNavigations.BeginInit();
            this.gcNavigations.SuspendLayout();
            this.navBarControl1.BeginInit();
            this.pnlFilter.SuspendLayout();
            this.tbFilter.Properties.BeginInit();
            this.barManager1.BeginInit();
            this.panelControl1.BeginInit();
            this.panelControl1.SuspendLayout();
            this.gcContainer.BeginInit();
            this.horzSplitter.BeginInit();
            this.pcActions.BeginInit();
            this.pcActions.SuspendLayout();
            this.gcDescription.BeginInit();
            this.gcDescription.SuspendLayout();
            base.SuspendLayout();
            this.gcNavigations.BorderStyle = BorderStyles.NoBorder;
            this.gcNavigations.CaptionLocation = Locations.Left;
            this.gcNavigations.Controls.Add(this.navBarControl1);
            this.gcNavigations.Controls.Add(this.pnlFilter);
            this.gcNavigations.Dock = DockStyle.Left;
            this.gcNavigations.Location = new Point(0, 0x18);
            this.gcNavigations.Name = "gcNavigations";
            this.gcNavigations.Padding = new Padding(1);
            this.gcNavigations.ShowCaption = false;
            this.gcNavigations.Size = new Size(200, 0x256);
            this.gcNavigations.TabIndex = 0;
            this.gcNavigations.Text = "Navigations:";
            this.navBarControl1.ActiveGroup = null;
            this.navBarControl1.LinkSelectionMode = LinkSelectionModeType.OneInControl;
            this.navBarControl1.Appearance.Item.Options.UseTextOptions = true;
            this.navBarControl1.Appearance.Item.TextOptions.WordWrap = WordWrap.Wrap;
            this.navBarControl1.BorderStyle = BorderStyles.NoBorder;
            this.navBarControl1.ContentButtonHint = null;
            this.navBarControl1.Dock = DockStyle.Fill;
            this.navBarControl1.Location = new Point(1, 0x20);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 0xba;
            this.navBarControl1.Size = new Size(0xc6, 0x235);
            this.navBarControl1.StoreDefaultPaintStyleName = true;
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            this.navBarControl1.SelectedLinkChanged += new NavBarSelectedLinkChangedEventHandler(this.navBarControl1_SelectedLinkChanged);
            this.pnlFilter.Controls.Add(this.lbFilter);
            this.pnlFilter.Controls.Add(this.tbFilter);
            this.pnlFilter.Dock = DockStyle.Top;
            this.pnlFilter.Location = new Point(1, 1);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new Size(0xc6, 0x1f);
            this.pnlFilter.TabIndex = 3;
            this.lbFilter.Location = new Point(3, 6);
            this.lbFilter.Name = "lbFilter";
            this.lbFilter.Size = new Size(0x1c, 13);
            this.lbFilter.TabIndex = 0;
            this.lbFilter.Text = "Filter:";
            this.tbFilter.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.tbFilter.Location = new Point(0x25, 3);
            this.tbFilter.MenuManager = this.barManager1;
            this.tbFilter.Name = "tbFilter";
            this.tbFilter.Size = new Size(0x9e, 20);
            this.tbFilter.TabIndex = 1;
            this.tbFilter.EditValueChanged += new EventHandler(this.tbFilter_EditValueChanged);
            this.barManager1.AllowCustomization = false;
            this.barManager1.AllowQuickCustomization = false;
            this.barManager1.Bars.AddRange(new Bar[] { this.mainMenu, this.statusBar });
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new BarItem[] { this.bsUser, this.bsData });
            this.barManager1.MainMenu = this.mainMenu;
            this.barManager1.MaxItemId = 2;
            this.barManager1.StatusBar = this.statusBar;
            this.mainMenu.BarName = "Main Menu";
            this.mainMenu.DockCol = 0;
            this.mainMenu.DockRow = 0;
            this.mainMenu.DockStyle = BarDockStyle.Top;
            this.mainMenu.OptionsBar.AllowQuickCustomization = false;
            this.mainMenu.OptionsBar.DisableCustomization = true;
            this.mainMenu.OptionsBar.MultiLine = true;
            this.mainMenu.OptionsBar.UseWholeRow = true;
            this.mainMenu.Text = "Main Menu";
            this.statusBar.BarName = "Status Bar";
            this.statusBar.CanDockStyle = BarCanDockStyle.Bottom;
            this.statusBar.DockCol = 0;
            this.statusBar.DockRow = 0;
            this.statusBar.DockStyle = BarDockStyle.Bottom;
            this.statusBar.LinksPersistInfo.AddRange(new LinkPersistInfo[] { new LinkPersistInfo(this.bsUser), new LinkPersistInfo(this.bsData) });
            this.statusBar.OptionsBar.AllowQuickCustomization = false;
            this.statusBar.OptionsBar.DrawDragBorder = false;
            this.statusBar.OptionsBar.UseWholeRow = true;
            this.statusBar.Text = "Status Bar";
            this.statusBar.Visible = false;
            this.bsUser.Id = 0;
            this.bsUser.Name = "bsUser";
            this.bsUser.TextAlignment = StringAlignment.Near;
            this.bsData.AutoSize = BarStaticItemSize.Spring;
            this.bsData.Id = 1;
            this.bsData.LeftIndent = 4;
            this.bsData.Name = "bsData";
            this.bsData.TextAlignment = StringAlignment.Near;
            this.bsData.Width = 0x20;
            this.panelControl1.BorderStyle = BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcContainer);
            this.panelControl1.Controls.Add(this.horzSplitter);
            this.panelControl1.Controls.Add(this.pcActions);
            this.panelControl1.Controls.Add(this.gcDescription);
            this.panelControl1.Dock = DockStyle.Fill;
            this.panelControl1.Location = new Point(200, 0x58);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new Padding(8);
            this.panelControl1.Size = new Size(750, 0x216);
            this.panelControl1.TabIndex = 1;
            this.gcContainer.BorderStyle = BorderStyles.NoBorder;
            this.gcContainer.Dock = DockStyle.Fill;
            this.gcContainer.Location = new Point(8, 0x2d);
            this.gcContainer.Name = "gcContainer";
            this.gcContainer.ShowCaption = false;
            this.gcContainer.Size = new Size(0x2de, 0x1b4);
            this.gcContainer.TabIndex = 1;
            this.gcContainer.Text = "Tutorial:";
            this.horzSplitter.BorderStyle = BorderStyles.NoBorder;
            this.horzSplitter.Dock = DockStyle.Bottom;
            this.horzSplitter.Location = new Point(8, 0x1e1);
            this.horzSplitter.Name = "horzSplitter";
            this.horzSplitter.Size = new Size(0x2de, 8);
            this.horzSplitter.TabIndex = 7;
            this.pcActions.BorderStyle = BorderStyles.NoBorder;
            this.pcActions.Controls.Add(this.sbRun);
            this.pcActions.Dock = DockStyle.Top;
            this.pcActions.Location = new Point(8, 8);
            this.pcActions.Name = "pcActions";
            this.pcActions.Size = new Size(0x2de, 0x25);
            this.pcActions.TabIndex = 9;
            this.pcActions.Visible = false;
            this.sbRun.Location = new Point(0, 0);
            this.sbRun.Name = "sbRun";
            this.sbRun.Size = new Size(0x80, 0x1b);
            this.sbRun.TabIndex = 1;
            this.sbRun.Text = "Run Active Demo";
            this.sbRun.Click += new EventHandler(this.sbRun_Click);
            this.gcDescription.Controls.Add(this.sbDescription);
            this.gcDescription.Controls.Add(this.pnlHint);
            this.gcDescription.Dock = DockStyle.Bottom;
            this.gcDescription.Location = new Point(8, 0x1e9);
            this.gcDescription.Name = "gcDescription";
            this.gcDescription.ShowCaption = false;
            this.gcDescription.Size = new Size(0x2de, 0x25);
            this.gcDescription.TabIndex = 3;
            this.sbDescription.AllowFocus = false;
            this.sbDescription.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.sbDescription.Appearance.Font = new Font("Arial", 6.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.sbDescription.Appearance.Options.UseFont = true;
            this.sbDescription.Location = new Point(0x2c8, -17);
            this.sbDescription.Name = "sbDescription";
            this.sbDescription.Size = new Size(20, 0x11);
            this.sbDescription.TabIndex = 1;
            this.sbDescription.Text = "<<";
            this.sbDescription.Click += new EventHandler(this.sbDescription_Click);
            this.pnlHint.Dock = DockStyle.Fill;
            this.pnlHint.Location = new Point(2, 2);
            this.pnlHint.MaxRows = 0x19;
            this.pnlHint.Name = "pnlHint";
            this.pnlHint.ParentAutoHeight = true;
            this.pnlHint.Size = new Size(730, 0x21);
            this.pnlHint.TabIndex = 0;
            this.pnlHint.TabStop = false;
            this.pnlCaption.Dock = DockStyle.Top;
            this.pnlCaption.Location = new Point(200, 0x18);
            this.pnlCaption.Name = "pnlCaption";
            this.pnlCaption.Size = new Size(750, 0x40);
            this.pnlCaption.TabIndex = 4;
            this.pnlCaption.TabStop = false;
            this.pnlCaption.Text = "Demo";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(950, 650);
            base.Controls.Add(this.panelControl1);
            base.Controls.Add(this.pnlCaption);
            base.Controls.Add(this.gcNavigations);
            base.Controls.Add(this.barDockControlLeft);
            base.Controls.Add(this.barDockControlRight);
            base.Controls.Add(this.barDockControlBottom);
            base.Controls.Add(this.barDockControlTop);
            base.KeyPreview = true;
            this.MinimumSize = new Size(800, 600);
            base.Name = "frmMain";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Features Demo (C# code)";
            base.Load += new EventHandler(this.OnLoad);
            base.KeyDown += new KeyEventHandler(this.frmMain_KeyDown);
            this.gcNavigations.EndInit();
            this.gcNavigations.ResumeLayout(false);
            this.navBarControl1.EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.tbFilter.Properties.EndInit();
            this.barManager1.EndInit();
            this.panelControl1.EndInit();
            this.panelControl1.ResumeLayout(false);
            this.gcContainer.EndInit();
            this.horzSplitter.EndInit();
            this.pcActions.EndInit();
            this.pcActions.ResumeLayout(false);
            this.gcDescription.EndInit();
            this.gcDescription.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void navBarControl1_SelectedLinkChanged(object sender, NavBarSelectedLinkChangedEventArgs e)
        {
            if (e.Link != null)
            {
                this.ShowModule(e.Link.Caption);
                this.prevLink = e.Link;
            }
        }

        protected virtual void OnLoad(object sender, EventArgs e)
        {
            MainFormHelper.RegisterDefaultBonusSkin();
            this.CreateMenu();
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(ProductIdAttribute), false);
                if (customAttributes.Length == 0)
                {
                    throw new Exception("Every demo application must have the ProductId assembly attribute");
                }
                ProductIdAttribute attribute = (ProductIdAttribute) customAttributes[0];
                MainFormRegisterDemoHelper.RegisterDemos(attribute.ProductId);
                this.FillNavBar();
                ModuleInfo itemByType = null;
                if (!this.startModule.Equals(string.Empty))
                {
                    itemByType = ModulesInfo.GetItemByType(this.startModule);
                    MainFormHelper.SelectNavBarItem(this.navBarControl1, this.startModule);
                }
                if (itemByType == null)
                {
                    itemByType = ModulesInfo.GetItem(0);
                    if (itemByType != null)
                    {
                        this.ShowModule(itemByType.Name);
                    }
                }
                if (this.fullWindow)
                {
                    this.HideServiceElements();
                    this.gcDescription.Visible = this.horzSplitter.Visible = ModulesInfo.Instance.CurrentModuleBase.Description != "";
                }
            }
        }

        public void ResetNavbarSelectedLink()
        {
            this.navBarControl1.SelectedLink = null;
        }

        private void sbDescription_Click(object sender, EventArgs e)
        {
            if (this.pnlHint.MaxRows == 1)
            {
                this.pnlHint.MaxRows = this.maxHintRows;
                this.sbDescription.Text = "<<";
            }
            else
            {
                this.pnlHint.MaxRows = 1;
                this.sbDescription.Text = ">>";
            }
        }

        private void sbRun_Click(object sender, EventArgs e)
        {
            TutorialControlBase tModule = ModulesInfo.Instance.CurrentModuleBase.TModule as TutorialControlBase;
            if ((tModule != null) && tModule.HasActiveDemo)
            {
                tModule.RunActiveDemo();
            }
        }

        public void SetDataInfo(string caption)
        {
            this.bsData.Caption = caption;
        }

        protected virtual void SetFormParam()
        {
        }

        public void SetUserInfo(string caption)
        {
            this.bsUser.Caption = caption;
        }

        public void ShowDemoFilter()
        {
            this.pnlFilter.Visible = !this.pnlFilter.Visible;
        }

        public void ShowModule(string name)
        {
            if (ModulesInfo.Instance.CurrentModuleBase != null)
            {
                this.prevModuleName = ModulesInfo.Instance.CurrentModuleBase.Name;
            }
            this.gcContainer.Parent.SuspendLayout();
            this.gcContainer.SuspendLayout();
            this.ShowModule(name, this.gcContainer, this.defaultLookAndFeel1, this.pnlCaption, this.pnlHint);
            this.gcDescription.Visible = this.horzSplitter.Visible = ModulesInfo.Instance.CurrentModuleBase.Description != "";
            if (ModulesInfo.Instance.CurrentModuleBase.Description != "*")
            {
                this.pnlHint.Text = ModulesInfo.Instance.CurrentModuleBase.Description;
            }
            TutorialControlBase tModule = ModulesInfo.Instance.CurrentModuleBase.TModule as TutorialControlBase;
            if (tModule != null)
            {
                this.pcActions.Visible = tModule.HasActiveDemo;
                this.sbRun.Enabled = tModule.HasActiveDemo;
                this.gcContainer.ResumeLayout();
                this.gcContainer.Parent.ResumeLayout();
                tModule.Invalidate();
                MainFormHelper.CompactCurrentProcessWorkingSet();
            }
        }

        protected virtual void ShowModule(string name, GroupControl group, DefaultLookAndFeel lookAndFeel, ApplicationCaption caption)
        {
        }

        protected virtual void ShowModule(string name, GroupControl group, DefaultLookAndFeel lookAndFeel, ApplicationCaption caption, NotePanel notePanel)
        {
            this.ShowModule(name, group, lookAndFeel, caption);
        }

        public void ShowServiceElements()
        {
            ModuleInfo currentModuleBase = ModulesInfo.Instance.CurrentModuleBase;
            this.gcDescription.Visible = this.horzSplitter.Visible = (currentModuleBase != null) && !string.IsNullOrEmpty(currentModuleBase.Description);
            this.gcNavigations.Show();
            this.pnlCaption.Show();
            this.navBarControl1.OptionsNavPane.NavPaneState = NavPaneState.Expanded;
            if (this.ShowStatusBar)
            {
                this.statusBar.Visible = true;
            }
        }

        private void tbFilter_EditValueChanged(object sender, EventArgs e)
        {
            this.filter.FilterNavBar(this.tbFilter.Text);
        }

        protected virtual bool AllowNavBarFilter
        {
            get
            {
                return true;
            }
        }

        bool ITutorialForm.AllowDemoFilter
        {
            get
            {
                return this.AllowNavBarFilter;
            }
        }

        bool ITutorialForm.IsDemoFilterVisible
        {
            get
            {
                return this.pnlFilter.Visible;
            }
        }

        bool ITutorialForm.IsFullMode
        {
            get
            {
                return !this.pnlCaption.Visible;
            }
        }

        protected BarManager Manager
        {
            get
            {
                return this.barManager1;
            }
        }

        protected virtual bool ShowStatusBar
        {
            get
            {
                return false;
            }
        }

        protected Bar StatusBar
        {
            get
            {
                return this.statusBar;
            }
        }
    }
}

