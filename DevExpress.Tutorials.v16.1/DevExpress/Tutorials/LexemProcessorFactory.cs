namespace DevExpress.Tutorials
{
    using System;

    public class LexemProcessorFactory : FactoryBase
    {
        protected override void RegisterEntries()
        {
            base.RegisterEntry("CSharp", Type.GetType("DevExpress.Tutorials.LexemProcessorCSharp"));
            base.RegisterEntry("VB", Type.GetType("DevExpress.Tutorials.LexemProcessorVB"));
            base.RegisterEntry("BoldIfSharp", Type.GetType("DevExpress.Tutorials.LexemProcessorBoldIfSharp"));
        }
    }
}

