namespace GeometryConverter.BLL.Bases
{
    class Element : ElementBase
    {
        public BasePoint Center;

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

        #region Constructors

        public Element(BasePoint center, double xLength, double yLength, double zLength)
            : base(xLength, yLength, zLength)
        {
            Center = center;
            XLength = xLength;
            YLength = yLength;
            ZLength = zLength;
            ResetNeighbours();
        }

        public Element(BasePoint center, ElementBase elementBase)
            : base(elementBase.XLength, elementBase.YLength, elementBase.ZLength)
        {
            Center = center;
            XLength = elementBase.XLength;
            YLength = elementBase.YLength;
            ZLength = elementBase.ZLength;
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