using Fds2AcadSetupActions.BLL;
using MbUnit.Framework;

namespace UnitTests.InstallerTests
{
    [TestFixture]
    public class RegistryHelperTests
    {
        [Test]
        public void CreateFdsBranchTest()
        {
            RegistryHelper.CreateFdsBranch(Constants.AutoCadApplicationsRegistryKey);
            RegistryHelper.CreateFdsBranch(Constants.AutoCadArchitectureApplicationsRegistryKey);
        }

        [Test]
        public void RemoveFdsBranchTest()
        {
            RegistryHelper.RemoveFdsBranch(Constants.AutoCadApplicationsRegistryKey);
            RegistryHelper.RemoveFdsBranch(Constants.AutoCadArchitectureApplicationsRegistryKey);
        }

        [Test]
        public void IsAutoCadIntalledTest()
        {
            Assert.IsTrue(RegistryHelper.IsAutoCadInstalled());
        }
    }
}