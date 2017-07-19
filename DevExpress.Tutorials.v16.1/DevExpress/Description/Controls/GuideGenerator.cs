namespace DevExpress.Description.Controls
{
    using DevExpress.Demos.XmlSerialization;
    using DevExpress.NoteHint;
    using DevExpress.Tutorials.Description.Hint;
    using DevExpress.Utils;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Ribbon;
    using DevExpress.XtraBars.Ribbon.ViewInfo;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Interop;

    public class GuideGenerator
    {
        public bool AllowEditorDescriptions;
        private GuideGetCurrentModuleDelegate getCurrentModule;

        public virtual void CreateWhatsThisItem(RibbonControl ribbon, GuideGetCurrentModuleDelegate getCurrentModule)
        {
            this.getCurrentModule = getCurrentModule;
            BarButtonItem item = new BarButtonItem {
                Caption = "What's this"
            };
            ribbon.Items.Add(item);
            item.ItemClick += new ItemClickEventHandler(this.OnWhatsThis);
            ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Above;
            ribbon.Toolbar.ItemLinks.Add(item);
            item.Hint = "That's super hint";
            item.SuperTip = new SuperToolTip();
            item.SuperTip.AllowHtmlText = DefaultBoolean.True;
            item.SuperTip.Items.AddTitle("What's this");
            item.SuperTip.Items.Add("Click here if you want learn more on controls used in demo");
            if (ribbon.FindForm() != null)
            {
                ribbon.FindForm().Load += delegate (object sender, EventArgs e) {
                    if (item.Links.Count > 0)
                    {
                        item.Links[0].ShowHint();
                    }
                };
            }
        }

        public virtual void CreateWhatsThisItemAlt(RibbonControl ribbon, GuideGetCurrentModuleDelegate getCurrentModule)
        {
            this.getCurrentModule = getCurrentModule;
            BarWhatsThisItem item = new BarWhatsThisItem {
                Caption = "What's this",
                Hint = ""
            };
            item.SuperTip = new SuperToolTip();
            ribbon.Items.Add(item);
            item.ItemClick += new ItemClickEventHandler(this.OnWhatsThis);
            ribbon.ToolbarLocation = RibbonQuickAccessToolbarLocation.Above;
            ribbon.Toolbar.ItemLinks.Add(item);
            if (ribbon.FindForm() != null)
            {
                ribbon.FindForm().Load += delegate (object sender, EventArgs e) {
                    if (item.Links.Count != 0)
                    {
                        item.Links[0].ShowHint();
                    }
                };
            }
        }

        private void DoHide(RibbonControl ribbon, NoteWindow window, ItemClickEventHandler handler)
        {
            ribbon.ItemClick -= handler;
            window.Hide();
        }

        private void GenerateByCode(List<GuideControlDescription> list)
        {
            GuideControlDescription item = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraGrid.Controls.FindControl",
                AllowParseChildren = false,
                Description = "<b>Find Panel</b>\r\nEnables MS Outlook style search in the Grid Control. <href=https://www.devexpress.com/Products/NET/Controls/WinForms/Grid/>Learn more.</href>\r\n\r\nToolbox Item: <b>GridControl</b>\r\nIncluded in subscriptions: <href=https://www.devexpress.com/Subscriptions/>WinForms, DXperience, Universal</href>\r\n"
            };
            list.Add(item);
            GuideControlDescription description2 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraGrid.GridControl:GridView",
                Description = "\r\n<b>Grid View</b>\r\nEmulates MS Outlook Mail view or MS Access Data Table view.  <href=https://www.devexpress.com/Products/NET/Controls/WinForms/Grid/>Learn more.</href>\r\n\r\nToolbox Item: <b>GridControl</b>\r\nIncluded in subscriptions: <href=https://www.devexpress.com/Subscriptions/>WinForms, DXperience, Universal</href>\r\n"
            };
            list.Add(description2);
            GuideControlDescription description3 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraGrid.GridControl:CardView",
                Description = "\r\n<b>Card View</b>\r\nEmulates MS Outlook Contacts view. <href=https://www.devexpress.com/Products/NET/Controls/WinForms/Grid/>Learn more.</href>\r\n\r\nToolbox Item: <b>GridControl</b>\r\nIncluded in subscriptions: <href=https://www.devexpress.com/Subscriptions/>WinForms, DXperience, Universal</href>\r\n"
            };
            list.Add(description3);
            GuideControlDescription description4 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraGrid.GridControl:CardView",
                Description = "\r\n<b>Layout View</b>\r\nEmulates MS Outlook Contacts view. <href=https://www.devexpress.com/Products/NET/Controls/WinForms/Grid/>Learn more.</href>\r\n\r\nToolbox Item: <b>GridControl</b>\r\nIncluded in subscriptions: <href=https://www.devexpress.com/Subscriptions/>WinForms, DXperience, Universal</href>\r\n"
            };
            list.Add(description4);
            GuideControlDescription description5 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraGrid.GridControl",
                Description = "\r\n<b>Grid View</b>\r\nEmulates MS Outlook Mail view or MS Access Data Table view.  <href=https://www.devexpress.com/Products/NET/Controls/WinForms/Grid/>Learn more.</href>\r\n\r\nToolbox Item: <b>GridControl</b>\r\nIncluded in subscriptions: <href=https://www.devexpress.com/Subscriptions/>WinForms, DXperience, Universal</href>\r\n"
            };
            list.Add(description5);
            GuideControlDescription description6 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraBars.Ribbon.RibbonControl",
                HighlightUseInsideBounds = true
            };
            list.Add(description6);
            GuideControlDescription description7 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
                HighlightUseInsideBounds = true
            };
            list.Add(description7);
            GuideControlDescription description8 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraNavBar.NavBarControl"
            };
            list.Add(description8);
            GuideControlDescription description9 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraSpreadsheet.SpreadsheetControl"
            };
            list.Add(description9);
            GuideControlDescription description10 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraScheduler.SchedulerControl"
            };
            list.Add(description10);
            GuideControlDescription description11 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraScheduler.DateNavigator"
            };
            list.Add(description11);
            GuideControlDescription description12 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraTreeList.TreeList"
            };
            list.Add(description12);
            GuideControlDescription description13 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraEditors.RangeControl"
            };
            list.Add(description13);
            GuideControlDescription description14 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraCharts.ChartControl"
            };
            list.Add(description14);
            GuideControlDescription description15 = new GuideControlDescription {
                ControlTypeName = "DevExpress.ProductsDemo.Win.PivotTileControl"
            };
            list.Add(description15);
            GuideControlDescription description16 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraPivotGrid.PivotGridControl"
            };
            list.Add(description16);
            GuideControlDescription description17 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraMap.MapControl"
            };
            list.Add(description17);
            GuideControlDescription description18 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraPdfViewer.PdfViewer",
                AllowParseChildren = false
            };
            list.Add(description18);
            GuideControlDescription description19 = new GuideControlDescription {
                ControlTypeName = "DevExpress.DocumentView.Controls.ViewControl"
            };
            list.Add(description19);
            GuideControlDescription description20 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraPrinting.Native.WinControls.BookmarkTreeView"
            };
            list.Add(description20);
            GuideControlDescription description21 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraGauges.Win.GaugeControl"
            };
            list.Add(description21);
            GuideControlDescription description22 = new GuideControlDescription {
                ControlTypeName = "DevExpress.Snap.SnapControl"
            };
            list.Add(description22);
            GuideControlDescription description23 = new GuideControlDescription {
                ControlTypeName = "DevExpress.Snap.Extensions.UI.FieldListDockPanel"
            };
            list.Add(description23);
            GuideControlDescription description24 = new GuideControlDescription {
                ControlTypeName = "DevExpress.Snap.Extensions.UI.ReportExplorerDockPanel"
            };
            list.Add(description24);
            GuideControlDescription description25 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraRichEdit.RichEditControl"
            };
            list.Add(description25);
            GuideControlDescription description26 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraTreeMap.TreeMapControl"
            };
            list.Add(description26);
            if (this.AllowEditorDescriptions)
            {
                this.GenerateEditors(list);
            }
        }

        protected virtual void GenerateEditors(List<GuideControlDescription> list)
        {
            GuideControlDescription item = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraEditors.LabelControl"
            };
            list.Add(item);
            GuideControlDescription description2 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraEditors.SimpleButton"
            };
            list.Add(description2);
            GuideControlDescription description3 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraEditors.TextEdit"
            };
            list.Add(description3);
            GuideControlDescription description4 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraEditors.ComboBoxEdit"
            };
            list.Add(description4);
            GuideControlDescription description5 = new GuideControlDescription {
                ControlTypeName = "DevExpress.XtraEditors.PictureEdit"
            };
            list.Add(description5);
        }

        protected virtual List<GuideControlDescription> GenerateTemplateDescriptions()
        {
            List<GuideControlDescription> list = new List<GuideControlDescription>();
            return XMLSerializer<List<GuideControlDescription>>.LoadXmlFromResources(typeof(GuideGenerator).Assembly, "DevExpress.Tutorials.Description.Guide.xml", null);
        }

        private void OnWhatsThis(object sender, ItemClickEventArgs e)
        {
            if (this.getCurrentModule != null)
            {
                Control root = this.getCurrentModule();
                IGuideDescriptionProvider provider = root as IGuideDescriptionProvider;
                if ((root != null) && ((provider == null) || provider.Enabled))
                {
                    GuideControl control2 = new GuideControlEx();
                    List<GuideControlDescription> templates = this.GenerateTemplateDescriptions();
                    if (provider != null)
                    {
                        provider.UpdateDescriptions(templates);
                    }
                    control2.Init(templates, root);
                    control2.Show();
                }
            }
        }

        private class BarWhatsThisItem : BarButtonItem
        {
            protected override BarItemLink CreateLinkCore(BarItemLinkReadOnlyCollection ALinks, BarItem AItem, object ALinkedObject)
            {
                return new GuideGenerator.BarWhatsThisItemLink(ALinks, AItem, ALinkedObject);
            }
        }

        private class BarWhatsThisItemLink : BarButtonItemLink
        {
            private Timer hintTimer;
            private bool hintVisible;

            public BarWhatsThisItemLink(BarItemLinkReadOnlyCollection ALinks, BarItem AItem, object ALinkedObject) : base(ALinks, AItem, ALinkedObject)
            {
            }

            private void DoHide(RibbonControl ribbon, NoteWindow window, ItemClickEventHandler handler)
            {
                this.hintVisible = false;
                ribbon.ItemClick -= handler;
                window.Hide();
                if (this.hintTimer != null)
                {
                    this.hintTimer.Dispose();
                }
                this.hintTimer = null;
            }

            protected override ToolTipControlInfo GetToolTipInfo(RibbonHitInfo hi, System.Drawing.Point point)
            {
                ToolTipControlInfo toolTipInfo = base.GetToolTipInfo(hi, point);
                toolTipInfo.ImmediateToolTip = true;
                return toolTipInfo;
            }

            protected override void OnBeforeShowHint(ToolTipControllerShowEventArgs e)
            {
                RibbonControl ribbon;
                NoteWindow nw;
                Rectangle display;
                ItemClickEventHandler click;
                int ticks;
                e.Show = false;
                if (!base.ScreenBounds.IsEmpty && !this.hintVisible)
                {
                    this.hintVisible = true;
                    GuideGenerator.BarWhatsThisItemLink link = this;
                    ribbon = base.Ribbon;
                    nw = new NoteWindow {
                        HintPosition = NoteHintPosition.Down,
                        HintContent = new GuideDescription()
                    };
                    new WindowInteropHelper(nw).Owner = ribbon.FindForm().Handle;
                    nw.ShowHtmlCloseButton = true;
                    display = link.ScreenBounds;
                    nw.ShowActivated = false;
                    nw.Show(display);
                    click = null;
                    click = (s1, e1) => this.DoHide(ribbon, nw, click);
                    nw.IsVisibleChanged += delegate (object s2, DependencyPropertyChangedEventArgs e2) {
                        if (!((Window) s2).IsVisible)
                        {
                            this.DoHide(ribbon, nw, click);
                        }
                    };
                    ribbon.ItemClick += click;
                    this.hintTimer = new Timer();
                    ticks = 0;
                    this.hintTimer.Interval = 100;
                    this.hintTimer.Tick += delegate (object s3, EventArgs e3) {
                        if (ticks++ == 70)
                        {
                            this.DoHide(ribbon, nw, click);
                        }
                        else if (this.ScreenBounds != display)
                        {
                            this.DoHide(ribbon, nw, click);
                        }
                    };
                    this.hintTimer.Start();
                }
            }
        }
    }
}

