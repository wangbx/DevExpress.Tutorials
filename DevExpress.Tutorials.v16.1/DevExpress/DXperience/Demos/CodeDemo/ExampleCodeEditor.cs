namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.Utils;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.Internal;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExampleCodeEditor
    {
        private readonly List<IRichEditControl> codeEditor = new List<IRichEditControl>();
        private readonly ExampleLanguage current;
        private DateTime lastExampleCodeModifiedTime = DateTime.Now;

        public ExampleCodeEditor(IRichEditControl codeEditor, ExampleLanguage currentLanguage)
        {
            this.codeEditor.Add(codeEditor);
            this.current = currentLanguage;
            if (this.current == ExampleLanguage.Csharp)
            {
                this.codeEditor[0].InnerControl.InitializeDocument += new EventHandler(this.InitializeSyntaxHighlightForCs);
            }
            else
            {
                this.codeEditor[0].InnerControl.InitializeDocument += new EventHandler(this.InitializeSyntaxHighlightForVb);
            }
        }

        public void AddCodeEditor(IRichEditControl codeEditor)
        {
            this.codeEditor.Add(codeEditor);
        }

        internal void AfterCompile(bool codeExecutedWithoutExceptions)
        {
            this.UpdatePageBackground(codeExecutedWithoutExceptions);
            this.ResetLastExampleModifiedTime();
            this.SubscribeRichEditEvent();
        }

        internal void BeforeCompile()
        {
            this.UnsubscribeRichEditEvents();
        }

        internal static void DisableRichEditFeatures(IRichEditControl codeEditor)
        {
            RichEditControlOptionsBase options = codeEditor.InnerDocumentServer.Options;
            options.DocumentCapabilities.Hyperlinks = DocumentCapability.Disabled;
            options.DocumentCapabilities.Numbering.Bulleted = DocumentCapability.Disabled;
            options.DocumentCapabilities.Numbering.Simple = DocumentCapability.Disabled;
            options.DocumentCapabilities.Numbering.MultiLevel = DocumentCapability.Disabled;
            options.DocumentCapabilities.Tables = DocumentCapability.Disabled;
            options.DocumentCapabilities.Bookmarks = DocumentCapability.Disabled;
            options.DocumentCapabilities.CharacterStyle = DocumentCapability.Disabled;
            options.DocumentCapabilities.ParagraphStyle = DocumentCapability.Disabled;
        }

        internal void InitializeSyntaxHighlightForCs(object sender, EventArgs e)
        {
            SyntaxHighlightInitializeHelper.Initialize(sender as IRichEditControl, "cs");
            DisableRichEditFeatures(sender as IRichEditControl);
        }

        internal void InitializeSyntaxHighlightForVb(object sender, EventArgs e)
        {
            SyntaxHighlightInitializeHelper.Initialize(sender as IRichEditControl, "vb");
            DisableRichEditFeatures(sender as IRichEditControl);
        }

        public void ResetLastExampleModifiedTime()
        {
            this.lastExampleCodeModifiedTime = DateTime.Now;
        }

        private void richEditControl_TextChanged(object sender, EventArgs e)
        {
            this.lastExampleCodeModifiedTime = DateTime.Now;
        }

        public string ShowExample(CodeExample newExample)
        {
            string str = string.Empty;
            if (newExample != null)
            {
                str = (this.CurrentExampleLanguage == ExampleLanguage.Csharp) ? newExample.UserCodeCS : newExample.UserCodeVB;
                this.codeEditor[0].InnerControl.Text = str;
            }
            return str;
        }

        internal void SubscribeRichEditEvent()
        {
            foreach (InnerRichEditControl control in this.CodeEditors)
            {
                control.ContentChanged += new EventHandler(this.richEditControl_TextChanged);
            }
        }

        internal void UnsubscribeRichEditEvents()
        {
            foreach (InnerRichEditControl control in this.CodeEditors)
            {
                control.ContentChanged -= new EventHandler(this.richEditControl_TextChanged);
            }
        }

        private void UpdatePageBackground(bool codeEvaluated)
        {
            foreach (InnerRichEditControl control in this.CodeEditors)
            {
                control.Document.SetPageBackground(codeEvaluated ? DXColor.Empty : DXColor.FromArgb(0xff, 0xbc, 200), true);
            }
        }

        public List<InnerRichEditControl> CodeEditors
        {
            get
            {
                return (from e in this.codeEditor select e.InnerControl).ToList<InnerRichEditControl>();
            }
        }

        public ExampleLanguage CurrentExampleLanguage
        {
            get
            {
                return this.current;
            }
        }

        public DateTime LastExampleCodeModifiedTime
        {
            get
            {
                return this.lastExampleCodeModifiedTime;
            }
        }
    }
}

