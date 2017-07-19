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
using System.Drawing.Printing;

namespace XtraPrintingDemos.BioLifePrinting {
    public abstract class PreviewControl : PreviewDockPanelControl {
        private string DBFileName;

        private PanelControl pnlNavigation;
        private PanelControl panel2;
        protected CheckEdit chAllRecords;
        private SimpleButton btnFirst;
        private SimpleButton btnPrev;
        private SimpleButton btnNext;
        private SimpleButton btnLast;
        private PanelControl panel1;
        private MemoEdit txtNotes;
        private LabelControl label8;
        private PictureEdit picFish;
        private CheckEdit chMark;
        private LabelControl label6;
        private TextEdit txtSpeciesName;
        private LabelControl label5;
        private TextEdit txtCommonName;
        private LabelControl label4;
        private TextEdit txtCategory;
        private TextEdit txtLength;
        private LabelControl label3;
        private TextEdit txtSpeciesNo;
        private LabelControl label2;
        private TextEdit txtID;
        private LabelControl label1;
        private System.ComponentModel.Container components = null;

        protected DataView dataView;
        protected BioLifePrintingLink bioLifeLink;

        public PreviewControl() {
            InitializeComponent();

            printingSystem.PageSettings.PaperKind = PaperKind.Letter;
            printingSystem.PageSettings.LeftMargin = 97;
            printingSystem.PageSettings.TopMargin = 97;
            printingSystem.PageSettings.RightMargin = 97;
            printingSystem.PageSettings.BottomMargin = 97;
            
            fDockPanel.Dock = DockingStyle.Bottom;
            fDockPanel.Visibility = DockVisibility.AutoHide;
            fDockPanel.Height = pnlNavigation.Height + 30;
            AddControlToDockPanel(pnlNavigation, "Navigation");

            DBFileName = XtraPrintingDemos.Helper.GetRelativePath("..\\..\\..\\Data\\BioLife.xml");
            if(DBFileName != "") {
                CreateDataView();
                Binding();
                bioLifeLink = new BioLifePrintingLink(printingSystem, null);
            }

        }
        protected override void Dispose(bool disposing) {
            if(disposing) {
                if(components != null) {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PreviewControl));
            this.pnlNavigation = new DevExpress.XtraEditors.PanelControl();
            this.panel2 = new DevExpress.XtraEditors.PanelControl();
            this.chAllRecords = new DevExpress.XtraEditors.CheckEdit();
            this.btnFirst = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrev = new DevExpress.XtraEditors.SimpleButton();
            this.btnNext = new DevExpress.XtraEditors.SimpleButton();
            this.btnLast = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new DevExpress.XtraEditors.PanelControl();
            this.txtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.label8 = new DevExpress.XtraEditors.LabelControl();
            this.picFish = new DevExpress.XtraEditors.PictureEdit();
            this.chMark = new DevExpress.XtraEditors.CheckEdit();
            this.label6 = new DevExpress.XtraEditors.LabelControl();
            this.txtSpeciesName = new DevExpress.XtraEditors.TextEdit();
            this.label5 = new DevExpress.XtraEditors.LabelControl();
            this.txtCommonName = new DevExpress.XtraEditors.TextEdit();
            this.label4 = new DevExpress.XtraEditors.LabelControl();
            this.txtCategory = new DevExpress.XtraEditors.TextEdit();
            this.txtLength = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new DevExpress.XtraEditors.LabelControl();
            this.txtSpeciesNo = new DevExpress.XtraEditors.TextEdit();
            this.label2 = new DevExpress.XtraEditors.LabelControl();
            this.txtID = new DevExpress.XtraEditors.TextEdit();
            this.label1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlNavigation)).BeginInit();
            this.pnlNavigation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chAllRecords.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFish.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chMark.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpeciesName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommonName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLength.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpeciesNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // fDockPanel
            // 
            this.fDockPanel.Options.ShowCloseButton = false;
            this.fDockPanel.Size = new System.Drawing.Size(200, 248);
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            // 
            // pnlNavigation
            // 
            this.pnlNavigation.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlNavigation.Controls.Add(this.panel2);
            this.pnlNavigation.Controls.Add(this.panel1);
            this.pnlNavigation.Location = new System.Drawing.Point(16, 8);
            this.pnlNavigation.Name = "pnlNavigation";
            this.pnlNavigation.Size = new System.Drawing.Size(504, 280);
            this.pnlNavigation.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panel2.Controls.Add(this.chAllRecords);
            this.panel2.Controls.Add(this.btnFirst);
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.btnLast);
            this.panel2.Location = new System.Drawing.Point(0, 250);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(498, 24);
            this.panel2.TabIndex = 3;
            // 
            // chAllRecords
            // 
            this.chAllRecords.EditValue = true;
            this.chAllRecords.Location = new System.Drawing.Point(8, 4);
            this.chAllRecords.Name = "chAllRecords";
            this.chAllRecords.Properties.Caption = "All Records";
            this.chAllRecords.Size = new System.Drawing.Size(88, 19);
            this.chAllRecords.TabIndex = 0;
            this.chAllRecords.CheckedChanged += new EventHandler(chAllRecords_CheckedChanged);
            // 
            // btnFirst
            // 
            this.btnFirst.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnFirst.Location = new System.Drawing.Point(80, 0);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(100, 24);
            this.btnFirst.TabIndex = 1;
            this.btnFirst.Text = "<< &First";
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnPrev.Location = new System.Drawing.Point(186, 0);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(100, 24);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.Text = "< &Previous";
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnNext.Location = new System.Drawing.Point(292, 0);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 24);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "&Next >";
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnLast
            // 
            this.btnLast.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLast.Location = new System.Drawing.Point(398, 0);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(100, 24);
            this.btnLast.TabIndex = 4;
            this.btnLast.Text = "&Last >>";
            this.btnLast.Click += new System.EventHandler(this.btnLast_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.txtNotes);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.picFish);
            this.panel1.Controls.Add(this.chMark);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtSpeciesName);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtCommonName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtCategory);
            this.panel1.Controls.Add(this.txtLength);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtSpeciesNo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(504, 244);
            this.panel1.TabIndex = 2;
            // 
            // txtNotes
            // 
            this.txtNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNotes.EditValue = "";
            this.txtNotes.Location = new System.Drawing.Point(104, 172);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(392, 68);
            this.txtNotes.TabIndex = 8;
            this.txtNotes.UseOptimizedRendering = true;
            // 
            // label8
            // 
            this.label8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label8.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label8.Location = new System.Drawing.Point(4, 172);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "Notes:";
            // 
            // picFish
            // 
            this.picFish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picFish.Location = new System.Drawing.Point(240, 4);
            this.picFish.Name = "picFish";
            this.picFish.Properties.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.picFish.Properties.Appearance.Options.UseBackColor = true;
            this.picFish.Properties.ShowMenu = false;
            this.picFish.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            this.picFish.Size = new System.Drawing.Size(256, 116);
            this.picFish.TabIndex = 3;
            // 
            // chMark
            // 
            this.chMark.Location = new System.Drawing.Point(104, 150);
            this.chMark.Name = "chMark";
            this.chMark.Properties.Caption = "Mark";
            this.chMark.Size = new System.Drawing.Size(60, 19);
            this.chMark.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label6.Location = new System.Drawing.Point(4, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Species Name:";
            // 
            // txtSpeciesName
            // 
            this.txtSpeciesName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSpeciesName.EditValue = "";
            this.txtSpeciesName.Location = new System.Drawing.Point(104, 124);
            this.txtSpeciesName.Name = "txtSpeciesName";
            this.txtSpeciesName.Size = new System.Drawing.Size(128, 20);
            this.txtSpeciesName.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label5.Location = new System.Drawing.Point(4, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Common Name:";
            // 
            // txtCommonName
            // 
            this.txtCommonName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommonName.EditValue = "";
            this.txtCommonName.Location = new System.Drawing.Point(104, 100);
            this.txtCommonName.Name = "txtCommonName";
            this.txtCommonName.Size = new System.Drawing.Size(128, 20);
            this.txtCommonName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label4.Location = new System.Drawing.Point(4, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Category:";
            // 
            // txtCategory
            // 
            this.txtCategory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCategory.EditValue = "";
            this.txtCategory.Location = new System.Drawing.Point(104, 76);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(128, 20);
            this.txtCategory.TabIndex = 3;
            // 
            // txtLength
            // 
            this.txtLength.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLength.EditValue = "";
            this.txtLength.Location = new System.Drawing.Point(104, 52);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(128, 20);
            this.txtLength.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label3.Location = new System.Drawing.Point(4, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Length(cm):";
            // 
            // txtSpeciesNo
            // 
            this.txtSpeciesNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSpeciesNo.EditValue = "";
            this.txtSpeciesNo.Location = new System.Drawing.Point(104, 28);
            this.txtSpeciesNo.Name = "txtSpeciesNo";
            this.txtSpeciesNo.Size = new System.Drawing.Size(128, 20);
            this.txtSpeciesNo.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label2.Location = new System.Drawing.Point(4, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Species No:";
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.EditValue = "";
            this.txtID.Enabled = false;
            this.txtID.Location = new System.Drawing.Point(104, 4);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(128, 20);
            this.txtID.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.label1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.label1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // PreviewControl
            // 
            this.Controls.Add(this.pnlNavigation);
            this.Name = "PreviewControl";
            this.Controls.SetChildIndex(this.pnlNavigation, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlNavigation)).EndInit();
            this.pnlNavigation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panel2)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chAllRecords.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFish.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chMark.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpeciesName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCommonName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCategory.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLength.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSpeciesNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtID.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        protected override void ActivateCore() {
            base.ActivateCore();

            if(BindingManager != null) {
                BindingManager.PositionChanged += new EventHandler(PositionChanged);
                PositionChanged(BindingManager, null);
            }
            if(printingSystem.Document.PageCount == 0)
                CreateDocument();
        }
        protected override void CreateDocumentCore() {
            int record = -1;
            if(BindingManager != null) {
                record = chAllRecords.Checked ? -1 : BindingManager.Position;
                BindingManager.EndCurrentEdit();
            }
            CreateBioLifePrintingLink(record);
        }
        protected abstract void CreateBioLifePrintingLink(int record);
        private void CreateDataView() {
            DataSet ds = new DataSet();
            ds.ReadXml(DBFileName);

            DataViewManager dvm = new DataViewManager(ds);
            dataView = dvm.CreateDataView(ds.Tables[0]);
        }

        private void PositionChanged(object sender, EventArgs e) {
            BindingManagerBase bmb = sender as BindingManagerBase;
            int pos = bmb.Position;
            btnFirst.Enabled = pos != 0;
            btnLast.Enabled = pos != bmb.Count - 1;
            btnPrev.Enabled = btnFirst.Enabled;
            btnNext.Enabled = btnLast.Enabled;
            PropertyDescriptor prop = BindingManager.GetItemProperties()["Picture"];
            if(prop != null)
                picFish.Image = BioLifePrintingLink.CreateFishImage(prop.GetValue(BindingManager.Current) as byte[]);
        }

        private void Binding() {
            txtID.DataBindings.Add("Text", dataView, "ID");
            txtSpeciesNo.DataBindings.Add("Text", dataView, "Species No");
            txtLength.DataBindings.Add("Text", dataView, "Length(cm)");
            txtCategory.DataBindings.Add("Text", dataView, "Category");
            txtCommonName.DataBindings.Add("Text", dataView, "Common Name");
            txtSpeciesName.DataBindings.Add("Text", dataView, "Species Name");
            txtNotes.DataBindings.Add("Text", dataView, "Notes");
            chMark.DataBindings.Add("Checked", dataView, "Mark");
        }

        private BindingManagerBase BindingManager {
            get { return txtID.DataBindings["Text"].BindingManagerBase; }
        }

        void chAllRecords_CheckedChanged(object sender, EventArgs e) {
            CreateDocument();
        }

        private void btnFirst_Click(object sender, System.EventArgs e) {
            BindingManager.Position = 0;
            if(!chAllRecords.Checked)
                CreateDocument();
        }

        private void btnPrev_Click(object sender, System.EventArgs e) {
            BindingManager.Position--;
            if(!chAllRecords.Checked)
                CreateDocument();
        }

        private void btnNext_Click(object sender, System.EventArgs e) {
            BindingManager.Position++;
            if(!chAllRecords.Checked)
                CreateDocument();
        }
        private void btnLast_Click(object sender, System.EventArgs e) {
            BindingManager.Position = BindingManager.Count;
            if(!chAllRecords.Checked)
                CreateDocument();
        }
    }

    public class PreviewControlTable : PreviewControl {
        public PreviewControlTable() {
            HideButtonOptions();
            MergeGroupActions();
        }
        protected override void CreateBioLifePrintingLink(int record) {
            if(!bioLifeLink.PrintGrid(dataView, record))
                MessageBox.Show("Page width is small...");
        }
    }
    public class PreviewControlGroups : PreviewControl {
        public PreviewControlGroups() {
            chAllRecords.Visible = false;
            HideButtonOptions();
            MergeGroupActions();
        }
        protected override void CreateBioLifePrintingLink(int record) {
            if(!bioLifeLink.PrintGroupingGrid(dataView))
                MessageBox.Show("Page size is small...");
        }
    }
    public class PreviewControlLabels : PreviewControl {
        public PreviewControlLabels() {
            HideButtonOptions();
            MergeGroupActions();
        }
        protected override void CreateBioLifePrintingLink(int record) {
            bioLifeLink.PrintLabels(dataView, record);
        }
    }
}
