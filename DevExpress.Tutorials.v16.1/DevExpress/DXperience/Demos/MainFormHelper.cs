namespace DevExpress.DXperience.Demos
{
    using DevExpress.LookAndFeel;
    using DevExpress.Tutorials.Properties;
    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Navigation;
    using DevExpress.XtraEditors;
    using DevExpress.XtraNavBar;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MainFormHelper
    {
        public static void CompactCurrentProcessWorkingSet()
        {
            try
            {
                Process currentProcess = Process.GetCurrentProcess();
                if ((currentProcess != null) && !currentProcess.HasExited)
                {
                    SetProcessWorkingSetSize(currentProcess.Handle, -1, -1);
                }
            }
            catch (Win32Exception)
            {
            }
        }

        public static string GetFileName(string extension, string filter)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = filter;
                dialog.FileName = Application.ProductName;
                dialog.DefaultExt = extension;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.FileName;
                }
            }
            return string.Empty;
        }

        public static Image GetImage(string name, ImageSize size)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            string fileName = string.Format("DevExpress.Tutorials.Images.{0}_{1}.png", name, GetImageSizeString(size));
            return ResourceImageHelper.CreateImageFromResources(fileName, typeof(RibbonMainForm).Assembly);
        }

        private static string GetImageSizeString(ImageSize size)
        {
            if (size == ImageSize.Small16)
            {
                return "16x16";
            }
            return "32x32";
        }

        public static void OpenExportedFile(string fileName)
        {
            if (XtraMessageBox.Show(Resources.OpenFileQuestion, Resources.ExportCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Process process = new Process();
                try
                {
                    process.StartInfo.FileName = fileName;
                    process.Start();
                }
                catch
                {
                }
            }
        }

        public static void RegisterDefaultBonusSkin()
        {
            UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
        }

        public static void RegisterRibbonDefaultBonusSkin()
        {
            UserLookAndFeel.Default.SetSkinStyle("Office 2016 Colorful");
        }

        public static void SelectAccordionControlItem(AccordionControl nbControl, object startModule)
        {
            foreach (AccordionControlElement element in nbControl.Elements)
            {
                foreach (AccordionControlElement element2 in element.Elements)
                {
                    DevExpress.DXperience.Demos.ModuleInfo tag = element2.Tag as DevExpress.DXperience.Demos.ModuleInfo;
                    if ((tag != null) && (startModule.Equals(tag.TypeName) || startModule.Equals(tag.FullTypeName)))
                    {
                        nbControl.SelectElement(element2);
                        element2.Visible = true;
                        if (!element.Expanded)
                        {
                            element.Expanded = true;
                        }
                        break;
                    }
                }
            }
        }

        public static void SelectNavBarItem(NavBarControl nbControl, object startModule)
        {
            foreach (NavBarGroup group in nbControl.Groups)
            {
                foreach (NavBarItemLink link in group.ItemLinks)
                {
                    DevExpress.DXperience.Demos.ModuleInfo tag = link.Item.Tag as DevExpress.DXperience.Demos.ModuleInfo;
                    if ((tag != null) && (startModule.Equals(tag.TypeName) || startModule.Equals(tag.FullTypeName)))
                    {
                        nbControl.SelectedLink = link;
                        nbControl.GetViewInfo().MakeLinkVisible(link);
                        break;
                    }
                }
            }
        }

        public static void SetBarButtonImage(BarItem item, string name)
        {
            item.LargeGlyph = GetImage(name, ImageSize.Large32);
            item.Glyph = GetImage(name, ImageSize.Small16);
        }

        public static void SetCommandLineArgs(string[] args, out string startModule, out bool fullWindow)
        {
            startModule = string.Empty;
            fullWindow = false;
            foreach (string str in args)
            {
                if (str.IndexOf(DemoHelper.StartModuleParameter) == 0)
                {
                    startModule = DemoHelper.GetModuleName(str);
                }
                if (str.Equals(DemoHelper.FullWindowModeParameter))
                {
                    fullWindow = true;
                }
            }
        }

        public static void SetFormClientSize(Rectangle workingArea, Form form, int width, int height)
        {
            Size scaleSize = ScaleUtils.GetScaleSize(new Size(width, height));
            width = scaleSize.Width;
            height = scaleSize.Height;
            form.ClientSize = new Size(Math.Min(workingArea.Width, width), Math.Min(workingArea.Height, height));
            form.Location = new Point(workingArea.X + ((workingArea.Width - form.Width) / 2), workingArea.Y + ((workingArea.Height - form.Height) / 2));
        }

        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr handle, int minimumWorkingSetSize, int maximumWorkingSetSize);
        public static void ShowExportErrorMessage()
        {
            XtraMessageBox.Show(Resources.ExportError, Resources.ExportCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        public static void ShowExportErrorMessage(Exception e)
        {
            XtraMessageBox.Show(e.Message, Resources.ExportCaption, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}

