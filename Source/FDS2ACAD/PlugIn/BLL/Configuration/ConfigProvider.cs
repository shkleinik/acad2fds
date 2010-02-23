namespace Fds2AcadPlugin.BLL.Configuration
{
    public class ConfigProvider
    {
        public string PathToFds
        {
            get
            {
                var fdsPluginConfig = new FdsPluginConfig();
                fdsPluginConfig.InitializeFromFile();
                return fdsPluginConfig.PathToFds;
            }
        }

        public string PathToSmokeView
        {
            get
            {
                var fdsPluginConfig = new FdsPluginConfig();
                fdsPluginConfig.InitializeFromFile();
                return fdsPluginConfig.PathToSmokeView;
            }
        }
    }
}