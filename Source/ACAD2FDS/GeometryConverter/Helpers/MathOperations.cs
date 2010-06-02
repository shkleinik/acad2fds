using System.Diagnostics;

namespace GeometryConverter.Helpers
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using BrFace = Autodesk.AutoCAD.BoundaryRepresentation.Face;

    static class MathOperations
    {
        private const double OrthDeviation = 0.001;

        /// <summary>
        /// Provides MCD of edge collections
        /// </summary>
        /// <param name="edges">Edge collection</param>
        /// <returns>GCD</returns>
        /// 
        public static double FindGcd(List<Edge> edges)
        {
            //double mcd = 100;
            int count = 1;
            int gcd = count;
            bool ok = true;
            double minLength = edges[FindMinIndex(edges)].Length();

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


        public static double FindGcd(List<double> points)
        {
            //todo: change or deprecate due to strict method dependence on measure units
            int count = 1;
            int gcd = count;
            bool ok = true;
            double minPoint = points[FindMinIndex(points)];

            var halfEdgeLength = minPoint / 2;

            while (count <= halfEdgeLength)
            {
                foreach (double point in points)
                {
                    if (Convert.ToInt32(Math.Round(point, 0)) % count == 0)
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

        private static int FindMinIndex(IList<Edge> edges)
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

        private static int FindMinIndex(IList<double> points)
        {
            int result = 0;
            var minPoint = points[1];
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i] < minPoint)
                {
                    minPoint = points[i];
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

        #region isCorvex

        public static bool IsCorvex(this BrFace face)
        {
            if (HasHoles(face))
                return false;
            var flagPos = 0;
            var flagNeg = 0;
            var edges = new List<Edge>();
            foreach (BoundaryLoop lp in face.Loops)
            {
                if (!IsAtLeastTriangle(lp))
                    return false;

                foreach (Edge edge in lp.Edges)
                    edges.Add(edge);

                for (int i = 1; i < edges.Count; i++)
                {
                    if (VectorMultiplicationResultIsPositive(edges[i - 1], edges[i]))
                        flagPos++;
                    else
                        flagNeg++;
                    //if ((flag < i && flag != 0)) //TODO: make it MOOOARRR readable!
                    //    return false;
                }
                if (VectorMultiplicationResultIsPositive(edges[edges.Count - 1], edges[0]))
                    flagPos++;
                else
                    flagNeg++;
            }
            Debug.WriteLine(flagPos);
            Debug.WriteLine(flagNeg);
            //return (edges.Count == flagPos) || (flagPos == 0);
            return (flagPos == flagNeg) || (flagNeg == 0) || (flagPos == 0); //WRONG!
        }

        private static bool HasHoles(BrFace face)
        {
            var lpSum = 0;
            foreach (BoundaryLoop lp in face.Loops)
            {
                lpSum++;
            }
            return lpSum != 1;
        }

        private static bool IsAtLeastTriangle(BoundaryLoop loop)
        {
            var edgeSum = 0;
            foreach (Edge edg in loop.Edges)
            {
                edgeSum++;
            }
            return edgeSum > 2;
        }

        private static bool VectorMultiplicationResultIsPositive(Edge edge1, Edge edge2)
        {
            var a1 = edge1.Vertex1.Point.X - edge1.Vertex2.Point.X;
            var a2 = edge1.Vertex1.Point.Y - edge1.Vertex2.Point.Y;
            var a3 = edge1.Vertex1.Point.Z - edge1.Vertex2.Point.Z;
            Debug.WriteLine(string.Format("{0}, {1}, {2}", a1, a2, a3));

            var b1 = edge2.Vertex2.Point.X - edge2.Vertex1.Point.X;
            var b2 = edge2.Vertex2.Point.Y - edge2.Vertex1.Point.Y;
            var b3 = edge2.Vertex2.Point.Z - edge2.Vertex1.Point.Z;
            Debug.WriteLine(string.Format("{0}, {1}, {2}", b1, b2, b3));

            //var result = edge1.Length()*edge2.Length()*Math.Sin(GetAngleBetweenEdges(edge1, edge2));
            var result = (a2 * b3 - a3 * b2) + (a3 * b1 - a1 * b3) + (a1 * b2 - a2 * b1);

            Debug.WriteLine(string.Format("Vector Multiplication: {0}", result));
            return result > 0;
        }

        public static double GetAngleBetweenEdges(Edge edge1, Edge edge2)
        {
            var result = (edge1.Vertex2.Point.X * edge2.Vertex2.Point.X +
                edge1.Vertex2.Point.Y * edge2.Vertex2.Point.Y +
                edge1.Vertex2.Point.Z * edge2.Vertex2.Point.Z) /
                (edge1.Length() * edge2.Length());
            Debug.WriteLine(string.Format("Angle: {0}", result));
            return result;
        }

        #endregion

        public static double FindMin(double X, double Y, double Z)
        {
            double min;
            if (X <= Y)
                if (X <= Z)
                    min = X;
                else
                    min = Z;
            else
                if (Y <= Z)
                    min = Y;
                else
                    min = Z;
            
            return min;
        }

        public static double FindMax(double X, double Y, double Z)
        {
            double max;
            if (X >= Y)
                if (X >= Z)
                    max = X;
                else
                    max = Z;
            else
                if (Y >= Z)
                    max = Y;
                else
                    max = Z;

            return max;
        }
    }
}