namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public class ControlHotTrackPainterHint : ControlHotTrackPainterBase
    {
        public ControlHotTrackPainterHint(ModuleWhatsThis module) : base(module)
        {
        }

        public override void DrawHotTrackSign(Graphics g, WhatsThisControlEntry entry)
        {
            Rectangle controlRect = base.module.RectangleToClient(entry.ControlScreenRect);
            Rectangle hintRect = this.GetHintRect(controlRect);
            Rectangle shadowRect = this.GetShadowRect(hintRect);
            g.FillRectangle(new SolidBrush(Color.FromArgb(150, SystemColors.ControlDark)), shadowRect);
            g.FillRectangle(SystemBrushes.Info, hintRect);
            hintRect.Width--;
            hintRect.Height--;
            g.DrawRectangle(SystemPens.WindowText, hintRect);
            g.DrawImage(entry.ControlBitmap, controlRect);
        }

        private Rectangle GetHintRect(Rectangle controlRect)
        {
            return base.GetOuterRect(controlRect, 4);
        }

        public override Region GetInvalidateRegion(WhatsThisControlEntry entry)
        {
            Rectangle controlRect = base.module.RectangleToClient(entry.ControlScreenRect);
            Region region = new Region(this.GetHintRect(controlRect));
            region.Union(this.GetShadowRect(this.GetHintRect(controlRect)));
            region.Exclude(controlRect);
            return region;
        }

        private Rectangle GetShadowRect(Rectangle hintRect)
        {
            return new Rectangle(hintRect.X + 3, hintRect.Y + 3, hintRect.Width, hintRect.Height);
        }

        public override string Name
        {
            get
            {
                return "Hint";
            }
        }
    }
}

