namespace DevExpress.Tutorials
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    public class ColorUtils
    {
        public static Color GetGradientActiveCaptionColor()
        {
            int colorIndex = 0x1b;
            return ColorTranslator.FromWin32(GetSysColor(colorIndex));
        }

        [DllImport("user32.dll")]
        internal static extern int GetSysColor(int colorIndex);
    }
}

