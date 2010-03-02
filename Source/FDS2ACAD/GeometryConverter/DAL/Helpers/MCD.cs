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
        /// <returns>MCD</returns>
        public static double FindMcd(List<Edge> edges)
        {
            double result = 100;
            //todo: find max common divider
            return result;
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