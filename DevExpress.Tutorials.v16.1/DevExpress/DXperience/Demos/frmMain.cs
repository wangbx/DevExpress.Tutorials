using DevExpress.LookAndFeel;
using DevExpress.Skins;
using DevExpress.Utils.About;
using DevExpress.Utils.Frames;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace DevExpress.DXperience.Demos
{
    public partial class frmMain : XtraForm, ITutorialForm
    {
        protected DevExpress.DXperience.Demos.LookAndFeelMenu appearanceMenu;
        private BarDockControl barDockControlBottom;
        private BarDockControl barDockControlLeft;
        private BarDockControl barDockControlRight;
        private BarDockControl barDockControlTop;
        private BarManager barManager1;
        private BarStaticItem bsData;
        private BarStaticItem bsUser;
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

