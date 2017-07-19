namespace DevExpress.DXperience.Demos.CodeDemo
{
    using DevExpress.DXperience.Demos;
    using DevExpress.Utils;
    using DevExpress.XtraEditors;
    using DevExpress.XtraLayout;
    using DevExpress.XtraRichEdit;
    using DevExpress.XtraSplashScreen;
    using DevExpress.XtraTreeList;
    using DevExpress.XtraTreeList.Columns;
    using DevExpress.XtraTreeList.Nodes;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    [ToolboxItem(false)]
    public class CodeExampleControlBase : TutorialControlBase
    {
        private SimpleLabelItem codeExampleName;
        protected CodeExampleURI[] CodeExamplesURLs;
        private TreeList codeTreeList;
        private LayoutControlItem codeTreeListLCI;
        private IContainer components;
        private LayoutControl layoutControl1;
        private LayoutControlGroup layoutControlGroup1;
        private LayoutControlItem layoutControlItem1;
        private LayoutControlItem layoutControlItem2;
        private string MSBuildPath;
        private string PathToExampleRunner;
        private RichEditControl richEditControlCS;
        private LayoutControlGroup richEditControlCSGroup;
        private LayoutControlItem richEditControlCSLCI;
        private RichEditControl richEditControlVB;
        private LayoutControlGroup richEditControlVBGroup;
        private LayoutControlItem richEditControlVBLCI;
        protected PanelControl rootContainer;
        private SimpleButton simpleButton1;
        private SplitterItem splitterItem1;
        private TabbedControlGroup tabbedControlGroup;
        private TreeListColumn treeListColumnDescription;
        private TreeListColumn treeListColumnName;
        private TreeListColumn treeListColumnUri;
        private WebClient webClient;

        public CodeExampleControlBase()
        {
            this.PathToExampleRunner = this.TryGetPathToExampleRunner();
            this.MSBuildPath = this.TryGetPathToMSBuild();
            if (this.PathToExampleRunner != string.Empty)
            {
                bool flag1 = this.MSBuildPath == string.Empty;
            }
            this.InitializeComponent();
            this.CodeExamplesURLs = this.FillCodeExamples();
            this.FillTreeListNode();
            this.webClient = new WebClient();
            this.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.OnDownloadExampleCompleted);
        }

        public CodeExampleControlBase(string Uri) : this()
        {
            this.CodeExamplesURLs = new CodeExampleURI[] { new CodeExampleURI(Uri, "Test", "Test") };
            this.FillTreeListNode();
        }

        private string CallExampleRunner(string pathToDxSample)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(this.PathToExampleRunner, string.Format("/silence {0} {1}", pathToDxSample, "16.1.8.0")) {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            Process process = Process.Start(startInfo);
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }

        private void CompileExample(string tempPath, string pathToSLN)
        {
            string arguments = string.Format("\"{0}\" /t:Build /p:OutDir={1}", pathToSLN, tempPath);
            ProcessStartInfo startInfo = new ProcessStartInfo(this.MSBuildPath, arguments) {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(startInfo).WaitForExit();
        }

        private void CreateInstanceAndAddToPanel(string tempPath)
        {
            string path = Directory.GetFiles(tempPath, "*.exe")[0];
            Assembly assembly = Assembly.LoadFile(path);
            System.Type[] exportedTypes = assembly.GetExportedTypes();
            Form form = assembly.CreateInstance(exportedTypes.First<System.Type>(q => typeof(Form).IsAssignableFrom(q)).FullName) as Form;
            SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 80);
            ArrayList list = new ArrayList(form.Controls);
            foreach (Control control in list)
            {
                control.Parent = this.rootContainer;
            }
            SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 100);
            form.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected virtual CodeExampleURI[] FillCodeExamples()
        {
            return new CodeExampleURI[0];
        }

        private void FillTreeListNode()
        {
            foreach (CodeExampleURI euri in this.CodeExamplesURLs)
            {
                this.codeTreeList.AppendNode(new object[] { euri.ExampleName, euri.Uri, euri.Description }, (TreeListNode) null);
            }
        }

        private string GetPathToSln(string pathToDxSample)
        {
            string path = this.CallExampleRunner(pathToDxSample);
            if (path == string.Empty)
            {
                return string.Empty;
            }
            FileInfo[] files = new DirectoryInfo(path).GetFiles("*.sln", SearchOption.AllDirectories);
            if (files.Count<FileInfo>() == 0)
            {
                return string.Empty;
            }
            return files[0].FullName;
        }

        private string GetRandomDirectory()
        {
            return Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Path.GetRandomFileName())).FullName;
        }

        private void InitializeComponent()
        {
            this.layoutControl1 = new LayoutControl();
            this.simpleButton1 = new SimpleButton();
            this.rootContainer = new PanelControl();
            this.codeTreeList = new TreeList();
            this.richEditControlVB = new RichEditControl();
            this.richEditControlCS = new RichEditControl();
            this.layoutControlGroup1 = new LayoutControlGroup();
            this.tabbedControlGroup = new TabbedControlGroup();
            this.richEditControlCSGroup = new LayoutControlGroup();
            this.richEditControlCSLCI = new LayoutControlItem();
            this.richEditControlVBGroup = new LayoutControlGroup();
            this.richEditControlVBLCI = new LayoutControlItem();
            this.codeTreeListLCI = new LayoutControlItem();
            this.codeExampleName = new SimpleLabelItem();
            this.splitterItem1 = new SplitterItem();
            this.layoutControlItem1 = new LayoutControlItem();
            this.layoutControlItem2 = new LayoutControlItem();
            this.treeListColumnName = new TreeListColumn();
            this.treeListColumnUri = new TreeListColumn();
            this.treeListColumnDescription = new TreeListColumn();
            this.layoutControl1.BeginInit();
            this.layoutControl1.SuspendLayout();
            this.rootContainer.BeginInit();
            this.codeTreeList.BeginInit();
            this.layoutControlGroup1.BeginInit();
            this.tabbedControlGroup.BeginInit();
            this.richEditControlCSGroup.BeginInit();
            this.richEditControlCSLCI.BeginInit();
            this.richEditControlVBGroup.BeginInit();
            this.richEditControlVBLCI.BeginInit();
            this.codeTreeListLCI.BeginInit();
            this.codeExampleName.BeginInit();
            this.splitterItem1.BeginInit();
            this.layoutControlItem1.BeginInit();
            this.layoutControlItem2.BeginInit();
            base.SuspendLayout();
            this.layoutControl1.AllowCustomization = false;
            this.layoutControl1.Controls.Add(this.simpleButton1);
            this.layoutControl1.Controls.Add(this.rootContainer);
            this.layoutControl1.Controls.Add(this.codeTreeList);
            this.layoutControl1.Controls.Add(this.richEditControlVB);
            this.layoutControl1.Controls.Add(this.richEditControlCS);
            this.layoutControl1.Dock = DockStyle.Fill;
            this.layoutControl1.Location = new Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(-1364, 0x30, 0x3bc, 0x395);
            this.layoutControl1.OptionsCustomizationForm.EnableUndoManager = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new Size(0x3bb, 510);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl2";
            this.simpleButton1.Location = new Point(0x13e, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new Size(0x271, 0x21);
            this.simpleButton1.StyleController = this.layoutControl1;
            this.simpleButton1.TabIndex = 20;
            this.simpleButton1.Text = "simpleButton1";
            this.simpleButton1.Click += new EventHandler(this.simpleButton1_Click);
            this.rootContainer.Location = new Point(12, 0xf2);
            this.rootContainer.Name = "rootContainer";
            this.rootContainer.Size = new Size(0x2d5, 0x100);
            this.rootContainer.TabIndex = 0x13;
            this.codeTreeList.Columns.AddRange(new TreeListColumn[] { this.treeListColumnName, this.treeListColumnUri, this.treeListColumnDescription });
            this.codeTreeList.Location = new Point(0x2e5, 0x31);
            this.codeTreeList.Name = "codeTreeList";
            this.codeTreeList.Size = new Size(0xca, 0x1c1);
            this.codeTreeList.TabIndex = 0x12;
            this.richEditControlVB.ActiveViewType = RichEditViewType.Draft;
            this.richEditControlVB.EnableToolTips = true;
            this.richEditControlVB.Location = new Point(0x18, 0x53);
            this.richEditControlVB.Name = "richEditControlVB";
            this.richEditControlVB.Options.HorizontalRuler.Visibility = RichEditRulerVisibility.Hidden;
            this.richEditControlVB.Size = new Size(0x2bd, 0x8a);
            this.richEditControlVB.TabIndex = 0x10;
            this.richEditControlCS.ActiveViewType = RichEditViewType.Draft;
            this.richEditControlCS.EnableToolTips = true;
            this.richEditControlCS.Location = new Point(0x18, 0x53);
            this.richEditControlCS.Name = "richEditControlCS";
            this.richEditControlCS.Options.HorizontalRuler.Visibility = RichEditRulerVisibility.Hidden;
            this.richEditControlCS.Size = new Size(0x2bd, 0x8a);
            this.richEditControlCS.TabIndex = 15;
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { this.tabbedControlGroup, this.codeTreeListLCI, this.codeExampleName, this.splitterItem1, this.layoutControlItem1, this.layoutControlItem2 });
            this.layoutControlGroup1.Location = new Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new Size(0x3bb, 510);
            this.layoutControlGroup1.TextVisible = false;
            this.tabbedControlGroup.Location = new Point(0, 0x25);
            this.tabbedControlGroup.MultiLine = DefaultBoolean.True;
            this.tabbedControlGroup.Name = "tabbedControlGroup";
            this.tabbedControlGroup.SelectedTabPage = this.richEditControlCSGroup;
            this.tabbedControlGroup.SelectedTabPageIndex = 0;
            this.tabbedControlGroup.Size = new Size(0x2d9, 0xbc);
            this.tabbedControlGroup.TabPages.AddRange(new BaseLayoutItem[] { this.richEditControlCSGroup, this.richEditControlVBGroup });
            this.richEditControlCSGroup.Items.AddRange(new BaseLayoutItem[] { this.richEditControlCSLCI });
            this.richEditControlCSGroup.Location = new Point(0, 0);
            this.richEditControlCSGroup.Name = "richEditControlCSGroup";
            this.richEditControlCSGroup.Size = new Size(0x2c1, 0x8e);
            this.richEditControlCSGroup.Text = "C#";
            this.richEditControlCSLCI.Control = this.richEditControlCS;
            this.richEditControlCSLCI.Location = new Point(0, 0);
            this.richEditControlCSLCI.Name = "richEditControlCSLCI";
            this.richEditControlCSLCI.Size = new Size(0x2c1, 0x8e);
            this.richEditControlCSLCI.TextSize = new Size(0, 0);
            this.richEditControlCSLCI.TextVisible = false;
            this.richEditControlVBGroup.Items.AddRange(new BaseLayoutItem[] { this.richEditControlVBLCI });
            this.richEditControlVBGroup.Location = new Point(0, 0);
            this.richEditControlVBGroup.Name = "richEditControlVBGroup";
            this.richEditControlVBGroup.Size = new Size(0x2c1, 0x8e);
            this.richEditControlVBGroup.Text = "VB";
            this.richEditControlVBLCI.Control = this.richEditControlVB;
            this.richEditControlVBLCI.Location = new Point(0, 0);
            this.richEditControlVBLCI.Name = "richEditControlVBLCI";
            this.richEditControlVBLCI.Size = new Size(0x2c1, 0x8e);
            this.richEditControlVBLCI.TextSize = new Size(0, 0);
            this.richEditControlVBLCI.TextVisible = false;
            this.codeTreeListLCI.Control = this.codeTreeList;
            this.codeTreeListLCI.Location = new Point(0x2d9, 0x25);
            this.codeTreeListLCI.Name = "codeTreeListLCI";
            this.codeTreeListLCI.Size = new Size(0xce, 0x1c5);
            this.codeTreeListLCI.TextSize = new Size(0, 0);
            this.codeTreeListLCI.TextVisible = false;
            this.codeExampleName.AllowHotTrack = false;
            this.codeExampleName.AppearanceItemCaption.Font = new Font("Tahoma", 20.25f);
            this.codeExampleName.AppearanceItemCaption.Options.UseFont = true;
            this.codeExampleName.Location = new Point(0, 0);
            this.codeExampleName.Name = "simpleLabelItem1";
            this.codeExampleName.Size = new Size(0x132, 0x25);
            this.codeExampleName.Text = "CodeExampleName";
            this.codeExampleName.TextSize = new Size(0xe5, 0x21);
            this.splitterItem1.AllowHotTrack = true;
            this.splitterItem1.Location = new Point(0, 0xe1);
            this.splitterItem1.Name = "splitterItem1";
            this.splitterItem1.Size = new Size(0x2d9, 5);
            this.layoutControlItem1.Control = this.rootContainer;
            this.layoutControlItem1.Location = new Point(0, 230);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new Size(0x2d9, 260);
            this.layoutControlItem1.TextSize = new Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            this.layoutControlItem2.Control = this.simpleButton1;
            this.layoutControlItem2.Location = new Point(0x132, 0);
            this.layoutControlItem2.MinSize = new Size(0x52, 0x1a);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new Size(0x275, 0x25);
            this.layoutControlItem2.SizeConstraintsType = SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            this.treeListColumnName.Caption = "Name";
            this.treeListColumnName.FieldName = "Name";
            this.treeListColumnName.Name = "treeListColumnName";
            this.treeListColumnName.Visible = true;
            this.treeListColumnName.VisibleIndex = 0;
            this.treeListColumnUri.Caption = "Uri";
            this.treeListColumnUri.FieldName = "Uri";
            this.treeListColumnUri.Name = "treeListColumnUri";
            this.treeListColumnDescription.Caption = "Description";
            this.treeListColumnDescription.FieldName = "Description";
            this.treeListColumnDescription.Name = "treeListColumnDescription";
            base.AutoScaleDimensions = new SizeF(6f, 13f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.layoutControl1);
            base.Name = "CodeTutorialControlBase";
            base.Size = new Size(0x3bb, 510);
            this.layoutControl1.EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.rootContainer.EndInit();
            this.codeTreeList.EndInit();
            this.layoutControlGroup1.EndInit();
            this.tabbedControlGroup.EndInit();
            this.richEditControlCSGroup.EndInit();
            this.richEditControlCSLCI.EndInit();
            this.richEditControlVBGroup.EndInit();
            this.richEditControlVBLCI.EndInit();
            this.codeTreeListLCI.EndInit();
            this.codeExampleName.EndInit();
            this.splitterItem1.EndInit();
            this.layoutControlItem1.EndInit();
            this.layoutControlItem2.EndInit();
            base.ResumeLayout(false);
        }

        private void OnDownloadExampleCompleted(object sender, AsyncCompletedEventArgs e)
        {
            SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 0x21);
            string randomDirectory = this.GetRandomDirectory();
            string pathToSln = this.GetPathToSln(Path.GetTempPath() + @"\dxSample.dxSample");
            if (pathToSln != string.Empty)
            {
                this.CompileExample(randomDirectory, pathToSln);
                SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 0x42);
                this.CreateInstanceAndAddToPanel(randomDirectory);
                SplashScreenManager.CloseDefaultSplashScreen();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.rootContainer.Controls.Clear();
            if (this.codeTreeList.FocusedNode != null)
            {
                this.StartDownloadExample(this.codeTreeList.FocusedNode);
            }
        }

        private void StartDownloadExample(TreeListNode focused)
        {
            Uri uri;
            string uriString = focused.GetValue(1) as string;
            if (Uri.TryCreate(uriString, UriKind.Absolute, out uri))
            {
                this.codeExampleName.Text = focused.GetValue(0) as string;
                string fileName = Path.GetTempPath() + @"\dxSample.dxSample";
                SplashScreenManager.ShowDefaultProgressSplashScreen("Download");
                SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 0);
                this.webClient.DownloadFileAsync(uri, fileName);
            }
        }

        private string TryGetPathToExampleRunner()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\DevExpress\CodeCentral Example Runner\DXCodeCentralExampleRunner.exe";
            if (System.IO.File.Exists(path))
            {
                return path;
            }
            path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\DevExpress\CodeCentral Example Runner\DXCodeCentralExampleRunner.exe";
            if (System.IO.File.Exists(path))
            {
                return path;
            }
            if (MessageBox.Show("Would you like to download DXCodeCentralExampleRunner?", "DXCodeCentralExampleRunner", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (WebClient client = new WebClient())
                {
                    SplashScreenManager.ShowDefaultProgressSplashScreen("Download DevExpress.ExampleRunner.Setup.msi");
                    SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 10);
                    FileInfo info = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "DevExpress.ExampleRunner.Setup.msi"));
                    SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 30);
                    client.DownloadFile("https://www.devexpress.com/Support/Center/Attachment/GetExampleRunner", info.FullName);
                    SplashScreenManager.SetDefaultProgressSplashScreenValue(false, 100);
                    SplashScreenManager.CloseDefaultSplashScreen();
                    Process.Start(info.FullName).WaitForExit();
                }
            }
            path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\DevExpress\CodeCentral Example Runner\DXCodeCentralExampleRunner.exe";
            if (System.IO.File.Exists(path))
            {
                return path;
            }
            path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\DevExpress\CodeCentral Example Runner\DXCodeCentralExampleRunner.exe";
            if (System.IO.File.Exists(path))
            {
                return path;
            }
            return "";
        }

        private string TryGetPathToMSBuild()
        {
            string path = RuntimeEnvironment.GetRuntimeDirectory() + "MSBuild.exe";
            if (System.IO.File.Exists(path))
            {
                return path;
            }
            return "";
        }
    }
}

