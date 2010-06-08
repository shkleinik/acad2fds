namespace Fds2AcadPlugin.BLL.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Threading;
    using Autodesk.AutoCAD.DatabaseServices;
    using MaterialManager.BLL;
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
                if (!materials.Contains(solid.Material))
                    materials.Add(solid.Material);
            }

            return materials;
        }

        public static List<MaterialManager.BLL.Surface> GetUniqueSurfaces(this Dictionary<string, MaterialManager.BLL.Surface> mapping)
        {
            var uniqueSurfaces = new List<MaterialManager.BLL.Surface>();

            foreach (var pair in mapping)
            {
                if (!uniqueSurfaces.Contains(pair.Value))
                    uniqueSurfaces.Add(pair.Value);
            }

            return uniqueSurfaces;
        }

        public static IList CreateStringWrapperForBinding(this IEnumerable<string> strings)
        {
            var values = from data in strings
                         select new { Value = data };

            return values.ToList();
        }

        public static IEnumerable<string> GetSurfacesIDs(this List<MaterialManager.BLL.Surface> surfaces)
        {
            foreach (var surface in surfaces)
            {
                yield return surface.ID;
            }
        }

        public static IList<MaterialManager.BLL.Surface> GetAllUsedSurfaces()
        {
            var usedMaterials = AcadInfoProvider.AllSolidsFromCurrentDrawing().GetMaterials();
            var surfacesStore = XmlSerializer<List<MaterialManager.BLL.Surface>>.Deserialize(PluginInfoProvider.PathToSurfacesStore);
            var mappingMaterials = XmlSerializer<List<MaterialAndSurface>>.Deserialize(PluginInfoProvider.PathToMappingsMaterials);

            //var usedSuraces = from surface in surfacesStore
            //       from mapping in mappingMaterials
            //       where surface.ID == mapping.SurfaceName && usedMaterials.Contains(mapping.MaterialName)
            //       select surface;

            var usedSurfaces = new List<MaterialManager.BLL.Surface>();

            foreach (var material in usedMaterials)
            {
                var mapItem = mappingMaterials.Find(mapping => mapping.MaterialName == material);
                if (mapItem == null)
                    continue;

                var surf = surfacesStore.Find(surface => surface.ID == mapItem.SurfaceName);

                if (surf == null)
                    continue;

                if (!usedSurfaces.Contains(surf))
                    usedSurfaces.Add(surf);
            }


            return usedSurfaces;
        }

        public static IList<MaterialManager.BLL.Material> GetNecessaryMaterialsFromSurfaces(this IList<MaterialManager.BLL.Surface> surfaces)
        {
            var necessaryMaterials = new List<MaterialManager.BLL.Material>();
            var allMaterials = XmlSerializer<List<MaterialManager.BLL.Material>>.Deserialize(PluginInfoProvider.PathToMaterialsStore) ?? new List<MaterialManager.BLL.Material>();

            foreach (var surface in surfaces)
            {
                var material = allMaterials.Find(mat => mat.ID == surface.MaterialID);

                if(material == null)
                    continue;

                necessaryMaterials.Add(material);
            }

            return necessaryMaterials;
        }

        public static Dictionary<string, string> GetDictionary(this List<MaterialAndSurface> materialAndSurfaces)
        {
            var dictionary = new Dictionary<string, string>();

            foreach (var materialAndSurface in materialAndSurfaces)
            {
                dictionary.Add(materialAndSurface.MaterialName, materialAndSurface.SurfaceName);
            }

            return dictionary;
        }
    }
}