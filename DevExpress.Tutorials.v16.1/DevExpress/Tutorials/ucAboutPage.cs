namespace DevExpress.Tutorials
{
    using DevExpress.DXperience.Demos;
    using DevExpress.LookAndFeel;
    using DevExpress.Utils;
    using DevExpress.Utils.About;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ucAboutPage : TutorialControlBase
    {
        private IContainer components;
        protected EmptySpaceItem emptySpaceItem1;
        protected EmptySpaceItem emptySpaceItem3;
        protected EmptySpaceItem emptySpaceItem4;
        protected EmptySpaceItem emptySpaceItem5;
        protected EmptySpaceItem emptySpaceItem6;
        protected EmptySpaceItem emptySpaceItem7;
        protected EmptySpaceItem esiStart1;
        protected EmptySpaceItem esiStart2;
        protected EmptySpaceItem esiStart3;
        protected LayoutControl layoutControl1;
        protected LayoutControlGroup layoutControlGroup1;
        protected LayoutControlItem layoutControlItem1;
        protected LayoutControlItem layoutControlItem2;
        protected LayoutControlItem layoutControlItem3;
        protected LayoutControlItem layoutControlItem4;
        protected LayoutControlItem layoutControlItem5;
        protected LayoutControlItem layoutControlItem6;
        protected LabelControl lcAbout;
        protected LabelControl lcCopyright;
        protected LayoutControlItem lciStart;
        protected PictureEdit peAwards;
        protected PictureEdit peLogo;
        protected PictureEdit peProduct;
        protected SimpleButton simpleButton1;
        protected SimpleSeparator simpleSeparator1;
        protected SimpleSeparator simpleSeparator2;
        protected SimpleLabelItem sliStart;
        protected VersionControl versionControl1;

        public ucAboutPage()
        {
            this.InitializeComponent();
            this.versionControl1.SetProduct(this.ProductKind);
            this.InitData();
            this.UpdateImages();
            UserLookAndFeel.Default.StyleChanged += new EventHandler(this.Default_StyleChanged);
            if (!this.ShowStartButton)
            {
                this.esiStart1.Visibility = this.esiStart2.Visibility = this.esiStart3.Visibility = this.lciStart.Visibility = this.sliStart.Visibility = LayoutVisibility.Never;
            }
            this.simpleButton1.Image = MainFormHelper.GetImage("ActiveDemo", ImageSize.Large32);
        }

        private void Default_StyleChanged(object sender, EventArgs e)
        {
            this.UpdateImages();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            if (disposing)
            {
                UserLookAndFeel.Default.StyleChanged -= new EventHandler(this.Default_StyleChanged);
            }
            base.Dispose(disposing);
        }

        public static string GetLogoImageName()
        {
            if (FrameHelper.IsDarkSkin(UserLookAndFeel.Default))
            {
                return "DevExpress.Utils.XtraFrames.dx-logo-light.png";
            }
            return "DevExpress.Utils.XtraFrames.dx-logo.png";
        }

        private void InitData()
        {
            this.peAwards.Image = ResourceImageHelper.CreateImageFromResources("DevExpress.Tutorials.Images.awards.png", typeof(ucAboutPage).Assembly);
            this.lcCopyright.Text = AboutHelper.CopyRight;
            this.lcAbout.Text = this.ProductText;
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ucAboutPage));
            this.layoutControl1 = new LayoutControl();
            this.simpleButton1 = new SimpleButton();
            this.peAwards = new PictureEdit();
            this.peLogo = new PictureEdit();
            this.lcCopyright = new LabelControl();
            this.versionControl1 = new VersionControl();
            this.peProduct = new PictureEdit();
            this.lcAbout = new LabelControl();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.layoutControlItem2 = new LayoutControlItem();
            this.simpleSeparator1 = new SimpleSeparator();
            this.emptySpaceItem4 = new EmptySpaceItem();
            this.emptySpaceItem5 = new EmptySpaceItem();
            this.emptySpaceItem1 = new EmptySpaceItem();
            this.layoutControlItem3 = new LayoutControlItem();
            this.layoutControlItem4 = new LayoutControlItem();
            this.layoutControlItem5 = new LayoutControlItem();
            this.emptySpaceItem3 = new EmptySpaceItem();
            this.emptySpaceItem6 = new EmptySpaceItem();
            this.simpleSeparator2 = new SimpleSeparator();
            this.emptySpaceItem7 = new EmptySpaceItem();
            this.layoutControlItem6 = new LayoutControlItem();
            this.layoutControlItem1 = new LayoutControlItem();
            this.lciStart = new LayoutControlItem();
            this.esiStart1 = new EmptySpaceItem();
            this.esiStart2 = new EmptySpaceItem();
            this.esiStart3 = new EmptySpaceItem();
            this.sliStart = new SimpleLabelItem();
            this.layoutControl1.BeginInit();
            this.layoutControl1.SuspendLayout();
            this.peAwards.Properties.BeginInit();
            this.peLogo.Properties.BeginInit();
            this.versionControl1.BeginInit();
            this.peProduct.Properties.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.layoutControlItem2.BeginInit();
            this.simpleSeparator1.BeginInit();
            this.emptySpaceItem4.BeginInit();
            this.emptySpaceItem5.BeginInit();
            this.emptySpaceItem1.BeginInit();
            this.layoutControlItem3.BeginInit();
            this.layoutControlItem4.BeginInit();
            this.layoutControlItem5.BeginInit();
            this.emptySpaceItem3.BeginInit();
            this.emptySpaceItem6.BeginInit();
            this.simpleSeparator2.BeginInit();
            this.emptySpaceItem7.BeginInit();
            this.layoutControlItem6.BeginInit();
            this.layoutControlItem1.BeginInit();
            this.lciStart.BeginInit();
            this.esiStart1.BeginInit();
            this.esiStart2.BeginInit();
            this.esiStart3.BeginInit();
            this.sliStart.BeginInit();
            base.SuspendLayout();
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.peAwards);
            this.layoutControl1.Controls.Add(this.peLogo);
            this.layoutControl1.Controls.Add(this.lcCopyright);
            this.layoutControl1.Controls.Add(this.versionControl1);
            this.layoutControl1.Controls.Add(this.peProduct);
            this.layoutControl1.Controls.Add(this.lcAbout);
            manager.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(0x719, 0x79, 0x5de, 0x2a9);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.simpleButton1.Appearance.Font = (Font) manager.GetObject("simpleButton1.Appearance.Font");
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Cursor = Cursors.Hand;
            manager.ApplyResources(this.simpleButton1, "simpleButton1");
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            manager.ApplyResources(this.peAwards, "peAwards");
            this.peAwards.Name = "peAwards";
            this.peAwards.Properties.AllowFocused = false;
            this.peAwards.Properties.Appearance.BackColor = (Color) manager.GetObject("peAwards.Properties.Appearance.BackColor");
            this.peAwards.Properties.Appearance.Options.UseBackColor = true;
            this.peAwards.Properties.BorderStyle = BorderStyles.NoBorder;
            this.peAwards.Properties.NullText = manager.GetString("peAwards.Properties.NullText");
            this.peAwards.Properties.ShowMenu = false;
            this.peAwards.Properties.SizeMode = PictureSizeMode.Squeeze;
            this.peAwards.StyleController = this.layoutControl1;
            manager.ApplyResources(this.peLogo, "peLogo");
            this.peLogo.Name = "peLogo";
            this.peLogo.Properties.AllowFocused = false;
            this.peLogo.Properties.Appearance.BackColor = (Color) manager.GetObject("peLogo.Properties.Appearance.BackColor");
            this.peLogo.Properties.Appearance.Options.UseBackColor = true;
            this.peLogo.Properties.BorderStyle = BorderStyles.NoBorder;
            this.peLogo.Properties.NullText = manager.GetString("peLogo.Properties.NullText");
            this.peLogo.Properties.PictureAlignment = ContentAlignment.MiddleLeft;
            this.peLogo.Properties.ShowMenu = false;
            this.peLogo.StyleController = this.layoutControl1;
            this.lcCopyright.Appearance.TextOptions.HAlignment = HorzAlignment.Far;
            manager.ApplyResources(this.lcCopyright, "lcCopyright");
            this.lcCopyright.Name = "lcCopyright";
            this.lcCopyright.StyleController = this.layoutControl1;
            this.versionControl1.BorderStyle = BorderStyles.NoBorder;
            manager.ApplyResources(this.versionControl1, "versionControl1");
            this.versionControl1.Name = "versionControl1";
            manager.ApplyResources(this.peProduct, "peProduct");
            this.peProduct.Name = "peProduct";
            this.peProduct.Properties.AllowFocused = false;
            this.peProduct.Properties.Appearance.BackColor = (Color) manager.GetObject("peProduct.Properties.Appearance.BackColor");
            this.peProduct.Properties.Appearance.Options.UseBackColor = true;
            this.peProduct.Properties.BorderStyle = BorderStyles.NoBorder;
            this.peProduct.Properties.NullText = manager.GetString("peProduct.Properties.NullText");
            this.peProduct.Properties.PictureAlignment = ContentAlignment.TopLeft;
            this.peProduct.Properties.ShowMenu = false;
            this.peProduct.StyleController = this.layoutControl1;
            this.lcAbout.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
            this.lcAbout.Appearance.TextOptions.Trimming = Trimming.EllipsisCharacter;
            this.lcAbout.Appearance.TextOptions.VAlignment = VertAlignment.Top;
            this.lcAbout.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            manager.ApplyResources(this.lcAbout, "lcAbout");
            this.lcAbout.Name = "lcAbout";
            this.lcAbout.StyleController = this.layoutControl1;
            manager.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { 
                this.layoutControlItem2, this.simpleSeparator1, this.emptySpaceItem4, this.emptySpaceItem5, this.emptySpaceItem1, this.layoutControlItem3, this.layoutControlItem4, this.layoutControlItem5, this.emptySpaceItem3, this.emptySpaceItem6, this.simpleSeparator2, this.emptySpaceItem7, this.layoutControlItem6, this.layoutControlItem1, this.lciStart, this.esiStart1,
                this.esiStart2, this.esiStart3, this.sliStart
            });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(30, 10, 10, 2);
            this.layoutControlGroup1.ShowInCustomizationForm = false;
            this.layoutControlGroup1.Size = new Size(0x429, 0x266);
            this.layoutControlGroup1.TextVisible = false;
            this.layoutControlItem2.Control = this.versionControl1;
            manager.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new Point(0x2ef, 70);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(0x112, 0xae);
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            this.simpleSeparator1.AllowHotTrack = false;
            manager.ApplyResources(this.simpleSeparator1, "simpleSeparator1");
            this.simpleSeparator1.Location = new Point(0x2e3, 70);
            this.simpleSeparator1.Name = "simpleSeparator1";
            this.simpleSeparator1.Size = new Size(2, 0xae);
            this.emptySpaceItem4.AllowHotTrack = false;
            manager.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new Point(0x2e5, 70);
            this.emptySpaceItem4.MaxSize = new Size(10, 0);
            this.emptySpaceItem4.MinSize = new Size(10, 10);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new Size(10, 0xae);
            this.emptySpaceItem4.SizeConstraintsType = SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new Size(0, 0);
            this.emptySpaceItem5.AllowHotTrack = false;
            manager.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new Point(0x2d9, 70);
            this.emptySpaceItem5.MaxSize = new Size(10, 0);
            this.emptySpaceItem5.MinSize = new Size(10, 10);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new Size(10, 0xae);
            this.emptySpaceItem5.SizeConstraintsType = SizeConstraintsType.Custom;
            this.emptySpaceItem5.TextSize = new Size(0, 0);
            this.emptySpaceItem1.AllowHotTrack = false;
            manager.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new Point(0x2d9, 0xf4);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new Size(0x128, 70);
            this.emptySpaceItem1.TextSize = new Size(0, 0);
            this.layoutControlItem3.Control = this.lcAbout;
            manager.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new Point(0, 70);
            this.layoutControlItem3.MinSize = new Size(14, 0x11);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new Size(0x2d9, 0xf4);
            this.layoutControlItem3.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            this.layoutControlItem4.Control = this.lcCopyright;
            this.layoutControlItem4.ControlAlignment = ContentAlignment.TopRight;
            manager.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new Point(0, 0x249);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new Size(0x401, 0x11);
            this.layoutControlItem4.TextSize = new Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.layoutControlItem5.Control = this.peLogo;
            manager.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new Point(0x2ef, 0x1b9);
            this.layoutControlItem5.MaxSize = new Size(0, 0x7e);
            this.layoutControlItem5.MinSize = new Size(0x18, 0x7e);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new Size(0x112, 0x7e);
            this.layoutControlItem5.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            this.emptySpaceItem3.AllowHotTrack = false;
            manager.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new Point(0x2d9, 0x1b9);
            this.emptySpaceItem3.MaxSize = new Size(10, 0);
            this.emptySpaceItem3.MinSize = new Size(10, 10);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new Size(10, 0x7e);
            this.emptySpaceItem3.SizeConstraintsType = SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new Size(0, 0);
            this.emptySpaceItem6.AllowHotTrack = false;
            manager.ApplyResources(this.emptySpaceItem6, "emptySpaceItem6");
            this.emptySpaceItem6.Location = new Point(0x2e5, 0x1b9);
            this.emptySpaceItem6.MaxSize = new Size(10, 0);
            this.emptySpaceItem6.MinSize = new Size(10, 10);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new Size(10, 0x7e);
            this.emptySpaceItem6.SizeConstraintsType = SizeConstraintsType.Custom;
            this.emptySpaceItem6.TextSize = new Size(0, 0);
            this.simpleSeparator2.AllowHotTrack = false;
            manager.ApplyResources(this.simpleSeparator2, "simpleSeparator2");
            this.simpleSeparator2.Location = new Point(0x2e3, 0x1b9);
            this.simpleSeparator2.Name = "simpleSeparator2";
            this.simpleSeparator2.Size = new Size(2, 0x7e);
            this.emptySpaceItem7.AllowHotTrack = false;
            manager.ApplyResources(this.emptySpaceItem7, "emptySpaceItem7");
            this.emptySpaceItem7.Location = new Point(0, 0x237);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new Size(0x401, 0x12);
            this.emptySpaceItem7.TextSize = new Size(0, 0);
            this.layoutControlItem6.Control = this.peAwards;
            manager.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new Point(0, 0x1b9);
            this.layoutControlItem6.MaxSize = new Size(0, 0x7e);
            this.layoutControlItem6.MinSize = new Size(0x18, 0x7e);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Size = new Size(0x2d9, 0x7e);
            this.layoutControlItem6.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            this.layoutControlItem1.Control = this.peProduct;
            manager.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new Point(0, 0);
            this.layoutControlItem1.MaxSize = new Size(0, 70);
            this.layoutControlItem1.MinSize = new Size(0x18, 70);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(0x401, 70);
            this.layoutControlItem1.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.lciStart.Control = this.simpleButton1;
            manager.ApplyResources(this.lciStart, "lciStart");
            this.lciStart.Location = new Point(0x197, 0x13a);
            this.lciStart.Name = "lciStart";
            this.lciStart.Size = new Size(0xce, 0x1a);
            this.lciStart.TextSize = new Size(0, 0);
            this.lciStart.TextToControlDistance = 0;
            this.lciStart.TextVisible = false;
            this.esiStart1.AllowHotTrack = false;
            manager.ApplyResources(this.esiStart1, "esiStart1");
            this.esiStart1.Location = new Point(0, 0x13a);
            this.esiStart1.Name = "esiStart1";
            this.esiStart1.Size = new Size(0x197, 0x1a);
            this.esiStart1.TextSize = new Size(0, 0);
            this.esiStart2.AllowHotTrack = false;
            manager.ApplyResources(this.esiStart2, "esiStart2");
            this.esiStart2.Location = new Point(0x265, 0x13a);
            this.esiStart2.Name = "esiStart2";
            this.esiStart2.Size = new Size(0x19c, 0x1a);
            this.esiStart2.TextSize = new Size(0, 0);
            this.esiStart3.AllowHotTrack = false;
            manager.ApplyResources(this.esiStart3, "esiStart3");
            this.esiStart3.Location = new Point(0, 0x165);
            this.esiStart3.Name = "esiStart3";
            this.esiStart3.Size = new Size(0x401, 0x54);
            this.esiStart3.TextSize = new Size(0, 0);
            this.sliStart.AllowHotTrack = false;
            this.sliStart.AppearanceItemCaption.Options.UseTextOptions = true;
            this.sliStart.AppearanceItemCaption.TextOptions.HAlignment = HorzAlignment.Center;
            manager.ApplyResources(this.sliStart, "sliStart");
            this.sliStart.Location = new Point(0, 340);
            this.sliStart.Name = "sliStart";
            this.sliStart.Size = new Size(0x401, 0x11);
            this.sliStart.TextSize = new Size(0xb7, 13);
            manager.ApplyResources(this, "$this");
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.layoutControl1);
            base.Name = "ucAboutPage";
            this.layoutControl1.EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.peAwards.Properties.EndInit();
            this.peLogo.Properties.EndInit();
            this.versionControl1.EndInit();
            this.peProduct.Properties.EndInit();
            this.layoutControlGroup1.EndInit();
            this.layoutControlItem2.EndInit();
            this.simpleSeparator1.EndInit();
            this.emptySpaceItem4.EndInit();
            this.emptySpaceItem5.EndInit();
            this.emptySpaceItem1.EndInit();
            this.layoutControlItem3.EndInit();
            this.layoutControlItem4.EndInit();
            this.layoutControlItem5.EndInit();
            this.emptySpaceItem3.EndInit();
            this.emptySpaceItem6.EndInit();
            this.simpleSeparator2.EndInit();
            this.emptySpaceItem7.EndInit();
            this.layoutControlItem6.EndInit();
            this.layoutControlItem1.EndInit();
            this.lciStart.EndInit();
            this.esiStart1.EndInit();
            this.esiStart2.EndInit();
            this.esiStart3.EndInit();
            this.sliStart.EndInit();
            base.ResumeLayout(false);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            RibbonMainForm form = base.FindForm() as RibbonMainForm;
            if (form != null)
            {
                form.StartDemo();
            }
        }

        private void UpdateImages()
        {
            this.peLogo.Image = ucOverviewPage.GetSVGLogoImage();
            Image productImage = FrameHelper.IsDarkSkin(UserLookAndFeel.Default) ? this.ProductImageLight : this.ProductImage;
            if (FrameHelper.IsDarkSkin(UserLookAndFeel.Default) && (productImage == null))
            {
                productImage = this.ProductImage;
            }
            this.peProduct.Image = productImage;
        }

        protected virtual Image ProductImage
        {
            get
            {
                return null;
            }
        }

        protected virtual Image ProductImageLight
        {
            get
            {
                return null;
            }
        }

        protected virtual DevExpress.Utils.About.ProductKind ProductKind
        {
            get
            {
                return DevExpress.Utils.About.ProductKind.Default;
            }
        }

        protected virtual string ProductText
        {
            get
            {
                return string.Empty;
            }
        }

        protected internal override bool ShowCaption
        {
            get
            {
                return false;
            }
        }

        protected virtual bool ShowStartButton
        {
            get
            {
                return true;
            }
        }
    }
}

