namespace DevExpress.Tutorials
{
    using DevExpress.LookAndFeel;
    using DevExpress.Skins;
    using DevExpress.Tutorials.Properties;
    using DevExpress.Utils;
    using DevExpress.Utils.About;
    using DevExpress.Utils.Controls;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraEditors;
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class VersionControl : PanelControl, IXtraResizableControl
    {
        private Size controlSize = ScaleUtils.GetScaleSize(Resources.VCSize);
        private string dxMail = "info@devexpress.com";
        private string dxSite = Resources.DXSite;
        private string dxSupport = "Https://go.devexpress.com/Demo_2013_GetSupport.aspx";
        private LabelInfo labelInfo = new LabelInfo();
        private ProductKind product;
        private ContextMenu serialMenu;

        public event EventHandler Changed;

        public VersionControl()
        {
            UserLookAndFeel.Default.StyleChanged += new EventHandler(this.Default_StyleChanged);
            this.CreateLabelInfo();
        }

        private void CreateLabelInfo()
        {
            Rectangle rectangle = new Rectangle(0, 0, this.controlSize.Width, this.controlSize.Height);
            rectangle.Inflate(-2, -2);
            this.labelInfo.BackColor = Color.Transparent;
            this.labelInfo.Bounds = rectangle;
            this.labelInfo.Parent = this;
            this.labelInfo.ItemClick += new LabelInfoItemClickEvent(this.OnLabelClick);
            this.UpdateLabelText();
        }

        private void Default_StyleChanged(object sender, EventArgs e)
        {
            this.UpdateTextColor();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                UserLookAndFeel.Default.StyleChanged -= new EventHandler(this.Default_StyleChanged);
            }
        }

        public static Color GetLinkColor(UserLookAndFeel lookAndFeel)
        {
            Color empty = Color.Empty;
            if (lookAndFeel.ActiveStyle == ActiveLookAndFeelStyle.Skin)
            {
                empty = EditorsSkins.GetSkin(lookAndFeel).Colors.GetColor(EditorsSkins.SkinHyperlinkTextColor);
            }
            if (empty.IsEmpty)
            {
                empty = Color.Blue;
            }
            return empty;
        }

        private string GetProcessName(LabelInfoItemClickEventArgs e)
        {
            if (e.InfoText.Text == this.dxSite)
            {
                return "Https://go.devexpress.com/Demo_2013_BuyNow.aspx";
            }
            if (e.InfoText.Text == this.dxMail)
            {
                return "mailto:info@devexpress.com";
            }
            if (e.InfoText.Text == this.dxSupport)
            {
                return "Https://go.devexpress.com/Demo_2013_GetSupport.aspx";
            }
            return null;
        }

        private void OnLabelClick(object sender, LabelInfoItemClickEventArgs e)
        {
            string processName = this.GetProcessName(e);
            if (processName == null)
            {
                this.SerialMenu.Show(this, base.PointToClient(Control.MousePosition));
            }
            else
            {
                new Process { StartInfo = { 
                    FileName = processName,
                    Verb = "Open",
                    WindowStyle = ProcessWindowStyle.Normal
                } }.Start();
            }
        }

        private void OnMenuClick(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(AboutHelper.GetSerial(this.ProductInfo));
        }

        protected virtual void RaiseChanged()
        {
            if (this.Changed != null)
            {
                this.Changed(this, EventArgs.Empty);
            }
        }

        internal void SetProduct(ProductKind ProductKind)
        {
            this.product = ProductKind;
            this.UpdateLabelText();
        }

        private string UpdateDPIString(string s)
        {
            if (ScaleUtils.IsLargeFonts)
            {
                return s;
            }
            return s.Replace(",", ",\r\n");
        }

        private void UpdateLabelText()
        {
            if (this.ProductInfo.Kind != ProductKind.Default)
            {
                this.labelInfo.Texts.Clear();
                this.labelInfo.Texts.Add(string.Format("{0}: ", Resources.Version));
                this.labelInfo.Texts.Add("v2016 vol 1", Color.Empty, false, true);
                this.labelInfo.Texts.Add("\r\n\r\n");
                if (!this.IsTrial)
                {
                    this.labelInfo.Texts.Add(string.Format("{0}: ", Resources.SerialNumber));
                    this.labelInfo.Texts.Add(AboutHelper.GetSerial(this.ProductInfo), !this.IsTrial);
                    this.labelInfo.Texts.Add("\r\n");
                }
                if (this.IsTrial)
                {
                    this.labelInfo.Texts.Add("\r\n");
                    this.labelInfo.Texts.Add(string.Format("{0} ", Resources.ToPurchase));
                    this.labelInfo.Texts.Add(this.dxSite, true);
                    if (!LocalizationHelper.IsJapanese)
                    {
                        this.labelInfo.Texts.Add(string.Format("\r\n{0} ", Resources.CallUs));
                        this.labelInfo.Texts.Add("+1 (818) 844-3383", Color.Empty, false, true);
                    }
                    this.labelInfo.Texts.Add("\r\n\r\n");
                    this.labelInfo.Texts.Add(string.Format(this.DPIDisplayFormat, this.UpdateDPIString(Resources.ForPrePurchase)));
                    this.labelInfo.Texts.Add(this.dxMail, true);
                    this.labelInfo.Texts.Add("\r\n");
                }
                this.labelInfo.Texts.Add("\r\n\r\n");
                if (!LocalizationHelper.IsJapanese)
                {
                    this.labelInfo.Texts.Add(string.Format(this.DPIDisplayFormat, this.UpdateDPIString(Resources.TechQuestions)));
                    this.labelInfo.Texts.Add(this.dxSupport, true);
                }
                this.UpdateTextColor();
            }
        }

        private void UpdateTextColor()
        {
            foreach (LabelInfoText text in this.labelInfo.Texts)
            {
                text.Color = text.Active ? GetLinkColor(this.LookAndFeel) : this.ForeColor;
            }
        }

        private string DPIDisplayFormat
        {
            get
            {
                if (ScaleUtils.IsLargeFonts)
                {
                    return "{0}: ";
                }
                return "{0}:\r\n";
            }
        }

        public bool IsCaptionVisible
        {
            get
            {
                return false;
            }
        }

        protected bool IsTrial
        {
            get
            {
                return (AboutHelper.GetSerial(this.ProductInfo) == "-- TRIAL VERSION --");
            }
        }

        public Size MaxSize
        {
            get
            {
                return this.controlSize;
            }
        }

        public Size MinSize
        {
            get
            {
                return this.controlSize;
            }
        }

        protected virtual DevExpress.Utils.About.ProductInfo ProductInfo
        {
            get
            {
                return new DevExpress.Utils.About.ProductInfo(string.Empty, typeof(VersionControl), this.product, ProductInfoStage.Registered);
            }
        }

        protected ContextMenu SerialMenu
        {
            get
            {
                if (this.serialMenu == null)
                {
                    this.serialMenu = new ContextMenu(new MenuItem[] { new MenuItem("Copy to clipboard", new EventHandler(this.OnMenuClick)) });
                }
                return this.serialMenu;
            }
            set
            {
                if (this.serialMenu != null)
                {
                    this.serialMenu.Dispose();
                }
                this.serialMenu = null;
            }
        }
    }
}

