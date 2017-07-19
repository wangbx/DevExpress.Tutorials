namespace DevExpress.Tutorials
{
    using System;
    using System.Xml;

    public class WhatsThisInfoReader : SimpleXmlReaderBase
    {
        private string code;
        private WhatsThisController controller;
        private string controlName;
        private string currentNodeName;
        private string description;
        private string dtImage;
        private string hintText;
        private string memberlist;
        private const string NodeControl = "controlentry";
        private const string NodeDescription = "description";
        private const string NodeDTImage = "dtimage";
        private const string NodeHint = "hint";
        private const string NodeHintText = "hinttext";
        private const string NodeMemberList = "memberlist";
        private const string NodeName = "name";
        private const string NodeNewLine = "newline";
        private const string NodeWindowCaption = "windowcaption";
        private string windowCaption;

        public WhatsThisInfoReader(WhatsThisController controller)
        {
            this.controller = controller;
            this.currentNodeName = string.Empty;
            this.ResetData();
        }

        protected override void ProcessEndElement(XmlNodeReader reader)
        {
            if (reader.Name == "controlentry")
            {
                WhatsThisParams popupInfo = new WhatsThisParams(this.windowCaption, this.description, this.memberlist, this.code, this.dtImage);
                this.controller.Collection.Add(new WhatsThisControlEntry(this.controlName, popupInfo));
                this.ResetData();
            }
            if (reader.Name == "newline")
            {
                this.description = this.description + Environment.NewLine;
                this.hintText = this.hintText + Environment.NewLine;
            }
            if (reader.Name == "hint")
            {
                this.controller.Hints.Add(this.controlName, this.hintText);
                this.ResetData();
            }
        }

        protected override void ProcessStartElement(XmlNodeReader reader)
        {
            if (reader.Name != "newline")
            {
                this.currentNodeName = reader.Name;
            }
        }

        protected override void ProcessText(XmlNodeReader reader)
        {
            string str = base.RemoveLineBreaks(reader.Value);
            string currentNodeName = this.currentNodeName;
            if (currentNodeName != null)
            {
                if (currentNodeName != "windowcaption")
                {
                    if (currentNodeName != "description")
                    {
                        if (currentNodeName == "memberlist")
                        {
                            this.memberlist = str;
                            return;
                        }
                        if (currentNodeName == "dtimage")
                        {
                            this.dtImage = str;
                            return;
                        }
                        if (currentNodeName == "name")
                        {
                            this.controlName = str;
                            return;
                        }
                        if (currentNodeName == "hinttext")
                        {
                            this.hintText = this.hintText + str;
                        }
                        return;
                    }
                }
                else
                {
                    this.windowCaption = str;
                    return;
                }
                this.description = this.description + str;
            }
        }

        private void ResetData()
        {
            this.controlName = this.windowCaption = this.description = this.code = this.memberlist = this.dtImage = string.Empty;
            this.hintText = string.Empty;
        }
    }
}

