namespace Fds2AcadPlugin.BLL.Helpers
{
    using System.Diagnostics;
    using System;
    using System.Threading;
    using NativeMethods;

    public static class CommonHelper
    {
        public static string GetFolderPath(this string filePath)
        {
            var subStrings = filePath.Split('\\');
            var fileName = subStrings[subStrings.Length - 1];

            return filePath.Replace(fileName, string.Empty);
        }

        public static string GetFileNameWithoutExtension(this string filePath, string extension)
        {
            var subStrings = filePath.Split('\\');
            var fileName = subStrings[subStrings.Length - 1];
            return fileName.Replace(extension, string.Empty);
        }

        public static IntPtr StartSmokeViewProcess(string pathToSmokeView, string pathToSmokeViewScene)
        {
            var smvProcess = new Process();
            smvProcess.StartInfo.FileName = pathToSmokeView;
            smvProcess.StartInfo.Arguments = pathToSmokeViewScene;
            smvProcess.StartInfo.WorkingDirectory = pathToSmokeViewScene.GetFolderPath();
            smvProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            smvProcess.Start();

            //var count = 0;
            //// wait to smoke view window be opened
            //while (smvProcess.MainWindowHandle == IntPtr.Zero)
            //{
            //    count++;

            //    if(count > 100)
            //        break;
            //    System.Threading.Thread.Sleep(100);
            //}
            // return smvProcess.MainWindowHandle;


            var count = 0;
            var smokeViewHandle = IntPtr.Zero;
            // wait to smoke view window to be opened
            while (smvProcess.MainWindowHandle == IntPtr.Zero)
            {
                smokeViewHandle = NativeMethods.FindWindow(null, pathToSmokeViewScene.GetFileNameWithoutExtension(".smv"));

                if (smokeViewHandle != IntPtr.Zero)
                    break;

                count++;
                if (count > 100)
                    break;

                Thread.Sleep(100);
            }

            return smokeViewHandle;
        }
    }
}