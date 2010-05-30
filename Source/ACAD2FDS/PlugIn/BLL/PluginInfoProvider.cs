namespace Fds2AcadPlugin.BLL
{
    using System;
    using System.IO;

    public static class PluginInfoProvider
    {
        public static string PathToMaterialsStore
        {
            get
            {
                return Path.Combine(GetPathToPluginDirectory(), Constants.MaterialsBasePath);
            }
        }

        public static string PathToMappingsMaterials
        {
            get
            {
                return Path.Combine(GetPathToPluginDirectory(), Constants.MappingsMaterialsPath);
            }
        }

        public static string PathToPluginConfig
        {
            get
            {
                return Path.Combine(GetPathToPluginDirectory(), Constants.ConfigName);
            }
        }

        public static string PathToFdsTemplate
        {
            get
            {
                return Path.Combine(GetPathToPluginDirectory(), Constants.FdsTemplateName);
            }
        }

        public static string GetPathToPluginDirectory()
        {
            return string.Format(Constants.PluginFileSystemLocationPattern, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
        }
    }
}