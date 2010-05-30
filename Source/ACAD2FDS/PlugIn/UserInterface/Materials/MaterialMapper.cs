namespace Fds2AcadPlugin.UserInterface.Materials
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
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
            chlbAvaileable.DataSource = allMaterials;
            chlbAvaileable.DisplayMember = "ID";

            lbFounded.DataSource = usedMaterials;
        }

        #endregion

        #region Handling controls' events

        private void On_lbFounded_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (map.ContainsKey((string)lbFounded.SelectedValue))
            {
                UncheckAll();
                // 
                var indexToCheck = chlbAvaileable.Items.IndexOf(map[(string)lbFounded.SelectedValue]);
                if(indexToCheck > 0)
                    chlbAvaileable.SetItemCheckState(indexToCheck, CheckState.Checked);
            }
            else
            {
                if (allMaterials.Count > 0)
                {
                    map[(string)lbFounded.SelectedValue] = allMaterials[0];
                    chlbAvaileable.SetItemCheckState(0, CheckState.Checked);
                }
            }
        }

        private void On_chlbAvaileable_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (chlbAvaileable.CheckedItems.Count == 0 || e.CurrentValue != CheckState.Unchecked)
                return;

            // Todo : Add condition to forbid uncheck items
            // if (chlbAvaileable.CheckedItems.Count == 1 && e.CurrentValue != CheckState.Checked)
            //     chlbAvaileable.SetItemChecked(e.Index, true);

            chlbAvaileable.SetItemCheckState(chlbAvaileable.CheckedIndices[0], CheckState.Unchecked);

            if (lbFounded.Items.Count > 0)
                map[(string)lbFounded.SelectedItem] = (Surface)chlbAvaileable.Items[e.Index];
        }

        private void On_btnApply_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        #endregion

        #region Internal implementation

        private void UncheckAll()
        {
            var myEnumerator = chlbAvaileable.CheckedIndices.GetEnumerator();

            while (myEnumerator.MoveNext())
            {
                chlbAvaileable.SetItemChecked((int)myEnumerator.Current, false);
            }
        }

        #endregion
    }
}
