namespace DevExpress.Tutorials
{
    using DevExpress.Utils;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraEditors;
    using DevExpress.XtraTab;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;

    public class FrmWhatsThis : FrmWhatsThisBase
    {
        private SimpleButton btnClose;
        private SimpleButton btnCopyClipboard;
        private ColoredTextControl coloredTextControl;
        private IContainer components;
        private ImageList imageListTabs;
        private bool isExistMembers;
        private Label lblDescription;
        private Label lblDescriptionHeader;
        private Label lblMemberList;
        private Label lblMemberListHeader;
        private LabelInfo memberInfo = new LabelInfo();
        private PictureEdit pictureDTScreenshot;
        private Panel pnlButtonContainer;
        private Panel pnlDescContainer;
        private Panel pnlDescriptionSeparator1;
        private Panel pnlDescriptionSeparator2;
        private Panel pnlDescriptionSeparator3;
        private Panel pnlDescriptionSeparator4;
        private Panel pnlDescriptionSeparator5;
        private Panel pnlMemoLeft;
        private Panel pnlMemoRight;
        private Panel pnlTextContainer;
        private XtraTabControl tabctrlInfo;
        private XtraTabPage tabpgColoredCode;
        private XtraTabPage tabpgImage;

        public FrmWhatsThis()
        {
            this.InitializeComponent();
        }

