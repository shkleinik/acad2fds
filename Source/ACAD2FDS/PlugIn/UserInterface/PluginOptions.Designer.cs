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
            this.components = new System.ComponentModel.Container();
            this.lblFdsPath = new System.Windows.Forms.Label();
            this.tbFdsPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFds = new System.Windows.Forms.Button();
            this.lblSmokeViewPath = new System.Windows.Forms.Label();
            this.tbSmokeViewPath = new System.Windows.Forms.TextBox();
            this.btnBrowseSvPath = new System.Windows.Forms.Button();
            this.gbFdsOptions = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.gbElementSize = new System.Windows.Forms.GroupBox();
            this.tbElementSize = new System.Windows.Forms.TextBox();
            this.lblElementSize = new System.Windows.Forms.Label();
            this.chbElementSize = new System.Windows.Forms.CheckBox();
            this.gbDevicesDensity = new System.Windows.Forms.GroupBox();
            this.chlbDevices = new System.Windows.Forms.CheckedListBox();
            this.tbDevicesDensity = new System.Windows.Forms.TextBox();
            this.lblDevicesDensity = new System.Windows.Forms.Label();
            this.chbDevicesDensity = new System.Windows.Forms.CheckBox();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFdsOptions.SuspendLayout();
            this.gbElementSize.SuspendLayout();
            this.gbDevicesDensity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.tbFdsPath.Size = new System.Drawing.Size(274, 20);
            this.tbFdsPath.TabIndex = 1;
            this.tbFdsPath.TextChanged += new System.EventHandler(this.On_textBox_TextChanged);
            this.tbFdsPath.Validating += new System.ComponentModel.CancelEventHandler(this.On_strTextBox_Validating);
            // 
            // btnBrowseFds
            // 
            this.btnBrowseFds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseFds.Location = new System.Drawing.Point(395, 17);
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
            this.tbSmokeViewPath.Size = new System.Drawing.Size(274, 20);
            this.tbSmokeViewPath.TabIndex = 4;
            this.tbSmokeViewPath.TextChanged += new System.EventHandler(this.On_textBox_TextChanged);
            this.tbSmokeViewPath.Validating += new System.ComponentModel.CancelEventHandler(this.On_strTextBox_Validating);
            // 
            // btnBrowseSvPath
            // 
            this.btnBrowseSvPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseSvPath.Location = new System.Drawing.Point(395, 46);
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
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(407, 389);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.On_btnSave_Click);
            // 
            // gbElementSize
            // 
            this.gbElementSize.Controls.Add(this.tbElementSize);
            this.gbElementSize.Controls.Add(this.lblElementSize);
            this.gbElementSize.Location = new System.Drawing.Point(12, 115);
            this.gbElementSize.Name = "gbElementSize";
            this.gbElementSize.Size = new System.Drawing.Size(470, 67);
            this.gbElementSize.TabIndex = 6;
            this.gbElementSize.TabStop = false;
            // 
            // tbElementSize
            // 
            this.tbElementSize.Location = new System.Drawing.Point(102, 28);
            this.tbElementSize.Name = "tbElementSize";
            this.tbElementSize.Size = new System.Drawing.Size(140, 20);
            this.tbElementSize.TabIndex = 2;
            this.tbElementSize.TextChanged += new System.EventHandler(this.On_textBox_TextChanged);
            this.tbElementSize.Validating += new System.ComponentModel.CancelEventHandler(this.On_intTextBox_Validating);
            // 
            // lblElementSize
            // 
            this.lblElementSize.AutoSize = true;
            this.lblElementSize.Location = new System.Drawing.Point(6, 31);
            this.lblElementSize.Name = "lblElementSize";
            this.lblElementSize.Size = new System.Drawing.Size(69, 13);
            this.lblElementSize.TabIndex = 1;
            this.lblElementSize.Text = "Element size:";
            // 
            // chbElementSize
            // 
            this.chbElementSize.AutoSize = true;
            this.chbElementSize.Location = new System.Drawing.Point(18, 114);
            this.chbElementSize.Name = "chbElementSize";
            this.chbElementSize.Size = new System.Drawing.Size(223, 17);
            this.chbElementSize.TabIndex = 0;
            this.chbElementSize.Text = "Use custom element size for comlex solids";
            this.chbElementSize.UseVisualStyleBackColor = true;
            this.chbElementSize.CheckedChanged += new System.EventHandler(this.On_chbElementSize_CheckedChanged);
            // 
            // gbDevicesDensity
            // 
            this.gbDevicesDensity.Controls.Add(this.chlbDevices);
            this.gbDevicesDensity.Controls.Add(this.tbDevicesDensity);
            this.gbDevicesDensity.Controls.Add(this.lblDevicesDensity);
            this.gbDevicesDensity.Location = new System.Drawing.Point(12, 188);
            this.gbDevicesDensity.Name = "gbDevicesDensity";
            this.gbDevicesDensity.Size = new System.Drawing.Size(470, 195);
            this.gbDevicesDensity.TabIndex = 8;
            this.gbDevicesDensity.TabStop = false;
            // 
            // chlbDevices
            // 
            this.chlbDevices.CheckOnClick = true;
            this.chlbDevices.DisplayMember = "DisplayName";
            this.chlbDevices.FormattingEnabled = true;
            this.chlbDevices.Location = new System.Drawing.Point(9, 61);
            this.chlbDevices.Name = "chlbDevices";
            this.chlbDevices.Size = new System.Drawing.Size(454, 124);
            this.chlbDevices.TabIndex = 3;
            // 
            // tbDevicesDensity
            // 
            this.tbDevicesDensity.Location = new System.Drawing.Point(102, 30);
            this.tbDevicesDensity.Name = "tbDevicesDensity";
            this.tbDevicesDensity.Size = new System.Drawing.Size(140, 20);
            this.tbDevicesDensity.TabIndex = 2;
            this.tbDevicesDensity.TextChanged += new System.EventHandler(this.On_textBox_TextChanged);
            this.tbDevicesDensity.Validating += new System.ComponentModel.CancelEventHandler(this.On_intTextBox_Validating);
            // 
            // lblDevicesDensity
            // 
            this.lblDevicesDensity.AutoSize = true;
            this.lblDevicesDensity.Location = new System.Drawing.Point(9, 33);
            this.lblDevicesDensity.Name = "lblDevicesDensity";
            this.lblDevicesDensity.Size = new System.Drawing.Size(85, 13);
            this.lblDevicesDensity.TabIndex = 1;
            this.lblDevicesDensity.Text = "Devices density:";
            // 
            // chbDevicesDensity
            // 
            this.chbDevicesDensity.AutoSize = true;
            this.chbDevicesDensity.Location = new System.Drawing.Point(18, 185);
            this.chbDevicesDensity.Name = "chbDevicesDensity";
            this.chbDevicesDensity.Size = new System.Drawing.Size(158, 17);
            this.chbDevicesDensity.TabIndex = 0;
            this.chbDevicesDensity.Text = "Use custom devices density";
            this.chbDevicesDensity.UseVisualStyleBackColor = true;
            this.chbDevicesDensity.CheckedChanged += new System.EventHandler(this.On_chbDevicesDensity_CheckedChanged);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // PluginOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 424);
            this.Controls.Add(this.chbDevicesDensity);
            this.Controls.Add(this.chbElementSize);
            this.Controls.Add(this.gbDevicesDensity);
            this.Controls.Add(this.gbElementSize);
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
            this.gbElementSize.ResumeLayout(false);
            this.gbElementSize.PerformLayout();
            this.gbDevicesDensity.ResumeLayout(false);
            this.gbDevicesDensity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.GroupBox gbElementSize;
        private System.Windows.Forms.CheckBox chbElementSize;
        private System.Windows.Forms.TextBox tbElementSize;
        private System.Windows.Forms.Label lblElementSize;
        private System.Windows.Forms.GroupBox gbDevicesDensity;
        private System.Windows.Forms.CheckBox chbDevicesDensity;
        private System.Windows.Forms.TextBox tbDevicesDensity;
        private System.Windows.Forms.Label lblDevicesDensity;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.CheckedListBox chlbDevices;
    }
}