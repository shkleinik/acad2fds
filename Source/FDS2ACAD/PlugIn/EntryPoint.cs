using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using GeometryConverter.DAL;
using GeometryConverter.DAL.Collections;

namespace Fds2AcadPlugin
{
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Autodesk.AutoCAD.Interop;
    using Autodesk.AutoCAD.Interop.Common;
    using Autodesk.AutoCAD.Runtime;
    using BLL;
    using BLL.Helpers;
    using BLL.NativeMethods;
    using UserInterface;

    public class EntryPoint
    {
        [CommandMethod(Constants.BuildMenuCommandName)]
        public static void BuildFdsMenu()
        {
            try
            {
                AcadApplication app = new DefaultFactory().CreateAcadApplication();
                AcadMenuBar menuBar = app.MenuBar;
                AcadMenuGroup menuGroup = app.MenuGroups.Item(1);
                AcadPopupMenus menus = menuGroup.Menus;

                for (var i = 0; i < menuBar.Count; i++)
                {
                    AcadPopupMenu existingMenu = menuBar.Item(i);
                    if (existingMenu.Name == Constants.FdsMenuName)
                        return;
                }

                AcadPopupMenu mymenu = menus.Add(Constants.FdsMenuName);

                mymenu.AddMenuItem(0, Constants.RunFdsMenuItem, Constants.RunFdsCommandName);
                mymenu.AddMenuItem(1, Constants.RunSmokeViewMenuItem, Constants.RunSmokeViewCommandName);
                mymenu.AddMenuItem(2, Constants.OptionsMenuItem, Constants.OptionsCommandName);
                mymenu.InsertInMenuBar(menuBar.Count);
                menuGroup.Save(AcMenuFileType.acMenuFileCompiled);
            }
            catch (COMException ex)
            {
                MessageBox.Show(string.Format(Constants.MenuBuildErrorMessagePattern, ex.Message));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format(Constants.MenuBuildErrorMessagePattern, ex.Message));
            }
        }

        //[CommandMethod(Constants.RunFdsCommandName)]
        //public static void RunCalculationInFds()
        //{
        //    var calculationInfo = new CalculationInfo();
        //    var dialogResult = calculationInfo.ShowDialog();

        //    if (dialogResult == DialogResult.Cancel)
        //        return;

        //    MessageBox.Show(string.Format("Calculation results were saved here: {0}", calculationInfo.OutputPath));
        //    // Note : how to use factory
        //    // var pathToFds = new DefaultFactory().CreateConfigProvider().PathToFds;
        //}

        [CommandMethod(Constants.RunSmokeViewCommandName)]
        public static void ViewResultInSmokeView()
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "SmokeView files|*.smv",
            };

            var dialogResult = openFileDialog.ShowDialog();

            if (DialogResult.OK != dialogResult)
                return;

            var smokeViewHandle = CommonHelper.StartSmokeViewProcess(new DefaultFactory().CreateConfigProvider().PathToSmokeView, openFileDialog.FileName);
            var mdiHostHandle = NativeMethods.GetParent(new DefaultFactory().CreateAcadActiveWindow().Handle);

            NativeMethods.SetParent(smokeViewHandle, mdiHostHandle);
        }

        [CommandMethod(Constants.OptionsCommandName)]
        public static void PluginOptions()
        {
            var plugionOptions = new PluginOptions();
            plugionOptions.ShowDialog();
        }

        [CommandMethod(Constants.RunFdsCommandName)]
        static public void RunCalculationInFds()
        {
            var calculationInfo = new CalculationInfo();
            var dialogResult = calculationInfo.ShowDialog();

            if (dialogResult == DialogResult.Cancel)
                return;

            MessageBox.Show(string.Format("Calculation results were saved here: {0}", calculationInfo.OutputPath));

            Document doc = new DefaultFactory().CreateDocumentManager().MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Transaction tr = db.TransactionManager.StartTransaction();

            using (tr)
            {
                try
                {
                    // Prompt for selection of a solid to be traversed
                    PromptEntityOptions prEntOpt = new PromptEntityOptions("\nSelect a 3D solid:");
                    prEntOpt.SetRejectMessage("\nMust be a 3D solid.");
                    prEntOpt.AddAllowedClass(typeof(Solid3d), true);

                    PromptEntityResult prEntRes = ed.GetEntity(prEntOpt);

                    Solid3d sol = (Solid3d)tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead);


                    Acad3DSolid oSol = (Acad3DSolid)sol.AcadObject;
                    ed.WriteMessage("\nSolid type: {0}", oSol.SolidType);
                    ElementCollection result = SolidOperator.Analyze(sol);
                    ed.WriteMessage("\nElement count: {0}", result.Elements.Count);
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("\nException during analysis: {0}", ex.Message);
                }

            }
        }
    }
}