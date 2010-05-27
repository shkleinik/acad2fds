using System.Windows.Forms;
using System;

namespace Fds2AcadSetupActions.UserInterface
{
    public partial class PluginOptions : Form
    {
        public PluginOptions()
        {
            InitializeComponent();
        }

        private void On_btnBrowseFds_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         Multiselect = false,
                                         Filter = "Executable files|*.exe"
                                         //,InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                                     };

            var dialogResult = openFileDialog.ShowDialog(this);

            if (DialogResult.OK == dialogResult)
                tbFdsPath.Text = openFileDialog.FileName;

        }

        private void On_btnBrowseSvPath_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         Multiselect = false,
                                         Filter = "Executable files|*.exe",
                                         InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                                     };

            var dialogResult = openFileDialog.ShowDialog(this);

            if (DialogResult.OK == dialogResult)
                tbSmokeViewPath.Text = openFileDialog.FileName;

        }

        private void On_btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
