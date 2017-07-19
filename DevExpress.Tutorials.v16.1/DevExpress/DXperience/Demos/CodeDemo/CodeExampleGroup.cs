namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class CodeExampleGroup
    {
        public CodeExampleGroup()
        {
            this.VersionID = -1;
        }

        public string NameSpace()
        {
            return this.ToPascalCase(this.Name, false);
        }

        private string ToPascalCase(string input, bool firstCharToLower = false)
        {
            input = new Regex("[^a-zA-Z0-9]").Replace(input, " ");
            string[] strArray = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string str = string.Empty;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (firstCharToLower && (i == 0))
                {
                    str = str + strArray[i].Substring(0, 1).ToLower() + strArray[i].Substring(1);
                }
                else
                {
                    str = str + strArray[i].Substring(0, 1).ToUpper() + strArray[i].Substring(1);
                }
            }
            return str;
        }

        public List<CodeExample> Examples { get; set; }

        public string FileName { get; set; }

        public bool HasNestedClassStrings
        {
            get
            {
                return ((this.NestedClassStrings != null) && (this.NestedClassStrings.Count > 0));
            }
        }

        public List<string> HighlightTokens { get; set; }

        public string Name { get; set; }

        public Dictionary<Type, NestedCodeContainer> NestedClassStrings { get; set; }

        public List<Type> NestedTypes { get; set; }

        public Type RootType { get; set; }

        public MethodInfo SetUp { get; set; }

        public string SetUpCode { get; set; }

        public MethodInfo TearDown { get; set; }

        public string TearDownCode { get; set; }

        public List<string> UnderlineTokens { get; set; }

        public int VersionID { get; set; }
    }
}

