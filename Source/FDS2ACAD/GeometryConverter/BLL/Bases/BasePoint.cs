using System;
using Autodesk.AutoCAD.Geometry;

namespace GeometryConverter.BLL.Bases
{
    class BasePoint
    {
        public double X;
        public double Y;
        public double Z;

        /// <summary>
        /// Provides new Base point from 3 coordinates
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="z">Z</param>
        public BasePoint(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Provides new Base point from Acad point
        /// </summary>
        /// <param name="point3D">Acad point</param>
        public BasePoint(Point3d point3D)
        {
            X = point3D.X;
            Y = point3D.Y;
            Z = point3D.Z;
        }

        /// <summary>
        /// Provides Acad point from this Base point
        /// </summary>
        /// <returns>Acad point</returns>
        public Point3d ConverToAcadPoint()
        {
            return new Point3d(X, Y, Z);
        }

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
        /// Compares probe to basis
        /// </summary>
        /// <param name="probe">Probe to be compared</param>
        /// <returns>True if probe LOWER than basis by all coordinates</returns>
        public bool IsLower(BasePoint probe)
        {
            return (probe.X < X && probe.Y < Y && probe.Z < Z);
        }

        /// <summary>
        /// Compares probe to basis
        /// </summary>
        /// <param name="probe">Probe to be compared</param>
        /// <returns>True if probe GREATER than basis by all coordinates</returns>
        public bool IsGreater(BasePoint probe)
        {
            return (probe.X > X && probe.Y > Y && probe.Z > Z);
        }
    }
}