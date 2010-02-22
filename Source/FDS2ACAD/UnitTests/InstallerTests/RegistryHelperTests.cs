using Fds2AcadSetupActions.BLL;
using MbUnit.Framework;

namespace UnitTests.InstallerTests
{
    [TestFixture]
    public class RegistryHelperTests
    {
        [Test]
        public void CreateFdsBrunchTest()
        {
            RegistryHelper.CreateFdsBrunch();
        }

        [Test]
        public void RemoveFdsBrunchTest()
        {
            RegistryHelper.RemoveFdsBrunch();
        }

        [Test]
        public void IsAutoCadIntalledTest()
        {
            Assert.IsTrue(RegistryHelper.IsAutoCadInstalled());
        }
    }
}