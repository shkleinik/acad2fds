namespace GeometryConverter.Bases
{
    using System;
    using Autodesk.AutoCAD.Geometry;

    public class BasePoint : ICloneable
    {
        #region Properties

        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        #endregion

        #region Constructors

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

        #endregion

        #region Internal implementation

        /// <summary>
        /// Provides Acad point from this Base point
        /// </summary>
        /// <returns>Acad point</returns>
        public Point3d ConverToAcadPoint()
        {
            return new Point3d(X, Y, Z);
        }

        /// <summary>
        /// Compares probe to basis
        /// </summary>
        /// <param name="probe">Probe to be compared</param>
        /// <returns>True if probe LOWER than basis by all coordinates</returns>
        public bool IsLower(BasePoint probe)
        {
            return (X < probe.X && Y < probe.Y && Z < probe.Z);
        }

        /// <summary>
        /// Compares probe to basis
        /// </summary>
        /// <param name="probe">Probe to be compared</param>
        /// <returns>True if probe GREATER than basis by all coordinates</returns>
        public bool IsGreater(BasePoint probe)
        {
            return (X > probe.X && Y > probe.Y && Z > probe.Z);
        }

        /// <summary>
        /// Round values of X, Y, Z
        /// </summary>
        public BasePoint Round()
        {
            X = Math.Round(X, 0);
            Y = Math.Round(Y, 0);
            Z = Math.Round(Z, 0);
            return this;
        }

        #endregion

        public Direction? GetPosition(BasePoint another)
        {
            Direction? result;
            const double deltaX = 0.1;
            const double deltaY = 0.1;
            const double deltaZ = 0.1;
            if ((Z < another.Z) && (Math.Abs(Y - another.Y) < deltaY) && (Math.Abs(X - another.X) < deltaX))
                result = Direction.Top;

            else if ((Z > another.Z) && (Math.Abs(Y - another.Y) < deltaY) && (Math.Abs(X - another.X) < deltaX))
                result = Direction.Bottom;

            else if ((Y > another.Y) && (Math.Abs(Z - another.Z) < deltaZ) && (Math.Abs(X - another.X) < deltaX))
                result = Direction.Front;

            else if ((Y < another.Y) && (Math.Abs(Z - another.Z) < deltaZ) && (Math.Abs(X - another.X) < deltaX))
                result = Direction.Back;

            else if ((X < another.X) && (Math.Abs(Y - another.Y) < deltaY) && (Math.Abs(Z - another.Z) < deltaZ))
                result = Direction.Right;

            else if ((X > another.X) && (Math.Abs(Y - another.Y) < deltaY) && (Math.Abs(Z - another.Z) < deltaZ))
                result = Direction.Left;

            else result = null;
            // Todo : cross fingers and hope that all of the conditions will be true.
            return result;
        }

        #region ICloneable Members

        public object Clone()
        {
            return new BasePoint(X, Y, Z);
        }

        #endregion
    }
}