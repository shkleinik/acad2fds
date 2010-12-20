namespace Fds2AcadPlugin.BLL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;
    using Common;
    using Common.UI;
    using GeometryConverter;
    using GeometryConverter.Bases;
    using GeometryConverter.Helpers;
    using GeometryConverter.Optimization;
    using GeometryConverter.Templates;
    using Helpers;
    using MaterialManager.BLL;
    using UserInterface;

    public class ThreadedCalculationManager : IDisposable
    {
        #region Contants

        private const string CalculationThreadName = "Calculation Thread";

        #endregion

        #region Fields

        private readonly ConversionProgress progressWindow;
        private Thread calculationThread;
        private readonly FdsStartInfo fdsStartInfo;
        private readonly ILogger log;

        #endregion

        public ThreadedCalculationManager(FdsStartInfo fdsStartInfo, ILogger logger, ConversionProgress conversionProgressForm)
        {
            log = logger;
            progressWindow = conversionProgressForm;
            this.fdsStartInfo = fdsStartInfo;
        }

        public ManualResetEvent WaitEvent { get; set; }

        public void StartCalculation()
        {
            WaitEvent = new ManualResetEvent(false);

            calculationThread = new Thread(CalculationThread);
            calculationThread.Name = CalculationThreadName;
            calculationThread.Start();
        }

        public void Dispose()
        {
            if (calculationThread.IsAlive)
            {
                calculationThread.Abort();
                calculationThread = null;
            }
        }

        private void CalculationThread()
        {
            WaitEvent.WaitOne(); // Waiting for GUI thread.

            #region Input validation

            if (fdsStartInfo == null)
            {
                log.LogInfo("Calculation was not started because no start info was provided.");
                return;
            }

            var selectedSolids = fdsStartInfo.SelectedSolids;

            if (selectedSolids == null || selectedSolids.Count < 1)
            {
                log.LogInfo("No solids to convert were provided.");
                return;
            }

            var pluginConfig = new DefaultFactory(log).CreateFdsConfig();

            if (pluginConfig == null)
            {
                log.LogInfo("No config was provided.");
                return;
            }

            #endregion

            #region Convert geometry

            var allOptimizedElements = new List<Element>();
            var minMaxPoint = SolidToElementConverter.GetMaxMinPoint(selectedSolids);
            var progress = 0;

            foreach (var solid in selectedSolids)
            {
                // LEVEL OPTIMIZER TEST

                SolidToElementConverter converter = pluginConfig.UseCustomElementSize
                                                  ? new SolidToElementConverter(solid, pluginConfig.ElementSize)
                                                  : new SolidToElementConverter(solid);

                var valuableElements = converter.ValueableElements;

                #region Handle out of memory exception

                if (!converter.IsSuccessfullConversion)
                {
                    var result = UserNotifier.ShowWarningWithConfirmation(Constants.OutOfMemoruMessage);

                    if (result == DialogResult.Cancel)
                    {
                        if (progressWindow != null)
                        {
                            progressWindow.ForceClose = true;
                            Action action = progressWindow.Close;
                            progressWindow.Invoke(action);
                        }

                        return;
                    }

                    break;
                }

                #endregion

                var optimizer = new LevelOptimizer(valuableElements);

                allOptimizedElements.AddRange(optimizer.Optimize());

                // GET ALL VALUABLE ELEMENTS TEST
                // allOptimizedElements.AddRange(new SolidToElementConverter(solid).ValueableElements);

                if (progressWindow != null)
                    progressWindow.Update(progress, string.Format(Constants.ConvertedSolidsInfoPattern, progress, selectedSolids.Count));

                progress++;
            }

            selectedSolids.Clear();
            selectedSolids = null;

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

            #region Generating output

            var usedSurfaces = fdsStartInfo.UsedSurfaces;
            var requiredMaterials = usedSurfaces.GetNecessaryMaterialsFromSurfaces(log);
            var mappings = XmlSerializer<List<MaterialAndSurface>>.Deserialize(PluginInfoProvider.PathToMappingsMaterials, log);
            var materialsToSurfaces = mappings.GetDictionary();

            var maxPoint = minMaxPoint[1];

            var documentName = fdsStartInfo.DocumentName;

            var pathToFile = Path.Combine(fdsStartInfo.WorkingDirectory, string.Concat(documentName, Constants.FdsFileExtension));

            var templateManager = new TemplateManager(Info.PluginDirectory, Constants.FdsTemplateName);

            var parameters = new Dictionary<string, object>
                                         {
                                             {"elements", allOptimizedElements},
                                             {"usedSurfaces", usedSurfaces},
                                             {"usedMaterials", requiredMaterials},
                                             {"materialsToSurfaces", materialsToSurfaces},
                                             {"calculationTime", fdsStartInfo.CalculationTime},
                                             {"name", documentName},
                                             {"maxPoint", maxPoint}
                                         };

            templateManager.MergeTemplateWithObjects(parameters, pathToFile);

            #endregion

            #region Start FDS

            var startInfo = new ProcessStartInfo
                            {
                                Arguments = string.Concat("\"", pathToFile, "\""),//fdsStartInfo.Arguments,
                                FileName = fdsStartInfo.PathToFds,
                                WorkingDirectory = fdsStartInfo.WorkingDirectory
                            };

            Process.Start(startInfo);

            #endregion

            if (progressWindow != null)
            {
                progressWindow.ForceClose = true;
                progressWindow.Close();
            }
        }
    }
}