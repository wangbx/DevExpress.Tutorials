namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraNavBar;
    using System;
    using System.Collections.Generic;

    public class NavBarFilter : IDisposable
    {
        private Dictionary<NavBarGroup, bool> initialGroupsVisibility;
        private Dictionary<NavBarItemLink, bool> initialLinksVisibility;
        private NavBarItemLink initialSelectedLink;
        private NavBarControl navBar;

        public NavBarFilter(NavBarControl navBar)
        {
            this.navBar = navBar;
        }

        private void CheckSelectedLink()
        {
            if (this.initialSelectedLink != this.NavBar.SelectedLink)
            {
                if ((this.NavBar.SelectedLink != null) && (this.NavBar.SelectedLink.Group != null))
                {
                    this.NavBar.SelectedLink.Group.SelectedLinkIndex = -1;
                }
                if (((this.NavBar.SelectedLink == null) && (this.initialSelectedLink != null)) && (this.initialSelectedLink.Visible && this.initialSelectedLink.Group.Visible))
                {
                    this.NavBar.SelectedLink = this.initialSelectedLink;
                }
            }
        }

        public virtual void Dispose()
        {
            if (this.initialGroupsVisibility != null)
            {
                this.initialGroupsVisibility.Clear();
            }
            if (this.initialLinksVisibility != null)
            {
                this.initialLinksVisibility.Clear();
            }
            this.initialSelectedLink = null;
        }

        public void FilterNavBar(string text)
        {
            if (this.initialLinksVisibility == null)
            {
                this.UpdateLinksVisibility();
            }
            if (this.initialGroupsVisibility == null)
            {
                this.UpdateGroupsVisibility();
            }
            if (this.NavBar.SelectedLink != null)
            {
                this.initialSelectedLink = this.NavBar.SelectedLink;
            }
            text = text.ToLowerInvariant();
            this.NavBar.BeginUpdate();
            try
            {
                foreach (KeyValuePair<NavBarItemLink, bool> pair in this.initialLinksVisibility)
                {
                    string str = pair.Key.Caption.ToLowerInvariant();
                    pair.Key.Visible = string.IsNullOrEmpty(text) || str.Contains(text);
                }
                foreach (NavBarGroup group in this.NavBar.Groups)
                {
                    if (group.VisibleItemLinks.Count == 0)
                    {
                        group.Visible = false;
                    }
                    else
                    {
                        group.Visible = this.initialGroupsVisibility.ContainsKey(group);
                    }
                }
                this.CheckSelectedLink();
            }
            finally
            {
                this.NavBar.EndUpdate();
            }
        }

        public void Reset()
        {
            this.FilterNavBar("");
            this.initialGroupsVisibility = null;
            this.initialLinksVisibility = null;
            this.initialSelectedLink = null;
        }

        private void UpdateGroupsVisibility()
        {
            this.initialGroupsVisibility = new Dictionary<NavBarGroup, bool>();
            foreach (NavBarGroup group in this.NavBar.Groups)
            {
                if (group.Visible)
                {
                    this.initialGroupsVisibility[group] = true;
                }
            }
        }

        private void UpdateLinksVisibility()
        {
            this.initialLinksVisibility = new Dictionary<NavBarItemLink, bool>();
            this.initialSelectedLink = this.NavBar.SelectedLink;
            foreach (NavBarGroup group in this.NavBar.Groups)
            {
                foreach (NavBarItemLink link in group.ItemLinks)
                {
                    if (link.Visible)
                    {
                        this.initialLinksVisibility[link] = true;
                    }
                }
            }
        }

        protected NavBarControl NavBar
        {
            get
            {
                return this.navBar;
            }
        }
    }
}

