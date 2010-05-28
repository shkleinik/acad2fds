namespace GeometryConverter.Bases
{
    public class ElementBase
    {
        public double XLength;
        public double YLength;
        public double ZLength;

        #region Constructors

        public ElementBase(double length)
        {
            XLength = length;
            YLength = length;
            ZLength = length;
        }

        public ElementBase(double xLength, double yLength, double zLength)
        {
            XLength = xLength;
            YLength = yLength;
            ZLength = zLength;
        }

        #endregion

    }
}