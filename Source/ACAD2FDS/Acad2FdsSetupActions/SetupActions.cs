namespace Acad2FdsSetupActions
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Configuration.Install;
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
            RegistryHelper.CreateFdsBranch(Constants.AutoCadApplicationsRegistryKey);
            RegistryHelper.CreateFdsBranch(Constants.AutoCadArchitectureApplicationsRegistryKey);
        }

        public override void Rollback(IDictionary savedState)
        {
            RegistryHelper.RemoveFdsBranch(Constants.AutoCadApplicationsRegistryKey);
            RegistryHelper.RemoveFdsBranch(Constants.AutoCadArchitectureApplicationsRegistryKey);

            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            CheckIfAutoCadIsRunning();
            RegistryHelper.RemoveFdsBranch(Constants.AutoCadApplicationsRegistryKey);
            RegistryHelper.RemoveFdsBranch(Constants.AutoCadArchitectureApplicationsRegistryKey);

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

            CheckIfAutoCadIsRunning:
            if (!CommonHelper.IsAutoCadRunning())
                return;

            var userChoice = MessageBox.Show(Resources.AcadIsRunningMessage,
                                             Resources.InstallPreventionWindowCaption,
                                             MessageBoxButtons.RetryCancel,
                                             MessageBoxIcon.Warning,
                                             MessageBoxDefaultButton.Button1
                );

            if (DialogResult.Retry == userChoice)
                goto CheckIfAutoCadIsRunning;

            throw new OperationCanceledException(Resources.UserSetupCancelation);
        }

        #endregion
    }
}