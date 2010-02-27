using System.Collections.Generic;
using GeometryConverter.BLL.Bases;

namespace GeometryConverter.BLL.Collections
{
    class ElementCollection
    {
        protected bool _related;

        public List<Element> Elements;

        #region Constructors

        public ElementCollection()
        {
            Elements = new List<Element>();
        }

        #endregion

        public int? GetElementNoByCenter(BasePoint center)
        {
            for (int i = 0; i < Elements.Count; i++)
                if (Elements[i].Center.Equals(center))
                    return i;
            return null;
        }

        public Element ToElement()
        {
            BasePoint centerMin = new BasePoint(double.MaxValue, double.MaxValue, double.MaxValue);
            BasePoint centerMax = new BasePoint(double.MinValue, double.MinValue, double.MinValue);
            foreach (Element element in Elements)
            {
                if (element.Center.IsGreater(centerMax))
                    centerMax = element.Center;
                if (element.Center.IsLower(centerMin))
                    centerMin = element.Center;
            }
            double centerX = (centerMax.X + centerMin.X) / 2;
            double centerY = (centerMax.Y + centerMin.Y) / 2;
            double centerZ = (centerMax.Z + centerMin.Z) / 2;
            BasePoint center = new BasePoint(centerX, centerY, centerZ);
            double lengthX = centerMax.X - centerMin.X + Elements[0].XLength;
            double lengthY = centerMax.Y - centerMin.Y + Elements[0].YLength;
            double lengthZ = centerMax.Z - centerMin.Z + Elements[0].ZLength;
            Element result = new Element(center, lengthX, lengthY, lengthZ);
            return result;
        }
    }
}