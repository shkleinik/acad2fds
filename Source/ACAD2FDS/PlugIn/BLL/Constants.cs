using System;

namespace Fds2AcadPlugin.BLL
{
    public class Constants
    {
        #region Common Constants

        // public const string PluginFileSystemLocationPattern = @"{0}\Walash Ltd\Fds to AutoCad plugin\";

        public const string FdsTemplateName = "fds.template";

        public const string MaterialsBasePath = "materials.xml";

        public const string SurfacesBasePath = "surfaces.xml";

        public const string MappingsMaterialsPath = "mapping_materials.xml";

        public const string ConfigName = "fdsPlugin.config";

        public const string FdsFileExtension = ".fds";

        #endregion

        #region Menu

        public const string FdsMenuName = "FDS (Fire Dynamics Simulator)";
        public const string RunFdsMenuItem = "Calculation";
        public const string RunFdsCommandName = "RunCalculationInFds";
        public const string RunSmokeViewMenuItem = "View result in SmokeView";
        public const string RunSmokeViewCommandName = "ViewResultInSmokeView";
        public const string ConvertTo3dSolidsMenuItem = "Convert Objects to 3D Solids";
        public const string ConvertTo3dSolidsCommandName = "ConvertTo3dSolids";
        public const string OpenMaterialManagerMenuItem = "Materials and Surfaces";
        public const string OpenMaterialManagerCommandName = "OpenMaterialManager";
        public const string EditMaterialsMappingsMenuItem = "Material Mappings";
        public const string EditMaterialsMappingsCommandName = "EditMaterialsMappings";
        public const string OptionsMenuItem = "Options";
        public const string OptionsCommandName = "PluginOptions";
        public const string AboutMenuItem = "About";
        public const string AboutCommandName = "ShowAbout";

        public const string BuildMenuCommandName = "BuildFdsMenu";

        #endregion

        #region Messages

        public const string MenuBuildErrorMessagePattern = "Error occured during FDS menu building.\r\n\r\n {0}";

        public const string OutOfMemoruMessage = "Lack of system resources. Do you want to proceed?";

        public const string ConvertedSolidsInfoPattern = "{0} of {1} solids converted";

        public static string PluginWasNotConfigured = "Plugin was not configured yet. Navigate to 'Acad to FDS plugin -> Options' to configure.";

        public const string SmokeViewPathIsnotConfigured = "Path to SmokeView is not configured.\r\nNavigate to 'Acad to FDS plugin -> Options' to configure.";

        #endregion
    }
}