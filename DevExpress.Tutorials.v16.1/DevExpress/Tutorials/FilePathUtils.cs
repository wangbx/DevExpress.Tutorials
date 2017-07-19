namespace DevExpress.Tutorials
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public class FilePathUtils
    {
        public static string FindFilePath(string path, bool showError)
        {
            string str = @"\";
            for (int i = 0; i <= 10; i++)
            {
                if (File.Exists(Application.StartupPath + str + path))
                {
                    return (Application.StartupPath + str + path);
                }
                str = str + @"..\";
            }
            if (showError)
            {
                MessageBox.Show("File " + path + " is not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            return string.Empty;
        }
    }
}

