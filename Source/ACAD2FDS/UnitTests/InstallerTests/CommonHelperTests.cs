using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.InstallerTests
{
    using Acad2FdsSetupActions.BLL;

    [TestClass]
    public class CommonHelperTests
    {
        [TestMethod]
        public void IsAutoCadRunningTest()
        {
            Assert.IsTrue(CommonHelper.IsAutoCadRunning());
        }
    }
}