namespace Fds2AcadPlugin.UserInterface.Materials
{
    partial class MaterialEditor
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
            this.btnSave = new System.Windows.Forms.Button();
            this.lbMaterialType = new System.Windows.Forms.Label();
            this.tbID = new System.Windows.Forms.TextBox();
            this.cbMaterialType = new System.Windows.Forms.ComboBox();
            this.lbID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbConductivity = new System.Windows.Forms.TextBox();
            this.lbSpecificHeat = new System.Windows.Forms.Label();
            this.tbSpecificHeat = new System.Windows.Forms.TextBox();
            this.lbEmissivity = new System.Windows.Forms.Label();
            this.tbEmissivity = new System.Windows.Forms.TextBox();
            this.lbDensity = new System.Windows.Forms.Label();
            this.tbDensity = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(237, 252);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.On_btnSave_);
            // 
            // lbMaterialType
            // 
            this.lbMaterialType.AutoSize = true;
            this.lbMaterialType.Location = new System.Drawing.Point(12, 40);
            this.lbMaterialType.Name = "lbMaterialType";
            this.lbMaterialType.Size = new System.Drawing.Size(84, 13);
            this.lbMaterialType.TabIndex = 1;
            this.lbMaterialType.Text = "Тип материала";
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(172, 73);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(121, 20);
            this.tbID.TabIndex = 2;
            // 
            // cbMaterialType
            // 
            this.cbMaterialType.FormattingEnabled = true;
            this.cbMaterialType.Items.AddRange(new object[] {
            "Лесной",
            "Нефтехимический"});
            this.cbMaterialType.Location = new System.Drawing.Point(172, 40);
            this.cbMaterialType.Name = "cbMaterialType";
            this.cbMaterialType.Size = new System.Drawing.Size(121, 21);
            this.cbMaterialType.TabIndex = 3;
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(12, 80);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(87, 13);
            this.lbID.TabIndex = 4;
            this.lbID.Text = "Идентификатор";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Удельная теплопроводность";
            // 
            // tbConductivity
            // 
            this.tbConductivity.Location = new System.Drawing.Point(172, 110);
            this.tbConductivity.Name = "tbConductivity";
            this.tbConductivity.Size = new System.Drawing.Size(121, 20);
            this.tbConductivity.TabIndex = 6;
            // 
            // lbSpecificHeat
            // 
            this.lbSpecificHeat.AutoSize = true;
            this.lbSpecificHeat.Location = new System.Drawing.Point(12, 138);
            this.lbSpecificHeat.Name = "lbSpecificHeat";
            this.lbSpecificHeat.Size = new System.Drawing.Size(81, 13);
            this.lbSpecificHeat.TabIndex = 7;
            this.lbSpecificHeat.Text = "Теплоемкость";
            // 
            // tbSpecificHeat
            // 
            this.tbSpecificHeat.Location = new System.Drawing.Point(172, 138);
            this.tbSpecificHeat.Name = "tbSpecificHeat";
            this.tbSpecificHeat.Size = new System.Drawing.Size(121, 20);
            this.tbSpecificHeat.TabIndex = 8;
            // 
            // lbEmissivity
            // 
            this.lbEmissivity.AutoSize = true;
            this.lbEmissivity.Location = new System.Drawing.Point(12, 166);
            this.lbEmissivity.Name = "lbEmissivity";
            this.lbEmissivity.Size = new System.Drawing.Size(122, 13);
            this.lbEmissivity.TabIndex = 9;
            this.lbEmissivity.Text = "Коэффициент черноты";
            // 
            // tbEmissivity
            // 
            this.tbEmissivity.Location = new System.Drawing.Point(172, 166);
            this.tbEmissivity.Name = "tbEmissivity";
            this.tbEmissivity.Size = new System.Drawing.Size(121, 20);
            this.tbEmissivity.TabIndex = 10;
            // 
            // lbDensity
            // 
            this.lbDensity.AutoSize = true;
            this.lbDensity.Location = new System.Drawing.Point(15, 202);
            this.lbDensity.Name = "lbDensity";
            this.lbDensity.Size = new System.Drawing.Size(61, 13);
            this.lbDensity.TabIndex = 11;
            this.lbDensity.Text = "Плотность";
            // 
            // tbDensity
            // 
            this.tbDensity.Location = new System.Drawing.Point(172, 202);
            this.tbDensity.Name = "tbDensity";
            this.tbDensity.Size = new System.Drawing.Size(121, 20);
            this.tbDensity.TabIndex = 12;
            // 
            // MaterialEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 297);
            this.Controls.Add(this.tbDensity);
            this.Controls.Add(this.lbDensity);
            this.Controls.Add(this.tbEmissivity);
            this.Controls.Add(this.lbEmissivity);
            this.Controls.Add(this.tbSpecificHeat);
            this.Controls.Add(this.lbSpecificHeat);
            this.Controls.Add(this.tbConductivity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.cbMaterialType);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.lbMaterialType);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(340, 325);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(340, 325);
            this.Name = "MaterialEditor";
            this.Text = "Редактирование материала";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lbMaterialType;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.ComboBox cbMaterialType;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbConductivity;
        private System.Windows.Forms.Label lbSpecificHeat;
        private System.Windows.Forms.TextBox tbSpecificHeat;
        private System.Windows.Forms.Label lbEmissivity;
        private System.Windows.Forms.TextBox tbEmissivity;
        private System.Windows.Forms.Label lbDensity;
        private System.Windows.Forms.TextBox tbDensity;
    }
}