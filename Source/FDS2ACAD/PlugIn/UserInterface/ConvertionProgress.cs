namespace Fds2AcadPlugin.UserInterface
{
    using System.Windows.Forms;

    public partial class ConvertionProgress : Form
    {
        public ConvertionProgress(int maxProgress)
        {
            InitializeComponent();

            pbStatus.Maximum = maxProgress;
        }
    }
}
