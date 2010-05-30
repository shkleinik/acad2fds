namespace Fds2AcadPlugin.BLL
{
    using Autodesk.AutoCAD.ApplicationServices;
    using Autodesk.AutoCAD.Interop;
    using Autodesk.AutoCAD.Windows;
    using Configuration;
    using Helpers;

    public class DefaultFactory
    {
        public FdsPluginConfig CreateFdsConfig()
        {
            return XmlSerializer<FdsPluginConfig>.Deserialize(PluginInfoProvider.PathToPluginConfig);
        }

        public AcadApplication CreateAcadApplication()
        {
            return (AcadApplication)Application.AcadApplication;
        }

        public Window CreateAcadActiveWindow()
        {
            return Application.DocumentManager.MdiActiveDocument.Window;
        }

        public DocumentCollection CreateDocumentManager()
        {
            return Application.DocumentManager;
        }
    }
}