namespace DevExpress.Tutorials
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;

    public class SimpleXmlReaderBase
    {
        private XmlNodeReader GetXmlNodeReader(string xmlFileName, System.Type type)
        {
            if (xmlFileName == null)
            {
                return null;
            }
            XmlDocument node = new XmlDocument();
            try
            {
                if (type == null)
                {
                    string path = FilePathUtils.FindFilePath(xmlFileName, false);
                    if (path == string.Empty)
                    {
                        Assembly.GetExecutingAssembly().GetManifestResourceStream(xmlFileName);
                        return null;
                    }
                    using (File.OpenRead(path))
                    {
                        node.Load(path);
                        goto Label_00A0;
                    }
                }
                using (Stream stream2 = type.Assembly.GetManifestResourceStream(xmlFileName))
                {
                    node.Load(stream2);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message + ", " + exception.GetType().ToString());
                return null;
            }
        Label_00A0:
            return new XmlNodeReader(node);
        }

        protected virtual void ProcessEndElement(XmlNodeReader reader)
        {
        }

        protected virtual void ProcessStartElement(XmlNodeReader reader)
        {
        }

        protected virtual void ProcessText(XmlNodeReader reader)
        {
        }

        public bool ProcessXml(string xmlFileName, System.Type type)
        {
            XmlNodeReader xmlNodeReader = this.GetXmlNodeReader(xmlFileName, type);
            if (xmlNodeReader != null)
            {
                while (xmlNodeReader.Read())
                {
                    switch (xmlNodeReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            this.ProcessStartElement(xmlNodeReader);
                            break;

                        case XmlNodeType.Text:
                            this.ProcessText(xmlNodeReader);
                            break;

                        case XmlNodeType.EndElement:
                            this.ProcessEndElement(xmlNodeReader);
                            break;
                    }
                }
                xmlNodeReader.Close();
                return true;
            }
            return false;
        }

        protected string RemoveLineBreaks(string s)
        {
            return s.Replace("\n", string.Empty).Replace("\r\n", string.Empty).Replace(Environment.NewLine, string.Empty);
        }
    }
}

