namespace DevExpress.Tutorials.Controls
{
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class frmAbout : XtraForm
    {
        private Container components;
        private PanelControl panelControl1;
        private PanelControl panelControl2;
        private RichTextBox richTextBox1;
        private SimpleButton simpleButton1;

        public frmAbout(string aboutFileName, string text, Icon icon)
        {
            this.InitializeComponent();
            if (text != null)
            {
                this.Text = text;
            }
            if (icon != null)
            {
                base.Icon = icon;
            }
            this.LoadAboutFile(aboutFileName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.richTextBox1 = new RichTextBox();
            this.panelControl1 = new PanelControl();
            this.panelControl2 = new PanelControl();
            this.simpleButton1 = new SimpleButton();
            this.panelControl1.BeginInit();
            this.panelControl1.SuspendLayout();
            this.panelControl2.BeginInit();
            this.panelControl2.SuspendLayout();
            base.SuspendLayout();
            this.richTextBox1.BorderStyle = BorderStyle.None;
            this.richTextBox1.Dock = DockStyle.Fill;
            this.richTextBox1.Location = new Point(2, 2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new Size(0x234, 0x16c);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.panelControl1.BorderStyle = BorderStyles.NoBorder;
            this.panelControl1.Controls.AddRange(new Control[] { this.simpleButton1 });
            this.panelControl1.Dock = DockStyle.Bottom;
            this.panelControl1.Location = new Point(0, 0x170);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new Size(0x238, 0x2e);
            this.panelControl1.TabIndex = 1;
            this.panelControl1.Text = "&Close";
            this.panelControl2.Controls.AddRange(new Control[] { this.richTextBox1 });
            this.panelControl2.Dock = DockStyle.Fill;
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new Size(0x238, 0x170);
            this.panelControl2.TabIndex = 2;
            this.panelControl2.Text = "panelControl2";
            this.simpleButton1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
            this.simpleButton1.DialogResult = DialogResult.Cancel;
            this.simpleButton1.Location = new Point(0x1c8, 8);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x68, 30);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "&Close";
            base.AcceptButton = this.simpleButton1;
            this.AutoScaleBaseSize = new Size(5, 14);
            base.CancelButton = this.simpleButton1;
            base.ClientSize = new Size(0x238, 0x19e);
            base.Controls.AddRange(new Control[] { this.panelControl2, this.panelControl1 });
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmAbout";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "About";
            this.panelControl1.EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl2.EndInit();
            this.panelControl2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void LoadAboutFile(string fileName)
        {
            try
            {
                this.richTextBox1.LoadFile(fileName);
            }
            catch
            {
            }
        }
    }
}

