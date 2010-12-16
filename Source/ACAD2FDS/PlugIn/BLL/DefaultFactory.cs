namespace Fds2AcadPlugin.BLL
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.Interop;
    using Autodesk.AutoCAD.Windows;
    using Common;
    using Configuration;
    using Helpers;

    public class DefaultFactory
    {
        public DefaultFactory()
        {
        }

        public DefaultFactory(ILogger log)
        {
            Log = log;
        }

        private ILogger Log { get; set; }

        public FdsPluginConfig CreateFdsConfig()
        {
            return XmlSerializer<FdsPluginConfig>.Deserialize(PluginInfoProvider.PathToPluginConfig, Log);
        }

        public AcadApplication CreateAcadApplication()
        {
            return (AcadApplication)Application.AcadApplication;
        }

        public Window GetAcadActiveWindow()
        {
            return Application.DocumentManager.MdiActiveDocument.Window;
        }

        public Window GetAcadMainWindows()
        {
            return Application.MainWindow;
        }

        public DocumentCollection CreateDocumentManager()
        {
            return Application.DocumentManager;
        }
    }
}