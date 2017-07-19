namespace DevExpress.Description.Controls
{
    using System;

    public interface IGuideForm
    {
        event EventHandler FormClosed;

        void Dispose();
        bool IsHandle(IntPtr intPtr);
        void Show(GuideControl owner, GuideControlDescription description);

        bool Visible { get; }
    }
}

