using System;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraNavBar;
using DevExpress.DXperience.Demos;
using DevExpress.XtraBars;
using DevExpress.LookAndFeel;

namespace XtraPrintingDemos {
	public class DemosInfo : ModulesInfo {
		public static void ShowModuleEx(string name, DevExpress.XtraEditors.GroupControl group, DevExpress.LookAndFeel.DefaultLookAndFeel lookAndFeel, DevExpress.Utils.Frames.ApplicationCaption caption) {
			ModuleInfo item = DemosInfo.GetItem(name);
			Cursor currentCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
            try {
                Control oldTutorial = null;
                if(Instance.CurrentModuleBase != null) {
                    if(Instance.CurrentModuleBase.Name == name)
                        return;
                    Instance.CurrentModuleBase.TModule.Visible = false;
                }

                TutorialControlBase tutorial = item.TModule as TutorialControlBase;
                tutorial.Bounds = group.DisplayRectangle;
                Instance.CurrentModuleBase = item;
                tutorial.Visible = false;
                group.Controls.Add(tutorial);
                tutorial.Dock = DockStyle.Fill;

                tutorial.TutorialName = name;
                tutorial.Caption = caption;

                tutorial.Visible = true;
                tutorial.BringToFront();
                tutorial.Focus();
                if(!item.WasShown && tutorial is ModuleControl) {
                    ((ModuleControl)tutorial).Activate();
                    if(tutorial.GetType().Name != "MainFeaturesControl")
                        item.WasShown = true;
                }
                if(oldTutorial != null)
                    oldTutorial.Visible = false;
            }
			finally {
				Cursor.Current = currentCursor;
			}
			RaiseModuleChanged();
		}
	}
}

