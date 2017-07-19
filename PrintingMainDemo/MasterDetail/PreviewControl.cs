using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;

namespace XtraPrintingDemos.HierarchicalReport {
	public class PreviewControl : XtraPrintingDemos.PreviewControl {
		private MasterDetailControl masterDetailControl;

        public override DocumentViewer PrintControl {
			get {
				if(masterDetailControl != null)
					return masterDetailControl.PrintControl;
				return base.PrintControl;
			}
		}
		protected override ContainerControl PrintBarManagerForm { get { return this; }
		}
        protected override PrintPreviewFormEx PreviewForm {
            get { return ((PrintingSystem)PrintControl.PrintingSystem).PreviewFormEx; }
		}

		public PreviewControl() {
			PrintControl.Visible = false;
            InitializeComponent();
            HideButtonOptions();
            fPrintRibbonController.PrintControl = masterDetailControl.PrintControl;
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.masterDetailControl = new XtraPrintingDemos.HierarchicalReport.MasterDetailControl();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.SuspendLayout();
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
            // masterDetailControl
            // 
            this.masterDetailControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterDetailControl.Location = new System.Drawing.Point(0, 142);
            this.masterDetailControl.Name = "masterDetailControl";
            this.masterDetailControl.PrintingSystem = this.printingSystem;
            this.masterDetailControl.Size = new System.Drawing.Size(1211, 332);
            this.masterDetailControl.TabIndex = 6;
            this.masterDetailControl.PrintControlChanged += new System.EventHandler(this.masterDetailControl1_PrintControlChanged);
            // 
            // PreviewControl
            // 
            this.Controls.Add(this.masterDetailControl);
            this.Name = "PreviewControl";
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar1, 0);
            this.Controls.SetChildIndex(this.masterDetailControl, 0);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void masterDetailControl1_PrintControlChanged(object sender, EventArgs e) {
            fPrintRibbonController.PrintControl = masterDetailControl.PrintControl;
		}
	}
}
