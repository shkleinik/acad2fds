namespace UnitTests.InstallerTests
{
    using Fds2AcadSetupActions.BLL;
    using MbUnit.Framework;

    [TestFixture]
    public class AcadAutoLoadModifierTests
    {
        [Test]
        public void AddCommandToAutocad2009StartUpTest()
        {
            AcadAutoLoadModifier.AddCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand, Constants.AutoCad2009AutoLoadFilePath);
            AcadAutoLoadModifier.AddCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand, Constants.AutoCad2009ArchitectureAutoLoadFilePath);
        }

        [Test]
        public void RemoveCommandToAutocad2009StartUpTest()
        {
            AcadAutoLoadModifier.RemoveCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand, Constants.AutoCad2009AutoLoadFilePath);
            AcadAutoLoadModifier.RemoveCommandToAutocad2009StartUp(Constants.FdsMenuBuildCommand, Constants.AutoCad2009ArchitectureAutoLoadFilePath);
        }
    }
}