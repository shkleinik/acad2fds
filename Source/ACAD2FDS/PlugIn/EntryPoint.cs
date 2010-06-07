using GeometryConverter.Helpers;

///wouldn't it be great...
/// 
/// ...to save current calculation directory if user wants to make some changes and calculate again
/// 
/// ...to add devices (array[][] and spheric)

namespace Fds2AcadPlugin
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Autodesk.AutoCAD.Interop.Common;
    using Autodesk.AutoCAD.Runtime;
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
                fdsMenu.AddMenuItem(3, Constants.EditMaterialsMappingsMenuItem, Constants.EditMaterialsMappingsCommandName);
                fdsMenu.AddMenuItem(4, Constants.OptionsMenuItem, Constants.OptionsCommandName);
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

            var smokeViewHandle = CommonHelper.StartSmokeViewProcess(new DefaultFactory().CreateFdsConfig().PathToSmokeView, openFileDialog.FileName);
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
            #region Check if config exists

            if(new DefaultFactory().CreateFdsConfig() == null)
            {
                var fdsConfig = new PluginOptions();
                fdsConfig.ShowDialog();
            }

            #endregion

            #region Collect information

            // Ask user to configure calculation
            var calculationInfo = new CalculationInfo();
            var dialogResult = calculationInfo.ShowDialog();

            if (dialogResult == DialogResult.Cancel)
                return;

            // get solids
            var selectedSolids = AcadInfoProvider.AskUserToSelectSolids();

            if (selectedSolids.Count < 1)
                return;

            #endregion

            #region Initialize progress window

            var progressWindow = new ConversionProgress(selectedSolids.Count);
            progressWindow.Show(new DefaultFactory().CreateAcadActiveWindow());

            #endregion

            #region Convert geometry

            var allOptimizedElements = new List<Element>();
            var minMaxPoint = SolidToElementConverter.GetMaxMinPoint(selectedSolids);
            var progress = 0;
            foreach (var solid in selectedSolids)
            {
                progressWindow.Update(progress, string.Format("{0} of {1} solids converted", ++progress, selectedSolids.Count));

                // LEVEL OPTIMIZER TEST
                var converter = new SolidToElementConverter(solid)
                                    {
                                        SolidVolume = ((Acad3DSolid)solid.AcadObject).Volume
                                    };
                var valuableElements = converter.ValueableElements;

                #region Handle out of memory exception

                if (!converter.IsSuccessfullConversion)
                {
                    var result = MessageBox.Show("Lack of system resources. Proceed?", "Warning",
                                    MessageBoxButtons.OKCancel,
                                    MessageBoxIcon.Exclamation,
                                    MessageBoxDefaultButton.Button1);

                    if (result == DialogResult.Cancel)
                    {
                        progressWindow.Close();
                        return;
                    }
                    break;
                }

                #endregion

                var optimizer = new LevelOptimizer(valuableElements);

                allOptimizedElements.AddRange(optimizer.Optimize());

                // GET ALL VALUABLE ELEMENTS TEST
                // allOptimizedElements.AddRange(new SolidToElementConverter(solid).ValueableElements);
            }

            progressWindow.Close();

            #endregion

            #region Handle negative offset

            var minPoint = minMaxPoint[0];
            var vector = ElementHelper.InitNegativeOffsetVector(minPoint);
            if (vector != null)
            {
                foreach (var element in allOptimizedElements)
                    element.Center.MoveUsingNegativeOffsetVector(vector);

                minMaxPoint[0].MoveUsingNegativeOffsetVector(vector);
                minMaxPoint[1].MoveUsingNegativeOffsetVector(vector);
            }

            #endregion

            #region Genrating output

            // EditMaterialsMappings();
            var mappings = XmlSerializer<List<MaterialAndSurface>>.Deserialize(PluginInfoProvider.PathToMappingsMaterials);

            var maxPoint = minMaxPoint[1];

            var documentName = AcadInfoProvider.GetDocumentName();

            var pathToFile = Path.Combine(calculationInfo.OutputPath, string.Concat(documentName, Constants.FdsFileExtension));

            var templateManager = new TemplateManager(PluginInfoProvider.GetPathToPluginDirectory(), Constants.FdsTemplateName);
            var parameters = new Dictionary<string, object>
                                         {
                                             {"elements", allOptimizedElements},
                                             {"mappings", mappings},
                                             {"calculationTime", calculationInfo.CalculationTime},
                                             {"name", documentName},
                                             {"maxPoint", maxPoint}
                                         };

            templateManager.MergeTemplateWithObjects(parameters, pathToFile);

            #endregion

            #region Start Calculations

            var startInfo = new ProcessStartInfo
                        {
                            FileName = new DefaultFactory().CreateFdsConfig().PathToFds,
                            Arguments = string.Concat("\"", pathToFile, "\""),
                            WorkingDirectory = calculationInfo.OutputPath
                        };

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Process.Start(startInfo); // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! 

            #endregion
        }

        [CommandMethod(Constants.OpenMaterialManagerCommandName)]
        static public void OpenMaterialManager()
        {
            var surfacesStore = XmlSerializer<List<Surface>>.Deserialize(PluginInfoProvider.PathToSurfacesStore) ?? new List<Surface>();
            var materialsStore = XmlSerializer<List<Material>>.Deserialize(PluginInfoProvider.PathToMaterialsStore) ?? new List<Material>();

            var materialProvider = new MaterialProvider(materialsStore, surfacesStore);
            var dialogResult = materialProvider.ShowDialog();

            if (dialogResult != DialogResult.OK) 
                return;

            XmlSerializer<List<Material>>.Serialize(materialProvider.MaterialsStore, PluginInfoProvider.PathToMaterialsStore);
            XmlSerializer<List<Surface>>.Serialize(materialProvider.SurfacesStore, PluginInfoProvider.PathToSurfacesStore);
        }

        [CommandMethod(Constants.EditMaterialsMappingsCommandName)]
        static public void EditMaterialsMappings()
        {
            var allUsedMaterials = AcadInfoProvider.AllSolidsFromCurrentDrawing().GetMaterials();
            var materialsStore = XmlSerializer<List<Surface>>.Deserialize(PluginInfoProvider.PathToMaterialsStore);
            var mappingMaterials = XmlSerializer<List<MaterialAndSurface>>.Deserialize(PluginInfoProvider.PathToMappingsMaterials);

            var materialMapper = new MaterialMapper(allUsedMaterials, materialsStore, mappingMaterials);

            var dialogResult = materialMapper.ShowDialog();
            if (dialogResult == DialogResult.OK)
                XmlSerializer<List<MaterialAndSurface>>.Serialize(materialMapper.MappingMaterials, PluginInfoProvider.PathToMappingsMaterials);
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
