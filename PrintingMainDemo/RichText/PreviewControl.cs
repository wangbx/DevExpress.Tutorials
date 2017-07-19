using System;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraPrintingLinks;
using System.IO;

namespace XtraPrintingDemos.RichRext
{
	/// <summary>
	/// Summary description for PreviewControl.
	/// </summary>
	public class PreviewControl : PreviewDockPanelControl
	{
		public class MyRichTextBox : RichTextBox {
			protected override void WndProc(ref Message m) {
				const int WM_SETFONT = 0x30;
				switch (m.Msg) {
					case WM_SETFONT:
						return;
				}
				base.WndProc(ref m);
			}
		}
		RichTextBoxLink link;
		private DevExpress.XtraBars.BarButtonItem bbiLoadFile;
		RichTextBox box;

		public PreviewControl()
		{
			InitializeComponent();
            HideButtonOptions();

			box = new MyRichTextBox();
			box.Dock = DockStyle.Fill;

			fDockPanel.Dock = DockingStyle.Bottom;
			fDockPanel.ControlContainer.Controls.Add(box);
			fDockPanel.Height = 150;

			link = new RichTextBoxLink();
			link.RichTextBox = box;
			link.PrintingSystem = printingSystem;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(link != null) {
					link.Dispose();
					link = null;
				}
			}
			base.Dispose( disposing );
		}

		public override void Activate() {
			System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("XtraPrintingDemos.BaseForms.PrintingAbout.rtf");
			box.LoadFile(stream,RichTextBoxStreamType.RichText);
			base.Activate();
		}
		protected override void ActivateCore() {
			base.ActivateCore();
			CreateDocument();
		}

		protected override void CreateDocumentCore() {
			link.CreateDocument();
		}
        void bbiLoadFile_ItemClick(object sender, ItemClickEventArgs e) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Rich Text Format|*.rtf|Text Files|*.txt";
            if(dialog.ShowDialog() == DialogResult.OK) {
                RichTextBoxStreamType? rtFileType = GetRichTextFileTypeFromExtension(dialog.FileName);
                if(rtFileType != null) {
                    box.LoadFile(dialog.FileName, rtFileType.Value);
                }
            }
        }
        RichTextBoxStreamType? GetRichTextFileTypeFromExtension(string pathStr) {
            if(Path.GetExtension(pathStr).Equals(".rtf", StringComparison.InvariantCultureIgnoreCase))
                return RichTextBoxStreamType.RichText;
            if(Path.GetExtension(pathStr).Equals(".txt", StringComparison.InvariantCultureIgnoreCase))
                return RichTextBoxStreamType.PlainText;
            return null;
        }
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewControl));
            this.bbiLoadFile = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            this.SuspendLayout();
            // 
            // fDockPanel
            // 
            this.fDockPanel.Options.ShowCloseButton = false;
            this.fDockPanel.Size = new System.Drawing.Size(200, 227);
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiLoadFile});
            this.ribbonControl.Size = new System.Drawing.Size(896, 142);
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 369);
            this.ribbonStatusBar1.Size = new System.Drawing.Size(896, 27);
            // 
            // ribbonActionsGroup
            // 
            this.ribbonGroupActions.ItemLinks.Add(this.bbiLoadFile);
            // 
            // bbiLoadFile
            // 
            this.bbiLoadFile.Caption = "&Open";
            this.bbiLoadFile.Glyph = ((System.Drawing.Image)(resources.GetObject("bbiLoadFile.Glyph")));
            this.bbiLoadFile.Hint = "Open a file";
            this.bbiLoadFile.Id = 2;
            this.bbiLoadFile.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bbiLoadFile.LargeGlyph")));
            this.bbiLoadFile.Name = "bbiLoadFile";
            this.bbiLoadFile.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiLoadFile_ItemClick);
            // 
            // PreviewControl
            // 
            this.Name = "PreviewControl";
            this.Size = new System.Drawing.Size(896, 396);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            this.ResumeLayout(false);

		}
	}
}
