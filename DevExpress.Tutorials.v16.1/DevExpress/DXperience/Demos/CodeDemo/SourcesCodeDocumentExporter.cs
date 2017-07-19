namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.Office.Internal;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.Export;

    public class SourcesCodeDocumentExporter : PlainTextDocumentExporter
    {
        public override FileDialogFilter Filter
        {
            get
            {
                return SourcesCodeDocumentImporter.filter;
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

