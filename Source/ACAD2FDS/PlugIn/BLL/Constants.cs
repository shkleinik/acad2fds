namespace Fds2AcadPlugin.BLL
{
    public class Constants
    {
        #region Common Constants

        public const string PluginFileSystemLocationPattern = @"{0}\Walash Ltd\Fds to AutoCad plugin\";

        public const string FdsTemplateName = "fds.template";

        public const string MaterialsBasePath = "materials.xml";

        public const string SurfacesBasePath = "surfaces.xml";

        public const string MappingsMaterialsPath = "mapping_materials.xml";

        public const string ConfigName = "fdsPlugin.config";

        public const string FdsFileExtension = ".fds";
        
        #endregion
        
        #region Menu

        public const string FdsMenuName = "Acad to FDS plugin";
        public const string RunFdsMenuItem = "Start calculation";
        public const string RunFdsCommandName = "RunCalculationInFds";
        public const string RunSmokeViewMenuItem = "View result in SmokeView";
        public const string RunSmokeViewCommandName = "ViewResultInSmokeView";
        public const string ConvertTo3dSolidsMenuItem = "Convert Objects to 3D Solids";
        public const string ConvertTo3dSolidsCommandName = "ConvertTo3dSolids";
        public const string OptionsMenuItem = "Options";
        public const string OptionsCommandName = "PluginOptions";
        public const string EditMaterialsMappingsMenuItem = "Edit materials mappings";
        public const string EditMaterialsMappingsCommandName = "EditMaterialsMappings";
        public const string OpenMaterialManagerMenuItem = "Material and Surfaces Manager";
        public const string OpenMaterialManagerCommandName = "OpenMaterialManager";

        public const string BuildMenuCommandName = "BuildFdsMenu";

        #endregion

        #region Error Messages

        public const string MenuBuildErrorMessagePattern = "Error occured during FDS menu building.\n\n {0}";

        #endregion
    }
}