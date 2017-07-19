namespace DevExpress.Tutorials
{
    using System;
    using System.IO;
    using System.Text;

    public class WhatsThisSourceFileReader
    {
        private string commentString;
        private WhatsThisController controller;
        private ControlCodes listenerControlCodes;
        private string[] separatedCodeFileNames;
        private bool skipLine;
        private static string SkipTag = "skip";

        public WhatsThisSourceFileReader(WhatsThisController controller, string codeFileNames, string commentString)
        {
            this.controller = controller;
            if (codeFileNames != null)
            {
                this.separatedCodeFileNames = codeFileNames.Split(new char[] { ';' }, 10);
            }
            else
            {
                this.separatedCodeFileNames = new string[] { string.Empty };
            }
            this.commentString = commentString;
            this.listenerControlCodes = new ControlCodes();
            this.skipLine = false;
            controller.Collection.ResetControlCodes();
        }

        private void AddCodeLine(ControlCodeEntry entry, string currentString)
        {
            entry.Code = entry.Code + currentString + "\r\n";
        }

        private void CheckSkipLine(string s)
        {
            if (s.IndexOf(this.GetSkipTag(true)) != -1)
            {
                this.skipLine = true;
            }
            if (s.IndexOf(this.GetSkipTag(false)) != -1)
            {
                this.skipLine = false;
            }
        }

        private string GetSkipTag(bool opening)
        {
            string str = "</";
            if (opening)
            {
                str = "<";
            }
            return (str + SkipTag + ">");
        }

        private void ProcessCommentedString(string s)
        {
            this.CheckSkipLine(s);
            foreach (WhatsThisControlEntry entry in this.controller.Collection)
            {
                if (s.IndexOf(entry.OpeningTag) != -1)
                {
                    this.listenerControlCodes.AddListener(entry.ControlName);
                }
                if (s.IndexOf(entry.ClosingTag) != -1)
                {
                    WhatsThisParams popupInfo = this.controller.Collection[entry.ControlName].PopupInfo;
                    if (popupInfo.Code != string.Empty)
                    {
                        popupInfo.Code = popupInfo.Code + this.commentString + "..\r\n";
                    }
                    popupInfo.Code = popupInfo.Code + CodeLineShifter.ShiftLeftToFit(this.listenerControlCodes.GetCodeByControlName(entry.ControlName));
                    this.listenerControlCodes.RemoveListener(entry.ControlName);
                }
            }
        }

        private void ProcessFile(string codeFileName)
        {
            if (codeFileName != string.Empty)
            {
                string path = FilePathUtils.FindFilePath(codeFileName, false);
                if (File.Exists(path))
                {
                    FileStream stream = File.OpenRead(path);
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    StringReader reader = new StringReader(Encoding.ASCII.GetString(buffer));
                    this.ProcessFileString(reader);
                    stream.Close();
                }
            }
        }

        public void ProcessFiles()
        {
            foreach (string str in this.separatedCodeFileNames)
            {
                this.ProcessFile(str);
            }
        }

        private void ProcessFileString(StringReader reader)
        {
            string s = string.Empty;
            while (s != null)
            {
                s = reader.ReadLine();
                if (this.StringCommented(s))
                {
                    this.ProcessCommentedString(s);
                }
                else if (!this.skipLine)
                {
                    foreach (ControlCodeEntry entry in this.listenerControlCodes)
                    {
                        this.AddCodeLine(entry, s);
                    }
                    continue;
                }
            }
        }

        private bool StringCommented(string s)
        {
            if (s == null)
            {
                return false;
            }
            string str = s.TrimStart(null);
            return ((str.StartsWith(this.commentString) && (str.Length > 1)) && (str[1] != '~'));
        }
    }
}

