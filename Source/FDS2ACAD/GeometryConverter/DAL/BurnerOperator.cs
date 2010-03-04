using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;
using GeometryConverter.DAL.Bases;

namespace GeometryConverter.DAL
{
    class BurnerOperator
    {
        private readonly Solid3d _solid;

        public BasePoint[] element
        {
            get
            {
                return SolidOperator.GetMaxMinPoint(new List<Solid3d> {_solid});
            }
        }

        public BurnerOperator(Solid3d solid)
        {
            _solid = solid;
        }
    }
}
