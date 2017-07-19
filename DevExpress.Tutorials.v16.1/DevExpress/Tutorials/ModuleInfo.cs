namespace DevExpress.Tutorials
{
    using System;
    using System.Reflection;

    public class ModuleInfo
    {
        private string aboutFile;
        private string description;
        private int id;
        private ModuleBase module;
        private Type moduleType;
        private string whatsThisCodeFile;
        private string whatsThisXMLFile;

        public ModuleInfo(int id, Type moduleType, string description, string whatsThisXMLFile, string whatsThisCodeFile) : this(id, moduleType, description, whatsThisXMLFile, whatsThisCodeFile, "")
        {
        }

        public ModuleInfo(int id, Type moduleType, string description, string whatsThisXMLFile, string whatsThisCodeFile, string aboutFile)
        {
            if (!moduleType.IsSubclassOf(typeof(ModuleBase)))
            {
                throw new ArgumentException("Module class should be derived from ModuleBase");
            }
            this.description = description;
            this.aboutFile = aboutFile;
            this.whatsThisXMLFile = whatsThisXMLFile;
            this.whatsThisCodeFile = whatsThisCodeFile;
            this.moduleType = moduleType;
            this.module = null;
            this.id = id;
        }

        private ModuleBase CreateModule(Type moduleType)
        {
            if (this.module != null)
            {
                return this.module;
            }
            ConstructorInfo constructor = moduleType.GetConstructor(Type.EmptyTypes);
            if (constructor == null)
            {
                throw new ApplicationException(moduleType.FullName + " doesn't have public constructor with empty parameters");
            }
            ModuleBase base2 = constructor.Invoke(null) as ModuleBase;
            if (this.description != string.Empty)
            {
                base2.TutorialInfo.Description = this.description;
            }
            if (this.whatsThisXMLFile != string.Empty)
            {
                base2.TutorialInfo.WhatsThisXMLFile = this.whatsThisXMLFile;
            }
            if (this.whatsThisCodeFile != string.Empty)
            {
                base2.TutorialInfo.WhatsThisCodeFile = this.whatsThisCodeFile;
            }
            if (this.aboutFile != string.Empty)
            {
                base2.TutorialInfo.AboutFile = this.aboutFile;
            }
            return base2;
        }

        public void Hide()
        {
            if (this.module != null)
            {
                this.module.Visible = false;
            }
        }

        public int Id
        {
            get
            {
                return this.id;
            }
        }

        public ModuleBase Module
        {
            get
            {
                if (this.module == null)
                {
                    this.module = this.CreateModule(this.moduleType);
                }
                return this.module;
            }
        }
    }
}

