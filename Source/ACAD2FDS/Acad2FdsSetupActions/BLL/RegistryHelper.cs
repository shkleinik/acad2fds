namespace Acad2FdsSetupActions.BLL
{
    using System;
    using System.IO;
    using Microsoft.Win32;
    using Properties;

    public class RegistryHelper
    {
        public static bool IsAutoCadInstalled()
        {
            return null != Registry.LocalMachine.OpenSubKey(Constants.AutoCadRegistryKey);
        }

        public static void CreateFdsBranch(string pluginLocation, string acadRegistryKeyName)
        {
            var acadApplicationsKey = Registry.LocalMachine.OpenSubKey(acadRegistryKeyName, true);

            if (acadApplicationsKey == null)
                //throw new ArgumentException("Check if this acad product version is installed and this registry hive exists", "acadRegistryKeyName");
                return;

            var fdsKey = acadApplicationsKey.CreateSubKey(Constants.FdsPluginRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            fdsKey.SetValue(Constants.DescriptionRegValue, Resources.FdsPluginDescription, RegistryValueKind.String);
            var pathToPluginAssembly = Path.Combine(pluginLocation, Constants.FdsPluginAssemblyName);
            fdsKey.SetValue(Constants.LoaderRegValue, pathToPluginAssembly);
            fdsKey.SetValue(Constants.LoadctrlsRegValue, 2, RegistryValueKind.DWord);
            fdsKey.SetValue(Constants.ManagedRegValue, 1, RegistryValueKind.DWord);

            var fdsCommandsKey = fdsKey.CreateSubKey(Constants.CommandsRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree);

            fdsCommandsKey.SetValue(Constants.BuildMenuCommandName, Constants.BuildMenuCommandName, RegistryValueKind.String);
        }

        public static void RemoveFdsBranch(string acadRegistryKeyName)
        {
            var fdsPluginRegistryKey = String.Concat(acadRegistryKeyName, Constants.FdsPluginRegistryKey);

            if (Registry.LocalMachine.OpenSubKey(fdsPluginRegistryKey, true) == null)
                return;

            Registry.LocalMachine.DeleteSubKeyTree(fdsPluginRegistryKey);
        }
    }
}

// Todo :
// On installing plugin
//
// 1 Get all installed versions form SOFTWARE\Autodesk\AutoCAD key
//     (R1*.*\ACAD-*00*:***)
// 2 Get supported versions from predefined dictionary
//                 var versions = new Dictionary<string, string>
//                                {
//                                    {"ACAD-7001:409", "AutoCad"},
//                                    {"ACAD-7004:419", "AutoCad Architecture"}
//                                };
// 3 Populate dialog from this dictionary and allow user to select in wich product version 
//   should the plugin being installed.
// 
// 4 Create additional keys only for selected products.
// 
// On plugin unistall 
//
// 1 Scan registry and find for what products plugin was installed
// 2 Show user dialog, from wich he can select products, from wich he wants to uninstall plugin
// 3 a. If user chose all products - remove additional registry keys from all products hives and unistall binaries
//   b. User chose to unistall plugin only for some versions - remove additional registry keys from 
//      chosen products ONLY. DO NOT REMOVE BINARIES (prevent standart unistall).