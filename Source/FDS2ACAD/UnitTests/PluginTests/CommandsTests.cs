namespace UnitTests.PluginTests
{
    using MbUnit.Framework;
    using Fds2AcadPlugin;

    [TestFixture]
    public class CommandsTests
    {
        [Test]
        public void ViewResultInSmokeViewTest()
        {
            EntryPoint.ViewResultInSmokeView();
        }
    }
}