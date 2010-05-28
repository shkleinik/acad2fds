namespace MaterialManager.BLL
{
    public class Material
    {
        public string ID { get; set; }

        public string FYI { get; set; }

        public string SPECIFIC_HEAT { get; set; }

        public double CONDUCTIVITY { get; set; }

        public double DENSITY { get; set; }

        public double N_REACTIONS { get; set; }

        public double NU_FUEL { get; set; }

        public double REFERENCE_TEMPERATURE { get; set; }

        public double HEAT_OF_REACTION { get; set; }

        public double HEAT_OF_COMBUSTION { get; set; }
    }
}