namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.Export.PlainText;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class RichEditUserControl : XtraUserControl
    {
        private IContainer components;
        public RichEditControl richEditControl;

        public RichEditUserControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.richEditControl = new RichEditControl();
            base.SuspendLayout();
            this.richEditControl.ActiveViewType = RichEditViewType.Draft;
            this.richEditControl.Views.DraftView.AdjustColorsToSkins = true;
            this.richEditControl.Dock = DockStyle.Fill;
            this.richEditControl.EnableToolTips = true;
            this.richEditControl.Location = new Point(0, 0);
            this.richEditControl.Name = "richEditControl";
            this.richEditControl.Options.DocumentCapabilities.Bookmarks = DocumentCapability.Disabled;
            this.richEditControl.Options.DocumentCapabilities.CharacterFormatting = DocumentCapability.Enabled;
            this.richEditControl.Options.DocumentCapabilities.CharacterStyle = DocumentCapability.Disabled;
            this.richEditControl.Options.DocumentCapabilities.Hyperlinks = DocumentCapability.Disabled;
            this.richEditControl.Options.DocumentCapabilities.Numbering.Bulleted = DocumentCapability.Disabled;
            this.richEditControl.Options.DocumentCapabilities.Numbering.MultiLevel = DocumentCapability.Disabled;
            this.richEditControl.Options.DocumentCapabilities.Numbering.Simple = DocumentCapability.Disabled;
            this.richEditControl.Options.DocumentCapabilities.ParagraphStyle = DocumentCapability.Disabled;
            this.richEditControl.Options.DocumentCapabilities.Tables = DocumentCapability.Disabled;
            this.richEditControl.Options.Export.PlainText.ExportFinalParagraphMark = ExportFinalParagraphMark.Never;
            this.richEditControl.Options.HorizontalRuler.Visibility = RichEditRulerVisibility.Hidden;
            this.richEditControl.Options.HorizontalScrollbar.Visibility = RichEditScrollbarVisibility.Hidden;
            this.richEditControl.Options.TableOptions.GridLines = RichEditTableGridLinesVisibility.Hidden;
            this.richEditControl.Size = new Size(0x353, 0x1c1);
            this.richEditControl.TabIndex = 0x10;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.richEditControl);
            base.Name = "RichEditUserControl";
            base.Size = new Size(0x353, 0x1c1);
            base.ResumeLayout(false);
        }

        public void InitializeSyntaxHighlight(ExampleLanguage language)
        {
            SyntaxHighlightInitializeHelper.Initialize(this.richEditControl, (language == ExampleLanguage.Csharp) ? "cs" : "vb");
            ExampleCodeEditor.DisableRichEditFeatures(this.richEditControl);
        }

        public System.Type CurrentNestedType { get; set; }

        public string RichText
        {
            get
            {
                return this.richEditControl.Text;
            }
            set
            {
                this.richEditControl.Text = value;
            }
        }
    }
}

