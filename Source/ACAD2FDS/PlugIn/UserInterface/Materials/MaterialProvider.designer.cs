namespace Fds2AcadPlugin.UserInterface.Materials
{
    partial class MaterialProvider
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEditMaterial = new System.Windows.Forms.Button();
            this.lbMaterials = new System.Windows.Forms.ListBox();
            this.cbMaterialTypes = new System.Windows.Forms.ComboBox();
            this.btnAddMaterial = new System.Windows.Forms.Button();
            this.lblCategory = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabsContainer = new System.Windows.Forms.TabControl();
            this.tpMaterials = new System.Windows.Forms.TabPage();
            this.tpSurfaces = new System.Windows.Forms.TabPage();
            this.btnEditSurface = new System.Windows.Forms.Button();
            this.btnAddSurface = new System.Windows.Forms.Button();
            this.lbSurfaces = new System.Windows.Forms.ListBox();
            this.tabsContainer.SuspendLayout();
            this.tpMaterials.SuspendLayout();
            this.tpSurfaces.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEditMaterial
            // 
            this.btnEditMaterial.Location = new System.Drawing.Point(6, 101);
            this.btnEditMaterial.Name = "btnEditMaterial";
            this.btnEditMaterial.Size = new System.Drawing.Size(75, 23);
            this.btnEditMaterial.TabIndex = 0;
            this.btnEditMaterial.Text = "Edit";
            this.btnEditMaterial.UseVisualStyleBackColor = true;
            this.btnEditMaterial.Click += new System.EventHandler(this.On_btnEditMaterial_Click);
            // 
            // lbMaterials
            // 
            this.lbMaterials.DisplayMember = "ID";
            this.lbMaterials.FormattingEnabled = true;
            this.lbMaterials.Location = new System.Drawing.Point(168, 6);
            this.lbMaterials.Name = "lbMaterials";
            this.lbMaterials.Size = new System.Drawing.Size(210, 264);
            this.lbMaterials.TabIndex = 1;
            // 
            // cbMaterialTypes
            // 
            this.cbMaterialTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMaterialTypes.FormattingEnabled = true;
            this.cbMaterialTypes.Location = new System.Drawing.Point(6, 31);
            this.cbMaterialTypes.Name = "cbMaterialTypes";
            this.cbMaterialTypes.Size = new System.Drawing.Size(121, 21);
            this.cbMaterialTypes.TabIndex = 2;
            this.cbMaterialTypes.SelectedIndexChanged += new System.EventHandler(this.On_cbMaterialType_SelectedIndexChanged);
            // 
            // btnAddMaterial
            // 
            this.btnAddMaterial.Location = new System.Drawing.Point(6, 72);
            this.btnAddMaterial.Name = "btnAddMaterial";
            this.btnAddMaterial.Size = new System.Drawing.Size(75, 23);
            this.btnAddMaterial.TabIndex = 3;
            this.btnAddMaterial.Text = "Add";
            this.btnAddMaterial.UseVisualStyleBackColor = true;
            this.btnAddMaterial.Click += new System.EventHandler(this.On_btnAddMaterial_Click);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(8, 6);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(52, 13);
            this.lblCategory.TabIndex = 4;
            this.lblCategory.Text = "Category:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(298, 295);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.On_btnSave_Click);
            // 
            // tabsContainer
            // 
            this.tabsContainer.Controls.Add(this.tpMaterials);
            this.tabsContainer.Controls.Add(this.tpSurfaces);
            this.tabsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsContainer.Location = new System.Drawing.Point(0, 0);
            this.tabsContainer.Name = "tabsContainer";
            this.tabsContainer.SelectedIndex = 0;
            this.tabsContainer.Size = new System.Drawing.Size(389, 352);
            this.tabsContainer.TabIndex = 6;
            // 
            // tpMaterials
            // 
            this.tpMaterials.BackColor = System.Drawing.SystemColors.Control;
            this.tpMaterials.Controls.Add(this.btnAddMaterial);
            this.tpMaterials.Controls.Add(this.btnSave);
            this.tpMaterials.Controls.Add(this.btnEditMaterial);
            this.tpMaterials.Controls.Add(this.lblCategory);
            this.tpMaterials.Controls.Add(this.lbMaterials);
            this.tpMaterials.Controls.Add(this.cbMaterialTypes);
            this.tpMaterials.Location = new System.Drawing.Point(4, 22);
            this.tpMaterials.Name = "tpMaterials";
            this.tpMaterials.Padding = new System.Windows.Forms.Padding(3);
            this.tpMaterials.Size = new System.Drawing.Size(381, 326);
            this.tpMaterials.TabIndex = 0;
            this.tpMaterials.Text = "Materials";
            // 
            // tpSurfaces
            // 
            this.tpSurfaces.BackColor = System.Drawing.SystemColors.Control;
            this.tpSurfaces.Controls.Add(this.btnEditSurface);
            this.tpSurfaces.Controls.Add(this.btnAddSurface);
            this.tpSurfaces.Controls.Add(this.lbSurfaces);
            this.tpSurfaces.Location = new System.Drawing.Point(4, 22);
            this.tpSurfaces.Name = "tpSurfaces";
            this.tpSurfaces.Padding = new System.Windows.Forms.Padding(3);
            this.tpSurfaces.Size = new System.Drawing.Size(381, 326);
            this.tpSurfaces.TabIndex = 1;
            this.tpSurfaces.Text = "Surfaces";
            // 
            // btnEditSurface
            // 
            this.btnEditSurface.Location = new System.Drawing.Point(9, 56);
            this.btnEditSurface.Name = "btnEditSurface";
            this.btnEditSurface.Size = new System.Drawing.Size(75, 23);
            this.btnEditSurface.TabIndex = 2;
            this.btnEditSurface.Text = "Edit";
            this.btnEditSurface.UseVisualStyleBackColor = true;
            this.btnEditSurface.Click += new System.EventHandler(this.On_btnEditSurface_Click);
            // 
            // btnAddSurface
            // 
            this.btnAddSurface.Location = new System.Drawing.Point(9, 26);
            this.btnAddSurface.Name = "btnAddSurface";
            this.btnAddSurface.Size = new System.Drawing.Size(75, 23);
            this.btnAddSurface.TabIndex = 1;
            this.btnAddSurface.Text = "Add";
            this.btnAddSurface.UseVisualStyleBackColor = true;
            this.btnAddSurface.Click += new System.EventHandler(this.On_btnAddSurface_Click);
            // 
            // lbSurfaces
            // 
            this.lbSurfaces.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSurfaces.DisplayMember = "ID";
            this.lbSurfaces.FormattingEnabled = true;
            this.lbSurfaces.Location = new System.Drawing.Point(166, 6);
            this.lbSurfaces.Name = "lbSurfaces";
            this.lbSurfaces.Size = new System.Drawing.Size(213, 225);
            this.lbSurfaces.TabIndex = 0;
            // 
            // MaterialProvider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 352);
            this.Controls.Add(this.tabsContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(395, 380);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(395, 380);
            this.Name = "MaterialProvider";
            this.Text = "Materials Provider";
            this.Load += new System.EventHandler(this.On_MaterialProvider_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.On_MaterialProvider_FormClosing);
            this.tabsContainer.ResumeLayout(false);
            this.tpMaterials.ResumeLayout(false);
            this.tpMaterials.PerformLayout();
            this.tpSurfaces.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEditMaterial;
        private System.Windows.Forms.ListBox lbMaterials;
        private System.Windows.Forms.ComboBox cbMaterialTypes;
        private System.Windows.Forms.Button btnAddMaterial;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabsContainer;
        private System.Windows.Forms.TabPage tpMaterials;
        private System.Windows.Forms.TabPage tpSurfaces;
        private System.Windows.Forms.ListBox lbSurfaces;
        private System.Windows.Forms.Button btnEditSurface;
        private System.Windows.Forms.Button btnAddSurface;
    }
}