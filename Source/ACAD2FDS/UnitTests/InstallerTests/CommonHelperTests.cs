namespace UnitTests.InstallerTests
{
    using Acad2FdsSetupActions.BLL;
    using MbUnit.Framework;

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