using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraEditors;

namespace XtraPrintingDemos.PrintableList {
	public class PreviewControl : PreviewDockPanelControl {
		private XtraPrintingDemos.PrintableList.PrintableListView printableListView;
		private DevExpress.XtraPrinting.PrintableComponentLink printableComponentLink;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.ComponentModel.IContainer components;

		public PreviewControl() {
			InitializeComponent();
            fDockPanel.Dock = DockingStyle.Bottom;

			CreateLink();
			CreatePrinableListView();
			AddControlToDockPanel(printableListView, "ListView");

			printableComponentLink.Component = printableListView;
		}

		protected override void Dispose( bool disposing ) {
			if( disposing ) {
                printableListView.StyleChanged -= new EventHandler(printableListView_StyleChanged);
                printableListView.SizeChanged -= new System.EventHandler(printableListView_StyleChanged);
                if(components != null) {
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewControl));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.SuspendLayout();
            // 
            // fDockPanel
            // 
            this.fDockPanel.Options.ShowCloseButton = false;
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Size = new System.Drawing.Size(1211, 142);
            // 
            // ribbonPageActions
            // 
            this.ribbonPageActions.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            // 
            // PreviewControl
            // 
            this.Name = "PreviewControl";
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		protected override void ActivateCore() {
			base.ActivateCore();
			CreateDocument();
		}
		protected override void CreateDocumentCore() {
            if(printableComponentLink.Component != null && printableListView.IsHandleCreated) {
				printableComponentLink.CreateDocument();
			}
		}
		private void CreateLink() {
			printableComponentLink = new DevExpress.XtraPrinting.PrintableComponentLink();
			printableComponentLink.PrintingSystem = printingSystem;
			printableComponentLink.SkipArea = DevExpress.XtraPrinting.BrickModifier.None;
		}
		private void CreatePrinableListView() {
			columnHeader1 = new System.Windows.Forms.ColumnHeader();
			columnHeader2 = new System.Windows.Forms.ColumnHeader();
			columnHeader3 = new System.Windows.Forms.ColumnHeader();

			columnHeader1.Text = "Country";
			columnHeader1.Width = 99;

			columnHeader2.Text = "Currency";
			columnHeader2.Width = 129;

			columnHeader3.Text = "Capital";
			columnHeader3.Width = 81;

			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Belgium", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(1)))),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Belgian Franc"),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Brussels")}, 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Brazil", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(1)))),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Real"),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Brasilia")}, 1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Canada", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(1)))),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Canadian Dollar"),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Ottawa")}, 2);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Denmark", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(1)))),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Krone"),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Copenhagen")}, 3);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new System.Windows.Forms.ListViewItem.ListViewSubItem[] {
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Finland", System.Drawing.SystemColors.WindowText, System.Drawing.SystemColors.Window, new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(1)))),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Markka"),
																																								new System.Windows.Forms.ListViewItem.ListViewSubItem(null, "Helsinki")}, 4);

			printableListView = new XtraPrintingDemos.PrintableList.PrintableListView();
			printableListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.columnHeader1,
																						   this.columnHeader2,
																						   this.columnHeader3});
			printableListView.GridLines = true;
			printableListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
																						 listViewItem1,
																						 listViewItem2,
																						 listViewItem3,
																						 listViewItem4,
																						 listViewItem5});
			printableListView.LargeImageList = this.imageList2;
			printableListView.SmallImageList = this.imageList1;
			printableListView.View = System.Windows.Forms.View.Details;

			printableListView.StyleChanged += new EventHandler(printableListView_StyleChanged);
			printableListView.SizeChanged += new System.EventHandler(printableListView_StyleChanged);
		}
		private void printableListView_StyleChanged(object sender, EventArgs e) {
			CreateDocument();
		}
	}
}
