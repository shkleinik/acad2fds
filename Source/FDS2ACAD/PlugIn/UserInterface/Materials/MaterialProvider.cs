using System.Collections.Generic;
using System.Windows.Forms;
using MaterialManager.BLL;

namespace Fds2AcadPlugin.UserInterface.Materials
{
    public partial class MaterialProvider : Form
    {
        private readonly List<Material> materialsBase;

        // Note : Add this file with some basic materials to installer
        private const string BASE_PATH = "materials.xml";

        public MaterialProvider()
        {
            InitializeComponent();
            
            materialsBase = MaterialSerializer.DeserializeMaterials(BASE_PATH);
            if(materialsBase == null)
                materialsBase = new List<Material>();

            cbMaterialTypes.SelectedIndex = 0;
            lbMaterials.DisplayMember = "ID";
            On_cbMaterialType_SelectedIndexChanged(null, null);
        }

        private void On_btnAdd_Click(object sender, System.EventArgs e)
        {
            var materialEditor = new MaterialEditor();
            var dialogResult = materialEditor.ShowDialog(this);

            if (dialogResult != DialogResult.OK)
                return;

            materialsBase.Add(materialEditor.Material);
            On_cbMaterialType_SelectedIndexChanged(null, null);
        }

        private void On_btnEdit_Click(object sender, System.EventArgs e)
        {
            var materialEditor = new MaterialEditor();
            var dialogResult = materialEditor.ShowDialog(this);
        }

        private void On_cbMaterialTypes_KeyPressed(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;

        }

        private void On_MaterialProvider_FormClosing(object sender, FormClosingEventArgs e)
        {
            MaterialSerializer.SerializeMaterials(BASE_PATH, materialsBase);
        }

        private void On_cbMaterialType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch(cbMaterialTypes.SelectedIndex)
            {
                case (int)MaterialType.Лесные:

                    var woodType = new List<Material>();

                    foreach (var material in materialsBase)
                    {
                        if (material.MaterialType == MaterialType.Лесные)
                            woodType.Add(material);
                    }

                    lbMaterials.DataSource = woodType;

                    break;

                case (int)MaterialType.Нефтехимические:

                    var oilMaterials = new List<Material>();

                    foreach (var material in materialsBase)
                    {
                        if(material.MaterialType == MaterialType.Нефтехимические)
                            oilMaterials.Add(material);
                    }

                    lbMaterials.DataSource = oilMaterials;

                    break;
            }

        }
    }
}