using Common;

namespace Acad2FdsSetupActions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using BLL;
    using Properties;

    [RunInstaller(true)]
    public partial class SetupActions : Installer
    {
        #region Constructors

        public SetupActions()
        {
            InitializeComponent();
        }

        #endregion

        #region Overrides

        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);

            if (!RegistryHelper.IsAutoCadInstalled())
                throw new InvalidOperationException(Resources.AcadNotInstalledMessage);

            CheckIfAutoCadIsRunning();

            // Note: Add logging here
            var pluginLocation = Context.Parameters["targetdir"];
            var pathToPluginAssembly = Path.Combine(pluginLocation, Constants.FdsPluginAssemblyName);

            var updatedInstances = RegistryHelper.CreateFdsBranchForEachAutoCadInstance(pathToPluginAssembly.Replace(@"\\", @"\")); // Todo : find out why path combine works incorrectly.

            ShowSetupResult(updatedInstances, true);
        }

        public override void Rollback(IDictionary savedState)
        {
            var updatedInstances = RegistryHelper.RemoveFdsBranchFromEachAutoCadInstance();
            ShowSetupResult(updatedInstances, false);

            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            CheckIfAutoCadIsRunning();
            var updatedInstances = RegistryHelper.RemoveFdsBranchFromEachAutoCadInstance();
            ShowSetupResult(updatedInstances, false);

            base.Uninstall(savedState);
        }

        #endregion

        #region Internal implementation

        /// <summary>
        /// Checks if Autocad process is running. If not continues code runing in other
        /// offer user to close AutoCad instance.
        /// </summary>
        public void CheckIfAutoCadIsRunning()
        {
            if (!CommonHelper.IsAutoCadRunning())
                return;

            var userChoice = MessageBox.Show(Resources.AcadIsRunningMessage,
                                             Resources.InstallPreventionWindowCaption,
                                             MessageBoxButtons.RetryCancel,
                                             MessageBoxIcon.Warning,
                                             MessageBoxDefaultButton.Button1
                                            );

            if (DialogResult.Retry == userChoice)
                CheckIfAutoCadIsRunning();
            else
                throw new OperationCanceledException(Resources.UserSetupCancelation);
        }

        /// <summary>
        /// Informs user about actions perfomed under the registry.
        /// </summary>
        /// <param name="updatedInstances">List of registry keys on whi</param>
        /// <param name="stopInstallation">Indicates if it necessary to exit setup if error occurs.</param>
        private static void ShowSetupResult(IList<string> updatedInstances, bool stopInstallation)
        {
            if (updatedInstances.Count <= 0)
            {
                MessageBox.Show(Resources.AcadNotInstalledMessage,
                Resources.InfoCaption,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1
                );

                if (stopInstallation)
                    throw new InvalidOperationException(Resources.AcadNotInstalledMessage);
            }
            else
            {
                var infoMessage = "Following AutoCAD instances were updated:\n"; // Todo : Replace with resources.

                foreach (var updatedInstance in updatedInstances)
                {
                    infoMessage = string.Concat(infoMessage, updatedInstance, Environment.NewLine);
                }

                StaticLogger.LogInfo(infoMessage);

                MessageBox.Show(infoMessage,
                                Resources.InfoCaption,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information,
                                MessageBoxDefaultButton.Button1
                    );
            }
        }

        #endregion
    }
}