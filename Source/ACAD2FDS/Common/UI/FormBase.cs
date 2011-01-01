namespace Common.UI
{
    using System.Windows.Forms;

    public partial class FormBase : Form
    {
        public FormBase()
        {
            InitializeComponent();
        }

        public override string Text
        {
            set
            {
                base.Text = string.Format(CommonConstants.WindowCaptionPattern, value);
            }
        }
    }
}
