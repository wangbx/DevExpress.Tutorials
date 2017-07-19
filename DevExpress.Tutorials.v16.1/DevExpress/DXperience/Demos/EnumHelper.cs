namespace DevExpress.DXperience.Demos
{
    using System;

    public class EnumHelper
    {
        public static T[] GetValues<T>()
        {
            return (T[]) Enum.GetValues(typeof(T));
        }
    }
}

