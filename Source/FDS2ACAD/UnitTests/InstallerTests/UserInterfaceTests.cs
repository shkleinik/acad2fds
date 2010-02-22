namespace UnitTests.InstallerTests
{
    using MbUnit.Framework;
    using Fds2AcadSetupActions.UserInterface;

    [TestFixture]
    public class UserInterfaceTests
    {
        [Test]
        public void PluginOptionsTest()
        {
            var plugionOptions = new PluginOptions();
            plugionOptions.ShowDialog();
        }
    }
}