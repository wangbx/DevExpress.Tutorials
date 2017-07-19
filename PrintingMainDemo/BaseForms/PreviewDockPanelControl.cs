using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using System.Reflection;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Control;

namespace XtraPrintingDemos
{
	/// <summary>
	/// Summary description for PreviewDockPanelControl.
	/// </summary>
	public class PreviewDockPanelControl : PreviewControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private DevExpress.XtraBars.Docking.DockManager dockManager1;
		protected DevExpress.XtraBars.Docking.DockPanel fDockPanel = null;

		public DockPanel DockPanel { get { return null; }
		}

		public PreviewDockPanelControl()
		{
			InitializeComponent();
			CreateDockPanel();		
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
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
		private void InitializeComponent()
		{
			this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager();
			((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager1
			// 
//			this.ribbonControl.doc.DockManager = this.dockManager1;
			// 
			// dockManager1
			// 
			this.dockManager1.TopZIndexControls.AddRange(new string[] {
																		  "DevExpress.XtraBars.BarDockControl",
																		  "System.Windows.Forms.StatusBar"});
			// 
			// PreviewDockPanelControl
			// 
			this.Name = "PreviewDockPanelControl";
			((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		protected void AddControlToDockPanel(Control control, string captionText) {
			control.Dock = DockStyle.Fill;
			fDockPanel.ControlContainer.Controls.Add(control);
			fDockPanel.Text = captionText;
		}
		private void CreateDockPanel() {
			dockManager1.Form = this;
			fDockPanel = dockManager1.AddPanel(DockingStyle.Left);
			fDockPanel.Options.ShowCloseButton = false;
		}
	}
}
