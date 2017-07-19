namespace DevExpress.DXperience.Demos
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class ModuleInfo
    {
        private string fAboutFile;
        private string fCodeFile;
        private string fDescription;
        private string fGroup;
        private Image fIcon;
        private Control fModule;
        private string fName;
        private string fTypeName;
        private string fUri;
        private string fXmlFile;
        private Assembly moduleAssembly;
        private int priority;
        private bool wasShown;

        public ModuleInfo(ModuleInfo info) : this(info.fName, info.fTypeName, info.fDescription, info.fIcon, info.fGroup, info.fCodeFile, info.fXmlFile, info.fAboutFile, info.Uri)
        {
        }

        public ModuleInfo(ModuleInfo info, Assembly moduleAssembly) : this(info)
        {
            this.moduleAssembly = moduleAssembly;
        }

        public ModuleInfo(string fName, string fTypeName) : this(fName, fTypeName, "")
        {
        }

        public ModuleInfo(string fName, string fTypeName, string fDescription) : this(fName, fTypeName, fDescription, null)
        {
        }

        public ModuleInfo(string fName, string fTypeName, string fDescription, Image fIcon) : this(fName, fTypeName, fDescription, fIcon, "")
        {
        }

        public ModuleInfo(string fName, string fTypeName, string fDescription, Image fIcon, string fGroup) : this(fName, fTypeName, fDescription, fIcon, fGroup, "", "")
        {
        }

        public ModuleInfo(string fName, string fTypeName, string fDescription, Image fIcon, string fCodeFile, string fXmlFile) : this(fName, fTypeName, fDescription, fIcon, "", fCodeFile, fXmlFile)
        {
        }

        public ModuleInfo(string fName, string fTypeName, string fDescription, Image fIcon, string fGroup, string fCodeFile, string fXmlFile) : this(fName, fTypeName, fDescription, fIcon, fGroup, fCodeFile, fXmlFile, "")
        {
        }

        public ModuleInfo(string fName, string fTypeName, string fDescription, Image fIcon, string fGroup, string fCodeFile, string fXmlFile, string fAboutFile) : this(fName, fTypeName, fDescription, fIcon, fGroup, fCodeFile, fXmlFile, fAboutFile, "")
        {
        }

        public ModuleInfo(string fName, string fTypeName, string fDescription, Image fIcon, string fGroup, string fCodeFile, string fXmlFile, string fAboutFile, string fUri)
        {
            this.fName = fName;
            this.fTypeName = fTypeName;
            this.fIcon = fIcon;
            this.fDescription = fDescription;
            this.fUri = fUri;
            this.fGroup = fGroup;
            this.fCodeFile = fCodeFile;
            this.fXmlFile = fXmlFile;
            this.fAboutFile = fAboutFile;
            this.fModule = null;
        }

        public void RecreateModuleIfNecessary(Assembly asm)
        {
            if (this.fModule == null)
            {
                System.Type type = asm.GetType(this.fTypeName, true);
                ConstructorInfo constructor = type.GetConstructor(System.Type.EmptyTypes);
                if (constructor == null)
                {
                    throw new ApplicationException(type.FullName + " doesn't have public constructor with empty parameters");
                }
                try
                {
                    this.fModule = constructor.Invoke(null) as Control;
                }
                catch (TargetInvocationException exception)
                {
                    throw new ApplicationException(("Module " + type.FullName + " constructor throws an exception:") + Environment.NewLine, exception.InnerException);
                }
            }
        }

        public void ResetModule()
        {
            this.fModule = null;
        }

        public string AboutFile
        {
            get
            {
                return this.fAboutFile;
            }
        }

        public string CodeFile
        {
            get
            {
                return this.fCodeFile;
            }
        }

        public bool Created
        {
            get
            {
                return (this.fModule != null);
            }
        }

        public string Description
        {
            get
            {
                return this.fDescription;
            }
        }

        public string FullTypeName
        {
            get
            {
                return this.fTypeName;
            }
        }

        public string Group
        {
            get
            {
                return this.fGroup;
            }
            set
            {
                this.fGroup = value;
            }
        }

        public Image Icon
        {
            get
            {
                return this.fIcon;
            }
        }

        public string Name
        {
            get
            {
                return this.fName;
            }
        }

        public int Priority
        {
            get
            {
                return this.priority;
            }
            set
            {
                this.priority = value;
            }
        }

        public Control TModule
        {
            get
            {
                if ((this.Uri != string.Empty) && (this.fModule == null))
                {
                    System.Type type = (this.moduleAssembly ?? Assembly.GetExecutingAssembly()).GetType(this.fTypeName, true);
                    ConstructorInfo constructor = type.GetConstructor(new System.Type[] { typeof(string) });
                    if (constructor == null)
                    {
                        throw new ApplicationException(type.FullName + " doesn't have public constructor with empty parameters");
                    }
                    try
                    {
                        this.fModule = constructor.Invoke(new object[] { this.Uri }) as Control;
                    }
                    catch (TargetInvocationException exception)
                    {
                        throw new ApplicationException(("Module " + type.FullName + " constructor throws an exception:") + Environment.NewLine, exception.InnerException);
                    }
                }
                this.RecreateModuleIfNecessary(this.moduleAssembly ?? Assembly.GetCallingAssembly());
                return this.fModule;
            }
        }

        public string TypeName
        {
            get
            {
                return this.FullTypeName.Substring(this.FullTypeName.LastIndexOf('.') + 1);
            }
        }

        public string Uri
        {
            get
            {
                return this.fUri;
            }
            set
            {
                this.fUri = value;
            }
        }

        public bool WasShown
        {
            get
            {
                return this.wasShown;
            }
            set
            {
                this.wasShown = value;
            }
        }

        public string XMLFile
        {
            get
            {
                return this.fXmlFile;
            }
        }
    }
}

