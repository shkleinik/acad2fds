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
        private const string GuiThreadName = "Gui Thread";
        private const string CalculationThreadName = "Calculation Thread";

        private ManualResetEvent waitEvent;
        private ConversionProgress progressWindow;
        private Thread guiThread;
        private Thread calculationThread;
        private readonly FdsStartInfo fdsStartInfo;
        private readonly ILogger log;

        public ThreadedCalculationManager(FdsStartInfo fdsStartInfo, ILogger logger)
        {
            log = logger;
            this.fdsStartInfo = fdsStartInfo;
        }

        public void StartCalculation()
        {
            waitEvent = new ManualResetEvent(false);

            guiThread = new Thread(GuiThread);
            guiThread.Name = GuiThreadName;
            guiThread.Start();

            calculationThread = new Thread(CalculationThread);
            calculationThread.Name = CalculationThreadName;
            calculationThread.Start();
        }

        private void GuiThread()
        {
            progressWindow = new ConversionProgress(fdsStartInfo.SelectedSolids.Count);
            progressWindow.Shown += (s, e) => waitEvent.Set();
            progressWindow.ShowDialog();
            progressWindow.Dispose();
            progressWindow = null;

            if (calculationThread.IsAlive)
                calculationThread.Abort();
        }

        private void CalculationThread()
        {
            waitEvent.WaitOne(); // Waiting for gui thread.

            //for (var i = 0; i < Count; i += 1000)
            //{
            //    var message = string.Format("{0} of {1} steps passed:", i, Count);

            //    if (progressWindow != null)
            //        progressWindow.Update(i, message);
            //}

            if (fdsStartInfo == null)
            {
                log.LogInfo("Calculation was not started because no start info was provided.");
                return;
            }

            var selectedSolids = fdsStartInfo.SelectedSolids;

            if (selectedSolids == null || selectedSolids.Count < 1)
            {
                log.LogInfo("No solids to convers were provided.");
                return;
            }

            var pluginConfig = new DefaultFactory(log).CreateFdsConfig();

            if (pluginConfig == null)
            {
                log.LogInfo("No confign was provided.");
                return;
            }

            #region Convert geometry

            var allOptimizedElements = new List<Element>();
            var minMaxPoint = SolidToElementConverter.GetMaxMinPoint(selectedSolids);
            var progress = 0;

            foreach (var solid in selectedSolids)
            {
                Thread.Sleep(300);

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

            var startInfo = new ProcessStartInfo
            {
                Arguments = string.Concat("\"", pathToFile, "\""),//fdsStartInfo.Arguments,
                FileName = fdsStartInfo.PathToFds,
                WorkingDirectory = fdsStartInfo.WorkingDirectory
            };

            Process.Start(startInfo);

            if (progressWindow != null)
            {
                progressWindow.ForceClose = true;
                progressWindow.Close();
            }
        }

        public void Dispose()
        {
            if (guiThread.IsAlive)
            {
                guiThread.Abort();
                guiThread = null;
            }

            if (calculationThread.IsAlive)
            {
                calculationThread.Abort();
                calculationThread = null;
            }
        }
    }
}