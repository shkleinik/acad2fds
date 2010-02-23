namespace Fds2AcadPlugin.UserInterface
{
    using System.Windows.Forms;
    using System;
    using BLL.Configuration;

    public partial class PluginOptions : Form
    {
        public PluginOptions()
        {
            InitializeComponent();

            var fdsPluginConfig = new FdsPluginConfig();
            fdsPluginConfig.InitializeFromFile();

            tbFdsPath.Text = fdsPluginConfig.PathToFds;
            tbSmokeViewPath.Text = fdsPluginConfig.PathToFds;
        }

        private void On_btnBrowseFds_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                                     {
                                         Multiselect = false,
                                         Filter = "Executable files|*.exe",
                                         InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
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

            if (string.IsNullOrEmpty(tbFdsPath.Text))
            {
                MessageBox.Show("Please, specify path to FDS",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }

            if (string.IsNullOrEmpty(tbSmokeViewPath.Text))
            {
                MessageBox.Show("Please, specify path to SmokeView",
                                "Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                return;
            }


            var fdsPluginConfig = new FdsPluginConfig
                                      {
                                          PathToFds = tbFdsPath.Text,
                                          PathToSmokeView = tbSmokeViewPath.Text
                                      };


            fdsPluginConfig.Save();

            DialogResult = DialogResult.OK;
        }
    }
}