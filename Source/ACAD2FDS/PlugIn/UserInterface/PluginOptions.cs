namespace Fds2AcadPlugin.UserInterface
{
    using System;
    using System.Windows.Forms;
    using BLL;
    using BLL.Helpers;
    using BLL.Configuration;

    public partial class PluginOptions : Form
    {
        #region Constructor

        public PluginOptions()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handling

        private void On_PluginOptions_Load(object sender, EventArgs e)
        {
            var config = new DefaultFactory().CreateFdsConfig();

            tbFdsPath.Text = config.PathToFds;
            tbSmokeViewPath.Text = config.PathToSmokeView;
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


            XmlSerializer<FdsPluginConfig>.Serialize(fdsPluginConfig, PluginInfoProvider.PathToPluginConfig);

            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}