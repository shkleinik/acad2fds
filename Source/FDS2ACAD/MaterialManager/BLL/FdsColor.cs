namespace MaterialManager.BLL
{
    public class FdsColor
    {
        private FdsColor() { }

        public FdsColor(double Red, double Green, double Blue)
        {
            R = Red;
            G = Green;
            B = Blue;
        }

        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }
    }
}