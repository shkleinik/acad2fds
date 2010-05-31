namespace Acad2FdsSetupActions.BLL
{
    public class Constants
    {
        #region File system

        public const string FdsPluginAssemblyName = "Fds2AcadPlugin.dll";
        public const string PluginFileSystemLocationPattern = @"{0}\Walash Ltd\Fds to AutoCad plugin\{1}";

        #endregion

        #region Registry

        public const string DescriptionRegValue = "DESCRIPTION";
        public const string LoaderRegValue = "LOADER";
        public const string LoadctrlsRegValue = "LOADCTRLS";
        public const string ManagedRegValue = "MANAGED";
        public const string FdsPluginRegistryKey = "FDS2ACAD";
        public const string AutoCadRegistryKey = @"SOFTWARE\Autodesk\AutoCAD";
        public const string AutoCadApplicationsRegistryKey = @"SOFTWARE\Autodesk\AutoCAD\R17.2\ACAD-7001:409\Applications\";
        public const string AutoCadArchitectureApplicationsRegistryKey = @"SOFTWARE\Autodesk\AutoCAD\R17.2\ACAD-7004:419\Applications\";
        public const string CommandsRegistryKey = "Commands";
        public const string BuildMenuCommandName = "BuildFdsMenu";

        #endregion

        #region System

        public const string AutoCadProcessName = "acad";

        #endregion

    }
}