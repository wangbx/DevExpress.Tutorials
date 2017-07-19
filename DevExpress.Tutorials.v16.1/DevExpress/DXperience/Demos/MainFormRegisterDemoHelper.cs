using DevExpress.DemoData.Model;
using DevExpress.Tutorials.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.DXperience.Demos
{
    public class MainFormRegisterDemoHelper
    {
        public static string GetTitle(SimpleModule module)
        {
            string displayName = module.DisplayName;
            if (module.IsUpdated)
            {
                return displayName + string.Format(" ({0})", Resources.Updated);
            }
            if (module.IsNew)
            {
                return displayName + string.Format(" ({0})", Resources.New);
            }
            return displayName;
        }

        private static void AddNewAndUpdatedDemos(List<ModuleInfo> newAndUpdated)
        {
            newAndUpdated.Sort((ModuleInfo x, ModuleInfo y) => x.Priority - y.Priority);
            MainFormRegisterDemoHelper.AddDemos(newAndUpdated);
        }

        private static void AddHighlightedDemos(List<ModuleInfo> highlighted)
        {
            highlighted.Sort((ModuleInfo x, ModuleInfo y) => x.Priority - y.Priority);
            MainFormRegisterDemoHelper.AddDemos(highlighted);
        }

        private static void AddDemos(List<ModuleInfo> demos)
        {
            foreach (ModuleInfo current in demos)
            {
                ModulesInfo.Add(current);
            }
        }

        public static void RegisterDemos(string productID)
        {
            Product product = Repository.Platforms.SelectMany((Platform p) => p.Products).First((Product p) => p.Name == productID);
            Demo demo = product.Demos.FirstOrDefault((Demo x) => x.Modules.Count > 0);
            List<ModuleInfo> list = new List<ModuleInfo>();
            List<ModuleInfo> list2 = new List<ModuleInfo>();
            List<ModuleInfo> list3 = new List<ModuleInfo>();
            ModuleInfo info = null;
            foreach (SimpleModule current in demo.Modules.Cast<SimpleModule>())
            {
                ModuleInfo moduleInfo = new ModuleInfo(MainFormRegisterDemoHelper.GetTitle(current), current.Type, current.Description, current.Icon.Image, current.Group);
                if (current is ExampleModule)
                {
                    moduleInfo.Uri = (current as ExampleModule).Uri;
                }
                if (current.Group == "About")
                {
                    info = moduleInfo;
                }
                else
                {
                    list3.Add(moduleInfo);
                }
                if (current.IsNew || current.IsUpdated)
                {
                    list.Add(new ModuleInfo(moduleInfo)
                    {
                        Group = Resources.NewUpdateGroup,
                        Priority = current.NewUpdatedPriority
                    });
                }
                if (current.IsFeatured)
                {
                    list2.Add(new ModuleInfo(moduleInfo)
                    {
                        Group = Resources.HighlightedFeaturesGroup,
                        Priority = current.FeaturedPriority
                    });
                }
            }
            ModulesInfo.Add(info);
            MainFormRegisterDemoHelper.AddNewAndUpdatedDemos(list);
            MainFormRegisterDemoHelper.AddHighlightedDemos(list2);
            MainFormRegisterDemoHelper.AddDemos(list3);
        }
    }
}