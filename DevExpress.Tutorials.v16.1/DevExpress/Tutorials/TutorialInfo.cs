namespace DevExpress.Tutorials
{
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class TutorialInfo
    {
        private string aboutFile;
        private string description;
        private string tutorialName;
        private string whatsThisCodeFile;
        private string whatsThisXMLFile;

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
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        [Browsable(true)]
        public string TutorialName
        {
            get
            {
                return this.tutorialName;
            }
            set
            {
                this.tutorialName = value;
            }
        }

        [Browsable(true)]
        public string WhatsThisCodeFile
        {
            get
            {
                return this.whatsThisCodeFile;
            }
            set
            {
                this.whatsThisCodeFile = value;
            }
        }

        [Browsable(true)]
        public string WhatsThisXMLFile
        {
            get
            {
                return this.whatsThisXMLFile;
            }
            set
            {
                this.whatsThisXMLFile = value;
            }
        }
    }
}

