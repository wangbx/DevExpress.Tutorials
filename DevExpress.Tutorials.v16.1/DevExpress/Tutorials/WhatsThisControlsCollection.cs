namespace DevExpress.Tutorials
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class WhatsThisControlsCollection : CollectionBase
    {
        public void Add(WhatsThisControlEntry entry)
        {
            base.List.Add(entry);
        }

        public bool HasControl(string controlName)
        {
            foreach (WhatsThisControlEntry entry in this)
            {
                if (entry.ControlName.Equals(controlName))
                {
                    return true;
                }
            }
            return false;
        }

        public WhatsThisParams PopupInfoByControlName(string controlName)
        {
            foreach (WhatsThisControlEntry entry in this)
            {
                if (entry.ControlName.Equals(controlName))
                {
                    return entry.PopupInfo;
                }
            }
            return null;
        }

        public void ResetControlCodes()
        {
            foreach (WhatsThisControlEntry entry in this)
            {
                entry.PopupInfo.Code = string.Empty;
            }
        }

        public void SetControlBitmapBounds(Bitmap bmp, Control control)
        {
            foreach (WhatsThisControlEntry entry in this)
            {
                if (entry.ControlName.Equals(control.Name))
                {
                    entry.ControlScreenRect = control.RectangleToScreen(new Rectangle(0, 0, control.Width, control.Height));
                    entry.ControlBitmap = bmp;
                }
            }
        }

        public void UpdateControlRects(int offsetX, int offsetY)
        {
            foreach (WhatsThisControlEntry entry in this)
            {
                entry.ControlScreenRect = new Rectangle(entry.ControlScreenRect.Left + offsetX, entry.ControlScreenRect.Y + offsetY, entry.ControlScreenRect.Width, entry.ControlScreenRect.Height);
            }
        }

        public WhatsThisControlEntry this[int index]
        {
            get
            {
                return (base.List[index] as WhatsThisControlEntry);
            }
        }

        public WhatsThisControlEntry this[string controlName]
        {
            get
            {
                foreach (WhatsThisControlEntry entry in this)
                {
                    if (entry.ControlName == controlName)
                    {
                        return entry;
                    }
                }
                return null;
            }
        }
    }
}

