namespace DevExpress.DXperience.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public static class EnumTitlesKeeper<T>
    {
        private static object nullValue;
        private static Dictionary<object, string> titles;

        static EnumTitlesKeeper()
        {
            EnumTitlesKeeper<T>.nullValue = new object();
            EnumTitlesKeeper<T>.titles = new Dictionary<object, string>();
        }

        public static List<TI> GetItemsList<TI>()
        {
            List<TI> list = new List<TI>();
            if (EnumTitlesKeeper<T>.titles != null)
            {
                ConstructorInfo constructor = typeof(TI).GetConstructor(new Type[] { typeof(T), typeof(string) });
                if (constructor == null)
                {
                    constructor = typeof(TI).GetConstructor(new Type[] { typeof(object), typeof(string) });
                }
                if (constructor == null)
                {
                    return list;
                }
                foreach (T local in EnumHelper.GetValues<T>())
                {
                    list.Add((TI) constructor.Invoke(new object[] { local, EnumTitlesKeeper<T>.GetTitle(local) }));
                }
            }
            return list;
        }

        public static string GetTitle(object value)
        {
            string str;
            if (value == null)
            {
                value = EnumTitlesKeeper<T>.nullValue;
            }
            if (!EnumTitlesKeeper<T>.titles.TryGetValue(value, out str))
            {
                str = value.ToString();
            }
            return str;
        }

        public static void RegisterTitle(object id, string value)
        {
            if (id == null)
            {
                id = EnumTitlesKeeper<T>.nullValue;
            }
            if (!EnumTitlesKeeper<T>.titles.ContainsKey(id))
            {
                EnumTitlesKeeper<T>.titles.Add(id, value);
            }
        }
    }
}

