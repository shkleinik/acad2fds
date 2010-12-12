using System.Windows.Forms;

namespace Common.UI
{
    public class UserNotifier
    {
        public const string WarningCaption = "Warning";

        public const string ErrorCaption = "Error";

        public static void ShowWarning(string text)
        {
            MessageBox.Show(text, WarningCaption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static DialogResult ShowWarningWithConfirmation(string text)
        {
            return MessageBox.Show(text,
                                   WarningCaption ,
                                   MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button1);
        }

        public static void ShowError(string text)
        {
            MessageBox.Show(text, ErrorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}