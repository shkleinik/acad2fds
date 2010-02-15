using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Windows.Forms;

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
            MessageBox.Show("Hello, Wolrld! I'm from Istall phase");
        }

        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            MessageBox.Show("Hello, Wolrld! I'm from Commit phase");
        }

        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
            MessageBox.Show("Hello, Wolrld! I'm from Rollback phase");
        }

        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            MessageBox.Show("Hello, Wolrld! I'm from Unistall phase");
        }
        #endregion
    }
}
