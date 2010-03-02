    namespace MaterialManager.BLL
{
    public class Material
    {
        public string ID { get; set; }

        public double Conductivity { get; set; }

        public double SpecificHeat { get; set; }

        public double Emissivity { get; set; }

        public double Density { get; set; }

        public double HeatOfReaction { get; set; }

        public MaterialType MaterialType { get; set; }
    }
}