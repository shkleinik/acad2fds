namespace GeometryConverter.DAL.Bases
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

        public BasePoint Factorize(int factor)
        {
            X *= factor;
            Y *= factor;
            Z *= factor;
            return this;
        }

        public BasePoint Unfactorize(int factor)
        {
            X /= factor;
            Y /= factor;
            Z /= factor;
            return this;
        }

        #endregion

        public Direction GetPosition(BasePoint another)
        {
            var result = new Direction();

            if (Z < another.Z)
                result =  Direction.Top;

            if (Z > another.Z)
                result = Direction.Bottom;

            if (Y > another.Y)
                result = Direction.Front;

            if (Y < another.Y)
                result = Direction.Back;

            if (X < another.X)
                result = Direction.Right;

            if (X > another.X)
                result = Direction.Left;

            if (result == 0)
                throw new ArgumentException("Ooops!");

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