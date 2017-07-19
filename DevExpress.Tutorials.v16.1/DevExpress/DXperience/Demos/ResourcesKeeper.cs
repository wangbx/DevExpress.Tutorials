namespace DevExpress.DXperience.Demos
{
    using System;
    using System.Collections.Generic;

    public static class ResourcesKeeper
    {
        private static Dictionary<object, string> titles = new Dictionary<object, string>();

        public static string GetTitle(object value)
        {
            if (titles.ContainsKey(value))
            {
                return titles[value];
            }
            return string.Format("{0}", value);
        }

        public static void RegisterTitle(object id, string value)
        {
            if (!titles.ContainsKey(id))
            {
                titles.Add(id, value);
            }
        }
    }
}

