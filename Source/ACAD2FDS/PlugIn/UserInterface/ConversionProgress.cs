namespace Fds2AcadPlugin.UserInterface
{
    using System.Windows.Forms;
    using Common.UI;

    public partial class ConversionProgress : FormBase
    {
        #region Fields

        private object syncRoot = new object();

        private delegate void InvokeDelegate();

        #endregion

        #region Properties

        public bool AllowUpdate { get; set; }

        public bool ForceClose { get; set; }

        #endregion

        #region Consturctors

        public ConversionProgress(int maxProgress)
        {
            AllowUpdate = true;
            ForceClose = false;

            InitializeComponent();

            pbStatus.Maximum = maxProgress;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the description and the progress bar
        /// </summary>
        /// <param name="progress">Percent of completion, between 0 and 100</param>
        /// <param name="description">Description of progress</param>
        public void Update(int progress, string description)
        {
            if (AllowUpdate)
            {
                InvokeDelegate invokeDelegate = delegate
                {
                    pbStatus.Value = progress;
                    pbStatus.Update();
                    lblProgressStatus.Text = description;
                    lblProgressStatus.Update();
                };

                Invoke(invokeDelegate);
            }
        }

        #endregion

        private void ConversionProgress_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ForceClose)
                return;

            if (AllowUpdate)
            {
                e.Cancel = true;

                var result = UserNotifier.ShowQuestion("Do you want to stop conversion?");

                if (result == DialogResult.Yes)
                {
                    AllowUpdate = false;
                    BeginInvoke(new InvokeDelegate(Close));
                }
            }
        }
    }
}
