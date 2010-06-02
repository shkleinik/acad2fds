namespace GeometryConverter
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using Autodesk.AutoCAD.DatabaseServices;
    using Bases;
    using Helpers;
    using Element = Bases.Element;
    using BrFace = Autodesk.AutoCAD.BoundaryRepresentation.Face;

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

        public bool IsSuccessfullConversion { get; set; }

        public double SolidVolume { get; set; }

        #endregion

        #region Constructors

        public SolidToElementConverter(Entity solid)
            : this(new List<Entity> { solid })
        {

        }

        public SolidToElementConverter(List<Entity> solids)
        {
            IsSuccessfullConversion = true;
            _solids = solids;
            MaxMinPoint = GetMaxMinPoint(_solids);
            _elementBase = InitializeElementBase();
            //_elementBase = InitializeElementBasePro();
        }

        #endregion

        #region Internal implementation

        /// <summary>
        /// Returns minimal and maximal Bases point
        /// </summary>
        /// <returns>Array of 2 elements where [0] is Max and [1] is Min</returns>
        public static BasePoint[] GetMaxMinPoint(IEnumerable<Entity> solids)
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
            var isCorvexSolid = true;
            foreach (var solid in _solids)
            {
                var brep = new Brep(solid);
                using (brep)
                {
                    foreach (BrFace fce in brep.Faces)
                    {
                        if (!fce.IsCorvex())
                        {
                            isCorvexSolid = false;
                            break;
                        }
                    }
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
            const int deltaTotalEdges = 1;

            //return InitializeElementBasePro(0.1, 0);

            if ((totalEdges - (xEdges.Count + yEdges.Count + zEdges.Count) < deltaTotalEdges) && isCorvexSolid)
            {
                    xEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));
                    yEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));
                    zEdges.Sort((e1, e2) => e1.Length().CompareTo(e2.Length()));

                    xLength = xEdges[0].Length();
                    yLength = yEdges[0].Length();
                    zLength = zEdges[0].Length();
            }
            else
            {
                return InitializeElementBaseByMinMaxPoint();
            }
            return new ElementBase(xLength, yLength, zLength);
        }

        private ElementBase InitializeElementBaseByMinMaxPoint()
        {
            const int nils = 3;
            int multiplier = (int)Math.Pow(10, nils);
            var X = (int)Math.Round((MaxMinPoint[1].X - MaxMinPoint[0].X) * multiplier, 0);
            var Y = (int)Math.Round((MaxMinPoint[1].Y - MaxMinPoint[0].Y) * multiplier, 0);
            var Z = (int)Math.Round((MaxMinPoint[1].Z - MaxMinPoint[0].Z) * multiplier, 0);

            var xLength = X.ToString().Length;
            var yLength = Y.ToString().Length;
            var zLength = Z.ToString().Length;

            var minLength = (int)MathOperations.FindMin(xLength, yLength, zLength);

            var size = minLength >= 6 ? 100 : 0.1;
            //int size = (int)Math.Pow(10, minLength - (nils + 1)); // 2 is a kind of magic...
            return new ElementBase(size);
        }

        //#region ElementBasePro

        //private ElementBase InitializeElementBasePro(double delta, int stage)
        //{
        //    var containmentPrev = double.MinValue;
        //    var difference = 1d;
        //    while (difference > delta)
        //    {
        //        var containment = GetContainmentPercentage(MaxMinPoint, stage);
        //        if (stage > 0)
        //            difference = containment - containmentPrev;
        //        containmentPrev = containment;
        //        stage++;
        //    }

        //    var dimension = GetDimensions(MaxMinPoint);
        //    //var coefficient = GetCoefficient(dimension);
        //    //var result = new ElementBase
        //    //    (
        //    //    Math.Round(dimension.X / (Math.Pow(2, stage) * coefficient.X), 0),
        //    //    Math.Round(dimension.Y / (Math.Pow(2, stage) * coefficient.Y), 0),
        //    //    Math.Round(dimension.Z / (Math.Pow(2, stage) * coefficient.Z), 0)
        //    //    );
        //    var result = new ElementBase //wrong
        //        (
        //        Math.Round(dimension.X / (Math.Pow(2, stage)), 0),
        //        Math.Round(dimension.Y / (Math.Pow(2, stage)), 0),
        //        Math.Round(dimension.Z / (Math.Pow(2, stage)), 0)
        //        );

        //    return result;
        //}

        //private double GetContainmentPercentage(BasePoint[] maxMinPoint, int stage)
        //{
        //    var collection = GetCollection(maxMinPoint, stage);
        //    var containees = 0;
        //    for (var i = 0; i < collection.Count; i++)
        //    {
        //        foreach (var solid in _solids)
        //        {
        //            var brep = new Brep(solid);
        //            PointContainment containment;
        //            brep.GetPointContainment(collection[i].Center.ConverToAcadPoint(), out containment);

        //            if (containment != PointContainment.Inside)
        //                continue;
        //            containees++;
        //        }
        //    }
        //    return (double)containees / collection.Count;
        //}

        //private List<Element> GetCollection(BasePoint[] maxMinPoint, int stage)
        //{
        //    var resultCollection = new List<Element>();
        //    var dimension = GetDimensions(maxMinPoint);
        //    var tmpElementBase = new ElementBase(
        //        Math.Round(dimension.X / (Math.Pow(2, stage)), 0),
        //        Math.Round(dimension.Y / (Math.Pow(2, stage)), 0),
        //        Math.Round(dimension.Z / (Math.Pow(2, stage)), 0)
        //        );


        //    var limitX = Math.Abs((int)(MaxMinPoint[0].X - MaxMinPoint[1].X) / tmpElementBase.XLength);
        //    var limitY = Math.Abs((int)(MaxMinPoint[0].Y - MaxMinPoint[1].Y) / tmpElementBase.YLength);
        //    var limitZ = Math.Abs((int)(MaxMinPoint[0].Z - MaxMinPoint[1].Z) / tmpElementBase.ZLength);

        //    var startX = MaxMinPoint[0].X;
        //    var startY = MaxMinPoint[0].Y;
        //    var startZ = MaxMinPoint[0].Z;

        //    for (var z = 0; z < limitZ; z++)
        //        for (var y = 0; y < limitY; y++)
        //            for (var x = 0; x < limitX; x++)
        //            {
        //                var center = new BasePoint(startX + (x + 0.5) * tmpElementBase.XLength,
        //                                           startY + (y + 0.5) * tmpElementBase.YLength,
        //                                           startZ + (z + 0.5) * tmpElementBase.ZLength);

        //                var element = new Element(center, tmpElementBase, resultCollection.Count);
        //                resultCollection.Add(element);
        //            }

        //    return resultCollection;

        //}

        //private static BasePoint GetDimensions(BasePoint[] maxMinPoint)
        //{
        //    var result = new BasePoint
        //                     (
        //                         maxMinPoint[1].X - maxMinPoint[0].X,
        //                         maxMinPoint[1].Y - maxMinPoint[0].Y,
        //                         maxMinPoint[1].Z - maxMinPoint[0].Z
        //                     );
        //    return result;
        //}

        //private static BasePoint GetCoefficient(BasePoint dimensions)
        //{
        //    const int decimals = 1;

        //    var minValue = MathOperations.FindMin(dimensions.X, dimensions.Y, dimensions.Z);
            
        //    var result = new BasePoint
        //                     (
        //                         Math.Round(dimensions.X / minValue, decimals),
        //                         Math.Round(dimensions.Y / minValue, decimals),
        //                         Math.Round(dimensions.Z / minValue, decimals)
        //                     );
        //    return result;

        //}

        //#endregion

        //private static double Length(Edge edge)
        //{
        //    return Math.Sqrt(
        //        edge.Length()
        //        Math.Pow(edge.Vertex2.Point.X - edge.Vertex1.Point.X, 2) +
        //        Math.Pow(edge.Vertex2.Point.Y - edge.Vertex1.Point.X, 2) +
        //        Math.Pow(edge.Vertex2.Point.Z - edge.Vertex1.Point.X, 2) +
        //        )
        //}

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
                        try
                        {
                            var element = new Element(center, _elementBase, _fullCollection.Count);
                            _fullCollection.Add(element);
                        }
                        catch (System.Exception)
                        {
                            IsSuccessfullConversion = false;
                            return _fullCollection;
                        }
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

        public static double Volume(List<Element> elements)
        {
            var result = 0d;
            foreach (var element in elements)
            {
                result += element.XLength * element.YLength * element.ZLength;
            }
            return result;
        }

        #endregion
    }
}