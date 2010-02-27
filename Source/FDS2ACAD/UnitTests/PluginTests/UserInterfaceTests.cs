using Fds2AcadPlugin.UserInterface;
using MbUnit.Framework;

namespace UnitTests.PluginTests
{
    [TestFixture]
    public class UserInterfaceTests
    {
        [Test]
        public void PluginOptionsTest()
        {
            var plugionOptions = new PluginOptions();
            plugionOptions.ShowDialog();
        }


        [Test]
        public void CalculationInfoTest()
        {
            var calculationInfo = new CalculationInfo();
            calculationInfo.ShowDialog();
        }
    }
}