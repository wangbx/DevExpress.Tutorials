namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;

    public abstract class ControlHotTrackPainterBase
    {
        protected ModuleWhatsThis module;

        public ControlHotTrackPainterBase(ModuleWhatsThis module)
        {
            this.module = module;
        }

        public abstract void DrawHotTrackSign(Graphics g, WhatsThisControlEntry entry);
        public abstract Region GetInvalidateRegion(WhatsThisControlEntry entry);
        protected Rectangle GetOuterRect(Rectangle rect, int offset)
        {
            return new Rectangle(rect.Left - offset, rect.Top - offset, rect.Width + (offset * 2), rect.Height + (offset * 2));
        }

        protected virtual int HotTrackRegionWidth
        {
            get
            {
                return 3;
            }
        }

        public virtual string Name
        {
            get
            {
                return "Base";
            }
        }
    }
}

