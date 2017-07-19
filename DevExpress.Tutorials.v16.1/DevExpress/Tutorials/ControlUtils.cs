namespace DevExpress.Tutorials
{
    using DevExpress.Utils.Drawing;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class ControlUtils
    {
        public static void AlignControlInParent(Control ctrl, int boundsOffset)
        {
            HorzAlignControlInParent(ctrl, boundsOffset);
            VertAlignControlInParent(ctrl, boundsOffset);
        }

        public static void CenterControlInParent(Control ctrl)
        {
            CenterControlInParentCustom(ctrl, true, true);
        }

        public static void CenterControlInParentCustom(Control ctrl, bool centerHorz, bool centerVert)
        {
            Control parent = ctrl.Parent;
            if (centerHorz)
            {
                ctrl.Left = (parent.Width - ctrl.Width) / 2;
            }
            if (centerVert)
            {
                ctrl.Top = (parent.Height - ctrl.Height) / 2;
            }
        }

        public static bool ControlHasInvisibleParent(Control control)
        {
            for (Control control2 = control; control2.Parent != null; control2 = control2.Parent)
            {
                if (!control2.Parent.Visible)
                {
                    return true;
                }
            }
            return false;
        }

        public static Control GetControlByName(string name, Control parent)
        {
            FindControlByNameIterator iterator = new FindControlByNameIterator(name, parent);
            iterator.ProcessControls();
            return iterator.ControlResult;
        }

        private static string GetCorrectNewLineString(string s)
        {
            int index = s.IndexOf("\r");
            if ((index != -1) && (s.IndexOf("\r\n") != index))
            {
                return s.Replace("\r", "\r\n");
            }
            return s;
        }

        private static Rectangle GetCurrentScreenBounds(Point location)
        {
            Rectangle workingArea = SystemInformation.WorkingArea;
            if (SystemInformation.MonitorCount > 1)
            {
                workingArea = Screen.FromPoint(location).Bounds;
            }
            return workingArea;
        }

        public static void HorzAlignControlInParent(Control ctrl, int boundsOffset)
        {
            ctrl.Left = boundsOffset;
            ctrl.Width = ctrl.Parent.Width - (boundsOffset * 2);
        }

        public static void UpdateFrmToFitScreen(Form frm)
        {
            Rectangle currentScreenBounds = GetCurrentScreenBounds(frm.Location);
            if ((frm.Top + frm.Height) > (currentScreenBounds.Top + currentScreenBounds.Height))
            {
                frm.Top = (currentScreenBounds.Top + currentScreenBounds.Height) - frm.Height;
            }
            if ((frm.Left + frm.Width) > (currentScreenBounds.X + currentScreenBounds.Width))
            {
                frm.Left = (currentScreenBounds.X + currentScreenBounds.Width) - frm.Width;
            }
        }

        public static void UpdateLabelHeight(Label label)
        {
            StringFormat format = new StringFormat {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Near
            };
            format.FormatFlags &= ~StringFormatFlags.NoWrap;
            SizeF empty = SizeF.Empty;
            GraphicsInfo info = new GraphicsInfo();
            info.AddGraphics(null);
            try
            {
                empty = info.Graphics.MeasureString(GetCorrectNewLineString(label.Text), label.Font, label.Width, format);
            }
            finally
            {
                info.ReleaseGraphics();
            }
            label.Height = Convert.ToInt32(empty.Height);
        }

        public static void VertAlignControlInParent(Control ctrl, int boundsOffset)
        {
            ctrl.Top = boundsOffset;
            ctrl.Height = ctrl.Parent.Height - (boundsOffset * 2);
        }
    }
}

