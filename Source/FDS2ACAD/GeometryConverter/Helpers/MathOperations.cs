namespace GeometryConverter.Helpers
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;

    static class MathOperations
    {
        private const double OrthDeviation = 0.001;

        /// <summary>
        /// Provides MCD of edge collections
        /// </summary>
        /// <param name="edges">Edge collection</param>
        /// <returns>GCD</returns>
        public static double FindGcd(List<Edge> edges)
        {
            //double mcd = 100;
            int count = 1;
            int gcd = count;
            bool ok = true;
            double minLength = edges[FindMinEdgeIndex(edges)].Length();

            var halfEdgeLength = minLength / 2;

            while (count <= halfEdgeLength)
            {
                foreach (Edge edge in edges)
                {
                    if (Convert.ToInt32(Math.Round(edge.Length(), 0)) % count == 0)
                        continue;

                    ok = false;
                    break;
                }
                if (ok)
                    gcd = count;

                count++;
                ok = true;
            }

            return gcd;
        }


        public static double GetElementLengthByPoints(List<double> points)
        {
            var maxDigitsAfterPoint = 0;
            const int factor = 0; //todo: introduce factor
            foreach (var point in points)
            {
                var digitsAfterPoint = (Math.Round(point, factor) % 1).ToString().Length - 1;
                if (digitsAfterPoint > maxDigitsAfterPoint)
                    maxDigitsAfterPoint = digitsAfterPoint;
            }

            var result = Math.Pow(10, -maxDigitsAfterPoint);
            return result;
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

        public static double Length(this Edge edge)
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