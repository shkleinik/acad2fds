namespace Acad2FdsSetupActions.BLL
{
    using System.Collections.Generic;
    using Microsoft.Win32;
    using Properties;

    public class RegistryHelper
    {
        private delegate bool RegistryAction(RegistryKey registryKey, params object[] objects);

        public static bool IsAutoCadInstalled()
        {
            return null != Registry.LocalMachine.OpenSubKey(Constants.AutoCadRegistryKey);
        }

        public static IList<string> CreateFdsBranchForEachAutoCadInstance(string pathToPluginAssembly)
        {
            return IterateAutocadProducts(CreateFdsBranch, pathToPluginAssembly);
        }

        public static IList<string> RemoveFdsBranchFromEachAutoCadInstance()
        {
            return IterateAutocadProducts(RemoveFdsBranch, null);
        }

        private static IList<string> IterateAutocadProducts(RegistryAction action, params object[] objects)
        {
            var autocadInstances = new List<string>();

            var autocadRootKey = Registry.LocalMachine.OpenSubKey(Constants.AutoCadRegistryKey, true);

            if (autocadRootKey != null)
            {
                var autocadVersionsNames = autocadRootKey.GetSubKeyNames();

                foreach (var autocadVersionName in autocadVersionsNames)
                {
                    var autocadVersion = autocadRootKey.OpenSubKey(autocadVersionName, true);

                    if (autocadVersion != null)
                    {
                        var autocadProductNames = autocadVersion.GetSubKeyNames();

                        foreach (var autocadProductName in autocadProductNames)
                        {
                            var autocadProduct = autocadVersion.OpenSubKey(autocadProductName);

                            if (autocadProduct != null)
                            {
                                var productApplications = autocadProduct.OpenSubKey(Constants.AutoCadApplicationsRegistryKeyName, true);

                                if (action(productApplications, objects))
                                    autocadInstances.Add(autocadProduct.Name.Replace(@"HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\", ""));
                            }
                        }
                    }
                }
            }

            return autocadInstances;
        }

        /*

        public static IList<string> CreateFdsBranchForEachAutoCadInstance(string pathToPluginAssembly)
        {
            var autocadInstances = new List<string>();

            var autocadRootKey = Registry.LocalMachine.OpenSubKey(Constants.AutoCadRegistryKey, true);

            if (autocadRootKey != null)
            {
                var autocadVersionsNames = autocadRootKey.GetSubKeyNames();

                foreach (var autocadVersionName in autocadVersionsNames)
                {
                    var autocadVersion = autocadRootKey.OpenSubKey(autocadVersionName, true);

                    if (autocadVersion != null)
                    {
                        var autocadProductNames = autocadVersion.GetSubKeyNames();

                        foreach (var autocadProductName in autocadProductNames)
                        {
                            var autocadProduct = autocadVersion.OpenSubKey(autocadProductName);

                            if (autocadProduct != null)
                            {
                                var productApplications = autocadProduct.OpenSubKey(Constants.AutoCadApplicationsRegistryKeyName, true);

                                if (CreateFdsBranch(pathToPluginAssembly, productApplications))
                                    autocadInstances.Add(autocadProduct.Name.Replace(@"HKEY_LOCAL_MACHINE\SOFTWARE\Autodesk\AutoCAD\", ""));
                            }
                        }
                    }
                }
            }

            return autocadInstances;
        }

        */

        private static bool CreateFdsBranch(RegistryKey acadApplicationsKey, params object[] pathToPluginAssembly)
        {
            if (acadApplicationsKey == null || pathToPluginAssembly == null || pathToPluginAssembly.Length == 0)
                return false;

            var fdsKey = acadApplicationsKey.CreateSubKey(Constants.FdsPluginRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            fdsKey.SetValue(Constants.DescriptionRegValue, Resources.FdsPluginDescription, RegistryValueKind.String);
            fdsKey.SetValue(Constants.LoaderRegValue, pathToPluginAssembly[0].ToString());
            fdsKey.SetValue(Constants.LoadctrlsRegValue, 2, RegistryValueKind.DWord);
            fdsKey.SetValue(Constants.ManagedRegValue, 1, RegistryValueKind.DWord);

            var fdsCommandsKey = fdsKey.CreateSubKey(Constants.CommandsRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree);

            fdsCommandsKey.SetValue(Constants.BuildMenuCommandName, Constants.BuildMenuCommandName, RegistryValueKind.String);

            return true;
        }

        private static bool RemoveFdsBranch(RegistryKey acadRegistryKeyName, params object[] objects)
        {
            if (acadRegistryKeyName == null)
                return false;

            var fdsPluginRegistryKey = acadRegistryKeyName.OpenSubKey(Constants.FdsPluginRegistryKey);

            if (fdsPluginRegistryKey == null)
                return false;

            acadRegistryKeyName.DeleteSubKeyTree(Constants.FdsPluginRegistryKey);

            return true;
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