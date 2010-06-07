namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using MaterialManager.BLL;
    using System.ComponentModel;

    public partial class EditRamp : Form
    {
        #region Fields

        private readonly List<Ramp> ramps;

        private BindingList<Ramp> dataSource;

        #endregion

        #region Properties

        public List<Ramp> Ramps
        {
            get { return ramps; }
        }

        #endregion

        #region Constructors

        private EditRamp()
        {
            InitializeComponent();
        }

        public EditRamp(List<Ramp> ramps)
        {
            InitializeComponent();

            if(ramps == null)
                ramps = new List<Ramp>();

            this.ramps = ramps;
        }

        #endregion

        #region Events Handling

        private void On_EditRamp_Load(object sender, System.EventArgs e)
        {
            dataSource = new BindingList<Ramp> { AllowNew = true };

            foreach (var ramp in ramps)
            {
                dataSource.Add(ramp);
            }

            dataGridView.DataSource = dataSource;
        }

        private void On_dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            MessageBox.Show("Please, enter correct value");
        }

        private void On_btnSave_Click(object sender, System.EventArgs e)
        {
            ramps.Clear();
            ramps.AddRange(dataSource);

            DialogResult = DialogResult.OK;
        }

        #endregion
    }
}
