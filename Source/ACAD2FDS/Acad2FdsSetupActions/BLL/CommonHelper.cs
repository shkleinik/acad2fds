namespace Acad2FdsSetupActions.BLL
{
    using System;
    using System.Diagnostics;

    public class CommonHelper
    {
        public static bool IsAutoCadRunning()
        {
            var acadProcesses = Process.GetProcessesByName(Constants.AutoCadProcessName);

            return acadProcesses.Length > 0;
        }

        public static IntPtr GetProcessMainWindowHandle(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);

            if (0 == processes.Length)
                return IntPtr.Zero;

            var mainWindowHandle = IntPtr.Zero;

            foreach (var process in processes)
            {
                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    mainWindowHandle = process.MainWindowHandle;
                    break;
                }
            }

            return mainWindowHandle;
        }
    }
}