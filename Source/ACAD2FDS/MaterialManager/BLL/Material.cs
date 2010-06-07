namespace MaterialManager.BLL
{
    using System.Collections.Generic;

    public class Material
    {
        public string ID { get; set; }

        public string FYI { get; set; }

        public MaterialCategory MaterialCategory { get; set; }

        public double SPECIFIC_HEAT { get; set; }

        public double CONDUCTIVITY { get; set; }

        public double DENSITY { get; set; }

        public int N_REACTIONS { get; set; }

        public double NU_FUEL { get; set; }

        public double REFERENCE_TEMPERATURE { get; set; }

        public double HEAT_OF_REACTION { get; set; }

        public double HEAT_OF_COMBUSTION { get; set; }

        public List<Ramp> SPECIFIC_HEAT_RAMP { get; set; }

        public List<Ramp> CONDUCTIVITY_RAMP { get; set; }

    }
}