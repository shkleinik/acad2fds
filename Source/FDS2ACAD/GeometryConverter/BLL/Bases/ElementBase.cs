namespace GeometryConverter.BLL.Bases
{
    class ElementBase
    {
        public double XLength;
        public double YLength;
        public double ZLength;

        #region Constructors

        public ElementBase(double xLength, double yLength, double zLength)
        {
            XLength = xLength;
            YLength = yLength;
            ZLength = zLength;
        }

        #endregion

    }
}
