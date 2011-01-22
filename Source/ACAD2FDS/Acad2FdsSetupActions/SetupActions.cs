namespace Acad2FdsSetupActions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.IO;
    using System.Windows.Forms;
    using BLL;
    using Common;
    using Common.UI;
    using Properties;

    [RunInstaller(true)]
    public partial class SetupActions : Installer
    {
        private const string InstallerProcessName = "msiexec";

        private ILogger Log { get; set; }

        private IWin32Window InstallerMainWindow
        {
            get
            {
                var mainWindowHandle = CommonHelper.GetProcessMainWindowHandle(InstallerProcessName);

                Log.LogInfo("Installer's Main Window Handle - " + mainWindowHandle);

                return new WindowWrapper(mainWindowHandle);
            }
        }

        #region Constructors

        public SetupActions()
        {
            InitializeComponent();

            Log = new Logger();
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

            var userChoice = UserNotifier.ShowRetry(Resources.AcadIsRunningMessage);

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
        private void ShowSetupResult(IList<string> updatedInstances, bool stopInstallation)
        {
            if (updatedInstances.Count <= 0)
            {
                UserNotifier.ShowInfo(InstallerMainWindow, Resources.AcadNotInstalledMessage);

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

                Log.LogInfo(infoMessage);

                UserNotifier.ShowInfo(InstallerMainWindow, infoMessage);
            }
        }

        #endregion
    }
}