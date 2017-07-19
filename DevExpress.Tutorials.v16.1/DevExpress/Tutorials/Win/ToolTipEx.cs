namespace DevExpress.Tutorials.Win
{
    using DevExpress.Utils;
    using Microsoft.Win32;
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class ToolTipEx : IDisposable
    {
        private object activeObject;
        private bool allowAutoPop;
        private int autoPopDelay;
        private System.Windows.Forms.Timer autoPopTimer;
        private static AppearanceObject defaultStyle;
        private int initialDelay;
        private System.Windows.Forms.Timer initialTimer;
        private int reshowDelay;
        private AppearanceObject style = null;
        private ToolTipWindow toolWindow = new ToolTipWindow();

        public event ToolTipCalcSizeEventHandler ToolTipCalcSize;

        public event ToolTipCanShowEventHandler ToolTipCanShow;

        public event ToolTipCustomDrawEventHandler ToolTipCustomDraw;

        static ToolTipEx()
        {
            CreateStyle();
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(ToolTipEx.OnUserPreferencesChanged);
        }

        public ToolTipEx()
        {
            this.ToolWindow.ToolTipCalcSize += new ToolTipCalcSizeEventHandler(this.OnToolTipCalcSize);
            this.ToolWindow.ToolTipCustomDraw += new ToolTipCustomDrawEventHandler(this.OnToolTipCustomDraw);
            this.activeObject = null;
            this.reshowDelay = 100;
            this.initialDelay = 500;
            this.autoPopDelay = 0x1388;
            this.allowAutoPop = true;
            this.initialTimer = new System.Windows.Forms.Timer();
            this.autoPopTimer = new System.Windows.Forms.Timer();
            this.AutoPopTimer.Tick += new EventHandler(this.OnAutoPopTimerTick);
            this.InitialTimer.Tick += new EventHandler(this.OnInitialTimerTick);
            this.AutoPopTimer.Interval = this.AutoPopDelay;
            this.InitialTimer.Interval = this.InitialDelay;
            this.UpdateToolStyle();
        }

        private static void CreateStyle()
        {
            defaultStyle = new AppearanceObject("ToolTip");
            defaultStyle.BackColor = SystemColors.Info;
            defaultStyle.ForeColor = SystemColors.InfoText;
        }

        public virtual void Dispose()
        {
            if (this.ToolWindow != null)
            {
                this.ToolWindow.Dispose();
                this.toolWindow = null;
            }
        }

        public virtual void HideHint()
        {
            this.activeObject = null;
            bool visible = this.ToolWindow.Visible;
            this.ToolWindow.HideTip();
            this.AutoPopTimer.Stop();
            this.InitialTimer.Stop();
        }

        public virtual void ObjectEnter(object obj)
        {
            this.ActiveObjectCore = obj;
        }

        public virtual void ObjectLeave(object newObject)
        {
            this.ActiveObjectCore = newObject;
        }

        protected virtual void OnAutoPopTimerTick(object sender, EventArgs e)
        {
            if (this.ToolWindow.Visible && this.AllowAutoPop)
            {
                this.ToolWindow.HideTip();
            }
        }

        protected virtual void OnInitialTimerTick(object sender, EventArgs e)
        {
            this.ShowHint();
        }

        protected virtual void OnToolTipCalcSize(object sender, ToolTipCalcSizeEventArgs e)
        {
            if (this.ToolTipCalcSize != null)
            {
                this.ToolTipCalcSize(this, e);
            }
        }

        protected virtual void OnToolTipCustomDraw(object sender, ToolTipCustomDrawEventArgs e)
        {
            if (this.ToolTipCustomDraw != null)
            {
                this.ToolTipCustomDraw(this, e);
            }
        }

        private static void OnUserPreferencesChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            CreateStyle();
        }

        public virtual void ShowHint()
        {
            ToolTipCanShowEventArgs e = new ToolTipCanShowEventArgs(false, "", Point.Empty);
            if (this.ToolTipCanShow != null)
            {
                this.ToolTipCanShow(this, e);
            }
            this.ShowHint(e);
        }

        public virtual void ShowHint(ToolTipCanShowEventArgs e)
        {
            this.InitialTimer.Stop();
            if (e.Show)
            {
                this.ToolWindow.ToolTipAlignment = e.WindowAlignment;
                this.ToolWindow.Font = this.ActiveStyle.Font;
                this.ToolWindow.BackColor = this.ActiveStyle.BackColor;
                this.ToolWindow.ForeColor = this.ActiveStyle.ForeColor;
                this.ToolWindow.ToolTip = e.Text;
                this.ToolWindow.ShowTip(e.Position, new Point(e.Position.X, e.Position.Y + 10));
                if (this.AllowAutoPop)
                {
                    this.AutoPopTimer.Start();
                }
            }
            else
            {
                this.HideHint();
            }
        }

        protected virtual void UpdateToolStyle()
        {
            this.ToolWindow.Font = this.ActiveStyle.Font;
            this.ToolWindow.BackColor = this.ActiveStyle.BackColor;
            this.ToolWindow.ForeColor = this.ActiveStyle.ForeColor;
        }

        public virtual object ActiveObject
        {
            get
            {
                return this.ActiveObjectCore;
            }
        }

        protected virtual object ActiveObjectCore
        {
            get
            {
                return this.activeObject;
            }
            set
            {
                if (this.ActiveObjectCore != value)
                {
                    object activeObjectCore = this.ActiveObjectCore;
                    this.activeObject = value;
                    if (this.activeObject != null)
                    {
                        this.InitialTimer.Interval = (activeObjectCore != null) ? this.ReshowDelay : this.InitialDelay;
                        this.InitialTimer.Start();
                    }
                    else
                    {
                        this.HideHint();
                    }
                }
            }
        }

        protected virtual AppearanceObject ActiveStyle
        {
            get
            {
                if (this.Style != null)
                {
                    return this.Style;
                }
                return defaultStyle;
            }
        }

        public bool AllowAutoPop
        {
            get
            {
                return this.allowAutoPop;
            }
            set
            {
                this.allowAutoPop = value;
            }
        }

        public int AutoPopDelay
        {
            get
            {
                return this.autoPopDelay;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                if (this.AutoPopDelay != value)
                {
                    this.autoPopDelay = value;
                    this.AutoPopTimer.Interval = this.AutoPopDelay;
                }
            }
        }

        protected virtual System.Windows.Forms.Timer AutoPopTimer
        {
            get
            {
                return this.autoPopTimer;
            }
        }

        public int InitialDelay
        {
            get
            {
                return this.initialDelay;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                if (this.InitialDelay != value)
                {
                    this.initialDelay = value;
                    this.InitialTimer.Interval = this.InitialDelay;
                }
            }
        }

        protected virtual System.Windows.Forms.Timer InitialTimer
        {
            get
            {
                return this.initialTimer;
            }
        }

        public int ReshowDelay
        {
            get
            {
                return this.reshowDelay;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                if (this.ReshowDelay != value)
                {
                    this.reshowDelay = value;
                }
            }
        }

        public virtual AppearanceObject Style
        {
            get
            {
                return this.style;
            }
            set
            {
                if (this.Style != value)
                {
                    this.style = value;
                    this.UpdateToolStyle();
                }
            }
        }

        public virtual ToolTipWindow ToolWindow
        {
            get
            {
                return this.toolWindow;
            }
        }
    }
}

