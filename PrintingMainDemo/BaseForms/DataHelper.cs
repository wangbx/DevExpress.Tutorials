using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraPrinting.Control;
using System.Data;
using System.Collections.Generic;

namespace XtraPrintingDemos {
    public static class Helper {
		public static string demoPath = Directory.GetCurrentDirectory();

        static string GetRelativePathCore(string name) {
            return DevExpress.Utils.FilesHelper.FindingFileName(Application.StartupPath, "Data\\" + name);
        }
        public static string GetRelativePath(string path) {
            if(Assembly.GetEntryAssembly() != Assembly.GetExecutingAssembly())
                return Path.Combine(demoPath, path);
            int index = path.LastIndexOf(@"\") + 1;
            return GetRelativePathCore(path.Substring(index));
        }
    }
}
