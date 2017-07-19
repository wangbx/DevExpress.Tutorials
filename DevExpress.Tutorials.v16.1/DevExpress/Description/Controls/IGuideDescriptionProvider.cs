namespace DevExpress.Description.Controls
{
    using System;
    using System.Collections.Generic;

    public interface IGuideDescriptionProvider
    {
        void UpdateDescriptions(List<GuideControlDescription> templates);

        bool Enabled { get; }
    }
}

