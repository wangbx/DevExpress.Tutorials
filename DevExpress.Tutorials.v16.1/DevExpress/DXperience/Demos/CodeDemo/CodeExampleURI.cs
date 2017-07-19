namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;

    public class CodeExampleURI
    {
        private string descriptionCore;
        private string exampleName;
        private string uriCore;

        public CodeExampleURI(string uri, string description, string exampleName)
        {
            this.Uri = uri;
            this.Description = description;
            this.ExampleName = exampleName;
        }

        public string Description
        {
            get
            {
                return this.descriptionCore;
            }
            set
            {
                this.descriptionCore = value;
            }
        }

        public string ExampleName
        {
            get
            {
                return this.exampleName;
            }
            set
            {
                this.exampleName = value;
            }
        }

        public string Uri
        {
            get
            {
                return this.uriCore;
            }
            set
            {
                this.uriCore = value;
            }
        }
    }
}

