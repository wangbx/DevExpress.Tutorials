namespace DevExpress.DXperience.Demos
{
    using DevExpress.Tutorials.Properties;
    using DevExpress.XtraBars;
    using DevExpress.XtraBars.Navigation;
    using System;

    public class AccordionNavigationMenuHelper
    {
        private static BarButtonItem CreateBarButtonItem(BarManager manager, AccordionControlElement node)
        {
            BarButtonItem item = new BarButtonItem(manager, node.Text);
            item.ItemClick += new ItemClickEventHandler(AccordionNavigationMenuHelper.item_ItemClick);
            item.Tag = node;
            item.ButtonStyle = BarButtonStyle.Check;
            return item;
        }

        private static BarItem CreateBarSubItem(BarManager manager, AccordionControlElement group)
        {
            BarSubItem item = new BarSubItem(manager, group.Text);
            item.Popup += new EventHandler(AccordionNavigationMenuHelper.item_Popup);
            foreach (AccordionControlElement element in group.Elements)
            {
                item.ItemLinks.Add(CreateBarButtonItem(manager, element));
            }
            return item;
        }

        public static void CreateNavigationMenu(BarSubItem menu, AccordionControl accordionControl, BarManager manager)
        {
            foreach (AccordionControlElement element in accordionControl.Elements)
            {
                if (((element.Text != Resources.NewUpdateGroup) && (element.Text != Resources.HighlightedFeaturesGroup)) && ((element.Text != Resources.OutdatedStylesGroup) && element.Visible))
                {
                    menu.ItemLinks.Add(CreateBarSubItem(manager, element));
                }
            }
        }

        private static ModuleInfo GetModuleInfoBarItemLink(BarItemLink link)
        {
            AccordionControlElement tag = link.Item.Tag as AccordionControlElement;
            if (tag == null)
            {
                return null;
            }
            return (tag.Tag as ModuleInfo);
        }

        private static AccordionControlElement GetNext(AccordionControl accordionControl, bool forceSearch)
        {
            return GetNext(accordionControl, forceSearch, false);
        }

        private static AccordionControlElement GetNext(AccordionControl accordionControl, bool forceSearch, bool demo)
        {
            bool flag = forceSearch;
            foreach (AccordionControlElement element2 in accordionControl.Elements)
            {
                if (((element2.Text != Resources.NewUpdateGroup) || !demo) && element2.Visible)
                {
                    foreach (AccordionControlElement element3 in element2.Elements)
                    {
                        if (flag && element3.Visible)
                        {
                            return element3;
                        }
                        if (element3.Visible && (element3 == accordionControl.SelectedElement))
                        {
                            flag = true;
                        }
                    }
                }
            }
            return null;
        }

        public static int GetNodeCount(AccordionControl accordionControl)
        {
            int num = 0;
            foreach (AccordionControlElement element in accordionControl.Elements)
            {
                if (element.Visible)
                {
                    foreach (AccordionControlElement element2 in element.Elements)
                    {
                        if (element2.Visible)
                        {
                            num++;
                        }
                    }
                }
            }
            return num;
        }

        private static AccordionControlElement GetPrev(AccordionControl accordionControl, bool forceSearch)
        {
            bool flag = forceSearch;
            for (int i = accordionControl.Elements.Count - 1; i >= 0; i--)
            {
                if (accordionControl.Elements[i].Visible)
                {
                    for (int j = accordionControl.Elements[i].Elements.Count - 1; j >= 0; j--)
                    {
                        AccordionControlElement element2 = accordionControl.Elements[i].Elements[j];
                        if (flag && element2.Visible)
                        {
                            return element2;
                        }
                        if (element2.Visible && (element2 == accordionControl.SelectedElement))
                        {
                            flag = true;
                        }
                    }
                }
            }
            return null;
        }

        private static void item_ItemClick(object sender, ItemClickEventArgs e)
        {
            AccordionControlElement tag = (AccordionControlElement) e.Item.Tag;
            ShowModule(tag.AccordionControl, tag);
        }

        private static void item_Popup(object sender, EventArgs e)
        {
            BarSubItem item = sender as BarSubItem;
            foreach (BarItemLink link in item.ItemLinks)
            {
                ((BarButtonItem) link.Item).Down = ModulesInfo.Instance.CurrentModuleBase == GetModuleInfoBarItemLink(link);
            }
        }

        private static void ShowModule(AccordionControl accordionControl, AccordionControlElement node)
        {
            if (node != null)
            {
                accordionControl.SelectElement(node);
                node.Visible = true;
                if (!node.OwnerElement.Expanded)
                {
                    node.OwnerElement.Expanded = true;
                }
            }
        }

        public static void ShowNext(AccordionControl accordionControl)
        {
            AccordionControlElement next = GetNext(accordionControl, false);
            if (next == null)
            {
                next = GetNext(accordionControl, true);
            }
            ShowModule(accordionControl, next);
        }

        public static void ShowPrev(AccordionControl accordionControl)
        {
            AccordionControlElement prev = GetPrev(accordionControl, false);
            if (prev == null)
            {
                prev = GetPrev(accordionControl, true);
            }
            ShowModule(accordionControl, prev);
        }

        public static void StartDemo(AccordionControl accordionControl)
        {
            AccordionControlElement node = null;
            if (accordionControl.SelectedElement != null)
            {
                node = GetNext(accordionControl, false);
            }
            else
            {
                node = GetNext(accordionControl, true, true);
            }
            if ((node == null) && (accordionControl.FindForm() is RibbonMainForm))
            {
                node = GetNext(accordionControl, true, true);
            }
            ShowModule(accordionControl, node);
        }
    }
}

