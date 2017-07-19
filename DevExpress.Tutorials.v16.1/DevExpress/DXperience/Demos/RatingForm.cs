namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraEditors;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class RatingForm : XtraForm
    {
        private SimpleButton btnSend;
        private Container components;
        private Form form;
        private Label label1;
        private Label lbExcellent;
        private Label lbOpinion;
        private Label lbPoor;
        private MemoEdit meOpinion;
        private const string notSeen = "You have'nt seen any demo to have your opinion";
        private Panel pnlOpinion;
        private TrackBarControl trbOpinion;

        public RatingForm(Form form)
        {
            this.form = form;
            this.InitializeComponent();
            base.Icon = form.Icon;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                string str = (("Rate = " + this.trbOpinion.Value.ToString()) + "\r\n\r\nDescription:\r\n" + this.meOpinion.Text).Replace("%", "$prc$").Replace("$prc$", "%25").Replace("\r\n", "%0D%0A").Replace("&", "%26").Replace(" ", "%20");
                Process.Start("mailto:" + Email + "?subject=" + this.EmailSubj + "&body=" + str);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                base.Close();
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

        private void InitializeComponent()
        {
            this.label1 = new Label();
            this.lbPoor = new Label();
            this.lbExcellent = new Label();
            this.lbOpinion = new Label();
            this.pnlOpinion = new Panel();
            this.trbOpinion = new TrackBarControl();
            this.meOpinion = new MemoEdit();
            this.btnSend = new SimpleButton();
            this.pnlOpinion.SuspendLayout();
            this.meOpinion.Properties.BeginInit();
            base.SuspendLayout();
            this.label1.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 1);
            this.label1.Location = new Point(0x20, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x10c, 0x13);
            this.label1.TabIndex = 0;
            this.label1.Text = "How would you rate the quality of this demo?";
            this.lbPoor.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 1);
            this.lbPoor.Location = new Point(9, 60);
            this.lbPoor.Name = "lbPoor";
            this.lbPoor.Size = new Size(0x1f, 0x1b);
            this.lbPoor.TabIndex = 2;
            this.lbPoor.Text = "Poor";
            this.lbExcellent.Font = new Font("Tahoma", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 1);
            this.lbExcellent.Location = new Point(0x10b, 60);
            this.lbExcellent.Name = "lbExcellent";
            this.lbExcellent.Size = new Size(50, 0x1b);
            this.lbExcellent.TabIndex = 3;
            this.lbExcellent.Text = "Excellent";
            this.lbOpinion.Font = new Font("Tahoma", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 1);
            this.lbOpinion.Location = new Point(12, 0x73);
            this.lbOpinion.Name = "lbOpinion";
            this.lbOpinion.Size = new Size(300, 0x12);
            this.lbOpinion.TabIndex = 4;
            this.lbOpinion.Text = "Tell us your opinion about the demos:";
            this.pnlOpinion.Controls.AddRange(new Control[] { this.trbOpinion });
            this.pnlOpinion.Location = new Point(40, 0x2e);
            this.pnlOpinion.Name = "pnlOpinion";
            this.pnlOpinion.Size = new Size(0xe0, 0x33);
            this.pnlOpinion.TabIndex = 8;
            this.trbOpinion.Dock = DockStyle.Fill;
            this.trbOpinion.Name = "trbOpinion";
            this.trbOpinion.Size = new Size(0xe0, 0x33);
            this.trbOpinion.TabIndex = 0;
            this.trbOpinion.Value = 5;
            this.meOpinion.EditValue = "";
            this.meOpinion.Location = new Point(12, 0x86);
            this.meOpinion.Name = "meOpinion";
            this.meOpinion.Size = new Size(300, 130);
            this.meOpinion.TabIndex = 9;
            this.btnSend.Location = new Point(0xec, 0x10c);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new Size(0x4b, 0x1c);
            this.btnSend.TabIndex = 10;
            this.btnSend.Text = "Email...";
            this.btnSend.Click += new EventHandler(this.btnSend_Click);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.ClientSize = new Size(0x142, 0x12f);
            base.Controls.AddRange(new Control[] { this.btnSend, this.meOpinion, this.pnlOpinion, this.lbOpinion, this.lbExcellent, this.lbPoor, this.label1 });
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "RatingForm";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Demo rating";
            base.Load += new EventHandler(this.RatingForm_Load);
            this.pnlOpinion.ResumeLayout(false);
            this.meOpinion.Properties.EndInit();
            base.ResumeLayout(false);
        }

        private void RatingForm_Load(object sender, EventArgs e)
        {
            bool flag = false;
            if (this.meOpinion.Text == "You have'nt seen any demo to have your opinion")
            {
                this.meOpinion.Text = "";
            }
            int count = ModulesInfo.Count;
            for (int i = 0; i < count; i++)
            {
                DevExpress.DXperience.Demos.ModuleInfo item = ModulesInfo.GetItem(i);
                if ((item != null) && item.WasShown)
                {
                    flag = true;
                    if (this.meOpinion.Text.IndexOf(item.Name) == -1)
                    {
                        this.meOpinion.Text = this.meOpinion.Text + item.Name + " : \r\n";
                    }
                }
            }
            if (!flag)
            {
                this.meOpinion.Text = "You have'nt seen any demo to have your opinion";
            }
            this.meOpinion.Enabled = flag;
            this.btnSend.Enabled = flag;
            this.trbOpinion.Enabled = flag;
            this.meOpinion.DeselectAll();
        }

        private static string Email
        {
            get
            {
                return "DemetriusB@devexpress.com";
            }
        }

        private string EmailSubj
        {
            get
            {
                return (Assembly.GetEntryAssembly().GetName().Name + " - user rating");
            }
        }
    }
}

