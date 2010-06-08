namespace GeometryConverter.Helpers
{
    using System;
    using Autodesk.AutoCAD.Geometry;
    using Bases;
    using System.Collections.Generic;

    static class CastHelper
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

        /// <summary>
        /// Converts array of two base points to element.
        /// </summary>
        /// <param name="maxMinPoint">Array of front-bottom left and top-righ-back points of the element.</param>
        /// <returns>Elements limited with passed front-bottom left and top-righ-back points.</returns>
        public static Element ToElement(this BasePoint[] maxMinPoint)
        {
            if (maxMinPoint.Length > 2)
                throw new ArgumentException("maxMinPoint");

            var center = new BasePoint(maxMinPoint[0].X - (maxMinPoint[0].X - maxMinPoint[1].X) / 2,
                                   maxMinPoint[0].Y - (maxMinPoint[0].Y - maxMinPoint[1].Y) / 2,
                                   maxMinPoint[0].Z - (maxMinPoint[0].Z - maxMinPoint[1].Z) / 2);

            var xLength = (maxMinPoint[0].X - maxMinPoint[1].X);
            var yLength = (maxMinPoint[0].Y - maxMinPoint[1].Y);
            var zLength = (maxMinPoint[0].Z - maxMinPoint[1].Z);

            return new Element(center, xLength, yLength, zLength);
        }

        /// <summary>
        /// Converts collection to single element
        /// </summary>
        /// <returns>Element</returns>
        public static Element ToElement(this List<Element> elements)
        {
            var minX = double.MaxValue;
            var minY = double.MaxValue;
            var minZ = double.MaxValue;

            var maxX = double.MinValue;
            var maxY = double.MinValue;
            var maxZ = double.MinValue;

            foreach (var element in elements)
            {
                if (element.X1 < minX)
                    minX = element.X1;
                if (element.Y1 < minY)
                    minY = element.Y1;
                if (element.Z1 < minZ)
                    minZ = element.Z1;

                if (element.X2 > maxX)
                    maxX = element.X2;
                if (element.Y2 > maxY)
                    maxY = element.Y2;
                if (element.Z2 > maxZ)
                    maxZ = element.Z2;
            }

            var centerX = (minX + maxX) / 2;
            var centerY = (minY + maxY) / 2;
            var centerZ = (minZ + maxZ) / 2;
            var center = new BasePoint(centerX, centerY, centerZ);

            // Todo : decide if it is necessary to set in this constructor index
            return new Element(center, maxX - minX, maxY - minY, maxZ - minZ, elements[0].Material);
        }
    }
}