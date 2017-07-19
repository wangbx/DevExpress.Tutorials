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
using System.Collections.Generic;

namespace XtraPrintingDemos.DataGridPrinting
{
	public class PreviewControl : PreviewDockPanelControl
	{
        private string connectionStr;
		private DataGridLink printLink;

        private System.ComponentModel.Container components = null;
        private DevExpress.XtraBars.BarListItem barListTables;
		private System.Windows.Forms.DataGrid dataGrid;
        string ConnectionString {
            get { 
                if(string.IsNullOrEmpty(connectionStr)) {
                    string path = XtraPrintingDemos.Helper.GetRelativePath("..\\..\\..\\Data\\nwind.mdb");
                    if(!string.IsNullOrEmpty(path))
                        connectionStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path;
                }
                return connectionStr;
            }
        }

		public PreviewControl()
		{
			InitializeComponent();
            HideButtonOptions();

			dataGrid = new System.Windows.Forms.DataGrid();
			dataGrid.DataMember = "";
			dataGrid.HeaderForeColor = System.Drawing.SystemColors.ControlText;

			printLink = new DataGridLink();
			printLink.PrintingSystem = printingSystem;

			AddControlToDockPanel(dataGrid, "DataGrid");
			fDockPanel.Dock = DockingStyle.Bottom;

            ApplyTableNames(GetTableNames());
        }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewControl));
            this.barListTables = new DevExpress.XtraBars.BarListItem();
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
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barListTables});
            this.ribbonControl.MaxItemId = 57;
            this.ribbonControl.Size = new System.Drawing.Size(1211, 142);
            // 
            // ribbonGroupActions
            // 
            this.ribbonGroupActions.ItemLinks.Add(this.barListTables);
            // 
            // barListTables
            // 
            this.barListTables.Caption = "Table";
            this.barListTables.Glyph = ((System.Drawing.Image)(resources.GetObject("barListTables.Glyph")));
            this.barListTables.Hint = "Select a data table";
            this.barListTables.Id = 56;
            this.barListTables.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("barListTables.LargeGlyph")));
            this.barListTables.Name = "barListTables";
            this.barListTables.ListItemClick += new DevExpress.XtraBars.ListItemClickEventHandler(this.barListItem1_ListItemClick);
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
            UpdateDataGrid(barListTables.Caption);
            CreateDocument();
        }
		protected override void CreateDocumentCore() {
			printLink.DataGrid = dataGrid;
			printLink.CreateDocument(printingSystem);
		}
        private string[] GetTableNames() {
            List<string> names = new List<string>();
            if(!string.IsNullOrEmpty(ConnectionString)) {
                using(OleDbConnection oleCon = new OleDbConnection(ConnectionString)) {
                    oleCon.Open();
                    using(DataTable table = oleCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null)) {
                        foreach(DataRow row in table.Rows) {
                            if(row["TABLE_TYPE"].ToString() != "TABLE")
                                continue;
                            string tableName = row["TABLE_NAME"].ToString();
                            if(!string.IsNullOrEmpty(tableName))
                                names.Add(tableName);
                        }
                    }
                }
            }
            return names.ToArray();
        }
		private void ApplyTableNames(string[] tableNames) {
            foreach(string name in tableNames)
                barListTables.Strings.Add(name + "\r\n");
            if(barListTables.Strings.Count > 0)
                barListTables.Caption = barListTables.Strings[0];
		}
		private void UpdateDataGrid(string tableName) {
            if(!string.IsNullOrEmpty(ConnectionString)) {
                tableName = tableName.Trim('\r', '\n');
                DataSet dataSet1 = new DataSet();
                string columns = string.Equals(tableName, "categories", StringComparison.OrdinalIgnoreCase) ?
                    "CategoryID, CategoryName, Description, Picture" : "*";
                OleDbDataAdapter work1 = new OleDbDataAdapter(String.Format("SELECT {0} FROM [{1}]", columns, tableName), ConnectionString);
                work1.Fill(dataSet1, tableName);
                dataGrid.DataSource = dataSet1.Tables[tableName];
            }
			dataGrid.CaptionText = tableName;
		}
		private void bbiRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
			CreateDocument();
		}
        private void barListItem1_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e) {
            barListTables.Caption = barListTables.Strings[e.Index];
            UpdateDataGrid(barListTables.Caption);
        }
	}
}
