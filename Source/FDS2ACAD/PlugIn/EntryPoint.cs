///wouldn't it be great...
/// 
/// ...to save current calculation directory if user wants to make some changes and calculate again

namespace Fds2AcadPlugin
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Autodesk.AutoCAD.Interop.Common;
    using Autodesk.AutoCAD.Runtime;
    using Autodesk.AutoCAD.DatabaseServices;
    using BLL;
    using BLL.Helpers;
    using BLL.NativeMethods;
    using GeometryConverter;
    using GeometryConverter.Bases;
    using GeometryConverter.Optimization;
    using GeometryConverter.Templates;
    using MaterialManager.BLL;
    using UserInterface;
    using UserInterface.Materials;
    using System.IO;

    public class EntryPoint : IExtensionApplication
    {
        #region Commands

        [CommandMethod(Constants.BuildMenuCommandName)]
        public static void BuildFdsMenu()
        {
            try
            {
                var app = new DefaultFactory().CreateAcadApplication();
                var menuBar = app.MenuBar;
                var menuGroup = app.MenuGroups.Item(1);
                var menus = menuGroup.Menus;

                for (var i = 0; i < menuBar.Count; i++)
                {
                    var existingMenu = menuBar.Item(i);
                    if (existingMenu.Name == Constants.FdsMenuName)
                        return;
                }

                var fdsMenu = menus.Add(Constants.FdsMenuName);

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

            var allOptimizedElements = new List<Element>();
            foreach (var solid in selectedSolids)
            {
                // LEVEL OPTIMIZER TEST
                var valuableElements = new SolidToElementConverter(solid).ValueableElements;
                var optimizer = new LevelOptimizer(valuableElements);
                allOptimizedElements.AddRange(optimizer.Optimize());

                //allOptimizedElements.AddRange(valuableElements);

                // GET ALL VALUABLE ELEMENTS TEST
                // allOptimizedElements.AddRange(new SolidToElementConverter(solid).ValueableElements);
            }

            var maxPoint = SolidToElementConverter.GetMaxMinPoint(selectedSolids)[1];

            // var uniqueMaterials = MaterialFinder.ReturnMaterials(elements);
            var uniqueMaterials = MaterialSerializer.DeserializeMaterials(Path.Combine(AcadInfoProvider.GetPathToPluginDirectory(), Constants.MaterialsBasePath));

            // save to file
            var documentName = AcadInfoProvider.GetDocumentName();

            var pathToFile = string.Concat(calculationInfo.OutputPath, "\\", documentName, ".fds");

            var templateManager = new TemplateManager(AcadInfoProvider.GetPathToPluginDirectory(), Constants.FdsTemplateName);
            var parameters = new Dictionary<string, object>
                                         {
                                             {"elements", allOptimizedElements},
                                             {"materials", uniqueMaterials},
                                             {"calculationTime", calculationInfo.CalculationTime},
                                             {"name", documentName},
                                             {"maxPoint", maxPoint}
                                         };

            templateManager.MergeTemplateWithObjects(parameters, pathToFile);

            var startInfo = new ProcessStartInfo
                                {
                                    FileName = new DefaultFactory().CreateConfigProvider().PathToFds,
                                    Arguments = string.Concat("\"", pathToFile, "\""),
                                    WorkingDirectory = calculationInfo.OutputPath
                                };
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Process.Start(startInfo); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        [CommandMethod(Constants.OpenMaterialManagerCommandName)]
        static public void OpenMaterialManager()
        {
            var materialProvider = new MaterialProvider();
            materialProvider.ShowDialog();
        }

        #endregion

        #region IExtensionApplication Members

        public void Initialize()
        {
            BuildFdsMenu();
        }

        public void Terminate()
        {
        }

        #endregion
    }
}
