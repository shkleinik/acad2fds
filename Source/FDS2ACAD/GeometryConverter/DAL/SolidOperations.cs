using System.Collections.Generic;
using Autodesk.AutoCAD.BoundaryRepresentation;
using Autodesk.AutoCAD.DatabaseServices;
using GeometryConverter.BLL.Bases;
using GeometryConverter.BLL.Collections;
using BrFace = Autodesk.AutoCAD.BoundaryRepresentation.Face;
using Element = GeometryConverter.BLL.Bases.Element;

namespace GeometryConverter.DAL
{
    static class SolidOperations
    {
        public static Brep Brep;

        /// <summary>
        /// Returns minimal and maximal Bases point
        /// </summary>
        /// <param name="solid">ACAD solid</param>
        /// <returns>Array of 2 elements where [0] is Max and [1] is Min</returns>
        public static BasePoint[] GetMaxMinPoint(Entity solid)
        {
            BasePoint[] result = new BasePoint[2];
            result[0] = new BasePoint(double.MaxValue, double.MaxValue, double.MaxValue);
            result[1] = new BasePoint(double.MinValue, double.MinValue, double.MinValue);

            using (new Brep(solid))
            {
                foreach (Complex cmp in Brep.Complexes)
                {
                    foreach (Shell shl in cmp.Shells)
                    {
                        foreach (BrFace fce in shl.Faces)
                        {
                            foreach (BoundaryLoop lp in fce.Loops)
                            {
                                foreach (Edge edg in lp.Edges)
                                {
                                    BasePoint tmp = BLL.Helpers.PointBridge.ConvertToBasePoint(edg.Vertex1.Point);
                                    ///todo: round the real value
                                    if (tmp.IsGreater(result[1]))
                                        result[1] = tmp;
                                    if (tmp.IsLower(result[0]))
                                        result[0] = tmp;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Initialize ElementBase through the information about mcd of edges
        /// </summary>
        /// <param name="solid">Acad solid</param>
        /// <returns>Element base</returns>
        public static ElementBase ElementInitializer(Entity solid)
        {
            List<Edge> xEdges = new List<Edge>();
            List<Edge> yEdges = new List<Edge>();
            List<Edge> zEdges = new List<Edge>();

            using (new Brep(solid))
            {
                foreach (Complex cmp in Brep.Complexes)
                {
                    foreach (Shell shl in cmp.Shells)
                    {
                        foreach (BrFace fce in shl.Faces)
                        {
                            foreach (BoundaryLoop lp in fce.Loops)
                            {
                                foreach (Edge edg in lp.Edges)
                                {
                                    /// filling 3 collection of edges, each collection responses for X, Y or Z direction
                                    /// todo: be sure that ALL edges are orthogonal
                                    if ((edg.Vertex1.Point.Y == edg.Vertex2.Point.Y) && (edg.Vertex1.Point.Z == edg.Vertex2.Point.Z))
                                        xEdges.Add(edg);
                                    else if ((edg.Vertex1.Point.X == edg.Vertex2.Point.X) && (edg.Vertex1.Point.Z == edg.Vertex2.Point.Z))
                                        yEdges.Add(edg);
                                    else if ((edg.Vertex1.Point.X == edg.Vertex2.Point.X) && (edg.Vertex1.Point.Y == edg.Vertex2.Point.Y))
                                        zEdges.Add(edg);
                                }
                            }
                        }
                    }
                }
            }

            double xLength = BLL.Helpers.MathOperations.FindMcd(xEdges);
            double yLength = BLL.Helpers.MathOperations.FindMcd(yEdges);
            double zLength = BLL.Helpers.MathOperations.FindMcd(zEdges);
            ElementBase result = new ElementBase(xLength, yLength, zLength);

            return result;
        }

        /// <summary>
        /// Provides collection of all elements bounded by rectangle of maxMinPoint
        /// </summary>
        /// <param name="maxMinPoint">Array of MAX and MIN bound points</param>
        /// <param name="elementBase">Element base that provides size of element</param>
        /// <returns>Collection of all elements</returns>
        public static ElementCollection GetAllElements(BasePoint[] maxMinPoint, ElementBase elementBase)
        {
            ElementCollection result = new ElementCollection();
            int limitX = (int)((int)(maxMinPoint[0].X - maxMinPoint[1].X) / elementBase.XLength);
            int limitY = (int)((int)(maxMinPoint[0].Y - maxMinPoint[1].Y) / elementBase.YLength);
            int limitZ = (int)((int)(maxMinPoint[0].Z - maxMinPoint[1].Z) / elementBase.ZLength);
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
        public static ElementCollection GetValuableElements(ElementCollection input, Entity solid)
        {
            ElementCollection result = new ElementCollection();
            for (int i = 0; i < input.Elements.Count; i++)
            {
                ///todo: check whether point belongs to solid
            }
            return result;
        }

        /// <summary>
        /// Absolute MUST to perform before any computation and element analysis
        /// </summary>
        public static void SetNeighbourhoodRelations(ElementCollection input, Entity solid)
        {
            for (int i = 0; i < input.Elements.Count; i++)
            {
                BasePoint tmpCenter = input.Elements[i].Center;
                tmpCenter.X = input.Elements[i].Center.X + input.Elements[i].XLength;
                if (input.GetElementNoByCenter(tmpCenter) != null)
                    input.Elements[i].NeighbourRight = i;

                tmpCenter = input.Elements[i].Center;
                tmpCenter.X = input.Elements[i].Center.X - input.Elements[i].XLength;
                if (input.GetElementNoByCenter(tmpCenter) != null)
                    input.Elements[i].NeighbourLeft = i;

                tmpCenter = input.Elements[i].Center;
                tmpCenter.Y = input.Elements[i].Center.Y + input.Elements[i].YLength;
                if (input.GetElementNoByCenter(tmpCenter) != null)
                    input.Elements[i].NeighbourBack = i;

                tmpCenter = input.Elements[i].Center;
                tmpCenter.Y = input.Elements[i].Center.Y - input.Elements[i].YLength;
                if (input.GetElementNoByCenter(tmpCenter) != null)
                    input.Elements[i].NeighbourFront = i;

                tmpCenter = input.Elements[i].Center;
                tmpCenter.Z = input.Elements[i].Center.Z + input.Elements[i].ZLength;
                if (input.GetElementNoByCenter(tmpCenter) != null)
                    input.Elements[i].NeighbourTop = i;

                tmpCenter = input.Elements[i].Center;
                tmpCenter.Z = input.Elements[i].Center.Z - input.Elements[i].ZLength;
                if (input.GetElementNoByCenter(tmpCenter) != null)
                    input.Elements[i].NeighbourBottom = i;
            }
        }



        public static ElementCollection GetBlocksFromElements(ElementCollection input)
        {
            List<ElementCollection> listOfCollections = new List<ElementCollection>();
            while (input.Elements.Count > 0)
            {
                ElementCollection collection = new ElementCollection();
                int current = 0;

                if (input.Elements[current].NeighbourRight != null)
                {

                }
            }
            return result;
        }
    }
}
