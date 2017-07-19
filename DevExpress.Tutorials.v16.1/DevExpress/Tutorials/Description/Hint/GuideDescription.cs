namespace DevExpress.Tutorials.Description.Hint
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class GuideDescription : UserControl, IComponentConnector
    {
        private bool _contentLoaded;

        public GuideDescription()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Hide();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/DevExpress.Tutorials.v16.1;component/description/hint/guidedescription.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 1)
            {
                ((Button) target).Click += new RoutedEventHandler(this.Button_Click);
            }
            else
            {
                this._contentLoaded = true;
            }
        }
    }
}

