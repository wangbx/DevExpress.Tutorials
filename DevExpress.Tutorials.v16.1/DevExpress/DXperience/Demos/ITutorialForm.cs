namespace DevExpress.DXperience.Demos
{
    using System;

    public interface ITutorialForm
    {
        void HideServiceElements();
        void ResetNavbarSelectedLink();
        void ShowDemoFilter();
        void ShowModule(string name);
        void ShowServiceElements();

        bool AllowDemoFilter { get; }

        bool IsDemoFilterVisible { get; }

        bool IsFullMode { get; }
    }
}

