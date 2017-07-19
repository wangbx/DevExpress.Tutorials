using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace XtraPrintingDemos.PrintableList
{
	public class PropertyEditorControl : System.Windows.Forms.UserControl
	{
		private System.ComponentModel.Container components = null;
        private RadioGroup viewRadioGroup;
		private System.Windows.Forms.Label label1;
		private View viewType;

        public View View {
            get { return viewType; }
            set { viewType = value; }
        }
		public PropertyEditorControl(View view)
		{
			InitializeComponent();

			View = view;
            viewRadioGroup.SelectedIndex = ViewToIndex(View);
		}
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.viewRadioGroup = new DevExpress.XtraEditors.RadioGroup();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.viewRadioGroup.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // viewRadioGroup
            // 
            this.viewRadioGroup.Location = new System.Drawing.Point(23, 20);
            this.viewRadioGroup.Name = "viewRadioGroup";
            this.viewRadioGroup.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.viewRadioGroup.Properties.Appearance.Options.UseBackColor = true;
            this.viewRadioGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.viewRadioGroup.Properties.Columns = 1;
            this.viewRadioGroup.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("", "Large Icons"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("", "Details"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("", "Small Icons"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("", "List")});
            this.viewRadioGroup.Size = new System.Drawing.Size(167, 121);
            this.viewRadioGroup.TabIndex = 2;
            this.viewRadioGroup.SelectedIndexChanged += new System.EventHandler(this.viewRadioGroup_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "View:";
            // 
            // PropertyEditorControl
            // 
            this.Controls.Add(this.viewRadioGroup);
            this.Controls.Add(this.label1);
            this.Name = "PropertyEditorControl";
            this.Size = new System.Drawing.Size(193, 144);
            ((System.ComponentModel.ISupportInitialize)(this.viewRadioGroup.Properties)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		View StringToView(string s) {
			if(s.Equals("Large Icons"))
				return View.LargeIcon;
			else if(s.Equals("Small Icons"))
				return View.SmallIcon;
			else if(s.Equals("Details"))
				return View.Details;
			else
				return View.List;
		}

		int ViewToIndex(View view) {
			if(view == System.Windows.Forms.View.LargeIcon)
				return 0;
            else if(view == System.Windows.Forms.View.SmallIcon)
				return 2;
            else if(view == System.Windows.Forms.View.Details)
				return 1;
			else
				return 3;
		}

        private void viewRadioGroup_SelectedValueChanged(object sender, System.EventArgs e) {
            View = StringToView(viewRadioGroup.Properties.Items[viewRadioGroup.SelectedIndex].Description);
		}
	}
}
