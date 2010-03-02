using System.Windows.Forms;
using System;
using MaterialManager.BLL;

namespace Fds2AcadPlugin.UserInterface.Materials
{
    public partial class MaterialEditor : Form
    {
        public MaterialEditor()
        {
            InitializeComponent();
            cbMaterialType.SelectedIndex = 0;
        }

        public MaterialEditor(Material material)
        {
            cbMaterialType.SelectedIndex = (int)material.MaterialType;
            tbID.Text = material.ID;
            tbConductivity.Text = material.Conductivity.ToString();
            tbSpecificHeat.Text = material.SpecificHeat.ToString();
            tbEmissivity.Text = material.Emissivity.ToString();
            tbDensity.Text = material.Density.ToString();
        }

        public Material Material
        {
            get
            {
                return new Material
                           {
                               ID = tbID.Text,
                               MaterialType = (MaterialType)cbMaterialType.SelectedIndex,
                               Conductivity = Convert.ToDouble(tbConductivity.Text),
                               Density = Convert.ToDouble(tbDensity.Text),
                               Emissivity = Convert.ToDouble(tbEmissivity.Text),
                               SpecificHeat = Convert.ToDouble(tbSpecificHeat.Text)
                           };
            }
        }

        private void On_btnSave_(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}