namespace Acad2FdsSetupActions.BLL
{
    public class Constants
    {
        #region File system

        public const string FdsPluginAssemblyName = "Acad2FdsPlugin.dll";

        #endregion

        #region Registry

        //public const string Registry
        public const string DescriptionRegValue = "DESCRIPTION";
        public const string LoaderRegValue = "LOADER";
        public const string LoadctrlsRegValue = "LOADCTRLS";
        public const string ManagedRegValue = "MANAGED";
        public const string FdsPluginRegistryKey = "ACAD2FDS";
        public const string AutoCadRegistryKey = @"SOFTWARE\Autodesk\AutoCAD";
        public const string AutoCadApplicationsRegistryKeyName = "Applications";
        public const string CommandsRegistryKey = "Commands";
        public const string BuildMenuCommandName = "BuildFdsMenu";

        #endregion

        #region System

        public const string AutoCadProcessName = "acad";

        #endregion

    }
}