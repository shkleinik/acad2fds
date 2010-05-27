using System.Collections.Generic;
using Autodesk.AutoCAD.BoundaryRepresentation;

namespace GeometryConverter.DAL.Helpers
{
    static class MathOperations
    {
        /// <summary>
        /// Provides MCD of edge collections
        /// </summary>
        /// <param name="edges">Edge collection</param>
        /// <returns>MCD</returns>
        public static double FindMcd(List<Edge> edges)
        {
            double result = 0;
            //todo: find max common divider
            return result;
        }
    }
}