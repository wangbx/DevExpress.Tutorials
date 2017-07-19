namespace DevExpress.Tutorials
{
    using System;
    using System.IO;

    public class CodeLineShifter
    {
        private static int GetLeftWhiteSpaceCount(string s)
        {
            int length = s.Length;
            string str = s.TrimStart(null);
            return (length - str.Length);
        }

        private static bool IsIgnoreString(string s)
        {
            return ((s == string.Empty) || (s.StartsWith("using") || s.StartsWith("Imports")));
        }

        public static string ShiftLeftBy(string code, int offset)
        {
            string str;
            StringReader reader = new StringReader(code);
            string str2 = string.Empty;
            while ((str = reader.ReadLine()) != null)
            {
                if (str != string.Empty)
                {
                    str2 = str2 + str.Substring(offset);
                }
                str2 = str2 + "\r\n";
            }
            return str2;
        }

        public static string ShiftLeftToFit(string code)
        {
            string str;
            if (code == string.Empty)
            {
                return code;
            }
            StringReader reader = new StringReader(code);
            int leftWhiteSpaceCount = GetLeftWhiteSpaceCount(reader.ReadLine());
            while ((str = reader.ReadLine()) != null)
            {
                if (!IsIgnoreString(str) && (leftWhiteSpaceCount > GetLeftWhiteSpaceCount(str)))
                {
                    leftWhiteSpaceCount = GetLeftWhiteSpaceCount(str);
                }
            }
            return ShiftLeftBy(code, leftWhiteSpaceCount);
        }
    }
}

