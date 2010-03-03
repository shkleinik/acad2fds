namespace Fds2AcadSetupActions.BLL
{
    using Microsoft.Win32;
    using Properties;
    using System;

    public class RegistryHelper
    {
        public static bool IsAutoCadInstalled()
        {
            return null != Registry.LocalMachine.OpenSubKey(Constants.AutoCadRegistryKey);
        }

        public static void CreateFdsBranch()
        {
            var acadApplicationsKey = Registry.LocalMachine.OpenSubKey(Constants.AutoCadApplicationsRegistryKey, true);
            var fdsKey = acadApplicationsKey.CreateSubKey(Constants.FdsPluginRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree);
            fdsKey.SetValue(Constants.DescriptionRegValue, Resources.FdsPluginDescription, RegistryValueKind.String);
            var pathToPluginAssembly = string.Format(Constants.PluginFileSystemLocationPattern,
                                                     Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                                     Constants.FdsPluginAssemblyName
                                                     );
            fdsKey.SetValue(Constants.LoaderRegValue, pathToPluginAssembly);
            fdsKey.SetValue(Constants.LoadctrlsRegValue, 2, RegistryValueKind.DWord);
            fdsKey.SetValue(Constants.ManagedRegValue, 1, RegistryValueKind.DWord);

            var fdsCommandsKey = fdsKey.CreateSubKey(Constants.CommandsRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree);

            fdsCommandsKey.SetValue(Constants.BuildMenuCommandName, Constants.BuildMenuCommandName, RegistryValueKind.String);
        }

        public static void RemoveFdsBranch()
        {
            var fdsPluginRegistryKey = String.Concat(Constants.AutoCadApplicationsRegistryKey, Constants.FdsPluginRegistryKey);
            Registry.LocalMachine.DeleteSubKeyTree(fdsPluginRegistryKey);
        }
    }
}