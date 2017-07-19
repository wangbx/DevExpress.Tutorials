namespace DevExpress.Description.Controls.Windows
{
    using DevExpress.Description.Controls;
    using DevExpress.Skins;
    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Text;

    public class DXGuideLayeredWindowEx : DXGuideLayeredWindow
    {
        private const int ShadowSize = 4;

        public DXGuideLayeredWindowEx(GuideControl owner) : base(owner)
        {
        }

        private Rectangle CalcInActiveInfoBounds(GraphicsCache cache, GuideControlDescription description)
        {
            Rectangle controlBounds = description.ControlBounds;
            int width = Math.Max(0x10, Math.Min(50, controlBounds.Height - 8));
            if (width > controlBounds.Height)
            {
                return Rectangle.Empty;
            }
            if (width > controlBounds.Width)
            {
                width = controlBounds.Width - 4;
            }
            if (width < 0x10)
            {
                return Rectangle.Empty;
            }
            return RectangleHelper.GetCenterBounds(controlBounds, new Size(width, width));
        }

        protected void DrawActiveControl(GraphicsCache cache, GuideControlDescription description)
        {
            if ((description.AssociatedControl != null) && description.ControlVisible)
            {
                this.DrawControlMarker(cache, description);
                Rectangle controlBounds = description.ControlBounds;
                this.DrawIntersectedInActiveControls(cache, description);
                Rectangle rect = Rectangle.Inflate(controlBounds, 4, 4);
                Rectangle r = Rectangle.Inflate(controlBounds, 2, 2);
                if (description.HighlightUseInsideBounds)
                {
                    r = Rectangle.Inflate(controlBounds, -2, -2);
                    rect = controlBounds;
                }
                cache.DrawRectangle(cache.GetPen(Color.Yellow, 3), r);
                if (description.HighlightUseInsideBounds)
                {
                    r = Rectangle.Inflate(r, 2, 2);
                }
                else
                {
                    r = Rectangle.Inflate(r, 2, 2);
                }
                cache.DrawRectangle(cache.GetPen(Color.Black, 1), r);
                Brush solidBrush = cache.GetSolidBrush(Color.FromArgb(100, Color.Black));
                Rectangle rectangle4 = new Rectangle(r.Right, r.Top + 4, 4, r.Height);
                cache.Paint.FillRectangle(cache.Graphics, solidBrush, rectangle4);
                cache.Graphics.ExcludeClip(rectangle4);
                rectangle4 = new Rectangle(r.X + 4, r.Bottom, r.Width, 4);
                cache.Paint.FillRectangle(cache.Graphics, solidBrush, rectangle4);
                cache.Graphics.ExcludeClip(rectangle4);
                cache.Graphics.ExcludeClip(rect);
            }
        }

        protected override void DrawBackgroundCore(Graphics g)
        {
            using (GraphicsCache cache = new GraphicsCache(g))
            {
                if (this.Owner.ActiveControl != null)
                {
                    this.DrawActiveControl(cache, this.Owner.ActiveControl);
                }
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(180, Color.Gray)))
                {
                    foreach (GuideControlDescription description in this.Owner.Descriptions)
                    {
                        if (description.IsValidNow && (this.Owner.ActiveControl != description))
                        {
                            this.DrawInActiveControl(cache, description);
                        }
                    }
                    foreach (GuideControlDescription description2 in this.Owner.Descriptions)
                    {
                        if (description2.IsValidNow)
                        {
                            g.ExcludeClip(description2.ControlBounds);
                        }
                    }
                    g.FillRectangle(brush, new Rectangle(Point.Empty, base.Bounds.Size));
                }
            }
        }

        private void DrawControlMarker(GraphicsCache cache, GuideControlDescription description)
        {
            Rectangle controlBounds = description.ControlBounds;
            Rectangle rect = this.CalcInActiveInfoBounds(cache, description);
            if (!rect.IsEmpty)
            {
                cache.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                cache.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                cache.Graphics.FillEllipse(cache.GetSolidBrush(Color.FromArgb(150, Color.Orange)), rect);
                using (new Pen(Color.White, 1f))
                {
                    cache.Graphics.DrawEllipse(Pens.White, rect);
                    cache.Graphics.DrawEllipse(Pens.White, Rectangle.Inflate(rect, -1, -1));
                }
                rect.Inflate(-2, -2);
                FontFamily fontFamily = AppearanceObject.DefaultFont.FontFamily;
                int lineSpacing = fontFamily.GetLineSpacing(FontStyle.Regular);
                int emHeight = fontFamily.GetEmHeight(FontStyle.Regular);
                float emSize = (rect.Height * 0.7f) / (((float) lineSpacing) / ((float) emHeight));
                using (Font font = new Font(fontFamily, emSize))
                {
                    using (StringFormat format = StringFormat.GenericTypographic.Clone() as StringFormat)
                    {
                        format.Alignment = StringAlignment.Center;
                        format.LineAlignment = StringAlignment.Center;
                        int num4 = this.Owner.Descriptions.IndexOf(description) + 1;
                        cache.Graphics.DrawString(num4.ToString(), font, Brushes.White, new RectangleF((float) rect.X, (float) rect.Y, (float) rect.Width, (float) rect.Height), format);
                    }
                }
                cache.Graphics.SmoothingMode = SmoothingMode.Default;
            }
        }

        private void DrawInActiveControl(GraphicsCache cache, GuideControlDescription description)
        {
            Rectangle controlBounds = description.ControlBounds;
            cache.Graphics.FillRectangle(cache.GetSolidBrush(Color.FromArgb(90, Color.Gray)), controlBounds);
            this.DrawControlMarker(cache, description);
        }

        private void DrawIntersectedInActiveControls(GraphicsCache cache, GuideControlDescription description)
        {
            foreach (GuideControlDescription description2 in this.Owner.Descriptions)
            {
                if (((description2 != description) && description2.IsValidNow) && (description2.ControlBounds.IntersectsWith(description.ControlBounds) && this.CalcInActiveInfoBounds(cache, description2).IntersectsWith(description.ControlBounds)))
                {
                    this.DrawControlMarker(cache, description2);
                }
            }
        }

        public GuideControlEx Owner
        {
            get
            {
                return (GuideControlEx) base.Owner;
            }
        }
    }
}

