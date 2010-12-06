namespace UnitTests.InstallerTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Acad2FdsSetupActions.BLL;

    [TestClass]
    public class RegistryHelperTests
    {
        [TestMethod]
        public void CreateFdsBranchTest()
        {
            RegistryHelper.CreateFdsBranchForEachAutoCadInstance("c:\\Test.dll");
        }

        [TestMethod]
        public void RemoveFdsBranchTest()
        {
            RegistryHelper.RemoveFdsBranchFromEachAutoCadInstance();
        }

        [TestMethod]
        public void IsAutoCadIntalledTest()
        {
            Assert.IsTrue(RegistryHelper.IsAutoCadInstalled());
        }
    }
}