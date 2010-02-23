using System.Threading;

namespace Fds2AcadSetupActions
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Configuration.Install;
    using System.Windows.Forms;
    using BLL;
    using Properties;
    using Fds2AcadPlugin.UserInterface;

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

            // Note: Remove this stupid messagebox
            // MessageBox.Show("Hello, Wolrld! I'm from Istall phase");

            if (!RegistryHelper.IsAutoCadInstalled())
                throw new InvalidOperationException("You have no AutoCad installed. The installation will be canceled.");

        CheckIfAutoCadIsRunning:
            if (CommonHelper.IsAutoCadRunning())
            {
                var userChoice = MessageBox.Show(Resources.AcadIsRunningMessage,
                                Resources.InstallPreventionWindowCaption,
                                MessageBoxButtons.RetryCancel,
                                MessageBoxIcon.Warning,
                                MessageBoxDefaultButton.Button1
                                );

                if (DialogResult.Retry == userChoice)
                    goto CheckIfAutoCadIsRunning;

                throw new OperationCanceledException("Setup has been cancelled by the user.");
            }


            // Note: Add logging here
            RegistryHelper.CreateFdsBranch();
            AcadAutoLoadModifier.AddCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand);

            // Note: need some work around to configure fds.
            //var thread = new Thread(ConfigureFds);

            //thread.SetApartmentState(ApartmentState.STA);

            //thread.Start();
        }

        //private void ConfigureFds()
        //{
        //    // Note: some magic here
        //    var plugionOptions = new PluginOptions();

        //    if (plugionOptions.ShowDialog() != DialogResult.OK)
        //        throw new OperationCanceledException("Setup has been cancelled by the user.");
        //}

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            // MessageBox.Show("Hello, Wolrld! I'm from Commit phase");
        }

        public override void Rollback(IDictionary savedState)
        {
            // Note: Remove this stupid messagebox
            // MessageBox.Show("Hello, Wolrld! I'm from Rollback phase");

            RegistryHelper.RemoveFdsBranch();
            AcadAutoLoadModifier.RemoveCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand);

            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            // Note: Remove this stupid messagebox
            // MessageBox.Show("Hello, Wolrld! I'm from Unistall phase");

            RegistryHelper.RemoveFdsBranch();
            AcadAutoLoadModifier.RemoveCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand);

            base.Uninstall(savedState);
        }

        #endregion
    }
}
