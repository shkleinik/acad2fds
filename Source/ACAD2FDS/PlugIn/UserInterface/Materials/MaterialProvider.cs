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

        private readonly List<Surface> materialsStore;

        #endregion

        #region Properties

        public List<Surface> MaterialsStore
        {
            get
            {
                return materialsStore;
            }
        }

        #endregion

        #region Constructors

        private MaterialProvider() { }

        public MaterialProvider(List<Surface> materialsStore)
        {
            InitializeComponent();
            this.materialsStore = materialsStore;
        }

        #endregion

        #region Form's events handling

        private void On_MaterialProvider_Load(object sender, EventArgs e)
        {
            InitMaterialCategoryComboBox();
        }

        private void On_MaterialProvider_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Controls events handling

        private void On_btnAdd_Click(object sender, EventArgs e)
        {
            var materialEditor = new MaterialEditor();
            var dialogResult = materialEditor.ShowDialog(this);

            if (dialogResult != DialogResult.OK)
                return;

            materialsStore.Add(materialEditor.Surface);
            On_cbMaterialType_SelectedIndexChanged(null, null);
        }

        private void On_btnEdit_Click(object sender, EventArgs e)
        {
            var materialEditor = new MaterialEditor((Surface)lbMaterials.SelectedItem);
            materialEditor.ShowDialog(this);
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
            lbMaterials.DisplayMember = "ID";
            On_cbMaterialType_SelectedIndexChanged(null, null);
        }

        private IList<Surface> GetMaterialsByCategory(MaterialCategory category)
        {
            var materialsForCategory = from surface in materialsStore
                                       where surface.MaterialCategory == category
                                       select surface;

            return materialsForCategory.ToList();
        }

        #endregion
    }
}