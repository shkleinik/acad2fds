using System.IO;
using System.Xml.Serialization;
using System;

namespace Fds2AcadPlugin.BLL.Configuration
{
    public class FdsPluginConfig
    {
        #region Constants

        public const string FdsFileSystemLocationPattern = @"{0}\Walash Ltd\Fds to AutoCad plugin\{1}";

        private const string ConfigFileName = "fdsPlugin.config";

        #endregion

        #region Properties

        public string PathToFds { get; set; }

        public string PathToSmokeView { get; set; }

        #endregion

        #region Methods

        public void InitializeFromFile()
        {
            var serializer = new XmlSerializer(typeof(FdsPluginConfig));
            try
            {
                var configFilePath = String.Format(FdsFileSystemLocationPattern,
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                    ConfigFileName
                    );

                Stream reader = new FileStream(configFilePath, FileMode.Open, FileAccess.Read);
                var fdsPluginConfig = serializer.Deserialize(reader) as FdsPluginConfig;
                reader.Close();
                PathToFds = fdsPluginConfig.PathToFds;
                PathToSmokeView = fdsPluginConfig.PathToSmokeView;

            }
            catch (Exception)
            {
            }
        }

        public void Save()
        {
            var configFilePath = String.Format(FdsFileSystemLocationPattern,
                Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                ConfigFileName
                );

            var serializer = new XmlSerializer(typeof(FdsPluginConfig));
            Stream writer = new FileStream(configFilePath, FileMode.Create);
            serializer.Serialize(writer, this);
            writer.Close();
        }

        #endregion
    }
}