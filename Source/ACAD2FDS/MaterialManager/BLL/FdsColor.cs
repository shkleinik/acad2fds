namespace MaterialManager.BLL
{
    public class FdsColor
    {
        private FdsColor() { }

        public FdsColor(int Red, int Green, int Blue)
        {
            R = Red;
            G = Green;
            B = Blue;
        }

        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
    }
}