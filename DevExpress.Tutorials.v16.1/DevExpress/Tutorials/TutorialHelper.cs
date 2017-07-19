namespace DevExpress.Tutorials
{
    using DevExpress.XtraEditors;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Text;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    public class TutorialHelper
    {
        private const string endDescription = "[end description]";

        public static string[] GetDescriptionsFromResource(string name, Assembly asm)
        {
            StreamReader reader = new StreamReader(asm.GetManifestResourceStream(name));
            ArrayList list = new ArrayList();
            string str = "";
            while (!reader.EndOfStream)
            {
                string str2 = reader.ReadLine();
                if ((str != "") && (str2 != "[end description]"))
                {
                    str = str + "\r\n";
                }
                if (str2 != "[end description]")
                {
                    str = str + str2;
                }
                else
                {
                    list.Add(str);
                    str = "";
                }
            }
            reader.Close();
            return (string[]) list.ToArray(typeof(string));
        }

        public static void InitFont(ImageListBoxControl ilb)
        {
            int width = 20;
            int height = 0x10;
            int x = 1;
            ImageList list = new ImageList {
                ImageSize = new Size(width, height)
            };
            Rectangle rect = new Rectangle(x, x, width - (x * 2), height - (x * 2));
            ilb.BeginUpdate();
            try
            {
                ilb.Items.Clear();
                ilb.ImageList = list;
                int num4 = 0;
                for (int i = 0; i < FontFamily.Families.Length; i++)
                {
                    try
                    {
                        Font font = new Font(FontFamily.Families[i].Name, 8f);
                        string name = FontFamily.Families[i].Name;
                        Image image = new Bitmap(width, height);
                        using (Graphics graphics = Graphics.FromImage(image))
                        {
                            graphics.FillRectangle(Brushes.White, rect);
                            graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                            graphics.DrawString("abc", font, Brushes.Black, (float) x, (float) x);
                            graphics.DrawRectangle(Pens.Black, rect);
                        }
                        list.Images.Add(image);
                        ilb.Items.Add(name, num4++);
                    }
                    catch
                    {
                    }
                }
            }
            finally
            {
                ilb.CancelUpdate();
            }
        }
    }
}

