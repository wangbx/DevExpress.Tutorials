namespace DevExpress.DXperience.Demos
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class WebControlBase : TutorialControlBase
    {
        private IContainer components;
        private IntPtr m_hMod;
        private IntPtr m_hMod2;
        private WebBrowser webBrowser;

        public WebControlBase()
        {
            this.SetWebBrowserXPTheme();
            this.webBrowser = new WebBrowser();
            this.webBrowser.Dock = DockStyle.Fill;
            base.Controls.Add(this.webBrowser);
            this.webBrowser.ContainingControl = this;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                WebHelper.FreeLibrary(this.m_hMod);
                WebHelper.FreeLibrary(this.m_hMod2);
                if (this.components != null)
                {
                    this.components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected void Navigate(string url)
        {
            this.webBrowser.Navigate(url);
        }

        private void SetWebBrowserXPTheme()
        {
            if ((this.m_hMod == IntPtr.Zero) || (this.m_hMod2 == IntPtr.Zero))
            {
                WebHelper.INITCOMMONCONTROLSEX icc = new WebHelper.INITCOMMONCONTROLSEX {
                    dwICC = 0x200
                };
                WebHelper.InitCommonControlsEx(icc);
                this.m_hMod = WebHelper.LoadLibrary("shell32.dll");
                this.m_hMod2 = WebHelper.LoadLibrary("explorer.exe");
            }
        }

        [ToolboxItem(false)]
        private class WebBrowser : AxHost
        {
            private object ocx;

            public WebBrowser() : base("8856f961-340a-11d0-a96b-00c04fd705a2")
            {
            }

            protected override void AttachInterfaces()
            {
                try
                {
                    this.ocx = base.GetOcx();
                }
                catch
                {
                }
            }

            public void Navigate(string url)
            {
                if (this.ocx != null)
                {
                    object obj2 = null;
                    this.ocx.GetType().InvokeMember("Navigate2", BindingFlags.InvokeMethod, null, this.ocx, new object[] { url, obj2, obj2, obj2, obj2 });
                }
            }
        }
    }
}

