namespace Fds2AcadPlugin.UserInterface
{
    using System;
    using System.Windows.Forms;
    using BLL;
    using BLL.Helpers;
    using BLL.Configuration;
    using System.ComponentModel;
    using System.IO;

    public partial class PluginOptions : Form
    {
        #region Constants

        private const string Int32ValidationError = "Please, enter correct integer value.";

        private const string StringValidationError = "Please, specify path to executable";

        #endregion

        #region Constructor

        public PluginOptions()
        {
            InitializeComponent();
        }

        #endregion

        #region Event handling

        private void On_PluginOptions_Load(object sender, EventArgs e)
        {
            var config = new DefaultFactory().CreateFdsConfig() ?? new FdsPluginConfig { ElementSize = 100, DevicesDensity = 4 };

            tbFdsPath.Text = config.PathToFds;
            tbSmokeViewPath.Text = config.PathToSmokeView;

            chbElementSize.Checked = config.UseCustomElementSize;
            gbElementSize.Enabled = config.UseCustomElementSize;
            tbElementSize.Text = config.ElementSize.ToString();

            chbDevicesDensity.Checked = config.DefineCustomDevicesDensity;
            gbDevicesDensity.Enabled = config.DefineCustomDevicesDensity;
            tbDevicesDensity.Text = config.DevicesDensity.ToString();
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
            var fdsPluginConfig = new FdsPluginConfig
                                      {
                                          PathToFds = tbFdsPath.Text,
                                          PathToSmokeView = tbSmokeViewPath.Text,
                                          UseCustomElementSize = chbElementSize.Checked,
                                          ElementSize = Int32.Parse(tbElementSize.Text),
                                          DefineCustomDevicesDensity = chbDevicesDensity.Checked,
                                          DevicesDensity = Int32.Parse(tbDevicesDensity.Text)
                                      };

            XmlSerializer<FdsPluginConfig>.Serialize(fdsPluginConfig, PluginInfoProvider.PathToPluginConfig);

            DialogResult = DialogResult.OK;
        }

        private void On_textBox_TextChanged(object sender, EventArgs e)
        {
            btnSave.Enabled = ValidateChildren();
        }

        private void On_intTextBox_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
            e.Cancel = false;

            int value;

            if (Int32.TryParse((sender as TextBox).Text, out value) && value > 0)
            {
                errorProvider.SetError((Control)sender, string.Empty);
                e.Cancel = false;
            }
            else
            {
                errorProvider.SetError((Control)sender, Int32ValidationError);
                e.Cancel = true;
            }
        }

        private void On_strTextBox_Validating(object sender, CancelEventArgs e)
        {
            errorProvider.SetError((Control)sender, string.Empty);
            e.Cancel = false;

            if (!string.IsNullOrEmpty((sender as TextBox).Text))
            {
                if (File.Exists((sender as TextBox).Text))
                {
                    errorProvider.SetError((Control)sender, string.Empty);
                    e.Cancel = false;
                }
            }
            else
            {
                errorProvider.SetError((Control)sender, StringValidationError);
                e.Cancel = true;
            }
        }

        private void On_chbElementSize_CheckedChanged(object sender, EventArgs e)
        {
            gbElementSize.Enabled = chbElementSize.Checked;
        }

        private void On_chbDevicesDensity_CheckedChanged(object sender, EventArgs e)
        {
            gbDevicesDensity.Enabled = chbDevicesDensity.Checked;
        }

        #endregion
    }
}