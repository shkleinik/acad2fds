namespace GeometryConverter
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using Autodesk.AutoCAD.DatabaseServices;
    using Bases;
    using Helpers;
    using Element = Bases.Element;

    public class SolidToElementConverter
    {
        #region Fields

        protected List<Entity> _solids;
        protected ElementBase _elementBase;
        private List<Element> _fullCollection;
        private List<Element> _valueableElements;

        #endregion

        #region Properties

        public BasePoint[] MaxMinPoint { get; private set; }

        public List<Element> AllElements { get { return GetAllElements(); } }

        public List<Element> ValueableElements { get { return GetValuableElements(); } }

        #endregion

        #region Constructors

        public SolidToElementConverter(Entity solid)
            : this(new List<Entity> { solid })
        {

        }

        public SolidToElementConverter(List<Entity> solids)
        {
            _solids = solids;
            MaxMinPoint = GetMaxMinPoint(_solids);
            _elementBase = InitializeElementBase();
        }

        #endregion

        #region Internal implementation

        /// <summary>
        /// Returns minimal and maximal Bases point
        /// </summary>
        /// <returns>Array of 2 elements where [0] is Max and [1] is Min</returns>
        private static BasePoint[] GetMaxMinPoint(IEnumerable<Entity> solids)
        {
            var result = new BasePoint[2];
            var xMax = double.MinValue;
            var yMax = double.MinValue;
            var zMax = double.MinValue;
            var xMin = double.MaxValue;
            var yMin = double.MaxValue;
            var zMin = double.MaxValue;

            foreach (var solid in solids)
            {
                var brep = new Brep(solid);
                using (brep)
                {
                    foreach (var edg in brep.Edges)
                    {
                        var tmp = CastHelper.ConvertToBasePoint(edg.Vertex1.Point);

                        if (tmp.X > xMax) xMax = tmp.X;
                        if (tmp.X < xMin) xMin = tmp.X;
                        if (tmp.Y > yMax) yMax = tmp.Y;
                        if (tmp.Y < yMin) yMin = tmp.Y;
                        if (tmp.Z > zMax) zMax = tmp.Z;
                        if (tmp.Z < zMin) zMin = tmp.Z;
                    }
                }
            }

            result[0] = new BasePoint(xMin, yMin, zMin);
            result[1] = new BasePoint(xMax, yMax, zMax);
            return result;
        }

        /// <summary>
        /// Initialize _elementBase through the information about mcd of edges
        /// </summary>
        /// <returns>Element base</returns>
        private ElementBase InitializeElementBase()
        {
            var xEdges = new List<Edge>();
            var yEdges = new List<Edge>();
            var zEdges = new List<Edge>();

            var xPoints = new List<double>();
            var yPoints = new List<double>();
            var zPoints = new List<double>();

            var totalEdges = 0;
            const int deltaTotalEdges = 1;

            foreach (var solid in _solids)
            {
                var brep = new Brep(solid);
                using (brep)
                {
                    foreach (var edg in brep.Edges)
                    {
                        xPoints.Add(edg.Vertex1.Point.X);
                        xPoints.Add(edg.Vertex2.Point.X);
                        yPoints.Add(edg.Vertex1.Point.Y);
                        yPoints.Add(edg.Vertex2.Point.Y);
                        zPoints.Add(edg.Vertex1.Point.Z);
                        zPoints.Add(edg.Vertex2.Point.Z);

                        if (edg.IsAlongX())
                            xEdges.Add(edg);
                        else if (edg.IsAlongY())
                            yEdges.Add(edg);
                        else if (edg.IsAlongZ())
                            zEdges.Add(edg);

                        totalEdges++;
                    }
                }
            }

            double xLength;
            double yLength;
            double zLength;

            if (totalEdges - (xEdges.Count + yEdges.Count + zEdges.Count) > deltaTotalEdges)
            {
                xEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));
                yEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));
                zEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));

                //xLength = xEdges[0].Length();
                //yLength = yEdges[0].Length();
                //zLength = zEdges[0].Length();

                xLength = MathOperations.FindGcd(xEdges);
                yLength = MathOperations.FindGcd(yEdges);
                zLength = MathOperations.FindGcd(zEdges);
            }
            else
            {
                xLength = MathOperations.GetElementLengthByPoints(xPoints);
                yLength = MathOperations.GetElementLengthByPoints(yPoints);
                zLength = MathOperations.GetElementLengthByPoints(zPoints);
            }

            return new ElementBase(xLength, yLength, zLength);
        }


        /// <summary>
        /// Provides collection of all elements bounded by rectangle of MaxMinPoint
        /// </summary>
        /// <returns>Collection of all elements</returns>
        protected List<Element> GetAllElements()
        {
            if (_fullCollection == null)
                _fullCollection = new List<Element>();
            else
                return _fullCollection;

            var limitX = Math.Abs((int)(MaxMinPoint[0].X - MaxMinPoint[1].X) / _elementBase.XLength);
            var limitY = Math.Abs((int)(MaxMinPoint[0].Y - MaxMinPoint[1].Y) / _elementBase.YLength);
            var limitZ = Math.Abs((int)(MaxMinPoint[0].Z - MaxMinPoint[1].Z) / _elementBase.ZLength);

            var startX = MaxMinPoint[0].X;
            var startY = MaxMinPoint[0].Y;
            var startZ = MaxMinPoint[0].Z;

            for (var z = 0; z < limitZ; z++)
                for (var y = 0; y < limitY; y++)
                    for (var x = 0; x < limitX; x++)
                    {
                        var center = new BasePoint(startX + (x + 0.5) * _elementBase.XLength,
                                                   startY + (y + 0.5) * _elementBase.YLength,
                                                   startZ + (z + 0.5) * _elementBase.ZLength);

                        var element = new Element(center, _elementBase, _fullCollection.Count);
                        _fullCollection.Add(element);
                    }

            return _fullCollection;
        }

        /// <summary>
        /// Provides collection of valuable elements that belongs to the Solid from all elements
        /// </summary>
        /// <returns>Collection of valuable elements</returns>
        private List<Element> GetValuableElements()
        {
            if (_valueableElements == null)
                _valueableElements = new List<Element>();
            else
                return _valueableElements;


            for (var i = 0; i < AllElements.Count; i++)
            {
                foreach (var solid in _solids)
                {
                    var brep = new Brep(solid);
                    PointContainment containment;
                    brep.GetPointContainment(AllElements[i].Center.ConverToAcadPoint(), out containment);

                    if (containment != PointContainment.Inside)
                        continue;

                    AllElements[i].Material = solid.Material;

                    var tmpElement = (Element)AllElements[i].Clone();
                    tmpElement.Index = _valueableElements.Count;

                    _valueableElements.Add(tmpElement);
                }
            }

            return _valueableElements;
        }

        #endregion
    }
}