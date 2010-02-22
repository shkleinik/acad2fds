namespace UnitTests.InstallerTests
{
    using MbUnit.Framework;
    using Fds2AcadSetupActions.BLL;

    [TestFixture]
    public class CommonHelperTests
    {
        [Test]
        public void IsAutoCadRunningTest()
        {
            Assert.IsTrue(CommonHelper.IsAutoCadRunning());
        }
    }
}