namespace Fds2AcadPlugin.UserInterface
{
    partial class CalculationInfo
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
            this.btnStart = new System.Windows.Forms.Button();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblPath = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblUnits = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(223, 79);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.On_btnStart_Click);
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(12, 39);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(84, 13);
            this.lblTime.TabIndex = 1;
            this.lblTime.Text = "Calculation time:";
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(12, 12);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(66, 13);
            this.lblPath.TabIndex = 2;
            this.lblPath.Text = "Output path:";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(313, 79);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.On_btnCancel_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(100, 9);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(257, 20);
            this.textBox1.TabIndex = 4;
            // 
            // tbTime
            // 
            this.tbTime.Location = new System.Drawing.Point(100, 36);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(52, 20);
            this.tbTime.TabIndex = 5;
            this.tbTime.TextChanged += new System.EventHandler(this.On_tbTime_TextChanged);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(363, 7);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(25, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.Location = new System.Drawing.Point(177, 39);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(27, 13);
            this.lblUnits.TabIndex = 7;
            this.lblUnits.Text = "sec.";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // CalculationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 114);
            this.Controls.Add(this.lblUnits);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.tbTime);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalculationInfo";
            this.Text = "Calculation Info";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblUnits;
        private System.Windows.Forms.ErrorProvider errorProvider;
    }
}