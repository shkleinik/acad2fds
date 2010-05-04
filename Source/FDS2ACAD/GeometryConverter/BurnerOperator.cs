namespace GeometryConverter
{
    using Autodesk.AutoCAD.DatabaseServices;
    using Bases;


    public class BurnerOperator
    {
        private readonly Solid3d _solid;

        public Element Element
        {
            get
            {
                var converter = new SolidToElementConverter(_solid);

                var maxMinPoint = converter.MaxMinPoint;

                return new Element(maxMinPoint);
            }
        }

        public BurnerOperator(Solid3d solid)
        {
            _solid = solid;
        }
    }
}