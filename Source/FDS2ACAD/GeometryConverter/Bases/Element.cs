namespace GeometryConverter.Bases
{
    using System;

    public class Element : ElementBase, ICloneable
    {
        public BasePoint Center;

        public string Material { get; set; }

        private const int NeighboursNumber = 6;

        #region Neighbour properties

        /// <summary>
        /// Imagine OX runing from lefts to rights...
        /// Imagine OY runing from you ahead far away...
        /// Imagine OZ runing from ground to skies...
        /// Here are your neighbours:
        /// </summary>
        public int? NeighbourTop { get { return Neighbours[0]; } set { Neighbours[0] = value; } }
        public int? NeighbourBottom { get { return Neighbours[1]; } set { Neighbours[1] = value; } }
        public int? NeighbourFront { get { return Neighbours[2]; } set { Neighbours[2] = value; } }
        public int? NeighbourBack { get { return Neighbours[3]; } set { Neighbours[3] = value; } }
        public int? NeighbourLeft { get { return Neighbours[4]; } set { Neighbours[4] = value; } }
        public int? NeighbourRight { get { return Neighbours[5]; } set { Neighbours[5] = value; } }
        #endregion

        // Todo : Try to replace links to neighbour with reference : public Element[] Neighbours
        public int?[] Neighbours;

        public int? Index;

        #region Properties

        public double X1 { get { return Center.X - XLength / 2; } }
        public double X2 { get { return Center.X + XLength / 2; } }
        public double Y1 { get { return Center.Y - YLength / 2; } }
        public double Y2 { get { return Center.Y + YLength / 2; } }
        public double Z1 { get { return Center.Z - ZLength / 2; } }
        public double Z2 { get { return Center.Z + ZLength / 2; } }

        #endregion

        #region Properties for FDS

        public double FdsX1 { get { return Math.Round(X1, 0); } }
        public double FdsX2 { get { return Math.Round(X2, 0); } }
        public double FdsY1 { get { return Math.Round(Y1, 0); } }
        public double FdsY2 { get { return Math.Round(Y2, 0); } }
        public double FdsZ1 { get { return Math.Round(Z1, 0); } }
        public double FdsZ2 { get { return Math.Round(Z2, 0); } }

        #endregion

        #region Constructors

        public Element(BasePoint center, double xLength, double yLength, double zLength)
            : this(center, xLength, yLength, zLength, new int?[NeighboursNumber], string.Empty, null)
        {
        }

        public Element(BasePoint center, ElementBase elementBase, int index)
            : this(center, elementBase.XLength, elementBase.YLength, elementBase.ZLength, new int?[NeighboursNumber], string.Empty, index)
        {
        }

        protected Element(BasePoint center, double xLength, double yLength, double zLength, int?[] neighbours, string material, int? index)
            : base(xLength, yLength, zLength)
        {
            Center = center;
            XLength = xLength;
            YLength = yLength;
            ZLength = zLength;
            Material = material;
            Neighbours = neighbours;
            Index = index;
        }

        /// <summary>
        /// Set all neighbourhood relations of current element to null
        /// </summary>
        public void ResetNeighbours()
        {
            Neighbours = new int?[NeighboursNumber];

            for (var i = 0; i < Neighbours.Length; i++)
            {
                Neighbours[i] = null;
            }
        }

        #endregion

        public void DefinePositionIfNeighbour(Element anotherElement)
        {
            // this coef guaratees that center will fall in the interval
            var k = 1.5;

            if (!((Center.X - anotherElement.Center.X < k * XLength) &&
                  (Center.Y - anotherElement.Center.Y < k * YLength) &&
                  (Center.Z - anotherElement.Center.Z < k * ZLength)))
                return;

            Neighbours[(int)Center.GetPosition(anotherElement.Center)] = anotherElement.Index;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new Element((BasePoint)Center.Clone(), XLength, YLength, ZLength, Neighbours, Material, Index);
        }

        #endregion
    }
}