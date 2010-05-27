namespace Fds2AcadPlugin.BLL
{
    using Autodesk.AutoCAD.Interop;
    using Autodesk.AutoCAD.ApplicationServices;
    using Configuration;
    using Autodesk.AutoCAD.Windows;

    public class DefaultFactory
    {
        public ConfigProvider CreateConfigProvider()
        {
            return new ConfigProvider();
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