namespace DevExpress.Description.Controls
{
    using DevExpress.Utils.Text;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    public class GuideControlDescription : IStringImageProvider
    {
        public GuideControlDescription()
        {
            this.AllowParseChildren = true;
        }

        internal GuideControlDescription Clone()
        {
            return new GuideControlDescription { 
                ControlName = this.ControlName,
                ControlTypeName = this.ControlTypeName,
                ControlType = this.ControlType,
                Description = this.Description,
                DescriptionPicture = this.DescriptionPicture,
                HighlightUseInsideBounds = this.HighlightUseInsideBounds,
                AllowParseChildren = this.AllowParseChildren
            };
        }

        Image IStringImageProvider.GetImage(string id)
        {
            if (this.DescriptionPicture != null)
            {
                return this.DescriptionPicture;
            }
            return null;
        }

        internal string GetTypeName()
        {
            if (!string.IsNullOrEmpty(this.ControlTypeName))
            {
                return this.ControlTypeName;
            }
            if (this.AssociatedControl != null)
            {
                return this.AssociatedControl.GetType().FullName;
            }
            if (this.ControlType != null)
            {
                return this.ControlType.FullName;
            }
            return "";
        }

        public override string ToString()
        {
            string typeName = this.GetTypeName();
            string str2 = ((this.AssociatedControl != null) && !string.IsNullOrEmpty(this.AssociatedControl.Name)) ? this.AssociatedControl.Name : this.ControlName;
            if (!string.IsNullOrEmpty(str2))
            {
                typeName = typeName + string.Format(" [{0}]", str2);
            }
            return typeName;
        }

        [DefaultValue(true)]
        public bool AllowParseChildren { get; set; }

        [XmlIgnore, Browsable(false)]
        public Control AssociatedControl { get; set; }

        [XmlIgnore]
        internal Rectangle ControlBounds { get; set; }

        [DefaultValue((string) null)]
        public string ControlName { get; set; }

        [Browsable(false), XmlIgnore]
        public System.Type ControlType { get; set; }

        [DefaultValue((string) null)]
        public string ControlTypeName { get; set; }

        [XmlIgnore]
        internal bool ControlVisible { get; set; }

        public string Description { get; set; }

        [DefaultValue((string) null), XmlIgnore]
        public Image DescriptionPicture { get; set; }

        [XmlElement("DescriptionPicture"), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public byte[] DescriptionPictureSerialized
        {
            get
            {
                if (this.DescriptionPicture == null)
                {
                    return null;
                }
                using (MemoryStream stream = new MemoryStream())
                {
                    this.DescriptionPicture.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
            set
            {
                if (value == null)
                {
                    this.DescriptionPicture = null;
                }
                else
                {
                    using (MemoryStream stream = new MemoryStream(value))
                    {
                        this.DescriptionPicture = new Bitmap(stream);
                    }
                }
            }
        }

        [DefaultValue(false)]
        public bool HighlightUseInsideBounds { get; set; }

        [Browsable(false), XmlIgnore]
        public virtual bool IsValidNow
        {
            get
            {
                return ((this.AssociatedControl != null) && this.ControlVisible);
            }
        }

        [XmlIgnore]
        internal Rectangle ScreenBounds { get; set; }
    }
}

