namespace DevExpress.DXperience.Demos.CodeDemo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class CodeExample
    {
        public CodeExample()
        {
            this.VersionID = -1;
        }

        public string RegionName()
        {
            return this.ToPascalCase(this.Name, true);
        }

        internal void Reset()
        {
            this.UserCodeCS = this.CodeCS;
            this.UserCodeVB = this.CodeVB;
            if (((this.Parent != null) && this.Parent.HasNestedClassStrings) && ((this.NestedTypes != null) && (this.NestedTypes.Length != 0)))
            {
                foreach (KeyValuePair<Type, NestedCodeContainer> pair in this.Parent.NestedClassStrings)
                {
                    if (this.NestedTypes.Contains<Type>(pair.Key))
                    {
                        pair.Value.UserCode = pair.Value.Code;
                    }
                }
            }
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

        public string UserControlName(bool firstCharToLower)
        {
            return (this.ToPascalCase(this.Name, firstCharToLower) + "UserControl");
        }

        public string BeginCSCode { get; set; }

        public string BeginVBCode { get; set; }

        public string CodeCS { get; set; }

        public string CodeVB { get; set; }

        public string EndCSCode { get; set; }

        public string EndVBCode { get; set; }

        public string GroupName { get; set; }

        public List<string> HighlightTokens { get; set; }

        public System.Reflection.MethodInfo MethodInfo { get; set; }

        public string Name { get; set; }

        public Type[] NestedTypes { get; set; }

        public CodeExampleGroup Parent { get; set; }

        public System.Reflection.MethodInfo TearDown { get; set; }

        public List<string> UnderlineTokens { get; set; }

        public string UserCodeCS { get; set; }

        public string UserCodeVB { get; set; }

        public int VersionID { get; set; }
    }
}

