namespace DevExpress.DXperience.Demos
{
    using DevExpress.Utils;
    using DevExpress.Utils.About;
    using System;
    using System.Windows.Forms;

    public class SearchControlLogger : ISearchControlClient
    {
        private object owner;
        private ISearchControlClient source;
        private Timer timer;
        private string timerText;

        public SearchControlLogger(object owner, ISearchControlClient source)
        {
            this.source = source;
            this.owner = owner;
        }

        void ISearchControlClient.ApplyFindFilter(SearchInfoBase searchInfo)
        {
            this.Log(searchInfo);
            this.source.ApplyFindFilter(searchInfo);
        }

        SearchControlProviderBase ISearchControlClient.CreateSearchProvider()
        {
            return this.source.CreateSearchProvider();
        }

        void ISearchControlClient.SetSearchControl(ISearchControl searchControl)
        {
            this.source.SetSearchControl(searchControl);
        }

        private void Log(SearchInfoBase searchInfo)
        {
            if (!string.IsNullOrWhiteSpace(searchInfo.SearchText))
            {
                this.Log(searchInfo.SearchText);
            }
        }

        private void Log(string text)
        {
            if (this.timer != null)
            {
                this.timer.Stop();
                this.timer.Dispose();
                this.timer = null;
                if (text.ToLower().StartsWith(this.timerText.ToLower()))
                {
                    this.StartTimer(text);
                    return;
                }
                this.LogCore(this.timerText);
            }
            this.StartTimer(text);
        }

        private void LogCore(string text)
        {
            UAlgo.Default.DoEvent(4, 1, this.owner.GetType().FullName + ", DemoSearch: " + text);
        }

        private void StartTimer(string text)
        {
            Timer timer = new Timer {
                Interval = 0x7d0
            };
            this.timer = timer;
            this.timerText = text;
            this.timer.Tick += delegate (object s, EventArgs e) {
                if (this.timer != null)
                {
                    this.timer.Dispose();
                    this.timer = null;
                    this.LogCore(text);
                }
            };
            this.timer.Start();
        }

        bool ISearchControlClient.IsAttachedToSearchControl
        {
            get
            {
                return this.source.IsAttachedToSearchControl;
            }
        }
    }
}

