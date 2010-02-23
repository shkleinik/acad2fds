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
            RegistryHelper.CreateFdsBranch();
        }

        [Test]
        public void RemoveFdsBranchTest()
        {
            RegistryHelper.RemoveFdsBranch();
        }

        [Test]
        public void IsAutoCadIntalledTest()
        {
            Assert.IsTrue(RegistryHelper.IsAutoCadInstalled());
        }
    }
}