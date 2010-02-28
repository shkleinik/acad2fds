using Autodesk.AutoCAD.Interop.Common;
using GeometryConverter.DAL.Bases;
using GeometryConverter.DAL.Collections;

namespace GeometryConverter.DAL.Helpers
{
    static class CollectionDrawer
    {
        static public void Draw(ElementCollection input)
        {
            foreach (Element element in input.Elements)
            {
                Draw(element);
            }
        }

        static public void Draw(Element element)
        {
            //Acad3DSolid solid = new Acad3DSolid();
        }
    }
}
