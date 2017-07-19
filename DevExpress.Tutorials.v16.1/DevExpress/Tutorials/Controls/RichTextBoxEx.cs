namespace DevExpress.Tutorials.Controls
{
    using DevExpress.Utils.Drawing.Helpers;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Drawing;
    using System;
    using System.Windows.Forms;

    public class RichTextBoxEx : RichTextBox, IMouseWheelSupport
    {
        private int lockSmartMouse;

        void IMouseWheelSupport.OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                this.lockSmartMouse++;
                if (base.IsHandleCreated)
                {
                    DevExpress.Utils.Drawing.Helpers.NativeMethods.SendMessage(base.Handle, 0x20a, new IntPtr(e.Delta << 0x10), new IntPtr(e.X + (e.Y << 0x10)));
                }
                else
                {
                    this.OnMouseWheelCore(e);
                }
            }
            finally
            {
                this.lockSmartMouse--;
            }
        }

        protected sealed override void OnMouseWheel(MouseEventArgs ev)
        {
            if ((this.lockSmartMouse != 0) || !XtraForm.IsAllowSmartMouseWheel(this, ev))
            {
                this.OnMouseWheelCore(ev);
            }
        }

        protected virtual void OnMouseWheelCore(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
        }
    }
}

