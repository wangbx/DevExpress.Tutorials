namespace DevExpress.Tutorials
{
    using DevExpress.LookAndFeel;
    using DevExpress.Utils;
    using DevExpress.Utils.About;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class FrmMainBase : XtraForm, IMessageFilter
    {
        private IContainer components;
        public DefaultLookAndFeel defaultLookAndFeel;
        protected bool fHintVisible = true;
        public GroupControl gcContainer;
        public GroupControl gcDescription;
        public GroupControl gcNavigations;
        public PanelControl horzSplitter;
        protected PanelControl pcMain;
        public ApplicationCaption8_1 pnlCaption;
        public NotePanel8_1 pnlHint;
        private FormTutorialInfo tutorialInfo;

        public FrmMainBase()
        {
            UAlgo.Default.DoEventObject(1, 1, this);
            this.tutorialInfo = new FormTutorialInfo();
            this.InitializeComponent();
            Application.AddMessageFilter(this);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual void HideHint()
        {
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            this.gcNavigations = new GroupControl();
            this.defaultLookAndFeel = new DefaultLookAndFeel(this.components);
            this.pcMain = new PanelControl();
            this.gcContainer = new GroupControl();
            this.horzSplitter = new PanelControl();
            this.gcDescription = new GroupControl();
            this.pnlHint = new NotePanel8_1();
            this.pnlCaption = new ApplicationCaption8_1();
            this.gcNavigations.BeginInit();
            this.pcMain.BeginInit();
            this.pcMain.SuspendLayout();
            this.gcContainer.BeginInit();
            this.horzSplitter.BeginInit();
            this.gcDescription.BeginInit();
            this.gcDescription.SuspendLayout();
            base.SuspendLayout();
            this.gcNavigations.CaptionLocation = Locations.Left;
            this.gcNavigations.Dock = DockStyle.Left;
            this.gcNavigations.Location = new Point(0, 0);
            this.gcNavigations.Name = "gcNavigations";
            this.gcNavigations.Size = new Size(0xa6, 0x1d2);
            this.gcNavigations.TabIndex = 0;
            this.gcNavigations.Text = "Tutorial Names:";
            this.pcMain.BorderStyle = BorderStyles.NoBorder;
            this.pcMain.Controls.Add(this.gcContainer);
            this.pcMain.Controls.Add(this.horzSplitter);
            this.pcMain.Controls.Add(this.gcDescription);
            this.pcMain.Dock = DockStyle.Fill;
            this.pcMain.Location = new Point(0xa6, 0x33);
            this.pcMain.Name = "pcMain";
            this.pcMain.Padding = new Padding(8);
            this.pcMain.Size = new Size(0x222, 0x19f);
            this.pcMain.TabIndex = 1;
            this.gcContainer.Dock = DockStyle.Fill;
            this.gcContainer.Location = new Point(8, 8);
            this.gcContainer.Name = "gcContainer";
            this.gcContainer.ShowCaption = false;
            this.gcContainer.Size = new Size(530, 0x162);
            this.gcContainer.TabIndex = 1;
            this.gcContainer.Text = "Tutorial:";
            this.horzSplitter.BorderStyle = BorderStyles.NoBorder;
            this.horzSplitter.Dock = DockStyle.Bottom;
            this.horzSplitter.Location = new Point(8, 0x16a);
            this.horzSplitter.Name = "horzSplitter";
            this.horzSplitter.Size = new Size(530, 8);
            this.horzSplitter.TabIndex = 7;
            this.gcDescription.Controls.Add(this.pnlHint);
            this.gcDescription.Dock = DockStyle.Bottom;
            this.gcDescription.Location = new Point(8, 370);
            this.gcDescription.Name = "gcDescription";
            this.gcDescription.ShowCaption = false;
            this.gcDescription.Size = new Size(530, 0x25);
            this.gcDescription.TabIndex = 3;
            this.gcDescription.Text = "Description:";
            this.pnlHint.Dock = DockStyle.Fill;
            this.pnlHint.ForeColor = Color.Black;
            this.pnlHint.Location = new Point(2, 2);
            this.pnlHint.MaxRows = 5;
            this.pnlHint.Name = "pnlHint";
            this.pnlHint.ParentAutoHeight = true;
            this.pnlHint.Size = new Size(0x20e, 0x21);
            this.pnlHint.TabIndex = 0;
            this.pnlHint.TabStop = false;
            this.pnlCaption.Dock = DockStyle.Top;
            this.pnlCaption.Location = new Point(0xa6, 0);
            this.pnlCaption.Name = "pnlCaption";
            this.pnlCaption.Size = new Size(0x222, 0x33);
            this.pnlCaption.TabIndex = 4;
            this.pnlCaption.TabStop = false;
            this.pnlCaption.Text = "Tutorials";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x2c8, 0x1d2);
            base.Controls.Add(this.pcMain);
            base.Controls.Add(this.pnlCaption);
            base.Controls.Add(this.gcNavigations);
            base.Name = "FrmMainBase";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Tutorials ";
            base.Load += new EventHandler(this.OnLoad);
            this.gcNavigations.EndInit();
            this.pcMain.EndInit();
            this.pcMain.ResumeLayout(false);
            this.gcContainer.EndInit();
            this.horzSplitter.EndInit();
            this.gcDescription.EndInit();
            this.gcDescription.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected virtual void OnLoad(object sender, EventArgs e)
        {
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        public virtual bool PreFilterMessage(ref Message m)
        {
            if ((m.Msg == 0x201) && this.fHintVisible)
            {
                this.HideHint();
            }
            return false;
        }

        public bool HintVisible
        {
            get
            {
                return this.fHintVisible;
            }
            set
            {
                this.fHintVisible = value;
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FormTutorialInfo TutorialInfo
        {
            get
            {
                return this.tutorialInfo;
            }
        }
    }
}

