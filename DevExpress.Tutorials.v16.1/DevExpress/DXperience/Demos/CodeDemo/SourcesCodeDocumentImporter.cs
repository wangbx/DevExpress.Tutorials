namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.Office.Internal;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.Import;

    public class SourcesCodeDocumentImporter : PlainTextDocumentImporter
    {
        internal static readonly FileDialogFilter filter = new FileDialogFilter("Source Files", new string[] { "cs", "vb", "html", "htm", "js", "xml", "css" });

        public override FileDialogFilter Filter
        {
            get
            {
                return filter;
            }
        }

        public override DocumentFormat Format
        {
            get
            {
                return SourceCodeDocumentFormat.Id;
            }
        }
    }
}

