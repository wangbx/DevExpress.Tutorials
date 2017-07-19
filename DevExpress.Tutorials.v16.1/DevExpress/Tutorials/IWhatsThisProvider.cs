namespace DevExpress.Tutorials
{
    using System;
    using System.Windows.Forms;

    public interface IWhatsThisProvider
    {
        UserControl CurrentModule { get; }

        ImageShaderBase CurrentShader { get; }

        bool HintVisible { get; set; }

        FormTutorialInfo TutorialInfo { get; }
    }
}

