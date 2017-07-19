namespace DevExpress.Tutorials
{
    using DevExpress.XtraEditors;
    using System;

    public class FrmWhatsThisBase : XtraForm
    {
        private string controlName;
        private bool resizing;

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            base.Close();
        }

        protected override void OnResize(EventArgs e)
        {
            if (!this.resizing)
            {
                this.resizing = true;
                base.OnResize(e);
                this.UpdateDescriptionPanel();
                this.resizing = false;
            }
        }

        public virtual void Show(string controlName, WhatsThisParams whatsThisParams, SourceFileType sourceFileType)
        {
            this.controlName = controlName;
            this.UpdateControls(whatsThisParams);
            this.UpdateDescriptionPanel();
            ControlUtils.UpdateFrmToFitScreen(this);
            base.Show();
        }

        protected virtual void UpdateControls(WhatsThisParams whatsThisParams)
        {
            this.Text = whatsThisParams.Caption + " (" + this.controlName + ")";
        }

        protected virtual void UpdateDescriptionPanel()
        {
        }

        public string ControlName
        {
            get
            {
                return this.controlName;
            }
        }
    }
}

