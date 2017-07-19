using System;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;

namespace DevExpress.DXperience.Demos
{
    public class WebHelper
    {
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public class INITCOMMONCONTROLSEX
        {
            public int dwSize = 8;

            public int dwICC;
        }

        public const int ICC_USEREX_CLASSES = 512;

        public static string demoPath = Directory.GetCurrentDirectory();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

        [DllImport("comctl32.dll")]
        public static extern bool InitCommonControlsEx(WebHelper.INITCOMMONCONTROLSEX icc);

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibrary(string libname);

        [DllImport("kernel32.dll")]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("uxtheme.dll")]
        public static extern void SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        public static void SetConnectionString(OleDbConnection oleDbConnection, string path)
        {
            oleDbConnection.ConnectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;User ID=Admin;Data Source={0};Mode=Share Deny None;Extended Properties=\"\";Jet OLEDB:System database=\"\";Jet OLEDB:Registry Path=\"\";Jet OLEDB:Database Password=\"\";Jet OLEDB:Engine Type=5;Jet OLEDB:Database Locking Mode=1;Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Global Bulk Transactions=1;Jet OLEDB:New Database Password=\"\";Jet OLEDB:Create System Database=False;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;Jet OLEDB:SFP=False", path);
        }
    }
}