using System;
using System.Collections.Generic;
using Autodesk.AutoCAD.BoundaryRepresentation;

namespace GeometryConverter.DAL.Helpers
{
    static class MathOperations
    {
        private const double OrthDeviation = 0.001;

        /// <summary>
        /// Provides MCD of edge collections
        /// </summary>
        /// <param name="edges">Edge collection</param>
        /// <returns>GCD</returns>
        /// todo: check this sh*t
        public static double FindGcd(List<Edge> edges)
        {
            //double mcd = 100;
            double mcd = 1;
            bool ok = true;
            double minLength = edges[FindMinEdgeIndex(edges)].Length();

            while (mcd <= minLength)
            {
                foreach (Edge edge in edges)
                {
                    if (Convert.ToInt32(edge.Length()) % mcd == 0)
                    {
                        ok = true;
                        continue;
                    }
                    else
                    {
                        ok = false;
                        break;
                    }
                }
                if (!ok)
                    break;
                else
                    mcd++;
            }

            return mcd;
        }

        private static int FindMinEdgeIndex(IList<Edge> edges)
        {
            int result = 0;
            Edge minEdge = edges[1];
            for (int i = 0; i < edges.Count; i++)
            {
                if (edges[i].Length() < minEdge.Length())
                {
                    minEdge = edges[i];
                    result = i;
                }
            }
            return result;
        }

        private static double Length(this Edge edge)
        {
            return Math.Sqrt(
                Math.Pow(edge.Vertex1.Point.X - edge.Vertex2.Point.X, 2) +
                Math.Pow(edge.Vertex1.Point.Y - edge.Vertex2.Point.Y, 2) +
                Math.Pow(edge.Vertex1.Point.Z - edge.Vertex2.Point.Z, 2));
        }

        public static bool IsAlongX(this Edge edg)
        {
            bool isOrthY = Math.Abs(edg.Vertex1.Point.Y - edg.Vertex2.Point.Y) < OrthDeviation;
            bool isOrthZ = Math.Abs(edg.Vertex1.Point.Z - edg.Vertex2.Point.Z) < OrthDeviation;
            return (isOrthY && isOrthZ);
        }

        public static bool IsAlongY(this Edge edg)
        {
            bool isOrthX = Math.Abs(edg.Vertex1.Point.X - edg.Vertex2.Point.X) < OrthDeviation;
            bool isOrthZ = Math.Abs(edg.Vertex1.Point.Z - edg.Vertex2.Point.Z) < OrthDeviation;
            return (isOrthX && isOrthZ);
        }

        public static bool IsAlongZ(this Edge edg)
        {
            bool isOrthY = Math.Abs(edg.Vertex1.Point.Y - edg.Vertex2.Point.Y) < OrthDeviation;
            bool isOrthX = Math.Abs(edg.Vertex1.Point.X - edg.Vertex2.Point.X) < OrthDeviation;
            return (isOrthY && isOrthX);
        }


    }
}