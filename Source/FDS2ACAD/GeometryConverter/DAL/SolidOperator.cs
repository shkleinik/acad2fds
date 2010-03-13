namespace GeometryConverter.DAL
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using Autodesk.AutoCAD.DatabaseServices;
    using Bases;
    using Collections;
    using Helpers;
    using BrFace = Autodesk.AutoCAD.BoundaryRepresentation.Face;
    using Element = Bases.Element;

    public class SolidOperator
    {
        #region Fields

        private readonly List<Solid3d> _solids;
        private readonly ElementBase _elementBase;
        private readonly ElementCollection _fullCollection;
        private readonly int _factor = 1;

        #endregion

        #region Properties

        public ElementCollection UsefulElementCollectionProvider
        {
            get
            {
                return GetValuableElements(Factorize(_fullCollection));//.SetNeighbourhoodRelations();
            }
        }

        public BasePoint[] MaxMinPoint { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="solids">Array of selected solids</param>
        public SolidOperator(List<Solid3d> solids)
        {
            _solids = new List<Solid3d>();
            _solids = solids;
            MaxMinPoint = GetMaxMinPoint(_solids);
            _elementBase = InitializeElementBase();
            _fullCollection = GetAllElements(MaxMinPoint, _elementBase);
        } 
        
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="solids">Array of selected solids</param>
        /// <param name="factor">Unit factor</param>
        public SolidOperator(List<Solid3d> solids, int factor)
        {
            _factor = factor;
            _solids = new List<Solid3d>();
            _solids = solids;
            MaxMinPoint = GetMaxMinPoint(_solids, _factor);
            _elementBase = InitializeElementBase();
            _fullCollection = GetAllElements(MaxMinPoint, _elementBase);
        } 

        #endregion

        #region Internal implementation

        /// <summary>
        /// Returns minimal and maximal Bases point
        /// </summary>
        /// <returns>Array of 2 elements where [0] is Max and [1] is Min</returns>
        private static BasePoint[] GetMaxMinPoint(IEnumerable<Solid3d> solids, int factor)
        {
            BasePoint[] result = new BasePoint[2];
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
                            foreach (BrFace fce in shl.Faces)
                            {
                                foreach (BoundaryLoop lp in fce.Loops)
                                {
                                    foreach (Edge edg in lp.Edges)
                                    {
                                        BasePoint tmp = PointBridge.ConvertToBasePoint(edg.Vertex1.Point);
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
            xMin = Math.Round(xMin, 0) * factor;
            yMin = Math.Round(yMin, 0) * factor;
            zMin = Math.Round(zMin, 0) * factor;
            xMax = Math.Round(xMax, 0) * factor;
            yMax = Math.Round(yMax, 0) * factor;
            zMax = Math.Round(zMax, 0) * factor;
            result[0] = new BasePoint(xMin, yMin, zMin);
            result[1] = new BasePoint(xMax, yMax, zMax);
            return result;
        }

        /// <summary>
        /// Returns minimal and maximal Bases point
        /// </summary>
        /// <returns>Array of 2 elements where [0] is Max and [1] is Min</returns>
        public static BasePoint[] GetMaxMinPoint(List<Solid3d> solids)
        {
            BasePoint[] result = new BasePoint[2];
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
                            foreach (BrFace fce in shl.Faces)
                            {
                                foreach (BoundaryLoop lp in fce.Loops)
                                {
                                    foreach (Edge edg in lp.Edges)
                                    {
                                        BasePoint tmp = PointBridge.ConvertToBasePoint(edg.Vertex1.Point);
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
            xMin = Math.Round(xMin, 0);
            yMin = Math.Round(yMin, 0);
            zMin = Math.Round(zMin, 0);
            xMax = Math.Round(xMax, 0);
            yMax = Math.Round(yMax, 0);
            zMax = Math.Round(zMax, 0);
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
            List<Edge> xEdges = new List<Edge>();
            List<Edge> yEdges = new List<Edge>();
            List<Edge> zEdges = new List<Edge>();

            foreach (Solid3d solid in _solids)
            {
                Brep brep = new Brep(solid);
                using (brep)
                {
                    foreach (Complex cmp in brep.Complexes)
                    {
                        foreach (Shell shl in cmp.Shells)
                        {
                            foreach (BrFace fce in shl.Faces)
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
            double xLength = _factor * MathOperations.FindGcd(xEdges);
            double yLength = _factor * MathOperations.FindGcd(yEdges);
            double zLength = _factor * MathOperations.FindGcd(zEdges);
            ElementBase result = new ElementBase(xLength, yLength, zLength);

            return result;
        }

        /// <summary>
        /// Provides collection of all elements bounded by rectangle of MaxMinPoint
        /// </summary>
        /// <param name="maxMinPoint">Array of MAX and MIN bound points</param>
        /// <param name="elementBase">Element base that provides size of element</param>
        /// <returns>Collection of all elements</returns>
        private static ElementCollection GetAllElements(BasePoint[] maxMinPoint, ElementBase elementBase)
        {
            ElementCollection result = new ElementCollection();
            int limitX = Math.Abs((int)((int)(maxMinPoint[0].X - maxMinPoint[1].X) / elementBase.XLength));
            int limitY = Math.Abs((int)((int)(maxMinPoint[0].Y - maxMinPoint[1].Y) / elementBase.YLength));
            int limitZ = Math.Abs((int)((int)(maxMinPoint[0].Z - maxMinPoint[1].Z) / elementBase.ZLength));
            for (int z = 0; z < limitZ; z++)
                for (int y = 0; y < limitY; y++)
                    for (int x = 0; x < limitX; x++)
                    {
                        BasePoint center = new BasePoint((x + 0.5) * elementBase.XLength, (y + 0.5) * elementBase.YLength, (z + 0.5) * elementBase.ZLength);
                        Element element = new Element(center, elementBase);
                        result.Elements.Add(element);
                    }
            return result;
        }

        /// <summary>
        /// Set factor field for each element of Collection
        /// </summary>
        /// <param name="collection">Unfactorized collection</param>
        /// <returns>Factorized collection</returns>
        private ElementCollection Factorize(ElementCollection collection)
        {
            foreach (var element in collection.Elements)
            {
                element.Factor = _factor;
            }
            return collection;
        }

        /// <summary>
        /// Provides collection of valuable elements that belongs to the Solid from all elements
        /// </summary>
        /// <param name="input">All elements collection</param>
        /// <param name="input">Input</param>
        /// <returns>Collection of valuable elements</returns>
        private ElementCollection GetValuableElements(ElementCollection input)
        {
            ElementCollection result = new ElementCollection();
            for (int i = 0; i < input.Elements.Count; i++)
            {
                foreach (Solid3d solid in _solids)
                {
                    string material = solid.Material;
                    var brep = new Brep(solid);
                    PointContainment containment;
                    brep.GetPointContainment(input.Elements[i].Center.Unfactorize(_factor).ConverToAcadPoint(), out containment);
                    if (containment == PointContainment.Inside)
                    {
                        input.Elements[i].Material = material;
                        result.Elements.Add(input.Elements[i]);
                    }
                }
            }
            return result;
        } 
        #endregion
    }
}
