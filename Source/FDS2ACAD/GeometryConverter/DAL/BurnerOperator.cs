using System.Collections.Generic;
using Autodesk.AutoCAD.DatabaseServices;
using GeometryConverter.DAL.Bases;

namespace GeometryConverter.DAL
{
    public class BurnerOperator
    {
        private readonly Solid3d _solid;

        public Element Element
        {
            get
            {
                return new Element(SolidOperator.GetMaxMinPoint(new List<Solid3d> {_solid}));
            }
        }

        public BurnerOperator(Solid3d solid)
        {
            _solid = solid;
        }
    }
}
