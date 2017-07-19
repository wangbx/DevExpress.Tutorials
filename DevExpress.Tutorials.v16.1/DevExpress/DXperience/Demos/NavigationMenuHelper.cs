namespace DevExpress.DXperience.Demos
{
    using DevExpress.Tutorials.Properties;
    using DevExpress.XtraBars;
    using DevExpress.XtraNavBar;
    using System;

    public class NavigationMenuHelper
    {
        private static BarButtonItem CreateBarButtonItem(BarManager manager, NavBarItemLink link)
        {
            BarButtonItem item = new BarButtonItem(manager, link.Item.Caption) {
                Glyph = link.Item.SmallImage
            };
            item.ItemClick += new ItemClickEventHandler(NavigationMenuHelper.item_ItemClick);
            item.Tag = link;
            item.ButtonStyle = BarButtonStyle.Check;
            return item;
        }

        private static BarItem CreateBarSubItem(BarManager manager, NavBarGroup group)
        {
            BarSubItem item = new BarSubItem(manager, group.Caption);
            item.Popup += new EventHandler(NavigationMenuHelper.item_Popup);
            foreach (NavBarItemLink link in group.ItemLinks)
            {
                item.ItemLinks.Add(CreateBarButtonItem(manager, link));
            }
            return item;
        }

        public static void CreateNavigationMenu(BarSubItem menu, NavBarControl navBar, BarManager manager)
        {
            foreach (NavBarGroup group in navBar.Groups)
            {
                if (((group.Caption != Resources.NewUpdateGroup) && (group.Caption != Resources.HighlightedFeaturesGroup)) && ((group.Caption != Resources.OutdatedStylesGroup) && group.Visible))
                {
                    menu.ItemLinks.Add(CreateBarSubItem(manager, group));
                }
            }
        }

        public static int GetLinkCount(NavBarControl navBar)
        {
            int num = 0;
            foreach (NavBarGroup group in navBar.Groups)
            {
                if (group.Visible)
                {
                    foreach (NavBarItemLink link in group.ItemLinks)
                    {
                        if (link.Visible)
                        {
                            num++;
                        }
                    }
                }
            }
            return num;
        }

        private static ModuleInfo GetModuleInfoBarItemLink(BarItemLink link)
        {
            NavBarItemLink tag = link.Item.Tag as NavBarItemLink;
            if (tag == null)
            {
                return null;
            }
            return (tag.Item.Tag as ModuleInfo);
        }

        private static NavBarItemLink GetNext(NavBarControl navBar, bool forceSearch)
        {
            return GetNext(navBar, forceSearch, false);
        }

        private static NavBarItemLink GetNext(NavBarControl navBar, bool forceSearch, bool demo)
        {
            bool flag = forceSearch;
            foreach (NavBarGroup group in navBar.Groups)
            {
                if (((group.Caption != Resources.NewUpdateGroup) || !demo) && group.Visible)
                {
                    foreach (NavBarItemLink link2 in group.ItemLinks)
                    {
                        if (flag && link2.Visible)
                        {
                            return link2;
                        }
                        if (link2.Visible && (link2 == navBar.SelectedLink))
                        {
                            flag = true;
                        }
                    }
                }
            }
            return null;
        }

        private static NavBarItemLink GetPrev(NavBarControl navBar, bool forceSearch)
        {
            bool flag = forceSearch;
            for (int i = navBar.Groups.Count - 1; i >= 0; i--)
            {
                if (navBar.Groups[i].Visible)
                {
                    for (int j = navBar.Groups[i].ItemLinks.Count - 1; j >= 0; j--)
                    {
                        NavBarItemLink link2 = navBar.Groups[i].ItemLinks[j];
                        if (flag && link2.Visible)
                        {
                            return link2;
                        }
                        if (link2.Visible && (link2 == navBar.SelectedLink))
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
            NavBarItemLink tag = (NavBarItemLink) e.Item.Tag;
            SetLinkVisible(tag);
            ShowModule(tag.NavBar, tag);
        }

        private static void item_Popup(object sender, EventArgs e)
        {
            BarSubItem item = sender as BarSubItem;
            foreach (BarItemLink link in item.ItemLinks)
            {
                ((BarButtonItem) link.Item).Down = ModulesInfo.Instance.CurrentModuleBase == GetModuleInfoBarItemLink(link);
            }
        }

        private static void SetLinkVisible(NavBarItemLink link)
        {
            if (!link.Visible)
            {
                link.Visible = true;
                link.Group.Visible = true;
            }
        }

        private static void ShowModule(NavBarControl navBar, NavBarItemLink link)
        {
            if (link != null)
            {
                navBar.SelectedLink = link;
                navBar.GetViewInfo().MakeLinkVisible(link);
            }
        }

        public static void ShowNext(NavBarControl navBar)
        {
            NavBarItemLink next = GetNext(navBar, false);
            if (next == null)
            {
                next = GetNext(navBar, true);
            }
            ShowModule(navBar, next);
        }

        public static void ShowPrev(NavBarControl navBar)
        {
            NavBarItemLink prev = GetPrev(navBar, false);
            if (prev == null)
            {
                prev = GetPrev(navBar, true);
            }
            ShowModule(navBar, prev);
        }

        public static void StartDemo(NavBarControl navBar)
        {
            NavBarItemLink next = null;
            if (navBar.SelectedLink != null)
            {
                next = GetNext(navBar, false);
            }
            else
            {
                next = GetNext(navBar, true, true);
            }
            if (next == null)
            {
                RibbonMainForm form = navBar.FindForm() as RibbonMainForm;
                if (form != null)
                {
                    form.ClearNavBarFilter();
                    next = GetNext(navBar, true, true);
                }
            }
            ShowModule(navBar, next);
        }
    }
}

