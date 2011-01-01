namespace Common.UI
{
    using System.Windows.Forms;

    public class UserNotifier
    {
        #region Constants

        private static readonly string WarningCaption = string.Format(CommonConstants.WindowCaptionPattern, "Warning");

        private static readonly string ErrorCaption = string.Format(CommonConstants.WindowCaptionPattern, "Error");

        private static readonly string InfoCaption = string.Format(CommonConstants.WindowCaptionPattern, "Info");

        private static readonly string RetryCaption = string.Format(CommonConstants.WindowCaptionPattern, "Retry");

        private static readonly string QuestionCaption = string.Format(CommonConstants.WindowCaptionPattern, "Question"); 

        #endregion

        public static void ShowWarning(string text)
        {
            MessageBox.Show(text,
                            WarningCaption,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                            );
        }

        public static DialogResult ShowWarningWithConfirmation(string text)
        {
            return MessageBox.Show(text,
                                   WarningCaption,
                                   MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Exclamation,
                                   MessageBoxDefaultButton.Button1
                                   );
        }

        public static void ShowError(string text)
        {
            MessageBox.Show(text,
                            ErrorCaption,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                            );
        }

        public static void ShowInfo(string text)
        {
            MessageBox.Show(text,
                            InfoCaption,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information,
                            MessageBoxDefaultButton.Button1
                );
        }

        public static DialogResult ShowRetry(string text)
        {
            return MessageBox.Show(text,
                                   RetryCaption,
                                   MessageBoxButtons.RetryCancel,
                                   MessageBoxIcon.Warning,
                                   MessageBoxDefaultButton.Button1
                                   );
        }

        public static DialogResult ShowQuestion(string text)
        {
            return MessageBox.Show(text,
                                   QuestionCaption,
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button2
                                   );
        }
    }
}