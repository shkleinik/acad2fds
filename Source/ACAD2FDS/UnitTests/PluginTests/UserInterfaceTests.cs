using System.Collections.Generic;
using Fds2AcadPlugin.BLL.Helpers;

namespace UnitTests.PluginTests
{
    using Fds2AcadPlugin.UserInterface;
    using MbUnit.Framework;
    using Fds2AcadPlugin.UserInterface.Materials;
    using MaterialManager.BLL;

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

        [Test]
        public void MaterialEditorTest()
        {
            var r1 = new Ramp("C_P", 0.0, 0.0);
            var r2 = new Ramp("C_P", 1.0, 5.0);
            var r3 = new Ramp("C_P", 2.0, 15.0);

            var ramp_C_P = new List<Ramp> { r1, r2, r3 };

            var surface = new Surface
                              {
                                  ID = "Test",
                                  Alpha = 0.11,
                                  Backing = Backing.INSULATED,
                                  BurnAway = false,
                                  BurningRateMax = 0.03,
                                  C_Delta_Rho = 1.0,
                                  C_P = 0.88,
                                  Color = new FdsColor(255, 0, 0),
                                  Delta = 0.1,
                                  Density = 810,
                                  Emissivity = 0.8,
                                  ExtinguishingCoefficients = 0.5,
                                  ForYouInformation = "Test surface",
                                  HeatOfCombustion = 4000,
                                  HeatOfVaporization = 1620,
                                  KS = 0.19,
                                  MaterialCategory = MaterialCategory.LiquidFuel,
                                  Porosity = 0.5,
                                  RAMP_C_P = ramp_C_P,
                                  RAMP_KS = ramp_C_P,
                                  Ramp_Q = ramp_C_P
                              };

            var materialEditor = new MaterialEditor(surface);
            materialEditor.ShowDialog();

            var surfaceEditor = new MaterialEditor(materialEditor.Surface);
            surfaceEditor.ShowDialog();
        }

        [Test]
        public void MaterialMapperTest()
        {
            var usedMaterials = new List<string> { "wood", "plastic", "metan", "C4", "steel" };
            var availableSurfaces = new List<Surface> {
                new Surface{ID = "Surface for plastic"},
                new Surface{ID = "Surface for steel"},
                new Surface{ID = "Surface for metan"},
                new Surface{ID = "Surface for wood"},
                new Surface{ID = "Surface for C4"}
            };

            var materialMapper = new MaterialMapper(usedMaterials, availableSurfaces, new Dictionary<string, Surface>());
            materialMapper.ShowDialog();

            XmlSerializer<List<Entry>>.Serialize(materialMapper.MappingMaterials.ToEntryList(), @"C:\surface.xml");
            var deserialized = XmlSerializer<List<Entry>>.Deserialize(@"C:\surface.xml");

            var dict = deserialized.ToDictionary();

            var materialMapper2 = new MaterialMapper(usedMaterials, availableSurfaces, dict);
            materialMapper2.ShowDialog();
        }

        [Test]
        public void SerializationTest()
        {
            var r1 = new Ramp("C_P", 0.0, 0.0);
            var r2 = new Ramp("C_P", 1.0, 5.0);
            var r3 = new Ramp("C_P", 2.0, 15.0);

            var ramp_C_P = new List<Ramp> { r1, r2, r3 };

            var surface = new Surface
            {
                ID = "Test",
                Alpha = 0.11,
                Backing = Backing.INSULATED,
                BurnAway = false,
                BurningRateMax = 0.03,
                C_Delta_Rho = 1.0,
                C_P = 0.88,
                Color = new FdsColor(255, 0, 0),
                Delta = 0.1,
                Density = 810,
                Emissivity = 0.8,
                ExtinguishingCoefficients = 0.5,
                ForYouInformation = "Test surface",
                HeatOfCombustion = 4000,
                HeatOfVaporization = 1620,
                KS = 0.19,
                MaterialCategory = MaterialCategory.LiquidFuel,
                Porosity = 0.5,
                RAMP_C_P = ramp_C_P,
                RAMP_KS = ramp_C_P,
                Ramp_Q = ramp_C_P
            };

            var entries = new List<Entry>
                              {
                                  new Entry("qwerty", surface),
                                  new Entry("asdff", surface),
                                  new Entry("zxcvcz", surface)
                              };

            var store = @"C:\surface.xml";

            XmlSerializer<List<Entry>>.Serialize(entries, store);

            var entries2 = XmlSerializer<List<Entry>>.Deserialize(store);

        }
    }
}