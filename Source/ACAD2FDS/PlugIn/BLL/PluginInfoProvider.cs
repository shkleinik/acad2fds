namespace Fds2AcadPlugin.BLL
{
    using System.IO;
    using Common;

    public static class PluginInfoProvider
    {
        public static string PathToMaterialsStore
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.MaterialsBasePath);
            }
        }

        public static string PathToSurfacesStore
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.SurfacesBasePath);
            }
        }

        public static string PathToMappingsMaterials
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.MappingsMaterialsPath);
            }
        }

        public static string PathToPluginConfig
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.ConfigName);
            }
        }

        public static string PathToFdsTemplate
        {
            get
            {
                return Path.Combine(Info.PluginDirectory, Constants.FdsTemplateName);
            }
        }
    }
}