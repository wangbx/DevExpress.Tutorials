using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraPrintingLinks;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace XtraPrintingDemos.Calendar {
	public class CalendarModule : PreviewControl {
		private CalendarLink cLink =  new CalendarLink(null);

		private System.Windows.Forms.Label label3;
        private DevExpress.XtraBars.BarListItem bePageSize;
        private DevExpress.XtraBars.PopupControlContainer popupControlContainer1;
        private DevExpress.XtraBars.BarButtonItem beMonthAndYear;
        private DevExpress.XtraBars.BarCheckItem bchPreviewMode;
        private DevExpress.XtraBars.BarCheckItem bchPortrait;
        private DevExpress.XtraBars.BarCheckItem bchLandscape;
        private IContainer components;
        CalendarControl calendar;

		public CalendarModule() {
			InitializeComponent();
            HideButtonOptions();
            HideButtonRefresh();

            calendar = new CalendarControl() { DateTime = DevExpress.XtraPrinting.Native.DateTimeHelper.Today };
            calendar.ShowClearButton = false;
            calendar.DateTimeChanged += new EventHandler(calendar_EditDateModified);
            popupControlContainer1.Size = calendar.CalcBestSize();

            calendar.Dock = DockStyle.Fill;
            popupControlContainer1.Controls.Add(calendar);
            printingSystem.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
		}

		protected override void Dispose( bool disposing ) {
			if( disposing ) {
				if(components != null) {
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
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalendarModule));
            this.label3 = new System.Windows.Forms.Label();
            this.bePageSize = new DevExpress.XtraBars.BarListItem();
            this.popupControlContainer1 = new DevExpress.XtraBars.PopupControlContainer(this.components);
            this.beMonthAndYear = new DevExpress.XtraBars.BarButtonItem();
            this.bchPreviewMode = new DevExpress.XtraBars.BarCheckItem();
            this.bchPortrait = new DevExpress.XtraBars.BarCheckItem();
            this.bchLandscape = new DevExpress.XtraBars.BarCheckItem();
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).BeginInit();
            this.SuspendLayout();
            // 
            // ribbonControl
            // 
            this.ribbonControl.ExpandCollapseItem.Id = 0;
            this.ribbonControl.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bePageSize,
            this.beMonthAndYear,
            this.bchPreviewMode,
            this.bchPortrait,
            this.bchLandscape});
            this.ribbonControl.MaxItemId = 74;
            this.ribbonControl.Size = new System.Drawing.Size(1211, 142);
            // 
            // ribbonGroupActions
            // 
            this.ribbonGroupActions.ItemLinks.Add(this.beMonthAndYear);
            this.ribbonGroupActions.ItemLinks.Add(this.bchPreviewMode);
            this.ribbonGroupActions.ItemLinks.Add(this.bePageSize);
            this.ribbonGroupActions.ItemLinks.Add(this.bchPortrait, true);
            this.ribbonGroupActions.ItemLinks.Add(this.bchLandscape);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(56, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            // 
            // bePageSize
            // 
            this.bePageSize.Caption = "Full Page";
            this.bePageSize.Glyph = ((System.Drawing.Image)(resources.GetObject("bePageSize.Glyph")));
            this.bePageSize.Hint = "Select page occupancy";
            this.bePageSize.Id = 68;
            this.bePageSize.ItemIndex = 0;
            this.bePageSize.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bePageSize.LargeGlyph")));
            this.bePageSize.Name = "bePageSize";
            this.bePageSize.ShowChecks = true;
            this.bePageSize.Strings.AddRange(new object[] {
            "Full Page",
            "2/3 of a Page\r\n",
            "1/2 of a Page\r\n"});
            this.bePageSize.ListItemClick += new DevExpress.XtraBars.ListItemClickEventHandler(this.bePageSize_ListItemClick);
            // 
            // popupControlContainer1
            // 
            this.popupControlContainer1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.popupControlContainer1.Location = new System.Drawing.Point(13, 148);
            this.popupControlContainer1.Name = "popupControlContainer1";
            this.popupControlContainer1.Ribbon = this.ribbonControl;
            this.popupControlContainer1.Size = new System.Drawing.Size(250, 130);
            this.popupControlContainer1.TabIndex = 4;
            this.popupControlContainer1.Visible = false;
            // 
            // beMonthAndYear
            // 
            this.beMonthAndYear.ActAsDropDown = true;
            this.beMonthAndYear.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.beMonthAndYear.Caption = "Date";
            this.beMonthAndYear.DropDownControl = this.popupControlContainer1;
            this.beMonthAndYear.Glyph = ((System.Drawing.Image)(resources.GetObject("beMonthAndYear.Glyph")));
            this.beMonthAndYear.Hint = "Select a date";
            this.beMonthAndYear.Id = 70;
            this.beMonthAndYear.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("beMonthAndYear.LargeGlyph")));
            this.beMonthAndYear.Name = "beMonthAndYear";
            // 
            // bchPreviewMode
            // 
            this.bchPreviewMode.Caption = "Month View";
            this.bchPreviewMode.Glyph = ((System.Drawing.Image)(resources.GetObject("bchPreviewMode.Glyph")));
            this.bchPreviewMode.Hint = "Select month or year view";
            this.bchPreviewMode.Id = 71;
            this.bchPreviewMode.ItemAppearance.Hovered.Options.UseTextOptions = true;
            this.bchPreviewMode.ItemAppearance.Hovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.bchPreviewMode.ItemAppearance.Normal.Options.UseTextOptions = true;
            this.bchPreviewMode.ItemAppearance.Normal.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.bchPreviewMode.ItemAppearance.Pressed.Options.UseTextOptions = true;
            this.bchPreviewMode.ItemAppearance.Pressed.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
            this.bchPreviewMode.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bchPreviewMode.LargeGlyph")));
            this.bchPreviewMode.Name = "bchPreviewMode";
            this.bchPreviewMode.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bchPreviewMode_CheckedChanged);
            // 
            // bchPortrait
            // 
            this.bchPortrait.Caption = "Portrait";
            this.bchPortrait.Checked = true;
            this.bchPortrait.GroupIndex = 99;
            this.bchPortrait.Hint = "Select a page orientation";
            this.bchPortrait.Id = 72;
            this.bchPortrait.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bchPortrait.LargeGlyph")));
            this.bchPortrait.Name = "bchPortrait";
            this.bchPortrait.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bchLandscape_CheckedChanged);
            // 
            // bchLandscape
            // 
            this.bchLandscape.Caption = "Landscape";
            this.bchLandscape.GroupIndex = 99;
            this.bchLandscape.Hint = "Select a page orientation";
            this.bchLandscape.Id = 73;
            this.bchLandscape.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("bchLandscape.LargeGlyph")));
            this.bchLandscape.Name = "bchLandscape";
            this.bchLandscape.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bchLandscape_CheckedChanged);
            // 
            // CalendarModule
            // 
            this.Controls.Add(this.popupControlContainer1);
            this.Name = "CalendarModule";
            this.Controls.SetChildIndex(this.ribbonControl, 0);
            this.Controls.SetChildIndex(this.ribbonStatusBar1, 0);
            this.Controls.SetChildIndex(this.popupControlContainer1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.printingSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fPrintRibbonController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupControlContainer1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        void calendar_EditDateModified(object sender, EventArgs e) {
            popupControlContainer1.HidePopup();
            CreateDocument();
        }
        
        protected override void ActivateCore() {
			base.ActivateCore();
			CreateDocument();
		}

		protected override void CreateDocumentCore() {
            if(calendar == null)
                return;
            printingSystem.PageSettingsChanged -= printingSystem_PageSettingsChanged;
            printingSystem.AfterMarginsChange -= printingSystem_AfterMarginsChange;
            try {
                int year = calendar.DateTime.Year;
                int month = calendar.DateTime.Month;
                int day = calendar.DateTime.Day;

                cLink.SelectedDate = new DateTime(year, month, day);
                if(bchPreviewMode.Checked) { //month
                    cLink.CreateMonthCalendar(printingSystem, bePageSize.ItemIndex, month, year, bchLandscape.Checked);
                    bePageSize.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                } else {
                    cLink.CreateYearCalendar(printingSystem, year, bchLandscape.Checked);
                    bePageSize.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
            } finally {
                printingSystem.PageSettingsChanged += printingSystem_PageSettingsChanged;
                printingSystem.AfterMarginsChange += printingSystem_AfterMarginsChange;
            }
        }

        void printingSystem_AfterMarginsChange(object sender, DevExpress.XtraPrinting.MarginsChangeEventArgs e) {
            CreateDocument();
        }
        void printingSystem_PageSettingsChanged(object sender, EventArgs e) {
            this.bchPortrait.CheckedChanged -= new DevExpress.XtraBars.ItemClickEventHandler(this.bchLandscape_CheckedChanged);
            this.bchLandscape.CheckedChanged -= new DevExpress.XtraBars.ItemClickEventHandler(this.bchLandscape_CheckedChanged);
            this.bchLandscape.Checked = printingSystem.PageSettings.Landscape;
            this.bchPortrait.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bchLandscape_CheckedChanged);
            this.bchLandscape.CheckedChanged += new DevExpress.XtraBars.ItemClickEventHandler(this.bchLandscape_CheckedChanged);

            CreateDocument();
        }

        private void bePageSize_ListItemClick(object sender, DevExpress.XtraBars.ListItemClickEventArgs e) {
            bePageSize.Caption = bePageSize.Strings[e.Index];
            CreateDocument();
        }

        private void bchPreviewMode_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CreateDocument();
        }

        private void bchLandscape_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            CreateDocument();
        }
	}
}
