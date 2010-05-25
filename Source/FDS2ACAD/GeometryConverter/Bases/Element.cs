using GeometryConverter.Helpers;

namespace GeometryConverter.Bases
{
    using System;

    public class Element : ElementBase, ICloneable
    {
        public BasePoint Center;

        public string Material { get; set; }

        private const int neighboursNumber = 6;

        #region Neighbour properties

        /// <summary>
        /// Imagine OX runing from lefts to rights...
        /// Imagine OY runing from you ahead far away...
        /// Imagine OZ runing from ground to skies...
        /// Here are your neighbours:
        /// </summary>
        public Element NeighbourTop
        {
            get
            {
                return Neighbours[(int)Direction.Top];
            }

            set
            {
                Neighbours[(int)Direction.Top] = value;
            }
        }

        public Element NeighbourBottom
        {
            get
            {
                return Neighbours[(int)Direction.Bottom];
            }

            set
            {
                Neighbours[(int)Direction.Bottom] = value;
            }
        }

        public Element NeighbourFront
        {
            get
            {
                return Neighbours[(int)Direction.Front];
            }

            set
            {
                Neighbours[(int)Direction.Front] = value;
            }
        }

        public Element NeighbourBack
        {
            get
            {
                return Neighbours[(int)Direction.Back];
            }

            set
            {
                Neighbours[(int)Direction.Back] = value;
            }
        }

        public Element NeighbourLeft
        {
            get
            {
                return Neighbours[(int)Direction.Left];
            }

            set
            {
                Neighbours[(int)Direction.Left] = value;
            }
        }

        public Element NeighbourRight
        {
            get
            {
                return Neighbours[(int)Direction.Right];
            }

            set
            {
                Neighbours[(int)Direction.Right] = value;
            }
        }

        #endregion

        public Element[] Neighbours;

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
            : this(center, xLength, yLength, zLength, new Element[neighboursNumber], string.Empty, null)
        {
        }

        public Element(BasePoint center, ElementBase elementBase, int index)
            : this(center, elementBase.XLength, elementBase.YLength, elementBase.ZLength, new Element[neighboursNumber], string.Empty, index)
        {
        }

        protected Element(BasePoint center, double xLength, double yLength, double zLength, Element[] neighbours, string material, int? index)
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
            Neighbours = new Element[neighboursNumber];

            for (var i = 0; i < Neighbours.Length; i++)
            {
                Neighbours[i] = null;
            }
        }

        #endregion

        public void SetReferenceIfNeighbour(Element anotherElement)
        {
            // this coef guaratees that center will fall in the interval
            const double k = 1.2;

            if (!((Math.Abs(Center.X - anotherElement.Center.X) < k * XLength) &&
                    (Math.Abs(Center.Y - anotherElement.Center.Y) < k * YLength) &&
                    (Math.Abs(Center.Z - anotherElement.Center.Z) < k * ZLength)))
                return;


            var positionOfAnotherElement = Center.GetPosition(anotherElement.Center);
            if (positionOfAnotherElement != null)
                //if (Neighbours[(int)positionOfAnotherElement] == null)
                    Neighbours[(int)positionOfAnotherElement] = anotherElement;
            
            //anotherElement.Neighbours[(int) ElementHelper.GetInverseDirection((Direction) positionOfAnotherElement)] = this;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new Element((BasePoint)Center.Clone(), XLength, YLength, ZLength, Neighbours, Material, Index);
        }

        #endregion
    }
}