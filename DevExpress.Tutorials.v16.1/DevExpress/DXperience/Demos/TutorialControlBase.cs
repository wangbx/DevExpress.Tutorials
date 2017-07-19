namespace DevExpress.DXperience.Demos
{
    using DevExpress.Tutorials;
    using DevExpress.Tutorials.Properties;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraSplashScreen;
    using DevExpress.XtraWaitForm;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class TutorialControlBase : ModuleBase
    {
        protected ArrayList BarInfos = new ArrayList();
        private ApplicationCaption fCaption;
        private string fName = string.Empty;
        private DevExpress.DXperience.Demos.RibbonMenuManager manager;
        protected bool start = true;

        public TutorialControlBase()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.LookAndFeel.StyleChanged += new EventHandler(this.LookAndFeel_StyleChanged);
        }

        protected virtual void AllowExport()
        {
            this.EnabledPrintExportActions(false, ExportFormats.None, true);
        }

        private void CreateBar()
        {
            DevExpress.XtraBars.Bar bar = new DevExpress.XtraBars.Bar(this.Manager);
            bar.BarName = bar.Text = this.BarName;
            bar.DockStyle = BarDockStyle.Top;
            foreach (BarInfo info in this.BarInfos)
            {
                BarItem item = info.CreateItem(this.Manager);
                bar.AddItem(item).BeginGroup = info.BeginGroup;
            }
        }

        protected void CreateTimer()
        {
            if (this.start)
            {
                this.start = false;
                Timer timer = new Timer {
                    Interval = 500
                };
                timer.Tick += new EventHandler(this.OnTick);
                timer.Start();
            }
        }

        public void CreateWaitDialog()
        {
            SplashScreenManager.ShowForm(null, typeof(DemoWaitForm), false, true, false, ParentFormState.Unlocked);
            SplashScreenManager manager = SplashScreenManager.Default;
            if (manager != null)
            {
                try
                {
                    manager.SetWaitFormCaption(Resources.WaitCaption);
                    manager.SetWaitFormDescription(Resources.WaitDescription);
                }
                catch
                {
                }
            }
        }

        protected virtual void DoHide()
        {
            if ((this.AutoMergeRibbon && (this.ParentFormMain != null)) && (this.ParentFormMain.RibbonControl.MergedRibbon == this.ChildRibbon))
            {
                this.ParentFormMain.UnMergeRibbon();
            }
        }

        protected virtual void DoShow()
        {
            this.AllowExport();
            if ((this.AutoMergeRibbon && (this.ChildRibbon != null)) && (this.ParentFormMain != null))
            {
                this.ParentFormMain.MergeRibbon(this.ChildRibbon);
            }
            if (this.ParentFormMain != null)
            {
                this.SetExportBarItemAvailability(this.ParentFormMain.ShowInVisualStudio, false, false);
            }
        }

        protected internal void EnabledPrintExportActions(bool allowPrintPreview, ExportFormats formats, bool showDisabledButtons)
        {
            this.EnabledPrintExportActions(allowPrintPreview, false, formats, showDisabledButtons);
        }

        public void EnabledPrintExportActions(bool allowPrintPreview, bool allowPrint, ExportFormats formats, bool showDisabledButtons)
        {
            if (this.ParentFormMain != null)
            {
                this.ParentFormMain.PrintPreviewButton.Enabled = allowPrintPreview;
                this.ParentFormMain.PrintButton.Visibility = this.GetVisibility(allowPrint);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToPDFButton, (formats & ExportFormats.PDF) == ExportFormats.PDF, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToEPUBButton, (formats & ExportFormats.EPUB) == ExportFormats.EPUB, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToXMLButton, (formats & ExportFormats.XML) == ExportFormats.XML, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToHTMLButton, (formats & ExportFormats.HTML) == ExportFormats.HTML, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToMHTButton, (formats & ExportFormats.MHT) == ExportFormats.MHT, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToDOCButton, (formats & ExportFormats.DOC) == ExportFormats.DOC, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToDOCXButton, (formats & ExportFormats.DOCX) == ExportFormats.DOCX, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToXLSButton, (formats & ExportFormats.XLS) == ExportFormats.XLS, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToXLSXButton, (formats & ExportFormats.XLSX) == ExportFormats.XLSX, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToRTFButton, (formats & ExportFormats.RTF) == ExportFormats.RTF, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToODTButton, (formats & ExportFormats.ODT) == ExportFormats.ODT, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToImageButton, (formats & ExportFormats.Image) == ExportFormats.Image, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToImageExButton, (formats & ExportFormats.ImageEx) == ExportFormats.ImageEx, showDisabledButtons);
                this.SetExportBarItemAvailability(this.ParentFormMain.ExportToTextButton, (formats & ExportFormats.Text) == ExportFormats.Text, showDisabledButtons);
                this.ParentFormMain.ExportButton.Enabled = formats != ExportFormats.None;
            }
        }

        public override void EndWhatsThis()
        {
            if (this.SetNewWhatsThisPadding)
            {
                base.Padding = new Padding(0);
            }
        }

        protected internal virtual void ExportTo(string ext, string filter)
        {
            string fileName = MainFormHelper.GetFileName(string.Format("*.{0}", ext), filter);
            if (!string.IsNullOrEmpty(fileName))
            {
                try
                {
                    this.ExportToCore(fileName, ext);
                    MainFormHelper.OpenExportedFile(fileName);
                }
                catch (Exception exception)
                {
                    MainFormHelper.ShowExportErrorMessage(exception);
                }
            }
        }

        protected internal virtual void ExportToCore(string filename, string ext)
        {
        }

        protected internal virtual void ExportToDOC()
        {
        }

        protected internal virtual void ExportToDOCX()
        {
        }

        protected internal virtual void ExportToEPUB()
        {
        }

        protected internal virtual void ExportToHTML()
        {
        }

        protected internal virtual void ExportToImage()
        {
        }

        protected internal virtual void ExportToMHT()
        {
        }

        protected internal virtual void ExportToODT()
        {
        }

        protected internal virtual void ExportToPDF()
        {
        }

        protected internal virtual void ExportToRTF()
        {
        }

        protected internal virtual void ExportToText()
        {
        }

        protected internal virtual void ExportToXLS()
        {
        }

        protected internal virtual void ExportToXLSX()
        {
        }

        protected internal virtual void ExportToXML()
        {
        }

        private RibbonControl FindRibbon(Control.ControlCollection controls)
        {
            RibbonControl control = controls.OfType<Control>().FirstOrDefault<Control>(x => (x is RibbonControl)) as RibbonControl;
            if (control != null)
            {
                return control;
            }
            foreach (Control control2 in controls)
            {
                if (control2.HasChildren)
                {
                    control = this.FindRibbon(control2.Controls);
                    if (control != null)
                    {
                        return control;
                    }
                }
            }
            return null;
        }

        protected internal virtual void GenerateReport()
        {
        }

        protected BarItem GetBarItem(int index)
        {
            this.InitBars();
            if (this.BarInfos.Count <= index)
            {
                return null;
            }
            return ((BarInfo) this.BarInfos[index]).BarItem;
        }

        protected bool GetBarItemPushed(int index)
        {
            this.InitBars();
            if (this.BarInfos.Count <= index)
            {
                return false;
            }
            return ((BarInfo) this.BarInfos[index]).Pushed;
        }

        protected internal virtual BarItemVisibility GetVisibility(bool isVisible)
        {
            if (!isVisible)
            {
                return BarItemVisibility.Never;
            }
            return BarItemVisibility.Always;
        }

        private void HideBar()
        {
            if ((this.BarName != string.Empty) && (this.Manager != null))
            {
                this.Bar.Visible = false;
            }
        }

        protected virtual void InitBarInfo()
        {
        }

        private void InitBars()
        {
            if (this.BarInfos.Count == 0)
            {
                this.InitBarInfo();
            }
            if ((this.Manager != null) && (this.Bar == null))
            {
                this.CreateBar();
            }
        }

        private void LookAndFeel_StyleChanged(object sender, EventArgs e)
        {
            this.OnStyleChanged();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (SplashScreenManager.Default != null)
            {
                SplashScreenManager.CloseForm(false);
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if ((this.NoGap && (base.Parent != null)) && (base.Parent.Parent != null))
            {
                base.Parent.Parent.Padding = new Padding(0);
            }
        }

        protected virtual void OnSetCaption(string fCaption)
        {
            this.Caption.Text = string.Format("{0}", this.TutorialName);
        }

        protected virtual void OnStyleChanged()
        {
        }

        protected virtual void OnSwitchStyle()
        {
        }

        protected virtual void OnTick()
        {
        }

        private void OnTick(object sender, EventArgs e)
        {
            ((Timer) sender).Stop();
            this.OnTick();
        }

        protected override void OnVisibleChanged(object sender, EventArgs e)
        {
            base.OnVisibleChanged(sender, e);
            if (base.Visible)
            {
                this.ShowBar();
                this.DoShow();
            }
            else
            {
                this.DoHide();
                this.HideBar();
            }
        }

        protected internal virtual void Print()
        {
        }

        protected internal virtual void PrintPreview()
        {
        }

        public virtual void RunActiveDemo()
        {
        }

        protected void SetBarItemChecked(int index, bool pushed)
        {
            this.InitBars();
            if (this.BarInfos.Count > index)
            {
                ((BarInfo) this.BarInfos[index]).Pushed = pushed;
            }
        }

        protected void SetBarItemEnabled(int index, bool enabled)
        {
            this.InitBars();
            if (this.BarInfos.Count > index)
            {
                ((BarInfo) this.BarInfos[index]).Enabled = enabled;
            }
        }

        protected internal virtual void SetExportBarItemAvailability(BarItem button, bool isEnabled, bool showDisabled)
        {
            button.Enabled = isEnabled;
            button.Visibility = this.GetVisibility(isEnabled || showDisabled);
        }

        public void SetWaitDialogCaption(string description)
        {
            SplashScreenManager manager = SplashScreenManager.Default;
            if (manager != null)
            {
                manager.SetWaitFormDescription(description);
            }
        }

        private void ShowBar()
        {
            if ((this.BarName != string.Empty) && (this.Manager != null))
            {
                this.InitBars();
                this.Bar.Visible = true;
            }
        }

        public override void StartWhatsThis()
        {
            if (this.SetNewWhatsThisPadding)
            {
                base.Padding = new Padding(8);
                this.Refresh();
            }
        }

        protected internal virtual bool AllowAppearanceGroup
        {
            get
            {
                return true;
            }
        }

        public virtual bool AllowGenerateReport
        {
            get
            {
                return false;
            }
        }

        public virtual bool AllowPrintOptions
        {
            get
            {
                return false;
            }
        }

        [DefaultValue(false)]
        public virtual bool AutoMergeRibbon { get; set; }

        private DevExpress.XtraBars.Bar Bar
        {
            get
            {
                foreach (DevExpress.XtraBars.Bar bar in this.Manager.Bars)
                {
                    if (bar.BarName == this.BarName)
                    {
                        return bar;
                    }
                }
                return null;
            }
        }

        protected virtual string BarName
        {
            get
            {
                return string.Empty;
            }
        }

        public ApplicationCaption Caption
        {
            get
            {
                return this.fCaption;
            }
            set
            {
                this.fCaption = value;
                this.OnSetCaption("");
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        protected virtual RibbonControl ChildRibbon
        {
            get
            {
                if (!this.AutoMergeRibbon)
                {
                    return null;
                }
                return this.FindRibbon(base.Controls);
            }
        }

        internal string FullTypeName
        {
            get
            {
                return base.GetType().FullName;
            }
        }

        public virtual bool HasActiveDemo
        {
            get
            {
                return false;
            }
        }

        protected virtual BarManager Manager
        {
            get
            {
                return null;
            }
        }

        public virtual bool NoGap
        {
            get
            {
                return false;
            }
        }

        public RibbonMainForm ParentFormMain
        {
            get
            {
                return (base.FindForm() as RibbonMainForm);
            }
        }

        public virtual DevExpress.DXperience.Demos.RibbonMenuManager RibbonMenuManager
        {
            get
            {
                return this.manager;
            }
            set
            {
                this.manager = value;
                if (this.manager != null)
                {
                    base.AddMenuManager(this.manager.Manager);
                }
            }
        }

        public virtual bool SetNewWhatsThisPadding
        {
            get
            {
                return true;
            }
        }

        protected internal virtual bool ShowCaption
        {
            get
            {
                return true;
            }
        }

        public string TutorialName
        {
            get
            {
                return this.fName;
            }
            set
            {
                this.fName = value;
            }
        }
    }
}

