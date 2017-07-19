namespace DevExpress.DXperience.Demos
{
    using DevExpress.Utils.About;
    using DevExpress.XtraBars.Navigation;
    using DevExpress.XtraEditors;
    using DevExpress.XtraNavBar;
    using System;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class ModulesInfo
    {
        private ModuleInfoCollection fCollection = new ModuleInfoCollection();
        private ModuleInfo fCurrentModule = null;
        private static ModulesInfo fInstance = new ModulesInfo();

        public event EventHandler CurrentModuleChanged;

        public static void Add(ModuleInfo info)
        {
            fInstance.fCollection.Add(info);
        }

        public static void Add(string fName, string fTypeName)
        {
            Add(fName, fTypeName, "");
        }

        public static void Add(string fName, string fTypeName, string fDescription)
        {
            Add(fName, fTypeName, fDescription, null);
        }

        public static void Add(string fName, string fTypeName, string fDescription, Image fIcon)
        {
            Add(fName, fTypeName, fDescription, fIcon, "");
        }

        public static void Add(string fName, string fTypeName, string fDescription, Image fIcon, string fGroup)
        {
            ModuleInfo info = new ModuleInfo(fName, fTypeName, fDescription, fIcon, fGroup);
            fInstance.fCollection.Add(info);
        }

        public static void Add(string fName, string fTypeName, string fDescription, Image fIcon, string fCodeFile, string fXmlFile)
        {
            Add(fName, fTypeName, fDescription, fIcon, fCodeFile, fXmlFile, "");
        }

        public static void Add(string fName, string fTypeName, string fDescription, Image fIcon, string fCodeFile, string fXmlFile, string fAboutFile)
        {
            Add(fName, fTypeName, fDescription, fIcon, fCodeFile, fXmlFile, fAboutFile, "");
        }

        public static void Add(string fName, string fTypeName, string fDescription, Image fIcon, string fCodeFile, string fXmlFile, string fAboutFile, string fUri)
        {
            ModuleInfo info = new ModuleInfo(fName, fTypeName, fDescription, fIcon, "", fCodeFile, fXmlFile, fAboutFile, fUri);
            fInstance.fCollection.Add(info);
        }

        public static void FillAccordionControl(AccordionControl accordionControl)
        {
            FillAccordionControl(accordionControl, NavBarGroupStyle.SmallIconsList);
        }

        public static void FillAccordionControl(AccordionControl accordionControl, NavBarGroupStyle groupStyle)
        {
            FillAccordionControl(accordionControl, groupStyle, true);
        }

        public static void FillAccordionControl(AccordionControl accordionControl, NavBarGroupStyle groupStyle, bool showOutdated)
        {
            FillAccordionControl(accordionControl, groupStyle, showOutdated, NavBarImage.Default);
        }

        public static void FillAccordionControl(AccordionControl accordionControl, NavBarGroupStyle groupStyle, bool showOutdated, NavBarImage groupCaptionImage)
        {
            if (accordionControl != null)
            {
                accordionControl.BeginUpdate();
                for (int i = 0; i < Count; i++)
                {
                    if ((GetItem(i).Group != "About") && (GetItem(i).Group.IndexOf("Outdated") == -1))
                    {
                        AccordionControlElement item = new AccordionControlElement {
                            Style = ElementStyle.Item,
                            Text = GetItem(i).Name,
                            Tag = GetItem(i)
                        };
                        GetAccordionControlGroup(accordionControl, GetItem(i).Group, showOutdated).Elements.Add(item);
                    }
                }
                accordionControl.EndUpdate();
            }
        }

        public static void FillListBox(ListBoxControl listBox)
        {
            if (listBox != null)
            {
                for (int i = 0; i < Count; i++)
                {
                    listBox.Items.Add(GetItem(i).Name);
                }
            }
        }

        public static void FillListBox(ListBoxControl listBox, string[] names)
        {
            if (listBox != null)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (!NameExist(names, GetItem(i).Name))
                    {
                        listBox.Items.Add(GetItem(i).Name);
                    }
                }
            }
        }

        public static void FillNavBar(NavBarControl navBar)
        {
            FillNavBar(navBar, NavBarGroupStyle.SmallIconsList);
        }

        public static void FillNavBar(NavBarControl navBar, NavBarGroupStyle groupStyle)
        {
            FillNavBar(navBar, groupStyle, true);
        }

        public static void FillNavBar(NavBarControl navBar, NavBarGroupStyle groupStyle, bool showOutdated)
        {
            FillNavBar(navBar, groupStyle, showOutdated, NavBarImage.Default);
        }

        public static void FillNavBar(NavBarControl navBar, NavBarGroupStyle groupStyle, bool showOutdated, NavBarImage groupCaptionImage)
        {
            if (navBar != null)
            {
                navBar.BeginUpdate();
                for (int i = 0; i < Count; i++)
                {
                    if ((GetItem(i).Group != "About") && (GetItem(i).Group.IndexOf("Outdated") == -1))
                    {
                        NavBarItem item = new NavBarItem();
                        navBar.Items.Add(item);
                        item.Caption = GetItem(i).Name;
                        item.SmallImage = item.LargeImage = GetItem(i).Icon;
                        item.Tag = GetItem(i);
                        GetNavBarGroup(navBar, GetItem(i).Group, groupStyle, showOutdated, groupCaptionImage).ItemLinks.Add(new NavBarItemLink(item));
                    }
                }
                navBar.EndUpdate();
            }
        }

        private static AccordionControlElement GetAccordionControlGroup(AccordionControl accordionControl, string groupName, bool showOutdated)
        {
            foreach (AccordionControlElement element in accordionControl.Elements)
            {
                if (element.Text == groupName)
                {
                    return element;
                }
            }
            AccordionControlElement item = new AccordionControlElement();
            accordionControl.Elements.Add(item);
            item.Text = groupName;
            item.Expanded = groupName.IndexOf("Outdated") == -1;
            if (!showOutdated && !item.Expanded)
            {
                item.Visible = false;
            }
            return item;
        }

        public static ModuleInfo GetItem(int index)
        {
            return fInstance.fCollection[index];
        }

        public static ModuleInfo GetItem(string fName)
        {
            return fInstance.fCollection[fName];
        }

        public static ModuleInfo GetItemByType(string fName)
        {
            foreach (ModuleInfo info in fInstance.fCollection)
            {
                if (fName.Equals(info.TypeName) || fName.Equals(info.FullTypeName))
                {
                    return info;
                }
            }
            return null;
        }

        public static int GetItemIndex(ModuleInfo item)
        {
            return fInstance.fCollection.IndexOf(item);
        }

        private static NavBarGroup GetNavBarGroup(NavBarControl navBar, string groupName, NavBarGroupStyle groupStyle, bool showOutdated, NavBarImage groupCaptionImage)
        {
            foreach (NavBarGroup group in navBar.Groups)
            {
                if (group.Caption == groupName)
                {
                    return group;
                }
            }
            NavBarGroup group2 = navBar.Groups.Add();
            group2.Caption = groupName;
            group2.GroupStyle = groupStyle;
            group2.Expanded = groupName.IndexOf("Outdated") == -1;
            group2.GroupCaptionUseImage = groupCaptionImage;
            if (!showOutdated && !group2.Expanded)
            {
                group2.Visible = false;
            }
            return group2;
        }

        private static bool NameExist(string[] names, string name)
        {
            foreach (string str in names)
            {
                if (str == name)
                {
                    return true;
                }
            }
            return false;
        }

        protected static void RaiseModuleChanged()
        {
            if (Instance.CurrentModuleChanged != null)
            {
                Instance.CurrentModuleChanged(Instance, EventArgs.Empty);
            }
        }

        public static void ShowModule(Control container, string fName)
        {
            ModuleInfo item = GetItem(fName);
            Cursor current = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            if (container.Parent != null)
            {
                container.Parent.SuspendLayout();
            }
            container.SuspendLayout();
            try
            {
                Control control = null;
                if (Instance.CurrentModuleBase != null)
                {
                    control = Instance.CurrentModuleBase.TModule;
                }
                Control tModule = item.TModule;
                tModule.Bounds = container.DisplayRectangle;
                Instance.CurrentModuleBase = item;
                tModule.Visible = false;
                container.Controls.Add(tModule);
                tModule.Dock = DockStyle.Fill;
                tModule.Visible = true;
                if (control != null)
                {
                    control.Visible = false;
                }
            }
            finally
            {
                container.ResumeLayout(true);
                if (container.Parent != null)
                {
                    container.Parent.ResumeLayout(true);
                }
                Cursor.Current = current;
            }
            RaiseModuleChanged();
        }

        public static int Count
        {
            get
            {
                return fInstance.fCollection.Count;
            }
        }

        public static Control CurrentModule
        {
            get
            {
                if (CurrentModuleInfo != null)
                {
                    return CurrentModuleInfo.TModule;
                }
                return null;
            }
        }

        public ModuleInfo CurrentModuleBase
        {
            get
            {
                return this.fCurrentModule;
            }
            set
            {
                this.fCurrentModule = value;
                if (value != null)
                {
                    UAlgo.Default.DoEvent(3, 1, value.FullTypeName);
                }
            }
        }

        public static ModuleInfo CurrentModuleInfo
        {
            get
            {
                return fInstance.fCurrentModule;
            }
        }

        public static ModulesInfo Instance
        {
            get
            {
                return fInstance;
            }
        }
    }
}

