using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace XtraPrintingDemos {
	/// <summary>
	/// Summary description for ModuleControl.
	/// </summary>
	public class PreviewControl : ModuleControl { 
		protected DevExpress.XtraPrinting.Preview.DocumentViewer pControl;
		protected DevExpress.XtraPrinting.PrintingSystem printingSystem;
		private System.ComponentModel.IContainer components;

        protected DocumentViewerRibbonController fPrintRibbonController;
        protected DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl;
        private PrintPreviewBarItem printPreviewBarItem3;
        protected PrintPreviewBarItem barItemOptions;
        private PrintPreviewBarItem printPreviewBarItem5;
        private PrintPreviewBarItem printPreviewBarItem6;
        private PrintPreviewBarItem printPreviewBarItem7;
        private PrintPreviewBarItem printPreviewBarItem8;
        private PrintPreviewBarItem printPreviewBarItem9;
        private PrintPreviewBarItem printPreviewBarItem10;
        private PrintPreviewBarItem printPreviewBarItem11;
        private PrintPreviewBarItem printPreviewBarItem12;
        private PrintPreviewBarItem printPreviewBarItem13;
        private PrintPreviewBarItem printPreviewBarItem14;
        private PrintPreviewBarItem printPreviewBarItem15;
        private PrintPreviewBarItem printPreviewBarItem16;
        private PrintPreviewBarItem printPreviewBarItem17;
        private PrintPreviewBarItem printPreviewBarItem18;
        private PrintPreviewBarItem printPreviewBarItem19;
        private PrintPreviewBarItem printPreviewBarItem20;
        private PrintPreviewBarItem printPreviewBarItem21;
        private PrintPreviewBarItem printPreviewBarItem22;
        private PrintPreviewBarItem printPreviewBarItem23;
        private PrintPreviewBarItem printPreviewBarItem24;
        private PrintPreviewBarItem printPreviewBarItem26;
        private PrintPreviewBarItem printPreviewBarItem27;
        private PrintPreviewBarItem printPreviewBarItem28;
        private PrintPreviewBarItem printPreviewBarItem29;
        private PrintPreviewBarItem printPreviewBarItem30;
        private PrintPreviewBarItem printPreviewBarItem31;
        private PrintPreviewBarItem printPreviewBarItem32;
        private PrintPreviewBarItem printPreviewBarItem33;
        private PrintPreviewBarItem printPreviewBarItem34;
        private PrintPreviewBarItem printPreviewBarItem35;
        private PrintPreviewBarItem printPreviewBarItem36;
        private PrintPreviewBarItem printPreviewBarItem37;
        private PrintPreviewBarItem printPreviewBarItem38;
        private PrintPreviewBarItem printPreviewBarItem39;
        private PrintPreviewBarItem printPreviewBarItem40;
        private PrintPreviewBarItem printPreviewBarItem41;
        private PrintPreviewBarItem printPreviewBarItem42;
        private PrintPreviewBarItem printPreviewBarItem43;
        private PrintPreviewBarItem printPreviewBarItem44;
        private PrintPreviewBarItem printPreviewBarItem45;
        private PrintPreviewBarItem printPreviewBarItem46;
        protected PrintPreviewBarItem barItemOpen;
        protected PrintPreviewBarItem barItemSave;
        private PrintPreviewStaticItem printPreviewStaticItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private ProgressBarEditItem progressBarEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private PrintPreviewBarItem printPreviewBarItem48;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private PrintPreviewStaticItem printPreviewStaticItem2;
        private ZoomTrackBarEditItem zoomTrackBarEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
        protected PrintPreviewRibbonPage ribbonPage1;
        protected PrintPreviewRibbonPageGroup pageGroupDocument;
        protected PrintPreviewRibbonPageGroup pageGroupPrint;
        protected PrintPreviewRibbonPageGroup pageGroupPageSetup;
        private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup4;
        private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup5;
        private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup6;
        private PrintPreviewRibbonPageGroup printPreviewRibbonPageGroup7;
        protected DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        protected DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageActions;
        private DevExpress.XtraBars.BarButtonItem bbiRefresh;
        protected DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonGroupActions;

		protected virtual ContainerControl PrintBarManagerForm { get { return pControl; }
		}
		protected virtual PrintPreviewFormEx PreviewForm { get { return printingSystem == null ? null : printingSystem.PreviewFormEx; }
		}

		public virtual DocumentViewer PrintControl { get { return pControl; }
		}

		public PreviewControl() {
			InitializeComponent();
			printingSystem.SetCommandVisibility(PrintingSystemCommand.ClosePreview, CommandVisibility.None);
		}
        protected void HideButtonRefresh() {
            ribbonGroupActions.ItemLinks.Remove(bbiRefresh);
        }
        protected void HideButtonOptions() {
            pageGroupPrint.ItemLinks.Remove(barItemOptions);
        }
        protected void MergeGroupActions() {
            pageGroupDocument.ItemLinks.Add(bbiRefresh);
            ribbonPageActions.Visible = false;
        }
        public override bool AutoMergeRibbon {
            get {
                return true;
            }
            set { }
        }

        public override void Activate() {
            base.Activate();
            if(pControl.DockManager != null) {
                DevExpress.XtraBars.Docking.DockPanel navigationDockPanel = pControl.DockManager.Panels[new System.Guid("6b2e64eb-afd0-4676-bc3d-eca7e99946aa")];
                if(navigationDockPanel != null)
                    navigationDockPanel.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            }
            if(ribbonPageActions.Visible && ribbonControl.MergeOwner != null) {
                ribbonControl.MergeOwner.SelectedPage = ribbonPageActions;
            }
            ActivateCore();
		}
        protected override void DoHide() {
        }
		protected virtual void ActivateCore() {
            CreateDocument();
		}
		protected void CreateDocument() {
			Cursor currentCursor = Cursor.Current;
			try {
                printingSystem.Graph.PageUnit = GraphicsUnit.Pixel;
				Cursor.Current = Cursors.WaitCursor;
				CreateDocumentCore();
			} finally {
				Cursor.Current = currentCursor;
			}
		}
		protected virtual void CreateDocumentCore() {
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem6 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip7 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem7 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem7 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip8 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem8 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem8 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip9 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem9 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem9 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip10 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem10 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem10 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip11 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem11 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem11 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip12 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem12 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem12 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip13 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem13 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem13 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip14 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem14 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem14 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip15 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem15 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem15 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip16 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem16 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem16 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip17 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem17 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem17 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip18 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem18 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem18 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip19 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem19 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem19 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip20 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem20 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem20 = new DevExpress.Utils.ToolTipItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewControl));
            DevExpress.Utils.SuperToolTip superToolTip21 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem21 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem21 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip22 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem22 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem22 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip23 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem23 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem23 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip24 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem24 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem24 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip25 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem25 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem25 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip26 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem26 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem26 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip27 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem27 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem27 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip28 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem28 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem28 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip29 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem29 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem29 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip30 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem30 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem30 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip31 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem31 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem31 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip32 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem32 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem32 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip33 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem33 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem33 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip34 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem34 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem34 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip35 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem35 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem35 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip36 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem36 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem36 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip37 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem37 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem37 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip38 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem38 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem38 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip39 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem39 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem39 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip40 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem40 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem40 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip41 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem41 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem41 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip42 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem42 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem42 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip43 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem43 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem43 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip44 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem44 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem44 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip45 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem45 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem45 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip46 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem46 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem46 = new DevExpress.Utils.ToolTipItem();
            this.pControl = new DevExpress.XtraPrinting.Preview.DocumentViewer();
            this.printingSystem = new DevExpress.XtraPrinting.PrintingSystem(this.components);
            this.fPrintRibbonController = new DevExpress.XtraPrinting.Preview.DocumentViewerRibbonController(this.components);
            this.ribbonControl = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.printPreviewBarItem3 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.barItemOptions = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem5 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem6 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem7 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem8 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem9 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem10 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem11 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem12 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem13 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem14 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem15 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem16 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem17 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem18 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem19 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem20 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem21 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem22 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem23 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem24 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem26 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem27 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem28 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem29 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem30 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem31 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem32 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem33 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem34 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem35 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem36 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem37 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem38 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem39 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem40 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem41 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem42 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem43 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem44 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem45 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewBarItem46 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.barItemOpen = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.barItemSave = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.printPreviewStaticItem1 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.progressBarEditItem1 = new DevExpress.XtraPrinting.Preview.ProgressBarEditItem();
            this.repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            this.printPreviewBarItem48 = new DevExpress.XtraPrinting.Preview.PrintPreviewBarItem();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.printPreviewStaticItem2 = new DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem();
            this.zoomTrackBarEditItem1 = new DevExpress.XtraPrinting.Preview.ZoomTrackBarEditItem();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.bbiRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPage();
            this.pageGroupDocument = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
            this.pageGroupPrint = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
            this.pageGroupPageSetup = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
            this.printPreviewRibbonPageGroup4 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
            this.printPreviewRibbonPageGroup5 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
            this.printPreviewRibbonPageGroup6 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
            this.printPreviewRibbonPageGroup7 = new DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroup();
            this.ribbonPageActions = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonGroupActions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // printControl
            // 
            this.pControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pControl.DocumentSource = this.printingSystem;
            this.pControl.IsMetric = false;
            this.pControl.Location = new System.Drawing.Point(0, 142);
            this.pControl.Name = "printControl";
            this.pControl.Size = new System.Drawing.Size(1211, 332);
            this.pControl.TabIndex = 1;
            this.pControl.TabStop = false;
            // 
            // fPrintRibbonController
            // 
            this.fPrintRibbonController.DocumentViewer = this.pControl;
            this.fPrintRibbonController.RibbonControl = this.ribbonControl;
            this.fPrintRibbonController.RibbonStatusBar = this.ribbonStatusBar1;
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl.ExpandCollapseItem,
            this.printPreviewBarItem3,
            this.barItemOptions,
            this.printPreviewBarItem5,
            this.printPreviewBarItem6,
            this.printPreviewBarItem7,
            this.printPreviewBarItem8,
            this.printPreviewBarItem9,
            this.printPreviewBarItem10,
            this.printPreviewBarItem11,
            this.printPreviewBarItem12,
            this.printPreviewBarItem13,
            this.printPreviewBarItem14,
            this.printPreviewBarItem15,
            this.printPreviewBarItem16,
            this.printPreviewBarItem17,
            this.printPreviewBarItem18,
            this.printPreviewBarItem19,
            this.printPreviewBarItem20,
            this.printPreviewBarItem21,
            this.printPreviewBarItem22,
            this.printPreviewBarItem23,
            this.printPreviewBarItem24,
            this.printPreviewBarItem26,
            this.printPreviewBarItem27,
            this.printPreviewBarItem28,
            this.printPreviewBarItem29,
            this.printPreviewBarItem30,
            this.printPreviewBarItem31,
            this.printPreviewBarItem32,
            this.printPreviewBarItem33,
            this.printPreviewBarItem34,
            this.printPreviewBarItem35,
            this.printPreviewBarItem36,
            this.printPreviewBarItem37,
            this.printPreviewBarItem38,
            this.printPreviewBarItem39,
            this.printPreviewBarItem40,
            this.printPreviewBarItem41,
            this.printPreviewBarItem42,
            this.printPreviewBarItem43,
            this.printPreviewBarItem44,
            this.printPreviewBarItem45,
            this.printPreviewBarItem46,
            this.barItemOpen,
            this.barItemSave,
            this.printPreviewStaticItem1,
            this.barStaticItem1,
            this.progressBarEditItem1,
            this.printPreviewBarItem48,
            this.barButtonItem1,
            this.printPreviewStaticItem2,
            this.zoomTrackBarEditItem1,
            this.bbiRefresh});
            this.ribbonControl.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl.MaxItemId = 56;
            this.ribbonControl.Name = "ribbonControl";
            this.ribbonControl.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1,
            this.ribbonPageActions});
            this.ribbonControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemProgressBar1,
            this.repositoryItemZoomTrackBar1});
            this.ribbonControl.Size = new System.Drawing.Size(1211, 142);
            this.ribbonControl.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl.TransparentEditors = true;
            // 
            // printPreviewBarItem3
            // 
            this.printPreviewBarItem3.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.printPreviewBarItem3.Caption = "Find";
            this.printPreviewBarItem3.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Find;
            this.printPreviewBarItem3.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem3.Enabled = false;
            this.printPreviewBarItem3.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Find;
            this.printPreviewBarItem3.Id = 3;
            this.printPreviewBarItem3.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_FindLarge;
            this.printPreviewBarItem3.Name = "printPreviewBarItem3";
            superToolTip1.FixedTooltipWidth = true;
            toolTipTitleItem1.Text = "Find";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Show the Find dialog to find text in the document.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            superToolTip1.MaxWidth = 210;
            this.printPreviewBarItem3.SuperTip = superToolTip1;
            // 
            // barItemOptions
            // 
            this.barItemOptions.Caption = "Options";
            this.barItemOptions.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Customize;
            this.barItemOptions.ContextSpecifier = this.fPrintRibbonController;
            this.barItemOptions.Enabled = false;
            this.barItemOptions.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Customize;
            this.barItemOptions.Id = 4;
            this.barItemOptions.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_CustomizeLarge;
            this.barItemOptions.Name = "barItemOptions";
            superToolTip2.FixedTooltipWidth = true;
            toolTipTitleItem2.Text = "Options";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "Open the Print Options dialog, in which you can change printing options.";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            superToolTip2.MaxWidth = 210;
            this.barItemOptions.SuperTip = superToolTip2;
            // 
            // printPreviewBarItem5
            // 
            this.printPreviewBarItem5.Caption = "Print";
            this.printPreviewBarItem5.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Print;
            this.printPreviewBarItem5.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem5.Enabled = false;
            this.printPreviewBarItem5.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Print;
            this.printPreviewBarItem5.Id = 5;
            this.printPreviewBarItem5.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PrintLarge;
            this.printPreviewBarItem5.Name = "printPreviewBarItem5";
            superToolTip3.FixedTooltipWidth = true;
            toolTipTitleItem3.Text = "Print (Ctrl+P)";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Select a printer, number of copies and other printing options before printing.";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            superToolTip3.MaxWidth = 210;
            this.printPreviewBarItem5.SuperTip = superToolTip3;
            // 
            // printPreviewBarItem6
            // 
            this.printPreviewBarItem6.Caption = "Quick Print";
            this.printPreviewBarItem6.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PrintDirect;
            this.printPreviewBarItem6.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem6.Enabled = false;
            this.printPreviewBarItem6.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PrintDirect;
            this.printPreviewBarItem6.Id = 6;
            this.printPreviewBarItem6.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PrintDirectLarge;
            this.printPreviewBarItem6.Name = "printPreviewBarItem6";
            superToolTip4.FixedTooltipWidth = true;
            toolTipTitleItem4.Text = "Quick Print";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "Send the document directly to the default printer without making changes.";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            superToolTip4.MaxWidth = 210;
            this.printPreviewBarItem6.SuperTip = superToolTip4;
            // 
            // printPreviewBarItem7
            // 
            this.printPreviewBarItem7.Caption = "Custom Margins...";
            this.printPreviewBarItem7.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageSetup;
            this.printPreviewBarItem7.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem7.Enabled = false;
            this.printPreviewBarItem7.Id = 7;
            this.printPreviewBarItem7.Name = "printPreviewBarItem7";
            superToolTip5.FixedTooltipWidth = true;
            toolTipTitleItem5.Text = "Page Setup";
            toolTipItem5.LeftIndent = 6;
            toolTipItem5.Text = "Show the Page Setup dialog.";
            superToolTip5.Items.Add(toolTipTitleItem5);
            superToolTip5.Items.Add(toolTipItem5);
            superToolTip5.MaxWidth = 210;
            this.printPreviewBarItem7.SuperTip = superToolTip5;
            // 
            // printPreviewBarItem8
            // 
            this.printPreviewBarItem8.Caption = "Header/Footer";
            this.printPreviewBarItem8.Command = DevExpress.XtraPrinting.PrintingSystemCommand.EditPageHF;
            this.printPreviewBarItem8.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem8.Enabled = false;
            this.printPreviewBarItem8.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_EditPageHF;
            this.printPreviewBarItem8.Id = 8;
            this.printPreviewBarItem8.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_EditPageHFLarge;
            this.printPreviewBarItem8.Name = "printPreviewBarItem8";
            superToolTip6.FixedTooltipWidth = true;
            toolTipTitleItem6.Text = "Header and Footer";
            toolTipItem6.LeftIndent = 6;
            toolTipItem6.Text = "Edit the header and footer of the document.";
            superToolTip6.Items.Add(toolTipTitleItem6);
            superToolTip6.Items.Add(toolTipItem6);
            superToolTip6.MaxWidth = 210;
            this.printPreviewBarItem8.SuperTip = superToolTip6;
            // 
            // printPreviewBarItem9
            // 
            this.printPreviewBarItem9.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem9.Caption = "Scale";
            this.printPreviewBarItem9.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Scale;
            this.printPreviewBarItem9.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem9.Enabled = false;
            this.printPreviewBarItem9.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Scale;
            this.printPreviewBarItem9.Id = 9;
            this.printPreviewBarItem9.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ScaleLarge;
            this.printPreviewBarItem9.Name = "printPreviewBarItem9";
            superToolTip7.FixedTooltipWidth = true;
            toolTipTitleItem7.Text = "Scale";
            toolTipItem7.LeftIndent = 6;
            toolTipItem7.Text = "Stretch or shrink the printed output to a percentage of its actual size.";
            superToolTip7.Items.Add(toolTipTitleItem7);
            superToolTip7.Items.Add(toolTipItem7);
            superToolTip7.MaxWidth = 210;
            this.printPreviewBarItem9.SuperTip = superToolTip7;
            // 
            // printPreviewBarItem10
            // 
            this.printPreviewBarItem10.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.printPreviewBarItem10.Caption = "Pointer";
            this.printPreviewBarItem10.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Pointer;
            this.printPreviewBarItem10.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem10.Down = true;
            this.printPreviewBarItem10.Enabled = false;
            this.printPreviewBarItem10.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Pointer;
            this.printPreviewBarItem10.GroupIndex = 1;
            this.printPreviewBarItem10.Id = 10;
            this.printPreviewBarItem10.Name = "printPreviewBarItem10";
            this.printPreviewBarItem10.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            superToolTip8.FixedTooltipWidth = true;
            toolTipTitleItem8.Text = "Mouse Pointer";
            toolTipItem8.LeftIndent = 6;
            toolTipItem8.Text = "Show the mouse pointer.";
            superToolTip8.Items.Add(toolTipTitleItem8);
            superToolTip8.Items.Add(toolTipItem8);
            superToolTip8.MaxWidth = 210;
            this.printPreviewBarItem10.SuperTip = superToolTip8;
            // 
            // printPreviewBarItem11
            // 
            this.printPreviewBarItem11.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.printPreviewBarItem11.Caption = "Hand Tool";
            this.printPreviewBarItem11.Command = DevExpress.XtraPrinting.PrintingSystemCommand.HandTool;
            this.printPreviewBarItem11.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem11.Enabled = false;
            this.printPreviewBarItem11.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_HandTool;
            this.printPreviewBarItem11.GroupIndex = 1;
            this.printPreviewBarItem11.Id = 11;
            this.printPreviewBarItem11.Name = "printPreviewBarItem11";
            this.printPreviewBarItem11.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            superToolTip9.FixedTooltipWidth = true;
            toolTipTitleItem9.Text = "Hand Tool";
            toolTipItem9.LeftIndent = 6;
            toolTipItem9.Text = "Invoke the Hand tool to manually scroll through pages.";
            superToolTip9.Items.Add(toolTipTitleItem9);
            superToolTip9.Items.Add(toolTipItem9);
            superToolTip9.MaxWidth = 210;
            this.printPreviewBarItem11.SuperTip = superToolTip9;
            // 
            // printPreviewBarItem12
            // 
            this.printPreviewBarItem12.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.printPreviewBarItem12.Caption = "Magnifier";
            this.printPreviewBarItem12.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Magnifier;
            this.printPreviewBarItem12.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem12.Enabled = false;
            this.printPreviewBarItem12.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Magnifier;
            this.printPreviewBarItem12.GroupIndex = 1;
            this.printPreviewBarItem12.Id = 12;
            this.printPreviewBarItem12.Name = "printPreviewBarItem12";
            this.printPreviewBarItem12.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithoutText;
            superToolTip10.FixedTooltipWidth = true;
            toolTipTitleItem10.Text = "Magnifier";
            toolTipItem10.LeftIndent = 6;
            toolTipItem10.Text = "Invoke the Magnifier tool.\r\n\r\nClicking once on a document zooms it so that a sing" +
    "le page becomes entirely visible, while clicking another time zooms it to 100% o" +
    "f the normal size.";
            superToolTip10.Items.Add(toolTipTitleItem10);
            superToolTip10.Items.Add(toolTipItem10);
            superToolTip10.MaxWidth = 210;
            this.printPreviewBarItem12.SuperTip = superToolTip10;
            // 
            // printPreviewBarItem13
            // 
            this.printPreviewBarItem13.Caption = "Zoom Out";
            this.printPreviewBarItem13.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomOut;
            this.printPreviewBarItem13.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem13.Enabled = false;
            this.printPreviewBarItem13.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ZoomOut;
            this.printPreviewBarItem13.Id = 13;
            this.printPreviewBarItem13.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ZoomOutLarge;
            this.printPreviewBarItem13.Name = "printPreviewBarItem13";
            superToolTip11.FixedTooltipWidth = true;
            toolTipTitleItem11.Text = "Zoom Out";
            toolTipItem11.LeftIndent = 6;
            toolTipItem11.Text = "Zoom out to see more of the page at a reduced size.";
            superToolTip11.Items.Add(toolTipTitleItem11);
            superToolTip11.Items.Add(toolTipItem11);
            superToolTip11.MaxWidth = 210;
            this.printPreviewBarItem13.SuperTip = superToolTip11;
            // 
            // printPreviewBarItem14
            // 
            this.printPreviewBarItem14.Caption = "Zoom In";
            this.printPreviewBarItem14.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ZoomIn;
            this.printPreviewBarItem14.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem14.Enabled = false;
            this.printPreviewBarItem14.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ZoomIn;
            this.printPreviewBarItem14.Id = 14;
            this.printPreviewBarItem14.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ZoomInLarge;
            this.printPreviewBarItem14.Name = "printPreviewBarItem14";
            superToolTip12.FixedTooltipWidth = true;
            toolTipTitleItem12.Text = "Zoom In";
            toolTipItem12.LeftIndent = 6;
            toolTipItem12.Text = "Zoom in to get a close-up view of the document.";
            superToolTip12.Items.Add(toolTipTitleItem12);
            superToolTip12.Items.Add(toolTipItem12);
            superToolTip12.MaxWidth = 210;
            this.printPreviewBarItem14.SuperTip = superToolTip12;
            // 
            // printPreviewBarItem15
            // 
            this.printPreviewBarItem15.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem15.Caption = "Zoom";
            this.printPreviewBarItem15.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Zoom;
            this.printPreviewBarItem15.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem15.Enabled = false;
            this.printPreviewBarItem15.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Zoom;
            this.printPreviewBarItem15.Id = 15;
            this.printPreviewBarItem15.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ZoomLarge;
            this.printPreviewBarItem15.Name = "printPreviewBarItem15";
            superToolTip13.FixedTooltipWidth = true;
            toolTipTitleItem13.Text = "Zoom";
            toolTipItem13.LeftIndent = 6;
            toolTipItem13.Text = "Change the zoom level of the document preview.";
            superToolTip13.Items.Add(toolTipTitleItem13);
            superToolTip13.Items.Add(toolTipItem13);
            superToolTip13.MaxWidth = 210;
            this.printPreviewBarItem15.SuperTip = superToolTip13;
            // 
            // printPreviewBarItem16
            // 
            this.printPreviewBarItem16.Caption = "First Page";
            this.printPreviewBarItem16.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowFirstPage;
            this.printPreviewBarItem16.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem16.Enabled = false;
            this.printPreviewBarItem16.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowFirstPage;
            this.printPreviewBarItem16.Id = 16;
            this.printPreviewBarItem16.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowFirstPageLarge;
            this.printPreviewBarItem16.Name = "printPreviewBarItem16";
            superToolTip14.FixedTooltipWidth = true;
            toolTipTitleItem14.Text = "First Page (Ctrl+Home)";
            toolTipItem14.LeftIndent = 6;
            toolTipItem14.Text = "Navigate to the first page of the document.";
            superToolTip14.Items.Add(toolTipTitleItem14);
            superToolTip14.Items.Add(toolTipItem14);
            superToolTip14.MaxWidth = 210;
            this.printPreviewBarItem16.SuperTip = superToolTip14;
            // 
            // printPreviewBarItem17
            // 
            this.printPreviewBarItem17.Caption = "Previous Page";
            this.printPreviewBarItem17.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowPrevPage;
            this.printPreviewBarItem17.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem17.Enabled = false;
            this.printPreviewBarItem17.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowPrevPage;
            this.printPreviewBarItem17.Id = 17;
            this.printPreviewBarItem17.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowPrevPageLarge;
            this.printPreviewBarItem17.Name = "printPreviewBarItem17";
            superToolTip15.FixedTooltipWidth = true;
            toolTipTitleItem15.Text = "Previous Page (PageUp)";
            toolTipItem15.LeftIndent = 6;
            toolTipItem15.Text = "Navigate to the previous page of the document.";
            superToolTip15.Items.Add(toolTipTitleItem15);
            superToolTip15.Items.Add(toolTipItem15);
            superToolTip15.MaxWidth = 210;
            this.printPreviewBarItem17.SuperTip = superToolTip15;
            // 
            // printPreviewBarItem18
            // 
            this.printPreviewBarItem18.Caption = "Next  Page ";
            this.printPreviewBarItem18.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowNextPage;
            this.printPreviewBarItem18.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem18.Enabled = false;
            this.printPreviewBarItem18.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowNextPage;
            this.printPreviewBarItem18.Id = 18;
            this.printPreviewBarItem18.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowNextPageLarge;
            this.printPreviewBarItem18.Name = "printPreviewBarItem18";
            superToolTip16.FixedTooltipWidth = true;
            toolTipTitleItem16.Text = "Next Page (PageDown)";
            toolTipItem16.LeftIndent = 6;
            toolTipItem16.Text = "Navigate to the next page of the document.";
            superToolTip16.Items.Add(toolTipTitleItem16);
            superToolTip16.Items.Add(toolTipItem16);
            superToolTip16.MaxWidth = 210;
            this.printPreviewBarItem18.SuperTip = superToolTip16;
            // 
            // printPreviewBarItem19
            // 
            this.printPreviewBarItem19.Caption = "Last  Page ";
            this.printPreviewBarItem19.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ShowLastPage;
            this.printPreviewBarItem19.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem19.Enabled = false;
            this.printPreviewBarItem19.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowLastPage;
            this.printPreviewBarItem19.Id = 19;
            this.printPreviewBarItem19.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ShowLastPageLarge;
            this.printPreviewBarItem19.Name = "printPreviewBarItem19";
            superToolTip17.FixedTooltipWidth = true;
            toolTipTitleItem17.Text = "Last Page (Ctrl+End)";
            toolTipItem17.LeftIndent = 6;
            toolTipItem17.Text = "Navigate to the last page of the document.";
            superToolTip17.Items.Add(toolTipTitleItem17);
            superToolTip17.Items.Add(toolTipItem17);
            superToolTip17.MaxWidth = 210;
            this.printPreviewBarItem19.SuperTip = superToolTip17;
            // 
            // printPreviewBarItem20
            // 
            this.printPreviewBarItem20.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem20.Caption = "Many Pages";
            this.printPreviewBarItem20.Command = DevExpress.XtraPrinting.PrintingSystemCommand.MultiplePages;
            this.printPreviewBarItem20.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem20.Enabled = false;
            this.printPreviewBarItem20.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_MultiplePages;
            this.printPreviewBarItem20.Id = 20;
            this.printPreviewBarItem20.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_MultiplePagesLarge;
            this.printPreviewBarItem20.Name = "printPreviewBarItem20";
            superToolTip18.FixedTooltipWidth = true;
            toolTipTitleItem18.Text = "View Many Pages";
            toolTipItem18.LeftIndent = 6;
            toolTipItem18.Text = "Choose the page layout to arrange the document pages in preview.";
            superToolTip18.Items.Add(toolTipTitleItem18);
            superToolTip18.Items.Add(toolTipItem18);
            superToolTip18.MaxWidth = 210;
            this.printPreviewBarItem20.SuperTip = superToolTip18;
            // 
            // printPreviewBarItem21
            // 
            this.printPreviewBarItem21.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem21.Caption = "Page Color";
            this.printPreviewBarItem21.Command = DevExpress.XtraPrinting.PrintingSystemCommand.FillBackground;
            this.printPreviewBarItem21.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem21.Enabled = false;
            this.printPreviewBarItem21.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_FillBackground;
            this.printPreviewBarItem21.Id = 21;
            this.printPreviewBarItem21.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_FillBackgroundLarge;
            this.printPreviewBarItem21.Name = "printPreviewBarItem21";
            superToolTip19.FixedTooltipWidth = true;
            toolTipTitleItem19.Text = "Background Color";
            toolTipItem19.LeftIndent = 6;
            toolTipItem19.Text = "Choose a color for the background of the document pages.";
            superToolTip19.Items.Add(toolTipTitleItem19);
            superToolTip19.Items.Add(toolTipItem19);
            superToolTip19.MaxWidth = 210;
            this.printPreviewBarItem21.SuperTip = superToolTip19;
            // 
            // printPreviewBarItem22
            // 
            this.printPreviewBarItem22.Caption = "Watermark";
            this.printPreviewBarItem22.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Watermark;
            this.printPreviewBarItem22.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem22.Enabled = false;
            this.printPreviewBarItem22.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Watermark;
            this.printPreviewBarItem22.Id = 22;
            this.printPreviewBarItem22.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_WatermarkLarge;
            this.printPreviewBarItem22.Name = "printPreviewBarItem22";
            superToolTip20.FixedTooltipWidth = true;
            toolTipTitleItem20.Text = "Watermark";
            toolTipItem20.LeftIndent = 6;
            toolTipItem20.Text = "Insert ghosted text or image behind the content of a page.\r\n\r\nThis is often used " +
    "to indicate that a document is to be treated specially.";
            superToolTip20.Items.Add(toolTipTitleItem20);
            superToolTip20.Items.Add(toolTipItem20);
            superToolTip20.MaxWidth = 210;
            this.printPreviewBarItem22.SuperTip = superToolTip20;
            // 
            // printPreviewBarItem23
            // 
            this.printPreviewBarItem23.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem23.Caption = "Export To";
            this.printPreviewBarItem23.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportFile;
            this.printPreviewBarItem23.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem23.Enabled = false;
            this.printPreviewBarItem23.Glyph = ((System.Drawing.Image)(resources.GetObject("printPreviewBarItem23.Glyph")));
            this.printPreviewBarItem23.Id = 23;
            this.printPreviewBarItem23.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("printPreviewBarItem23.LargeGlyph")));
            this.printPreviewBarItem23.Name = "printPreviewBarItem23";
            superToolTip21.FixedTooltipWidth = true;
            toolTipTitleItem21.Text = "Export To...";
            toolTipItem21.LeftIndent = 6;
            toolTipItem21.Text = "Export the current document in one of the available formats, and save it to the f" +
    "ile on a disk.";
            superToolTip21.Items.Add(toolTipTitleItem21);
            superToolTip21.Items.Add(toolTipItem21);
            superToolTip21.MaxWidth = 210;
            this.printPreviewBarItem23.SuperTip = superToolTip21;
            // 
            // printPreviewBarItem24
            // 
            this.printPreviewBarItem24.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem24.Caption = "E-Mail As";
            this.printPreviewBarItem24.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendFile;
            this.printPreviewBarItem24.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem24.Enabled = false;
            this.printPreviewBarItem24.Glyph = ((System.Drawing.Image)(resources.GetObject("printPreviewBarItem24.Glyph")));
            this.printPreviewBarItem24.Id = 24;
            this.printPreviewBarItem24.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("printPreviewBarItem24.LargeGlyph")));
            this.printPreviewBarItem24.Name = "printPreviewBarItem24";
            superToolTip22.FixedTooltipWidth = true;
            toolTipTitleItem22.Text = "E-Mail As...";
            toolTipItem22.LeftIndent = 6;
            toolTipItem22.Text = "Export the current document in one of the available formats, and attach it to the" +
    " e-mail.";
            superToolTip22.Items.Add(toolTipTitleItem22);
            superToolTip22.Items.Add(toolTipItem22);
            superToolTip22.MaxWidth = 210;
            this.printPreviewBarItem24.SuperTip = superToolTip22;
            // 
            // printPreviewBarItem26
            // 
            this.printPreviewBarItem26.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem26.Caption = "Orientation";
            this.printPreviewBarItem26.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageOrientation;
            this.printPreviewBarItem26.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem26.Enabled = false;
            this.printPreviewBarItem26.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PageOrientation;
            this.printPreviewBarItem26.Id = 26;
            this.printPreviewBarItem26.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PageOrientationLarge;
            this.printPreviewBarItem26.Name = "printPreviewBarItem26";
            superToolTip23.FixedTooltipWidth = true;
            toolTipTitleItem23.Text = "Page Orientation";
            toolTipItem23.LeftIndent = 6;
            toolTipItem23.Text = "Switch the pages between portrait and landscape layouts.";
            superToolTip23.Items.Add(toolTipTitleItem23);
            superToolTip23.Items.Add(toolTipItem23);
            superToolTip23.MaxWidth = 210;
            this.printPreviewBarItem26.SuperTip = superToolTip23;
            // 
            // printPreviewBarItem27
            // 
            this.printPreviewBarItem27.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem27.Caption = "Size";
            this.printPreviewBarItem27.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PaperSize;
            this.printPreviewBarItem27.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem27.Enabled = false;
            this.printPreviewBarItem27.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PaperSize;
            this.printPreviewBarItem27.Id = 27;
            this.printPreviewBarItem27.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PaperSizeLarge;
            this.printPreviewBarItem27.Name = "printPreviewBarItem27";
            superToolTip24.FixedTooltipWidth = true;
            toolTipTitleItem24.Text = "Page Size";
            toolTipItem24.LeftIndent = 6;
            toolTipItem24.Text = "Choose the paper size of the document.";
            superToolTip24.Items.Add(toolTipTitleItem24);
            superToolTip24.Items.Add(toolTipItem24);
            superToolTip24.MaxWidth = 210;
            this.printPreviewBarItem27.SuperTip = superToolTip24;
            // 
            // printPreviewBarItem28
            // 
            this.printPreviewBarItem28.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.printPreviewBarItem28.Caption = "Margins";
            this.printPreviewBarItem28.Command = DevExpress.XtraPrinting.PrintingSystemCommand.PageMargins;
            this.printPreviewBarItem28.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem28.Enabled = false;
            this.printPreviewBarItem28.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PageMargins;
            this.printPreviewBarItem28.Id = 28;
            this.printPreviewBarItem28.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PageMarginsLarge;
            this.printPreviewBarItem28.Name = "printPreviewBarItem28";
            superToolTip25.FixedTooltipWidth = true;
            toolTipTitleItem25.Text = "Page Margins";
            toolTipItem25.LeftIndent = 6;
            toolTipItem25.Text = "Select the margin sizes for the entire document.\r\n\r\nTo apply specific margin size" +
    "s to the document, click Custom Margins.";
            superToolTip25.Items.Add(toolTipTitleItem25);
            superToolTip25.Items.Add(toolTipItem25);
            superToolTip25.MaxWidth = 210;
            this.printPreviewBarItem28.SuperTip = superToolTip25;
            // 
            // printPreviewBarItem29
            // 
            this.printPreviewBarItem29.Caption = "PDF File";
            this.printPreviewBarItem29.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendPdf;
            this.printPreviewBarItem29.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem29.Description = "Adobe Portable Document Format";
            this.printPreviewBarItem29.Enabled = false;
            this.printPreviewBarItem29.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendPdf;
            this.printPreviewBarItem29.Id = 29;
            this.printPreviewBarItem29.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendPdfLarge;
            this.printPreviewBarItem29.Name = "printPreviewBarItem29";
            superToolTip26.FixedTooltipWidth = true;
            toolTipTitleItem26.Text = "E-Mail As PDF";
            toolTipItem26.LeftIndent = 6;
            toolTipItem26.Text = "Export the document to PDF and attach it to the e-mail.";
            superToolTip26.Items.Add(toolTipTitleItem26);
            superToolTip26.Items.Add(toolTipItem26);
            superToolTip26.MaxWidth = 210;
            this.printPreviewBarItem29.SuperTip = superToolTip26;
            // 
            // printPreviewBarItem30
            // 
            this.printPreviewBarItem30.Caption = "Text File";
            this.printPreviewBarItem30.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendTxt;
            this.printPreviewBarItem30.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem30.Description = "Plain Text";
            this.printPreviewBarItem30.Enabled = false;
            this.printPreviewBarItem30.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendTxt;
            this.printPreviewBarItem30.Id = 30;
            this.printPreviewBarItem30.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendTxtLarge;
            this.printPreviewBarItem30.Name = "printPreviewBarItem30";
            superToolTip27.FixedTooltipWidth = true;
            toolTipTitleItem27.Text = "E-Mail As Text";
            toolTipItem27.LeftIndent = 6;
            toolTipItem27.Text = "Export the document to Text and attach it to the e-mail.";
            superToolTip27.Items.Add(toolTipTitleItem27);
            superToolTip27.Items.Add(toolTipItem27);
            superToolTip27.MaxWidth = 210;
            this.printPreviewBarItem30.SuperTip = superToolTip27;
            // 
            // printPreviewBarItem31
            // 
            this.printPreviewBarItem31.Caption = "CSV File";
            this.printPreviewBarItem31.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendCsv;
            this.printPreviewBarItem31.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem31.Description = "Comma-Separated Values Text";
            this.printPreviewBarItem31.Enabled = false;
            this.printPreviewBarItem31.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendCsv;
            this.printPreviewBarItem31.Id = 31;
            this.printPreviewBarItem31.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendCsvLarge;
            this.printPreviewBarItem31.Name = "printPreviewBarItem31";
            superToolTip28.FixedTooltipWidth = true;
            toolTipTitleItem28.Text = "E-Mail As CSV";
            toolTipItem28.LeftIndent = 6;
            toolTipItem28.Text = "Export the document to CSV and attach it to the e-mail.";
            superToolTip28.Items.Add(toolTipTitleItem28);
            superToolTip28.Items.Add(toolTipItem28);
            superToolTip28.MaxWidth = 210;
            this.printPreviewBarItem31.SuperTip = superToolTip28;
            // 
            // printPreviewBarItem32
            // 
            this.printPreviewBarItem32.Caption = "MHT File";
            this.printPreviewBarItem32.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendMht;
            this.printPreviewBarItem32.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem32.Description = "Single File Web Page";
            this.printPreviewBarItem32.Enabled = false;
            this.printPreviewBarItem32.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendMht;
            this.printPreviewBarItem32.Id = 32;
            this.printPreviewBarItem32.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendMhtLarge;
            this.printPreviewBarItem32.Name = "printPreviewBarItem32";
            superToolTip29.FixedTooltipWidth = true;
            toolTipTitleItem29.Text = "E-Mail As MHT";
            toolTipItem29.LeftIndent = 6;
            toolTipItem29.Text = "Export the document to MHT and attach it to the e-mail.";
            superToolTip29.Items.Add(toolTipTitleItem29);
            superToolTip29.Items.Add(toolTipItem29);
            superToolTip29.MaxWidth = 210;
            this.printPreviewBarItem32.SuperTip = superToolTip29;
            // 
            // printPreviewBarItem33
            // 
            this.printPreviewBarItem33.Caption = "XLS File";
            this.printPreviewBarItem33.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXls;
            this.printPreviewBarItem33.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem33.Description = "Microsoft Excel 2000-2003 Workbook";
            this.printPreviewBarItem33.Enabled = false;
            this.printPreviewBarItem33.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendXls;
            this.printPreviewBarItem33.Id = 33;
            this.printPreviewBarItem33.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendXlsLarge;
            this.printPreviewBarItem33.Name = "printPreviewBarItem33";
            superToolTip30.FixedTooltipWidth = true;
            toolTipTitleItem30.Text = "E-Mail As XLS";
            toolTipItem30.LeftIndent = 6;
            toolTipItem30.Text = "Export the document to XLS and attach it to the e-mail.";
            superToolTip30.Items.Add(toolTipTitleItem30);
            superToolTip30.Items.Add(toolTipItem30);
            superToolTip30.MaxWidth = 210;
            this.printPreviewBarItem33.SuperTip = superToolTip30;
            // 
            // printPreviewBarItem34
            // 
            this.printPreviewBarItem34.Caption = "XLSX File";
            this.printPreviewBarItem34.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendXlsx;
            this.printPreviewBarItem34.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem34.Description = "Microsoft Excel 2007 Workbook";
            this.printPreviewBarItem34.Enabled = false;
            this.printPreviewBarItem34.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendXlsx;
            this.printPreviewBarItem34.Id = 34;
            this.printPreviewBarItem34.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendXlsxLarge;
            this.printPreviewBarItem34.Name = "printPreviewBarItem34";
            superToolTip31.FixedTooltipWidth = true;
            toolTipTitleItem31.Text = "E-Mail As XLSX";
            toolTipItem31.LeftIndent = 6;
            toolTipItem31.Text = "Export the document to XLSX and attach it to the e-mail.";
            superToolTip31.Items.Add(toolTipTitleItem31);
            superToolTip31.Items.Add(toolTipItem31);
            superToolTip31.MaxWidth = 210;
            this.printPreviewBarItem34.SuperTip = superToolTip31;
            // 
            // printPreviewBarItem35
            // 
            this.printPreviewBarItem35.Caption = "RTF File";
            this.printPreviewBarItem35.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendRtf;
            this.printPreviewBarItem35.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem35.Description = "Rich Text Format";
            this.printPreviewBarItem35.Enabled = false;
            this.printPreviewBarItem35.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendRtf;
            this.printPreviewBarItem35.Id = 35;
            this.printPreviewBarItem35.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendRtfLarge;
            this.printPreviewBarItem35.Name = "printPreviewBarItem35";
            superToolTip32.FixedTooltipWidth = true;
            toolTipTitleItem32.Text = "E-Mail As RTF";
            toolTipItem32.LeftIndent = 6;
            toolTipItem32.Text = "Export the document to RTF and attach it to the e-mail.";
            superToolTip32.Items.Add(toolTipTitleItem32);
            superToolTip32.Items.Add(toolTipItem32);
            superToolTip32.MaxWidth = 210;
            this.printPreviewBarItem35.SuperTip = superToolTip32;
            // 
            // printPreviewBarItem36
            // 
            this.printPreviewBarItem36.Caption = "Image File";
            this.printPreviewBarItem36.Command = DevExpress.XtraPrinting.PrintingSystemCommand.SendGraphic;
            this.printPreviewBarItem36.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem36.Description = "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
            this.printPreviewBarItem36.Enabled = false;
            this.printPreviewBarItem36.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendGraphic;
            this.printPreviewBarItem36.Id = 36;
            this.printPreviewBarItem36.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SendGraphicLarge;
            this.printPreviewBarItem36.Name = "printPreviewBarItem36";
            superToolTip33.FixedTooltipWidth = true;
            toolTipTitleItem33.Text = "E-Mail As Image";
            toolTipItem33.LeftIndent = 6;
            toolTipItem33.Text = "Export the document to Image and attach it to the e-mail.";
            superToolTip33.Items.Add(toolTipTitleItem33);
            superToolTip33.Items.Add(toolTipItem33);
            superToolTip33.MaxWidth = 210;
            this.printPreviewBarItem36.SuperTip = superToolTip33;
            // 
            // printPreviewBarItem37
            // 
            this.printPreviewBarItem37.Caption = "PDF File";
            this.printPreviewBarItem37.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportPdf;
            this.printPreviewBarItem37.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem37.Description = "Adobe Portable Document Format";
            this.printPreviewBarItem37.Enabled = false;
            this.printPreviewBarItem37.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportPdf;
            this.printPreviewBarItem37.Id = 37;
            this.printPreviewBarItem37.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportPdfLarge;
            this.printPreviewBarItem37.Name = "printPreviewBarItem37";
            superToolTip34.FixedTooltipWidth = true;
            toolTipTitleItem34.Text = "Export to PDF";
            toolTipItem34.LeftIndent = 6;
            toolTipItem34.Text = "Export the document to PDF and save it to the file on a disk.";
            superToolTip34.Items.Add(toolTipTitleItem34);
            superToolTip34.Items.Add(toolTipItem34);
            superToolTip34.MaxWidth = 210;
            this.printPreviewBarItem37.SuperTip = superToolTip34;
            // 
            // printPreviewBarItem38
            // 
            this.printPreviewBarItem38.Caption = "HTML File";
            this.printPreviewBarItem38.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportHtm;
            this.printPreviewBarItem38.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem38.Description = "Web Page";
            this.printPreviewBarItem38.Enabled = false;
            this.printPreviewBarItem38.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportHtm;
            this.printPreviewBarItem38.Id = 38;
            this.printPreviewBarItem38.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportHtmLarge;
            this.printPreviewBarItem38.Name = "printPreviewBarItem38";
            superToolTip35.FixedTooltipWidth = true;
            toolTipTitleItem35.Text = "Export to HTML";
            toolTipItem35.LeftIndent = 6;
            toolTipItem35.Text = "Export the document to HTML and save it to the file on a disk.";
            superToolTip35.Items.Add(toolTipTitleItem35);
            superToolTip35.Items.Add(toolTipItem35);
            superToolTip35.MaxWidth = 210;
            this.printPreviewBarItem38.SuperTip = superToolTip35;
            // 
            // printPreviewBarItem39
            // 
            this.printPreviewBarItem39.Caption = "Text File";
            this.printPreviewBarItem39.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportTxt;
            this.printPreviewBarItem39.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem39.Description = "Plain Text";
            this.printPreviewBarItem39.Enabled = false;
            this.printPreviewBarItem39.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportTxt;
            this.printPreviewBarItem39.Id = 39;
            this.printPreviewBarItem39.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportTxtLarge;
            this.printPreviewBarItem39.Name = "printPreviewBarItem39";
            superToolTip36.FixedTooltipWidth = true;
            toolTipTitleItem36.Text = "Export to Text";
            toolTipItem36.LeftIndent = 6;
            toolTipItem36.Text = "Export the document to Text and save it to the file on a disk.";
            superToolTip36.Items.Add(toolTipTitleItem36);
            superToolTip36.Items.Add(toolTipItem36);
            superToolTip36.MaxWidth = 210;
            this.printPreviewBarItem39.SuperTip = superToolTip36;
            // 
            // printPreviewBarItem40
            // 
            this.printPreviewBarItem40.Caption = "CSV File";
            this.printPreviewBarItem40.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportCsv;
            this.printPreviewBarItem40.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem40.Description = "Comma-Separated Values Text";
            this.printPreviewBarItem40.Enabled = false;
            this.printPreviewBarItem40.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportCsv;
            this.printPreviewBarItem40.Id = 40;
            this.printPreviewBarItem40.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportCsvLarge;
            this.printPreviewBarItem40.Name = "printPreviewBarItem40";
            superToolTip37.FixedTooltipWidth = true;
            toolTipTitleItem37.Text = "Export to CSV";
            toolTipItem37.LeftIndent = 6;
            toolTipItem37.Text = "Export the document to CSV and save it to the file on a disk.";
            superToolTip37.Items.Add(toolTipTitleItem37);
            superToolTip37.Items.Add(toolTipItem37);
            superToolTip37.MaxWidth = 210;
            this.printPreviewBarItem40.SuperTip = superToolTip37;
            // 
            // printPreviewBarItem41
            // 
            this.printPreviewBarItem41.Caption = "MHT File";
            this.printPreviewBarItem41.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportMht;
            this.printPreviewBarItem41.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem41.Description = "Single File Web Page";
            this.printPreviewBarItem41.Enabled = false;
            this.printPreviewBarItem41.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportMht;
            this.printPreviewBarItem41.Id = 41;
            this.printPreviewBarItem41.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportMhtLarge;
            this.printPreviewBarItem41.Name = "printPreviewBarItem41";
            superToolTip38.FixedTooltipWidth = true;
            toolTipTitleItem38.Text = "Export to MHT";
            toolTipItem38.LeftIndent = 6;
            toolTipItem38.Text = "Export the document to MHT and save it to the file on a disk.";
            superToolTip38.Items.Add(toolTipTitleItem38);
            superToolTip38.Items.Add(toolTipItem38);
            superToolTip38.MaxWidth = 210;
            this.printPreviewBarItem41.SuperTip = superToolTip38;
            // 
            // printPreviewBarItem42
            // 
            this.printPreviewBarItem42.Caption = "XLS File";
            this.printPreviewBarItem42.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXls;
            this.printPreviewBarItem42.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem42.Description = "Microsoft Excel 2000-2003 Workbook";
            this.printPreviewBarItem42.Enabled = false;
            this.printPreviewBarItem42.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportXls;
            this.printPreviewBarItem42.Id = 42;
            this.printPreviewBarItem42.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportXlsLarge;
            this.printPreviewBarItem42.Name = "printPreviewBarItem42";
            superToolTip39.FixedTooltipWidth = true;
            toolTipTitleItem39.Text = "Export to XLS";
            toolTipItem39.LeftIndent = 6;
            toolTipItem39.Text = "Export the document to XLS and save it to the file on a disk.";
            superToolTip39.Items.Add(toolTipTitleItem39);
            superToolTip39.Items.Add(toolTipItem39);
            superToolTip39.MaxWidth = 210;
            this.printPreviewBarItem42.SuperTip = superToolTip39;
            // 
            // printPreviewBarItem43
            // 
            this.printPreviewBarItem43.Caption = "XLSX File";
            this.printPreviewBarItem43.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportXlsx;
            this.printPreviewBarItem43.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem43.Description = "Microsoft Excel 2007 Workbook";
            this.printPreviewBarItem43.Enabled = false;
            this.printPreviewBarItem43.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportXlsx;
            this.printPreviewBarItem43.Id = 43;
            this.printPreviewBarItem43.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportXlsxLarge;
            this.printPreviewBarItem43.Name = "printPreviewBarItem43";
            superToolTip40.FixedTooltipWidth = true;
            toolTipTitleItem40.Text = "Export to XLSX";
            toolTipItem40.LeftIndent = 6;
            toolTipItem40.Text = "Export the document to XLSX and save it to the file on a disk.";
            superToolTip40.Items.Add(toolTipTitleItem40);
            superToolTip40.Items.Add(toolTipItem40);
            superToolTip40.MaxWidth = 210;
            this.printPreviewBarItem43.SuperTip = superToolTip40;
            // 
            // printPreviewBarItem44
            // 
            this.printPreviewBarItem44.Caption = "RTF File";
            this.printPreviewBarItem44.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportRtf;
            this.printPreviewBarItem44.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem44.Description = "Rich Text Format";
            this.printPreviewBarItem44.Enabled = false;
            this.printPreviewBarItem44.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportRtf;
            this.printPreviewBarItem44.Id = 44;
            this.printPreviewBarItem44.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportRtfLarge;
            this.printPreviewBarItem44.Name = "printPreviewBarItem44";
            superToolTip41.FixedTooltipWidth = true;
            toolTipTitleItem41.Text = "Export to RTF";
            toolTipItem41.LeftIndent = 6;
            toolTipItem41.Text = "Export the document to RTF and save it to the file on a disk.";
            superToolTip41.Items.Add(toolTipTitleItem41);
            superToolTip41.Items.Add(toolTipItem41);
            superToolTip41.MaxWidth = 210;
            this.printPreviewBarItem44.SuperTip = superToolTip41;
            // 
            // printPreviewBarItem45
            // 
            this.printPreviewBarItem45.Caption = "Image File";
            this.printPreviewBarItem45.Command = DevExpress.XtraPrinting.PrintingSystemCommand.ExportGraphic;
            this.printPreviewBarItem45.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem45.Description = "BMP, GIF, JPEG, PNG, TIFF, EMF, WMF";
            this.printPreviewBarItem45.Enabled = false;
            this.printPreviewBarItem45.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportGraphic;
            this.printPreviewBarItem45.Id = 45;
            this.printPreviewBarItem45.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportGraphicLarge;
            this.printPreviewBarItem45.Name = "printPreviewBarItem45";
            superToolTip42.FixedTooltipWidth = true;
            toolTipTitleItem42.Text = "Export to Image";
            toolTipItem42.LeftIndent = 6;
            toolTipItem42.Text = "Export the document to Image and save it to the file on a disk.";
            superToolTip42.Items.Add(toolTipTitleItem42);
            superToolTip42.Items.Add(toolTipItem42);
            superToolTip42.MaxWidth = 210;
            this.printPreviewBarItem45.SuperTip = superToolTip42;
            // 
            // printPreviewBarItem46
            // 
            this.printPreviewBarItem46.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.printPreviewBarItem46.Caption = "Thumbnails";
            this.printPreviewBarItem46.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Thumbnails;
            this.printPreviewBarItem46.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem46.Enabled = false;
            this.printPreviewBarItem46.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Thumbnails;
            this.printPreviewBarItem46.Id = 56;
            this.printPreviewBarItem46.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ThumbnailsLarge;
            this.printPreviewBarItem46.Name = "printPreviewBarItem46";
            superToolTip46.FixedTooltipWidth = true;
            toolTipTitleItem46.Text = "Thumbnails";
            toolTipItem46.LeftIndent = 6;
            toolTipItem46.Text = "Open the Thumbnails, which allows you to navigate through the document.";
            superToolTip46.Items.Add(toolTipTitleItem46);
            superToolTip46.Items.Add(toolTipItem46);
            superToolTip46.MaxWidth = 210;
            this.printPreviewBarItem46.SuperTip = superToolTip46;

            // 
            // barItemOpen
            // 
            this.barItemOpen.Caption = "Open";
            this.barItemOpen.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Open;
            this.barItemOpen.ContextSpecifier = this.fPrintRibbonController;
            this.barItemOpen.Enabled = false;
            this.barItemOpen.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Open;
            this.barItemOpen.Id = 46;
            this.barItemOpen.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_OpenLarge;
            this.barItemOpen.Name = "barItemOpen";
            superToolTip43.FixedTooltipWidth = true;
            toolTipTitleItem43.Text = "Open (Ctrl + O)";
            toolTipItem43.LeftIndent = 6;
            toolTipItem43.Text = "Open a document.";
            superToolTip43.Items.Add(toolTipTitleItem43);
            superToolTip43.Items.Add(toolTipItem43);
            superToolTip43.MaxWidth = 210;
            this.barItemOpen.SuperTip = superToolTip43;
            // 
            // barItemSave
            // 
            this.barItemSave.Caption = "Save";
            this.barItemSave.Command = DevExpress.XtraPrinting.PrintingSystemCommand.Save;
            this.barItemSave.ContextSpecifier = this.fPrintRibbonController;
            this.barItemSave.Enabled = false;
            this.barItemSave.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Save;
            this.barItemSave.Id = 47;
            this.barItemSave.LargeGlyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_SaveLarge;
            this.barItemSave.Name = "barItemSave";
            superToolTip44.FixedTooltipWidth = true;
            toolTipTitleItem44.Text = "Save (Ctrl + S)";
            toolTipItem44.LeftIndent = 6;
            toolTipItem44.Text = "Save the document.";
            superToolTip44.Items.Add(toolTipTitleItem44);
            superToolTip44.Items.Add(toolTipItem44);
            superToolTip44.MaxWidth = 210;
            this.barItemSave.SuperTip = superToolTip44;
            // 
            // printPreviewStaticItem1
            // 
            this.printPreviewStaticItem1.Caption = "Nothing";
            this.printPreviewStaticItem1.Id = 48;
            this.printPreviewStaticItem1.LeftIndent = 1;
            this.printPreviewStaticItem1.Name = "printPreviewStaticItem1";
            this.printPreviewStaticItem1.RightIndent = 1;
            this.printPreviewStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.printPreviewStaticItem1.Type = "PageOfPages";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Id = 49;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            this.barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
            // 
            // progressBarEditItem1
            // 
            this.progressBarEditItem1.ContextSpecifier = this.fPrintRibbonController;
            this.progressBarEditItem1.Edit = this.repositoryItemProgressBar1;
            this.progressBarEditItem1.EditHeight = 12;
            this.progressBarEditItem1.Id = 50;
            this.progressBarEditItem1.Name = "progressBarEditItem1";
            this.progressBarEditItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.progressBarEditItem1.Width = 150;
            // 
            // repositoryItemProgressBar1
            // 
            this.repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // printPreviewBarItem48
            // 
            this.printPreviewBarItem48.Caption = "Stop";
            this.printPreviewBarItem48.Command = DevExpress.XtraPrinting.PrintingSystemCommand.StopPageBuilding;
            this.printPreviewBarItem48.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewBarItem48.Enabled = false;
            this.printPreviewBarItem48.Id = 51;
            this.printPreviewBarItem48.Name = "printPreviewBarItem48";
            this.printPreviewBarItem48.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
            this.barButtonItem1.Enabled = false;
            this.barButtonItem1.Id = 52;
            this.barButtonItem1.Name = "barButtonItem1";
            this.barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.OnlyInRuntime;
            // 
            // printPreviewStaticItem2
            // 
            this.printPreviewStaticItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.printPreviewStaticItem2.AutoSize = DevExpress.XtraBars.BarStaticItemSize.None;
            this.printPreviewStaticItem2.Caption = "100%";
            this.printPreviewStaticItem2.Id = 53;
            this.printPreviewStaticItem2.Name = "printPreviewStaticItem2";
            this.printPreviewStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            this.printPreviewStaticItem2.Type = "ZoomFactorText";
            this.printPreviewStaticItem2.Width = 42;
            // 
            // zoomTrackBarEditItem1
            // 
            this.zoomTrackBarEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.zoomTrackBarEditItem1.ContextSpecifier = this.fPrintRibbonController;
            this.zoomTrackBarEditItem1.Edit = this.repositoryItemZoomTrackBar1;
            this.zoomTrackBarEditItem1.EditValue = 90;
            this.zoomTrackBarEditItem1.Enabled = false;
            this.zoomTrackBarEditItem1.Id = 54;
            this.zoomTrackBarEditItem1.Name = "zoomTrackBarEditItem1";
            this.zoomTrackBarEditItem1.Range = new int[] {
        10,
        500};
            this.zoomTrackBarEditItem1.Width = 140;
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.Alignment = DevExpress.Utils.VertAlignment.Center;
            this.repositoryItemZoomTrackBar1.AllowFocused = false;
            this.repositoryItemZoomTrackBar1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.repositoryItemZoomTrackBar1.Maximum = 180;
            this.repositoryItemZoomTrackBar1.Middle = 5;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            // 
            // bbiRefresh
            // 
            this.bbiRefresh.Caption = "&Refresh";
            this.bbiRefresh.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.Glyph")));
            this.bbiRefresh.Hint = "Refresh the preview";
            this.bbiRefresh.Id = 55;
            this.bbiRefresh.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiRefresh.LargeGlyph")));
            this.bbiRefresh.Name = "bbiRefresh";
            this.bbiRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiRefresh_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.ContextSpecifier = this.fPrintRibbonController;
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.pageGroupDocument,
            this.pageGroupPrint,
            this.pageGroupPageSetup,
            this.printPreviewRibbonPageGroup4,
            this.printPreviewRibbonPageGroup5,
            this.printPreviewRibbonPageGroup6,
            this.printPreviewRibbonPageGroup7});
            this.ribbonPage1.Name = "ribbonPage1";
            this.ribbonPage1.Text = "Print Preview";
            // 
            // pageGroupDocument
            // 
            this.pageGroupDocument.AllowTextClipping = false; 
            this.pageGroupDocument.ContextSpecifier = this.fPrintRibbonController;
            this.pageGroupDocument.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Document;
            this.pageGroupDocument.ItemLinks.Add(this.barItemOpen);
            this.pageGroupDocument.ItemLinks.Add(this.barItemSave);
            this.pageGroupDocument.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Document;
            this.pageGroupDocument.Name = "pageGroupDocument";
            this.pageGroupDocument.ShowCaptionButton = false;
            this.pageGroupDocument.Text = "Document";
            // 
            // pageGroupPrint
            // 
            this.pageGroupPrint.AllowTextClipping = false;
            this.pageGroupPrint.ContextSpecifier = this.fPrintRibbonController;
            this.pageGroupPrint.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PrintDirect;
            this.pageGroupPrint.ItemLinks.Add(this.printPreviewBarItem5);
            this.pageGroupPrint.ItemLinks.Add(this.printPreviewBarItem6);
            this.pageGroupPrint.ItemLinks.Add(this.barItemOptions);
            this.pageGroupPrint.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Print;
            this.pageGroupPrint.Name = "pageGroupPrint";
            this.pageGroupPrint.ShowCaptionButton = false;
            this.pageGroupPrint.Text = "Print";
            // 
            // pageGroupPageSetup
            // 
            this.pageGroupPageSetup.AllowTextClipping = false;
            this.pageGroupPageSetup.ContextSpecifier = this.fPrintRibbonController;
            this.pageGroupPageSetup.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_PageMargins;
            this.pageGroupPageSetup.ItemLinks.Add(this.printPreviewBarItem8);
            this.pageGroupPageSetup.ItemLinks.Add(this.printPreviewBarItem9);
            this.pageGroupPageSetup.ItemLinks.Add(this.printPreviewBarItem28);
            this.pageGroupPageSetup.ItemLinks.Add(this.printPreviewBarItem26);
            this.pageGroupPageSetup.ItemLinks.Add(this.printPreviewBarItem27);
            this.pageGroupPageSetup.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.PageSetup;
            this.pageGroupPageSetup.Name = "pageGroupPageSetup";
            superToolTip45.FixedTooltipWidth = true;
            toolTipTitleItem45.Text = "Page Setup";
            toolTipItem45.LeftIndent = 6;
            toolTipItem45.Text = "Show the Page Setup dialog.";
            superToolTip45.Items.Add(toolTipTitleItem45);
            superToolTip45.Items.Add(toolTipItem45);
            superToolTip45.MaxWidth = 210;
            this.pageGroupPageSetup.SuperTip = superToolTip45;
            this.pageGroupPageSetup.Text = "Page Setup";
            // 
            // printPreviewRibbonPageGroup4
            // 
            this.printPreviewRibbonPageGroup4.AllowTextClipping = false;
            this.printPreviewRibbonPageGroup4.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewRibbonPageGroup4.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Find;
            this.printPreviewRibbonPageGroup4.ItemLinks.Add(this.printPreviewBarItem3);
            this.printPreviewRibbonPageGroup4.ItemLinks.Add(this.printPreviewBarItem46);
            this.printPreviewRibbonPageGroup4.ItemLinks.Add(this.printPreviewBarItem16, true);
            this.printPreviewRibbonPageGroup4.ItemLinks.Add(this.printPreviewBarItem17);
            this.printPreviewRibbonPageGroup4.ItemLinks.Add(this.printPreviewBarItem18);
            this.printPreviewRibbonPageGroup4.ItemLinks.Add(this.printPreviewBarItem19);
            this.printPreviewRibbonPageGroup4.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Navigation;
            this.printPreviewRibbonPageGroup4.Name = "printPreviewRibbonPageGroup4";
            this.printPreviewRibbonPageGroup4.ShowCaptionButton = false;
            this.printPreviewRibbonPageGroup4.Text = "Navigation";
            // 
            // printPreviewRibbonPageGroup5
            // 
            this.printPreviewRibbonPageGroup5.AllowTextClipping = false;
            this.printPreviewRibbonPageGroup5.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewRibbonPageGroup5.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Zoom;
            this.printPreviewRibbonPageGroup5.ItemLinks.Add(this.printPreviewBarItem10);
            this.printPreviewRibbonPageGroup5.ItemLinks.Add(this.printPreviewBarItem11);
            this.printPreviewRibbonPageGroup5.ItemLinks.Add(this.printPreviewBarItem12);
            this.printPreviewRibbonPageGroup5.ItemLinks.Add(this.printPreviewBarItem20);
            this.printPreviewRibbonPageGroup5.ItemLinks.Add(this.printPreviewBarItem13);
            this.printPreviewRibbonPageGroup5.ItemLinks.Add(this.printPreviewBarItem15);
            this.printPreviewRibbonPageGroup5.ItemLinks.Add(this.printPreviewBarItem14);
            this.printPreviewRibbonPageGroup5.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Zoom;
            this.printPreviewRibbonPageGroup5.Name = "printPreviewRibbonPageGroup5";
            this.printPreviewRibbonPageGroup5.ShowCaptionButton = false;
            this.printPreviewRibbonPageGroup5.Text = "Zoom";
            // 
            // printPreviewRibbonPageGroup6
            // 
            this.printPreviewRibbonPageGroup6.AllowTextClipping = false;
            this.printPreviewRibbonPageGroup6.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewRibbonPageGroup6.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_Watermark;
            this.printPreviewRibbonPageGroup6.ItemLinks.Add(this.printPreviewBarItem21);
            this.printPreviewRibbonPageGroup6.ItemLinks.Add(this.printPreviewBarItem22);
            this.printPreviewRibbonPageGroup6.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Background;
            this.printPreviewRibbonPageGroup6.Name = "printPreviewRibbonPageGroup6";
            this.printPreviewRibbonPageGroup6.ShowCaptionButton = false;
            this.printPreviewRibbonPageGroup6.Text = "Page Background";
            // 
            // printPreviewRibbonPageGroup7
            // 
            this.printPreviewRibbonPageGroup7.AllowTextClipping = false;
            this.printPreviewRibbonPageGroup7.ContextSpecifier = this.fPrintRibbonController;
            this.printPreviewRibbonPageGroup7.Glyph = global::XtraPrintingDemos.PrintRibbonControllerResources.RibbonPrintPreview_ExportFile;
            this.printPreviewRibbonPageGroup7.ItemLinks.Add(this.printPreviewBarItem23);
            this.printPreviewRibbonPageGroup7.ItemLinks.Add(this.printPreviewBarItem24);
            this.printPreviewRibbonPageGroup7.Kind = DevExpress.XtraPrinting.Preview.PrintPreviewRibbonPageGroupKind.Export;
            this.printPreviewRibbonPageGroup7.Name = "printPreviewRibbonPageGroup7";
            this.printPreviewRibbonPageGroup7.ShowCaptionButton = false;
            this.printPreviewRibbonPageGroup7.Text = "Export";
            // 
            // ribbonPageActions
            // 
            this.ribbonPageActions.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonGroupActions});
            this.ribbonPageActions.Name = "ribbonPageActions";
            this.ribbonPageActions.Text = "Demo Actions";
            // 
            // ribbonGroupActions
            // 
            this.ribbonGroupActions.AllowTextClipping = false;
            this.ribbonGroupActions.ItemLinks.Add(this.bbiRefresh);
            this.ribbonGroupActions.Name = "ribbonGroupActions";
            this.ribbonGroupActions.ShowCaptionButton = false;
            this.ribbonGroupActions.Text = "Actions";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.printPreviewStaticItem1);
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItem1, true);
            this.ribbonStatusBar1.ItemLinks.Add(this.progressBarEditItem1);
            this.ribbonStatusBar1.ItemLinks.Add(this.printPreviewBarItem48);
            this.ribbonStatusBar1.ItemLinks.Add(this.barButtonItem1);
            this.ribbonStatusBar1.ItemLinks.Add(this.printPreviewStaticItem2);
            this.ribbonStatusBar1.ItemLinks.Add(this.zoomTrackBarEditItem1);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 474);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1211, 27);
            // 
            // PreviewControl
            // 
            this.Controls.Add(this.pControl);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl);
            this.Name = "PreviewControl";
            this.Size = new System.Drawing.Size(1211, 501);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemProgressBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CreateDocument();
        }
        // needed for tests
        PrintingSystem GetPrintingSystem() {
            return printingSystem;
        }
	}
}
