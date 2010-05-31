namespace Acad2FdsSetupActions.BLL
{
    using System.Diagnostics;

    public class CommonHelper
    {
        public static bool IsAutoCadRunning()
        {
            var acadProcesses = Process.GetProcessesByName(Constants.AutoCadProcessName);

            return acadProcesses.Length > 0;
        }
    }
}