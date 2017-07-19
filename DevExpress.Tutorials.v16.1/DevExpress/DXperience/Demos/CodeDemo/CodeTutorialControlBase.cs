namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.CodeParser;
    using DevExpress.CodeParser.CSharp;
    using DevExpress.CodeParser.VB;
    using DevExpress.DemoData.Model;
    using DevExpress.DXperience.Demos;
    using DevExpress.Utils;
    using DevExpress.Utils.Animation;
    using DevExpress.Utils.Frames;
    using DevExpress.XtraBars;
    using DevExpress.XtraEditors;
    using DevExpress.XtraEditors.Repository;
    using DevExpress.XtraLayout;
    using DevExpress.XtraLayout.Utils;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Columns;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using XtraEditors.Controls;

    [ToolboxItem(false)]
    public class CodeTutorialControlBase : TutorialControlBase
    {
        private ExampleCodeEditor codeEditor;
        private TreeList codeTreeList;
        private LayoutControlItem codeTreeListLCI;
        private RingWaitingIndicatorProperties compileLineWaitingIndicatorProperties;
        private IContainer components;
        private ExampleEvaluatorByTimer evaluator;
        public List<CodeExampleGroup> examples;
        protected internal IEnumerable<string> HighlightTokens;
        private LayoutControlItem itemForLayoutControl;
        private LayoutControl layoutControl1;
        private LayoutControl layoutControlForExampleCode;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem2;
        private ComponentResourceManager resources;
        private readonly List<RichEditUserControl> richControls;
        private readonly RichEditUserControl richEditUserControlForExampleCode;
        private LayoutControlGroup Root;
        protected XtraUserControl rootContainer;
        private LayoutControlItem rootContainerLCI;
        private SearchControl searchControl1;
        internal CodeExample SelectedExample;
        private SplitterItem splitterItem1;
        private SplitterItem splitterItem2;
        private static readonly string Tabulation = new string(' ', 8);
        private TransitionManager transitionManager1;
        private bool treeListRootNodeLoading = true;
        private SolidBrush updateSolidBrush;

        public CodeTutorialControlBase()
        {
            List<string> list = new List<string> { 
                "Random",
                "Thread",
                "EventHandler",
                "DefaultBoolean",
                "XtraMessageBox",
                "SimpleButton",
                "CheckEdit",
                "TextEdit",
                "MemoEdit",
                "PictureEdit",
                "DateEdit",
                "ImageListBoxItem",
                "ObservableCollection"
            };
            this.HighlightTokens = list;
            this.updateSolidBrush = new SolidBrush(Color.FromArgb(50, 0x7f, 0xdd, 0x86));
            base.CreateWaitDialog();
            this.InitializeComponent();
            this.resources = new ComponentResourceManager(typeof(CodeTutorialControlBase));
            this.richEditUserControlForExampleCode = new RichEditUserControl();
            this.richControls = new List<RichEditUserControl>();
            this.richControls.Add(this.richEditUserControlForExampleCode);
            Assembly callingAssembly = this.GetCallingAssembly();
            System.Type[] types = callingAssembly.GetTypes();
            this.examples = this.FillExamplesGroupFormType(types);
            this.FillCodeExamplesFromResourceFiles(callingAssembly, ref this.examples);
            this.ShowExamplesInTreeList(this.codeTreeList);
            this.codeEditor = new ExampleCodeEditor(this.richEditUserControlForExampleCode.richEditControl, this.CurrentExampleLanguage);
            this.evaluator = new ExampleEvaluatorByTimer();
            this.evaluator.QueryEvaluate += new CodeEvaluationEventHandler(this.OnExampleEvaluatorQueryEvaluate);
            this.evaluator.OnBeforeCompile += new EventHandler(this.evaluator_OnBeforeCompile);
            this.evaluator.OnAfterCompile += new OnAfterCompileEventHandler(this.evaluator_OnAfterCompile);
            this.ShowFirstExample();
            this.compileLineWaitingIndicatorProperties = this.InitializeAnimationForCustomCompile();
            this.HighlightTokens = this.HighlightTokens.Union<string>(this.InitializeHighlightTokens());
        }

        private bool CheckRichTextChanged()
        {
            object dataRecordByNode = this.codeTreeList.GetDataRecordByNode(this.codeTreeList.Selection[0]);
            CodeExample codeExampleFromDataRecordByNode = this.GetCodeExampleFromDataRecordByNode(dataRecordByNode);
            if (codeExampleFromDataRecordByNode != null)
            {
                if ((this.CurrentExampleLanguage == ExampleLanguage.Csharp) && (this.SelectedExample.UserCodeCS != this.richEditUserControlForExampleCode.richEditControl.Text))
                {
                    return true;
                }
                if ((this.CurrentExampleLanguage == ExampleLanguage.VB) && (this.SelectedExample.UserCodeVB != this.richEditUserControlForExampleCode.richEditControl.Text))
                {
                    return true;
                }
                if (codeExampleFromDataRecordByNode.NestedTypes != null)
                {
                    System.Type[] nestedTypes = codeExampleFromDataRecordByNode.NestedTypes;
                    for (int i = 0; i < nestedTypes.Length; i++)
                    {
                        Func<RichEditUserControl, bool> predicate = null;
                        System.Type type = nestedTypes[i];
                        NestedCodeContainer container = null;
                        if (this.SelectedExample.Parent.NestedClassStrings.TryGetValue(type, out container))
                        {
                            if (predicate == null)
                            {
                                predicate = rich => rich.CurrentNestedType == type;
                            }
                            string text = this.richControls.Where<RichEditUserControl>(predicate).First<RichEditUserControl>().richEditControl.Text;
                            if (container.UserCode != text)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void CleanUp(UserControl container)
        {
            Control[] array = new Control[container.Controls.Count];
            container.Controls.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                array[i].Dispose();
            }
            container.Controls.Clear();
        }

        private void codeTreeList_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            CodeExample dataRecordByNode = this.codeTreeList.GetDataRecordByNode(e.Node) as CodeExample;
            CodeExampleGroup group = this.codeTreeList.GetDataRecordByNode(e.Node) as CodeExampleGroup;
            if (((dataRecordByNode != null) && (dataRecordByNode.VersionID >= 0xa1)) || ((group != null) && (group.VersionID >= 0xa1)))
            {
                e.Graphics.FillRectangle(this.updateSolidBrush, e.Bounds);
            }
        }

        private static string CreateCodeWithDescription(string nestedClassCode, ExampleLanguage language, string description)
        {
            if (string.IsNullOrEmpty(description))
            {
                return nestedClassCode;
            }
            if (nestedClassCode.Contains(description))
            {
                return nestedClassCode;
            }
            string str = string.Empty;
            if (language == ExampleLanguage.Csharp)
            {
                return ((((str + "// " + Environment.NewLine) + "// " + description + Environment.NewLine) + "// " + Environment.NewLine) + nestedClassCode);
            }
            return ((((str + "'' " + Environment.NewLine) + "'' " + description + Environment.NewLine) + "'' " + Environment.NewLine) + nestedClassCode);
        }

        private void CreateLayoutForExample(CodeExample newExample)
        {
            this.layoutControlForExampleCode.BeginUpdate();
            this.layoutControlForExampleCode.Clear(true, false);
            if (newExample.Parent.HasNestedClassStrings && (newExample.NestedTypes != null))
            {
                TabbedControlGroup tabbedControlgroup = this.InitializeTabbedControlGroup();
                if (newExample.NestedTypes.Length > (this.richControls.Count - 1))
                {
                    this.CreateRichIfNeeded(newExample);
                }
                this.FillTabbedControlGroup(newExample, tabbedControlgroup);
                tabbedControlgroup.SelectedTabPageIndex = 0;
            }
            else
            {
                this.layoutControlForExampleCode.AddItem("", this.richControls[0]).TextVisible = false;
            }
            this.layoutControlForExampleCode.EndUpdate();
        }

        private string CreateProject(ExampleLanguage language)
        {
            string resultUserControlDesignerCode = string.Empty;
            string contents = (language == ExampleLanguage.Csharp) ? this.GetCSUserControlCode(this.SelectedExample, ref resultUserControlDesignerCode) : this.GetVBUserControlCode(this.SelectedExample, ref resultUserControlDesignerCode);
            string tempFileName = Path.GetTempFileName();
            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }
            if (!Directory.Exists(tempFileName))
            {
                Directory.CreateDirectory(tempFileName);
            }
            string str4 = (language == ExampleLanguage.Csharp) ? "cs" : "vb";
            WriteResourceToFile("DevExpress.Tutorials.CodeDemoBase.Template" + str4.ToUpper() + "Project.Form1." + str4, tempFileName + @"\Form1." + str4);
            WriteResourceToFile("DevExpress.Tutorials.CodeDemoBase.Template" + str4.ToUpper() + "Project.Program." + str4, tempFileName + @"\Program." + str4);
            string str5 = GetFormDesignerString(this.CurrentExampleLanguage).Replace("//UpperName", this.SelectedExample.UserControlName(false)).Replace("//LowerName", this.SelectedExample.UserControlName(true)).Replace("//NameSpace", "DxSample" + this.SelectedExample.Parent.NameSpace());
            File.WriteAllText(tempFileName + @"\Form1.Designer." + str4, str5);
            string pROJString = GetPROJString(this.CurrentExampleLanguage);
            string newValue = Guid.NewGuid().ToString().ToUpper();
            pROJString = pROJString.Replace("//Guid", newValue).Replace("//Reference", GetReferenceString(this.SelectedExample)).Replace("//UpperName", this.SelectedExample.UserControlName(false));
            File.WriteAllText(tempFileName + @"\DxSample.sln", this.GetSLNString(newValue, this.CurrentExampleLanguage));
            File.WriteAllText(tempFileName + @"\DxSample." + str4 + "proj", pROJString);
            File.WriteAllText(tempFileName + @"\" + this.SelectedExample.UserControlName(false) + ".Designer." + str4, resultUserControlDesignerCode);
            File.WriteAllText(tempFileName + @"\" + this.SelectedExample.UserControlName(false) + "." + str4, contents);
            return tempFileName;
        }

        private void CreateRichIfNeeded(CodeExample newExample)
        {
            for (int i = this.richControls.Count - 1; i < newExample.NestedTypes.Length; i++)
            {
                RichEditUserControl item = new RichEditUserControl();
                this.codeEditor.AddCodeEditor(item.richEditControl);
                this.richControls.Add(item);
                if (this.codeEditor.CurrentExampleLanguage == ExampleLanguage.Csharp)
                {
                    this.richControls[i + 1].richEditControl.InitializeDocument += new EventHandler(this.codeEditor.InitializeSyntaxHighlightForCs);
                }
                else
                {
                    this.richControls[i + 1].richEditControl.InitializeDocument += new EventHandler(this.codeEditor.InitializeSyntaxHighlightForVb);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void DoHide()
        {
            base.DoHide();
            if (base.ParentFormMain != null)
            {
                base.ParentFormMain.ShowInVisualStudio.ItemClick -= new ItemClickEventHandler(this.visualStudioSimpleButton_Click);
            }
        }

        protected override void DoShow()
        {
            base.DoShow();
            if (base.ParentFormMain != null)
            {
                this.SetExportBarItemAvailability(base.ParentFormMain.ShowInVisualStudio, true, true);
                base.ParentFormMain.ShowInVisualStudio.ItemClick += new ItemClickEventHandler(this.visualStudioSimpleButton_Click);
                if (base.ParentFormMain.ShowInVisualStudio.Glyph == null)
                {
                    base.ParentFormMain.ShowInVisualStudio.Glyph = this.resources.GetObject(string.Format("{0}.Glyph", (this.CurrentExampleLanguage == ExampleLanguage.Csharp) ? "cs" : "vb")) as Image;
                    base.ParentFormMain.ShowInVisualStudio.LargeGlyph = this.resources.GetObject(string.Format("{0}.LargeGlyph", (this.CurrentExampleLanguage == ExampleLanguage.Csharp) ? "cs" : "vb")) as Image;
                }
            }
        }

        private void evaluator_OnAfterCompile(object sender, OnAfterCompileEventArgs args)
        {
            this.codeEditor.AfterCompile(args.Result);
            this.transitionManager1.EndTransition();
        }

        private void evaluator_OnBeforeCompile(object sender, EventArgs e)
        {
            this.codeEditor.BeforeCompile();
        }

        private void FillCodeExamplesFromResourceFiles(Assembly callingAssembly, ref List<CodeExampleGroup> examples)
        {
            string[] manifestResourceNames = callingAssembly.GetManifestResourceNames();
            using (List<CodeExampleGroup>.Enumerator enumerator = examples.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    string str;
                    string str2;
                    Func<string, bool> predicate = null;
                    CodeExampleGroup codeExampleGroup = enumerator.Current;
                    try
                    {
                        if (predicate == null)
                        {
                            predicate = e => e.Contains(codeExampleGroup.FileName);
                        }
                        str = manifestResourceNames.First<string>(predicate);
                    }
                    catch
                    {
                        throw new ApplicationException(string.Format("Add {0} link.", codeExampleGroup.FileName));
                    }
                    using (Stream stream = callingAssembly.GetManifestResourceStream(str))
                    {
                        str2 = new StreamReader(stream).ReadToEnd();
                    }
                    if (this.CurrentExampleLanguage == ExampleLanguage.Csharp)
                    {
                        this.FillCSCodeExampleCore(codeExampleGroup, str2);
                    }
                    else
                    {
                        this.FillVBCodeExampleCore(codeExampleGroup, str2);
                    }
                }
            }
        }

        private List<CodeExample> FillCodeExamplesFromType(CodeExampleGroup group)
        {
            List<CodeExample> list = new List<CodeExample>();
            foreach (MethodInfo info in group.RootType.GetMethods())
            {
                object[] customAttributes = info.GetCustomAttributes(typeof(CodeExampleCase), false);
                if (customAttributes.Length == 1)
                {
                    CodeExample item = new CodeExample {
                        MethodInfo = info,
                        Name = (customAttributes[0] as CodeExampleCase).Category,
                        NestedTypes = (customAttributes[0] as CodeExampleCase).Types,
                        Parent = group
                    };
                    object[] objArray2 = info.GetCustomAttributes(typeof(CodeExampleGroupName), false);
                    if (objArray2.Length == 1)
                    {
                        item.GroupName = (objArray2[0] as CodeExampleGroupName).GroupName;
                    }
                    object[] objArray3 = info.GetCustomAttributes(typeof(CodeExampleHighlightTokens), false);
                    if (objArray3.Length == 1)
                    {
                        item.HighlightTokens = (objArray3[0] as CodeExampleHighlightTokens).Tokens;
                    }
                    object[] objArray4 = info.GetCustomAttributes(typeof(CodeExampleUnderlineTokens), false);
                    if (objArray4.Length == 1)
                    {
                        item.UnderlineTokens = (objArray4[0] as CodeExampleUnderlineTokens).Tokens;
                    }
                    object[] objArray5 = info.GetCustomAttributes(typeof(CodeExampleVersionID), false);
                    if (objArray5.Length == 1)
                    {
                        item.VersionID = (objArray5[0] as CodeExampleVersionID).VersionID;
                    }
                    list.Add(item);
                }
            }
            return list;
        }

        private void FillCSCodeExampleCore(CodeExampleGroup codeExampleGroup, string code)
        {
            CSharp30Parser parser = new CSharp30Parser();
            SourceFile file = parser.Parse(new SourceStringReader(code)) as SourceFile;
            string[] linesFromCodeString = GetLinesFromCodeString(code);
            Class class2 = file.FindChildByName(codeExampleGroup.RootType.Name) as Class;
            if (codeExampleGroup.SetUp != null)
            {
                LanguageElement element = class2.FindChildByName(codeExampleGroup.SetUp.Name);
                codeExampleGroup.SetUpCode = this.GetCodeFromLanguageElement(element, linesFromCodeString, true);
            }
            if (codeExampleGroup.TearDown != null)
            {
                LanguageElement element2 = class2.FindChildByName(codeExampleGroup.TearDown.Name);
                codeExampleGroup.TearDownCode = this.GetCodeFromLanguageElement(element2, linesFromCodeString, true);
            }
            foreach (System.Type type in codeExampleGroup.NestedTypes)
            {
                Class nestedClass = class2.FindChildByName(type.Name) as Class;
                this.FillNestedClassStrings(codeExampleGroup, type, linesFromCodeString, nestedClass);
            }
            foreach (CodeExample example in codeExampleGroup.Examples)
            {
                Method method = class2.FindChildByName(example.MethodInfo.Name) as Method;
                example.BeginCSCode = this.GetBeginCSCodeFromUsingList(file.UsingList, linesFromCodeString[method.StartLine - 1]);
                example.EndCSCode = string.Format("{0}{1}{0}{1}{0}{1}", "}", Environment.NewLine);
                example.CodeCS = example.UserCodeCS = this.GetCodeFromLanguageElement(method, linesFromCodeString, false);
            }
        }

        private List<CodeExampleGroup> FillExamplesGroupFormType(Type[] typesFromAssembly)
        {
            List<CodeExampleGroup> list = new List<CodeExampleGroup>();
            for (int i = 0; i < typesFromAssembly.Length; i++)
            {
                Type type = typesFromAssembly[i];
                object[] customAttributes = type.GetCustomAttributes(typeof(CodeExampleClass), false);
                if (customAttributes.Length == 1)
                {
                    string fileName = (customAttributes[0] as CodeExampleClass).FileName;
                    if ((!this.UseSameTutorialControlNameForGenerateExample || base.GetType().Name.Contains(fileName.Substring(0, fileName.Length - 3))) && (this.FileNamesForModule == null || this.FileNamesForModule.Length == 0 || this.FileNamesForModule.Contains(fileName)))
                    {
                        CodeExampleGroup codeExampleGroup = new CodeExampleGroup();
                        codeExampleGroup.RootType = type;
                        codeExampleGroup.FileName = fileName;
                        codeExampleGroup.Name = (customAttributes[0] as CodeExampleClass).Category;
                        codeExampleGroup.SetUp = (from e in type.GetMethods()
                                                  where e.GetCustomAttributes(typeof(CodeExampleSetUp), false).Any<object>()
                                                  select e).FirstOrDefault<MethodInfo>();
                        codeExampleGroup.TearDown = (from e in type.GetMethods()
                                                     where e.GetCustomAttributes(typeof(CodeExampleTearDown), false).Any<object>()
                                                     select e).FirstOrDefault<MethodInfo>();
                        codeExampleGroup.NestedTypes = (from e in type.GetNestedTypes()
                                                        where e.GetCustomAttributes(typeof(CodeExampleNestedClass), false).Any<object>()
                                                        select e).ToList<Type>();
                        codeExampleGroup.HighlightTokens = new List<string>();
                        CodeExampleGroup codeExampleGroup2 = codeExampleGroup;
                        codeExampleGroup2.HighlightTokens.AddRange(from e in codeExampleGroup2.NestedTypes
                                                                   select e.Name);
                        object[] customAttributes2 = type.GetCustomAttributes(typeof(CodeExampleHighlightTokens), false);
                        if (customAttributes2.Length == 1)
                        {
                            codeExampleGroup2.HighlightTokens.AddRange((customAttributes2[0] as CodeExampleHighlightTokens).Tokens);
                        }
                        object[] customAttributes3 = type.GetCustomAttributes(typeof(CodeExampleUnderlineTokens), false);
                        if (customAttributes3.Length == 1)
                        {
                            codeExampleGroup2.UnderlineTokens = (customAttributes3[0] as CodeExampleUnderlineTokens).Tokens;
                        }
                        object[] customAttributes4 = type.GetCustomAttributes(typeof(CodeExampleVersionID), false);
                        if (customAttributes4.Length == 1)
                        {
                            codeExampleGroup2.VersionID = (customAttributes4[0] as CodeExampleVersionID).VersionID;
                        }
                        codeExampleGroup2.Examples = this.FillCodeExamplesFromType(codeExampleGroup2);
                        list.Add(codeExampleGroup2);
                    }
                }
            }
            return list;
        }

        private void FillNestedClassStrings(CodeExampleGroup codeExampleGroup, System.Type nestedType, string[] linesOfCode, Class nestedClass)
        {
            if (codeExampleGroup.NestedClassStrings == null)
            {
                codeExampleGroup.NestedClassStrings = new Dictionary<System.Type, NestedCodeContainer>();
            }
            NestedCodeContainer container = new NestedCodeContainer(GetNestedClassCode(linesOfCode, nestedClass));
            codeExampleGroup.NestedClassStrings.Add(nestedType, container);
        }

        private void FillTabbedControlGroup(CodeExample newExample, TabbedControlGroup tabbedControlgroup)
        {
            List<string> listStringFromNestedTypes = GetListStringFromNestedTypes(newExample, this.CurrentExampleLanguage);
            for (int i = 0; i < newExample.NestedTypes.Length; i++)
            {
                LayoutControlGroup group = tabbedControlgroup.AddTabPage();
                group.AddItem("", this.richControls[i + 1]).TextVisible = false;
                group.Text = newExample.NestedTypes[i].Name + ((this.CurrentExampleLanguage == ExampleLanguage.Csharp) ? ".cs" : ".vb");
                this.richControls[i + 1].richEditControl.Text = listStringFromNestedTypes[i];
                this.richControls[i + 1].CurrentNestedType = newExample.NestedTypes[i];
            }
        }

        private static string FillUsingString(SortedList sortedList, ExampleLanguage language)
        {
            string str = string.Empty;
            foreach (object obj2 in sortedList.GetValueList())
            {
                if (language == ExampleLanguage.Csharp)
                {
                    str = str + string.Format("using {0};{1}", obj2, Environment.NewLine);
                }
                else
                {
                    str = str + string.Format("Imports {0}{1}", obj2, Environment.NewLine);
                }
            }
            return str;
        }

        private void FillVBCodeExampleCore(CodeExampleGroup codeExampleGroup, string code)
        {
            SourceFile file = LanguageUtils.Create(ParserLanguageID.Basic).CreateParser().ParseString(code) as SourceFile;
            string[] linesFromCodeString = GetLinesFromCodeString(code);
            Class class2 = file.FindChildByName(codeExampleGroup.RootType.Name) as Class;
            if (codeExampleGroup.SetUp != null)
            {
                LanguageElement element = class2.FindChildByName(codeExampleGroup.SetUp.Name);
                codeExampleGroup.SetUpCode = this.GetCodeFromLanguageElement(element, linesFromCodeString, true);
            }
            if (codeExampleGroup.TearDown != null)
            {
                LanguageElement element2 = class2.FindChildByName(codeExampleGroup.TearDown.Name);
                codeExampleGroup.TearDownCode = this.GetCodeFromLanguageElement(element2, linesFromCodeString, true);
            }
            foreach (System.Type type in codeExampleGroup.NestedTypes)
            {
                Class nestedClass = class2.FindChildByName(type.Name) as Class;
                this.FillNestedClassStrings(codeExampleGroup, type, linesFromCodeString, nestedClass);
            }
            foreach (CodeExample example in codeExampleGroup.Examples)
            {
                VBMethod method = class2.FindChildByName(example.MethodInfo.Name) as VBMethod;
                example.BeginVBCode = this.GetBeginVBCodeFromUsingList(file.UsingList, linesFromCodeString[method.StartLine - 1]);
                example.EndVBCode = string.Format("{0}{3}{1}{3}{2}{3}", new object[] { "End Sub", "End Class", "End Namespace", Environment.NewLine });
                example.CodeVB = example.UserCodeVB = this.GetCodeFromLanguageElement(method, linesFromCodeString, false);
            }
        }

        protected virtual Color GetAnimationLineColor()
        {
            if (((FrameHelper.IsDarkSkin(this.LookAndFeel) && (this.LookAndFeel.ActiveSkinName != "Dark Side")) && ((this.LookAndFeel.ActiveSkinName != "Office 2010 Black") && (this.LookAndFeel.ActiveSkinName != "Sharp"))) && (this.LookAndFeel.ActiveSkinName != "Office 2016 Dark"))
            {
                return Color.White;
            }
            return Color.Black;
        }

        private string GetBeginCSCodeFromUsingList(SortedList sortedList, string line)
        {
            string str = string.Empty;
            foreach (object obj2 in sortedList.GetValueList())
            {
                str = str + string.Format("using {0}; {1}", obj2, Environment.NewLine);
            }
            string str2 = Regex.Match(line, @"\(([^\)]*)\)").Value;
            return (str + string.Format("namespace DXSample {0} {1} public class SampleClass {0} {1} {3} {1} public static void Process{2} {0} {1}", new object[] { "{", Environment.NewLine, str2, StringContainer.NestedStringToReplace }));
        }

        private string GetBeginVBCodeFromUsingList(SortedList sortedList, string line)
        {
            string str = string.Empty;
            foreach (object obj2 in sortedList.GetValueList())
            {
                str = str + string.Format("Imports {0} {1}", obj2, Environment.NewLine);
            }
            string str2 = Regex.Match(line, @"\(([^\)]*)\)").Value;
            return (str + string.Format("Namespace DXSample {0} Public Class SampleClass {0} {2} {0} Public Shared Sub Process{1} {0}", Environment.NewLine, str2, StringContainer.NestedStringToReplace));
        }

        protected virtual Assembly GetCallingAssembly()
        {
            return base.GetType().Assembly;
        }

        private static string GetClassCode(SourceFile sourceFile, string[] lines, System.Type moduleType, string className)
        {
            Class class2 = sourceFile.FindChildByName(className) as Class;
            if (class2 == null)
            {
                return null;
            }
            string str = string.Empty;
            int tabCount = lines[(class2.StartLine - class2.AttributeCount) - 1].TakeWhile<char>(new Func<char, bool>(char.IsWhiteSpace)).Count<char>();
            for (int i = (class2.StartLine - class2.AttributeCount) - 1; i < class2.EndLine; i++)
            {
                str = str + NewLineWithoutStartTabulation(lines[i], tabCount);
            }
            return str;
        }

        private CodeExample GetCodeExampleFromDataRecordByNode(object dataRecord)
        {
            if (dataRecord is CodeExampleGroup)
            {
                return (dataRecord as CodeExampleGroup).Examples.First<CodeExample>();
            }
            if (dataRecord is GroupNode)
            {
                return ((dataRecord as GroupNode).Children[0] as CodeExample);
            }
            if (dataRecord is CodeExample)
            {
                return (dataRecord as CodeExample);
            }
            return null;
        }

        private string GetCodeFromLanguageElement(LanguageElement method, string[] linesOfCode, bool tearDownOrSetUp)
        {
            string str = string.Empty;
            for (int i = method.StartLine + (tearDownOrSetUp ? -1 : 0); i < (method.EndLine + (tearDownOrSetUp ? 0 : -1)); i++)
            {
                str = str + NewLineWithoutStartTabulation(linesOfCode[i], 8);
            }
            return str;
        }

        private static string GetCodeFromLanguageElement(ExampleLanguage language, Method method, string[] linesOfCode, bool replaceToThis = false, bool isTearDown = false, List<string> parameters = null)
        {
            if (method == null)
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            if (parameters == null)
            {
                string newValue = (language == ExampleLanguage.Csharp) ? "this" : "Me";
                for (int i = method.StartLine; i < (method.EndLine - (isTearDown ? 1 : 2)); i++)
                {
                    builder.Append(Tabulation);
                    if (replaceToThis)
                    {
                        builder.AppendLine(linesOfCode[i].Replace(method.Parameters[0].Name, newValue));
                    }
                    else
                    {
                        builder.AppendLine(linesOfCode[i]);
                    }
                }
            }
            else
            {
                for (int j = method.StartLine; j < (method.EndLine - 1); j++)
                {
                    if (method.Parameters.Count != parameters.Count)
                    {
                        throw new Exception(string.Format("Method: {0}. Wrong parameters count.", method.Name));
                    }
                    builder.Append(Tabulation);
                    string str2 = linesOfCode[j];
                    for (int k = 0; k < method.Parameters.Count; k++)
                    {
                        str2 = str2.Replace(method.Parameters[k].Name, parameters[k]);
                    }
                    builder.AppendLine(str2);
                }
            }
            return builder.ToString();
        }

        public string GetCSUserControlCode(CodeExample example, ref string resultUserControlDesignerCode)
        {
            resultUserControlDesignerCode = StringContainer.UserControlDesignerCSCode;
            string userControlCSCode = StringContainer.UserControlCSCode;
            string code = ExampleCodeEvaluator.CreateCode(example, this.CurrentExampleLanguage);
            if (code.Contains("using DevExpress.DXperience.Demos.CodeDemo; "))
            {
                code = code.Replace("using DevExpress.DXperience.Demos.CodeDemo; ", "");
            }
            CSharp30Parser parser = new CSharp30Parser();
            this.GetUserControlCodeCore(example, ref resultUserControlDesignerCode, ref userControlCSCode, code, parser);
            return userControlCSCode;
        }

        private static string GetElementCode(string[] linesOfCode, DocumentElement element, int tabCount = 8)
        {
            string str = string.Empty;
            int num = (element is Method) ? -1 : 0;
            int num2 = (element is Method) ? 0 : -1;
            for (int i = element.StartLine + num; i < (element.EndLine + num2); i++)
            {
                str = str + NewLineWithoutStartTabulation(linesOfCode[i], tabCount);
            }
            return str;
        }

        private static string GetFormDesignerString(ExampleLanguage langauge)
        {
            string str = string.Empty;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream((langauge == ExampleLanguage.Csharp) ? "DevExpress.Tutorials.CodeDemoBase.TemplateCSProject.Form1.Designer.cs" : "DevExpress.Tutorials.CodeDemoBase.TemplateVBProject.Form1.Designer.vb"))
            {
                stream.Position = 0L;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        private static string[] GetLinesFromCodeString(string code)
        {
            List<string> list = new List<string>();
            using (StringReader reader = new StringReader(code))
            {
                string str;
            Label_000D:
                str = reader.ReadLine();
                if (str != null)
                {
                    list.Add(str);
                    goto Label_000D;
                }
            }
            return list.ToArray();
        }

        internal static List<string> GetListStringFromNestedTypes(CodeExample newExample, ExampleLanguage language)
        {
            List<string> list = new List<string>();
            foreach (System.Type type in newExample.NestedTypes)
            {
                NestedCodeContainer container;
                if (newExample.Parent.NestedClassStrings.TryGetValue(type, out container))
                {
                    CodeExampleNestedClass class2 = type.GetCustomAttributes(typeof(CodeExampleNestedClass), false)[0] as CodeExampleNestedClass;
                    list.Add(CreateCodeWithDescription(container.UserCode, language, class2.Category));
                }
            }
            return list;
        }

        private static string GetMethodCode(SourceFile sourceFile, string[] lines, System.Type moduleType, string methodName)
        {
            Func<Method, bool> predicate = null;
            Class element = sourceFile.FindChildByName(moduleType.Name) as Class;
            if (element == null)
            {
                return null;
            }
            if (predicate == null)
            {
                predicate = m => m.Name == methodName;
            }
            Method method = element.AllMethods.OfType<Method>().FirstOrDefault<Method>(predicate);
            if (method != null)
            {
                return GetElementCode(lines, method, 8);
            }
            return GetElementCode(lines, element, 8);
        }

        public static string GetModuleClassCode(System.Type moduleType, string className)
        {
            string str = (from n in moduleType.Assembly.GetManifestResourceNames()
                where !string.IsNullOrEmpty(n) && (n.Length > 3)
                select n).FirstOrDefault<string>(n => n.Substring(0, n.Length - 3).EndsWith(moduleType.Name));
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            string moduleCode = GetModuleCode(moduleType.Assembly, str);
            if (string.IsNullOrEmpty(moduleCode))
            {
                return moduleCode;
            }
            ExampleLanguage language = str.EndsWith(".cs") ? ExampleLanguage.Csharp : ExampleLanguage.VB;
            using (SourceStringReader reader = new SourceStringReader(moduleCode))
            {
                SourceFile sourceFile = (language == ExampleLanguage.Csharp) ? (new CSharp30Parser().Parse(reader) as SourceFile) : (new VB90Parser().Parse(reader) as SourceFile);
                return GetClassCode(sourceFile, GetLinesFromCodeString(moduleCode), moduleType, className);
            }
        }

        private static string GetModuleCode(Assembly moduleAssembly, string fileNameInAssembly)
        {
            string str;
            using (Stream stream = moduleAssembly.GetManifestResourceStream(fileNameInAssembly))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        public static string GetModuleMethodCode(System.Type moduleType, string methodName)
        {
            string str = (from n in moduleType.Assembly.GetManifestResourceNames()
                where !string.IsNullOrEmpty(n) && (n.Length > 3)
                select n).FirstOrDefault<string>(n => n.Substring(0, n.Length - 3).EndsWith(moduleType.Name));
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            string moduleCode = GetModuleCode(moduleType.Assembly, str);
            if (string.IsNullOrEmpty(moduleCode))
            {
                return moduleCode;
            }
            ExampleLanguage language = str.EndsWith(".cs") ? ExampleLanguage.Csharp : ExampleLanguage.VB;
            using (SourceStringReader reader = new SourceStringReader(moduleCode))
            {
                SourceFile sourceFile = (language == ExampleLanguage.Csharp) ? (new CSharp30Parser().Parse(reader) as SourceFile) : (new VB90Parser().Parse(reader) as SourceFile);
                return GetMethodCode(sourceFile, GetLinesFromCodeString(moduleCode), moduleType, methodName);
            }
        }

        private static string GetNestedClassCode(string[] linesOfCode, Class nestedClass)
        {
            string str = string.Empty;
            for (int i = nestedClass.StartLine - nestedClass.AttributeCount; i < nestedClass.EndLine; i++)
            {
                str = str + NewLineWithoutStartTabulation(linesOfCode[i], 8);
            }
            return str;
        }

        private Color GetPageBackgroundColor()
        {
            if (((FrameHelper.IsDarkSkin(this.LookAndFeel) && (this.LookAndFeel.ActiveSkinName != "Dark Side")) && ((this.LookAndFeel.ActiveSkinName != "Office 2010 Black") && (this.LookAndFeel.ActiveSkinName != "Sharp"))) && (this.LookAndFeel.ActiveSkinName != "Office 2016 Dark"))
            {
                return Color.FromArgb(0x53, 0x53, 0x53);
            }
            return Color.WhiteSmoke;
        }

        private static string GetPROJString(ExampleLanguage langauge)
        {
            string str = string.Empty;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream((langauge == ExampleLanguage.Csharp) ? "DevExpress.Tutorials.CodeDemoBase.TemplateCSProject.DxSample.csproj" : "DevExpress.Tutorials.CodeDemoBase.TemplateVBProject.DxSample.vbproj"))
            {
                stream.Position = 0L;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    str = reader.ReadToEnd();
                }
            }
            return str;
        }

        public static string GetReferenceString(CodeExample codeExample)
        {
            string str = string.Empty;
            RuntimeHelpers.RunClassConstructor(codeExample.Parent.RootType.TypeHandle);
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if ((!assembly.IsDynamic && !string.IsNullOrEmpty(assembly.Location)) && assembly.GlobalAssemblyCache)
                {
                    str = str + string.Format("<Reference Include=\"{0}\"/>{1}", assembly.GetName().Name, Environment.NewLine);
                }
            }
            return str;
        }

        private string GetSLNString(string guid, ExampleLanguage language)
        {
            return StringContainer.SLNString.Replace("//GUIDCSproj", guid).Replace("//GUIDSLN", Guid.NewGuid().ToString().ToUpper()).Replace("#csprojOrvbprog", (language == ExampleLanguage.Csharp) ? "csproj" : "vbproj");
        }

        private void GetUserControlCodeCore(CodeExample example, ref string resultUserControlDesignerCode, ref string resultUserControlCode, string code, FormattingParserBase parser)
        {
            SourceFile file = parser.ParseString(code) as SourceFile;
            string[] linesFromCodeString = GetLinesFromCodeString(code);
            Method method = (example.Parent.SetUp != null) ? (file.FindChildByName(example.Parent.SetUp.Name) as Method) : null;
            Method method2 = (example.Parent.TearDown != null) ? (file.FindChildByName(example.Parent.TearDown.Name) as Method) : null;
            resultUserControlCode = resultUserControlCode.Replace(StringContainer.UserControlName, example.UserControlName(false));
            resultUserControlDesignerCode = resultUserControlDesignerCode.Replace(StringContainer.UserControlName, example.UserControlName(false));
            resultUserControlDesignerCode = resultUserControlDesignerCode.Replace(StringContainer.NameSpace, "DxSample" + example.Parent.NameSpace());
            resultUserControlCode = resultUserControlCode.Replace(StringContainer.Usings, FillUsingString(file.UsingList, this.CurrentExampleLanguage));
            resultUserControlCode = resultUserControlCode.Replace(StringContainer.SetUp, GetCodeFromLanguageElement(this.CurrentExampleLanguage, method, linesFromCodeString, true, false, null));
            resultUserControlCode = resultUserControlCode.Replace(StringContainer.TearDown, GetCodeFromLanguageElement(this.CurrentExampleLanguage, method2, linesFromCodeString, true, true, null));
            resultUserControlCode = resultUserControlCode.Replace(StringContainer.RegionExampleCodeName, example.RegionName());
            resultUserControlCode = resultUserControlCode.Replace(StringContainer.NameSpace, "DxSample" + example.Parent.NameSpace());
            ArrayCreateExpression expression = (method.LastNode as Return).Expression as ArrayCreateExpression;
            List<string> parameters = new List<string>();
            foreach (Expression expression2 in expression.Initializer.Initializers)
            {
                parameters.Add(expression2.Name);
            }
            Method method3 = file.FindChildByName("Process") as Method;
            resultUserControlCode = resultUserControlCode.Replace(StringContainer.ExampleMethod, GetCodeFromLanguageElement(this.CurrentExampleLanguage, method3, linesFromCodeString, true, false, parameters));
            if ((example.NestedTypes != null) && (example.NestedTypes.Length != 0))
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str in GetListStringFromNestedTypes(example, this.CurrentExampleLanguage))
                {
                    foreach (string str2 in GetLinesFromCodeString(str))
                    {
                        builder.Append(new string(' ', 8));
                        builder.AppendLine(str2);
                    }
                }
                resultUserControlCode = resultUserControlCode.Replace(StringContainer.NestedClasses, builder.ToString());
            }
            else
            {
                resultUserControlCode = resultUserControlCode.Replace(StringContainer.NestedClasses, "");
            }
        }

        public string GetVBUserControlCode(CodeExample example, ref string resultUserControlDesignerCode)
        {
            resultUserControlDesignerCode = StringContainer.UserControlDesignerVBCode;
            string userControlVBCode = StringContainer.UserControlVBCode;
            string code = ExampleCodeEvaluator.CreateCode(example, this.CurrentExampleLanguage);
            if (code.Contains("Imports DevExpress.DXperience.Demos.CodeDemo"))
            {
                code = code.Replace("Imports DevExpress.DXperience.Demos.CodeDemo", "");
            }
            VB90Parser parser = new VB90Parser();
            this.GetUserControlCodeCore(example, ref resultUserControlDesignerCode, ref userControlVBCode, code, parser);
            return userControlVBCode;
        }

        protected virtual RingWaitingIndicatorProperties InitializeAnimationForCustomCompile()
        {
            RingWaitingIndicatorProperties properties = new RingWaitingIndicatorProperties {
                ContentAlignment = ContentAlignment.MiddleCenter,
                AllowBackground = false,
                AnimationSpeed = 6f
            };
            properties.RingAnimationDiameter += 30;
            properties.AnimationElementCount = 5;
            return properties;
        }

        private void InitializeCodeEvaluationEventArgs(CodeEvaluationEventArgs e)
        {
            try
            {
                this.InitializeCodeEvaluationEventArgsCore(e);
            }
            catch
            {
                e.Result = false;
            }
        }

        private void InitializeCodeEvaluationEventArgsCore(CodeEvaluationEventArgs e)
        {
            object dataRecordByNode = this.codeTreeList.GetDataRecordByNode(this.codeTreeList.Selection[0]);
            CodeExample codeExampleFromDataRecordByNode = this.GetCodeExampleFromDataRecordByNode(dataRecordByNode);
            e.CodeExample = codeExampleFromDataRecordByNode;
            if (this.CurrentExampleLanguage == ExampleLanguage.Csharp)
            {
                e.CodeExample.UserCodeCS = this.richEditUserControlForExampleCode.richEditControl.Text;
            }
            else
            {
                e.CodeExample.UserCodeVB = this.richEditUserControlForExampleCode.richEditControl.Text;
            }
            if (e.CodeExample.NestedTypes != null)
            {
                System.Type[] nestedTypes = e.CodeExample.NestedTypes;
                for (int i = 0; i < nestedTypes.Length; i++)
                {
                    Func<RichEditUserControl, bool> predicate = null;
                    System.Type type = nestedTypes[i];
                    NestedCodeContainer container = null;
                    if (e.CodeExample.Parent.NestedClassStrings.TryGetValue(type, out container))
                    {
                        if (predicate == null)
                        {
                            predicate = rich => rich.CurrentNestedType == type;
                        }
                        string text = this.richControls.Where<RichEditUserControl>(predicate).First<RichEditUserControl>().richEditControl.Text;
                        container.UserCode = text;
                    }
                }
            }
            e.RootUserControl = this.rootContainer;
            e.Language = this.CurrentExampleLanguage;
            e.Result = true;
        }

        private void InitializeComponent()
        {
            Transition item = new Transition();
            SlideFadeTransition transition2 = new SlideFadeTransition();
            this.rootContainer = new XtraUserControl();
            this.layoutControl1 = new LayoutControl();
            this.searchControl1 = new SearchControl();
            this.codeTreeList = new TreeList();
            this.layoutControlForExampleCode = new LayoutControl();
            this.Root = new LayoutControlGroup();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.codeTreeListLCI = new LayoutControlItem();
            this.splitterItem1 = new SplitterItem();
            this.rootContainerLCI = new LayoutControlItem();
            this.splitterItem2 = new SplitterItem();
            this.itemForLayoutControl = new LayoutControlItem();
            this.layoutControlItem2 = new LayoutControlItem();
            this.transitionManager1 = new TransitionManager();
            this.layoutControl1.BeginInit();
            this.layoutControl1.SuspendLayout();
            this.searchControl1.Properties.BeginInit();
            this.codeTreeList.BeginInit();
            this.layoutControlForExampleCode.BeginInit();
            this.Root.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.codeTreeListLCI.BeginInit();
            this.splitterItem1.BeginInit();
            this.rootContainerLCI.BeginInit();
            this.splitterItem2.BeginInit();
            this.itemForLayoutControl.BeginInit();
            this.layoutControlItem2.BeginInit();
            base.SuspendLayout();
            this.rootContainer.Location = new Point(0x13e, 12);
            this.rootContainer.Name = "rootContainer";
            this.rootContainer.Size = new Size(0x271, 0x115);
            this.rootContainer.TabIndex = 0x13;
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.searchControl1);
            this.layoutControl1.Controls.Add(this.layoutControlForExampleCode);
            this.layoutControl1.Controls.Add(this.rootContainer);
            this.layoutControl1.Controls.Add(this.codeTreeList);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(0x9c, 0x44, 0x3bc, 0x395);
            this.layoutControl1.OptionsCustomizationForm.EnableUndoManager = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(0x3bb, 0x24b);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl2";
            this.searchControl1.Client = this.codeTreeList;
            this.searchControl1.Location = new Point(12, 12);
            this.searchControl1.Name = "searchControl1";
            this.searchControl1.Properties.Buttons.AddRange(new EditorButton[] { new ClearButton(), new SearchButton() });
            this.searchControl1.Properties.Client = this.codeTreeList;
            this.searchControl1.Properties.NullValuePrompt = "Enter text to search...";
            this.searchControl1.Size = new Size(0x129, 20);
            this.searchControl1.StyleController = this.layoutControl1;
            this.searchControl1.TabIndex = 0x15;
            this.codeTreeList.Cursor = Cursors.Default;
            this.codeTreeList.Location = new Point(12, 0x24);
            this.codeTreeList.Name = "codeTreeList";
            this.codeTreeList.OptionsBehavior.EnableFiltering = true;
            this.codeTreeList.OptionsBehavior.ExpandNodesOnFiltering = true;
            this.codeTreeList.OptionsFilter.FilterMode = FilterMode.Extended;
            this.codeTreeList.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.codeTreeList.OptionsView.AllowGlyphSkinning = true;
            this.codeTreeList.OptionsView.AnimationType = TreeListAnimationType.AnimateFocusedNode;
            this.codeTreeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.None;
            this.codeTreeList.OptionsView.ShowHorzLines = false;
            this.codeTreeList.OptionsView.ShowIndicator = false;
            this.codeTreeList.OptionsView.ShowVertLines = false;
            this.codeTreeList.Size = new Size(0x129, 0xfd);
            this.codeTreeList.TabIndex = 0x12;
            this.codeTreeList.TreeLineStyle = LineStyle.None;
            this.codeTreeList.CustomDrawNodeCell += new CustomDrawNodeCellEventHandler(this.codeTreeList_CustomDrawNodeCell);
            this.layoutControlForExampleCode.AllowCustomization = false;
            this.layoutControlForExampleCode.Location = new Point(12, 0x12a);
            this.layoutControlForExampleCode.Name = "layoutControlForExampleCode";
            this.layoutControlForExampleCode.Root = this.Root;
            this.layoutControlForExampleCode.Size = new Size(0x3a3, 0x115);
            this.layoutControlForExampleCode.TabIndex = 20;
            this.layoutControlForExampleCode.Text = "layoutControl2";
            this.Root.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Location = new Point(0, 0);
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new Size(0x3a3, 0x115);
            this.Root.TextVisible = false;
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { this.codeTreeListLCI, this.splitterItem1, this.rootContainerLCI, this.splitterItem2, this.itemForLayoutControl, this.layoutControlItem2 });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new Size(0x3bb, 0x24b);
            this.layoutControlGroup1.TextVisible = false;
            this.codeTreeListLCI.Control = this.codeTreeList;
            this.codeTreeListLCI.Location = new Point(0, 0x18);
            this.codeTreeListLCI.Name = "codeTreeListLCI";
            this.codeTreeListLCI.Size = new Size(0x12d, 0x101);
            this.codeTreeListLCI.TextSize = new Size(0, 0);
            this.codeTreeListLCI.TextVisible = false;
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new Point(0, 0x119);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new Size(0x3a7, 5);
            this.rootContainerLCI.Control = this.rootContainer;
            this.rootContainerLCI.Location = new Point(0x132, 0);
            this.rootContainerLCI.MinSize = new Size(100, 100);
            this.rootContainerLCI.Name = "rootContainerLCI";
            this.rootContainerLCI.Size = new Size(0x275, 0x119);
            this.rootContainerLCI.SizeConstraintsType = SizeConstraintsType.Custom;
            this.rootContainerLCI.TextSize = new Size(0, 0);
            this.rootContainerLCI.TextVisible = false;
            this.splitterItem2.AllowHotTrack = true;
            this.splitterItem2.Location = new Point(0x12d, 0);
            this.splitterItem2.Name = "splitterItem2";
            this.splitterItem2.Size = new Size(5, 0x119);
            this.itemForLayoutControl.Control = this.layoutControlForExampleCode;
            this.itemForLayoutControl.Location = new Point(0, 0x11e);
            this.itemForLayoutControl.Name = "itemForLayoutControl";
            this.itemForLayoutControl.Size = new Size(0x3a7, 0x119);
            this.itemForLayoutControl.TextSize = new Size(0, 0);
            this.itemForLayoutControl.TextVisible = false;
            this.layoutControlItem2.Control = this.searchControl1;
            this.layoutControlItem2.Location = new Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(0x12d, 0x18);
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            this.transitionManager1.FrameInterval = 0xbb8;
            this.transitionManager1.ShowWaitingIndicator = false;
            item.Control = this.rootContainer;
            item.LineWaitingIndicatorProperties.AnimationElementCount = 5;
            item.LineWaitingIndicatorProperties.Caption = "";
            item.LineWaitingIndicatorProperties.Description = "";
            item.RingWaitingIndicatorProperties.AnimationElementCount = 5;
            item.RingWaitingIndicatorProperties.Caption = "";
            item.RingWaitingIndicatorProperties.Description = "";
            transition2.Parameters.Background = Color.Empty;
            item.TransitionType = transition2;
            item.WaitingIndicatorProperties.Caption = "";
            item.WaitingIndicatorProperties.Description = "";
            this.transitionManager1.Transitions.Add(item);
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.layoutControl1);
            base.Name = "CodeTutorialControlBase";
            base.Size = new Size(0x3bb, 0x24b);
            this.layoutControl1.EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.searchControl1.Properties.EndInit();
            this.codeTreeList.EndInit();
            this.layoutControlForExampleCode.EndInit();
            this.Root.EndInit();
            this.layoutControlGroup1.EndInit();
            this.codeTreeListLCI.EndInit();
            this.splitterItem1.EndInit();
            this.rootContainerLCI.EndInit();
            this.splitterItem2.EndInit();
            this.itemForLayoutControl.EndInit();
            this.layoutControlItem2.EndInit();
            base.ResumeLayout(false);
        }

        protected virtual List<string> InitializeHighlightTokens()
        {
            return new List<string>();
        }

        private TabbedControlGroup InitializeTabbedControlGroup()
        {
            TabbedControlGroup group = this.layoutControlForExampleCode.Root.AddTabbedGroup();
            LayoutControlGroup group2 = group.AddTabPage();
            group2.Text = "ExampleCode";
            group2.AddItem("", this.richControls[0]).TextVisible = false;
            return group;
        }

        private void InvokeTearDown(CodeExample oldExample)
        {
            try
            {
                if (oldExample.TearDown != null)
                {
                    oldExample.TearDown.Invoke(null, new object[] { this.rootContainer });
                }
                if (this.rootContainer != null)
                {
                    this.CleanUp(this.rootContainer);
                }
            }
            catch
            {
            }
        }

        private static string NewLineWithoutStartTabulation(string lineOfCode, int tabCount = 8)
        {
            if (lineOfCode.Length > 8)
            {
                return (lineOfCode + Environment.NewLine).Substring(tabCount);
            }
            return (lineOfCode + Environment.NewLine);
        }

        private void OnExampleEvaluatorQueryEvaluate(object sender, CodeEvaluationEventArgs e)
        {
            e.Result = false;
            if (this.CheckRichTextChanged())
            {
                TimeSpan span = (TimeSpan) (DateTime.Now - this.codeEditor.LastExampleCodeModifiedTime);
                if (span < TimeSpan.FromMilliseconds(1000.0))
                {
                    this.codeEditor.ResetLastExampleModifiedTime();
                }
                else
                {
                    this.InitializeCodeEvaluationEventArgs(e);
                    this.ShowUserCustomCodeCompileAnimation();
                    this.transitionManager1.StartTransition(this.rootContainer);
                    this.InvokeTearDown(e.CodeExample);
                }
            }
        }

        private void OnNewExampleSelected(object sender, FocusedNodeChangedEventArgs e)
        {
            object dataRecordByNode = (sender as TreeList).GetDataRecordByNode(e.Node);
            object dataRecord = (sender as TreeList).GetDataRecordByNode(e.OldNode);
            CodeExample oldExample = null;
            CodeExample newExample = null;
            newExample = this.GetCodeExampleFromDataRecordByNode(dataRecordByNode);
            oldExample = this.GetCodeExampleFromDataRecordByNode(dataRecord);
            if (newExample != null)
            {
                this.transitionManager1.StartTransition(this.rootContainer);
                if (oldExample != null)
                {
                    this.InvokeTearDown(oldExample);
                }
                this.SelectedExample = newExample;
                this.CreateLayoutForExample(newExample);
                this.codeEditor.ShowExample(newExample);
                CodeEvaluationEventArgs args = new CodeEvaluationEventArgs();
                this.InitializeCodeEvaluationEventArgs(args);
                this.evaluator.ForceCompile(args);
            }
        }

        private void OnVirtualTreeGetCellValue(object sender, VirtualTreeGetCellValueInfo args)
        {
            CodeExampleGroup group = args.Node as CodeExampleGroup;
            if (group != null)
            {
                args.CellData = group.Name;
            }
            CodeExample example = args.Node as CodeExample;
            if (example != null)
            {
                args.CellData = example.Name;
            }
            GroupNode node = args.Node as GroupNode;
            if (node != null)
            {
                args.CellData = node.Name;
            }
        }

        private void OnVirtualTreeGetChildNodes(object sender, VirtualTreeGetChildNodesInfo args)
        {
            if (this.treeListRootNodeLoading)
            {
                args.Children = (from e in this.examples
                                 where e.Examples.Any<CodeExample>()
                                 select e).ToArray<CodeExampleGroup>();
                this.treeListRootNodeLoading = false;
                return;
            }
            if (args.Node == null)
            {
                return;
            }
            CodeExampleGroup codeExampleGroup = args.Node as CodeExampleGroup;
            if (codeExampleGroup != null)
            {
                List<object> list = new List<object>();
                List<IGrouping<string, CodeExample>> list2 = (from x in codeExampleGroup.Examples
                                                              select x into x
                                                              where x.GroupName != null
                                                              group x by x.GroupName).ToList<IGrouping<string, CodeExample>>();
                List<CodeExample> collection = (from x in codeExampleGroup.Examples
                                                select x into x
                                                where x.GroupName == null
                                                select x).ToList<CodeExample>();
                list.AddRange(collection);
                if (list2.Count > 0)
                {
                    List<GroupNode> collection2 = (from x in list2
                                                   select new GroupNode(x.Key, x.ToList<CodeExample>())).ToList<GroupNode>();
                    list.AddRange(collection2);
                }
                args.Children = list;
            }
            GroupNode groupNode = args.Node as GroupNode;
            if (groupNode != null)
            {
                args.Children = groupNode.Children;
            }
        }

        private void ShowExamplesInTreeList(TreeList treeList)
        {
            treeList.OptionsPrint.UsePrintStyles = true;
            treeList.FocusedNodeChanged += new FocusedNodeChangedEventHandler(this.OnNewExampleSelected);
            treeList.OptionsView.ShowColumns = false;
            treeList.OptionsView.ShowIndicator = false;
            treeList.VirtualTreeGetChildNodes += new VirtualTreeGetChildNodesEventHandler(this.OnVirtualTreeGetChildNodes);
            treeList.VirtualTreeGetCellValue += new VirtualTreeGetCellValueEventHandler(this.OnVirtualTreeGetCellValue);
            TreeListColumn column = new TreeListColumn {
                VisibleIndex = 0,
                FieldName = "Empty",
                OptionsColumn = { 
                    AllowEdit = false,
                    AllowMove = false,
                    ReadOnly = true
                }
            };
            treeList.Columns.AddRange(new TreeListColumn[] { column });
            treeList.DataSource = new object();
            treeList.ExpandAll();
        }

        private void ShowFirstExample()
        {
            this.codeTreeList.ExpandAll();
            if (this.codeTreeList.Nodes.Count > 0)
            {
                this.codeTreeList.FocusedNode = this.codeTreeList.MoveFirst().FirstNode;
            }
        }

        private void ShowUserCustomCodeCompileAnimation()
        {
            this.compileLineWaitingIndicatorProperties.Appearance.ForeColor = this.GetAnimationLineColor();
            this.transitionManager1.StartWaitingIndicator(this.richEditUserControlForExampleCode, this.compileLineWaitingIndicatorProperties);
            foreach (RichEditUserControl control in this.richControls)
            {
                control.richEditControl.Document.SetPageBackground(this.GetPageBackgroundColor(), true);
            }
            Application.DoEvents();
            Thread.Sleep(0x7d0);
            foreach (RichEditUserControl control2 in this.richControls)
            {
                control2.richEditControl.Document.SetPageBackground(DXColor.Empty, true);
            }
            this.transitionManager1.StopWaitingIndicator();
        }

        private void visualStudioSimpleButton_Click(object sender, ItemClickEventArgs e)
        {
            if (this.SelectedExample != null)
            {
                string str = this.CreateProject(this.CurrentExampleLanguage);
                try
                {
                    string str2 = (this.CurrentExampleLanguage == ExampleLanguage.Csharp) ? "cs" : "vb";
                    DemoRunner.RunProject(str + @"\DxSample.sln", new string[] { this.SelectedExample.UserControlName(false) + "." + str2 });
                }
                catch
                {
                }
            }
        }

        private static void WriteResourceToFile(string resourceName, string fileName)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (FileStream stream2 = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                {
                    stream.CopyTo(stream2);
                }
            }
        }

        protected virtual ExampleLanguage CurrentExampleLanguage
        {
            get
            {
                return ExampleLanguage.Csharp;
            }
        }

        protected virtual string[] FileNamesForModule
        {
            get
            {
                return new string[0];
            }
        }

        protected virtual bool UseSameTutorialControlNameForGenerateExample
        {
            get
            {
                return false;
            }
        }
    }
}

