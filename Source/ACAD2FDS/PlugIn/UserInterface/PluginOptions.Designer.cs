namespace Fds2AcadPlugin.UserInterface
{
    partial class PluginOptions
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
            this.lblFdsPath = new System.Windows.Forms.Label();
            this.tbFdsPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFds = new System.Windows.Forms.Button();
            this.lblSmokeViewPath = new System.Windows.Forms.Label();
            this.tbSmokeViewPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSvPath = new System.Windows.Forms.Button();
            this.gbFdsOptions = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbFdsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFdsPath
            // 
            this.lblFdsPath.AutoSize = true;
            this.lblFdsPath.Location = new System.Drawing.Point(6, 22);
            this.lblFdsPath.Name = "lblFdsPath";
            this.lblFdsPath.Size = new System.Drawing.Size(55, 13);
            this.lblFdsPath.TabIndex = 0;
            this.lblFdsPath.Text = "FDS path:";
            // 
            // tbFdsPath
            // 
            this.tbFdsPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFdsPath.Enabled = false;
            this.tbFdsPath.Location = new System.Drawing.Point(102, 19);
            this.tbFdsPath.Name = "tbFdsPath";
            this.tbFdsPath.Size = new System.Drawing.Size(287, 20);
            this.tbFdsPath.TabIndex = 1;
            // 
            // btnBrowseFds
            // 
            this.btnBrowseFds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFds.Location = new System.Drawing.Point(395, 19);
            this.btnBrowseFds.Name = "btnBrowseFds";
            this.btnBrowseFds.Size = new System.Drawing.Size(68, 23);
            this.btnBrowseFds.TabIndex = 2;
            this.btnBrowseFds.Text = "Browse";
            this.btnBrowseFds.UseVisualStyleBackColor = true;
            this.btnBrowseFds.Click += new System.EventHandler(this.On_btnBrowseFds_Click);
            // 
            // lblSmokeViewPath
            // 
            this.lblSmokeViewPath.AutoSize = true;
            this.lblSmokeViewPath.Location = new System.Drawing.Point(6, 52);
            this.lblSmokeViewPath.Name = "lblSmokeViewPath";
            this.lblSmokeViewPath.Size = new System.Drawing.Size(90, 13);
            this.lblSmokeViewPath.TabIndex = 3;
            this.lblSmokeViewPath.Text = "SmokeView path:";
            // 
            // tbSmokeViewPath
            // 
            this.tbSmokeViewPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSmokeViewPath.Enabled = false;
            this.tbSmokeViewPath.Location = new System.Drawing.Point(102, 49);
            this.tbSmokeViewPath.Name = "tbSmokeViewPath";
            this.tbSmokeViewPath.Size = new System.Drawing.Size(287, 20);
            this.tbSmokeViewPath.TabIndex = 4;
            // 
            // btnBrowseSvPath
            // 
            this.btnBrowseSvPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseSvPath.Location = new System.Drawing.Point(395, 48);
            this.btnBrowseSvPath.Name = "btnBrowseSvPath";
            this.btnBrowseSvPath.Size = new System.Drawing.Size(68, 23);
            this.btnBrowseSvPath.TabIndex = 5;
            this.btnBrowseSvPath.Text = "Browse";
            this.btnBrowseSvPath.UseVisualStyleBackColor = true;
            this.btnBrowseSvPath.Click += new System.EventHandler(this.On_btnBrowseSvPath_Click);
            // 
            // gbFdsOptions
            // 
            this.gbFdsOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFdsOptions.Controls.Add(this.lblFdsPath);
            this.gbFdsOptions.Controls.Add(this.tbSmokeViewPath);
            this.gbFdsOptions.Controls.Add(this.btnBrowseSvPath);
            this.gbFdsOptions.Controls.Add(this.tbFdsPath);
            this.gbFdsOptions.Controls.Add(this.btnBrowseFds);
            this.gbFdsOptions.Controls.Add(this.lblSmokeViewPath);
            this.gbFdsOptions.Location = new System.Drawing.Point(12, 12);
            this.gbFdsOptions.Name = "gbFdsOptions";
            this.gbFdsOptions.Size = new System.Drawing.Size(470, 85);
            this.gbFdsOptions.TabIndex = 6;
            this.gbFdsOptions.TabStop = false;
            this.gbFdsOptions.Text = "Fds Options";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(407, 103);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.On_btnSave_Click);
            // 
            // PluginOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 135);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.gbFdsOptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fds to AutoCad plugin Options";
            this.Load += new System.EventHandler(this.On_PluginOptions_Load);
            this.gbFdsOptions.ResumeLayout(false);
            this.gbFdsOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblFdsPath;
        private System.Windows.Forms.TextBox tbFdsPath;
        private System.Windows.Forms.Button btnBrowseFds;
        private System.Windows.Forms.Label lblSmokeViewPath;
        private System.Windows.Forms.TextBox tbSmokeViewPath;
        private System.Windows.Forms.Button btnBrowseSvPath;
        private System.Windows.Forms.GroupBox gbFdsOptions;
        private System.Windows.Forms.Button btnSave;
    }
}