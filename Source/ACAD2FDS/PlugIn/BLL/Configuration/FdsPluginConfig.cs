namespace Fds2AcadPlugin.BLL.Configuration
{
    public class FdsPluginConfig
    {
        #region Properties

        public string PathToFds { get; set; }

        public string PathToSmokeView { get; set; }

        #endregion

        #region Methods

        //public void InitializeFromFile()
        //{
        //    var serializer = new XmlSerializer(typeof(FdsPluginConfig));

        //    try
        //    {
        //        var configFilePath = Path.Combine(AcadInfoProvider.GetPathToPluginDirectory(),Constants.ConfigName);

        //        Stream reader = new FileStream(configFilePath, FileMode.Open, FileAccess.Read);
        //        var fdsPluginConfig = serializer.Deserialize(reader) as FdsPluginConfig;
        //        reader.Close();
        //        PathToFds = fdsPluginConfig.PathToFds;
        //        PathToSmokeView = fdsPluginConfig.PathToSmokeView;

        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        //public void Save()
        //{
        //    var configFilePath = Path.Combine(AcadInfoProvider.GetPathToPluginDirectory(), Constants.ConfigName);

        //    var serializer = new XmlSerializer(typeof(FdsPluginConfig));
        //    Stream writer = new FileStream(configFilePath, FileMode.Create);
        //    serializer.Serialize(writer, this);
        //    writer.Close();
        //}

        #endregion
    }
}