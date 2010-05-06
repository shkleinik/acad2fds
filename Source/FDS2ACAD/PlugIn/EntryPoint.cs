using Autodesk.AutoCAD.Runtime;
using Fds2AcadPlugin;

[assembly: CommandClass(typeof(EntryPoint))]
namespace Fds2AcadPlugin
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Autodesk.AutoCAD.Interop;
    using Autodesk.AutoCAD.Interop.Common;
    using Autodesk.AutoCAD.Runtime;
    using BLL;
    using GeometryConverter;
    using GeometryConverter.Bases;
    using GeometryConverter.Templates;
    using BLL.Helpers;
    using BLL.NativeMethods;
    using MaterialManager.BLL;
    using UserInterface;
    using UserInterface.Materials;
    using System.Diagnostics;
    using GeometryConverter.Optimization;

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
            // ask user input
            var calculationInfo = new CalculationInfo();
            var dialogResult = calculationInfo.ShowDialog();

            if (dialogResult == DialogResult.Cancel)
                return;

            // get solids
            var selectedSolids = AcadInfoProvider.AskUserToSelectSolids();
            if (selectedSolids.Count < 1)
                return;

            // convert geometry
            var burnerSolid = selectedSolids.Find(s => s.Material == "Red");

            Element burner = null;

            if (burnerSolid != null)
                burner = new BurnerOperator(burnerSolid).Element;


            var allOptimizedElements = new List<Element>();
            foreach (var solid in selectedSolids)
            {

                //   GLUER TESTS 
                //var valuableElements = new SolidToElementConverter(solid).ValueableElements;
                //var gluer = new Gluer(valuableElements);
                //var gluedElements = gluer.GetGluedElements();

                // allOptimizedElements.AddRange(gluedElements);
                // allOptimizedElements.AddRange(new SolidToElementConverter(solid).ValueableElements);


                // LEVEL OPTIMIZER TEST
                var valuableElements = new SolidToElementConverter(solid).ValueableElements;
                var optimizer = new LevelOptimizer(valuableElements);
                allOptimizedElements = optimizer.Optimize();
            }

            // var valueableElements = elementConverter.ValueableElements;
            // var gluer = new Gluer(valueableElements);
            // var optimizedElements = gluer.GetGluedElements();

            // Todo : work out decision how to eliminate multiple SolidToElementConverter initializition
            var elementConverter = new SolidToElementConverter(selectedSolids);
            var maxPoint = elementConverter.MaxMinPoint[1];

            // var uniqueMaterials = MaterialFinder.ReturnMaterials(elements);
            var uniqueMaterials = MaterialSerializer.DeserializeMaterials(string.Concat(AcadInfoProvider.GetPathToPluginDirectory(), Constants.MaterialsBasePath));

            // save to file
            var documentName = AcadInfoProvider.GetDocumentName();

            var pathToFile = string.Concat(calculationInfo.OutputPath, "\\", documentName, ".fds");

            var templateManager = new TemplateManager(AcadInfoProvider.GetPathToPluginDirectory(), Constants.FdsTemplateName);
            var parameters = new Dictionary<string, object>
                                         {
                                             // {"elements", valueableElements},
                                             // {"elements", optimizedElements},
                                             {"elements", allOptimizedElements},
                                             {"materials", uniqueMaterials},
                                             {"calculationTime", calculationInfo.CalculationTime},
                                             {"name", documentName},
                                             {"maxPoint", maxPoint},
                                             {"burner", burner}
                                         };

            templateManager.MergeTemplateWithObjects(parameters, pathToFile);

            var startInfo = new ProcessStartInfo
                                {
                                    FileName = new DefaultFactory().CreateConfigProvider().PathToFds,
                                    Arguments = string.Concat("\"", pathToFile, "\""),
                                    WorkingDirectory = calculationInfo.OutputPath
                                };
#if DEBUG
            // startInfo.Arguments = "\"D:\\!Study\\Diplom\\FDS tests\\PluginTest\\room_fire.fds";
#endif

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            /*!!!!!!!!!!!!!*/  Process.Start(startInfo); //!!!!!!!!!!!!!
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        [CommandMethod(Constants.OpenMaterialManagerCommandName)]
        static public void OpenMaterialManager()
        {
            var materialProvider = new MaterialProvider();
            materialProvider.ShowDialog();
        }
    }
}
