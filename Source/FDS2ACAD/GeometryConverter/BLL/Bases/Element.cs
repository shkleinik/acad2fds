using System;
using System.Collections.Generic;
using System.Text;

namespace GeometryConverter.BLL.Bases
{
    class Element
    {
        protected BasePoint _center;
        protected double _xLength;
        protected double _yLength;
        protected double _zLength;

        #region Constructors

        public Element(BasePoint center, double xLength, double yLength, double zLength)
        {
            _center = center;
            _xLength = xLength;
            _yLength = yLength;
            _zLength = zLength;
        }

        #endregion
    }
}