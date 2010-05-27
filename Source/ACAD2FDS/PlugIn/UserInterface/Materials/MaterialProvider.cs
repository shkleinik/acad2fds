namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using BLL;
    using MaterialManager.BLL;
    using System.IO;

    public partial class MaterialProvider : Form
    {
        private readonly List<Surface> materialsBase;

        public MaterialProvider()
        {
            InitializeComponent();

            var materials = Path.Combine(AcadInfoProvider.GetPathToPluginDirectory(), Constants.MaterialsBasePath);
            materialsBase = MaterialSerializer.DeserializeMaterials(materials);

            if (materialsBase == null)
                materialsBase = new List<Surface>();

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

            materialsBase.Add(materialEditor.Surface);
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
            MaterialSerializer.SerializeMaterials(Constants.MaterialsBasePath, materialsBase);
        }

        private void On_cbMaterialType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            switch (cbMaterialTypes.SelectedIndex)
            {
                case (int)MaterialCategory.Gas:

                    var woodType = new List<Surface>();

                    foreach (var material in materialsBase)
                    {
                        if (material.MaterialCategory == MaterialCategory.Gas)
                            woodType.Add(material);
                    }

                    lbMaterials.DataSource = woodType;

                    break;

                case (int)MaterialCategory.LiquidFuel:

                    var oilMaterials = new List<Surface>();

                    foreach (var material in materialsBase)
                    {
                        if (material.MaterialCategory == MaterialCategory.LiquidFuel)
                            oilMaterials.Add(material);
                    }

                    lbMaterials.DataSource = oilMaterials;

                    break;
            }

        }
    }
}