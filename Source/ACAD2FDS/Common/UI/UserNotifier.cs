namespace Common.UI
{
    using System.Windows.Forms;

    public class UserNotifier
    {
        #region Constants

        public const string WarningCaption = "Warning" + CaptionPattern;

        public const string ErrorCaption = "Error" + CaptionPattern;

        public const string InfoCaption = "Info" + CaptionPattern;

        public const string RetryCaption = "Retry" + CaptionPattern;

        public const string QuestionCaption = "Question" + CaptionPattern;

        private const string CaptionPattern = " - AutoCAD to FDS plugin";

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