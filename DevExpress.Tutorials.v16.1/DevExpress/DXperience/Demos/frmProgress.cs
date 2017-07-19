namespace DevExpress.DXperience.Demos
{
    using DevExpress.Tutorials.Properties;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    internal class frmProgress : XtraForm
    {
        private IContainer components;
        private LayoutControl mainLayoutControl;
        private Form parent;
        private ProgressBarControl progressBarControl;
        private LayoutControlItem progressBarControlLCI;
        private const int requiredCount = 200;
        private const int requiredDataCount = 20;
        private int requiredUpdateCount;
        private LayoutControlGroup rootGroup;

        public frmProgress() : this(null)
        {
        }

        public frmProgress(Form parent)
        {
            this.parent = parent;
            this.InitializeComponent();
            this.progressBarControlLCI.Text = Resources.LookAndFeelChanging;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void FormInvalidate()
        {
            this.mainLayoutControl.Refresh();
            this.Refresh();
        }

        public void HideProgress()
        {
            this.progressBarControl.Position = this.progressBarControl.Properties.Maximum;
            this.progressBarControl.Refresh();
            base.Hide();
        }

        private void InitializeComponent()
        {
            this.progressBarControl = new ProgressBarControl();
            this.mainLayoutControl = new LayoutControl();
            this.rootGroup = new LayoutControlGroup();
            this.progressBarControlLCI = new LayoutControlItem();
            this.progressBarControl.Properties.BeginInit();
            this.mainLayoutControl.BeginInit();
            this.mainLayoutControl.SuspendLayout();
            this.rootGroup.BeginInit();
            this.progressBarControlLCI.BeginInit();
            base.SuspendLayout();
            this.progressBarControl.Location = new Point(0x10, 0x22);
            this.progressBarControl.Name = "progressBarControl";
            this.progressBarControl.Properties.ShowTitle = true;
            this.progressBarControl.Size = new Size(0x1bc, 0x10);
            this.progressBarControl.StyleController = this.mainLayoutControl;
            this.progressBarControl.TabIndex = 0;
            this.mainLayoutControl.AllowCustomization = false;
            this.mainLayoutControl.AutoScroll = false;
            this.mainLayoutControl.Controls.Add(this.progressBarControl);
            this.mainLayoutControl.Dock = DockStyle.Fill;
            this.mainLayoutControl.Location = new Point(0, 0);
            this.mainLayoutControl.Name = "mainLayoutControl";
            this.mainLayoutControl.OptionsView.UseParentAutoScaleFactor = true;
            this.mainLayoutControl.Root = this.rootGroup;
            this.mainLayoutControl.Size = new Size(0x1dc, 0x42);
            this.mainLayoutControl.TabIndex = 2;
            this.rootGroup.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.rootGroup.GroupBordersVisible = false;
            this.rootGroup.Items.AddRange(new BaseLayoutItem[] { this.progressBarControlLCI });
            this.rootGroup.Location = new Point(0, 0);
            this.rootGroup.Name = "rootGroup";
            this.rootGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(14, 14, 14, 14);
            this.rootGroup.Size = new Size(0x1dc, 0x42);
            this.rootGroup.TextVisible = false;
            this.progressBarControlLCI.AllowHtmlStringInCaption = true;
            this.progressBarControlLCI.Control = this.progressBarControl;
            this.progressBarControlLCI.Location = new Point(0, 0);
            this.progressBarControlLCI.MinSize = new Size(0x85, 0x26);
            this.progressBarControlLCI.Name = "progressBarControlLCI";
            this.progressBarControlLCI.Size = new Size(0x1c0, 0x26);
            this.progressBarControlLCI.SizeConstraintsType = SizeConstraintsType.Custom;
            this.progressBarControlLCI.Text = "Changing <b>Look and Feel</b>:";
            this.progressBarControlLCI.TextAlignMode = TextAlignModeItem.AutoSize;
            this.progressBarControlLCI.TextLocation = Locations.Top;
            this.progressBarControlLCI.TextSize = new Size(0x81, 13);
            this.progressBarControlLCI.TextToControlDistance = 5;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1dc, 0x42);
            base.ControlBox = false;
            base.Controls.Add(this.mainLayoutControl);
            base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "frmProgress";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.TopMost = true;
            this.progressBarControl.Properties.EndInit();
            this.mainLayoutControl.EndInit();
            this.mainLayoutControl.ResumeLayout(false);
            this.rootGroup.EndInit();
            this.progressBarControlLCI.EndInit();
            base.ResumeLayout(false);
        }

        private void Locate()
        {
            if (this.parent != null)
            {
                base.ClientSize = this.mainLayoutControl.Root.Size;
                base.Location = new Point(this.parent.Bounds.X + ((this.parent.Width - base.Width) / 2), this.parent.Bounds.Y + ((this.parent.Height - base.Height) / 2));
            }
        }

        public void Progress(int index)
        {
            if (base.Visible && ((index % this.requiredUpdateCount) == 0))
            {
                this.progressBarControl.Position = index;
                this.progressBarControl.Refresh();
            }
        }

        public void ShowProgress(int count)
        {
            this.Locate();
            this.progressBarControl.Position = 0;
            this.progressBarControl.Properties.Maximum = count;
            this.requiredUpdateCount = count / 0x37;
            if (this.requiredUpdateCount == 0)
            {
                this.requiredUpdateCount = 1;
            }
            if (count > 200)
            {
                base.Show();
            }
            this.FormInvalidate();
        }

        public void ShowProgress(int recordCount, string caption)
        {
            this.Locate();
            this.progressBarControl.Position = 0;
            this.progressBarControl.Properties.Maximum = 100;
            this.requiredUpdateCount = 1;
            this.progressBarControlLCI.Text = caption;
            if (recordCount > 20)
            {
                base.Show();
            }
            this.FormInvalidate();
        }
    }
}

