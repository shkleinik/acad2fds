namespace GeometryConverter.DAL
{
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using Bases;
    using BrFace = Autodesk.AutoCAD.BoundaryRepresentation.Face;
    using System.Collections.Generic;
    using Helpers;
    using Autodesk.AutoCAD.DatabaseServices;
    using System;

    public class BaseSolidToElementConverter
    {
        #region Constatnts

        protected const int DefaultFactor = 1;

        #endregion

        #region Fields

        protected double _factor = 1;
        protected List<Solid3d> _solids;
        protected ElementBase _elementBase;

        #endregion

        #region Properties

        public BasePoint[] MaxMinPoint { get; set; }

        #endregion

        #region Constructors

        public BaseSolidToElementConverter(List<Solid3d> solids, double factor)
        {
            _factor = factor;
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
        private BasePoint[] GetMaxMinPoint(IEnumerable<Solid3d> solids)
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
            xMin = Math.Round(xMin, 0) * _factor;
            yMin = Math.Round(yMin, 0) * _factor;
            zMin = Math.Round(zMin, 0) * _factor;
            xMax = Math.Round(xMax, 0) * _factor;
            yMax = Math.Round(yMax, 0) * _factor;
            zMax = Math.Round(zMax, 0) * _factor;
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
            var result = new ElementBase(xLength, yLength, zLength);

            return result;
        }

        #endregion
    }
}