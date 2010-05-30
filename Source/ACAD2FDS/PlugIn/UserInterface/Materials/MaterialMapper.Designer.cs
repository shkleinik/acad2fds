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
            this.lbFounded = new System.Windows.Forms.ListBox();
            this.lblFounded = new System.Windows.Forms.Label();
            this.lblAvailableMaterials = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.chlbAvaileable = new System.Windows.Forms.CheckedListBox();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlMiddle.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFounded
            // 
            this.lbFounded.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbFounded.FormattingEnabled = true;
            this.lbFounded.Location = new System.Drawing.Point(0, 0);
            this.lbFounded.Name = "lbFounded";
            this.lbFounded.Size = new System.Drawing.Size(270, 225);
            this.lbFounded.TabIndex = 0;
            this.lbFounded.SelectedIndexChanged += new System.EventHandler(this.On_lbFounded_SelectedIndexChanged);
            // 
            // lblFounded
            // 
            this.lblFounded.AutoSize = true;
            this.lblFounded.Location = new System.Drawing.Point(3, 9);
            this.lblFounded.Name = "lblFounded";
            this.lblFounded.Size = new System.Drawing.Size(124, 13);
            this.lblFounded.TabIndex = 1;
            this.lblFounded.Text = "Founded materials types:";
            // 
            // lblAvailableMaterials
            // 
            this.lblAvailableMaterials.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAvailableMaterials.AutoSize = true;
            this.lblAvailableMaterials.Location = new System.Drawing.Point(308, 9);
            this.lblAvailableMaterials.Name = "lblAvailableMaterials";
            this.lblAvailableMaterials.Size = new System.Drawing.Size(97, 13);
            this.lblAvailableMaterials.TabIndex = 3;
            this.lblAvailableMaterials.Text = "Available materials:";
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
            // pnlMiddle
            // 
            this.pnlMiddle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMiddle.Controls.Add(this.chlbAvaileable);
            this.pnlMiddle.Controls.Add(this.lbFounded);
            this.pnlMiddle.Location = new System.Drawing.Point(0, 40);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(581, 231);
            this.pnlMiddle.TabIndex = 5;
            // 
            // chlbAvaileable
            // 
            this.chlbAvaileable.CheckOnClick = true;
            this.chlbAvaileable.Dock = System.Windows.Forms.DockStyle.Right;
            this.chlbAvaileable.FormattingEnabled = true;
            this.chlbAvaileable.Location = new System.Drawing.Point(311, 0);
            this.chlbAvaileable.Name = "chlbAvaileable";
            this.chlbAvaileable.Size = new System.Drawing.Size(270, 229);
            this.chlbAvaileable.TabIndex = 1;
            this.chlbAvaileable.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.On_chlbAvaileable_ItemCheck);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblAvailableMaterials);
            this.pnlTop.Controls.Add(this.lblFounded);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(584, 34);
            this.pnlTop.TabIndex = 6;
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
            // MaterialMapper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 312);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlMiddle);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 500);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "MaterialMapper";
            this.Text = "MaterialMapper";
            this.Load += new System.EventHandler(this.On_MaterialMapper_Load);
            this.pnlMiddle.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbFounded;
        private System.Windows.Forms.Label lblFounded;
        private System.Windows.Forms.Label lblAvailableMaterials;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.CheckedListBox chlbAvaileable;
    }
}