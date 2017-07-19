namespace DevExpress.Tutorials
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FrmMainTreeViewReg : FrmMain
    {
        private IContainer components;
        public DevExpress.Tutorials.TreeViewAdapter fTreeViewAdapter;
        protected ImageList imageListTreeView;
        protected TreeView tvNavigation;

        public FrmMainTreeViewReg()
        {
            this.InitializeComponent();
        }

        public void AddModuleInfo(int id, int parentId, string nodeText, object nodeImage, System.Type moduleType)
        {
            this.AddModuleInfo(id, parentId, nodeText, nodeImage, moduleType, string.Empty, string.Empty, string.Empty);
        }

        public void AddModuleInfo(int id, int parentId, string nodeText, object nodeImage, System.Type moduleType, string description, string whatsThisXMLFile, string whatsThisCodeFile)
        {
            this.TreeViewAdapter.AddNode(id, parentId, nodeText, nodeImage);
            if (moduleType != null)
            {
                ModuleInfoCollection.Add(id, moduleType, description, whatsThisXMLFile, whatsThisCodeFile);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FrmMainTreeViewReg_Load(object sender, EventArgs e)
        {
            this.tvNavigation.ExpandAll();
            if ((this.tvNavigation.SelectedNode == null) && (this.tvNavigation.Nodes.Count > 0))
            {
                this.tvNavigation.SelectedNode = this.tvNavigation.Nodes[0];
            }
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmMainTreeViewReg));
            this.tvNavigation = new TreeView();
            this.imageListTreeView = new ImageList(this.components);
            base.barManager1.BeginInit();
            base.pnlCaption.SuspendLayout();
            base.gcNavigations.BeginInit();
            base.gcNavigations.SuspendLayout();
            base.gcContainer.BeginInit();
            base.gcDescription.BeginInit();
            base.gcDescription.SuspendLayout();
            base.pcMain.BeginInit();
            base.pcMain.SuspendLayout();
            base.SuspendLayout();
            base.btnWhatsThis.Location = new Point(0x24c, 8);
            base.btnWhatsThis.Size = new Size(0x60, 0x22);
            base.btnAbout.Location = new Point(0x11c, 9);
            base.pnlCaption.Location = new Point(0xa6, 0x18);
            base.gcNavigations.Controls.Add(this.tvNavigation);
            base.gcNavigations.Location = new Point(0, 0x18);
            base.gcNavigations.Size = new Size(0xa6, 0x1ba);
            base.gcContainer.Location = new Point(8, 8);
            base.gcContainer.Size = new Size(510, 330);
            base.horzSplitter.Location = new Point(8, 0x152);
            base.gcDescription.Appearance.BackColor = Color.Transparent;
            base.gcDescription.Appearance.Options.UseBackColor = true;
            base.gcDescription.Location = new Point(8, 0x15a);
            base.pcMain.Location = new Point(0xa6, 0x4b);
            base.pcMain.Padding = new Padding(8);
            base.pcMain.Size = new Size(0x20e, 0x187);
            this.tvNavigation.BorderStyle = BorderStyle.None;
            this.tvNavigation.Dock = DockStyle.Fill;
            this.tvNavigation.Font = new Font("Tahoma", 8.25f);
            this.tvNavigation.ImageIndex = 0;
            this.tvNavigation.ImageList = this.imageListTreeView;
            this.tvNavigation.Location = new Point(0x13, 2);
            this.tvNavigation.Name = "tvNavigation";
            this.tvNavigation.SelectedImageIndex = 0;
            this.tvNavigation.ShowPlusMinus = false;
            this.tvNavigation.ShowRootLines = false;
            this.tvNavigation.Size = new Size(0x91, 0x1b6);
            this.tvNavigation.TabIndex = 0;
            this.tvNavigation.BeforeCollapse += new TreeViewCancelEventHandler(this.tvNavigation_BeforeCollapse);
            this.tvNavigation.AfterSelect += new TreeViewEventHandler(this.tvNavigation_AfterSelect);
            this.imageListTreeView.ColorDepth = ColorDepth.Depth24Bit;
            this.imageListTreeView.ImageSize = new Size(0x10, 0x10);
            this.imageListTreeView.TransparentColor = Color.Magenta;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2b4, 0x1d2);
            base.Name = "FrmMainTreeViewReg";
            base.TutorialInfo.AboutFile = null;
            base.TutorialInfo.ImagePatternFill = null;
            base.TutorialInfo.ImageWhatsThisButton = (Image) manager.GetObject("FrmMainTreeViewReg.TutorialInfo.ImageWhatsThisButton");
            base.TutorialInfo.ImageWhatsThisButtonStop = (Image) manager.GetObject("FrmMainTreeViewReg.TutorialInfo.ImageWhatsThisButtonStop");
            base.TutorialInfo.SourceFileComment = null;
            base.TutorialInfo.SourceFileType = SourceFileType.CS;
            base.Load += new EventHandler(this.FrmMainTreeViewReg_Load);
            base.barManager1.EndInit();
            base.pnlCaption.ResumeLayout(false);
            base.gcNavigations.EndInit();
            base.gcNavigations.ResumeLayout(false);
            base.gcContainer.EndInit();
            base.gcDescription.EndInit();
            base.gcDescription.ResumeLayout(false);
            base.pcMain.EndInit();
            base.pcMain.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void tvNavigation_AfterSelect(object sender, TreeViewEventArgs e)
        {
            Cursor current = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            base.SuspendLayout();
            try
            {
                ModuleInfo info = ModuleInfoCollection.ModuleInfoById(this.fTreeViewAdapter.GetSelectedModuleId());
                if ((info != null) && ((base.CurrentModule == null) || (base.CurrentModule.Name != info.Module.Name)))
                {
                    base.SelectModule(info.Module, true);
                }
            }
            finally
            {
                base.ResumeLayout();
                Cursor.Current = current;
            }
        }

        private void tvNavigation_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = true;
        }

        public DevExpress.Tutorials.TreeViewAdapter TreeViewAdapter
        {
            get
            {
                if (this.fTreeViewAdapter == null)
                {
                    this.fTreeViewAdapter = new DevExpress.Tutorials.TreeViewAdapter(this.tvNavigation);
                }
                return this.fTreeViewAdapter;
            }
        }
    }
}

