namespace UnitTests.PluginTests
{
    using Fds2AcadPlugin;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandsTests
    {
        [TestMethod]
        public void ViewResultInSmokeViewTest()
        {
            EntryPoint.ViewResultInSmokeView();
        }
    }
}