using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.BoundaryRepresentation;
using Autodesk.AutoCAD.DatabaseServices;
using GeometryConverter.DAL.Bases;
using GeometryConverter.DAL.Collections;
using GeometryConverter.DAL.Helpers;
using BrFace = Autodesk.AutoCAD.BoundaryRepresentation.Face;
using Element = GeometryConverter.DAL.Bases.Element;

namespace GeometryConverter.DAL
{
    public static class SolidOperator
    {
        private const double OrthDeviation = 0.001;

        private static Entity _solid;
        public static BasePoint[] MaxMinPoint;
        public static ElementBase ElementBase;
        public static ElementCollection FullCollection;

        public static ElementCollection Analyze(Entity solid)
        {
            ElementCollection result;
            if (solid.GetType() == typeof(Solid3d))
            {
                _solid = solid;
                MaxMinPoint = GetMaxMinPoint(_solid);
                ElementBase = InitializeElementBase(_solid);
                FullCollection = GetAllElements(MaxMinPoint, ElementBase);
                result = GetValuableElements(FullCollection, _solid);
                result.SetNeighbourhoodRelations();
            }
            else
            {
                result = null;
            }
            return result;
        }

        /// <summary>
        /// Returns minimal and maximal Bases point
        /// </summary>
        /// <param name="solid">ACAD solid</param>
        /// <returns>Array of 2 elements where [0] is Max and [1] is Min</returns>
        private static BasePoint[] GetMaxMinPoint(Entity solid)
        {
            BasePoint[] result = new BasePoint[2];
            double xMax = double.MinValue;
            double yMax = double.MinValue;
            double zMax = double.MinValue;
            double xMin = double.MaxValue;
            double yMin = double.MaxValue;
            double zMin = double.MaxValue;
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
            //todo: round values
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
        /// Initialize ElementBase through the information about mcd of edges
        /// </summary>
        /// <param name="solid">Acad solid</param>
        /// <returns>Element base</returns>
        private static ElementBase InitializeElementBase(Entity solid)
        {
            List<Edge> xEdges = new List<Edge>();
            List<Edge> yEdges = new List<Edge>();
            List<Edge> zEdges = new List<Edge>();

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
                                    // todo: be sure that ALL edges are orthogonal
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

            double xLength = MathOperations.FindMcd(xEdges);
            double yLength = MathOperations.FindMcd(yEdges);
            double zLength = MathOperations.FindMcd(zEdges);
            ElementBase result = new ElementBase(xLength, yLength, zLength);

            return result;
        }

        private static bool IsAlongX(this Edge edg)
        {
            bool isOrthY = Math.Abs(edg.Vertex1.Point.Y - edg.Vertex2.Point.Y) < OrthDeviation;
            bool isOrthZ = Math.Abs(edg.Vertex1.Point.Z - edg.Vertex2.Point.Z) < OrthDeviation;
            return (isOrthY && isOrthZ);
        }

        private static bool IsAlongY(this Edge edg)
        {
            bool isOrthX = Math.Abs(edg.Vertex1.Point.X - edg.Vertex2.Point.X) < OrthDeviation;
            bool isOrthZ = Math.Abs(edg.Vertex1.Point.Z - edg.Vertex2.Point.Z) < OrthDeviation;
            return (isOrthX && isOrthZ);
        }

        private static bool IsAlongZ(this Edge edg)
        {
            bool isOrthY = Math.Abs(edg.Vertex1.Point.Y - edg.Vertex2.Point.Y) < OrthDeviation;
            bool isOrthX = Math.Abs(edg.Vertex1.Point.X - edg.Vertex2.Point.X) < OrthDeviation;
            return (isOrthY && isOrthX);
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
        /// Provides collection of valuable elements that belongs to the Solid from all elements
        /// </summary>
        /// <param name="input">All elements collection</param>
        /// <param name="solid">Solid</param>
        /// <returns>Collection of valuable elements</returns>
        private static ElementCollection GetValuableElements(ElementCollection input, Entity solid)
        {
            ElementCollection result = new ElementCollection();
            for (int i = 0; i < input.Elements.Count; i++)
            {
                var brep = new Brep(solid);
                PointContainment containment;
                brep.GetPointContainment(input.Elements[i].Center.ConverToAcadPoint(), out containment);
                if (containment == PointContainment.Inside)
                    result.Elements.Add(input.Elements[i]);
            }
            return result;
        }

        //private static ElementCollection GetBlocksFromElements(ElementCollection input)
        //{
        //    List<ElementCollection> listOfCollections = new List<ElementCollection>();
        //    while (input.Elements.Count > 0)
        //    {
        //        ElementCollection collection = new ElementCollection();
        //        int current = 0;

        //        if (input.Elements[current].NeighbourRight != null)
        //        {

        //        }
        //    }
        //    return result;
        //}
    }
}
