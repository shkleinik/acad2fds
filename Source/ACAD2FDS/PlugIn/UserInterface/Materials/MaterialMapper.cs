namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using BLL.Helpers;
    using Common.UI;
    using MaterialManager.BLL;

    public partial class MaterialMapper : FormBase
    {
        #region Fields

        private readonly List<MaterialAndSurface> map;
        private readonly List<string> usedMaterials;
        private readonly List<Surface> allSurfaces;

        #endregion

        #region Properties

        public List<MaterialAndSurface> MappingMaterials
        {
            get
            {
                return map;
            }
        }

        #endregion

        #region Constructor

        public MaterialMapper(List<string> usedMaterials, List<Surface> allSurfaces, List<MaterialAndSurface> existingMapping)
        {
            InitializeComponent();
            this.usedMaterials = usedMaterials ?? new List<string>();
            this.allSurfaces = allSurfaces ?? new List<Surface>();
            map = existingMapping ?? new List<MaterialAndSurface>();
        }

        #endregion

        #region Handling form's events

        private void On_MaterialMapper_Load(object sender, EventArgs e)
        {
            Application.EnableVisualStyles();
            dgvMapping.DataSource = CreateDataSource();

            usedMaterialsColumn.DataPropertyName = "MaterialName";

            availableMaterials.DataSource = allSurfaces.GetSurfacesIDs().CreateStringWrapperForBinding();
            availableMaterials.DisplayMember = "Value";
            availableMaterials.DataPropertyName = "SurfaceName";
        }

        #endregion

        #region Handling controls' events

        private void On_btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Internal Implementation

        private List<MaterialAndSurface> CreateDataSource()
        {
            foreach (var materialName in usedMaterials)
            {
                if (map.Find(mapItem => mapItem.MaterialName == materialName) == null)
                    map.Add(new MaterialAndSurface { MaterialName = materialName, SurfaceName = allSurfaces[0].ID });
            }

            return map;
        }

        #endregion
    }
}
