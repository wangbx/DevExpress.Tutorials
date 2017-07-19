namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.XtraEditors;
    using Microsoft.CSharp;
    using Microsoft.VisualBasic;
    using System;
    using System.CodeDom;
    using System.CodeDom.Compiler;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ExampleCodeEvaluator
    {
        public ExampleCodeEvaluator(DevExpress.DXperience.Demos.CodeDemo.ExampleLanguage language)
        {
            this.ExampleLanguage = language;
        }

        protected internal bool CompileAndRun(CodeExample codeExample, XtraUserControl userControl)
        {
            CompilerResults results;
            userControl.Controls.Clear();
            CompilerParameters options = new CompilerParameters {
                GenerateInMemory = true,
                TreatWarningsAsErrors = false,
                GenerateExecutable = false
            };
            RuntimeHelpers.RunClassConstructor(codeExample.Parent.RootType.TypeHandle);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location))
                {
                    options.ReferencedAssemblies.Add(assembly.Location);
                }
            }
            CodeDomProvider codeDomProvider = this.GetCodeDomProvider();
            CodeSnippetCompileUnit unit = new CodeSnippetCompileUnit(CreateCode(codeExample, this.ExampleLanguage));
            try
            {
                results = codeDomProvider.CompileAssemblyFromDom(options, new CodeCompileUnit[] { unit });
            }
            catch (Exception exception)
            {
                XtraMessageBox.Show(exception.ToString(), "Compile errors");
                return false;
            }
            if (results.Errors.HasErrors)
            {
                string text = string.Empty;
                foreach (CompilerError error in results.Errors)
                {
                    text = text + Environment.NewLine + error;
                }
                XtraMessageBox.Show(text, "Compile errors");
                return false;
            }
            Module module = null;
            try
            {
                module = results.CompiledAssembly.GetModules()[0];
            }
            catch
            {
            }
            if (module == null)
            {
                return false;
            }
            object[] parameters = new object[0];
            Type type = module.GetType("DXSample.SampleClass");
            if (type == null)
            {
                return false;
            }
            if (codeExample.Parent.SetUp != null)
            {
                MethodInfo info = type.GetMethod(codeExample.Parent.SetUp.Name);
                if (info != null)
                {
                    try
                    {
                        parameters = info.Invoke(null, new object[] { userControl }) as object[];
                    }
                    catch (Exception exception2)
                    {
                        XtraMessageBox.Show(exception2.ToString(), "Compile errors");
                        return false;
                    }
                }
                if (codeExample.Parent.TearDown != null)
                {
                    codeExample.TearDown = type.GetMethod(codeExample.Parent.TearDown.Name);
                }
            }
            MethodInfo method = type.GetMethod("Process");
            if (method == null)
            {
                return false;
            }
            try
            {
                method.Invoke(null, parameters);
            }
            catch (Exception exception3)
            {
                userControl.Controls.Clear();
                string str3 = (exception3.InnerException == null) ? exception3.Message : exception3.InnerException.Message;
                XtraMessageBox.Show(str3, "Compile errors");
                return false;
            }
            return true;
        }

        public static string CreateCode(CodeExample codeExample, DevExpress.DXperience.Demos.CodeDemo.ExampleLanguage langage)
        {
            string str = CreateCodeCore(codeExample, langage);
            string newValue = codeExample.Parent.SetUpCode + Environment.NewLine + codeExample.Parent.TearDownCode;
            if ((codeExample.NestedTypes != null) && (codeExample.NestedTypes.Length != 0))
            {
                foreach (string str3 in CodeTutorialControlBase.GetListStringFromNestedTypes(codeExample, langage))
                {
                    newValue = newValue + str3 + Environment.NewLine;
                }
            }
            return str.Replace(StringContainer.NestedStringToReplace, newValue);
        }

        private static string CreateCodeCore(CodeExample codeExample, DevExpress.DXperience.Demos.CodeDemo.ExampleLanguage langage)
        {
            if (langage == DevExpress.DXperience.Demos.CodeDemo.ExampleLanguage.Csharp)
            {
                return (codeExample.BeginCSCode + codeExample.UserCodeCS + codeExample.EndCSCode);
            }
            return (codeExample.BeginVBCode + codeExample.UserCodeVB + codeExample.EndVBCode);
        }

        public bool ExecuteCodeAndGenerateLayout(CodeEvaluationEventArgs args)
        {
            return this.CompileAndRun(args.CodeExample, args.RootUserControl);
        }

        protected CodeDomProvider GetCodeDomProvider()
        {
            if (this.ExampleLanguage == DevExpress.DXperience.Demos.CodeDemo.ExampleLanguage.Csharp)
            {
                return new CSharpCodeProvider();
            }
            return new VBCodeProvider();
        }

        private DevExpress.DXperience.Demos.CodeDemo.ExampleLanguage ExampleLanguage { get; set; }
    }
}

