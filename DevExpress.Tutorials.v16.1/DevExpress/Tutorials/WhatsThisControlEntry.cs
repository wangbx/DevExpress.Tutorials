namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class WhatsThisControlEntry
    {
        private System.Windows.Forms.Control control;
        private Bitmap controlBitmap;
        private string controlName;
        private Rectangle controlScreenRect;
        private WhatsThisParams popupInfo;

        public WhatsThisControlEntry(string controlName, WhatsThisParams popupInfo)
        {
            this.controlName = controlName;
            this.popupInfo = popupInfo;
            this.controlBitmap = null;
            this.controlScreenRect = Rectangle.Empty;
            this.control = null;
        }

        public string ClosingTag
        {
            get
            {
                return ("</" + this.controlName + ">");
            }
        }

        public System.Windows.Forms.Control Control
        {
            get
            {
                return this.control;
            }
            set
            {
                this.control = value;
            }
        }

        public Bitmap ControlBitmap
        {
            get
            {
                return this.controlBitmap;
            }
            set
            {
                this.controlBitmap = value;
            }
        }

        public string ControlName
        {
            get
            {
                return this.controlName;
            }
        }

        public Rectangle ControlScreenRect
        {
            get
            {
                return this.controlScreenRect;
            }
            set
            {
                this.controlScreenRect = value;
            }
        }

        public string OpeningTag
        {
            get
            {
                return ("<" + this.controlName + ">");
            }
        }

        public WhatsThisParams PopupInfo
        {
            get
            {
                return this.popupInfo;
            }
        }
    }
}

