namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using BLL.Helpers;
    using MaterialManager.BLL;

    public partial class MaterialMapper : Form
    {
        #region Fields

        private readonly Dictionary<string, Surface> map;
        private readonly List<string> usedMaterials;
        private readonly List<Surface> allMaterials;

        #endregion

        #region Properties

        public Dictionary<string, Surface> MappingMaterials
        {
            get
            {
                return map;
            }
        }

        #endregion

        #region Constructor

        public MaterialMapper(List<string> usedMaterials, List<Surface> allMaterials, Dictionary<string, Surface> existingMapping)
        {
            InitializeComponent();
            this.usedMaterials = usedMaterials ?? new List<string>();
            this.allMaterials = allMaterials ?? new List<Surface>();
            map = existingMapping ?? new Dictionary<string, Surface>();
        }

        #endregion

        #region Handling form's events

        private void On_MaterialMapper_Load(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            dgvMapping.DataSource = usedMaterials.CreateStringWrapperForBinding();

            availableMaterials.DataSource = allMaterials;
            availableMaterials.DisplayMember = "ID";
            foundMaterials.DataPropertyName = "Value";
        }

        #endregion

        #region Handling controls' events

        private void On_btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Internal implementation


        #endregion
    }
}
