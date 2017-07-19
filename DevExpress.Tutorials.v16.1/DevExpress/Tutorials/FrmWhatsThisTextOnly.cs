namespace DevExpress.Tutorials
{
    using DevExpress.XtraEditors;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class FrmWhatsThisTextOnly : FrmWhatsThisBase
    {
        private SimpleButton btnClose;
        private Label lblDescription;
        private Label lblDescriptionHeader;
        private Panel panel1;
        private Panel pnlButtonContainer;
        private Panel pnlDescContainer;
        private Panel pnlLeft;
        private Panel pnlRight;
        private Panel pnlSeparator1;

        public FrmWhatsThisTextOnly()
        {
            this.InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlButtonContainer = new Panel();
            this.btnClose = new SimpleButton();
            this.pnlDescContainer = new Panel();
            this.lblDescription = new Label();
            this.panel1 = new Panel();
            this.lblDescriptionHeader = new Label();
            this.pnlSeparator1 = new Panel();
            this.pnlLeft = new Panel();
            this.pnlRight = new Panel();
            this.pnlButtonContainer.SuspendLayout();
            this.pnlDescContainer.SuspendLayout();
            base.SuspendLayout();
            this.pnlButtonContainer.BackColor = SystemColors.Info;
            this.pnlButtonContainer.Controls.Add(this.btnClose);
            this.pnlButtonContainer.Dock = DockStyle.Bottom;
            this.pnlButtonContainer.Location = new Point(0, 0x56);
            this.pnlButtonContainer.Name = "pnlButtonContainer";
            this.pnlButtonContainer.Size = new Size(0x176, 40);
            this.pnlButtonContainer.TabIndex = 10;
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(0x11c, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(80, 0x18);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.pnlDescContainer.BackColor = SystemColors.Info;
            this.pnlDescContainer.Controls.Add(this.lblDescription);
            this.pnlDescContainer.Controls.Add(this.panel1);
            this.pnlDescContainer.Controls.Add(this.lblDescriptionHeader);
            this.pnlDescContainer.Controls.Add(this.pnlSeparator1);
            this.pnlDescContainer.Dock = DockStyle.Fill;
            this.pnlDescContainer.Location = new Point(8, 0);
            this.pnlDescContainer.Name = "pnlDescContainer";
            this.pnlDescContainer.Size = new Size(0x166, 0x56);
            this.pnlDescContainer.TabIndex = 11;
            this.lblDescription.BackColor = SystemColors.Info;
            this.lblDescription.Dock = DockStyle.Top;
            this.lblDescription.Font = new Font("Tahoma", 8.25f);
            this.lblDescription.ForeColor = Color.FromArgb(0, 0, 1);
            this.lblDescription.ImageAlign = ContentAlignment.MiddleLeft;
            this.lblDescription.Location = new Point(0, 0x20);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(0x166, 40);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description content";
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0x18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x166, 8);
            this.panel1.TabIndex = 13;
            this.lblDescriptionHeader.BackColor = SystemColors.Info;
            this.lblDescriptionHeader.Dock = DockStyle.Top;
            this.lblDescriptionHeader.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            this.lblDescriptionHeader.ForeColor = Color.FromArgb(0, 0, 1);
            this.lblDescriptionHeader.Location = new Point(0, 8);
            this.lblDescriptionHeader.Name = "lblDescriptionHeader";
            this.lblDescriptionHeader.Size = new Size(0x166, 0x10);
            this.lblDescriptionHeader.TabIndex = 11;
            this.lblDescriptionHeader.Text = "Description:";
            this.pnlSeparator1.Dock = DockStyle.Top;
            this.pnlSeparator1.Location = new Point(0, 0);
            this.pnlSeparator1.Name = "pnlSeparator1";
            this.pnlSeparator1.Size = new Size(0x166, 8);
            this.pnlSeparator1.TabIndex = 12;
            this.pnlLeft.BackColor = SystemColors.Info;
            this.pnlLeft.Dock = DockStyle.Left;
            this.pnlLeft.Location = new Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new Size(8, 0x56);
            this.pnlLeft.TabIndex = 12;
            this.pnlRight.BackColor = SystemColors.Info;
            this.pnlRight.Dock = DockStyle.Right;
            this.pnlRight.Location = new Point(0x16e, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new Size(8, 0x56);
            this.pnlRight.TabIndex = 13;
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.ClientSize = new Size(0x176, 0x7e);
            base.Controls.Add(this.pnlDescContainer);
            base.Controls.Add(this.pnlLeft);
            base.Controls.Add(this.pnlRight);
            base.Controls.Add(this.pnlButtonContainer);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new Size(300, 150);
            base.Name = "FrmWhatsThisTextOnly";
            base.ShowInTaskbar = false;
            base.TopMost = true;
            this.pnlButtonContainer.ResumeLayout(false);
            this.pnlDescContainer.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        protected override void UpdateControls(WhatsThisParams whatsThisParams)
        {
            base.UpdateControls(whatsThisParams);
            this.lblDescription.Text = whatsThisParams.Description;
        }

        protected override void UpdateDescriptionPanel()
        {
            ControlUtils.UpdateLabelHeight(this.lblDescription);
            int num = 0;
            foreach (Control control in this.pnlDescContainer.Controls)
            {
                num += control.Height;
            }
            base.ClientSize = new Size(base.ClientSize.Width, num + this.pnlButtonContainer.Height);
        }
    }
}

