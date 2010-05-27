namespace Fds2AcadPlugin.UserInterface
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;

    public partial class CalculationInfo : Form
    {
        #region Properties

        public string OutputPath
        {
            get
            {
                return tbPath.Text;
            }
        }

        public int CalculationTime
        {
            get
            {
                return int.Parse(tbTime.Text);
            }
        }

        #endregion

        #region Constructors

        public CalculationInfo()
        {
            InitializeComponent();
        }

        #endregion

        #region Events handling

        private void On_CalculationInfo_Load(object sender, EventArgs e)
        {
#if DEBUG

            tbPath.Text = @"C:\!FdsTest";
            tbTime.Text = "0";
#endif
            tbTime.Focus();
            tbTime.SelectAll();
            btnStart.Enabled = ValidateChildren();
        }

        private void On_tbTime_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    DialogResult = DialogResult.OK;
                    break;
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    break;
                default:
                    break;
            }
        }

        private void On_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void On_btnStart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void On_tbTime_TextChanged(object sender, EventArgs e)
        {
            btnStart.Enabled = ValidateChildren();
        }

        private void On_btnBrowse_Click(object sender, EventArgs e)
        {
            var browserDialog = new FolderBrowserDialog();
            var dialogResult = browserDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
                tbPath.Text = browserDialog.SelectedPath;

            btnStart.Enabled = ValidateChildren();
        }

        #endregion

        #region User input validation

        private void On_tbTime_Validating(object sender, CancelEventArgs e)
        {
            if (Directory.Exists(tbPath.Text))
            {
                errorProvider.SetError(tbPath, string.Empty);
                e.Cancel = false;
            }
            else
            {
                errorProvider.SetError(tbPath, "Please, enter folder to save output.");
                e.Cancel = true;
            }
        }


        private void On_tbPath_Validating(object sender, CancelEventArgs e)
        {
            int calculationTime;

            if (Int32.TryParse(tbTime.Text, out calculationTime))
            {
                errorProvider.SetError(tbTime, string.Empty);
                e.Cancel = false;
            }
            else
            {
                errorProvider.SetError(tbTime, "Please, enter correct calculation time.");
                e.Cancel = true;
            }
        }

        #endregion
    }
}
