using System.Collections.Generic;

namespace Fds2AcadPlugin
{
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;
    using Autodesk.AutoCAD.Interop;
    using Autodesk.AutoCAD.Interop.Common;
    using Autodesk.AutoCAD.Runtime;
    using BLL;
    using BLL.Helpers;
    using BLL.NativeMethods;
    using GeometryConverter.DAL;
    using GeometryConverter.DAL.Collections;
    using UserInterface;
    using UserInterface.Materials;

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

                AcadPopupMenu fdsMenu = menus.Add(Constants.FdsMenuName);

                fdsMenu.AddMenuItem(0, Constants.RunFdsMenuItem, Constants.RunFdsCommandName);
                fdsMenu.AddMenuItem(1, Constants.RunSmokeViewMenuItem, Constants.RunSmokeViewCommandName);
                fdsMenu.AddMenuItem(2, Constants.OpenMaterialManagerMenuItem, Constants.OpenMaterialManagerCommandName);
                fdsMenu.AddMenuItem(3, Constants.OptionsMenuItem, Constants.OptionsCommandName);
                fdsMenu.InsertInMenuBar(menuBar.Count);
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
            //var calculationInfo = new CalculationInfo();
            //var dialogResult = calculationInfo.ShowDialog();

            //if (dialogResult == DialogResult.Cancel)
                //return;

            //MessageBox.Show(string.Format("Calculation results were saved here: {0}", calculationInfo.OutputPath));

            Document doc = new DefaultFactory().CreateDocumentManager().MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Transaction tr = db.TransactionManager.StartTransaction();

            List<Solid3d> solids = new List<Solid3d>();

            using (tr)
            {
                try
                {
                    // Prompt for selection of a solid to be traversed
                    //PromptEntityOptions prEntOpt = new PromptEntityOptions("\nSelect a 3D solid:");
                    PromptSelectionOptions prSelOpt = new PromptSelectionOptions { MessageForAdding = "Select solids to analyze: " };
                    //prEntOpt.SetRejectMessage("\nMust be a 3D solid.");
                    //prEntOpt.AddAllowedClass(typeof(Solid3d), true);

                    PromptSelectionResult prEntRes = ed.GetSelection(prSelOpt);
                    SelectionSet set = prEntRes.Value;
                    var idArray = set.GetObjectIds();

                    foreach (var id in idArray)
                    {
                        var solid = tr.GetObject(id, OpenMode.ForRead);
                        if (solid.GetType() == typeof(Solid3d))
                        solids.Add((Solid3d) solid);
                    }
                    
                    //Solid3d sol = (Solid3d)tr.GetObject(prEntRes., OpenMode.ForRead);
                    //Acad3DSolid oSol = (Acad3DSolid)sol.AcadObject;
                    //ed.WriteMessage("\nSolid type: {0}", oSol.SolidType);
                    SolidOperator op = new SolidOperator();
                    ElementCollection result = op.Analyze(solids);
                    List<string> elements = MaterialManager.BLL.MaterialFinder.ReturnMaterials(result);
                    ed.WriteMessage("\nElement count: {0}", result.Elements.Count);
                    ed.WriteMessage("\nMaterials count: {0}", elements.Count);
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("\nException during analysis: {0}", ex.Message);
                }

            }
        }

        [CommandMethod(Constants.OpenMaterialManagerCommandName)]
        static public void OpenMaterialManager()
        {
            var materialProvider = new MaterialProvider();
            materialProvider.ShowDialog();
        }
    }
}