namespace DevExpress.Tutorials
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class FormTutorialInfo
    {
        private string aboutFile;
        private Image imagePatternFill;
        private Image imageWhatsThisButton;
        private Image imageWhatsThisButtonStop;
        private string sourceFileComment;
        private DevExpress.Tutorials.SourceFileType sourceFileType;

        [Browsable(true)]
        public string AboutFile
        {
            get
            {
                return this.aboutFile;
            }
            set
            {
                this.aboutFile = value;
            }
        }

        [Browsable(true)]
        public Image ImagePatternFill
        {
            get
            {
                return this.imagePatternFill;
            }
            set
            {
                this.imagePatternFill = value;
            }
        }

        [Browsable(true)]
        public Image ImageWhatsThisButton
        {
            get
            {
                return this.imageWhatsThisButton;
            }
            set
            {
                this.imageWhatsThisButton = value;
            }
        }

        [Browsable(true)]
        public Image ImageWhatsThisButtonStop
        {
            get
            {
                return this.imageWhatsThisButtonStop;
            }
            set
            {
                this.imageWhatsThisButtonStop = value;
            }
        }

        [Browsable(true)]
        public string SourceFileComment
        {
            get
            {
                return this.sourceFileComment;
            }
            set
            {
                this.sourceFileComment = value;
            }
        }

        [Browsable(true)]
        public DevExpress.Tutorials.SourceFileType SourceFileType
        {
            get
            {
                return this.sourceFileType;
            }
            set
            {
                this.sourceFileType = value;
            }
        }
    }
}

