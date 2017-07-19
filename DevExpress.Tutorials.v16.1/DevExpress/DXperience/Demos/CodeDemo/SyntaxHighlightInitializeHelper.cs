namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.Office.Utils;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraRichEdit.API.Native;
    using DevExpress.XtraRichEdit.Export;
    using DevExpress.XtraRichEdit.Import;
    using DevExpress.XtraRichEdit.Internal;
    using DevExpress.XtraRichEdit.Services;
    using System;

    public static class SyntaxHighlightInitializeHelper
    {
        public static void Initialize(IRichEditControl richEditControl, string codeExamplesFileExtension)
        {
            InnerRichEditControl innerControl = richEditControl.InnerControl;
            IRichEditCommandFactoryService service = innerControl.GetService<IRichEditCommandFactoryService>();
            if (service != null)
            {
                innerControl.ReplaceService<ISyntaxHighlightService>(new SyntaxHighlightService(innerControl, codeExamplesFileExtension));
                CustomRichEditCommandFactoryService serviceInstance = new CustomRichEditCommandFactoryService(service);
                innerControl.RemoveService(typeof(IRichEditCommandFactoryService));
                innerControl.AddService(typeof(IRichEditCommandFactoryService), serviceInstance);
                IDocumentImportManagerService service3 = innerControl.GetService<IDocumentImportManagerService>();
                service3.UnregisterAllImporters();
                service3.RegisterImporter(new PlainTextDocumentImporter());
                service3.RegisterImporter(new SourcesCodeDocumentImporter());
                IDocumentExportManagerService service4 = innerControl.GetService<IDocumentExportManagerService>();
                service4.UnregisterAllExporters();
                service4.RegisterExporter(new PlainTextDocumentExporter());
                service4.RegisterExporter(new SourcesCodeDocumentExporter());
                Document document = innerControl.Document;
                document.BeginUpdate();
                try
                {
                    document.DefaultCharacterProperties.FontName = "Consolas";
                    document.DefaultCharacterProperties.FontSize = 10f;
                    document.Sections[0].Page.Width = Units.InchesToDocumentsF(100f);
                    document.Sections[0].LineNumbering.CountBy = 1;
                    document.Sections[0].LineNumbering.RestartType = LineNumberingRestart.Continuous;
                }
                finally
                {
                    document.EndUpdate();
                }
            }
        }
    }
}

