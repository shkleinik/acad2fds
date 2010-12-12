using Common;

namespace Fds2AcadPlugin.UserInterface
{
    using System;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using BLL;
    using Common.UI;

    public partial class About : FormBase
    {
        #region Constants

        private const string ProductNamePattern = "Product Name: {0}";

        private const string AuthorsPattern = "Authors: {0}";

        private const string DescriptionPattern = "Description: {0}";

        private const string VersionPattern = "Version: {0}";

        #endregion

        private readonly ILogger log;

        public About(ILogger log)
        {
            this.log = log;

            InitializeComponent();
            pbLogo.Image = PluginInfoProvider.ProductLogo;
        }

        public Bitmap ProductLogo
        {
            get
            {
                return (Bitmap)pbLogo.Image;
            }

            set
            {
                pbLogo.Image = value;
            }
        }

        public string PluginName
        {
            get
            {
                return lbProductName.Text;
            }

            set
            {
                lbProductName.Text = string.Format(ProductNamePattern, value ?? string.Empty);
            }
        }

        public string Authors
        {
            get
            {
                return lbAuthors.Text;
            }

            set
            {
                lbAuthors.Text = string.Format(AuthorsPattern, value ?? string.Empty);
            }
        }

        public string Description
        {
            get
            {
                return lbDescription.Text;
            }

            set
            {
                lbDescription.Text = string.Format(DescriptionPattern, value ?? string.Empty);
            }
        }

        public string Version
        {
            get
            {
                return lbVersion.Text;
            }

            set
            {
                lbVersion.Text = string.Format(VersionPattern, value ?? string.Empty);
            }
        }

        private void OnOkClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void WebSiteClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Todo : Move strings to assembly attributes like version.
            try
            {
                Process.Start(lbWebSite.Text);
            }
            catch (Exception exception)
            {
                log.LogError(exception);
                UserNotifier.ShowError(exception.Message);
            }
        }

        private void MailToClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Todo : Move strings to assembly attributes like version.
            try
            {
                Process.Start(string.Format("mailto:{0}", lbMailTo.Text));
            }
            catch (Exception exception)
            {
                log.LogError(exception);
                UserNotifier.ShowError(exception.Message);
            }
        }

        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.OK;
        }
    }
}
