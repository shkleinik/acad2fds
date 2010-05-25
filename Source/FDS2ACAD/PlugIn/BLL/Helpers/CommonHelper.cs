namespace Fds2AcadPlugin.BLL.Helpers
{
    using System.Diagnostics;
    using System;
    using System.Threading;
    using NativeMethods;
    using System.Drawing;
    using MaterialManager.BLL;

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

        public static Color ToSystemColor(this FdsColor fdsColor)
        {
            return Color.FromArgb((int)Math.Round(fdsColor.R * 1000, 0),
                                  (int)Math.Round(fdsColor.G * 1000, 0),
                                  (int)Math.Round(fdsColor.B * 1000, 0)
                                  );
        }

        public static FdsColor ToFdsColor(this Color color)
        {
            return new FdsColor(Math.Round((double)color.R / 1000, 3),
                                Math.Round((double)color.G / 1000, 3),
                                Math.Round((double)color.B / 1000, 3)
                );
        }
    }
}