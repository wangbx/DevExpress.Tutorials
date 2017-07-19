namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class ControlHotTrackPainterFrame : ControlHotTrackPainterBase
    {
        public ControlHotTrackPainterFrame(ModuleWhatsThis module) : base(module)
        {
        }

        public override void DrawHotTrackSign(Graphics g, WhatsThisControlEntry entry)
        {
            Region invalidateRegion = this.GetInvalidateRegion(entry);
            LinearGradientBrush brush = new LinearGradientBrush(invalidateRegion.GetBounds(g), ColorUtils.GetGradientActiveCaptionColor(), SystemColors.ActiveCaption, LinearGradientMode.ForwardDiagonal);
            g.FillRegion(brush, invalidateRegion);
        }

        public override Region GetInvalidateRegion(WhatsThisControlEntry entry)
        {
            Rectangle rect = base.module.RectangleToClient(entry.ControlScreenRect);
            Region region = new Region(base.GetOuterRect(rect, 3));
            region.Exclude(rect);
            return region;
        }

        public override string Name
        {
            get
            {
                return "Frame";
            }
        }
    }
}

