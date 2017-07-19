using System;
using DevExpress.XtraBars;
using DevExpress.ReportServer.Printing;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ReportServer.ServiceModel.DataContracts;
using DevExpress.DocumentServices.ServiceModel;
using DevExpress.XtraPrinting.Native;

namespace XtraPrintingDemos.ReportService {
    /// <summary>
    /// Summary description for PreviewControl.
    /// </summary>
    public class PreviewControl : XtraPrintingDemos.PreviewControl {
        class ReportInfo {
            public int Id { get; set; }
            public Pair<string, object> Parameter { get; set; }
        }

        static readonly Dictionary<string, ReportInfo> AvailableReports = new Dictionary<string, ReportInfo>();

        private BarListItem barItemReports;

        public PreviewControl() {
            AvailableReports.Add("Invoice", new ReportInfo { Id = 5, Parameter = new Pair<string, object>("maxOrderId", 10260) } );
            AvailableReports.Add("Suppliers", new ReportInfo { Id = 6 } );
            AvailableReports.Add("Product List", new ReportInfo { Id = 4 } );
            AvailableReports.Add("Customer Order History", new ReportInfo { Id = 1113 });

            InitializeComponent();
            HideButtonOptions();

            pageGroupDocument.ItemLinks.Remove(barItemOpen);
            pageGroupDocument.ItemLinks.Remove(barItemSave);
            pageGroupPageSetup.ItemLinks.Clear();
            barItemReports.Strings.AddRange(AvailableReports.Keys.ToArray());
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if(disposing) {
                DisposeDocumentSource();
            }
            base.Dispose(disposing);
        }
        void LoadReport(int reportId, Pair<string, object> parameter) {
            DisposeDocumentSource();
            var documentSource = new RemoteDocumentSource {
                ServiceUri = Settings1.Default.ReportServer,
                ReportIdentity = new ReportIdentity(reportId),
                AuthenticationType = AuthenticationType.Guest
            };
            pControl.DocumentSource = documentSource;
            var parameters = new DefaultValueParameterContainer();
            if(parameter != null) {
                parameters[parameter.First].Value = parameter.Second;
            }
            documentSource.CreateDocument(parameters);
        }
        void DisposeDocumentSource() {
            RemoteDocumentSource documentSource = pControl.DocumentSource as RemoteDocumentSource;
            if(documentSource != null) {
                pControl.DocumentSource = null;
                documentSource.Dispose();
            }
        }
        protected override void CreateDocumentCore() {
            var itemIndex = Math.Max(barItemReports.ItemIndex, 0);
            var itemName = barItemReports.Strings[itemIndex];
            ReportInfo reportInfo = AvailableReports[itemName];
            LoadReport(reportInfo.Id, reportInfo.Parameter);
        }
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewControl));
            this.barItemReports = new DevExpress.XtraBars.BarListItem();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.SuspendLayout();
            // 
            // printControl
            // 
            this.pControl.RequestDocumentCreation = false;
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barItemReports});
            this.ribbonControl.MaxItemId = 58;
            this.ribbonControl.Size = new System.Drawing.Size(1211, 142);
            // 
            // pageGroupDocument
            // 
            this.pageGroupDocument.AllowTextClipping = false;
            this.pageGroupDocument.ItemLinks.Add(this.barItemReports);
            // 
            // ribbonPageActions
            // 
            this.ribbonPageActions.Visible = false;
            // 
            // barItemReports
            // 
            this.barItemReports.Caption = "Reports";
            this.barItemReports.Glyph = ((System.Drawing.Image)(resources.GetObject("barItemReports.Glyph")));
            this.barItemReports.Hint = "Select a report for preview";
            this.barItemReports.Id = 57;
            this.barItemReports.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barItemReports.LargeGlyph")));
            this.barItemReports.Name = "barItemReports";
            this.barItemReports.ListItemClick += new DevExpress.XtraBars.ListItemClickEventHandler(this.barItemReports_ListItemClick);
            // 
            // PreviewControl
            // 
            this.Name = "PreviewControl";
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);

        }

        private void barItemReports_ListItemClick(object sender, ListItemClickEventArgs e) {
            CreateDocument();
        }
    }
}
