namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class WhatsThisControlImageProcessor : ControlIterator
    {
        private WhatsThisController controller;
        private Graphics g;

        public WhatsThisControlImageProcessor(Control startControl, WhatsThisController controller, Graphics g) : base(startControl)
        {
            this.controller = controller;
            this.g = g;
        }

        protected override void ProcessControl(Control control)
        {
            if (!ControlUtils.ControlHasInvisibleParent(control) && this.controller.RegisteredVisibleControls.HasControl(control.Name))
            {
                Bitmap controlBitmap = ControlImageCapturer.GetControlBitmap(control, null);
                Point point = this.controller.CurrentModule.PointToClient(control.PointToScreen(new Point(0, 0)));
                this.g.DrawImage(controlBitmap, point.X, point.Y, controlBitmap.Width, controlBitmap.Height);
                this.controller.Collection.SetControlBitmapBounds(controlBitmap, control);
            }
        }
    }
}

