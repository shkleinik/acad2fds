using System.IO;
using System.Reflection;

namespace Common
{
    public static class Info
    {
        private static readonly string pluginDirectory;

        static Info()
        {
            pluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        public static string PluginDirectory
        {
            get
            {
                return pluginDirectory;
            }
        }
    }
}