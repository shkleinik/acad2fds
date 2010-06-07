using System.ComponentModel;

namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using MaterialManager.BLL;

    public partial class MaterialProvider : Form
    {
        #region Fields

        private readonly List<Surface> surfacesStore;

        private readonly List<Material> materialsStore;

        #endregion

        #region Properties

        public List<Material> MaterialsStore
        {
            get
            {
                return materialsStore;
            }
        }
        
        public List<Surface> SurfacesStore
        {
            get
            {
                return surfacesStore;
            }
        }

        #endregion

        #region Constructors

        private MaterialProvider() { }

        public MaterialProvider(List<Material> materialsStore, List<Surface> surfacesStore)
        {
            InitializeComponent();

            this.materialsStore = materialsStore;
            this.surfacesStore = surfacesStore;
        }

        #endregion

        #region Form's events handling

        private void On_MaterialProvider_Load(object sender, EventArgs e)
        {
            InitMaterialCategoryComboBox();
            On_cbMaterialType_SelectedIndexChanged(null, null);

            lbSurfaces.DataSource = surfacesStore;
        }

        private void On_MaterialProvider_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Controls events handling

        private void On_btnAddMaterial_Click(object sender, EventArgs e)
        {
            var materialEditor = new MaterialEditor();
            var dialogResult = materialEditor.ShowDialog(this);

            if (dialogResult != DialogResult.OK)
                return;

            materialsStore.Add(materialEditor.Material);
            // UpdateSurfacesListBox();
            On_cbMaterialType_SelectedIndexChanged(null, null);
        }

        private void On_btnEditMaterial_Click(object sender, EventArgs e)
        {
            var materialEditor = new MaterialEditor((Material)lbMaterials.SelectedItem);
            materialEditor.ShowDialog(this);
        }

        private void On_btnAddSurface_Click(object sender, EventArgs e)
        {
            var surfaceEditor = new SurfaceEditor();
            var dialogResult = surfaceEditor.ShowDialog(this);

            if (dialogResult != DialogResult.OK)
                return;

            surfacesStore.Add(surfaceEditor.Surface);

            UpdateSurfacesListBox();
        }

        private void On_btnEditSurface_Click(object sender, EventArgs e)
        {
            var surfaceEditor = new SurfaceEditor((Surface) lbSurfaces.SelectedItem);
            surfaceEditor.ShowDialog(this);
        }

        private void On_cbMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMaterialTypes.SelectedIndex)
            {
                case (int)MaterialCategory.Gas:

                    lbMaterials.DataSource = GetMaterialsByCategory(MaterialCategory.Gas);
                    lbMaterials.ResetBindings();

                    break;

                case (int)MaterialCategory.LiquidFuel:

                    lbMaterials.DataSource = GetMaterialsByCategory(MaterialCategory.LiquidFuel);
                    lbMaterials.ResetBindings();

                    break;

                case (int)MaterialCategory.SolidFuel:

                    lbMaterials.DataSource = GetMaterialsByCategory(MaterialCategory.SolidFuel);
                    lbMaterials.ResetBindings();

                    break;

                case (int)MaterialCategory.Engineering:

                    lbMaterials.DataSource = GetMaterialsByCategory(MaterialCategory.Engineering);
                    lbMaterials.ResetBindings();

                    break;
                default:
                    throw new NotSupportedException("This material category is not supported.");
            }
        }

        private void On_btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Internal implementation

        private void InitMaterialCategoryComboBox()
        {
            var enumValues = Enum.GetValues(typeof(MaterialCategory));

            foreach (var value in enumValues)
            {
                cbMaterialTypes.Items.Add(value);
            }

            cbMaterialTypes.SelectedIndex = 0;
        }

        private IList<Material> GetMaterialsByCategory(MaterialCategory category)
        {
            var materialsByCategory = from material in materialsStore
                                      where material.MaterialCategory == category
                                      select material;

            return materialsByCategory.ToList();
        }

        private void UpdateSurfacesListBox()
        {
            lbSurfaces.DataSource = null;
            lbSurfaces.DataSource = surfacesStore;
            lbSurfaces.DisplayMember = "ID";
            lbSurfaces.ResetBindings();
        }

        #endregion
    }
}