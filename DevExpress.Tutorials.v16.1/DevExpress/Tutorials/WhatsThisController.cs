namespace DevExpress.Tutorials
{
    using DevExpress.XtraEditors;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class WhatsThisController
    {
        private WhatsThisControlsCollection collection = new WhatsThisControlsCollection();
        private ColoredHint coloredToolTip;
        private IWhatsThisProvider frm;
        private WhatsThisHintCollection hints = new WhatsThisHintCollection();
        private WhatsThisControlsCollection registeredVisibleControls = new WhatsThisControlsCollection();
        private FrmWhatsThisBase whatsThisForm = null;
        private Bitmap whatsThisModuleBitmap;

        public WhatsThisController(IWhatsThisProvider frm)
        {
            this.frm = frm;
            this.whatsThisModuleBitmap = null;
            this.coloredToolTip = new ColoredHint();
        }

        private void AssignHintHandlers()
        {
            foreach (WhatsThisHintEntry entry in this.Hints)
            {
                Control controlByName = ControlUtils.GetControlByName(entry.ControlName, this.frm.CurrentModule);
                if (controlByName != null)
                {
                    controlByName.MouseEnter += new EventHandler(this.HintControlMouseEnter);
                    controlByName.MouseLeave += new EventHandler(this.HintControlMouseLeave);
                    PopupBaseEdit edit = controlByName as PopupBaseEdit;
                    if (edit != null)
                    {
                        edit.Popup += new EventHandler(this.pEdit_Popup);
                    }
                }
            }
        }

        public bool ControlExists(string controlName)
        {
            ControlFinder finder = new ControlFinder(controlName, this.frm.CurrentModule);
            finder.ProcessControls();
            return finder.Exists;
        }

        private string GetControlName(Control control)
        {
            return control.Name;
        }

        public Control GetVisibleControlByName(string controlName)
        {
            VisibleControlFinder finder = new VisibleControlFinder(controlName, this.frm.CurrentModule);
            finder.ProcessControls();
            return finder.Result;
        }

        private FrmWhatsThisBase GetWhatsThisFormByPopupInfo(WhatsThisParams popupInfo)
        {
            if (popupInfo.Code == string.Empty)
            {
                return new FrmWhatsThisTextOnly();
            }
            return new FrmWhatsThis();
        }

        public WhatsThisParams GetWhatsThisParams(string controlName)
        {
            return this.collection.PopupInfoByControlName(controlName);
        }

        private void HideCodeContainer()
        {
            if (this.whatsThisForm != null)
            {
                this.whatsThisForm.Close();
            }
        }

        public void HideHint()
        {
            this.coloredToolTip.HideTip();
            this.frm.HintVisible = false;
        }

        private void HintControlMouseEnter(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            this.coloredToolTip.ColoredHintText = this.hints.GetHintTextByControl(ctrl.Name);
            if (this.coloredToolTip.ColoredHintText != string.Empty)
            {
                this.coloredToolTip.ShowAtControl(ctrl);
                this.frm.HintVisible = true;
            }
        }

        private void HintControlMouseLeave(object sender, EventArgs e)
        {
            this.HideHint();
        }

        public bool IsControlRegistered(string controlName)
        {
            return this.collection.HasControl(controlName);
        }

        public bool IsWhatsThisInfoValid()
        {
            if (this.collection.Count == 0)
            {
                return false;
            }
            int num = 0;
            foreach (WhatsThisControlEntry entry in this.collection)
            {
                if (!this.ControlExists(entry.ControlName))
                {
                    MessageBox.Show("The control " + entry.ControlName + " doesn't exist on the current module", "What's This functionality error");
                    num++;
                }
            }
            if (num > 0)
            {
                return false;
            }
            return true;
        }

        private bool NeedToShowCodeContainer(string controlName)
        {
            return ((this.whatsThisForm == null) || ((this.whatsThisForm.ControlName != controlName) || (!this.whatsThisForm.Visible && (this.whatsThisForm.ControlName == controlName))));
        }

        private void pEdit_Popup(object sender, EventArgs e)
        {
            this.HideHint();
        }

        private void ShowCodeContainer(Control control, string controlName)
        {
            WhatsThisParams whatsThisParams = this.GetWhatsThisParams(controlName);
            if ((this.whatsThisForm != null) && this.whatsThisForm.Visible)
            {
                this.HideCodeContainer();
            }
            this.whatsThisForm = this.GetWhatsThisFormByPopupInfo(whatsThisParams);
            this.whatsThisForm.Location = control.PointToScreen(new Point(0, control.Height));
            this.whatsThisForm.StartPosition = FormStartPosition.Manual;
            this.whatsThisForm.Show(controlName, this.GetWhatsThisParams(controlName), this.frm.TutorialInfo.SourceFileType);
        }

        public bool TryToShowWhatsThis(Control control)
        {
            if ((control != null) && control.GetType().Equals(typeof(TextBoxMaskBox)))
            {
                control = control.Parent;
            }
            string controlName = this.GetControlName(control);
            if (control == this.whatsThisForm)
            {
                return false;
            }
            if ((this.whatsThisForm != null) && this.whatsThisForm.Contains(control))
            {
                return false;
            }
            if (!this.IsControlRegistered(controlName))
            {
                this.HideCodeContainer();
                return false;
            }
            if (this.NeedToShowCodeContainer(controlName))
            {
                this.ShowCodeContainer(control, controlName);
            }
            return true;
        }

        public void UpdateRegisteredVisibleControls()
        {
            this.registeredVisibleControls.Clear();
            foreach (WhatsThisControlEntry entry in this.Collection)
            {
                Control visibleControlByName = this.GetVisibleControlByName(entry.ControlName);
                if (visibleControlByName != null)
                {
                    entry.Control = visibleControlByName;
                    this.registeredVisibleControls.Add(entry);
                }
            }
        }

        public void UpdateWhatsThisBitmaps()
        {
            this.whatsThisModuleBitmap = ControlImageCapturer.GetControlBitmap(this.frm.CurrentModule, null);
            this.whatsThisModuleBitmap = this.frm.CurrentShader.ShadeBitmap(this.whatsThisModuleBitmap);
            new WhatsThisControlImageProcessor(this.frm.CurrentModule, this, Graphics.FromImage(this.whatsThisModuleBitmap)).ProcessControls();
        }

        public void UpdateWhatsThisInfo(string xmlFileName, string codeFileNames)
        {
            this.UpdateWhatsThisInfo(xmlFileName, codeFileNames, null);
        }

        public void UpdateWhatsThisInfo(string xmlFileName, string codeFileNames, System.Type type)
        {
            this.collection.Clear();
            this.hints.Clear();
            if (!string.IsNullOrEmpty(xmlFileName))
            {
                new WhatsThisInfoReader(this).ProcessXml(xmlFileName, type);
                this.AssignHintHandlers();
                new WhatsThisSourceFileReader(this, codeFileNames, this.frm.TutorialInfo.SourceFileComment).ProcessFiles();
            }
        }

        public WhatsThisControlsCollection Collection
        {
            get
            {
                return this.collection;
            }
        }

        public UserControl CurrentModule
        {
            get
            {
                return this.frm.CurrentModule;
            }
        }

        public WhatsThisHintCollection Hints
        {
            get
            {
                return this.hints;
            }
        }

        public WhatsThisControlsCollection RegisteredVisibleControls
        {
            get
            {
                return this.registeredVisibleControls;
            }
        }

        public Bitmap WhatsThisModuleBitmap
        {
            get
            {
                return this.whatsThisModuleBitmap;
            }
        }
    }
}

