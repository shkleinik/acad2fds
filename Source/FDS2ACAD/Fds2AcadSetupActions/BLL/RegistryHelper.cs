using Microsoft.Win32;
using Fds2AcadSetupActions.Properties;
using System;

namespace Fds2AcadSetupActions.BLL
{
    public class RegistryHelper
    {
        public static bool IsAutoCadInstalled()
        {
            return null != Registry.LocalMachine.OpenSubKey(Constants.AutoCadRegistryKey);
        }

        public static void CreateFdsBrunch()
        {
            var acadApplicationsKey = Registry.LocalMachine.OpenSubKey(Constants.AutoCadApplicationsRegistryKey, true);
            var fdsKey = acadApplicationsKey.CreateSubKey(Constants.FdsPluginRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            fdsKey.SetValue(Constants.DescriptionRegValue, Resources.FdsPluginDescription, RegistryValueKind.String);
            var pathToPluginAssembly = string.Format(Constants.FdsFileSystemLocationPattern,
                                                     Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                                     Constants.FdsPluginAssemblyName
                                                     );
            fdsKey.SetValue(Constants.LoaderRegValue, pathToPluginAssembly);
            fdsKey.SetValue(Constants.LoadctrlsRegValue, 2, RegistryValueKind.DWord);
            fdsKey.SetValue(Constants.ManagedRegValue, 1, RegistryValueKind.DWord);
        }

        public static void RemoveFdsBrunch()
        {
            var fdsPluginRegistryKey = String.Concat(Constants.AutoCadApplicationsRegistryKey, Constants.FdsPluginRegistryKey);
            Registry.LocalMachine.DeleteSubKeyTree(fdsPluginRegistryKey);
        }
    }
}