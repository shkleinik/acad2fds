namespace Fds2AcadPlugin.BLL.Helpers
{
    using System.Diagnostics;
    using System;
    using System.Threading;
    using NativeMethods;
    using System.Drawing;
    using MaterialManager.BLL;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.DatabaseServices;

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
            return Color.FromArgb(fdsColor.R, fdsColor.G, fdsColor.B);
        }

        public static FdsColor ToFdsColor(this Color color)
        {
            return new FdsColor(color.R, color.G, color.B);
        }

        public static List<string> GetMaterials(this IEnumerable<Entity> solids)
        {
            var materials = new List<string>();

            foreach (var solid in solids)
            {
                if(!materials.Contains(solid.Material))
                    materials.Add(solid.Material);
            }

            return materials;
        }

        public static List<MaterialManager.BLL.Surface> GetUniqueSurfaces(this Dictionary<string, MaterialManager.BLL.Surface> mapping)
        {
            var uniqueSurfaces = new List<MaterialManager.BLL.Surface>();

            foreach (var pair in mapping)
            {
                if(!uniqueSurfaces.Contains(pair.Value))
                    uniqueSurfaces.Add(pair.Value);
            }

            return uniqueSurfaces;
        }

        public static List<Entry> ToEntryList(this Dictionary<string, MaterialManager.BLL.Surface> mappings)
        {
            var entries = new List<Entry>();

            foreach (var pair in mappings)
            {
                entries.Add(new Entry(pair.Key, pair.Value));
            }

            return entries;
        }

        public static Dictionary<string , MaterialManager.BLL.Surface> ToDictionary(this List<Entry> entries)
        {
            var dictionary = new Dictionary<string, MaterialManager.BLL.Surface>();

            if (entries == null)
                return dictionary;

            foreach (var entry in entries)
            {
                dictionary[(string)entry.Key] = (MaterialManager.BLL.Surface)entry.Value;
            }

            return dictionary;
        }
    }
}