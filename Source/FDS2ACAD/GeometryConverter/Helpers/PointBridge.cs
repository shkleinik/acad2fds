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
    }
}