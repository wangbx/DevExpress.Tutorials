namespace DevExpress.Description.Controls
{
    using DevExpress.Description.Controls.Windows;
    using DevExpress.Utils.Drawing.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class GuideControlEx : GuideControl, IMessageFilter
    {
        private GuideControlDescription activeControl;
        private IGuideForm activeGuideForm;
        private bool hasMessageFilter;
        private bool trackMouse;

        private void ActivateRootControl()
        {
            if (base.Root != null)
            {
                Form form = base.Root.FindForm();
                if (form != null)
                {
                    form.Activate();
                }
            }
        }

        private bool CheckIsGuideFormMessage(ref Message m)
        {
            if ((this.ActiveGuideForm == null) || !this.ActiveGuideForm.Visible)
            {
                return false;
            }
            if (this.ActiveGuideForm.IsHandle(m.HWnd))
            {
                return true;
            }
            Control control = Control.FromHandle(m.HWnd);
            if (control == null)
            {
                return false;
            }
            return (control.FindForm() == this.ActiveGuideForm);
        }

        private int CompareControls(GuideControlDescription x, GuideControlDescription y)
        {
            if (!object.ReferenceEquals(x, y) && ((x.AssociatedControl != null) && (y.AssociatedControl != null)))
            {
                if (x.AssociatedControl.Contains(y.AssociatedControl))
                {
                    return -1;
                }
                if (y.AssociatedControl.Contains(x.AssociatedControl))
                {
                    return 1;
                }
                if (x.ControlBounds.Contains(y.ControlBounds))
                {
                    return -1;
                }
                if (y.ControlBounds.Contains(x.ControlBounds))
                {
                    return 1;
                }
            }
            return 0;
        }

        protected virtual IGuideForm CreageGuideForm()
        {
            return new GuideFormAlt();
        }

        protected override DXGuideLayeredWindow CreateWindow()
        {
            return new DXGuideLayeredWindowEx(this);
        }

        protected GuideControlDescription FromPoint(Point point)
        {
            List<GuideControlDescription> controls = null;
            GuideControlDescription description = null;
            foreach (GuideControlDescription description2 in base.Descriptions)
            {
                if (description2.IsValidNow && description2.ControlBounds.Contains(point))
                {
                    if (description == null)
                    {
                        description = description2;
                    }
                    else
                    {
                        if (controls == null)
                        {
                            controls = new List<GuideControlDescription> {
                                description
                            };
                        }
                        controls.Add(description2);
                    }
                }
            }
            if (controls == null)
            {
                return description;
            }
            return this.SelectControl(controls);
        }

        private void GenerateMouseMove()
        {
            Point point = base.ConvertPoint(base.Root.PointToClient(Control.MousePosition));
            this.OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, point.X, point.Y, 0));
        }

        protected MouseEventArgs GetMouseArgs(ref Message msg)
        {
            int num = msg.WParam.ToInt32();
            MouseButtons none = MouseButtons.None;
            if ((num & 1) != 0)
            {
                none |= MouseButtons.Left;
            }
            if ((num & 2) != 0)
            {
                none |= MouseButtons.Right;
            }
            Point point = this.PointToFormBounds(msg.HWnd, msg.LParam);
            return new MouseEventArgs(none, 1, point.X, point.Y, 0);
        }

        private void HookMouse()
        {
            if (!this.hasMessageFilter)
            {
                this.hasMessageFilter = true;
                Application.AddMessageFilter(this);
            }
        }

        private bool IsKeyboardMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x100:
                case 0x101:
                case 260:
                case 0x105:
                    return true;
            }
            return false;
        }

        private bool IsMouseMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x200:
                case 0x201:
                case 0x202:
                case 0x203:
                case 0x204:
                case 0x205:
                case 0x206:
                case 0x207:
                case 520:
                case 0x209:
                case 0x20a:
                case 0x20e:
                case 160:
                case 0x2a1:
                case 0x2a3:
                    return true;
            }
            return false;
        }

        protected virtual void OnActiveControlChanged()
        {
            if (this.IsVisible)
            {
                base.Window.Invalidate();
            }
        }

        private void OnActiveGuideForm_FormClosed(object sender, EventArgs e)
        {
            this.ActiveGuideForm = null;
        }

        protected override void OnHide()
        {
            base.OnHide();
            this.ActiveGuideForm = null;
            this.UnHookMouse();
        }

        private bool OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.Hide();
                return true;
            }
            if (e.Button == MouseButtons.Left)
            {
                GuideControlDescription description = this.FromPoint(e.Location);
                if ((description != null) && (description != this.ActiveControl))
                {
                    this.ActiveGuideForm = null;
                    this.ActiveControl = description;
                }
                if ((this.ActiveControl != null) && (this.ActiveGuideForm == null))
                {
                    this.ActiveGuideForm = this.CreageGuideForm();
                    this.ActiveGuideForm.Show(this, this.ActiveControl);
                    this.ActiveGuideForm.FormClosed += new EventHandler(this.OnActiveGuideForm_FormClosed);
                }
            }
            return true;
        }

        private void OnMouseLeave(ref Message m)
        {
            this.trackMouse = false;
            this.GenerateMouseMove();
        }

        private bool OnMouseMove(MouseEventArgs e)
        {
            GuideControlDescription description = this.FromPoint(e.Location);
            if (this.ActiveGuideForm == null)
            {
                Cursor.Current = (description == null) ? Cursors.Arrow : Cursors.Help;
            }
            this.ActiveControl = description;
            return true;
        }

        private bool OnMouseUp(MouseEventArgs e)
        {
            return true;
        }

        protected override void OnShowing()
        {
            this.HookMouse();
        }

        protected virtual Point PointToFormBounds(IntPtr hwnd, Point pt)
        {
            DevExpress.Utils.Drawing.Helpers.NativeMethods.POINT point = new DevExpress.Utils.Drawing.Helpers.NativeMethods.POINT(pt);
            DevExpress.Utils.Drawing.Helpers.NativeMethods.ClientToScreen(hwnd, ref point);
            return base.ConvertPoint(base.Root.PointToClient(new Point(point.X, point.Y)));
        }

        public Point PointToFormBounds(IntPtr hwnd, IntPtr ptr)
        {
            Point empty = Point.Empty;
            try
            {
                empty = new Point((int) ptr);
            }
            catch (Exception)
            {
                empty = Point.Empty;
            }
            return this.PointToFormBounds(hwnd, empty);
        }

        protected virtual bool ProcessKeyboard(ref Message m)
        {
            if ((m.Msg == 0x100) && (m.WParam.ToInt32() == 0x1b))
            {
                if (this.ActiveGuideForm != null)
                {
                    this.ActiveGuideForm = null;
                    return true;
                }
                this.Hide();
            }
            return true;
        }

        protected virtual bool ProcessMouse(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x200:
                    this.TrackMouseLeaveMessage(m.HWnd);
                    return this.OnMouseMove(this.GetMouseArgs(ref m));

                case 0x201:
                case 0x204:
                    return this.OnMouseDown(this.GetMouseArgs(ref m));

                case 0x202:
                case 0x205:
                    return this.OnMouseUp(this.GetMouseArgs(ref m));

                case 0x203:
                case 0x206:
                    return true;

                case 0x20a:
                case 0x20e:
                    return true;

                case 0x2a3:
                    this.OnMouseLeave(ref m);
                    return true;

                case 160:
                    this.GenerateMouseMove();
                    return true;
            }
            return false;
        }

        private GuideControlDescription SelectControl(List<GuideControlDescription> controls)
        {
            controls.Sort(new Comparison<GuideControlDescription>(this.CompareControls));
            return controls.Last<GuideControlDescription>();
        }

        bool IMessageFilter.PreFilterMessage(ref Message m)
        {
            if (this.IsKeyboardMessage(ref m))
            {
                return this.ProcessKeyboard(ref m);
            }
            if (this.CheckIsGuideFormMessage(ref m))
            {
                return false;
            }
            return (this.IsMouseMessage(ref m) && this.ProcessMouse(ref m));
        }

        private void TrackMouseLeaveMessage(IntPtr hwnd)
        {
            if (!this.trackMouse)
            {
                DevExpress.Utils.Drawing.Helpers.NativeMethods.TRACKMOUSEEVENTStruct tme = new DevExpress.Utils.Drawing.Helpers.NativeMethods.TRACKMOUSEEVENTStruct {
                    dwFlags = 3,
                    hwndTrack = hwnd
                };
                if (DevExpress.Utils.Drawing.Helpers.NativeMethods.TrackMouseEvent(tme))
                {
                    this.trackMouse = true;
                }
            }
        }

        private void UnHookMouse()
        {
            if (this.hasMessageFilter)
            {
                this.trackMouse = false;
                this.hasMessageFilter = false;
                Application.RemoveMessageFilter(this);
            }
        }

        public GuideControlDescription ActiveControl
        {
            get
            {
                return this.activeControl;
            }
            set
            {
                if ((this.ActiveControl != value) && (this.ActiveGuideForm == null))
                {
                    this.activeControl = value;
                    this.OnActiveControlChanged();
                }
            }
        }

        protected IGuideForm ActiveGuideForm
        {
            get
            {
                return this.activeGuideForm;
            }
            set
            {
                if (this.ActiveGuideForm != value)
                {
                    if (this.activeGuideForm != null)
                    {
                        this.activeGuideForm.Dispose();
                        this.ActivateRootControl();
                    }
                    this.activeGuideForm = value;
                }
            }
        }
    }
}

