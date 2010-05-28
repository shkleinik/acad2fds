using System;

namespace Fds2AcadPlugin.UserInterface
{
    using System.Windows.Forms;

    public partial class ConvertionProgress : Form
    {

        #region Fields

        private int _progress;
        private string _description = ""; 

        #endregion

        #region Consturctors

        public ConvertionProgress(int maxProgress)
        {
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
            SetProgress(progress);
            SetDescription(description);
        } 

        #endregion


        #region Internal implementation
        /// <summary>
        /// Wrapper for progress change invocation.
        /// </summary>
        /// <param name="progress"></param>
        private void SetProgress(int progress)
        {
            _progress = progress;
            Invoke(new EventHandler(SetProgress));
        }

        /// <summary>
        /// Changes text of the label under the progress bar.
        /// </summary>
        /// <param name="description"></param>
        private void SetDescription(string description)
        {
            _description = description;
            Invoke(new EventHandler(SetDescription));
        }

        /// <summary>
        /// Delegate for async progress bar update.
        /// </summary>
        /// <param name="sender">Required parameter.</param>
        /// <param name="args">Required parameter.</param>
        private void SetProgress(object sender, EventArgs args)
        {
            pbStatus.Value = _progress;
            Refresh();
        }

        /// <summary>
        /// Delegate for async label label update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SetDescription(object sender, EventArgs args)
        {
            lblProgressStatus.Text = _description;
            lblProgressStatus.Refresh();
            Refresh();
        } 
        #endregion
    }
}
