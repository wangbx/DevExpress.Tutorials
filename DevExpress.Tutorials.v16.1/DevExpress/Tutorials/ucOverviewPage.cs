namespace DevExpress.Tutorials
{
    using DevExpress.DXperience.Demos;
    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.Tutorials.Controls;
    using DevExpress.Tutorials.Properties;
    using DevExpress.Utils;
    using DevExpress.Utils.About;
    using DevExpress.Utils.Frames;
    using DevExpress.Utils.Svg;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;

    public class ucOverviewPage : TutorialControlBase
    {
        private IContainer components;
        private static readonly SvgBitmap DarkLogo = GetSvgBitmap(true);
        private EmptySpaceItem emptySpaceItem1;
        private EmptySpaceItem emptySpaceItem2;
        private EmptySpaceItem emptySpaceItem3;
        private EmptySpaceItem emptySpaceItem4;
        private EmptySpaceItem emptySpaceItem5;
        private bool firstShow;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem10;
        private LayoutControlItem layoutControlItem11;
        private LayoutControlItem layoutControlItem12;
        private LayoutControlItem layoutControlItem13;
        private LayoutControlItem layoutControlItem14;
        private LayoutControlItem layoutControlItem15;
        private LayoutControlItem layoutControlItem2;
        private LayoutControlItem layoutControlItem3;
        private LayoutControlItem layoutControlItem4;
        private LayoutControlItem layoutControlItem5;
        private LayoutControlItem layoutControlItem6;
        private LayoutControlItem layoutControlItem7;
        private LayoutControlItem layoutControlItem8;
        private LayoutControlItem layoutControlItem9;
        private LabelControl lcCopyright;
        private OverviewLabel lcLine1;
        private OverviewLabel lcLine2;
        private OverviewLabel lcLine3;
        private OverviewLabel lcLine4;
        private LabelControl lcVersion;
        private static readonly SvgBitmap Logo = GetSvgBitmap(false);
        private PanelControl panelControl1;
        private PanelControl panelControl2;
        private PanelControl panelControl3;
        private PanelControl pcBuyNow;
        private PanelControl pcFreeSupport;
        private PanelControl pcGettingStarted;
        private PanelControl pcRunButton;
        private PictureEdit peAward;
        private PictureEdit peLogo;

        public ucOverviewPage()
        {
            EventHandler handler = null;
            this.firstShow = true;
            this.InitializeComponent();
            if (DpiProvider.Default.DpiScaleFactor != 1f)
            {
                this.peAward.Properties.SizeMode = PictureSizeMode.Zoom;
            }
            this.peAward.Image = this.Awards;
            this.peAward.Properties.NullText = " ";
            this.UpdateAwardsSize();
            this.InitData();
            this.UpdateImages(UserLookAndFeel.Default);
            if (handler == null)
            {
                handler = (s, e) => this.UpdateAwardsSize();
            }
            base.SizeChanged += handler;
        }

        private void CheckOpened()
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void DoHide()
        {
            base.DoHide();
            base.BeginInvoke(new MethodInvoker(delegate
            {
                this.ShowOverview(true);
            }));
        }

        protected override void DoShow()
        {
            base.DoShow();
            this.ShowOverview(false);
        }

        private int GetAwardsMainImageHeight()
        {
            try
            {
                return Resources.Awards_main.Height;
            }
            catch
            {
                return 220;
            }
        }

        private static string GetLogoImageName(bool dark)
        {
            string str = "DevExpress.Utils.XtraFrames.DevExpress-Logo";
            if (!dark)
            {
                return (str + ".svg");
            }
            return (str + "-Mono.svg");
        }

        private static SvgBitmap GetSvgBitmap(bool dark)
        {
            using (Stream stream = typeof(ApplicationCaption).Assembly.GetManifestResourceStream(GetLogoImageName(dark)))
            {
                SvgBitmap bitmap = SvgLoader.LoadSvgBitmapFromStream(stream);
                bitmap.Scale = 4.0;
                return bitmap;
            }
        }

        public static Image GetSVGLogoImage()
        {
            return GetSVGLogoImage(UserLookAndFeel.Default);
        }

        private static Image GetSVGLogoImage(UserLookAndFeel lookAndFeel)
        {
            SvgBitmap bitmap = FrameHelper.IsDarkSkin(lookAndFeel) ? DarkLogo : Logo;
            return bitmap.Render(null, (double) DpiProvider.Default.DpiScaleFactor);
        }

        private string GetVSMText(string s)
        {
            string str = "of Visual Studio Magazine.";
            string newValue = "of\r\nVisual Studio Magazine.";
            if (s.IndexOf(str) > 0)
            {
                s = s.Replace(str, newValue);
            }
            return s;
        }

        private void InitData()
        {
            try
            {
                this.lcLine1.Font = new Font("Segoe UI Light", 36f, FontStyle.Regular);
                this.lcLine2.Font = new Font("Segoe UI Light", 24f, FontStyle.Regular);
                this.lcLine3.Font = new Font("Segoe UI Light", 24f, FontStyle.Regular);
                this.lcLine4.Font = new Font("Segoe UI Light", 18f, FontStyle.Regular);
            }
            catch
            {
            }
            this.lcLine1.Text = this.Line1Text;
            this.lcLine2.Text = this.Line2Text;
            this.lcLine3.Text = this.Line3Text;
            this.lcLine4.Text = this.GetVSMText(this.Line4Text);
            this.lcCopyright.Text = AboutHelper.CopyRightOverview;
            this.lcVersion.Text = string.Format("{0} {1}", Resources.Version, "16.1.8.0");
            if (!this.IsTrial)
            {
                this.lcVersion.Text = this.lcVersion.Text + string.Format("\r\n{0}: {1}", Resources.SerialNumber, AboutHelper.GetSerial(this.ProductInfo));
            }
            new OverviewButton(this.pcRunButton, ResourceImageHelper.CreateImageFromResources("DevExpress.Tutorials.Images.RunButtonNormal.png", typeof(ucAboutPage).Assembly), ResourceImageHelper.CreateImageFromResources("DevExpress.Tutorials.Images.RunButtonHover.png", typeof(ucAboutPage).Assembly), ResourceImageHelper.CreateImageFromResources("DevExpress.Tutorials.Images.RunButtonPressed.png", typeof(ucAboutPage).Assembly), string.Empty).ButtonClick += new EventHandler(this.runButton_ButtonClick);
            new OverviewButton(this.pcGettingStarted, ResourceImageHelper.CreateImageFromResources("DevExpress.Tutorials.Images.Overview-Getting-Started-Normal.png", typeof(ucAboutPage).Assembly), null, null, RibbonMainForm.GetStartedLink);
            new OverviewButton(this.pcBuyNow, ResourceImageHelper.CreateImageFromResources("DevExpress.Tutorials.Images.Overview-Buy-Normal.png", typeof(ucAboutPage).Assembly), null, null, "Https://go.devexpress.com/Demo_2013_BuyNow.aspx");
            new OverviewButton(this.pcFreeSupport, ResourceImageHelper.CreateImageFromResources("DevExpress.Tutorials.Images.Overview-Support-Normal.png", typeof(ucAboutPage).Assembly), null, null, "Https://go.devexpress.com/Demo_2013_GetSupport.aspx");
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new LayoutControl();
            this.panelControl3 = new PanelControl();
            this.panelControl2 = new PanelControl();
            this.panelControl1 = new PanelControl();
            this.pcGettingStarted = new PanelControl();
            this.pcFreeSupport = new PanelControl();
            this.pcBuyNow = new PanelControl();
            this.lcCopyright = new LabelControl();
            this.lcVersion = new LabelControl();
            this.pcRunButton = new PanelControl();
            this.lcLine4 = new OverviewLabel();
            this.lcLine3 = new OverviewLabel();
            this.peAward = new PictureEdit();
            this.lcLine2 = new OverviewLabel();
            this.lcLine1 = new OverviewLabel();
            this.peLogo = new PictureEdit();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.layoutControlItem1 = new LayoutControlItem();
            this.layoutControlItem2 = new LayoutControlItem();
            this.layoutControlItem3 = new LayoutControlItem();
            this.layoutControlItem4 = new LayoutControlItem();
            this.layoutControlItem7 = new LayoutControlItem();
            this.layoutControlItem8 = new LayoutControlItem();
            this.layoutControlItem9 = new LayoutControlItem();
            this.layoutControlItem10 = new LayoutControlItem();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.layoutControlItem11 = new LayoutControlItem();
            this.layoutControlItem12 = new LayoutControlItem();
            this.emptySpaceItem2 = new EmptySpaceItem();
            this.layoutControlItem5 = new LayoutControlItem();
            this.layoutControlItem6 = new LayoutControlItem();
            this.emptySpaceItem5 = new EmptySpaceItem();
            this.layoutControlItem13 = new LayoutControlItem();
            this.layoutControlItem14 = new LayoutControlItem();
            this.emptySpaceItem3 = new EmptySpaceItem();
            this.emptySpaceItem4 = new EmptySpaceItem();
            this.layoutControlItem15 = new LayoutControlItem();
            this.layoutControl1.BeginInit();
            this.layoutControl1.SuspendLayout();
            this.panelControl3.BeginInit();
            this.panelControl2.BeginInit();
            this.panelControl1.BeginInit();
            this.pcGettingStarted.BeginInit();
            this.pcFreeSupport.BeginInit();
            this.pcBuyNow.BeginInit();
            this.pcRunButton.BeginInit();
            this.peAward.Properties.BeginInit();
            this.peLogo.Properties.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.layoutControlItem1.BeginInit();
            this.layoutControlItem2.BeginInit();
            this.layoutControlItem3.BeginInit();
            this.layoutControlItem4.BeginInit();
            this.layoutControlItem7.BeginInit();
            this.layoutControlItem8.BeginInit();
            this.layoutControlItem9.BeginInit();
            this.layoutControlItem10.BeginInit();
            this.emptySpaceItem1.BeginInit();
            this.layoutControlItem11.BeginInit();
            this.layoutControlItem12.BeginInit();
            this.emptySpaceItem2.BeginInit();
            this.layoutControlItem5.BeginInit();
            this.layoutControlItem6.BeginInit();
            this.emptySpaceItem5.BeginInit();
            this.layoutControlItem13.BeginInit();
            this.layoutControlItem14.BeginInit();
            this.emptySpaceItem3.BeginInit();
            this.emptySpaceItem4.BeginInit();
            this.layoutControlItem15.BeginInit();
            base.SuspendLayout();
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.panelControl3);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Controls.Add(this.panelControl1);
            this.layoutControl1.Controls.Add(this.pcGettingStarted);
            this.layoutControl1.Controls.Add(this.pcFreeSupport);
            this.layoutControl1.Controls.Add(this.pcBuyNow);
            this.layoutControl1.Controls.Add(this.lcCopyright);
            this.layoutControl1.Controls.Add(this.lcVersion);
            this.layoutControl1.Controls.Add(this.pcRunButton);
            this.layoutControl1.Controls.Add(this.lcLine4);
            this.layoutControl1.Controls.Add(this.lcLine3);
            this.layoutControl1.Controls.Add(this.peAward);
            this.layoutControl1.Controls.Add(this.lcLine2);
            this.layoutControl1.Controls.Add(this.lcLine1);
            this.layoutControl1.Controls.Add(this.peLogo);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(0x2a1, 510, 0x358, 350);
            this.layoutControl1.OptionsView.UseParentAutoScaleFactor = true;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(0x4df, 0x2cf);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            this.panelControl3.BorderStyle = BorderStyles.NoBorder;
            this.panelControl3.Location = new Point(0x4d5, 0xe9);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new Size(10, 220);
            this.panelControl3.TabIndex = 0x12;
            this.panelControl2.BorderStyle = BorderStyles.NoBorder;
            this.panelControl2.Location = new Point(670, 0xe9);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new Size(0x237, 0x26);
            this.panelControl2.TabIndex = 0x11;
            this.panelControl1.BorderStyle = BorderStyles.NoBorder;
            this.panelControl1.Location = new Point(0x28f, 0xe9);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new Size(15, 220);
            this.panelControl1.TabIndex = 0x10;
            this.pcGettingStarted.BorderStyle = BorderStyles.NoBorder;
            this.pcGettingStarted.Location = new Point(0x3a9, 0x289);
            this.pcGettingStarted.Name = "pcGettingStarted";
            this.pcGettingStarted.Size = new Size(100, 50);
            this.pcGettingStarted.TabIndex = 15;
            this.pcFreeSupport.BorderStyle = BorderStyles.NoBorder;
            this.pcFreeSupport.Location = new Point(0x411, 0x289);
            this.pcFreeSupport.Name = "pcFreeSupport";
            this.pcFreeSupport.Size = new Size(100, 50);
            this.pcFreeSupport.TabIndex = 14;
            this.pcBuyNow.BorderStyle = BorderStyles.NoBorder;
            this.pcBuyNow.Location = new Point(0x479, 0x289);
            this.pcBuyNow.Name = "pcBuyNow";
            this.pcBuyNow.Size = new Size(100, 50);
            this.pcBuyNow.TabIndex = 13;
            this.lcCopyright.AllowHtmlString = true;
            this.lcCopyright.Appearance.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lcCopyright.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            this.lcCopyright.Enabled = false;
            this.lcCopyright.Location = new Point(12, 0x2be);
            this.lcCopyright.Name = "lcCopyright";
            this.lcCopyright.Size = new Size(70, 13);
            this.lcCopyright.StyleController = this.layoutControl1;
            this.lcCopyright.TabIndex = 12;
            this.lcCopyright.Text = "labelControl2";
            this.lcVersion.Appearance.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lcVersion.Location = new Point(12, 0x2ad);
            this.lcVersion.Name = "lcVersion";
            this.lcVersion.Size = new Size(70, 13);
            this.lcVersion.StyleController = this.layoutControl1;
            this.lcVersion.TabIndex = 11;
            this.lcVersion.Text = "labelControl1";
            this.pcRunButton.BorderStyle = BorderStyles.NoBorder;
            this.pcRunButton.Location = new Point(10, 0x1c7);
            this.pcRunButton.Name = "pcRunButton";
            this.pcRunButton.Size = new Size(0x4cb, 190);
            this.pcRunButton.TabIndex = 10;
            this.lcLine4.Appearance.Font = new Font("Segoe UI", 24f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lcLine4.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            this.lcLine4.Appearance.TextOptions.VAlignment = VertAlignment.Top;
            this.lcLine4.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            this.lcLine4.AutoSizeMode = LabelAutoSizeMode.None;
            this.lcLine4.Location = new Point(670, 0x12f);
            this.lcLine4.Name = "lcLine4";
            this.lcLine4.Size = new Size(0x237, 150);
            this.lcLine4.StyleController = this.layoutControl1;
            this.lcLine4.TabIndex = 9;
            this.lcLine3.Appearance.Font = new Font("Segoe UI", 18f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lcLine3.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            this.lcLine3.Appearance.TextOptions.VAlignment = VertAlignment.Top;
            this.lcLine3.AutoSizeMode = LabelAutoSizeMode.None;
            this.lcLine3.Location = new Point(670, 0x10f);
            this.lcLine3.Name = "lcLine3";
            this.lcLine3.Size = new Size(0x237, 0x20);
            this.lcLine3.StyleController = this.layoutControl1;
            this.lcLine3.TabIndex = 8;
            this.peAward.EditValue = Resources.Awards_main;
            this.peAward.Location = new Point(0, 0xe9);
            this.peAward.Name = "peAward";
            this.peAward.Properties.AllowFocused = false;
            this.peAward.Properties.Appearance.BackColor = Color.Transparent;
            this.peAward.Properties.Appearance.Options.UseBackColor = true;
            this.peAward.Properties.BorderStyle = BorderStyles.NoBorder;
            this.peAward.Properties.PictureAlignment = ContentAlignment.MiddleRight;
            this.peAward.Properties.ShowMenu = false;
            this.peAward.Size = new Size(0x28f, 220);
            this.peAward.StyleController = this.layoutControl1;
            this.peAward.TabIndex = 7;
            this.lcLine2.Appearance.Font = new Font("Segoe UI", 24f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lcLine2.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lcLine2.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            this.lcLine2.AutoSizeMode = LabelAutoSizeMode.None;
            this.lcLine2.Location = new Point(2, 0x89);
            this.lcLine2.Name = "lcLine2";
            this.lcLine2.Size = new Size(0x4db, 0x2d);
            this.lcLine2.StyleController = this.layoutControl1;
            this.lcLine2.TabIndex = 6;
            this.lcLine1.Appearance.Font = new Font("Segoe UI", 36f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lcLine1.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lcLine1.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            this.lcLine1.AutoSizeMode = LabelAutoSizeMode.None;
            this.lcLine1.Location = new Point(2, 0x44);
            this.lcLine1.Name = "lcLine1";
            this.lcLine1.Size = new Size(0x4db, 0x41);
            this.lcLine1.StyleController = this.layoutControl1;
            this.lcLine1.TabIndex = 5;
            this.peLogo.Location = new Point(12, 12);
            this.peLogo.Name = "peLogo";
            this.peLogo.Properties.AllowFocused = false;
            this.peLogo.Properties.Appearance.BackColor = Color.Transparent;
            this.peLogo.Properties.Appearance.Options.UseBackColor = true;
            this.peLogo.Properties.BorderStyle = BorderStyles.NoBorder;
            this.peLogo.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
            this.peLogo.Properties.ShowMenu = false;
            this.peLogo.Size = new Size(0x4d1, 0x34);
            this.peLogo.StyleController = this.layoutControl1;
            this.peLogo.TabIndex = 4;
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { 
                this.layoutControlItem1, this.layoutControlItem2, this.layoutControlItem3, this.layoutControlItem4, this.layoutControlItem7, this.layoutControlItem8, this.layoutControlItem9, this.layoutControlItem10, this.emptySpaceItem1, this.layoutControlItem11, this.layoutControlItem12, this.emptySpaceItem2, this.layoutControlItem5, this.layoutControlItem6, this.emptySpaceItem5, this.layoutControlItem13,
                this.layoutControlItem14, this.emptySpaceItem3, this.emptySpaceItem4, this.layoutControlItem15
            });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 10, 2);
            this.layoutControlGroup1.Size = new Size(0x4df, 0x2cf);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            this.layoutControlItem1.Control = this.peLogo;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new Point(10, 0);
            this.layoutControlItem1.MinSize = new Size(0x18, 0x20);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(0x4d5, 0x38);
            this.layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.layoutControlItem2.Control = this.lcLine1;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new Point(0, 0x38);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(0x4df, 0x45);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.layoutControlItem3.Control = this.lcLine2;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new Point(0, 0x7d);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(0x4df, 0x31);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            this.layoutControlItem4.Control = this.peAward;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new Point(0, 0xdf);
            this.layoutControlItem4.MaxSize = new Size(0x28f, 220);
            this.layoutControlItem4.MinSize = new Size(0x28f, 220);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem4.Size = new Size(0x28f, 220);
            this.layoutControlItem4.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.layoutControlItem7.Control = this.pcRunButton;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new Point(0, 0x1bb);
            this.layoutControlItem7.MinSize = new Size(0xd9, 70);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 2, 2);
            this.layoutControlItem7.Size = new Size(0x4df, 0xc2);
            this.layoutControlItem7.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            this.layoutControlItem8.Control = this.lcVersion;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new Point(10, 0x2a1);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new Size(0x4a, 0x11);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            this.layoutControlItem9.Control = this.lcCopyright;
            this.layoutControlItem9.CustomizationFormText = "layoutControlItem9";
            this.layoutControlItem9.Location = new Point(10, 690);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new Size(0x4a, 0x11);
            this.layoutControlItem9.Text = "layoutControlItem9";
            this.layoutControlItem9.TextSize = new Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            this.layoutControlItem10.AppearanceItemCaption.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.layoutControlItem10.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem10.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem10.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Center;
            this.layoutControlItem10.Control = this.pcBuyNow;
            this.layoutControlItem10.CustomizationFormText = "layoutControlItem10";
            this.layoutControlItem10.Location = new Point(0x477, 0x27d);
            this.layoutControlItem10.MaxSize = new Size(0x68, 70);
            this.layoutControlItem10.MinSize = new Size(0x68, 70);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new Size(0x68, 70);
            this.layoutControlItem10.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem10.Text = "Buy Now";
            this.layoutControlItem10.TextLocation = Locations.Bottom;
            this.layoutControlItem10.TextSize = new Size(0x4f, 13);
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new Point(0x54, 0x27d);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(0x353, 70);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            this.layoutControlItem11.AppearanceItemCaption.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.layoutControlItem11.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem11.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem11.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Center;
            this.layoutControlItem11.Control = this.pcFreeSupport;
            this.layoutControlItem11.CustomizationFormText = "layoutControlItem11";
            this.layoutControlItem11.Location = new Point(0x40f, 0x27d);
            this.layoutControlItem11.MaxSize = new Size(0x68, 70);
            this.layoutControlItem11.MinSize = new Size(0x68, 70);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new Size(0x68, 70);
            this.layoutControlItem11.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem11.Text = "Free Support";
            this.layoutControlItem11.TextLocation = Locations.Bottom;
            this.layoutControlItem11.TextSize = new Size(0x4f, 13);
            this.layoutControlItem12.AppearanceItemCaption.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.layoutControlItem12.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem12.AppearanceItemCaption.Options.UseTextOptions = true;
            this.layoutControlItem12.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Center;
            this.layoutControlItem12.Control = this.pcGettingStarted;
            this.layoutControlItem12.CustomizationFormText = "layoutControlItem12";
            this.layoutControlItem12.Location = new Point(0x3a7, 0x27d);
            this.layoutControlItem12.MaxSize = new Size(0x68, 70);
            this.layoutControlItem12.MinSize = new Size(0x68, 70);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new Size(0x68, 70);
            this.layoutControlItem12.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem12.Text = "Getting Started";
            this.layoutControlItem12.TextLocation = Locations.Bottom;
            this.layoutControlItem12.TextSize = new Size(0x4f, 13);
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new Point(10, 0x27d);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new Size(0x4a, 0x24);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new Size(0, 0);
            this.layoutControlItem5.Control = this.lcLine3;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new Point(670, 0x105);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new Size(0x237, 0x20);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            this.layoutControlItem6.Control = this.lcLine4;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new Point(670, 0x125);
            this.layoutControlItem6.MaxSize = new Size(0, 150);
            this.layoutControlItem6.MinSize = new Size(0x22, 150);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Size = new Size(0x237, 150);
            this.layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new Point(0, 0xae);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new Size(0x4df, 0x31);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new Size(0, 0);
            this.layoutControlItem13.Control = this.panelControl1;
            this.layoutControlItem13.CustomizationFormText = " ";
            this.layoutControlItem13.Location = new Point(0x28f, 0xdf);
            this.layoutControlItem13.MaxSize = new Size(15, 0);
            this.layoutControlItem13.MinSize = new Size(15, 0x18);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem13.Size = new Size(15, 220);
            this.layoutControlItem13.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem13.Text = " ";
            this.layoutControlItem13.TextSize = new Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            this.layoutControlItem14.Control = this.panelControl2;
            this.layoutControlItem14.CustomizationFormText = "layoutControlItem14";
            this.layoutControlItem14.Location = new Point(670, 0xdf);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem14.Size = new Size(0x237, 0x26);
            this.layoutControlItem14.Text = "layoutControlItem14";
            this.layoutControlItem14.TextSize = new Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new Point(0, 0x27d);
            this.emptySpaceItem3.MaxSize = new Size(10, 0);
            this.emptySpaceItem3.MinSize = new Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new Size(10, 70);
            this.emptySpaceItem3.SizeConstraintsType = SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new Size(0, 0);
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new Point(0, 0);
            this.emptySpaceItem4.MaxSize = new Size(10, 0);
            this.emptySpaceItem4.MinSize = new Size(10, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new Size(10, 0x38);
            this.emptySpaceItem4.SizeConstraintsType = SizeConstraintsType.Custom;
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new Size(0, 0);
            this.layoutControlItem15.Control = this.panelControl3;
            this.layoutControlItem15.CustomizationFormText = "layoutControlItem15";
            this.layoutControlItem15.Location = new Point(0x4d5, 0xdf);
            this.layoutControlItem15.MaxSize = new Size(10, 0);
            this.layoutControlItem15.MinSize = new Size(10, 0x18);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem15.Size = new Size(10, 220);
            this.layoutControlItem15.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem15.Text = "layoutControlItem15";
            this.layoutControlItem15.TextSize = new Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.layoutControl1);
            base.Name = "ucOverviewPage";
            base.Size = new Size(0x4df, 0x2cf);
            this.layoutControl1.EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.panelControl3.EndInit();
            this.panelControl2.EndInit();
            this.panelControl1.EndInit();
            this.pcGettingStarted.EndInit();
            this.pcFreeSupport.EndInit();
            this.pcBuyNow.EndInit();
            this.pcRunButton.EndInit();
            this.peAward.Properties.EndInit();
            this.peLogo.Properties.EndInit();
            this.layoutControlGroup1.EndInit();
            this.layoutControlItem1.EndInit();
            this.layoutControlItem2.EndInit();
            this.layoutControlItem3.EndInit();
            this.layoutControlItem4.EndInit();
            this.layoutControlItem7.EndInit();
            this.layoutControlItem8.EndInit();
            this.layoutControlItem9.EndInit();
            this.layoutControlItem10.EndInit();
            this.emptySpaceItem1.EndInit();
            this.layoutControlItem11.EndInit();
            this.layoutControlItem12.EndInit();
            this.emptySpaceItem2.EndInit();
            this.layoutControlItem5.EndInit();
            this.layoutControlItem6.EndInit();
            this.emptySpaceItem5.EndInit();
            this.layoutControlItem13.EndInit();
            this.layoutControlItem14.EndInit();
            this.emptySpaceItem3.EndInit();
            this.emptySpaceItem4.EndInit();
            this.layoutControlItem15.EndInit();
            base.ResumeLayout(false);
        }

        protected override void OnStyleChanged()
        {
            base.OnStyleChanged();
            this.UpdateImages(UserLookAndFeel.Default);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.StartDemo();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ResetShow()
        {
            this.firstShow = false;
            this.SaveInit();
        }

        private void runButton_ButtonClick(object sender, EventArgs e)
        {
            this.StartDemo();
        }

        private void SaveInit()
        {
        }

        private void ShowOverview(bool visible)
        {
            if (!visible && this.firstShow)
            {
                this.CheckOpened();
            }
            if (this.firstShow)
            {
                if (base.ParentFormMain != null)
                {
                    base.ParentFormMain.SuspendLayout();
                    if (base.ParentFormMain.MainPage != null)
                    {
                        base.ParentFormMain.MainPage.Visible = visible;
                    }
                    if (visible)
                    {
                        base.ParentFormMain.ShowServiceElements();
                    }
                    else
                    {
                        base.ParentFormMain.HideServiceElements();
                    }
                    base.ParentFormMain.ResumeLayout();
                }
                if (visible)
                {
                    this.ResetShow();
                }
            }
        }

        private void StartDemo()
        {
            RibbonMainForm form = base.FindForm() as RibbonMainForm;
            if (form != null)
            {
                form.StartDemo();
            }
        }

        private void UpdateAwardsSize()
        {
            int width = (this.Awards != null) ? Math.Max((int) (base.Width / 2), (int) (this.Awards.Width + 20)) : 10;
            int height = (this.Awards != null) ? (this.Awards.Height + 50) : this.GetAwardsMainImageHeight();
            this.layoutControlItem4.MaxSize = this.layoutControlItem4.MinSize = new Size(width, height);
        }

        private void UpdateImages(UserLookAndFeel lookAndFeel)
        {
            this.peLogo.Image = GetSVGLogoImage(lookAndFeel);
            this.lcLine2.ForeColor = this.lcLine4.ForeColor = CommonSkins.GetSkin(lookAndFeel).Colors.GetColor("DisabledText");
            if (lookAndFeel.ActiveSkinName == "Office 2013")
            {
                this.panelControl1.BackColor = this.panelControl2.BackColor = this.panelControl3.BackColor = this.peAward.BackColor = this.lcLine3.BackColor = this.lcLine4.BackColor = Color.FromArgb(0xf2, 0xf2, 0xf2);
            }
            else if (lookAndFeel.ActiveSkinName == "Office 2016 Colorful")
            {
                this.panelControl1.BackColor = this.panelControl2.BackColor = this.panelControl3.BackColor = this.peAward.BackColor = this.lcLine3.BackColor = this.lcLine4.BackColor = Color.FromArgb(0xe4, 0xe4, 0xe4);
            }
            else
            {
                this.panelControl1.BackColor = this.panelControl2.BackColor = this.panelControl3.BackColor = this.peAward.BackColor = this.lcLine3.BackColor = this.lcLine4.BackColor = Color.Transparent;
            }
        }

        protected virtual Image Awards
        {
            get
            {
                return Resources.Awards_main;
            }
        }

        private string IniFileName
        {
            get
            {
                return string.Format("{0}.ini", Application.ExecutablePath);
            }
        }

        protected bool IsTrial
        {
            get
            {
                return (AboutHelper.GetSerial(this.ProductInfo) == "-- TRIAL VERSION --");
            }
        }

        protected virtual string Line1Text
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual string Line2Text
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual string Line3Text
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual string Line4Text
        {
            get
            {
                return string.Empty;
            }
        }

        protected virtual DevExpress.Utils.About.ProductInfo ProductInfo
        {
            get
            {
                return new DevExpress.Utils.About.ProductInfo(string.Empty, typeof(ucOverviewPage), this.ProductKind, ProductInfoStage.Registered);
            }
        }

        protected virtual DevExpress.Utils.About.ProductKind ProductKind
        {
            get
            {
                return (DevExpress.Utils.About.ProductKind.Default | DevExpress.Utils.About.ProductKind.DXperienceWin);
            }
        }
    }
}

