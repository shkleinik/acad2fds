using Autodesk.AutoCAD.Interop.Common;

namespace Fds2AcadPlugin
{
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Autodesk.AutoCAD.Interop;
    using Autodesk.AutoCAD.Runtime;

    public class EntryPoint
    {
        [CommandMethod("BuildFdsMenu")]
        public static void BuildFdsMenu()
        {
            try
            {
                AcadApplication app = (AcadApplication)Marshal.GetActiveObject("AutoCAD.Application.17");

                AcadMenuBar menuBar = app.MenuBar;
                AcadMenuGroup menuGroup = app.MenuGroups.Item(1);
                AcadPopupMenus menus = menuGroup.Menus;

                AcadPopupMenu mymenu = menus.Add("FDS to Acad plugin");

                mymenu.AddMenuItem(0, "Start calculation", "RunCalculationInFds");
                //mymenu.AddSeparator(1);
                mymenu.AddMenuItem(1, "View result in SmokeView", "ViewResultInSmokeView");
                mymenu.AddMenuItem(2, "Options", "PluginOptions");
                //AcadPopupMenu ext = mymenu.AddSubMenu(4, "Ext");
                //ext.AddMenuItem(0, "Hello", "hello");
                //ext.AddSeparator(1);
                //ext.AddMenuItem(2, "Hello2", "hello");
                mymenu.InsertInMenuBar(menuBar.Count);
                menuGroup.Save(AcMenuFileType.acMenuFileSource);

                // MessageBox.Show("Hello, world in messagebox");
            }
            catch (Exception)
            {
                MessageBox.Show("Error occured during FDS menu building");
            }
        }

        [CommandMethod("RunCalculationInFds")]
        public static void RunCalculationInFds()
        {
            MessageBox.Show("Unfortunately not implemnted yet.");
        }

        [CommandMethod("ViewResultInSmokeView")]
        public static void ViewResultInSmokeView()
        {
            MessageBox.Show("Unfortunately not implemnted yet.");
        }

        [CommandMethod("PluginOptions")]
        public static void PluginOptions()
        {
            MessageBox.Show("Unfortunately not implemnted yet.");
        }
    }
}