namespace Fds2AcadPlugin.UserInterface
{
    partial class About
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
            this.btnOK = new System.Windows.Forms.Button();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lbVersion = new System.Windows.Forms.Label();
            this.lbProductName = new System.Windows.Forms.Label();
            this.lbAuthors = new System.Windows.Forms.Label();
            this.lbDescription = new System.Windows.Forms.Label();
            this.lbMailTo = new System.Windows.Forms.LinkLabel();
            this.lbWebSite = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(357, 187);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnOkClick);
            // 
            // pbLogo
            // 
            this.pbLogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbLogo.Location = new System.Drawing.Point(0, 0);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(445, 80);
            this.pbLogo.TabIndex = 1;
            this.pbLogo.TabStop = false;
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.Location = new System.Drawing.Point(13, 172);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(45, 13);
            this.lbVersion.TabIndex = 2;
            this.lbVersion.Text = "Version:";
            // 
            // lbProductName
            // 
            this.lbProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbProductName.AutoSize = true;
            this.lbProductName.Location = new System.Drawing.Point(12, 97);
            this.lbProductName.Name = "lbProductName";
            this.lbProductName.Size = new System.Drawing.Size(78, 13);
            this.lbProductName.TabIndex = 4;
            this.lbProductName.Text = "Product Name:";
            // 
            // lbAuthors
            // 
            this.lbAuthors.AutoSize = true;
            this.lbAuthors.Location = new System.Drawing.Point(13, 122);
            this.lbAuthors.Name = "lbAuthors";
            this.lbAuthors.Size = new System.Drawing.Size(46, 13);
            this.lbAuthors.TabIndex = 6;
            this.lbAuthors.Text = "Authors:";
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(13, 147);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(63, 13);
            this.lbDescription.TabIndex = 8;
            this.lbDescription.Text = "Description:";
            // 
            // lbMailTo
            // 
            this.lbMailTo.AutoSize = true;
            this.lbMailTo.Location = new System.Drawing.Point(182, 197);
            this.lbMailTo.Name = "lbMailTo";
            this.lbMailTo.Size = new System.Drawing.Size(135, 13);
            this.lbMailTo.TabIndex = 9;
            this.lbMailTo.TabStop = true;
            this.lbMailTo.Text = "pavel.shkleinik@gmail.com";
            this.lbMailTo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.MailToClicked);
            // 
            // lbWebSite
            // 
            this.lbWebSite.AutoSize = true;
            this.lbWebSite.Location = new System.Drawing.Point(13, 197);
            this.lbWebSite.Name = "lbWebSite";
            this.lbWebSite.Size = new System.Drawing.Size(151, 13);
            this.lbWebSite.TabIndex = 10;
            this.lbWebSite.TabStop = true;
            this.lbWebSite.Text = "http://acad2fds.codeplex.com";
            this.lbWebSite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.WebSiteClicked);
            // 
            // About
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 222);
            this.Controls.Add(this.lbWebSite);
            this.Controls.Add(this.lbMailTo);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.lbAuthors);
            this.Controls.Add(this.lbProductName);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 250);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(450, 250);
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About - AutoCAD to FDS plugin";
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label lbProductName;
        private System.Windows.Forms.Label lbAuthors;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.LinkLabel lbMailTo;
        private System.Windows.Forms.LinkLabel lbWebSite;
    }
}