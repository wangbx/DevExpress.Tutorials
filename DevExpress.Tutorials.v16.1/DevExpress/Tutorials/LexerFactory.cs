namespace DevExpress.Tutorials
{
    using System;

    public class LexerFactory : FactoryBase
    {
        protected override Type[] GetConstructorParamTypes()
        {
            return new Type[] { Type.GetType("System.String") };
        }

        protected override void RegisterEntries()
        {
            base.RegisterEntry("Lines", Type.GetType("DevExpress.Tutorials.LexerLines"));
            base.RegisterEntry("LinesSpaces", Type.GetType("DevExpress.Tutorials.LexerLinesSpaces"));
            base.RegisterEntry("CSharp", Type.GetType("DevExpress.Tutorials.LexerCSharp"));
            base.RegisterEntry("VB", Type.GetType("DevExpress.Tutorials.LexerVB"));
        }
    }
}

