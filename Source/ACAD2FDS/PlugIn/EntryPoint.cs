/*    wouldn't it be great
/// 
/// ...to save current calculation directory if user wants to make some changes and calculate again
/// 
/// ...to add devices (array[][] and spheric)
*/

using Autodesk.AutoCAD.DatabaseServices;

namespace Fds2AcadPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using Autodesk.AutoCAD.Interop.Common;
    using Autodesk.AutoCAD.Runtime;
    using BLL;
    using BLL.Helpers;
    using BLL.NativeMethods;
    using Common;
    using Common.UI;
    using GeometryConverter;
    using GeometryConverter.Bases;
    using GeometryConverter.Helpers;
    using GeometryConverter.Optimization;
    using GeometryConverter.Templates;
    using MaterialManager.BLL;
    using UserInterface;
    using UserInterface.Materials;

    public class EntryPoint : IExtensionApplication
    {
        #region Properties

        private static ILogger Log { get; set; }

        #endregion

        #region Commands

        [CommandMethod(Constants.BuildMenuCommandName)]
        public void BuildFdsMenu()
        {
            try
            {
                var app = new DefaultFactory(Log).CreateAcadApplication();
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
                fdsMenu.AddMenuItem(2, Constants.ConvertTo3dSolidsMenuItem, Constants.ConvertTo3dSolidsCommandName);
                fdsMenu.AddMenuItem(3, Constants.OpenMaterialManagerMenuItem, Constants.OpenMaterialManagerCommandName);
                fdsMenu.AddMenuItem(4, Constants.EditMaterialsMappingsMenuItem, Constants.EditMaterialsMappingsCommandName);
                fdsMenu.AddMenuItem(5, Constants.OptionsMenuItem, Constants.OptionsCommandName);
                fdsMenu.AddMenuItem(6, Constants.AboutMenuItem, Constants.AboutCommandName);
                fdsMenu.InsertInMenuBar(menuBar.Count);
                menuGroup.Save(AcMenuFileType.acMenuFileCompiled);

                Log.LogInfo("Plugin's menu was successfully built.");
            }
            catch (COMException ex)
            {
                Log.LogError(ex);
                UserNotifier.ShowError(string.Format(Constants.MenuBuildErrorMessagePattern, ex.Message));
            }
            catch (System.Exception ex)
            {
                Log.LogError(ex);
                UserNotifier.ShowError(string.Format(Constants.MenuBuildErrorMessagePattern, ex.Message));
            }
        }

        [CommandMethod(Constants.RunSmokeViewCommandName)]
        public void ViewResultInSmokeView()
        {
            try
            {
                var fdsConfig = new DefaultFactory(Log).CreateFdsConfig();


                if (fdsConfig == null)
                {
                    UserNotifier.ShowWarning(Constants.PluginWasNotConfigured);
                    return;
                }

                if (string.IsNullOrEmpty(fdsConfig.PathToSmokeView))
                {
                    UserNotifier.ShowWarning(Constants.SmokeViewPathIsnotConfigured);
                    return;
                }

                var openFileDialog = new OpenFileDialog
                                    {
                                        Multiselect = false,
                                        Filter = @"SmokeView files|*.smv",
                                    };

                var dialogResult = openFileDialog.ShowDialog();

                if (DialogResult.OK != dialogResult)
                    return;

                var smokeViewHandle = CommonHelper.StartSmokeViewProcess(fdsConfig.PathToSmokeView, openFileDialog.FileName);
                var mdiHostHandle = NativeMethods.GetParent(new DefaultFactory(Log).GetAcadActiveWindow().Handle);

                NativeMethods.SetParent(smokeViewHandle, mdiHostHandle);
                NativeMethods.ShowWindow(smokeViewHandle, NativeMethods.SW_SHOWMAXIMIZED);
            }
            catch (System.Exception exception)
            {
                Log.LogError(exception);
                UserNotifier.ShowError(exception.Message);
            }
        }

        [CommandMethod(Constants.OptionsCommandName)]
        public void PluginOptions()
        {
            var plugionOptions = new PluginOptions(Log);
            plugionOptions.ShowDialog();
        }

        [CommandMethod(Constants.RunFdsCommandName)]
        public void RunCalculationInFds()
        {
            // Todo : Bad practice to use such kind of exception handling.
            try
            {
                #region Check if config exists

                var pluginConfig = new DefaultFactory(Log).CreateFdsConfig();

                if (pluginConfig == null)
                {
                    var fdsConfig = new PluginOptions(Log);
                    fdsConfig.ShowDialog();
                    pluginConfig = fdsConfig.PluginConfig;
                    fdsConfig.Dispose();
                    fdsConfig = null;
                }

                #endregion

                #region Collect information

                // Ask user to configure calculation
                var calculationInfo = new CalculationInfo();
                var dialogResult = calculationInfo.ShowDialog();


                var calcTime = calculationInfo.CalculationTime;
                var workingDir = calculationInfo.OutputPath;

                calculationInfo.Dispose();
                calculationInfo = null;

                if (dialogResult == DialogResult.Cancel)
                    return;

                EditMaterialsMappings();

                // get solids
                var selectedSolids = AcadInfoProvider.AskUserToSelectSolids();

                if (selectedSolids.Count < 1)
                    return;

                #endregion

                #region Start Calculations

                var fdsStartInfo = new FdsStartInfo
                                       {
                                           // Arguments = string.Concat("\"", pathToFile, "\""),
                                           CalculationTime = calcTime,
                                           DocumentName = AcadInfoProvider.GetDocumentName(),
                                           PathToFds = pluginConfig.PathToFds,
                                           SelectedSolids = selectedSolids.CloneList(),
                                           UsedSurfaces = CommonHelper.GetAllUsedSurfaces(Log),
                                           WorkingDirectory = workingDir
                                       };

                selectedSolids.Clear();
                selectedSolids = null;

                var calcManager = new ThreadedCalculationManager(fdsStartInfo, Log);
                calcManager.StartCalculation();

                //var fdsThread = new Thread(StartFdsProcess)
                //                {
                //                    Name = "FDS process start thread"
                //                };

                //fdsThread.Start(fdsStartInfo);

                #endregion
            }
            catch (System.Exception exception)
            {
                Log.LogError(exception);
                UserNotifier.ShowError(exception.Message);
            }
        }

        [CommandMethod(Constants.OpenMaterialManagerCommandName)]
        public void OpenMaterialManager()
        {
            var surfacesStore = XmlSerializer<List<Surface>>.Deserialize(PluginInfoProvider.PathToSurfacesStore, Log) ?? new List<Surface>();
            var materialsStore = XmlSerializer<List<Material>>.Deserialize(PluginInfoProvider.PathToMaterialsStore, Log) ?? new List<Material>();

            var materialProvider = new MaterialProvider(materialsStore, surfacesStore);
            var dialogResult = materialProvider.ShowDialog();

            if (dialogResult != DialogResult.OK)
                return;

            XmlSerializer<List<Material>>.Serialize(materialProvider.MaterialsStore, PluginInfoProvider.PathToMaterialsStore);
            XmlSerializer<List<Surface>>.Serialize(materialProvider.SurfacesStore, PluginInfoProvider.PathToSurfacesStore);
        }

        [CommandMethod(Constants.EditMaterialsMappingsCommandName)]
        public void EditMaterialsMappings()
        {
            var usedMaterials = AcadInfoProvider.AllSolidsFromCurrentDrawing().GetMaterials();
            var surfacesStore = XmlSerializer<List<Surface>>.Deserialize(PluginInfoProvider.PathToSurfacesStore, Log);
            var mappingMaterials = XmlSerializer<List<MaterialAndSurface>>.Deserialize(PluginInfoProvider.PathToMappingsMaterials, Log);

            var materialMapper = new MaterialMapper(usedMaterials, surfacesStore, mappingMaterials);

            var dialogResult = materialMapper.ShowDialog();

            if (dialogResult == DialogResult.OK)
                XmlSerializer<List<MaterialAndSurface>>.Serialize(materialMapper.MappingMaterials, PluginInfoProvider.PathToMappingsMaterials);

            materialMapper.Dispose();
            materialMapper = null;
        }

        [CommandMethod(Constants.AboutCommandName)]
        public void ShowAboutForm()
        {
            var about = new About(Log)
                            {
                                ProductLogo = PluginInfoProvider.ProductLogo,
                                PluginName = PluginInfoProvider.ProductName,
                                Authors = string.Join(", ", PluginInfoProvider.Authors),
                                Description = PluginInfoProvider.ProductDescription,
                                Version = PluginInfoProvider.ProductVersion
                            };

            about.ShowDialog();
        }

#if DEBUG

        [CommandMethod("ThreadTest")]
        public void ThreadTest()
        {
            var thread = new Thread(RunTestThread);
            thread.Start();
        }

        public void RunTestThread()
        {
            Thread.Sleep(new TimeSpan(0, 0, 0, 30));
        }

#endif

        #endregion

        #region IExtensionApplication Members

        void IExtensionApplication.Initialize()
        {
            Log = new Logger();

            Log.LogInfo("Plugin initialization was started.");
            BuildFdsMenu();
            Log.LogInfo("Plugin initialization was finished.");
        }

        void IExtensionApplication.Terminate()
        {
            Log.LogInfo("Plugin was terminated.");
        }

        #endregion
    }

    public static class SomeExtensions
    {
        public static IList<Entity> CloneList(this IList<Entity> list)
        {
            var clonedList = new List<Entity>();

            foreach (var entity in list)
            {
                clonedList.Add((Entity)entity.Clone());
            }

            return clonedList;
        }
    }
}
