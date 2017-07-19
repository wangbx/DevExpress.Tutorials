namespace DevExpress.DXperience.Demos
{
    using DevExpress.XtraBars;
    using System;
    using System.Drawing;

    public class BarInfo
    {
        private string caption;
        private bool check;
        private bool fBeginGroup;
        private int gIndex;
        private ItemClickEventHandler handler;
        private Image image;
        private BarInfo[] info;
        private bool isCheckItem;
        private DevExpress.XtraBars.BarItem item;

        public BarInfo(string caption, ItemClickEventHandler handler, Image image, bool isCheckItem, bool check, bool beginGroup) : this(caption, handler, image, isCheckItem, check, beginGroup, null, -1)
        {
        }

        public BarInfo(string caption, ItemClickEventHandler handler, Image image, bool isCheckItem, bool check, bool beginGroup, BarInfo[] info, int gIndex)
        {
            this.caption = caption;
            this.handler = handler;
            this.image = image;
            this.isCheckItem = isCheckItem;
            this.check = check;
            this.fBeginGroup = beginGroup;
            this.info = info;
            this.gIndex = gIndex;
        }

        public DevExpress.XtraBars.BarItem CreateItem(BarManager manager)
        {
            return this.CreateItem(manager, -1);
        }

        public DevExpress.XtraBars.BarItem CreateItem(BarManager manager, int groupIndex)
        {
            if (this.isCheckItem)
            {
                this.item = new BarCheckItem(manager, this.check);
                this.item.Caption = this.caption;
                if (groupIndex != -1)
                {
                    ((BarCheckItem) this.item).GroupIndex = groupIndex;
                }
            }
            else
            {
                this.item = new BarButtonItem(manager, this.caption);
            }
            if (this.info != null)
            {
                BarButtonItem item = this.item as BarButtonItem;
                if (item != null)
                {
                    item.ButtonStyle = BarButtonStyle.DropDown;
                    PopupMenu menu = new PopupMenu(manager);
                    foreach (BarInfo info in this.info)
                    {
                        menu.ItemLinks.Add(info.CreateItem(manager, this.gIndex));
                    }
                    item.DropDownControl = menu;
                }
            }
            this.item.ItemClick += this.handler;
            this.item.Glyph = this.image;
            this.item.Hint = this.caption;
            return this.item;
        }

        public DevExpress.XtraBars.BarItem BarItem
        {
            get
            {
                return this.item;
            }
        }

        public bool BeginGroup
        {
            get
            {
                return this.fBeginGroup;
            }
        }

        public bool Enabled
        {
            get
            {
                return this.item.Enabled;
            }
            set
            {
                this.item.Enabled = value;
            }
        }

        public bool Pushed
        {
            get
            {
                BarCheckItem item = this.item as BarCheckItem;
                if (item == null)
                {
                    return false;
                }
                return item.Checked;
            }
            set
            {
                BarCheckItem item = this.item as BarCheckItem;
                if (item != null)
                {
                    item.Checked = value;
                }
            }
        }
    }
}