        private bool BestFitForm()
        {
            bool flag = false;
            Screen screen = Screen.FromControl(this);
            int height = ScaleUtils.GetScaleSize(new Size(0, 0x23)).Height;
            base.ClientSize = new Size(this.coloredTextControl.ViewInfo.Populator.TotalWidth + ((height * 4) / 3), ((this.pnlDescContainer.Height + this.pnlButtonContainer.Height) + Math.Max(this.coloredTextControl.ViewInfo.Populator.TotalHeight, height * 2)) + height);
            if (base.Width > ((screen.Bounds.Width / 3) * 2))
            {
                base.Width = (screen.Bounds.Width / 3) * 2;
                flag = true;
            }
            if (base.Height > ((screen.Bounds.Height / 3) * 2))
            {
                base.Height = (screen.Bounds.Height / 3) * 2;
                base.Width += height;
                flag = true;
            }
            DevExpress.Tutorials.ControlUtils.UpdateFrmToFitScreen(this);
            return flag;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCopyClipboard_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(this.coloredTextControl.Text);
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
            this.components = new Container();
            ComponentResourceManager manager = new ComponentResourceManager(typeof(FrmWhatsThis));
            this.btnCopyClipboard = new SimpleButton();
            this.lblDescription = new Label();
            this.pnlButtonContainer = new Panel();
            this.btnClose = new SimpleButton();
            this.pnlDescContainer = new Panel();
            this.pnlTextContainer = new Panel();
            this.pnlDescriptionSeparator4 = new Panel();
            this.lblMemberList = new Label();
            this.pnlDescriptionSeparator3 = new Panel();
            this.lblMemberListHeader = new Label();
            this.pnlDescriptionSeparator2 = new Panel();
            this.pnlDescriptionSeparator1 = new Panel();
            this.lblDescriptionHeader = new Label();
            this.pnlDescriptionSeparator5 = new Panel();
            this.pnlMemoLeft = new Panel();
            this.pnlMemoRight = new Panel();
            this.pictureDTScreenshot = new PictureEdit();
            this.imageListTabs = new ImageList(this.components);
            this.tabctrlInfo = new XtraTabControl();
            this.tabpgColoredCode = new XtraTabPage();
            this.coloredTextControl = new ColoredTextControl();
            this.tabpgImage = new XtraTabPage();
            this.pnlButtonContainer.SuspendLayout();
            this.pnlDescContainer.SuspendLayout();
            this.pnlTextContainer.SuspendLayout();
            this.pictureDTScreenshot.Properties.BeginInit();
            this.tabctrlInfo.BeginInit();
            this.tabctrlInfo.SuspendLayout();
            this.tabpgColoredCode.SuspendLayout();
            this.tabpgImage.SuspendLayout();
            base.SuspendLayout();
            this.btnCopyClipboard.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnCopyClipboard.Location = new Point(0x1d9, 8);
            this.btnCopyClipboard.Name = "btnCopyClipboard";
            this.btnCopyClipboard.Size = new Size(80, 0x18);
            this.btnCopyClipboard.TabIndex = 1;
            this.btnCopyClipboard.Text = "Copy";
            this.btnCopyClipboard.Click += new EventHandler(this.btnCopyClipboard_Click);
            this.lblDescription.BackColor = SystemColors.Info;
            this.lblDescription.Dock = DockStyle.Top;
            this.lblDescription.Font = new Font("Tahoma", 8.25f);
            this.lblDescription.ForeColor = Color.FromArgb(1, 1, 1);
            this.lblDescription.ImageAlign = ContentAlignment.MiddleLeft;
            this.lblDescription.Location = new Point(0, 0x16);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new Size(0x27b, 0x20);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Description content";
            this.pnlButtonContainer.BackColor = SystemColors.Info;
            this.pnlButtonContainer.Controls.Add(this.btnClose);
            this.pnlButtonContainer.Controls.Add(this.btnCopyClipboard);
            this.pnlButtonContainer.Dock = DockStyle.Bottom;
            this.pnlButtonContainer.Location = new Point(0, 0xb0);
            this.pnlButtonContainer.Name = "pnlButtonContainer";
            this.pnlButtonContainer.Size = new Size(0x28b, 40);
            this.pnlButtonContainer.TabIndex = 4;
            this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnClose.DialogResult = DialogResult.Cancel;
            this.btnClose.Location = new Point(0x231, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(80, 0x18);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.pnlDescContainer.BackColor = SystemColors.Info;
            this.pnlDescContainer.Controls.Add(this.pnlTextContainer);
            this.pnlDescContainer.Dock = DockStyle.Top;
            this.pnlDescContainer.Location = new Point(8, 0);
            this.pnlDescContainer.Name = "pnlDescContainer";
            this.pnlDescContainer.Size = new Size(0x27b, 110);
            this.pnlDescContainer.TabIndex = 5;
            this.pnlTextContainer.Controls.Add(this.pnlDescriptionSeparator4);
            this.pnlTextContainer.Controls.Add(this.lblMemberList);
            this.pnlTextContainer.Controls.Add(this.pnlDescriptionSeparator3);
            this.pnlTextContainer.Controls.Add(this.lblMemberListHeader);
            this.pnlTextContainer.Controls.Add(this.pnlDescriptionSeparator2);
            this.pnlTextContainer.Controls.Add(this.lblDescription);
            this.pnlTextContainer.Controls.Add(this.pnlDescriptionSeparator1);
            this.pnlTextContainer.Controls.Add(this.lblDescriptionHeader);
            this.pnlTextContainer.Controls.Add(this.pnlDescriptionSeparator5);
            this.pnlTextContainer.Dock = DockStyle.Fill;
            this.pnlTextContainer.Location = new Point(0, 0);
            this.pnlTextContainer.Name = "pnlTextContainer";
            this.pnlTextContainer.Size = new Size(0x27b, 110);
            this.pnlTextContainer.TabIndex = 10;
            this.pnlDescriptionSeparator4.Dock = DockStyle.Top;
            this.pnlDescriptionSeparator4.Location = new Point(0, 0x60);
            this.pnlDescriptionSeparator4.Name = "pnlDescriptionSeparator4";
            this.pnlDescriptionSeparator4.Size = new Size(0x27b, 8);
            this.pnlDescriptionSeparator4.TabIndex = 10;
            this.lblMemberList.Dock = DockStyle.Top;
            this.lblMemberList.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lblMemberList.ForeColor = Color.FromArgb(1, 1, 1);
            this.lblMemberList.Location = new Point(0, 80);
            this.lblMemberList.Name = "lblMemberList";
            this.lblMemberList.Size = new Size(0x27b, 0x10);
            this.lblMemberList.TabIndex = 6;
            this.lblMemberList.Text = "Member List";
            this.pnlDescriptionSeparator3.Dock = DockStyle.Top;
            this.pnlDescriptionSeparator3.Location = new Point(0, 0x4e);
            this.pnlDescriptionSeparator3.Name = "pnlDescriptionSeparator3";
            this.pnlDescriptionSeparator3.Size = new Size(0x27b, 2);
            this.pnlDescriptionSeparator3.TabIndex = 8;
            this.lblMemberListHeader.Dock = DockStyle.Top;
            this.lblMemberListHeader.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            this.lblMemberListHeader.ForeColor = Color.FromArgb(1, 1, 1);
            this.lblMemberListHeader.Location = new Point(0, 0x3e);
            this.lblMemberListHeader.Name = "lblMemberListHeader";
            this.lblMemberListHeader.Size = new Size(0x27b, 0x10);
            this.lblMemberListHeader.TabIndex = 5;
            this.lblMemberListHeader.Text = "Related API:";
            this.pnlDescriptionSeparator2.Dock = DockStyle.Top;
            this.pnlDescriptionSeparator2.Location = new Point(0, 0x36);
            this.pnlDescriptionSeparator2.Name = "pnlDescriptionSeparator2";
            this.pnlDescriptionSeparator2.Size = new Size(0x27b, 8);
            this.pnlDescriptionSeparator2.TabIndex = 7;
            this.pnlDescriptionSeparator1.Dock = DockStyle.Top;
            this.pnlDescriptionSeparator1.Location = new Point(0, 20);
            this.pnlDescriptionSeparator1.Name = "pnlDescriptionSeparator1";
            this.pnlDescriptionSeparator1.Size = new Size(0x27b, 2);
            this.pnlDescriptionSeparator1.TabIndex = 9;
            this.lblDescriptionHeader.Dock = DockStyle.Top;
            this.lblDescriptionHeader.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            this.lblDescriptionHeader.ForeColor = Color.FromArgb(1, 1, 1);
            this.lblDescriptionHeader.Location = new Point(0, 4);
            this.lblDescriptionHeader.Name = "lblDescriptionHeader";
            this.lblDescriptionHeader.Size = new Size(0x27b, 0x10);
            this.lblDescriptionHeader.TabIndex = 11;
            this.lblDescriptionHeader.Text = "Description:";
            this.pnlDescriptionSeparator5.Dock = DockStyle.Top;
            this.pnlDescriptionSeparator5.Location = new Point(0, 0);
            this.pnlDescriptionSeparator5.Name = "pnlDescriptionSeparator5";
            this.pnlDescriptionSeparator5.Size = new Size(0x27b, 4);
            this.pnlDescriptionSeparator5.TabIndex = 12;
            this.pnlMemoLeft.BackColor = SystemColors.Info;
            this.pnlMemoLeft.Dock = DockStyle.Left;
            this.pnlMemoLeft.Location = new Point(0, 0);
            this.pnlMemoLeft.Name = "pnlMemoLeft";
            this.pnlMemoLeft.Size = new Size(8, 0xb0);
            this.pnlMemoLeft.TabIndex = 6;
            this.pnlMemoRight.BackColor = SystemColors.Info;
            this.pnlMemoRight.Dock = DockStyle.Right;
            this.pnlMemoRight.Location = new Point(0x283, 0);
            this.pnlMemoRight.Name = "pnlMemoRight";
            this.pnlMemoRight.Size = new Size(8, 0xb0);
            this.pnlMemoRight.TabIndex = 7;
            this.pictureDTScreenshot.Dock = DockStyle.Fill;
            this.pictureDTScreenshot.Location = new Point(0, 0);
            this.pictureDTScreenshot.Name = "pictureDTScreenshot";
            this.pictureDTScreenshot.Size = new Size(0x275, 0xa4);
            this.pictureDTScreenshot.TabIndex = 0;
            this.imageListTabs.ImageStream = (ImageListStreamer) manager.GetObject("imageListTabs.ImageStream");
            this.imageListTabs.TransparentColor = Color.Magenta;
            this.imageListTabs.Images.SetKeyName(0, "");
            this.imageListTabs.Images.SetKeyName(1, "");
            this.tabctrlInfo.Appearance.BackColor = SystemColors.Info;
            this.tabctrlInfo.Appearance.Options.UseBackColor = true;
            this.tabctrlInfo.Dock = DockStyle.Fill;
            this.tabctrlInfo.Images = this.imageListTabs;
            this.tabctrlInfo.Location = new Point(8, 110);
            this.tabctrlInfo.Name = "tabctrlInfo";
            this.tabctrlInfo.SelectedTabPage = this.tabpgColoredCode;
            this.tabctrlInfo.Size = new Size(0x27b, 0x42);
            this.tabctrlInfo.TabIndex = 9;
            this.tabctrlInfo.TabPages.AddRange(new XtraTabPage[] { this.tabpgColoredCode, this.tabpgImage });
            this.tabpgColoredCode.Controls.Add(this.coloredTextControl);
            this.tabpgColoredCode.Name = "tabpgColoredCode";
            this.tabpgColoredCode.Size = new Size(0x275, 0x23);
            this.tabpgColoredCode.Text = "Code";
            this.coloredTextControl.BackColor = SystemColors.Window;
            this.coloredTextControl.Dock = DockStyle.Fill;
            this.coloredTextControl.ForeColor = Color.Black;
            this.coloredTextControl.HintBorderVisible = false;
            this.coloredTextControl.LexemProcessorKind = "CSharp";
            this.coloredTextControl.LexerKind = "CSharp";
            this.coloredTextControl.Location = new Point(0, 0);
            this.coloredTextControl.Name = "coloredTextControl";
            this.coloredTextControl.Size = new Size(0x275, 0x23);
            this.coloredTextControl.TabIndex = 0;
            this.coloredTextControl.TextPadding = 8;
            this.tabpgImage.Controls.Add(this.pictureDTScreenshot);
            this.tabpgImage.ImageIndex = 1;
            this.tabpgImage.Name = "tabpgImage";
            this.tabpgImage.Size = new Size(0x275, 0xa4);
            this.tabpgImage.Text = "Screenshot";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnClose;
            base.ClientSize = new Size(0x28b, 0xd8);
            base.Controls.Add(this.tabctrlInfo);
            base.Controls.Add(this.pnlDescContainer);
            base.Controls.Add(this.pnlMemoRight);
            base.Controls.Add(this.pnlMemoLeft);
            base.Controls.Add(this.pnlButtonContainer);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new Size(400, 250);
            base.Name = "FrmWhatsThis";
            base.ShowInTaskbar = false;
            base.TopMost = true;
            this.pnlButtonContainer.ResumeLayout(false);
            this.pnlDescContainer.ResumeLayout(false);
            this.pnlTextContainer.ResumeLayout(false);
            this.pictureDTScreenshot.Properties.EndInit();
            this.tabctrlInfo.EndInit();
            this.tabctrlInfo.ResumeLayout(false);
            this.tabpgColoredCode.ResumeLayout(false);
            this.tabpgImage.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void OnLabelClick(object sender, LabelInfoItemClickEventArgs e)
        {
            string text = e.InfoText.Text;
            try
            {
                new Process { StartInfo = { 
                    FileName = string.Format("https://search.devexpress.com/?q={0}&f=10&m=Documentation", text),
                    Verb = "Open",
                    WindowStyle = ProcessWindowStyle.Normal
                } }.Start();
            }
            catch
            {
            }
        }

        private void SetMemberList(string list)
        {
            this.memberInfo.Parent = this.lblMemberList.Parent;
            this.memberInfo.Size = new Size(0, string.IsNullOrEmpty(list) ? 0 : 0x17);
            this.memberInfo.Dock = DockStyle.Top;
            this.memberInfo.BringToFront();
            this.pnlDescriptionSeparator4.BringToFront();
            this.memberInfo.Font = this.lblMemberList.Font;
            string[] strArray = list.Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                this.memberInfo.Texts.Add(strArray[i].Trim(), Color.Blue, true);
                if (i != (strArray.Length - 1))
                {
                    this.memberInfo.Texts.Add(", ");
                }
            }
            this.memberInfo.AutoHeight = true;
            this.memberInfo.ItemClick += new LabelInfoItemClickEvent(this.OnLabelClick);
        }

        public override void Show(string controlName, WhatsThisParams whatsThisParams, SourceFileType sourceFileType)
        {
            this.lblMemberList.Visible = false;
            this.isExistMembers = !string.IsNullOrEmpty(whatsThisParams.MemberList);
            base.SuspendLayout();
            this.UpdateCodeControl(sourceFileType);
            base.Show(controlName, whatsThisParams, sourceFileType);
            this.UpdateMemberList();
            this.btnClose.Focus();
            base.BeginInvoke(new MethodInvoker(this.UpdateMemberList));
            bool flag = this.BestFitForm();
            base.Width--;
            base.ResumeLayout();
            base.Width++;
            if (!flag && this.coloredTextControl.ViewInfo.ScrollInfo.ScrollBarVisible)
            {
                this.BestFitForm();
            }
        }

        private void UpdateCodeControl(SourceFileType type)
        {
            switch (type)
            {
                case SourceFileType.CS:
                    this.coloredTextControl.LexerKind = "CSharp";
                    this.coloredTextControl.LexemProcessorKind = "CSharp";
                    return;

                case SourceFileType.VB:
                    this.coloredTextControl.LexerKind = "VB";
                    this.coloredTextControl.LexemProcessorKind = "VB";
                    return;
            }
        }

        protected override void UpdateControls(WhatsThisParams whatsThisParams)
        {
            base.UpdateControls(whatsThisParams);
            this.coloredTextControl.Text = whatsThisParams.Code;
            this.lblDescription.Text = whatsThisParams.Description;
            this.SetMemberList(whatsThisParams.MemberList);
            if (whatsThisParams.DTImage == string.Empty)
            {
                this.tabpgImage.PageVisible = false;
            }
            else
            {
                this.tabpgImage.PageVisible = true;
                this.pictureDTScreenshot.Image = Image.FromFile(FilePathUtils.FindFilePath(whatsThisParams.DTImage, true));
            }
        }

        protected override void UpdateDescriptionPanel()
        {
            DevExpress.Tutorials.ControlUtils.UpdateLabelHeight(this.lblDescription);
            DevExpress.Tutorials.ControlUtils.UpdateLabelHeight(this.lblMemberList);
            int num = 0;
            foreach (Control control in this.pnlTextContainer.Controls)
            {
                if (control.Visible)
                {
                    num += control.Height;
                }
            }
            this.pnlDescContainer.Height = num;
        }

        private void UpdateMemberList()
        {
            this.memberInfo.Refresh();
            this.UpdateDescriptionPanel();
            this.lblMemberListHeader.Visible = this.pnlDescriptionSeparator4.Visible = this.IsExistMembers;
            this.lblDescriptionHeader.Visible = this.pnlDescriptionSeparator2.Visible = this.IsExistDescription;
            int num = 0;
            foreach (Control control in this.pnlTextContainer.Controls)
            {
                if (control.Visible)
                {
                    num += control.Height;
                }
            }
            this.pnlDescContainer.Height = num;
        }

        private bool IsExistDescription
        {
            get
            {
                return !string.IsNullOrEmpty(this.lblDescription.Text);
            }
        }

        private bool IsExistMembers
        {
            get
            {
                return this.isExistMembers;
            }
        }
    }
}

