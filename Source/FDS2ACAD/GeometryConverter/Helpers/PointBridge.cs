using System;

namespace GeometryConverter.Helpers
{
    using Autodesk.AutoCAD.Geometry;
    using Bases;

    static class PointBridge
    {
        /// <summary>
        /// Provides Acad point from Base point
        /// </summary>
        /// <param name="basePoint">Base point</param>
        /// <returns>Acad point</returns>
        public static Point3d ConverToAcadPoint(BasePoint basePoint)
        {
            return new Point3d(basePoint.X, basePoint.Y, basePoint.Z);
        }

        /// <summary>
        /// Provides Base point from Acad point
        /// </summary>
        /// <param name="point3D">Base point</param>
        /// <returns>Acad point</returns>
        public static BasePoint ConvertToBasePoint(Point3d point3D)
        {
            return new BasePoint(point3D.X, point3D.Y, point3D.Z);
        }

        public static Element ToElement(this BasePoint[] maxMinPoint)
        {
            if(maxMinPoint.Length > 2)
                throw new ArgumentException("maxMinPoint");

            var center = new BasePoint(maxMinPoint[0].X - (maxMinPoint[0].X - maxMinPoint[1].X) / 2,
                                   maxMinPoint[0].Y - (maxMinPoint[0].Y - maxMinPoint[1].Y) / 2,
                                   maxMinPoint[0].Z - (maxMinPoint[0].Z - maxMinPoint[1].Z) / 2);

            var xLength = (maxMinPoint[0].X - maxMinPoint[1].X);
            var yLength = (maxMinPoint[0].Y - maxMinPoint[1].Y);
            var zLength = (maxMinPoint[0].Z - maxMinPoint[1].Z);

            return new Element(center, xLength, yLength, zLength);
        }
    }
}