namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using MaterialManager.BLL;

    public partial class EditRamp : Form
    {
        private EditRamp()
        {
            InitializeComponent();
        }

        public EditRamp(List<Ramp> ramps)
        {
            InitializeComponent();
            dataGridView.DataSource = ramps;
        }

        private void On_btnSave_Click(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
