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

        /// <summary>
        /// Converts collection to single element
        /// </summary>
        /// <returns>Element</returns>
        public static Element ToElement(this List<Element> Elements)
        {
            var centerMin = new BasePoint(double.MaxValue, double.MaxValue, double.MaxValue);
            var centerMax = new BasePoint(double.MinValue, double.MinValue, double.MinValue);

            foreach (var element in Elements)
            {
                if (element.Center.IsGreater(centerMax))
                    centerMax = element.Center;
                if (element.Center.IsLower(centerMin))
                    centerMin = element.Center;
            }

            double centerX = (centerMax.X + centerMin.X) / 2;
            double centerY = (centerMax.Y + centerMin.Y) / 2;
            double centerZ = (centerMax.Z + centerMin.Z) / 2;
            var center = new BasePoint(centerX, centerY, centerZ);
            double lengthX = centerMax.X - centerMin.X + Elements[0].XLength;
            double lengthY = centerMax.Y - centerMin.Y + Elements[0].YLength;
            double lengthZ = centerMax.Z - centerMin.Z + Elements[0].ZLength;
            // Todo : decide if it is necessary to set in this constructor index
            var result = new Element(center, lengthX, lengthY, lengthZ);
            return result;
        }
    }
}