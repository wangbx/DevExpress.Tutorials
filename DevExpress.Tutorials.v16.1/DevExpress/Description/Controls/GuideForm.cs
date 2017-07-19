namespace DevExpress.Description.Controls
{
    using DevExpress.Skins;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Controls;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    public class GuideForm : XtraForm, IGuideForm
    {
        private SimpleButton btHide;
        private IContainer components;
        private GroupControl groupControl1;
        private LabelControl labelControl1;
        private LabelControl lbDescription;
        private PanelControl panelControl1;

        event EventHandler IGuideForm.FormClosed
        {
            add
            {
                this.formClosed += value;
            }
            remove
            {
                this.formClosed -= value;
            }
        }

        private event EventHandler formClosed;

        public GuideForm()
        {
            this.InitializeComponent();
        }

        private Rectangle ApplyCenterbounds(Rectangle workingArea, Rectangle controlBounds, Rectangle formBounds, bool horz)
        {
            Point empty = Point.Empty;
            Rectangle rectangle = formBounds;
            if (formBounds.Width <= controlBounds.Width)
            {
                empty.X = controlBounds.X + ((controlBounds.Width - formBounds.Width) / 2);
            }
            else
            {
                empty.X = controlBounds.X + ((controlBounds.Width - formBounds.Width) / 2);
            }
            if (formBounds.Height <= controlBounds.Height)
            {
                empty.Y = controlBounds.Y + ((controlBounds.Height - formBounds.Height) / 2);
            }
            else
            {
                empty.Y = controlBounds.Y + ((controlBounds.Height - formBounds.Height) / 2);
            }
            if (horz)
            {
                formBounds.X = empty.X;
            }
            else
            {
                formBounds.Y = empty.Y;
            }
            if (workingArea.Contains(formBounds))
            {
                return formBounds;
            }
            return rectangle;
        }

        private void btHide_Click(object sender, EventArgs e)
        {
            base.Close();
            base.Disposed += new EventHandler(this.GuideForm_Disposed);
        }

        bool IGuideForm.IsHandle(IntPtr hwnd)
        {
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Rectangle GetDisplayBounds(GuideControlDescription description)
        {
            Rectangle screenBounds = description.ScreenBounds;
            Rectangle workingArea = Screen.FromRectangle(screenBounds).WorkingArea;
            Rectangle empty = this.TryAllBounds(workingArea, screenBounds, (((float) screenBounds.Width) / ((float) screenBounds.Height)) > 2.5f);
            if (!empty.IsEmpty)
            {
                Point point = new Point(Math.Abs((int) (Control.MousePosition.X - empty.X)), Math.Abs((int) (Control.MousePosition.Y - empty.Y)));
                Point point2 = new Point(Math.Abs((int) (Control.MousePosition.X - empty.Right)), Math.Abs((int) (Control.MousePosition.Y - empty.Bottom)));
                point.X = Math.Min(point.X, point2.X);
                point.Y = Math.Min(point.Y, point2.Y);
                if ((point.X > 300) || (point.Y > 300))
                {
                    empty = Rectangle.Empty;
                }
            }
            if (empty.IsEmpty)
            {
                empty = this.TryAllBounds(workingArea, new Rectangle(Control.MousePosition, new Size(0x10, 0x10)), true);
            }
            if (empty.IsEmpty)
            {
                empty = RectangleHelper.GetCenterBounds(workingArea, base.Size);
            }
            return empty;
        }

        private void GuideForm_Disposed(object sender, EventArgs e)
        {
            this.formClosed = null;
        }

        private void InitializeComponent()
        {
            this.panelControl1 = new PanelControl();
            this.btHide = new SimpleButton();
            this.groupControl1 = new GroupControl();
            this.lbDescription = new LabelControl();
            this.labelControl1 = new LabelControl();
            this.panelControl1.BeginInit();
            this.panelControl1.SuspendLayout();
            this.groupControl1.BeginInit();
            this.groupControl1.SuspendLayout();
            base.SuspendLayout();
            this.panelControl1.BorderStyle = BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.btHide);
            this.panelControl1.Dock = DockStyle.Bottom;
            this.panelControl1.Location = new Point(0, 0xa3);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new Size(0x21f, 10);
            this.panelControl1.TabIndex = 0;
            this.panelControl1.Visible = false;
            this.btHide.DialogResult = DialogResult.Cancel;
            this.btHide.Location = new Point(440, 11);
            this.btHide.Name = "btHide";
            this.btHide.Size = new Size(0x4b, 0x17);
            this.btHide.TabIndex = 0;
            this.btHide.Text = "Hide";
            this.btHide.Click += new EventHandler(this.btHide_Click);
            this.groupControl1.BorderStyle = BorderStyles.NoBorder;
            this.groupControl1.Controls.Add(this.lbDescription);
            this.groupControl1.Dock = DockStyle.Fill;
            this.groupControl1.Location = new Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Padding = new Padding(3);
            this.groupControl1.ShowCaption = false;
            this.groupControl1.Size = new Size(0x21f, 0x99);
            this.groupControl1.TabIndex = 1;
            this.groupControl1.Text = "Description";
            this.lbDescription.AllowHtmlString = true;
            this.lbDescription.Appearance.Font = new Font("Segoe UI", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0xcc);
            this.lbDescription.Appearance.TextOptions.VAlignment = VertAlignment.Top;
            this.lbDescription.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            this.lbDescription.AutoSizeMode = LabelAutoSizeMode.None;
            this.lbDescription.BorderStyle = BorderStyles.NoBorder;
            this.lbDescription.Cursor = Cursors.Default;
            this.lbDescription.Dock = DockStyle.Fill;
            this.lbDescription.Location = new Point(3, 3);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new Size(0x219, 0x93);
            this.lbDescription.TabIndex = 0;
            this.lbDescription.Text = "Grid\r\n\r\nTo get more information visit <href=https://www.devexpress.com/Products/NET/Controls/WinForms/Grid/>Learn more</href>\r\n";
            this.lbDescription.HyperlinkClick += new HyperlinkClickEventHandler(this.lbDescription_HyperlinkClick);
            this.labelControl1.AutoSizeMode = LabelAutoSizeMode.None;
            this.labelControl1.Dock = DockStyle.Bottom;
            this.labelControl1.Location = new Point(0, 0x99);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new Size(0x21f, 10);
            this.labelControl1.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btHide;
            base.ClientSize = new Size(0x21f, 0xad);
            base.Controls.Add(this.groupControl1);
            base.Controls.Add(this.labelControl1);
            base.Controls.Add(this.panelControl1);
            base.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            base.LookAndFeel.SkinName = "Office 2016 Colorful";
            base.LookAndFeel.UseDefaultLookAndFeel = false;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "GuideForm";
            base.ShowInTaskbar = false;
            this.Text = "Description";
            base.TopMost = true;
            this.panelControl1.EndInit();
            this.panelControl1.ResumeLayout(false);
            this.groupControl1.EndInit();
            this.groupControl1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void lbDescription_HyperlinkClick(object sender, HyperlinkClickEventArgs e)
        {
            using (Process process = new Process())
            {
                process.StartInfo = new ProcessStartInfo(e.Link);
                process.Start();
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (this.formClosed != null)
            {
                this.formClosed(this, EventArgs.Empty);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                base.Close();
            }
            return base.ProcessDialogKey(keyData);
        }

        public virtual void Show(GuideControl owner, GuideControlDescription description)
        {
            string text = this.lbDescription.Text;
            if (!string.IsNullOrEmpty(description.Description))
            {
                text = description.Description;
            }
            else
            {
                text = string.Format("<b>{0}</b><br>", description.GetTypeName()) + text;
            }
            this.lbDescription.Text = text;
            Rectangle displayBounds = this.GetDisplayBounds(description);
            if (!displayBounds.IsEmpty)
            {
                base.StartPosition = FormStartPosition.Manual;
                base.Bounds = displayBounds;
            }
            base.TopMost = false;
            base.Show(owner.Window);
        }

        private Rectangle TryAllBounds(Rectangle workingArea, Rectangle bounds, bool bottomFirst)
        {
            Rectangle rectangle;
            if (this.TryBounds(workingArea, bounds, new Rectangle(bounds.X, bounds.Bottom + 8, base.Size.Width, base.Size.Height), true, out rectangle))
            {
                return rectangle;
            }
            if (bottomFirst && this.TryBounds(workingArea, bounds, new Rectangle(bounds.X, (bounds.Y - base.Size.Height) - 8, base.Size.Width, base.Size.Height), true, out rectangle))
            {
                return rectangle;
            }
            if (this.TryBounds(workingArea, bounds, new Rectangle((bounds.X - base.Size.Width) - 8, bounds.Y - base.Size.Height, base.Size.Width, base.Size.Height), false, out rectangle))
            {
                return rectangle;
            }
            if (this.TryBounds(workingArea, bounds, new Rectangle(bounds.Right + 8, bounds.Y - base.Size.Height, base.Size.Width, base.Size.Height), false, out rectangle))
            {
                return rectangle;
            }
            if (!bottomFirst && this.TryBounds(workingArea, bounds, new Rectangle(bounds.X, bounds.Y - base.Size.Height, base.Size.Width, base.Size.Height), true, out rectangle))
            {
                return rectangle;
            }
            return Rectangle.Empty;
        }

        private bool TryBounds(Rectangle workingArea, Rectangle controlBounds, Rectangle formBounds, bool horz, out Rectangle bounds)
        {
            bounds = formBounds;
            if (workingArea.Contains(formBounds))
            {
                bounds = this.ApplyCenterbounds(workingArea, controlBounds, formBounds, horz);
                return true;
            }
            if ((workingArea.Y <= formBounds.Y) && (workingArea.Bottom >= formBounds.Bottom))
            {
                if (workingArea.X > formBounds.X)
                {
                    formBounds.X += workingArea.X - formBounds.X;
                }
                if (workingArea.Right < formBounds.Right)
                {
                    formBounds.X -= formBounds.Right - workingArea.Right;
                }
                bounds = formBounds;
                if (!formBounds.IntersectsWith(controlBounds))
                {
                    bounds = this.ApplyCenterbounds(workingArea, controlBounds, formBounds, horz);
                    if (!controlBounds.IntersectsWith(bounds))
                    {
                        return true;
                    }
                }
            }
            if ((workingArea.X <= formBounds.X) && (workingArea.Right >= formBounds.Right))
            {
                if (workingArea.Y > formBounds.Y)
                {
                    formBounds.Y += workingArea.Y - formBounds.Y;
                }
                if (workingArea.Bottom < formBounds.Bottom)
                {
                    formBounds.Y -= formBounds.Bottom - workingArea.Bottom;
                }
                bounds = formBounds;
                if (!formBounds.IntersectsWith(controlBounds))
                {
                    bounds = this.ApplyCenterbounds(workingArea, controlBounds, formBounds, horz);
                    if (!controlBounds.IntersectsWith(bounds))
                    {
                        return true;
                    }
                }
            }
            bounds = Rectangle.Empty;
            return false;
        }

        bool IGuideForm.Visible
        {
            get
            {
                return base.Visible;
            }
        }
    }
}

