namespace DevExpress.DXperience.Demos
{
    using DevExpress.Utils.Frames;
    using System;
    using System.Reflection;

    public class DemoHelper
    {
        public static string FullWindowModeParameter = "/fullwindow";
        public static string StartModuleParameter = "/start:";

        public static string GetLanguageString(Assembly asm)
        {
            return string.Format("{0}", FrameHelper.GetLanguage(asm));
        }

        internal static string GetModuleName(string name)
        {
            return name.Substring(StartModuleParameter.Length, name.Length - StartModuleParameter.Length);
        }

        public static string StringComposite(string str1, string str2)
        {
            string[] strArray = str1.Split(new char[] { ';' });
            string str = string.Empty;
            foreach (string str3 in strArray)
            {
                if (str != string.Empty)
                {
                    str = str + ";";
                }
                str = str + str2 + str3;
            }
            return str;
        }
    }
}

