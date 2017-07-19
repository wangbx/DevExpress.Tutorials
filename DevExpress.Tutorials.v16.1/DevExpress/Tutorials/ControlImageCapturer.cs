namespace DevExpress.Tutorials
{
    using DevExpress.Utils.Drawing.Helpers;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ControlImageCapturer
    {
        [DllImport("gdi32.dll")]
        internal static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreatePatternBrush(IntPtr hBitmap);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr CreateSolidBrush(int color);
        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);
        public static Bitmap GetControlBitmap(Control control, Bitmap pattern)
        {
            int width = control.Width;
            int height = control.Height;
            if (control is Form)
            {
                width = control.ClientRectangle.Width;
                height = control.ClientRectangle.Height;
            }
            IntPtr dC = GetDC(control.Handle);
            IntPtr hdc = CreateCompatibleDC(dC);
            IntPtr ptr3 = CreateCompatibleBitmap(dC, width, height);
            IntPtr ptr4 = SelectObject(hdc, ptr3);
            IntPtr zero = IntPtr.Zero;
            IntPtr ptr6 = IntPtr.Zero;
            if (pattern != null)
            {
                zero = CreatePatternBrush(pattern.GetHbitmap());
                ptr6 = SelectObject(hdc, zero);
            }
            Point point = new Point(0, 0);
            BitBlt(hdc, 0, 0, width, height, dC, point.X, point.Y, 0xc000ca);
            SelectObject(hdc, ptr4);
            if (ptr6 != IntPtr.Zero)
            {
                SelectObject(hdc, ptr6);
            }
            ReleaseDC(control.Handle, dC);
            DevExpress.Utils.Drawing.Helpers.NativeMethods.DeleteDC(hdc);
            Bitmap bitmap = Image.FromHbitmap(ptr3);
            DeleteObject(ptr3);
            if (zero != IntPtr.Zero)
            {
                DeleteObject(zero);
            }
            return bitmap;
        }

        [DllImport("USER32.dll")]
        internal static extern IntPtr GetDC(IntPtr dc);
        [DllImport("USER32.dll")]
        internal static extern IntPtr GetDesktopWindow();
        [DllImport("USER32.dll")]
        internal static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("gdi32.dll")]
        internal static extern IntPtr SelectObject(IntPtr hdc, IntPtr obj);
    }
}

