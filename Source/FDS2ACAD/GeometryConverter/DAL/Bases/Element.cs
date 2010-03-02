namespace GeometryConverter.DAL.Bases
{
    public class Element : ElementBase
    {
        public BasePoint Center;
        public string Material;

        /// <summary>
        /// Imagine OX runing from lefts to rights...
        /// Imagine OY runing from you ahead far away...
        /// Imagine OZ runing from ground to skies...
        /// Here are your neighbours:
        /// </summary>
        public int? NeighbourTop;
        public int? NeighbourBottom;
        public int? NeighbourFront;
        public int? NeighbourBack;
        public int? NeighbourLeft;
        public int? NeighbourRight;

        public double X1 { get { return Center.X - XLength / 2; } }
        public double X2 { get { return Center.X + XLength / 2; } }
        public double Y1 { get { return Center.Y - YLength / 2; } }
        public double Y2 { get { return Center.Y + YLength / 2; } }
        public double Z1 { get { return Center.Z - ZLength / 2; } }
        public double Z2 { get { return Center.Z + ZLength / 2; } }

        #region Constructors

        public Element(BasePoint center, double xLength, double yLength, double zLength)
            : base(xLength, yLength, zLength)
        {
            Center = center;
            XLength = xLength;
            YLength = yLength;
            ZLength = zLength;
            Material = string.Empty;
            ResetNeighbours();
        }

        public Element(BasePoint center, ElementBase elementBase)
            : base(elementBase.XLength, elementBase.YLength, elementBase.ZLength)
        {
            Center = center;
            XLength = elementBase.XLength;
            YLength = elementBase.YLength;
            ZLength = elementBase.ZLength;
            Material = string.Empty;
            ResetNeighbours();
        }

        /// <summary>
        /// Set all neighbourhood relations of current element to null
        /// </summary>
        public void ResetNeighbours()
        {
            NeighbourTop = null;
            NeighbourBottom = null;
            NeighbourFront = null;
            NeighbourBack = null;
            NeighbourLeft = null;
            NeighbourRight = null;
        }

        #endregion
    }
}