namespace MaterialManager.BLL
{
    using System.Collections.Generic;

    public class Surface
    {
        public string ID { get; set; }

        public string MaterialID { get; set; }

        public string ForYouInformation { get; set; }

        public double Alpha { get; set; }

        public bool BurnAway { get; set; }

        public Backing Backing { get; set; }

        public double BurningRateMax { get; set; }

        public FdsColor Color { get; set; }

        public double C_Delta_Rho { get; set; }

        public double C_P { get; set; }

        public double Delta { get; set; }

        public double Density { get; set; }

        public double Emissivity { get; set; }

        public double ExtinguishingCoefficients { get; set; }

        public double HeatOfVaporization { get; set; }

        public double KS { get; set; }

        public double Porosity { get; set; }

        public List<Ramp> RAMP_C_P { get; set; }

        public List<Ramp> RAMP_KS { get; set; }

        public List<Ramp> Ramp_Q { get; set; }

        public double THICKNESS { get; set; }
    }
}