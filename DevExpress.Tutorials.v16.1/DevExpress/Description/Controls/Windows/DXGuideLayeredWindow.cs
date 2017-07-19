namespace DevExpress.Description.Controls.Windows
{
    using DevExpress.Description.Controls;
    using System;
    using System.Drawing;

    public class DXGuideLayeredWindow : DXLayeredWindowAlt
    {
        private GuideControl owner;

        public DXGuideLayeredWindow(GuideControl owner)
        {
            this.owner = owner;
        }

        protected override void DrawBackground(Graphics g)
        {
        }

        protected override IntPtr InsertAfterWindow
        {
            get
            {
                return this.Owner.Root.Handle;
            }
        }

        public GuideControl Owner
        {
            get
            {
                return this.owner;
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

