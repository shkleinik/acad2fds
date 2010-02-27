using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Fds2AcadPlugin.UserInterface
{
    public partial class CalculationInfo : Form
    {
        public CalculationInfo()
        {
            InitializeComponent();
        }

        private void On_btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void On_btnStart_Click(object sender, EventArgs e)
        {
            ValidateTime();
            ValidatePath();
        }

        private void ValidatePath()
        {
            throw new NotImplementedException();
        }

        public void ValidateTime()
        {
            int calculationTime;
            if(Int32.TryParse(tbTime.Text,out calculationTime))
            {
                errorProvider.SetError(tbTime, string.Empty);
            }
            else
            {
                errorProvider.SetError(tbTime, "Please, enter correct calculation time.");
            }
        }

        private void On_tbTime_TextChanged(object sender, EventArgs e)
        {
            ValidateTime();
        }
    }
}
