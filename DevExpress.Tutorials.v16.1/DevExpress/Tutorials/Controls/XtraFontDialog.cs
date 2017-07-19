namespace DevExpress.Tutorials.Controls
{
    using DevExpress.Tutorials;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using DevExpress.XtraTab;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class XtraFontDialog : XtraForm
    {
        private ComboBoxEdit cbeFont;
        private CheckedListBoxControl clbStyle;
        private Container components;
        private Font fCurrentFont;
        private Font fResultFont;
        private ImageListBoxControl ilbcFont;
        private LabelControl labelControl1;
        private LabelControl labelControl2;
        private LabelControl labelControl3;
        private LabelControl labelControl4;
        private ListBoxControl lbcFontSize;
        private LabelControl lcPreview;
        private SimpleButton sbCancel;
        private SimpleButton sbOk;
        private SpinEdit seFontSize;
        private TextEdit teFontStyle;
        private XtraTabControl xtraTabControl1;
        private XtraTabPage xtraTabPage2;

        public XtraFontDialog(Font font)
        {
            this.InitializeComponent();
            this.InitFont();
            this.InitSize();
            if (base.LookAndFeel.GetTouchUI())
            {
                base.Scale(new SizeF(1.4f + (base.LookAndFeel.GetTouchScaleFactor() / 10f), 1.4f + (base.LookAndFeel.GetTouchScaleFactor() / 10f)));
            }
            this.ilbcFont.SelectedIndex = -1;
            this.CurrentFont = font;
            this.UpdatePreview();
            this.cbeFont.GotFocus += new EventHandler(this.cbeFont_GotFocus);
        }

        private void cbeFont_GotFocus(object sender, EventArgs e)
        {
            this.cbeFont.SelectAll();
        }

        private void cbeFont_SelectedValueChanged(object sender, EventArgs e)
        {
            this.ilbcFont.SelectedItem = this.cbeFont.SelectedItem;
        }

        private void clbStyle_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            this.InitStyleString();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.ResultFontDispose();
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private FontStyle GetFontStyleByValues(CheckedListBoxControl clb)
        {
            FontStyle regular = FontStyle.Regular;
            if (clb.GetItemChecked(0))
            {
                regular |= FontStyle.Bold;
            }
            if (clb.GetItemChecked(1))
            {
                regular |= FontStyle.Italic;
            }
            if (clb.GetItemChecked(2))
            {
                regular |= FontStyle.Strikeout;
            }
            if (clb.GetItemChecked(3))
            {
                regular |= FontStyle.Underline;
            }
            return regular;
        }

        private void ilbcFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ilbcFont.SelectedItem != null)
            {
                this.cbeFont.SelectedItem = this.ilbcFont.SelectedItem;
                this.UpdatePreview();
            }
        }

        private void InitFont()
        {
            TutorialHelper.InitFont(this.ilbcFont);
            this.cbeFont.Properties.Items.AddRange(this.ilbcFont.Items);
        }

        private void InitFontValues()
        {
            this.ilbcFont.Enabled = this.cbeFont.Enabled = this.teFontStyle.Enabled = this.clbStyle.Enabled = this.seFontSize.Enabled = this.lbcFontSize.Enabled = this.CurrentFont != null;
            if (this.CurrentFont != null)
            {
                this.ilbcFont.SelectedValue = this.CurrentFont.Name;
                foreach (CheckedListBoxItem item in this.clbStyle.Items)
                {
                    item.CheckState = (this.CurrentFont.Style.ToString().IndexOf(item.Value.ToString()) > -1) ? CheckState.Checked : CheckState.Unchecked;
                }
                this.InitStyleString();
                this.seFontSize.Value = Convert.ToInt32(this.CurrentFont.Size);
            }
        }

        private void InitializeComponent()
        {
            this.xtraTabControl1 = new XtraTabControl();
            this.xtraTabPage2 = new XtraTabPage();
            this.lcPreview = new LabelControl();
            this.labelControl4 = new LabelControl();
            this.lbcFontSize = new ListBoxControl();
            this.labelControl3 = new LabelControl();
            this.seFontSize = new SpinEdit();
            this.labelControl2 = new LabelControl();
            this.teFontStyle = new TextEdit();
            this.clbStyle = new CheckedListBoxControl();
            this.labelControl1 = new LabelControl();
            this.ilbcFont = new ImageListBoxControl();
            this.cbeFont = new ComboBoxEdit();
            this.sbCancel = new SimpleButton();
            this.sbOk = new SimpleButton();
            this.xtraTabControl1.BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage2.SuspendLayout();
            ((ISupportInitialize) this.lbcFontSize).BeginInit();
            this.seFontSize.Properties.BeginInit();
            this.teFontStyle.Properties.BeginInit();
            ((ISupportInitialize) this.clbStyle).BeginInit();
            ((ISupportInitialize) this.ilbcFont).BeginInit();
            this.cbeFont.Properties.BeginInit();
            base.SuspendLayout();
            this.xtraTabControl1.Location = new Point(8, 9);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage2;
            this.xtraTabControl1.Size = new Size(0x178, 0x12f);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new XtraTabPage[] { this.xtraTabPage2 });
            this.xtraTabControl1.TabStop = false;
            this.xtraTabControl1.Text = "xtcFont";
            this.xtraTabPage2.Controls.AddRange(new Control[] { this.lcPreview, this.labelControl4, this.lbcFontSize, this.labelControl3, this.seFontSize, this.labelControl2, this.teFontStyle, this.clbStyle, this.labelControl1, this.ilbcFont, this.cbeFont });
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new Size(0x16f, 0x111);
            this.xtraTabPage2.Text = "Font";
            this.lcPreview.Appearance.BackColor = Color.White;
            this.lcPreview.Appearance.Options.UseBackColor = true;
            this.lcPreview.Appearance.Options.UseTextOptions = true;
            this.lcPreview.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
            this.lcPreview.Appearance.TextOptions.VAlignment = VertAlignment.Center;
            this.lcPreview.BorderStyle = BorderStyles.Simple;
            this.lcPreview.LineLocation = LineLocation.Center;
            this.lcPreview.LineVisible = true;
            this.lcPreview.Location = new Point(8, 0xd8);
            this.lcPreview.Name = "lcPreview";
            this.lcPreview.AutoSizeMode = LabelAutoSizeMode.None;
            this.lcPreview.Size = new Size(0x160, 40);
            this.lcPreview.TabIndex = 0x19;
            this.lcPreview.Text = "Font Preview Text";
            this.labelControl4.LineVisible = true;
            this.labelControl4.Location = new Point(10, 0xc0);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new Size(350, 13);
            this.labelControl4.TabIndex = 0x18;
            this.labelControl4.Text = "Preview";
            this.lbcFontSize.Location = new Point(280, 0x2e);
            this.lbcFontSize.Name = "lbcFontSize";
            this.lbcFontSize.Size = new Size(80, 130);
            this.lbcFontSize.TabIndex = 5;
            this.lbcFontSize.SelectedIndexChanged += new EventHandler(this.lbcFontSize_SelectedIndexChanged);
            this.labelControl3.AutoSizeMode = LabelAutoSizeMode.Horizontal;
            this.labelControl3.Location = new Point(0x11a, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new Size(0x30, 13);
            this.labelControl3.TabIndex = 0x16;
            this.labelControl3.Text = "Font Size:";
            int[] bits = new int[4];
            bits[0] = 8;
            this.seFontSize.EditValue = new decimal(bits);
            this.seFontSize.Location = new Point(280, 0x18);
            this.seFontSize.Name = "seFontSize";
            this.seFontSize.Properties.Buttons.AddRange(new EditorButton[] { new EditorButton() });
            this.seFontSize.Properties.IsFloatValue = false;
            this.seFontSize.Properties.Mask.EditMask = "N00";
            int[] numArray2 = new int[4];
            numArray2[0] = 100;
            this.seFontSize.Properties.MaxValue = new decimal(numArray2);
            int[] numArray3 = new int[4];
            numArray3[0] = 6;
            this.seFontSize.Properties.MinValue = new decimal(numArray3);
            this.seFontSize.Size = new Size(80, 20);
            this.seFontSize.TabIndex = 4;
            this.seFontSize.EditValueChanged += new EventHandler(this.seFontSize_EditValueChanged);
            this.labelControl2.AutoSizeMode = LabelAutoSizeMode.Horizontal;
            this.labelControl2.Location = new Point(170, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new Size(0x35, 13);
            this.labelControl2.TabIndex = 20;
            this.labelControl2.Text = "Font Style:";
            this.teFontStyle.Location = new Point(0xa8, 0x18);
            this.teFontStyle.Name = "teFontStyle";
            this.teFontStyle.Properties.ReadOnly = true;
            this.teFontStyle.Size = new Size(0x68, 20);
            this.teFontStyle.TabIndex = 2;
            this.teFontStyle.TabStop = false;
            this.clbStyle.CheckOnClick = true;
            this.clbStyle.Items.AddRange(new CheckedListBoxItem[] { new CheckedListBoxItem("Bold"), new CheckedListBoxItem("Italic"), new CheckedListBoxItem("Strikeout"), new CheckedListBoxItem("Underline") });
            this.clbStyle.Location = new Point(0xa8, 0x2e);
            this.clbStyle.Name = "clbStyle";
            this.clbStyle.Size = new Size(0x68, 130);
            this.clbStyle.TabIndex = 3;
            this.clbStyle.ItemCheck += new DevExpress.XtraEditors.Controls.ItemCheckEventHandler(this.clbStyle_ItemCheck);
            this.labelControl1.AutoSizeMode = LabelAutoSizeMode.Horizontal;
            this.labelControl1.Location = new Point(10, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(0x1a, 13);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "Font:";
            this.ilbcFont.Location = new Point(8, 0x2e);
            this.ilbcFont.Name = "ilbcFont";
            this.ilbcFont.Size = new Size(0x98, 130);
            this.ilbcFont.TabIndex = 1;
            this.ilbcFont.SelectedIndexChanged += new EventHandler(this.ilbcFont_SelectedIndexChanged);
            this.cbeFont.Location = new Point(8, 0x18);
            this.cbeFont.Name = "cbeFont";
            this.cbeFont.Properties.ShowDropDown = ShowDropDown.Never;
            this.cbeFont.Size = new Size(0x98, 20);
            this.cbeFont.TabIndex = 0;
            this.cbeFont.SelectedValueChanged += new EventHandler(this.cbeFont_SelectedValueChanged);
            this.sbCancel.DialogResult = DialogResult.Cancel;
            this.sbCancel.Location = new Point(0x128, 320);
            this.sbCancel.Name = "sbCancel";
            this.sbCancel.Size = new Size(0x58, 0x18);
            this.sbCancel.TabIndex = 2;
            this.sbCancel.Text = "&Cancel";
            this.sbOk.DialogResult = DialogResult.OK;
            this.sbOk.Location = new Point(200, 320);
            this.sbOk.Name = "sbOk";
            this.sbOk.Size = new Size(0x58, 0x18);
            this.sbOk.TabIndex = 1;
            this.sbOk.Text = "&OK";
            base.AcceptButton = this.sbOk;
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.CancelButton = this.sbCancel;
            base.ClientSize = new Size(0x18a, 0x160);
            base.Controls.AddRange(new Control[] { this.sbOk, this.sbCancel, this.xtraTabControl1 });
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmFontDialog";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Font";
            this.xtraTabControl1.EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage2.ResumeLayout(false);
            ((ISupportInitialize) this.lbcFontSize).EndInit();
            this.seFontSize.Properties.EndInit();
            this.teFontStyle.Properties.EndInit();
            ((ISupportInitialize) this.clbStyle).EndInit();
            ((ISupportInitialize) this.ilbcFont).EndInit();
            this.cbeFont.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void InitSize()
        {
            for (int i = 8; i < 30; i += 2)
            {
                this.lbcFontSize.Items.Add(i);
            }
            this.lbcFontSize.Items.Add(0x24);
            this.lbcFontSize.Items.Add(0x30);
            this.lbcFontSize.Items.Add(0x48);
        }

        private void InitStyleString()
        {
            this.teFontStyle.Text = this.GetFontStyleByValues(this.clbStyle).ToString();
            this.UpdatePreview();
        }

        private void lbcFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lbcFontSize.SelectedIndex != -1)
            {
                this.seFontSize.Value = (int) this.lbcFontSize.SelectedItem;
            }
        }

        private void ResultFontDispose()
        {
            if (this.fResultFont != null)
            {
                this.fResultFont.Dispose();
                this.fResultFont = null;
            }
        }

        private void seFontSize_EditValueChanged(object sender, EventArgs e)
        {
            int item = (int) this.seFontSize.Value;
            if (this.lbcFontSize.Items.IndexOf(item) < 0)
            {
                this.lbcFontSize.SelectedIndex = -1;
            }
            else
            {
                this.lbcFontSize.SelectedValue = item;
            }
            this.UpdatePreview();
        }

        private void UpdatePreview()
        {
            this.ResultFontDispose();
            this.lcPreview.Font = this.ResultFont;
        }

        private Font CurrentFont
        {
            get
            {
                return this.fCurrentFont;
            }
            set
            {
                this.fCurrentFont = value;
                this.InitFontValues();
            }
        }

        public Font ResultFont
        {
            get
            {
                if (this.fResultFont == null)
                {
                    this.fResultFont = new Font(this.ResultFontName, (float) ((int) this.seFontSize.Value), this.GetFontStyleByValues(this.clbStyle));
                }
                return this.fResultFont;
            }
        }

        private string ResultFontName
        {
            get
            {
                if (this.ilbcFont.SelectedItem != null)
                {
                    return this.ilbcFont.SelectedItem.ToString();
                }
                return AppearanceObject.DefaultFont.Name;
            }
        }
    }
}

