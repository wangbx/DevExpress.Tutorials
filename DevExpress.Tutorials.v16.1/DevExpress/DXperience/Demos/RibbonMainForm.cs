using DevExpress.DemoData.Model;
using DevExpress.DXperience.Demos.CodeDemo;
using DevExpress.LookAndFeel;
using DevExpress.LookAndFeel.Design;
using DevExpress.Skins;
using DevExpress.Skins.Info;
using DevExpress.Tutorials;
using DevExpress.Tutorials.Properties;
using DevExpress.Utils;
using DevExpress.Utils.About;
using DevExpress.Utils.Diagnostics;
using DevExpress.Utils.Frames;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.ColorWheel;
using DevExpress.XtraEditors.Controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DevExpress.DXperience.Demos
{
    public class RibbonMainForm : RibbonForm, ITutorialForm, IWhatsThisProvider
    {
        protected DefaultLookAndFeel defaultLookAndFeel1;

        private LabelControl lineLabel;

        private PanelControl panelControl1;

        protected ApplicationCaption8_1 pnlCaption;

        private GroupControl gcNavigations;

        private GroupControl gcContainer;

        private PanelControl horzSplitter;

        protected AccordionControl accordionControl1;

        private DescriptionLabel gcDescription;

        private IContainer components;

        protected RibbonControl ribbonControl1;

        private RibbonPage rpMain;

        private RibbonPageGroup rpgNavigation;

        private RibbonPageGroup rpgAppearance;

        private RibbonPageGroup rpgReserv1;

        private RibbonPageGroup rpgActiveDemo;

        private RibbonPageGroup rpgReserv2;

        private RibbonPageGroup rpgExport;

        private RibbonPageGroup rpgView;

        private BarSubItem bsiModules;

        private RibbonGalleryBarItem rgbiSkins;

        private PopupMenu pmAppearance;

        private BarCheckItem bciAllowFormSkin;

        private BarButtonItem bbiUp;

        private BarButtonItem bbiDown;

        private BarButtonItem bbiActiveDemo;

        private BarButtonItem bbiAbout;

        private BarCheckItem bciFullWindow;

        protected BarCheckItem bciFilter;

        private BarButtonItem bbiPrintPreview;

        private BarButtonItem bbiPrint;

        private BarButtonItem bbiExport;

        private PopupMenu pmExport;

        private BarButtonItem bbiExporttoPDF;

        private BarButtonItem bbiExporttoEPUB;

        private BarButtonItem bbiExporttoXML;

        private BarButtonItem bbiExporttoHTML;

        private BarButtonItem bbiExporttoMHT;

        private BarButtonItem bbiExporttoDOC;

        private BarButtonItem bbiExporttoDOCX;

        private BarButtonItem bbiExporttoXLS;

        private BarButtonItem bbiExporttoXLSX;

        private BarButtonItem bbiExporttoRTF;

        private BarButtonItem bbiExporttoODT;

        private BarButtonItem bbiExporttoImage;

        private BarButtonItem bbiExporttoText;

        private AccordionControlElement prevLink;

        private string startModule = string.Empty;

        private bool fullWindow;

        private string prevModuleName = "";

        private RibbonMenuManager ribbonMenuManager;

        private RibbonPageGroup rpgAbout;

        private BarCheckItem bciShowRibbonPreview;

        private PopupMenu pmPrintOptions;

        private BarSubItem bsiExporttoImageEx;

        private BarButtonItem bbiCode;

        private RibbonPageGroup rpgCode;

        private RibbonPageCategory demoCategory;

        private BarButtonItem bbiGettingStarted;

        private BarButtonItem bbiGetFreeSupport;

        private BarButtonItem bbiBuyNow;

        protected internal RibbonStatusBar ribbonStatusBar;

        private BarButtonItem barButtonItem1;

        private BarButtonItem bbiShowInVisualStudio;

        private BarButtonItem bbiOpenSolution;

        private PopupMenu pmSolution;

        private BarButtonItem bbiCSSolution;

        private BarButtonItem bbiVBSolution;

        private BarButtonItem bbiGenerateReport;

        private frmProgress progress;

        private Image DevExpressLogoImageForRibbon = ucOverviewPage.GetSVGLogoImage();

        private WhatsThisController whatsThisController;

        private ModuleWhatsThis whatsThisModule;

        private FormTutorialInfo tutorialInfo = new FormTutorialInfo();

        private ImageShaderBase currentShader = new ImageShaderDisable();

        private static string getStartedLink = "Https://go.devexpress.com/Demo_2013_GetStartedOverall.aspx";

        private string keyUpCode = string.Empty;

        protected virtual bool AllowNavBarFilter
        {
            get
            {
                return true;
            }
        }

        protected virtual bool AllowSkinProgress
        {
            get
            {
                return true;
            }
        }

        protected BarManager Manager
        {
            get
            {
                return this.ribbonControl1.Manager;
            }
        }

        private TutorialControlBase CurrentModule
        {
            get
            {
                return ModulesInfo.Instance.CurrentModuleBase.TModule as TutorialControlBase;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RibbonControl RibbonControl
        {
            get
            {
                return this.ribbonControl1;
            }
        }

        protected virtual SourceFileType FileType
        {
            get
            {
                return SourceFileType.CS;
            }
        }

        protected virtual string DemoName
        {
            get
            {
                return "Features Demo (C# code)";
            }
        }

        private frmProgress Progress
        {
            get
            {
                if (this.progress == null)
                {
                    this.progress = new frmProgress(this);
                }
                return this.progress;
            }
        }

        public RibbonMenuManager RibbonMenuManager
        {
            get
            {
                return this.ribbonMenuManager;
            }
        }

        protected virtual int CustomWidth
        {
            get
            {
                return 1270;
            }
        }

        protected virtual int CustomHeight
        {
            get
            {
                return 855;
            }
        }

        protected virtual string NotTranslatedModuleTypes
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual bool ShowPanelDescription
        {
            get
            {
                return ModulesInfo.Instance.CurrentModuleBase != null && ModulesInfo.Instance.CurrentModuleBase.Description != "";
            }
        }

        protected virtual bool AllowDescriptionText
        {
            get
            {
                return ModulesInfo.Instance.CurrentModuleBase != null && ModulesInfo.Instance.CurrentModuleBase.Description != "*";
            }
        }

        private bool IsCurrentAbout
        {
            get
            {
                return ModulesInfo.Instance.CurrentModuleBase != null && this.IsAllowAboutModule && ModulesInfo.Instance.CurrentModuleBase.Name == ModulesInfo.GetItem(0).Name;
            }
        }

        protected virtual bool IsAllowAboutModule
        {
            get
            {
                return true;
            }
        }

        protected virtual int DefaultModuleIndex
        {
            get
            {
                return 1;
            }
        }

        bool ITutorialForm.IsFullMode
        {
            get
            {
                return !this.gcNavigations.Visible;
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
                return false;
            }
        }

        public RibbonPage MainPage
        {
            get
            {
                return this.rpMain;
            }
        }

        public RibbonPageGroup PrintExportGroup
        {
            get
            {
                return this.rpgExport;
            }
        }

        public RibbonPageGroup ReservGroup1
        {
            get
            {
                return this.rpgReserv1;
            }
        }

        public RibbonPageGroup ReservGroup2
        {
            get
            {
                return this.rpgReserv2;
            }
        }

        internal BarButtonItem GenerateReportButton
        {
            get
            {
                return this.bbiGenerateReport;
            }
        }

        internal BarButtonItem PrintPreviewButton
        {
            get
            {
                return this.bbiPrintPreview;
            }
        }

        internal BarButtonItem PrintButton
        {
            get
            {
                return this.bbiPrint;
            }
        }

        internal BarButtonItem ExportButton
        {
            get
            {
                return this.bbiExport;
            }
        }

        internal BarButtonItem ExportToPDFButton
        {
            get
            {
                return this.bbiExporttoPDF;
            }
        }

        internal BarButtonItem ExportToEPUBButton
        {
            get
            {
                return this.bbiExporttoEPUB;
            }
        }

        internal BarButtonItem ExportToXMLButton
        {
            get
            {
                return this.bbiExporttoXML;
            }
        }

        internal BarButtonItem ExportToHTMLButton
        {
            get
            {
                return this.bbiExporttoHTML;
            }
        }

        internal BarButtonItem ExportToMHTButton
        {
            get
            {
                return this.bbiExporttoMHT;
            }
        }

        internal BarButtonItem ExportToDOCButton
        {
            get
            {
                return this.bbiExporttoDOC;
            }
        }

        internal BarButtonItem ExportToDOCXButton
        {
            get
            {
                return this.bbiExporttoDOCX;
            }
        }

        internal BarButtonItem ExportToXLSButton
        {
            get
            {
                return this.bbiExporttoXLS;
            }
        }

        internal BarButtonItem ExportToXLSXButton
        {
            get
            {
                return this.bbiExporttoXLSX;
            }
        }

        internal BarButtonItem ExportToRTFButton
        {
            get
            {
                return this.bbiExporttoRTF;
            }
        }

        internal BarButtonItem ExportToODTButton
        {
            get
            {
                return this.bbiExporttoODT;
            }
        }

        internal BarButtonItem ExportToImageButton
        {
            get
            {
                return this.bbiExporttoImage;
            }
        }

        protected internal BarSubItem ExportToImageExButton
        {
            get
            {
                return this.bsiExporttoImageEx;
            }
        }

        internal BarButtonItem ExportToTextButton
        {
            get
            {
                return this.bbiExporttoText;
            }
        }

        internal BarButtonItem ShowInVisualStudio
        {
            get
            {
                return this.bbiShowInVisualStudio;
            }
        }

        protected bool IsRibbonRTL
        {
            get
            {
                return this.Ribbon.RightToLeft == RightToLeft.Yes;
            }
        }

        private bool WhatsThisEnabled
        {
            get
            {
                return !this.bbiCode.Down;
            }
        }

        public ImageShaderBase CurrentShader
        {
            get
            {
                return this.currentShader;
            }
        }

        public FormTutorialInfo TutorialInfo
        {
            get
            {
                return this.tutorialInfo;
            }
        }

        UserControl IWhatsThisProvider.CurrentModule
        {
            get
            {
                return this.CurrentModule;
            }
        }

        [DefaultValue(true)]
        public bool HintVisible
        {
            get
            {
                return true;
            }
            set
            {
            }
        }

        protected virtual bool AllowMergeStatusBar
        {
            get
            {
                return false;
            }
        }

        protected internal static string GetStartedLink
        {
            get
            {
                return RibbonMainForm.getStartedLink;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                RibbonMainForm.getStartedLink = value;
            }
        }

        private Color NewGroupColor
        {
            get
            {
                return Color.FromArgb(64, 78, 201, 85);
            }
        }

        private Color HighlightedGroupColor
        {
            get
            {
                return Color.FromArgb(64, 255, 97, 97);
            }
        }

        private Color NewItemColor
        {
            get
            {
                return Color.FromArgb(127, 255, 193, 24);
            }
        }

        private Color UpdatedItemColor
        {
            get
            {
                return Color.FromArgb(127, 127, 221, 134);
            }
        }

        private static string HtmlQuestionColor
        {
            get
            {
                Color questionColor = CommonColors.GetQuestionColor(UserLookAndFeel.Default);
                return RibbonMainForm.GetRGBColor(questionColor);
            }
        }

        private static string HtmlInformationColor
        {
            get
            {
                Color informationColor = CommonColors.GetInformationColor(UserLookAndFeel.Default);
                return RibbonMainForm.GetRGBColor(informationColor);
            }
        }

        private static string HtmlCriticalColor
        {
            get
            {
                Color criticalColor = CommonColors.GetCriticalColor(UserLookAndFeel.Default);
                return RibbonMainForm.GetRGBColor(criticalColor);
            }
        }

        protected new virtual string ProductName
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual void SetFormParam()
        {
        }

        protected virtual void FillNavBar()
        {
            ModulesInfo.FillAccordionControl(this.accordionControl1);
        }

        protected virtual void ShowModule(string name, GroupControl group, DefaultLookAndFeel lookAndFeel, ApplicationCaption caption)
        {
        }

        protected virtual void ShowModule(string name, GroupControl group, DefaultLookAndFeel lookAndFeel, ApplicationCaption caption, Control notePanel)
        {
            this.ShowModule(name, group, lookAndFeel, caption);
        }

        public RibbonMainForm() : this(new string[0])
        {
        }

        public RibbonMainForm(string[] arguments)
        {
            this.InitializeComponent();
            this.InitFileTypeInfo();
            SkinManager.EnableFormSkins();
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            this.SetFormParam();
            this.bciFilter.Visibility = BarItemVisibility.Never;
            UserLookAndFeel.Default.StyleChanged += new EventHandler(this.Default_StyleChanged);
            ((UserLookAndFeelDefault)UserLookAndFeel.Default).StyleChangeProgress += new LookAndFeelProgressEventHandler(this.Default_StyleChangeProgress);
            this.RegisterEnumTitles();
            this.SetFilterSize();
            this.whatsThisController = new WhatsThisController(this);
            this.InitWhatsThisModule();
            UAlgo.Default.DoEventObject(1, 1, this);
            this.gcDescription.Appearance.Font = new Font(AppearanceObject.DefaultFont.FontFamily, AppearanceObject.DefaultFont.Size + 1f);
            this.gcDescription.AppearanceHovered.Font = new Font(AppearanceObject.DefaultFont.FontFamily, AppearanceObject.DefaultFont.Size + 1f);
            this.gcDescription.AppearanceDisabled.Font = new Font(AppearanceObject.DefaultFont.FontFamily, AppearanceObject.DefaultFont.Size + 1f);
            this.gcDescription.AppearancePressed.Font = new Font(AppearanceObject.DefaultFont.FontFamily, AppearanceObject.DefaultFont.Size + 1f);
            this.InitOpenSolution(this.bbiCSSolution, false);
            MainFormHelper.SetCommandLineArgs(Environment.GetCommandLineArgs(), out this.startModule, out this.fullWindow);
        }

        private void InitRTLItem()
        {
            BarCheckItem barCheckItem = new BarCheckItem();
            this.ribbonControl1.Items.Add(barCheckItem);
            barCheckItem.Caption = "RTL";
            barCheckItem.CheckBoxVisibility = CheckBoxVisibility.BeforeText;
            barCheckItem.CheckedChanged += delegate (object s, ItemClickEventArgs e)
            {
                BarCheckItem barCheckItem2 = s as BarCheckItem;
                this.RightToLeftLayout = barCheckItem2.Checked;
                WindowsFormsSettings.RightToLeft = (barCheckItem2.Checked ? DefaultBoolean.True : DefaultBoolean.False);
            };
            this.rpgView.ItemLinks.Add(barCheckItem);
        }

        private void InitSkinTracingItem()
        {
            BarButtonItem barButtonItem = new BarButtonItem();
            barButtonItem.Caption = "SkinCounters";
            this.ribbonControl1.Items.Add(barButtonItem);
            barButtonItem.ItemClick += delegate (object s, ItemClickEventArgs e)
            {
                SkinCounterDiagnostic.Show();
            };
            this.rpgView.ItemLinks.Add(barButtonItem);
        }

        private void InitGUIResourcesTracingItem()
        {
            BarButtonItem barButtonItem = new BarButtonItem();
            barButtonItem.Caption = "GUI Resources";
            this.ribbonControl1.Items.Add(barButtonItem);
            barButtonItem.ItemClick += delegate (object s, ItemClickEventArgs e)
            {
                GUIResourcesDiagnostic.Show();
            };
            this.rpgView.ItemLinks.Add(barButtonItem);
        }

        private void InitFileTypeInfo()
        {
            this.TutorialInfo.SourceFileType = this.FileType;
            this.TutorialInfo.SourceFileComment = ((this.FileType == SourceFileType.CS) ? "//" : "'");
            MainFormHelper.SetBarButtonImage(this.bbiCode, (this.FileType == SourceFileType.CS) ? "C#" : "VB");
        }

        private void SetFilterSize()
        {
        }

        protected virtual void RegisterEnumTitles()
        {
        }

        private void Default_StyleChangeProgress(object sender, LookAndFeelProgressEventArgs e)
        {
            if (!this.AllowSkinProgress)
            {
                return;
            }
            if (e.State == 0)
            {
                this.Progress.ShowProgress(e.Progress);
                base.SuspendLayout();
            }
            if (e.State == 1)
            {
                this.Progress.Progress(e.Progress);
            }
            if (e.State == 2)
            {
                this.Progress.HideProgress();
                base.ResumeLayout();
            }
        }

        private void Default_StyleChanged(object sender, EventArgs e)
        {
            this.DevExpressLogoImageForRibbon = ucOverviewPage.GetSVGLogoImage();
            if (this.accordionControl1.SelectedElement != null)
            {
                this.accordionControl1.SelectedElement.Visible = true;
                if (!this.accordionControl1.SelectedElement.OwnerElement.Expanded)
                {
                    this.accordionControl1.SelectedElement.OwnerElement.Expanded = true;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.components != null)
                {
                    this.components.Dispose();
                }
                UserLookAndFeel.Default.StyleChanged -= new EventHandler(this.Default_StyleChanged);
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(RibbonMainForm));
            this.gcNavigations = new GroupControl();
            this.accordionControl1 = new AccordionControl();
            this.lineLabel = new LabelControl();
            this.defaultLookAndFeel1 = new DefaultLookAndFeel(this.components);
            this.panelControl1 = new PanelControl();
            this.gcContainer = new GroupControl();
            this.horzSplitter = new PanelControl();
            this.gcDescription = new DescriptionLabel();
            this.pnlCaption = new ApplicationCaption8_1();
            this.ribbonControl1 = new RibbonControl();
            this.bsiModules = new BarSubItem();
            this.rgbiSkins = new RibbonGalleryBarItem();
            this.bciAllowFormSkin = new BarCheckItem();
            this.bbiUp = new BarButtonItem();
            this.bbiDown = new BarButtonItem();
            this.bbiActiveDemo = new BarButtonItem();
            this.bbiAbout = new BarButtonItem();
            this.bciFullWindow = new BarCheckItem();
            this.bciFilter = new BarCheckItem();
            this.bbiPrintPreview = new BarButtonItem();
            this.bbiPrint = new BarButtonItem();
            this.bbiExport = new BarButtonItem();
            this.pmExport = new PopupMenu(this.components);
            this.bbiExporttoPDF = new BarButtonItem();
            this.bbiExporttoEPUB = new BarButtonItem();
            this.bbiExporttoXML = new BarButtonItem();
            this.bbiExporttoHTML = new BarButtonItem();
            this.bbiExporttoMHT = new BarButtonItem();
            this.bbiExporttoDOC = new BarButtonItem();
            this.bbiExporttoDOCX = new BarButtonItem();
            this.bbiExporttoXLS = new BarButtonItem();
            this.bbiExporttoXLSX = new BarButtonItem();
            this.bbiExporttoRTF = new BarButtonItem();
            this.bbiExporttoODT = new BarButtonItem();
            this.bbiExporttoImage = new BarButtonItem();
            this.bsiExporttoImageEx = new BarSubItem();
            this.bbiExporttoText = new BarButtonItem();
            this.bciShowRibbonPreview = new BarCheckItem();
            this.bbiCode = new BarButtonItem();
            this.bbiGettingStarted = new BarButtonItem();
            this.bbiGetFreeSupport = new BarButtonItem();
            this.bbiBuyNow = new BarButtonItem();
            this.barButtonItem1 = new BarButtonItem();
            this.bbiShowInVisualStudio = new BarButtonItem();
            this.bbiOpenSolution = new BarButtonItem();
            this.pmSolution = new PopupMenu(this.components);
            this.bbiCSSolution = new BarButtonItem();
            this.bbiVBSolution = new BarButtonItem();
            this.bbiGenerateReport = new BarButtonItem();
            this.demoCategory = new RibbonPageCategory();
            this.rpMain = new RibbonPage();
            this.rpgNavigation = new RibbonPageGroup();
            this.rpgAppearance = new RibbonPageGroup();
            this.rpgReserv1 = new RibbonPageGroup();
            this.rpgReserv2 = new RibbonPageGroup();
            this.rpgExport = new RibbonPageGroup();
            this.rpgView = new RibbonPageGroup();
            this.rpgCode = new RibbonPageGroup();
            this.rpgActiveDemo = new RibbonPageGroup();
            this.rpgAbout = new RibbonPageGroup();
            this.ribbonStatusBar = new RibbonStatusBar();
            this.pmAppearance = new PopupMenu(this.components);
            this.pmPrintOptions = new PopupMenu(this.components);
            ((ISupportInitialize)this.gcNavigations).BeginInit();
            this.gcNavigations.SuspendLayout();
            ((ISupportInitialize)this.accordionControl1).BeginInit();
            ((ISupportInitialize)this.panelControl1).BeginInit();
            this.panelControl1.SuspendLayout();
            ((ISupportInitialize)this.gcContainer).BeginInit();
            ((ISupportInitialize)this.horzSplitter).BeginInit();
            ((ISupportInitialize)this.ribbonControl1).BeginInit();
            ((ISupportInitialize)this.pmExport).BeginInit();
            ((ISupportInitialize)this.pmSolution).BeginInit();
            ((ISupportInitialize)this.pmAppearance).BeginInit();
            ((ISupportInitialize)this.pmPrintOptions).BeginInit();
            base.SuspendLayout();
            this.gcNavigations.BorderStyle = BorderStyles.NoBorder;
            this.gcNavigations.CaptionLocation = Locations.Left;
            this.gcNavigations.Controls.Add(this.accordionControl1);
            this.gcNavigations.Controls.Add(this.lineLabel);
            componentResourceManager.ApplyResources(this.gcNavigations, "gcNavigations");
            this.gcNavigations.Name = "gcNavigations";
            this.gcNavigations.ShowCaption = false;
            this.accordionControl1.AllowItemSelection = true;
            componentResourceManager.ApplyResources(this.accordionControl1, "accordionControl1");
            this.accordionControl1.ExpandElementMode = ExpandElementMode.Multiple;
            this.accordionControl1.Name = "accordionControl1";
            this.accordionControl1.ScrollBarMode = ScrollBarMode.Touch;
            this.accordionControl1.ShowFilterControl = ShowFilterControl.Always;
            this.accordionControl1.ShowGroupExpandButtons = false;
            this.accordionControl1.ElementClick += new ElementClickEventHandler(this.accordionControl1_LinkClicked);
            this.accordionControl1.SelectedElementChanged += new SelectedElementChangedEventHandler(this.accordionControl1_SelectedLinkChanged);
            this.accordionControl1.CustomDrawElement += new CustomDrawElementEventHandler(this.accordionControl1_CustomDrawElement);
            this.accordionControl1.CustomElementText += new CustomElementTextEventHandler(this.accordionControl1_CustomElementText);
            componentResourceManager.ApplyResources(this.lineLabel, "lineLabel");
            this.lineLabel.LineLocation = LineLocation.Center;
            this.lineLabel.LineOrientation = LabelLineOrientation.Vertical;
            this.lineLabel.LineVisible = true;
            this.lineLabel.Name = "lineLabel";
            this.panelControl1.BorderStyle = BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.gcContainer);
            this.panelControl1.Controls.Add(this.horzSplitter);
            this.panelControl1.Controls.Add(this.gcDescription);
            componentResourceManager.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            this.gcContainer.BorderStyle = BorderStyles.NoBorder;
            componentResourceManager.ApplyResources(this.gcContainer, "gcContainer");
            this.gcContainer.Name = "gcContainer";
            this.gcContainer.ShowCaption = false;
            this.horzSplitter.BorderStyle = BorderStyles.NoBorder;
            componentResourceManager.ApplyResources(this.horzSplitter, "horzSplitter");
            this.horzSplitter.Name = "horzSplitter";
            this.gcDescription.Appearance.Image = (Image)componentResourceManager.GetObject("gcDescription.Appearance.Image");
            this.gcDescription.Appearance.ImageAlign = ContentAlignment.TopLeft;
            this.gcDescription.AppearanceDisabled.Image = (Image)componentResourceManager.GetObject("gcDescription.AppearanceDisabled.Image");
            this.gcDescription.AppearanceDisabled.ImageAlign = ContentAlignment.TopLeft;
            this.gcDescription.AppearanceHovered.Image = (Image)componentResourceManager.GetObject("gcDescription.AppearanceHovered.Image");
            this.gcDescription.AppearanceHovered.ImageAlign = ContentAlignment.TopLeft;
            this.gcDescription.AppearancePressed.Image = (Image)componentResourceManager.GetObject("gcDescription.AppearancePressed.Image");
            this.gcDescription.AppearancePressed.ImageAlign = ContentAlignment.TopLeft;
            componentResourceManager.ApplyResources(this.gcDescription, "gcDescription");
            this.gcDescription.ImageAlignToText = ImageAlignToText.LeftCenter;
            this.gcDescription.Name = "gcDescription";
            this.gcDescription.UseMnemonic = false;
            componentResourceManager.ApplyResources(this.pnlCaption, "pnlCaption");
            this.pnlCaption.Name = "pnlCaption";
            this.pnlCaption.TabStop = false;
            this.ribbonControl1.AllowMinimizeRibbon = false;
            componentResourceManager.ApplyResources(this.ribbonControl1, "ribbonControl1");
            this.ribbonControl1.AutoSizeItems = true;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new BarItem[]
            {
                this.ribbonControl1.ExpandCollapseItem,
                this.bsiModules,
                this.rgbiSkins,
                this.bciAllowFormSkin,
                this.bbiUp,
                this.bbiDown,
                this.bbiActiveDemo,
                this.bbiAbout,
                this.bciFullWindow,
                this.bciFilter,
                this.bbiPrintPreview,
                this.bbiPrint,
                this.bbiExport,
                this.bbiExporttoPDF,
                this.bbiExporttoEPUB,
                this.bbiExporttoXML,
                this.bbiExporttoHTML,
                this.bbiExporttoMHT,
                this.bbiExporttoDOC,
                this.bbiExporttoDOCX,
                this.bbiExporttoXLS,
                this.bbiExporttoXLSX,
                this.bbiExporttoRTF,
                this.bbiExporttoODT,
                this.bbiExporttoImage,
                this.bbiExporttoText,
                this.bciShowRibbonPreview,
                this.bsiExporttoImageEx,
                this.bbiCode,
                this.bbiGettingStarted,
                this.bbiGetFreeSupport,
                this.bbiBuyNow,
                this.barButtonItem1,
                this.bbiShowInVisualStudio,
                this.bbiOpenSolution,
                this.bbiCSSolution,
                this.bbiVBSolution,
                this.bbiGenerateReport
            });
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.PageCategories.AddRange(new RibbonPageCategory[]
            {
                this.demoCategory
            });
            this.ribbonControl1.PageCategoryAlignment = RibbonPageCategoryAlignment.Left;
            this.ribbonControl1.Pages.AddRange(new RibbonPage[]
            {
                this.rpMain
            });
            this.ribbonControl1.RibbonStyle = RibbonControlStyle.Office2010;
            this.ribbonControl1.ShowApplicationButton = DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = ShowPageHeadersMode.ShowOnMultiplePages;
            this.ribbonControl1.StatusBar = this.ribbonStatusBar;
            this.ribbonControl1.ToolbarLocation = RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.Merge += new RibbonMergeEventHandler(this.ribbonControl1_Merge);
            this.ribbonControl1.UnMerge += new RibbonMergeEventHandler(this.ribbonControl1_UnMerge);
            this.ribbonControl1.Paint += new PaintEventHandler(this.ribbonControl1_Paint);
            componentResourceManager.ApplyResources(this.bsiModules, "bsiModules");
            this.bsiModules.Id = 1;
            this.bsiModules.Name = "bsiModules";
            this.bsiModules.ShowNavigationHeader = DefaultBoolean.True;
            componentResourceManager.ApplyResources(this.rgbiSkins, "rgbiSkins");
            this.rgbiSkins.Id = 2;
            this.rgbiSkins.Name = "rgbiSkins";
            componentResourceManager.ApplyResources(this.bciAllowFormSkin, "bciAllowFormSkin");
            this.bciAllowFormSkin.Id = 4;
            this.bciAllowFormSkin.Name = "bciAllowFormSkin";
            this.bciAllowFormSkin.ItemClick += new ItemClickEventHandler(this.bciAllowFormSkin_ItemClick);
            componentResourceManager.ApplyResources(this.bbiUp, "bbiUp");
            this.bbiUp.Id = 5;
            this.bbiUp.Name = "bbiUp";
            this.bbiUp.RibbonStyle = RibbonItemStyles.SmallWithText;
            this.bbiUp.ItemClick += new ItemClickEventHandler(this.bbiUp_ItemClick);
            componentResourceManager.ApplyResources(this.bbiDown, "bbiDown");
            this.bbiDown.Id = 6;
            this.bbiDown.Name = "bbiDown";
            this.bbiDown.RibbonStyle = RibbonItemStyles.SmallWithText;
            this.bbiDown.ItemClick += new ItemClickEventHandler(this.bbiDown_ItemClick);
            componentResourceManager.ApplyResources(this.bbiActiveDemo, "bbiActiveDemo");
            this.bbiActiveDemo.Id = 7;
            this.bbiActiveDemo.Name = "bbiActiveDemo";
            this.bbiActiveDemo.ItemClick += new ItemClickEventHandler(this.bbiActiveDemo_ItemClick);
            componentResourceManager.ApplyResources(this.bbiAbout, "bbiAbout");
            this.bbiAbout.Id = 8;
            this.bbiAbout.ItemShortcut = new BarShortcut(Keys.F1);
            this.bbiAbout.Name = "bbiAbout";
            this.bbiAbout.ItemClick += new ItemClickEventHandler(this.bbiAbout_ItemClick);
            componentResourceManager.ApplyResources(this.bciFullWindow, "bciFullWindow");
            this.bciFullWindow.Id = 11;
            this.bciFullWindow.ItemShortcut = new BarShortcut(Keys.F11);
            this.bciFullWindow.Name = "bciFullWindow";
            this.bciFullWindow.CheckedChanged += new ItemClickEventHandler(this.bciFullWindow_CheckedChanged);
            componentResourceManager.ApplyResources(this.bciFilter, "bciFilter");
            this.bciFilter.Id = 12;
            this.bciFilter.ItemShortcut = new BarShortcut(Keys.F3);
            this.bciFilter.Name = "bciFilter";
            this.bciFilter.CheckedChanged += new ItemClickEventHandler(this.bciFilter_CheckedChanged);
            componentResourceManager.ApplyResources(this.bbiPrintPreview, "bbiPrintPreview");
            this.bbiPrintPreview.Id = 13;
            this.bbiPrintPreview.Name = "bbiPrintPreview";
            componentResourceManager.ApplyResources(this.bbiPrint, "bbiPrint");
            this.bbiPrint.Id = 25;
            this.bbiPrint.Name = "bbiPrint";
            this.bbiExport.ActAsDropDown = true;
            this.bbiExport.ButtonStyle = BarButtonStyle.DropDown;
            componentResourceManager.ApplyResources(this.bbiExport, "bbiExport");
            this.bbiExport.DropDownControl = this.pmExport;
            this.bbiExport.Id = 14;
            this.bbiExport.Name = "bbiExport";
            this.pmExport.ItemLinks.Add(this.bbiExporttoPDF);
            this.pmExport.ItemLinks.Add(this.bbiExporttoEPUB);
            this.pmExport.ItemLinks.Add(this.bbiExporttoXML);
            this.pmExport.ItemLinks.Add(this.bbiExporttoHTML);
            this.pmExport.ItemLinks.Add(this.bbiExporttoMHT);
            this.pmExport.ItemLinks.Add(this.bbiExporttoDOC);
            this.pmExport.ItemLinks.Add(this.bbiExporttoDOCX);
            this.pmExport.ItemLinks.Add(this.bbiExporttoXLS);
            this.pmExport.ItemLinks.Add(this.bbiExporttoXLSX);
            this.pmExport.ItemLinks.Add(this.bbiExporttoRTF);
            this.pmExport.ItemLinks.Add(this.bbiExporttoODT);
            this.pmExport.ItemLinks.Add(this.bbiExporttoImage);
            this.pmExport.ItemLinks.Add(this.bsiExporttoImageEx);
            this.pmExport.ItemLinks.Add(this.bbiExporttoText);
            this.pmExport.MenuDrawMode = MenuDrawMode.SmallImagesText;
            this.pmExport.Name = "pmExport";
            this.pmExport.Ribbon = this.ribbonControl1;
            this.pmExport.ShowNavigationHeader = DefaultBoolean.True;
            componentResourceManager.ApplyResources(this.bbiExporttoPDF, "bbiExporttoPDF");
            this.bbiExporttoPDF.Id = 15;
            this.bbiExporttoPDF.Name = "bbiExporttoPDF";
            componentResourceManager.ApplyResources(this.bbiExporttoEPUB, "bbiExporttoEPUB");
            this.bbiExporttoEPUB.Id = 26;
            this.bbiExporttoEPUB.Name = "bbiExporttoEPUB";
            componentResourceManager.ApplyResources(this.bbiExporttoXML, "bbiExporttoXML");
            this.bbiExporttoXML.Id = 16;
            this.bbiExporttoXML.Name = "bbiExporttoXML";
            componentResourceManager.ApplyResources(this.bbiExporttoHTML, "bbiExporttoHTML");
            this.bbiExporttoHTML.Id = 17;
            this.bbiExporttoHTML.Name = "bbiExporttoHTML";
            componentResourceManager.ApplyResources(this.bbiExporttoMHT, "bbiExporttoMHT");
            this.bbiExporttoMHT.Id = 18;
            this.bbiExporttoMHT.Name = "bbiExporttoMHT";
            componentResourceManager.ApplyResources(this.bbiExporttoDOC, "bbiExporttoDOC");
            this.bbiExporttoDOC.Id = 27;
            this.bbiExporttoDOC.Name = "bbiExporttoDOC";
            componentResourceManager.ApplyResources(this.bbiExporttoDOCX, "bbiExporttoDOCX");
            this.bbiExporttoDOCX.Id = 28;
            this.bbiExporttoDOCX.Name = "bbiExporttoDOCX";
            componentResourceManager.ApplyResources(this.bbiExporttoXLS, "bbiExporttoXLS");
            this.bbiExporttoXLS.Id = 19;
            this.bbiExporttoXLS.Name = "bbiExporttoXLS";
            componentResourceManager.ApplyResources(this.bbiExporttoXLSX, "bbiExporttoXLSX");
            this.bbiExporttoXLSX.Id = 20;
            this.bbiExporttoXLSX.Name = "bbiExporttoXLSX";
            componentResourceManager.ApplyResources(this.bbiExporttoRTF, "bbiExporttoRTF");
            this.bbiExporttoRTF.Id = 21;
            this.bbiExporttoRTF.Name = "bbiExporttoRTF";
            componentResourceManager.ApplyResources(this.bbiExporttoODT, "bbiExporttoODT");
            this.bbiExporttoODT.Id = 29;
            this.bbiExporttoODT.Name = "bbiExporttoODT";
            componentResourceManager.ApplyResources(this.bbiExporttoImage, "bbiExporttoImage");
            this.bbiExporttoImage.Id = 22;
            this.bbiExporttoImage.Name = "bbiExporttoImage";
            componentResourceManager.ApplyResources(this.bsiExporttoImageEx, "bsiExporttoImageEx");
            this.bsiExporttoImageEx.Id = 30;
            this.bsiExporttoImageEx.Name = "bsiExporttoImageEx";
            this.bsiExporttoImageEx.ShowNavigationHeader = DefaultBoolean.True;
            componentResourceManager.ApplyResources(this.bbiExporttoText, "bbiExporttoText");
            this.bbiExporttoText.Id = 23;
            this.bbiExporttoText.Name = "bbiExporttoText";
            componentResourceManager.ApplyResources(this.bciShowRibbonPreview, "bciShowRibbonPreview");
            this.bciShowRibbonPreview.Id = 24;
            this.bciShowRibbonPreview.Name = "bciShowRibbonPreview";
            this.bciShowRibbonPreview.ItemClick += new ItemClickEventHandler(this.bciShowRibbonPreview_ItemClick);
            this.bbiCode.ButtonStyle = BarButtonStyle.Check;
            componentResourceManager.ApplyResources(this.bbiCode, "bbiCode");
            this.bbiCode.Id = 32;
            this.bbiCode.Name = "bbiCode";
            this.bbiCode.DownChanged += new ItemClickEventHandler(this.bbiCode_DownChanged);
            componentResourceManager.ApplyResources(this.bbiGettingStarted, "bbiGettingStarted");
            this.bbiGettingStarted.CategoryGuid = new Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.bbiGettingStarted.Id = 33;
            this.bbiGettingStarted.Name = "bbiGettingStarted";
            this.bbiGettingStarted.ItemClick += new ItemClickEventHandler(this.bbiGettingStarted_ItemClick);
            componentResourceManager.ApplyResources(this.bbiGetFreeSupport, "bbiGetFreeSupport");
            this.bbiGetFreeSupport.CategoryGuid = new Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.bbiGetFreeSupport.Id = 34;
            this.bbiGetFreeSupport.Name = "bbiGetFreeSupport";
            this.bbiGetFreeSupport.ItemClick += new ItemClickEventHandler(this.bbiGetFreeSupport_ItemClick);
            componentResourceManager.ApplyResources(this.bbiBuyNow, "bbiBuyNow");
            this.bbiBuyNow.CategoryGuid = new Guid("6ffddb2b-9015-4d97-a4c1-91613e0ef537");
            this.bbiBuyNow.Id = 35;
            this.bbiBuyNow.Name = "bbiBuyNow";
            this.bbiBuyNow.ItemClick += new ItemClickEventHandler(this.bbiBuyNow_ItemClick);
            componentResourceManager.ApplyResources(this.barButtonItem1, "barButtonItem1");
            this.barButtonItem1.Glyph = Resources.ColorMixer_16x16;
            this.barButtonItem1.Id = 38;
            this.barButtonItem1.LargeGlyph = Resources.ColorMixer_32x32;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.RibbonStyle = RibbonItemStyles.Large;
            this.barButtonItem1.ItemClick += new ItemClickEventHandler(this.barButtonItem1_ItemClick);
            componentResourceManager.ApplyResources(this.bbiShowInVisualStudio, "bbiShowInVisualStudio");
            this.bbiShowInVisualStudio.Id = 39;
            this.bbiShowInVisualStudio.Name = "bbiShowInVisualStudio";
            this.bbiShowInVisualStudio.RibbonStyle = RibbonItemStyles.Large;
            this.bbiShowInVisualStudio.Visibility = BarItemVisibility.Never;
            this.bbiOpenSolution.ButtonStyle = BarButtonStyle.DropDown;
            componentResourceManager.ApplyResources(this.bbiOpenSolution, "bbiOpenSolution");
            this.bbiOpenSolution.DropDownControl = this.pmSolution;
            this.bbiOpenSolution.Id = 40;
            this.bbiOpenSolution.Name = "bbiOpenSolution";
            this.bbiOpenSolution.ItemClick += new ItemClickEventHandler(this.bbiOpenSolution_ItemClick);
            this.pmSolution.ItemLinks.Add(this.bbiCSSolution);
            this.pmSolution.ItemLinks.Add(this.bbiVBSolution);
            this.pmSolution.Name = "pmSolution";
            this.pmSolution.Ribbon = this.ribbonControl1;
            componentResourceManager.ApplyResources(this.bbiCSSolution, "bbiCSSolution");
            this.bbiCSSolution.Glyph = (Image)componentResourceManager.GetObject("bbiCSSolution.Glyph");
            this.bbiCSSolution.Id = 41;
            this.bbiCSSolution.LargeGlyph = (Image)componentResourceManager.GetObject("bbiCSSolution.LargeGlyph");
            this.bbiCSSolution.Name = "bbiCSSolution";
            this.bbiCSSolution.ItemClick += new ItemClickEventHandler(this.bbiCSSolution_ItemClick);
            componentResourceManager.ApplyResources(this.bbiVBSolution, "bbiVBSolution");
            this.bbiVBSolution.Glyph = (Image)componentResourceManager.GetObject("bbiVBSolution.Glyph");
            this.bbiVBSolution.Id = 42;
            this.bbiVBSolution.LargeGlyph = (Image)componentResourceManager.GetObject("bbiVBSolution.LargeGlyph");
            this.bbiVBSolution.Name = "bbiVBSolution";
            this.bbiVBSolution.ItemClick += new ItemClickEventHandler(this.bbiVBSolution_ItemClick);
            componentResourceManager.ApplyResources(this.bbiGenerateReport, "bbiGenerateReport");
            this.bbiGenerateReport.Id = 1;
            this.bbiGenerateReport.Name = "bbiGenerateReport";
            this.bbiGenerateReport.Visibility = BarItemVisibility.Never;
            componentResourceManager.ApplyResources(this.demoCategory, "demoCategory");
            this.demoCategory.Name = "demoCategory";
            this.rpMain.Groups.AddRange(new RibbonPageGroup[]
            {
                this.rpgNavigation,
                this.rpgAppearance,
                this.rpgReserv1,
                this.rpgReserv2,
                this.rpgExport,
                this.rpgView,
                this.rpgCode,
                this.rpgActiveDemo,
                this.rpgAbout
            });
            this.rpMain.Name = "rpMain";
            componentResourceManager.ApplyResources(this.rpMain, "rpMain");
            this.rpgNavigation.ItemLinks.Add(this.bsiModules);
            this.rpgNavigation.ItemLinks.Add(this.bbiUp, true);
            this.rpgNavigation.ItemLinks.Add(this.bbiDown);
            this.rpgNavigation.Name = "rpgNavigation";
            this.rpgNavigation.ShowCaptionButton = false;
            componentResourceManager.ApplyResources(this.rpgNavigation, "rpgNavigation");
            this.rpgAppearance.AllowTextClipping = false;
            this.rpgAppearance.ItemLinks.Add(this.rgbiSkins);
            this.rpgAppearance.ItemLinks.Add(this.barButtonItem1);
            this.rpgAppearance.Name = "rpgAppearance";
            componentResourceManager.ApplyResources(this.rpgAppearance, "rpgAppearance");
            this.rpgAppearance.CaptionButtonClick += new RibbonPageGroupEventHandler(this.rpgAppearance_CaptionButtonClick);
            this.rpgReserv1.AllowTextClipping = false;
            this.rpgReserv1.Name = "rpgReserv1";
            this.rpgReserv1.ShowCaptionButton = false;
            this.rpgReserv1.Visible = false;
            this.rpgReserv2.AllowTextClipping = false;
            this.rpgReserv2.Name = "rpgReserv2";
            this.rpgReserv2.ShowCaptionButton = false;
            this.rpgReserv2.Visible = false;
            this.rpgExport.AllowTextClipping = false;
            this.rpgExport.ItemLinks.Add(this.bbiGenerateReport);
            this.rpgExport.ItemLinks.Add(this.bbiPrintPreview);
            this.rpgExport.ItemLinks.Add(this.bbiPrint);
            this.rpgExport.ItemLinks.Add(this.bbiExport);
            this.rpgExport.Name = "rpgExport";
            this.rpgExport.ShowCaptionButton = false;
            componentResourceManager.ApplyResources(this.rpgExport, "rpgExport");
            this.rpgExport.Visible = false;
            this.rpgExport.CaptionButtonClick += new RibbonPageGroupEventHandler(this.rpgExport_CaptionButtonClick);
            this.rpgView.ItemLinks.Add(this.bciFullWindow);
            this.rpgView.ItemLinks.Add(this.bciFilter);
            this.rpgView.Name = "rpgView";
            this.rpgView.ShowCaptionButton = false;
            componentResourceManager.ApplyResources(this.rpgView, "rpgView");
            this.rpgCode.ItemLinks.Add(this.bbiShowInVisualStudio);
            this.rpgCode.ItemLinks.Add(this.bbiCode);
            this.rpgCode.ItemLinks.Add(this.bbiOpenSolution);
            this.rpgCode.Name = "rpgCode";
            this.rpgCode.ShowCaptionButton = false;
            componentResourceManager.ApplyResources(this.rpgCode, "rpgCode");
            this.rpgActiveDemo.AllowTextClipping = false;
            this.rpgActiveDemo.ItemLinks.Add(this.bbiActiveDemo);
            this.rpgActiveDemo.Name = "rpgActiveDemo";
            this.rpgActiveDemo.ShowCaptionButton = false;
            componentResourceManager.ApplyResources(this.rpgActiveDemo, "rpgActiveDemo");
            this.rpgActiveDemo.Visible = false;
            this.rpgAbout.AllowTextClipping = false;
            this.rpgAbout.ItemLinks.Add(this.bbiGettingStarted);
            this.rpgAbout.ItemLinks.Add(this.bbiGetFreeSupport);
            this.rpgAbout.ItemLinks.Add(this.bbiBuyNow);
            this.rpgAbout.ItemLinks.Add(this.bbiAbout);
            this.rpgAbout.Name = "rpgAbout";
            this.rpgAbout.ShowCaptionButton = false;
            componentResourceManager.ApplyResources(this.rpgAbout, "rpgAbout");
            componentResourceManager.ApplyResources(this.ribbonStatusBar, "ribbonStatusBar");
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbonControl1;
            this.pmAppearance.ItemLinks.Add(this.bciAllowFormSkin);
            this.pmAppearance.MenuDrawMode = MenuDrawMode.SmallImagesText;
            this.pmAppearance.Name = "pmAppearance";
            this.pmAppearance.Ribbon = this.ribbonControl1;
            this.pmAppearance.ShowNavigationHeader = DefaultBoolean.True;
            this.pmAppearance.Popup += new EventHandler(this.pmAppearance_Popup);
            this.pmPrintOptions.ItemLinks.Add(this.bciShowRibbonPreview);
            this.pmPrintOptions.MenuDrawMode = MenuDrawMode.SmallImagesText;
            this.pmPrintOptions.Name = "pmPrintOptions";
            this.pmPrintOptions.Ribbon = this.ribbonControl1;
            this.pmPrintOptions.ShowNavigationHeader = DefaultBoolean.True;
            this.pmPrintOptions.BeforePopup += new CancelEventHandler(this.pmPrintOptions_BeforePopup);
            base.AllowFormGlass = DefaultBoolean.False;
            componentResourceManager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.panelControl1);
            base.Controls.Add(this.pnlCaption);
            base.Controls.Add(this.gcNavigations);
            base.Controls.Add(this.ribbonStatusBar);
            base.Controls.Add(this.ribbonControl1);
            base.FormBorderEffect = FormBorderEffect.Shadow;
            base.KeyPreview = true;
            base.Name = "RibbonMainForm";
            this.Ribbon = this.ribbonControl1;
            this.StatusBar = this.ribbonStatusBar;
            base.Load += new EventHandler(this.OnLoad);
            base.KeyDown += new KeyEventHandler(this.frmMain_KeyDown);
            base.Move += new EventHandler(this.RibbonMainForm_Move);
            base.Resize += new EventHandler(this.RibbonMainForm_Resize);
            ((ISupportInitialize)this.gcNavigations).EndInit();
            this.gcNavigations.ResumeLayout(false);
            ((ISupportInitialize)this.accordionControl1).EndInit();
            ((ISupportInitialize)this.panelControl1).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((ISupportInitialize)this.gcContainer).EndInit();
            ((ISupportInitialize)this.horzSplitter).EndInit();
            ((ISupportInitialize)this.ribbonControl1).EndInit();
            ((ISupportInitialize)this.pmExport).EndInit();
            ((ISupportInitialize)this.pmSolution).EndInit();
            ((ISupportInitialize)this.pmAppearance).EndInit();
            ((ISupportInitialize)this.pmPrintOptions).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        protected virtual void ShowAbout()
        {
        }

        protected virtual RibbonMenuManager CreateRibbonMenuManager()
        {
            return new RibbonMenuManager(this);
        }

        protected virtual void CreateRibbonMenu()
        {
            AccordionNavigationMenuHelper.CreateNavigationMenu(this.bsiModules, this.accordionControl1, this.Manager);
            SkinHelper.InitSkinPopupMenu(this.pmAppearance);
            SkinHelper.InitSkinGallery(this.rgbiSkins, true);
            this.rgbiSkins.GalleryInitDropDownGallery += new InplaceGalleryEventHandler(this.rgbiSkins_GalleryInitDropDownGallery);
            this.pmAppearance.ItemLinks[1].BeginGroup = true;
        }

        private void rgbiSkins_GalleryInitDropDownGallery(object sender, InplaceGalleryEventArgs e)
        {
            e.PopupGallery.GalleryDropDown.ItemLinks.Add(this.bciAllowFormSkin);
            this.bciAllowFormSkin.Checked = SkinManager.AllowFormSkins;
        }

        protected virtual void InitBarItemImages()
        {
            MainFormHelper.SetBarButtonImage(this.bsiModules, "Navigation");
            MainFormHelper.SetBarButtonImage(this.bbiUp, "Previous");
            MainFormHelper.SetBarButtonImage(this.bbiDown, "Next");
            MainFormHelper.SetBarButtonImage(this.bbiActiveDemo, "ActiveDemo");
            MainFormHelper.SetBarButtonImage(this.bbiAbout, "About");
            MainFormHelper.SetBarButtonImage(this.bbiBuyNow, "BuyNow");
            MainFormHelper.SetBarButtonImage(this.bbiGettingStarted, "GetStarted");
            MainFormHelper.SetBarButtonImage(this.bbiGetFreeSupport, "GetSupport");
            MainFormHelper.SetBarButtonImage(this.bciFullWindow, "FullWindowView");
            MainFormHelper.SetBarButtonImage(this.bciFilter, "Filter");
            MainFormHelper.SetBarButtonImage(this.bbiGenerateReport, "GenerateReport");
            MainFormHelper.SetBarButtonImage(this.bbiPrintPreview, "Preview");
            MainFormHelper.SetBarButtonImage(this.bbiPrint, "Print");
            MainFormHelper.SetBarButtonImage(this.bbiExport, "Export");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoPDF, "ExportToPDF");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoEPUB, "ExportToEPUB");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoXML, "ExportToXML");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoHTML, "ExportToHTML");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoMHT, "ExportToMHT");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoDOC, "ExportToDOC");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoDOCX, "ExportToDOCX");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoXLS, "ExportToExcel");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoXLSX, "ExportToExcel");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoRTF, "ExportToRTF");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoODT, "ExportToODT");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoImage, "ExportToIMG");
            MainFormHelper.SetBarButtonImage(this.bsiExporttoImageEx, "ExportToIMG");
            MainFormHelper.SetBarButtonImage(this.bbiExporttoText, "ExportToTXT");
            this.GenerateReportButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.GenerateReport();
            };
            this.PrintPreviewButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.PrintPreview();
            };
            this.PrintButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.Print();
            };
            this.ExportToPDFButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToPDF();
            };
            this.ExportToEPUBButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToEPUB();
            };
            this.ExportToXMLButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToXML();
            };
            this.ExportToHTMLButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToHTML();
            };
            this.ExportToMHTButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToMHT();
            };
            this.ExportToDOCButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToDOC();
            };
            this.ExportToDOCXButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToDOCX();
            };
            this.ExportToXLSButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToXLS();
            };
            this.ExportToXLSXButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToXLSX();
            };
            this.ExportToRTFButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToRTF();
            };
            this.ExportToODTButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToODT();
            };
            this.ExportToImageButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToImage();
            };
            this.ExportToTextButton.ItemClick += delegate (object param0, ItemClickEventArgs param1)
            {
                this.CurrentModule.ExportToText();
            };
        }

        protected virtual void OnLoad(object sender, EventArgs e)
        {
            MainFormHelper.SetFormClientSize(Screen.GetWorkingArea(base.Location), this, this.CustomWidth, this.CustomHeight);
            MainFormHelper.RegisterRibbonDefaultBonusSkin();
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly == null)
            {
                return;
            }
            object[] customAttributes = entryAssembly.GetCustomAttributes(typeof(ProductIdAttribute), false);
            if (customAttributes.Length == 0)
            {
                throw new Exception("Every demo application must have the ProductId assembly attribute");
            }
            ProductIdAttribute productIdAttribute = (ProductIdAttribute)customAttributes[0];
            MainFormRegisterDemoHelper.RegisterDemos(productIdAttribute.ProductId);
            this.FillNavBar();
            this.RemoveNavBarItems();
            this.InitBarItemImages();
            this.CreateRibbonMenu();
            this.ribbonMenuManager = this.CreateRibbonMenuManager();
            ModuleInfo moduleInfo = null;
            if (!this.startModule.Equals(string.Empty))
            {
                moduleInfo = ModulesInfo.GetItemByType(this.startModule);
                MainFormHelper.SelectAccordionControlItem(this.accordionControl1, this.startModule);
            }
            if (moduleInfo == null)
            {
                moduleInfo = ModulesInfo.GetItem(this.IsAllowAboutModule ? 0 : this.DefaultModuleIndex);
                if (moduleInfo != null)
                {
                    this.ShowModule(moduleInfo.Name);
                    MainFormHelper.SelectAccordionControlItem(this.accordionControl1, moduleInfo.FullTypeName);
                }
            }
            if (this.fullWindow)
            {
                this.bciFullWindow.Checked = true;
                this.gcDescription.Visible = (this.horzSplitter.Visible = (ModulesInfo.Instance.CurrentModuleBase.Description != ""));
            }
        }

        protected virtual void RemoveNavBarItems()
        {
            foreach (AccordionControlElement current in this.accordionControl1.Elements)
            {
                for (int i = current.Elements.Count - 1; i >= 0; i--)
                {
                    ModuleInfo moduleInfo = current.Elements[i].Tag as ModuleInfo;
                    if (moduleInfo == null)
                    {
                        return;
                    }
                    if (this.NotTranslatedModuleTypes.Contains(string.Format(";{0};", moduleInfo.TypeName)))
                    {
                        current.Elements.RemoveAt(i);
                    }
                }
                current.Visible = (current.Elements.Count != 0);
            }
        }

        public void ResetNavbarSelectedLink()
        {
            this.accordionControl1.SelectedElement = null;
        }

        public void ShowModule(string name)
        {
            try
            {
                this.CloseWhatsThis();
                if (ModulesInfo.Instance.CurrentModuleBase != null)
                {
                    this.prevModuleName = ModulesInfo.Instance.CurrentModuleBase.Name;
                }
                this.gcContainer.Parent.SuspendLayout();
                this.gcContainer.SuspendLayout();
                this.ShowModule(name, this.gcContainer, this.defaultLookAndFeel1, this.pnlCaption, this.gcDescription);
                this.gcDescription.Visible = (this.horzSplitter.Visible = this.ShowPanelDescription);
                if (this.AllowDescriptionText)
                {
                    this.gcDescription.Text = ModulesInfo.Instance.CurrentModuleBase.Description;
                }
                if (this.CurrentModule != null)
                {
                    this.ShowCaption();
                    this.gcContainer.ResumeLayout();
                    this.gcContainer.Parent.ResumeLayout();
                    this.CurrentModule.Invalidate();
                    this.InitCurrentRibbon();
                    this.Text = (this.IsCurrentAbout ? this.DemoName : string.Format("{0} - {1}", this.DemoName, this.CurrentModule.TutorialName));
                    MainFormHelper.CompactCurrentProcessWorkingSet();
                    this.whatsThisController.UpdateWhatsThisInfo(this.CurrentModule.TutorialInfo.WhatsThisXMLFile, this.CurrentModule.TutorialInfo.WhatsThisCodeFile, base.GetType());
                    this.rpgCode.Visible = (this.whatsThisController.IsWhatsThisInfoValid() || !string.IsNullOrEmpty(this.ProductName));
                    this.bbiCode.Visibility = (this.whatsThisController.IsWhatsThisInfoValid() ? BarItemVisibility.Always : BarItemVisibility.Never);
                    this.bbiOpenSolution.Visibility = ((!string.IsNullOrEmpty(this.ProductName) && !(this.CurrentModule is CodeTutorialControlBase)) ? BarItemVisibility.Always : BarItemVisibility.Never);
                    this.UpdateAboutButtonInfo();
                }
            }
            catch (Exception innerException)
            {
                Assembly entryAssembly = Assembly.GetEntryAssembly();
                string message = string.Concat(new string[]
                {
                    "Error on Showing Module:",
                    name,
                    Environment.NewLine,
                    (!string.IsNullOrEmpty(this.prevModuleName)) ? ("PrevModule:" + this.prevModuleName + Environment.NewLine) : string.Empty,
                    (entryAssembly != null) ? ("StartUp:" + entryAssembly.Location) : string.Empty
                });
                throw new ApplicationException(message, innerException);
            }
        }

        private void UpdateAboutButtonInfo()
        {
            this.bbiAbout.Caption = Resources.AboutCaption;
        }

        protected virtual void InitCurrentRibbon()
        {
            this.rpgActiveDemo.Visible = this.CurrentModule.HasActiveDemo;
            this.rpgAppearance.Visible = this.CurrentModule.AllowAppearanceGroup;
            this.rpgExport.ShowCaptionButton = this.CurrentModule.AllowPrintOptions;
            this.bbiGenerateReport.Visibility = (this.CurrentModule.AllowGenerateReport ? BarItemVisibility.Always : BarItemVisibility.Never);
            this.UpdateNavigationButton();
        }

        private void UpdateNavigationButton()
        {
            this.bbiUp.Enabled = (this.bbiDown.Enabled = (!this.IsCurrentAbout && this.accordionControl1.SelectedElement != null && AccordionNavigationMenuHelper.GetNodeCount(this.accordionControl1) > 1));
        }

        private void accordionControl1_SelectedLinkChanged(object sender, SelectedElementChangedEventArgs e)
        {
            if (e.Element == null)
            {
                return;
            }
            this.ShowModule(e.Element.Text);
            this.prevLink = e.Element;
        }

        private void accordionControl1_LinkClicked(object sender, ElementClickEventArgs e)
        {
            if (e.Element == this.accordionControl1.SelectedElement && this.IsCurrentAbout && !string.IsNullOrEmpty(this.prevModuleName))
            {
                this.ShowModuleByName(this.prevModuleName);
            }
        }

        private void ShowModuleByName(string name)
        {
            this.ShowModule(name);
            if (this.prevLink != null)
            {
                this.accordionControl1.SelectElement(this.prevLink);
            }
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape && this.IsCurrentAbout && !string.IsNullOrEmpty(this.prevModuleName))
            {
                this.ShowModuleByName(this.prevModuleName);
            }
        }

        public void ClearNavBarFilter()
        {
        }

        private void ShowCaption()
        {
            if (this.CurrentModule != null)
            {
                this.pnlCaption.Visible = false;
            }
        }

        public void HideServiceElements()
        {
            this.gcDescription.Visible = (this.horzSplitter.Visible = false);
            this.gcNavigations.Hide();
            this.pnlCaption.Hide();
            this.bciFilter.Enabled = false;
        }

        public void ShowServiceElements()
        {
            ModuleInfo currentModuleBase = ModulesInfo.Instance.CurrentModuleBase;
            this.gcDescription.Visible = (this.horzSplitter.Visible = (currentModuleBase != null && !string.IsNullOrEmpty(currentModuleBase.Description)));
            this.gcNavigations.Show();
            this.ShowCaption();
            this.bciFilter.Enabled = true;
        }

        public void ShowDemoFilter()
        {
        }

        private void rpgAppearance_CaptionButtonClick(object sender, RibbonPageGroupEventArgs e)
        {
            this.pmAppearance.ShowPopup(Control.MousePosition);
        }

        private void pmAppearance_Popup(object sender, EventArgs e)
        {
            this.bciAllowFormSkin.Checked = SkinManager.AllowFormSkins;
            this.bciAllowFormSkin.Enabled = (UserLookAndFeel.Default.ActiveSkinName != "Office 2016 Colorful");
        }

        private void bciAllowFormSkin_ItemClick(object sender, ItemClickEventArgs e)
        {
            base.AllowFormGlass = (SkinManager.AllowFormSkins ? DefaultBoolean.True : DefaultBoolean.False);
            if (SkinManager.AllowFormSkins)
            {
                SkinManager.DisableFormSkins();
                return;
            }
            SkinManager.EnableFormSkins();
        }

        private void bbiUp_ItemClick(object sender, ItemClickEventArgs e)
        {
            AccordionNavigationMenuHelper.ShowPrev(this.accordionControl1);
        }

        private void bbiDown_ItemClick(object sender, ItemClickEventArgs e)
        {
            AccordionNavigationMenuHelper.ShowNext(this.accordionControl1);
        }

        private void bbiActiveDemo_ItemClick(object sender, ItemClickEventArgs e)
        {
            TutorialControlBase tutorialControlBase = ModulesInfo.Instance.CurrentModuleBase.TModule as TutorialControlBase;
            if (tutorialControlBase == null || !tutorialControlBase.HasActiveDemo)
            {
                return;
            }
            tutorialControlBase.RunActiveDemo();
        }

        private void bbiAbout_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.ShowAbout();
        }

        internal void StartDemo()
        {
            AccordionNavigationMenuHelper.StartDemo(this.accordionControl1);
        }

        private void bciFullWindow_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            if (this.bciFullWindow.Checked)
            {
                this.HideServiceElements();
                return;
            }
            this.ShowServiceElements();
        }

        private void bciFilter_CheckedChanged(object sender, ItemClickEventArgs e)
        {
        }

        private void pmPrintOptions_BeforePopup(object sender, CancelEventArgs e)
        {
            this.bciShowRibbonPreview.Checked = this.RibbonMenuManager.PrintOptions.ShowRibbonPreviewForm;
        }

        private void bciShowRibbonPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.RibbonMenuManager.PrintOptions.ShowRibbonPreviewForm = this.bciShowRibbonPreview.Checked;
        }

        private void rpgExport_CaptionButtonClick(object sender, RibbonPageGroupEventArgs e)
        {
            this.pmPrintOptions.ShowPopup(Control.MousePosition);
        }

        private void ribbonControl1_Paint(object sender, PaintEventArgs e)
        {
            if (this.IsCurrentAbout)
            {
                return;
            }
            RibbonViewInfo viewInfo = this.ribbonControl1.ViewInfo;
            if (viewInfo == null)
            {
                return;
            }
            RibbonPanelViewInfo panel = viewInfo.Panel;
            if (panel == null)
            {
                return;
            }
            Rectangle bounds = panel.Bounds;
            int num = bounds.X;
            RibbonPageGroupViewInfoCollection groups = panel.Groups;
            if (groups == null)
            {
                return;
            }
            if (groups.Count > 0)
            {
                num = (this.IsRibbonRTL ? groups[groups.Count - 1].Bounds.X : groups[groups.Count - 1].Bounds.Right);
            }
            Image devExpressLogoImageForRibbon = this.DevExpressLogoImageForRibbon;
            if (bounds.Height < devExpressLogoImageForRibbon.Height)
            {
                return;
            }
            int num2 = 15;
            int num3 = (bounds.Height - devExpressLogoImageForRibbon.Height) / 2;
            bounds.X = (this.IsRibbonRTL ? num2 : (bounds.Width - devExpressLogoImageForRibbon.Width - num2));
            bounds.Width = devExpressLogoImageForRibbon.Width;
            bounds.Y += num3;
            bounds.Height = devExpressLogoImageForRibbon.Height;
            if (this.IsRibbonRTL ? (bounds.Right > num) : (bounds.X < num))
            {
                return;
            }
            e.Graphics.DrawImage(devExpressLogoImageForRibbon, bounds.Location);
        }

        private void bbiCode_DownChanged(object sender, ItemClickEventArgs e)
        {
            if (this.WhatsThisEnabled)
            {
                this.DisableWhatsThis();
                return;
            }
            this.EnableWhatsThis();
        }

        protected virtual void NotifyModuleWhatsThisStateChange(bool whatsThisStarted)
        {
            if (this.CurrentModule == null)
            {
                return;
            }
            if (whatsThisStarted)
            {
                this.CurrentModule.StartWhatsThis();
            }
            else
            {
                this.CurrentModule.EndWhatsThis();
            }
            this.accordionControl1.Enabled = !whatsThisStarted;
            foreach (RibbonPage ribbonPage in this.ribbonControl1.Pages)
            {
                foreach (RibbonPageGroup ribbonPageGroup in ribbonPage.Groups)
                {
                    if (ribbonPageGroup != this.rpgCode)
                    {
                        ribbonPageGroup.Enabled = !whatsThisStarted;
                    }
                }
            }
            this.bbiOpenSolution.Enabled = !whatsThisStarted;
        }

        protected void EnableWhatsThis()
        {
            if (this.WhatsThisEnabled)
            {
                return;
            }
            this.NotifyModuleWhatsThisStateChange(true);
            this.whatsThisController.UpdateRegisteredVisibleControls();
            this.ShowWhatsThisModule();
        }

        private void CloseWhatsThis()
        {
            this.bbiCode.Down = false;
        }

        protected void DisableWhatsThis()
        {
            if (!this.WhatsThisEnabled)
            {
                return;
            }
            this.NotifyModuleWhatsThisStateChange(false);
            this.HideWhatsThisModule();
        }

        private void ShowWhatsThisModule()
        {
            this.whatsThisController.UpdateWhatsThisBitmaps();
            this.CurrentModule.Hide();
            this.whatsThisModule.Show();
        }

        private void HideWhatsThisModule()
        {
            this.whatsThisModule.Visible = false;
            this.CurrentModule.Show();
        }

        private void InitWhatsThisModule()
        {
            this.whatsThisModule = new ModuleWhatsThis(this.whatsThisController);
            this.whatsThisModule.Parent = this.gcContainer;
            this.whatsThisModule.Dock = DockStyle.Fill;
            this.whatsThisModule.Visible = false;
        }

        private void RibbonMainForm_Resize(object sender, EventArgs e)
        {
            this.CloseWhatsThis();
        }

        private void RibbonMainForm_Move(object sender, EventArgs e)
        {
            this.CloseWhatsThis();
        }

        public void MergeRibbon(RibbonControl childRibbon)
        {
            base.SuspendLayout();
            this.RibbonControl.MergeRibbon(childRibbon);
            if (this.demoCategory.Pages.Count == 0)
            {
                this.demoCategory.Pages.Add(this.rpMain);
            }
            if (this.RibbonControl.MergedPages.Count > 0)
            {
                this.RibbonControl.SelectedPage = childRibbon.SelectedPage;
            }
            base.ResumeLayout();
        }

        public void UnMergeRibbon()
        {
            this.RibbonControl.UnMergeRibbon();
            this.RibbonControl.Pages.Add(this.rpMain);
        }

        private void ribbonControl1_UnMerge(object sender, RibbonMergeEventArgs e)
        {
            if (!this.AllowMergeStatusBar)
            {
                return;
            }
            RibbonControl ribbonControl = (RibbonControl)sender;
            if (e.MergedChild.StatusBar != null && ribbonControl.StatusBar != null)
            {
                ribbonControl.StatusBar.UnMergeStatusBar();
            }
        }

        private void ribbonControl1_Merge(object sender, RibbonMergeEventArgs e)
        {
            if (!this.AllowMergeStatusBar)
            {
                return;
            }
            RibbonControl ribbonControl = (RibbonControl)sender;
            if (e.MergedChild.StatusBar != null && ribbonControl.StatusBar != null)
            {
                ribbonControl.StatusBar.ItemLinks.Clear();
                ribbonControl.StatusBar.MergeStatusBar(e.MergedChild.StatusBar);
            }
        }

        private void bbiGettingStarted_ItemClick(object sender, ItemClickEventArgs e)
        {
            PictureButton.ProcessStart(this.GetLink(RibbonMainForm.GetStartedLink));
        }

        private void bbiGetFreeSupport_ItemClick(object sender, ItemClickEventArgs e)
        {
            PictureButton.ProcessStart(this.GetLink("Https://go.devexpress.com/Demo_2013_GetSupport.aspx"));
        }

        private void bbiBuyNow_ItemClick(object sender, ItemClickEventArgs e)
        {
            PictureButton.ProcessStart(this.GetLink("Https://go.devexpress.com/Demo_2013_BuyNow.aspx"));
        }

        protected virtual string GetLink(string link)
        {
            return string.Format("{0}?gldata={1}_{2}", link, "16.1", this.CurrentModule.FullTypeName);
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            new ColorWheelForm
            {
                StartPosition = FormStartPosition.CenterParent,
                SkinMaskColor = UserLookAndFeel.Default.SkinMaskColor,
                SkinMaskColor2 = UserLookAndFeel.Default.SkinMaskColor2
            }.ShowDialog(this);
        }

        private void accordionControl1_CustomElementText(object sender, CustomElementTextEventArgs e)
        {
            if (e.Text.Contains(" (new)"))
            {
                e.Text = e.Text.Replace(" (new)", "");
                e.ObjectInfo.Element.TagInternal = "new";
            }
            if (e.Text.Contains(" (updated)"))
            {
                e.Text = e.Text.Replace(" (updated)", "");
                e.ObjectInfo.Element.TagInternal = "updated";
            }
        }

        private void accordionControl1_CustomDrawElement(object sender, CustomDrawElementEventArgs e)
        {
            if (e.ObjectInfo.HeaderBounds == Rectangle.Empty)
            {
                return;
            }
            e.DrawHeaderBackground();
            if (e.ObjectInfo.ControlInfo.AccordionControl.ShowGroupExpandButtons)
            {
                e.DrawExpandCollapseButton();
            }
            e.DrawImage();
            e.DrawContextButtons();
            this.DrawHighlight(e);
            e.DrawText();
            e.DrawElementSelection();
            e.Handled = true;
        }

        protected void DrawHighlight(CustomDrawElementEventArgs e)
        {
            if (e.ObjectInfo is AccordionGroupViewInfo)
            {
                this.DrawGroupHighlight(e);
            }
            if (e.ObjectInfo is AccordionItemViewInfo)
            {
                this.DrawItemHighlight(e);
            }
        }

        protected void DrawItemHighlight(CustomDrawElementEventArgs e)
        {
            if (e.ObjectInfo.Element.OwnerElement != null && e.ObjectInfo.Element.OwnerElement.Text.Contains("New"))
            {
                return;
            }
            if (e.ObjectInfo.Element == this.accordionControl1.SelectedElement || e.ObjectInfo.ControlInfo.PressedInfo.ItemInfo == e.ObjectInfo)
            {
                return;
            }
            if (e.ObjectInfo.Element.TagInternal != null && e.ObjectInfo.Element.TagInternal.ToString() == "new")
            {
                Rectangle textBounds = e.ObjectInfo.TextBounds;
                Rectangle rect = new Rectangle(textBounds.X - 3, textBounds.Y - 3, textBounds.Width + 6, textBounds.Height + 6);
                e.Cache.FillRectangle(this.NewItemColor, rect);
            }
            if (e.ObjectInfo.Element.TagInternal != null && e.ObjectInfo.Element.TagInternal.ToString() == "updated")
            {
                Rectangle textBounds2 = e.ObjectInfo.TextBounds;
                Rectangle rect2 = new Rectangle(textBounds2.X - 3, textBounds2.Y - 3, textBounds2.Width + 6, textBounds2.Height + 6);
                e.Cache.FillRectangle(this.UpdatedItemColor, rect2);
            }
        }

        protected void DrawGroupHighlight(CustomDrawElementEventArgs e)
        {
            if (e.ObjectInfo.Text.Contains("New"))
            {
                Rectangle rect = new Rectangle(e.ObjectInfo.HeaderBounds.X + 2, e.ObjectInfo.HeaderBounds.Y + 2, e.ObjectInfo.HeaderBounds.Width - 4, e.ObjectInfo.HeaderBounds.Height - 4);
                e.Cache.FillRectangle(this.NewGroupColor, rect);
                return;
            }
            if (e.ObjectInfo.Text.Contains("Highlighted"))
            {
                Rectangle rect2 = new Rectangle(e.ObjectInfo.HeaderBounds.X + 2, e.ObjectInfo.HeaderBounds.Y + 2, e.ObjectInfo.HeaderBounds.Width - 4, e.ObjectInfo.HeaderBounds.Height - 4);
                e.Cache.FillRectangle(this.HighlightedGroupColor, rect2);
            }
        }

        private static string GetRGBColor(Color color)
        {
            return string.Format("{0},{1},{2}", color.R, color.G, color.B);
        }

        protected virtual Product GetProduct()
        {
            return Repository.WinPlatform.Products.First((Product p) => p.Name == this.ProductName);
        }

        protected virtual void OpenSolution()
        {
            if (string.IsNullOrEmpty(this.ProductName))
            {
                return;
            }
            Product product = this.GetProduct();
            Demo demo = product.Demos.FirstOrDefault((Demo x) => x.Modules.Count > 0);
            SimpleModule simpleModule = (SimpleModule)demo.Modules.First((DevExpress.DemoData.Model.Module p) => p.Type == this.CurrentModule.FullTypeName);
            if (product.Name == "XtraReportsForWin" && simpleModule.Demo.Name == "MainDemo")
            {
                string csPath = "\\Reporting\\CS\\DevExpress.DemoReports\\DevExpress.DemoReports.sln";
                string vbPath = "\\Reporting\\VB\\DevExpress.DemoReports\\DevExpress.DemoReports.sln";
                simpleModule = simpleModule.WithDemo(((SimpleDemo)simpleModule.Demo).WithSolutionPaths(csPath, vbPath));
            }
            if (ExampleLanguage.Csharp.Equals(this.bbiOpenSolution.Tag))
            {
                DemoRunner.OpenCSSolution(simpleModule, CallingSite.WinDemoChooser, true);
                return;
            }
            if (ExampleLanguage.VB.Equals(this.bbiOpenSolution.Tag))
            {
                DemoRunner.OpenVBSolution(simpleModule, CallingSite.WinDemoChooser, true);
            }
        }

        private void InitOpenSolution(BarButtonItem item, bool forceOpen)
        {
            this.bbiOpenSolution.Glyph = item.Glyph;
            this.bbiOpenSolution.LargeGlyph = item.LargeGlyph;
            this.bbiOpenSolution.Tag = ((item == this.bbiCSSolution) ? ExampleLanguage.Csharp : ExampleLanguage.VB);
            if (forceOpen)
            {
                this.OpenSolution();
            }
        }

        private void bbiOpenSolution_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.OpenSolution();
        }

        private void bbiCSSolution_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.InitOpenSolution(this.bbiCSSolution, true);
        }

        private void bbiVBSolution_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.InitOpenSolution(this.bbiVBSolution, true);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Alt && e.Control)
            {
                this.keyUpCode += e.KeyCode.ToString().ToLower();
                this.ApplyCode();
            }
        }

        private void ApplyCode()
        {
            if (this.keyUpCode.IndexOf("size") != -1)
            {
                this.keyUpCode = string.Empty;
                this.MinimumSize = Size.Empty;
            }
            if (this.keyUpCode.Length > 10)
            {
                this.keyUpCode = string.Empty;
            }
        }
    }
}
