namespace GeometryConverter
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using Autodesk.AutoCAD.DatabaseServices;
    using Bases;
    using Helpers;
    using Face = Autodesk.AutoCAD.BoundaryRepresentation.Face;
    using Element = Bases.Element;

    public class SolidToElementConverter
    {
        #region Fields
        protected List<Solid3d> _solids;
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

        public SolidToElementConverter(Solid3d solid)
            : this(new List<Solid3d> { solid })
        {

        }

        public SolidToElementConverter(List<Solid3d> solids)
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
        private static BasePoint[] GetMaxMinPoint(IEnumerable<Solid3d> solids)
        {
            var result = new BasePoint[2];
            double xMax = double.MinValue;
            double yMax = double.MinValue;
            double zMax = double.MinValue;
            double xMin = double.MaxValue;
            double yMin = double.MaxValue;
            double zMin = double.MaxValue;
            foreach (Solid3d solid in solids)
            {
                Brep brep = new Brep(solid);
                using (brep)
                {
                    foreach (Complex cmp in brep.Complexes)
                    {
                        foreach (Shell shl in cmp.Shells)
                        {
                            foreach (Face fce in shl.Faces)
                            {
                                foreach (BoundaryLoop lp in fce.Loops)
                                {
                                    foreach (Edge edg in lp.Edges)
                                    {
                                        BasePoint tmp = CastHelper.ConvertToBasePoint(edg.Vertex1.Point);
                                        if (tmp.X > xMax) xMax = tmp.X;
                                        if (tmp.X < xMin) xMin = tmp.X;
                                        if (tmp.Y > yMax) yMax = tmp.Y;
                                        if (tmp.Y < yMin) yMin = tmp.Y;
                                        if (tmp.Z > zMax) zMax = tmp.Z;
                                        if (tmp.Z < zMin) zMin = tmp.Z;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            // xMin = Math.Round(xMin, 0);
            // yMin = Math.Round(yMin, 0);
            // zMin = Math.Round(zMin, 0);
            // xMax = Math.Round(xMax, 0);
            // yMax = Math.Round(yMax, 0);
            // zMax = Math.Round(zMax, 0);
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

            foreach (Solid3d solid in _solids)
            {
                Brep brep = new Brep(solid);
                using (brep)
                {
                    foreach (Complex cmp in brep.Complexes)
                    {
                        foreach (Shell shl in cmp.Shells)
                        {
                            foreach (Face fce in shl.Faces)
                            {
                                foreach (BoundaryLoop lp in fce.Loops)
                                {
                                    foreach (Edge edg in lp.Edges)
                                    {
                                        // filling 3 collection of edges, each collection responses for X, Y or Z direction
                                        if (edg.IsAlongX())
                                            xEdges.Add(edg);
                                        else if (edg.IsAlongY())
                                            yEdges.Add(edg);
                                        else if (edg.IsAlongZ())
                                            zEdges.Add(edg);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            xEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));
            yEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));
            zEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));

            var xLength = xEdges[0].Length();
            var yLength = yEdges[0].Length();
            var zLength = zEdges[0].Length();


            // Todo :  Remove this stuff

            ////double xLength = MathOperations.FindGcd(xEdges);
            ////double yLength = MathOperations.FindGcd(yEdges);
            ////double zLength = MathOperations.FindGcd(zEdges);

            ////var xLength = 100;
            ////var yLength = 100;
            ////var zLength = 100;

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