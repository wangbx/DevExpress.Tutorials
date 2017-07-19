namespace DevExpress.Tutorials
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    [ListBindable(false)]
    public class ModuleInfoCollection : CollectionBase
    {
        private ModuleBase currentModule = null;
        private static ModuleInfoCollection instance = new ModuleInfoCollection();

        private ModuleInfoCollection()
        {
        }

        private void Add(ModuleInfo value)
        {
            if (base.List.IndexOf(value) < 0)
            {
                base.List.Add(value);
            }
        }

        public static void Add(int id, System.Type moduleType, string description, string whatsThisXMLFile, string whatsThisCodeFile)
        {
            Add(id, moduleType, description, whatsThisXMLFile, whatsThisCodeFile, "");
        }

        public static void Add(int id, System.Type moduleType, string description, string whatsThisXMLFile, string whatsThisCodeFile, string aboutFile)
        {
            ModuleInfo info = new ModuleInfo(id, moduleType, description, whatsThisXMLFile, whatsThisCodeFile, aboutFile);
            instance.Add(info);
        }

        public static ModuleInfo ModuleInfoById(int id)
        {
            foreach (ModuleInfo info in instance)
            {
                if (info.Id == id)
                {
                    return info;
                }
            }
            return null;
        }

        public static void SetCurrentModule(ModuleBase module)
        {
            instance.currentModule = module;
        }

        public static void ShowModule(ModuleBase module, Control parent)
        {
            parent.Parent.SuspendLayout();
            if (module != instance.currentModule)
            {
                if (instance.currentModule != null)
                {
                    instance.currentModule.Hide();
                }
                if (module != null)
                {
                    module.Visible = false;
                    module.Parent = parent;
                    module.Dock = DockStyle.Fill;
                    module.Visible = true;
                    parent.Parent.ResumeLayout(true);
                    SetCurrentModule(module);
                }
            }
        }

        public static ModuleBase CurrentModule
        {
            get
            {
                return instance.currentModule;
            }
        }

        public static ModuleInfoCollection Instance
        {
            get
            {
                return instance;
            }
        }

        public ModuleInfo this[int index]
        {
            get
            {
                return (base.List[index] as ModuleInfo);
            }
        }
    }
}

