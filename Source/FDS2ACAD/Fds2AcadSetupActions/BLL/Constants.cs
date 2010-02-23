namespace Fds2AcadSetupActions.BLL
{
    public class Constants
    {
        #region File system

        public const string FdsPluginAssemblyName = "Fds2AcadPlugin.dll";
        public const string FdsFileSystemLocationPattern = @"{0}\Walash Ltd\Fds to AutoCad plugin\{1}";

        #endregion

        #region Registry

        public const string DescriptionRegValue = "DESCRIPTION";
        public const string LoaderRegValue = "LOADER";
        public const string LoadctrlsRegValue = "LOADCTRLS";
        public const string ManagedRegValue = "MANAGED";
        public const string FdsPluginRegistryKey = "FDS2ACAD";
        public const string AutoCadRegistryKey = @"SOFTWARE\Autodesk\AutoCAD";
        public const string AutoCadApplicationsRegistryKey = @"SOFTWARE\Autodesk\AutoCAD\R17.2\ACAD-7001:409\Applications\";
        public const string CommandsRegistryKey = "Commands";
        public const string BuildMenuCommandName = "BuildFdsMenu";

        #endregion

        #region System

        public const string AutoCadProcessName = "acad";

        #endregion

        #region AutoLoad
        
        public const string AutoCad2009AutoLoadFilePathPattern = @"{0}\{1}\{2}";
        public const string AutoCad2009AutoLoadFilePath = @"AutoCAD 2009\Support";
        public const string AutoCad2009AutoLoadFileName = "acad2009.lsp";
        public const string LispCommandCallPattern = "(command \"{0}\")";
        public const string FdsMenuBuildCommand = "BuildFdsMenu";

        #endregion
    }
}