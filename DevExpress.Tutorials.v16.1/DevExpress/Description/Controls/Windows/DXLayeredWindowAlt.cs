namespace DevExpress.Description.Controls.Windows
{
    using DevExpress.Utils.Drawing.Helpers;
    using DevExpress.Utils.Internal;
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Windows.Forms;

    public class DXLayeredWindowAlt : DXLayeredWindow
    {
        private Bitmap buffer;

        public DXLayeredWindowAlt()
        {
            base.TransparencyKey = Color.Empty;
            base.Alpha = 0xff;
        }

        private Bitmap CheckBuffer(int width, int height)
        {
            if ((width < 1) || (height < 1))
            {
                return null;
            }
            if (this.buffer == null)
            {
                return new Bitmap(width, height);
            }
            if ((this.buffer.Width < width) || (this.buffer.Height < height))
            {
                this.buffer.Dispose();
                this.buffer = new Bitmap(width, height);
            }
            return this.buffer;
        }

        private bool CheckVisible(Control control)
        {
            if (control.FindForm() != null)
            {
                if (control.Parent != null)
                {
                    if (!control.Parent.Visible)
                    {
                        return false;
                    }
                    return this.CheckVisible(control.Parent);
                }
                return true;
            }
            return false;
        }

        public void CleanUp()
        {
            if (base.IsCreated)
            {
                this.DestroyHandle();
            }
            if (this.buffer != null)
            {
                this.buffer.Dispose();
            }
            this.buffer = null;
        }

        protected virtual void Draw()
        {
            if ((this.Size.Width >= 1) && (this.Size.Height >= 1))
            {
                using (Bitmap bitmap = this.CheckBuffer(this.Size.Width, this.Size.Height))
                {
                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        this.DrawBackgroundCore(graphics);
                        this.UpdateLayered(bitmap);
                    }
                }
            }
        }

        protected override void DrawBackground(Graphics g)
        {
        }

        protected virtual void DrawBackgroundCore(Graphics g)
        {
            g.Clear(Color.Transparent);
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(1, Color.LightGray)))
            {
                g.FillRectangle(brush, new Rectangle(Point.Empty, this.Size));
            }
            PaintEventArgs e = new PaintEventArgs(g, Rectangle.Empty);
            this.ProcessPaint(e);
        }

        protected override void DrawForeground(Graphics g)
        {
        }

        public override void Invalidate()
        {
            if (base.Visible)
            {
                this.Draw();
            }
        }

        protected virtual void ProcessPaint(PaintEventArgs e)
        {
        }

        private void UpdateLayered(Bitmap bmp)
        {
            if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
            {
                Rectangle rectangle = this.ValidateBounds(base.Bounds);
                if (!rectangle.IsEmpty)
                {
                    IntPtr dC = (IntPtr) DevExpress.Utils.Drawing.Helpers.NativeMethods.GetDC(IntPtr.Zero);
                    IntPtr hdc = DevExpress.Utils.Drawing.Helpers.NativeMethods.CreateCompatibleDC(dC);
                    IntPtr hbitmap = bmp.GetHbitmap(Color.FromArgb(0));
                    IntPtr ptr4 = DevExpress.Utils.Drawing.Helpers.NativeMethods.SelectObject(hdc, hbitmap);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.BLENDFUNCTION pBlend = new DevExpress.Utils.Drawing.Helpers.NativeMethods.BLENDFUNCTION {
                        BlendOp = 0,
                        BlendFlags = 0,
                        SourceConstantAlpha = 0xff,
                        AlphaFormat = 1
                    };
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.POINT pptDst = new DevExpress.Utils.Drawing.Helpers.NativeMethods.POINT(rectangle.Left, rectangle.Top);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.SIZE pSizeDst = new DevExpress.Utils.Drawing.Helpers.NativeMethods.SIZE(rectangle.Width, rectangle.Height);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.POINT pptSrc = new DevExpress.Utils.Drawing.Helpers.NativeMethods.POINT(0, 0);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.UpdateLayeredWindow(base.Handle, dC, ref pptDst, ref pSizeDst, hdc, ref pptSrc, 0, ref pBlend, 2);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.SelectObject(hdc, ptr4);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.DeleteObject(hbitmap);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.DeleteDC(hdc);
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.ReleaseDC(IntPtr.Zero, dC);
                }
            }
        }

        protected override void UpdateLayeredWindow()
        {
            this.Draw();
        }

        public override bool IsTransparent
        {
            get
            {
                return false;
            }
        }

        protected override bool UseDoubleBuffer
        {
            get
            {
                return true;
            }
        }
    }
}

