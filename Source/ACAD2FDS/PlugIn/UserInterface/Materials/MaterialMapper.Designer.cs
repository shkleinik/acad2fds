using System.Windows.Forms;

namespace Fds2AcadPlugin.UserInterface.Materials
{
    partial class MaterialMapper
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
            this.btnApply = new System.Windows.Forms.Button();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.dgvMapping = new System.Windows.Forms.DataGridView();
            this.usedMaterialsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.availableMaterials = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.pnlBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).BeginInit();
            this.SuspendLayout();
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(497, 3);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 4;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.On_btnApply_Click);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnApply);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 274);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(584, 38);
            this.pnlBottom.TabIndex = 7;
            // 
            // dgvMapping
            // 
            this.dgvMapping.AllowUserToAddRows = false;
            this.dgvMapping.AllowUserToDeleteRows = false;
            this.dgvMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMapping.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usedMaterialsColumn,
            this.availableMaterials});
            this.dgvMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMapping.Location = new System.Drawing.Point(0, 0);
            this.dgvMapping.Name = "dgvMapping";
            this.dgvMapping.Size = new System.Drawing.Size(584, 274);
            this.dgvMapping.TabIndex = 8;
            this.dgvMapping.AutoGenerateColumns = false;
            this.dgvMapping.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // 
            // usedMaterialsColumn
            // 
            this.usedMaterialsColumn.HeaderText = "Found Materials";
            this.usedMaterialsColumn.Name = "usedMaterialsColumn";
            this.usedMaterialsColumn.ReadOnly = true;
            this.usedMaterialsColumn.Width = 300;
            // 
            // availableMaterials
            // 
            this.availableMaterials.HeaderText = "Available Materials";
            this.availableMaterials.Name = "availableMaterials";
            this.availableMaterials.Width = 200;
            // this.availableMaterials.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            // this.availableMaterials.FlatStyle = FlatStyle.Flat;
            // 
            // MaterialMapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 312);
            this.Controls.Add(this.dgvMapping);
            this.Controls.Add(this.pnlBottom);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "MaterialMapper";
            this.Text = "Material Mappings";
            this.Load += new System.EventHandler(this.On_MaterialMapper_Load);
            this.pnlBottom.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMapping)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnApply;
        private Panel pnlBottom;
        private DataGridView dgvMapping;
        private DataGridViewTextBoxColumn usedMaterialsColumn;
        private DataGridViewComboBoxColumn availableMaterials;
    }
}