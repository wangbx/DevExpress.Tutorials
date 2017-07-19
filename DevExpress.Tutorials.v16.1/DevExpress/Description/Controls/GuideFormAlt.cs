namespace DevExpress.Description.Controls
{
    using DevExpress.NoteHint;
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows;
    using System.Windows.Interop;

    public class GuideFormAlt : IGuideForm
    {
        private NoteWindow note;

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

        void IGuideForm.Dispose()
        {
            this.formClosed = null;
            this.note.Hide();
        }

        bool IGuideForm.IsHandle(IntPtr hwnd)
        {
            if ((this.note == null) || (hwnd == IntPtr.Zero))
            {
                return false;
            }
            HwndSource source = HwndSource.FromHwnd(hwnd);
            if (source == null)
            {
                return false;
            }
            Window rootVisual = (Window) source.RootVisual;
            return (rootVisual == this.note);
        }

        void IGuideForm.Show(GuideControl owner, GuideControlDescription description)
        {
            this.note = new NoteWindow();
            WindowInteropHelper helper = new WindowInteropHelper(this.note) {
                Owner = owner.Window.Handle
            };
            this.note.IsVisibleChanged += delegate (object s, DependencyPropertyChangedEventArgs e) {
                if (!this.note.IsVisible && (this.formClosed != null))
                {
                    this.formClosed(this, EventArgs.Empty);
                }
            };
            string text = "";
            if (!string.IsNullOrEmpty(description.Description))
            {
                text = description.Description;
            }
            else
            {
                text = string.Format("<b>{0}</b><br>", description.GetTypeName()) + text;
            }
            this.note.ShowHtmlCloseButton = true;
            this.note.SetHtmlContent(text, description);
            Rectangle screenBounds = description.ScreenBounds;
            this.note.HintPosition = NoteHintPosition.Down;
            this.note.Show(new Rect((double) screenBounds.X, (double) screenBounds.Y, (double) screenBounds.Width, (double) screenBounds.Height));
        }

        bool IGuideForm.Visible
        {
            get
            {
                return ((this.note != null) && this.note.IsVisible);
            }
        }
    }
}

