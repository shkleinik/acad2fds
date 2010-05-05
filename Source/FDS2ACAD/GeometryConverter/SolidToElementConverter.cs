namespace GeometryConverter
{
    using System.Collections.Generic;
    using Autodesk.AutoCAD.DatabaseServices;

    public class SolidToElementConverter : BaseSolidToElementConverter
    {
        #region Constructors

        public SolidToElementConverter(Solid3d solid)
            : base(new List<Solid3d> { solid }, DefaultFactor)
        {
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="solids">Array of selected solids</param>
        public SolidToElementConverter(List<Solid3d> solids)
            :base(solids, DefaultFactor)
        {
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="solids">Array of selected solids</param>
        /// <param name="factor">Unit factor</param>
        public SolidToElementConverter(List<Solid3d> solids, double factor)
            :base(solids, factor)
        {
        }

        #endregion
    }
}