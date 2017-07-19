namespace DevExpress.Tutorials
{
    using System;

    public class WhatsThisParams
    {
        private string fCaption;
        private string fCode;
        private string fDescription;
        private string fDTImage;
        private string fMemberList;

        public WhatsThisParams(string caption, string description, string memberList, string code, string dtImage)
        {
            this.fCode = code;
            this.fCaption = caption;
            this.fDescription = description;
            this.fMemberList = memberList;
            this.fDTImage = dtImage;
        }

        public string Caption
        {
            get
            {
                return this.fCaption;
            }
        }

        public string Code
        {
            get
            {
                return this.fCode;
            }
            set
            {
                this.fCode = value;
            }
        }

        public string Description
        {
            get
            {
                return this.fDescription;
            }
        }

        public string DTImage
        {
            get
            {
                return this.fDTImage;
            }
        }

        public string MemberList
        {
            get
            {
                return this.fMemberList;
            }
        }
    }
}

