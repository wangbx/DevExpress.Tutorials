namespace DevExpress.Description.Controls
{
    using DevExpress.Description.Controls.Windows;
    using DevExpress.Skins;
    using DevExpress.Utils.Controls;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    public class GuideControl
    {
        private List<GuideControlDescription> descriptions;
        private List<GuideControlDescription> descriptionTemplates;
        private Control root;
        private DXGuideLayeredWindow window;

        private void AddDescription(GuideControlDescription description, Control c)
        {
            if (c.Visible)
            {
                IGuideDescription gdc = c as IGuideDescription;
                if (gdc != null)
                {
                    description = this.LookupSubType(c, gdc, description);
                }
                GuideControlDescription item = description.Clone();
                item.AssociatedControl = c;
                item.ControlBounds = this.ConvertBounds(this.root.RectangleToClient(item.AssociatedControl.RectangleToScreen(item.AssociatedControl.ClientRectangle)));
                item.ScreenBounds = item.AssociatedControl.RectangleToScreen(item.AssociatedControl.ClientRectangle);
                item.ControlVisible = item.AssociatedControl.Visible;
                this.Descriptions.Add(item);
            }
        }

        private int CompareByRect(GuideControlDescription x, GuideControlDescription y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return 0;
            }
            Rectangle controlBounds = x.ControlBounds;
            Rectangle dest = y.ControlBounds;
            Point location = RectangleHelper.GetCenterBounds(controlBounds, new Size(1, 1)).Location;
            Point point2 = RectangleHelper.GetCenterBounds(dest, new Size(1, 1)).Location;
            if (!controlBounds.Contains(dest))
            {
                if (dest.Contains(controlBounds))
                {
                    return -1;
                }
                dest.Offset(-controlBounds.X, -controlBounds.Y);
                controlBounds.Offset(-controlBounds.X, -controlBounds.Y);
                controlBounds.IntersectsWith(dest);
                int num = this.CompareHeight(controlBounds, dest);
                if ((controlBounds.Right < dest.X) || (controlBounds.Right == dest.X))
                {
                    if (num != 0)
                    {
                        return -1;
                    }
                    if (controlBounds.Y < dest.Y)
                    {
                        return -1;
                    }
                    return 1;
                }
                if ((controlBounds.X < dest.X) || (controlBounds.X == dest.X))
                {
                    if (num != 0)
                    {
                        return -1;
                    }
                    if (controlBounds.Y < dest.Y)
                    {
                        return -1;
                    }
                    return 1;
                }
                if (num != 0)
                {
                    return num;
                }
                if (controlBounds.Y < dest.Y)
                {
                    return -1;
                }
            }
            return 1;
        }

        private int CompareHeight(Rectangle b1, Rectangle b2)
        {
            if ((Math.Abs((int) (b1.Y - b2.Y)) >= 0x19) || (Math.Abs((int) (b1.Height - b2.Height)) <= 50))
            {
                return 0;
            }
            if (b1.Height < b2.Height)
            {
                return -1;
            }
            return 1;
        }

        protected Rectangle ConvertBounds(Rectangle rectangle)
        {
            if (!this.UseClientRectangle)
            {
                rectangle.Location = this.ConvertPoint(rectangle.Location);
            }
            return rectangle;
        }

        protected Point ConvertPoint(Point point)
        {
            if (!this.UseClientRectangle)
            {
                Point point2 = this.root.PointToClient(this.Root.Location);
                point.X -= point2.X;
                point.Y -= point2.Y;
            }
            return point;
        }

        protected virtual DXGuideLayeredWindow CreateWindow()
        {
            return new DXGuideLayeredWindow(this);
        }

        public virtual void Hide()
        {
            if (this.IsVisible)
            {
                this.window.Hide();
                this.Root.LocationChanged -= new EventHandler(this.OnRootLocationChanged);
                this.Root.SizeChanged -= new EventHandler(this.OnRootLocationChanged);
                this.Root.Move -= new EventHandler(this.OnRootLocationChanged);
                this.OnHide();
            }
        }

        public virtual void Init(List<GuideControlDescription> descriptionTemplates, Control root)
        {
            this.descriptionTemplates = descriptionTemplates;
            this.root = root;
            this.InitControls();
        }

        private void InitControls()
        {
            foreach (Control control in this.ParseAllControls())
            {
                foreach (GuideControlDescription description in this.DescriptionTemplates)
                {
                    if (this.IsMatch(description, control))
                    {
                        this.AddDescription(description, control);
                        break;
                    }
                }
            }
            this.Descriptions.Sort(new Comparison<GuideControlDescription>(this.CompareByRect));
        }

        protected virtual bool IsMatch(GuideControlDescription description, Control c)
        {
            if ((description.ControlType != null) && !c.GetType().Equals(description.ControlType))
            {
                return false;
            }
            if (description.ControlTypeName != null)
            {
                return (c.GetType().FullName == description.ControlTypeName);
            }
            if (!string.IsNullOrEmpty(description.ControlName))
            {
                return (c.Name == description.ControlName);
            }
            return true;
        }

        protected GuideControlDescription Lookup(List<GuideControlDescription> collection, Control c)
        {
            foreach (GuideControlDescription description in collection)
            {
                if (this.IsMatch(description, c))
                {
                    return description;
                }
            }
            return null;
        }

        private GuideControlDescription LookupSubType(Control c, IGuideDescription gdc, GuideControlDescription description)
        {
            string typeName = (description.ControlTypeName == null) ? c.GetType().FullName : description.ControlTypeName;
            if ((gdc != null) && !string.IsNullOrEmpty(gdc.SubType))
            {
                GuideControlDescription description2 = this.DescriptionTemplates.FirstOrDefault<GuideControlDescription>(q => q.ControlTypeName == (typeName + ":" + gdc.SubType));
                if (description2 != null)
                {
                    return description2;
                }
            }
            return description;
        }

        protected virtual void OnHide()
        {
        }

        private void OnRootLocationChanged(object sender, EventArgs e)
        {
            this.Hide();
        }

        protected virtual void OnShowing()
        {
        }

        private List<Control> ParseAllControls()
        {
            List<Control> res = new List<Control>();
            this.ParseAllControls(res, this.Root);
            return res;
        }

        private void ParseAllControls(List<Control> res, Control parent)
        {
            res.Add(parent);
            GuideControlDescription description = this.Lookup(this.DescriptionTemplates, parent);
            if (((description == null) || description.AllowParseChildren) && parent.HasChildren)
            {
                foreach (Control control in parent.Controls)
                {
                    this.ParseAllControls(res, control);
                }
            }
        }

        public void Show()
        {
            if (this.HasValidDescriptions && ((!this.IsVisible && (this.Root != null)) && this.Root.Visible))
            {
                this.OnShowing();
                this.window = this.CreateWindow();
                Rectangle rectangle = this.root.RectangleToScreen(this.root.ClientRectangle);
                if (!this.UseClientRectangle)
                {
                    rectangle = this.root.RectangleToScreen(this.root.RectangleToClient(this.root.Bounds));
                }
                this.window.Bounds = rectangle;
                this.window.Create(this.Root);
                this.window.Show();
                this.Root.LocationChanged += new EventHandler(this.OnRootLocationChanged);
                this.Root.SizeChanged += new EventHandler(this.OnRootLocationChanged);
                this.Root.Move += new EventHandler(this.OnRootLocationChanged);
            }
        }

        public List<GuideControlDescription> Descriptions
        {
            get
            {
                if (this.descriptions == null)
                {
                    this.descriptions = new List<GuideControlDescription>();
                }
                return this.descriptions;
            }
        }

        public List<GuideControlDescription> DescriptionTemplates
        {
            get
            {
                if (this.descriptionTemplates == null)
                {
                    this.descriptionTemplates = new List<GuideControlDescription>();
                }
                return this.descriptionTemplates;
            }
        }

        public bool HasValidDescriptions
        {
            get
            {
                return (this.Descriptions.Count<GuideControlDescription>(q => ((q.AssociatedControl != null) && q.ControlVisible)) > 0);
            }
        }

        public virtual bool IsVisible
        {
            get
            {
                return ((this.window != null) && this.window.Visible);
            }
        }

        public Control Root
        {
            get
            {
                return this.root;
            }
        }

        protected virtual bool UseClientRectangle
        {
            get
            {
                return !(this.Root is Form);
            }
        }

        protected internal DXGuideLayeredWindow Window
        {
            get
            {
                return this.window;
            }
        }
    }
}

