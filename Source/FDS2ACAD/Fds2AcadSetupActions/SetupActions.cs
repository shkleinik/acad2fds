using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;
using Fds2AcadSetupActions.BLL;
using Fds2AcadSetupActions.Properties;
using Fds2AcadSetupActions.UserInterface;

namespace Fds2AcadSetupActions
{
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
            MessageBox.Show("Hello, Wolrld! I'm from Istall phase");

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
            RegistryHelper.CreateFdsBrunch();
            AcadAutoLoadModifier.AddCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand);

            // Note: some magic here
            //DialogResult dialogResult = DialogResult.Cancel;
            //var plugionOptions = new PluginOptions();
            //try
            //{
            //    dialogResult = plugionOptions.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            //if(dialogResult != DialogResult.OK)
            //    throw new OperationCanceledException("Setup has been cancelled by the user.");

        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            MessageBox.Show("Hello, Wolrld! I'm from Commit phase");
        }

        public override void Rollback(IDictionary savedState)
        {
            // Note: Remove this stupid messagebox
            MessageBox.Show("Hello, Wolrld! I'm from Rollback phase");

            RegistryHelper.RemoveFdsBrunch();
            AcadAutoLoadModifier.RemoveCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand);

            base.Rollback(savedState);
        }

        public override void Uninstall(IDictionary savedState)
        {
            // Note: Remove this stupid messagebox
            MessageBox.Show("Hello, Wolrld! I'm from Unistall phase");

            RegistryHelper.RemoveFdsBrunch();
            AcadAutoLoadModifier.RemoveCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand);

            base.Uninstall(savedState);
        }

        #endregion
    }
}
