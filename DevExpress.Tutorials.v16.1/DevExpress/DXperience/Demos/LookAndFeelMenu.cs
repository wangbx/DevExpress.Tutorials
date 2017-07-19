namespace DevExpress.DXperience.Demos
{
    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.Tutorials.Properties;
    using DevExpress.Utils;
    using DevExpress.Utils.About;
    using DevExpress.Utils.Controls;
    using DevExpress.Utils.WXPaint;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Helpers;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraEditors;
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class LookAndFeelMenu : IDisposable
    {
        protected string about;
        private DefaultLookAndFeel lookAndFeel;
        protected BarManager manager;
        public ButtonBarItem miAboutProduct;
        protected CheckBarItem miAllowFormSkins;
        protected ButtonBarItem miFeatures;
        protected CheckBarItem miFullViewMode;
        protected BarSubItem miHelp;
        protected BarSubItem miLookAndFeel;
        protected ButtonBarItem miRate;
        protected CheckBarItem miShowNavBarFilter;
        protected BarButtonItem miSkin;
        protected BarSubItem miView;
        private GalleryDropDown skinGallery;

        public event EventHandler BeginSkinChanging;

        public event EventHandler EndSkinChanging;

        public LookAndFeelMenu(BarManager manager, DefaultLookAndFeel lookAndFeel, string about)
        {
            if ((manager != null) && (manager.Form != null))
            {
                this.about = about;
                this.lookAndFeel = lookAndFeel;
                this.manager = manager;
                this.manager.Images = ImageHelper.CreateImageCollectionFromResources("DevExpress.Tutorials.MainDemo.menu.bmp", typeof(DevExpress.DXperience.Demos.LookAndFeelMenu).Assembly, new Size(0x10, 0x10), Color.Magenta);
                this.manager.ForceInitialize();
                this.manager.BeginUpdate();
                this.SetupMenu();
                this.manager.EndUpdate();
            }
        }

        protected virtual void AddItems()
        {
            if (this.MainMenu != null)
            {
                this.MainMenu.ItemLinks.Add(this.miLookAndFeel);
                this.MainMenu.ItemLinks.Add(this.miView);
                this.MainMenu.ItemLinks.Add(this.miHelp);
                this.InitLookAndFeelMenu();
            }
        }

        protected void AddOptionsMenu(BarSubItem miItem, object options, ItemClickEventHandler handler)
        {
            AddOptionsMenu(miItem, options, handler, this.manager);
        }

        public static void AddOptionsMenu(BarSubItem miItem, object options, ItemClickEventHandler handler, BarManager manager)
        {
            miItem.Visibility = (options != null) ? BarItemVisibility.Always : BarItemVisibility.Never;
            if (options != null)
            {
                ArrayList optionNames = SetOptions.GetOptionNames(options);
                for (int i = 0; i < optionNames.Count; i++)
                {
                    miItem.ItemLinks.Add(new OptionBarItem(manager, ResourcesKeeper.GetTitle(optionNames[i]), handler, optionNames[i]));
                }
                InitOptionsMenu(miItem, options);
            }
        }

        private bool AvailableStyle(LookAndFeelStyle style)
        {
            return (((this.lookAndFeel.LookAndFeel.Style == style) && !this.UsingXP) && !this.Mixed);
        }

        private void biDeveloperExpress_Click(object sender, ItemClickEventArgs e)
        {
            Process.Start("http://www.devexpress.com");
        }

        private void biDownloads_Click(object sender, ItemClickEventArgs e)
        {
            Process.Start("http://www.devexpress.com/downloads");
        }

        private void biForum_Click(object sender, ItemClickEventArgs e)
        {
            Process.Start("Https://go.devexpress.com/Demo_2013_GetSupport.aspx");
        }

        private void biKnowledgeBase_Click(object sender, ItemClickEventArgs e)
        {
            Process.Start("http://www.devexpress.com/Support/KnowledgeBase/");
        }

        private void biMyDevExpress_Click(object sender, ItemClickEventArgs e)
        {
            Process.Start(RibbonMainForm.GetStartedLink);
        }

        private void biProducts_Click(object sender, ItemClickEventArgs e)
        {
            Process.Start("http://www.devexpress.com/products");
        }

        protected virtual void biProductWebPage_Click(object sender, ItemClickEventArgs e)
        {
        }

        private void biRateDemo_Click(object sender, ItemClickEventArgs e)
        {
            Form menuForm = this.MenuForm;
            if (menuForm != null)
            {
                new RatingForm(menuForm).ShowDialog(menuForm);
            }
        }

        protected void ClearOptionItems()
        {
            ClearOptionItems(this.manager);
        }

        public static void ClearOptionItems(BarManager manager)
        {
            for (int i = manager.Items.Count - 1; i >= 0; i--)
            {
                OptionBarItem item = manager.Items[i] as OptionBarItem;
                if ((item != null) && item.IsOption)
                {
                    item.Dispose();
                }
            }
        }

        public virtual void Dispose()
        {
            if (this.MenuForm != null)
            {
                this.MenuForm.Disposed -= new EventHandler(this.form_Dispose);
            }
            this.lookAndFeel = null;
            this.miLookAndFeel = null;
        }

        public void EnabledLookFeelMenu(bool enable)
        {
            this.miLookAndFeel.Enabled = enable;
        }

        private void form_Dispose(object sender, EventArgs e)
        {
            this.miView.Popup -= new EventHandler(this.OnView);
            this.Dispose();
        }

        protected virtual void InitLookAndFeelEnabled()
        {
            if (this.lookAndFeel != null)
            {
                foreach (BarItemLink link in this.miLookAndFeel.ItemLinks)
                {
                    CheckBarItemWithStyle item = link.Item as CheckBarItemWithStyle;
                    if ((item != null) && (item.LookAndFeelStyle == LookAndFeelStyle.Skin))
                    {
                        item.Enabled = Painter.ThemesEnabled;
                    }
                }
            }
        }

        public void InitLookAndFeelMenu()
        {
            this.InitLookAndFeelMenu(this.LookAndFeel);
        }

        public void InitLookAndFeelMenu(DefaultLookAndFeel lookAndFeel)
        {
            this.lookAndFeel = lookAndFeel;
            this.miLookAndFeel.Visibility = (lookAndFeel != null) ? BarItemVisibility.Always : BarItemVisibility.Never;
            this.InitLookAndFeelEnabled();
        }

        public static void InitOptionsMenu(BarSubItem miItem, object options)
        {
            for (int i = 0; i < miItem.ItemLinks.Count; i++)
            {
                OptionBarItem item = miItem.ItemLinks[i].Item as OptionBarItem;
                if (item != null)
                {
                    item.Checked = SetOptions.OptionValueByString(item.Tag.ToString(), options);
                }
            }
        }

        protected void miAbout_Click(object sender, ItemClickEventArgs e)
        {
            frmAbout about = new frmAbout((this.about == "") ? "DXperience by Developer Express inc." : this.about);
            about.ShowDialog();
            about.Dispose();
        }

        protected virtual void miAboutProduct_Click(object sender, ItemClickEventArgs e)
        {
        }

        protected virtual void miFeatures_Click(object sender, ItemClickEventArgs e)
        {
            ITutorialForm form = this.manager.Form as ITutorialForm;
            if (form != null)
            {
                form.ShowModule(ModulesInfo.GetItem(0).Name);
                form.ResetNavbarSelectedLink();
            }
        }

        private void OnDemoFilterClick(object sender, EventArgs e)
        {
            if (this.MainForm != null)
            {
                this.MainForm.ShowDemoFilter();
            }
        }

        private void OnFullViewModeClick(object sender, EventArgs e)
        {
            if (this.MainForm != null)
            {
                if (this.MainForm.IsFullMode)
                {
                    this.MainForm.ShowServiceElements();
                }
                else
                {
                    this.MainForm.HideServiceElements();
                }
            }
        }

        private void OnPopupLookAndFeel(object sender, EventArgs e)
        {
            BarSubItem item = sender as BarSubItem;
            if ((item != null) && (this.LookAndFeel != null))
            {
                foreach (BarItemLink link in item.ItemLinks)
                {
                    CheckBarItemWithStyle style = link.Item as CheckBarItemWithStyle;
                    if (style != null)
                    {
                        if (style.LookAndFeelStyle == LookAndFeelStyle.Skin)
                        {
                            style.Checked = this.UsingXP && !this.Mixed;
                        }
                        else
                        {
                            style.Checked = this.AvailableStyle(style.LookAndFeelStyle);
                        }
                    }
                }
                this.miAllowFormSkins.Checked = SkinManager.AllowFormSkins;
            }
        }

        private void OnPopupSkinNames(object sender, EventArgs e)
        {
            BarSubItem item = sender as BarSubItem;
            if ((item != null) && (this.LookAndFeel != null))
            {
                foreach (BarItemLink link in item.ItemLinks)
                {
                    CheckBarItem item2 = link.Item as CheckBarItem;
                    if (item2 != null)
                    {
                        item2.Checked = this.AvailableStyle(LookAndFeelStyle.Skin) && (this.LookAndFeel.LookAndFeel.SkinName == link.Caption);
                    }
                }
            }
        }

        protected virtual void OnSwitchFormSkinStyle_Click(object sender, ItemClickEventArgs e)
        {
            this.RaiseBeginSkinChanging();
            if (SkinManager.AllowFormSkins)
            {
                SkinManager.DisableFormSkins();
            }
            else
            {
                SkinManager.EnableFormSkins();
            }
            LookAndFeelHelper.ForceDefaultLookAndFeelChanged();
            this.RaiseEndSkinChanging();
        }

        private void OnSwitchSkin(object sender, ItemClickEventArgs e)
        {
            this.OnSwitchStyle_Click(sender, e);
            if (this.LookAndFeel != null)
            {
                this.RaiseBeginSkinChanging();
                this.LookAndFeel.LookAndFeel.SetSkinStyle(((CheckBarItem) e.Item).Caption);
                this.RaiseEndSkinChanging();
            }
        }

        protected virtual void OnSwitchStyle_Click(object sender, ItemClickEventArgs e)
        {
            this.MenuForm.SuspendLayout();
            this.RaiseBeginSkinChanging();
            try
            {
                Application.DoEvents();
                CheckBarItem item = e.Item as CheckBarItem;
                if ((item != null) && (this.LookAndFeel != null))
                {
                    bool useWindowsXPTheme = item.Style == ActiveLookAndFeelStyle.WindowsXP;
                    if (item.Style != ActiveLookAndFeelStyle.Skin)
                    {
                        this.LookAndFeel.LookAndFeel.SetStyle((LookAndFeelStyle) item.Style, useWindowsXPTheme, this.LookAndFeel.LookAndFeel.UseDefaultLookAndFeel, this.LookAndFeel.LookAndFeel.SkinName);
                    }
                    this.InitLookAndFeelMenu();
                    Application.DoEvents();
                }
            }
            finally
            {
                this.RaiseEndSkinChanging();
                this.MenuForm.ResumeLayout(true);
            }
        }

        private void OnView(object sender, EventArgs e)
        {
            this.miFullViewMode.Checked = (this.MainForm != null) && this.MainForm.IsFullMode;
            this.miShowNavBarFilter.Checked = this.MainForm.IsDemoFilterVisible;
            this.miShowNavBarFilter.Enabled = !this.MainForm.IsFullMode;
        }

        protected void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show(Resources.OpenFileQuestion, Resources.ExportCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    new Process { StartInfo = { 
                        FileName = fileName,
                        Verb = "Open",
                        WindowStyle = ProcessWindowStyle.Normal
                    } }.Start();
                }
                catch
                {
                    XtraMessageBox.Show(Resources.ApplicationOpenWarning, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void RaiseBeginSkinChanging()
        {
            if (this.BeginSkinChanging != null)
            {
                this.BeginSkinChanging(this, EventArgs.Empty);
            }
        }

        private void RaiseEndSkinChanging()
        {
            if (this.EndSkinChanging != null)
            {
                this.EndSkinChanging(this, EventArgs.Empty);
            }
        }

        internal void SetTutorialsMenu()
        {
            this.miRate.Visibility = BarItemVisibility.Never;
            this.miFeatures.Visibility = BarItemVisibility.Never;
            this.miAboutProduct.ItemShortcut = new BarShortcut(Shortcut.F1);
        }

        private void SetupMenu()
        {
            this.skinGallery = new GalleryDropDown();
            SkinHelper.InitSkinGalleryDropDown(this.skinGallery);
            this.skinGallery.Manager = this.manager;
            this.miLookAndFeel = new BarSubItem(this.manager, "&Look and Feel");
            this.miAllowFormSkins = new CheckBarItem(this.manager, "Allow Form Skins", new ItemClickEventHandler(this.OnSwitchFormSkinStyle_Click));
            this.miLookAndFeel.ItemLinks.Add(this.miAllowFormSkins);
            this.miLookAndFeel.ItemLinks.Add(new CheckBarItemWithStyle(this.manager, "&Native", new ItemClickEventHandler(this.OnSwitchStyle_Click), ActiveLookAndFeelStyle.WindowsXP, LookAndFeelStyle.Skin));
            this.miLookAndFeel.ItemLinks.Add(new CheckBarItemWithStyle(this.manager, "&Flat", new ItemClickEventHandler(this.OnSwitchStyle_Click), ActiveLookAndFeelStyle.Flat, LookAndFeelStyle.Flat));
            this.miLookAndFeel.ItemLinks.Add(new CheckBarItemWithStyle(this.manager, "&Ultra Flat", new ItemClickEventHandler(this.OnSwitchStyle_Click), ActiveLookAndFeelStyle.UltraFlat, LookAndFeelStyle.UltraFlat));
            this.miLookAndFeel.ItemLinks.Add(new CheckBarItemWithStyle(this.manager, "&Style3D", new ItemClickEventHandler(this.OnSwitchStyle_Click), ActiveLookAndFeelStyle.Style3D, LookAndFeelStyle.Style3D));
            this.miLookAndFeel.ItemLinks.Add(new CheckBarItemWithStyle(this.manager, "&Office2003", new ItemClickEventHandler(this.OnSwitchStyle_Click), ActiveLookAndFeelStyle.Office2003, LookAndFeelStyle.Office2003));
            this.miSkin = new BarButtonItem(this.manager, "S&kins");
            this.miSkin.ButtonStyle = BarButtonStyle.DropDown;
            this.miSkin.DropDownControl = this.skinGallery;
            this.miSkin.ActAsDropDown = true;
            this.miView = new BarSubItem(this.manager, "&View");
            this.miHelp = new BarSubItem(this.manager, "&Help");
            this.miFullViewMode = new CheckBarItem(this.manager, "Full-Window Mode", new ItemClickEventHandler(this.OnFullViewModeClick));
            this.miFullViewMode.ItemShortcut = new BarShortcut(Shortcut.F11);
            this.miShowNavBarFilter = new CheckBarItem(this.manager, "Show Demo Filter", new ItemClickEventHandler(this.OnDemoFilterClick));
            this.miShowNavBarFilter.ItemShortcut = new BarShortcut(Shortcut.F3);
            this.miView.ItemLinks.Add(this.miFullViewMode);
            if ((this.MainForm != null) && this.MainForm.AllowDemoFilter)
            {
                this.miView.ItemLinks.Add(this.miShowNavBarFilter);
            }
            this.miLookAndFeel.Popup += new EventHandler(this.OnPopupLookAndFeel);
            this.miView.Popup += new EventHandler(this.OnView);
            this.miLookAndFeel.ItemLinks.Insert(1, this.miSkin).BeginGroup = true;
            this.miRate = new ButtonBarItem(this.manager, "&Rate this demo...", 3, new ItemClickEventHandler(this.biRateDemo_Click));
            this.miHelp.ItemLinks.Add(new ButtonBarItem(this.manager, string.Format("{0} Web Page", this.ProductName), 0, new ItemClickEventHandler(this.biProductWebPage_Click)));
            this.miFeatures = new ButtonBarItem(this.manager, string.Format("{0} Features", this.ProductName), new ItemClickEventHandler(this.miFeatures_Click));
            this.miFeatures.ItemShortcut = new BarShortcut(Shortcut.F1);
            this.miHelp.ItemLinks.Add(this.miFeatures).BeginGroup = true;
            this.miAboutProduct = new ButtonBarItem(this.manager, string.Format("About {0}", this.ProductName), new ItemClickEventHandler(this.miAboutProduct_Click));
            this.miHelp.ItemLinks.Add(this.miAboutProduct);
            if (this.MenuForm != null)
            {
                this.MenuForm.Disposed += new EventHandler(this.form_Dispose);
            }
            this.AddItems();
        }

        protected string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            string productName = Application.ProductName;
            int startIndex = productName.LastIndexOf(".") + 1;
            if (startIndex > 0)
            {
                productName = productName.Substring(startIndex, productName.Length - startIndex);
            }
            dialog.Title = string.Format(Resources.ExportTo, title);
            dialog.FileName = productName;
            dialog.Filter = filter;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.FileName;
            }
            return "";
        }

        public string About
        {
            get
            {
                return this.about;
            }
            set
            {
                this.about = value;
            }
        }

        public DefaultLookAndFeel LookAndFeel
        {
            get
            {
                return this.lookAndFeel;
            }
        }

        private ITutorialForm MainForm
        {
            get
            {
                return (this.MenuForm as ITutorialForm);
            }
        }

        public Bar MainMenu
        {
            get
            {
                if (this.manager != null)
                {
                    return this.manager.Bars["Main Menu"];
                }
                return null;
            }
        }

        public Form MenuForm
        {
            get
            {
                if (this.manager != null)
                {
                    return (this.manager.Form as Form);
                }
                return null;
            }
        }

        protected virtual bool Mixed
        {
            get
            {
                return false;
            }
        }

        protected virtual string ProductName
        {
            get
            {
                return "Product";
            }
        }

        private bool UsingXP
        {
            get
            {
                return (this.LookAndFeel.LookAndFeel.UseWindowsXPTheme && Painter.ThemesEnabled);
            }
        }
    }
}

