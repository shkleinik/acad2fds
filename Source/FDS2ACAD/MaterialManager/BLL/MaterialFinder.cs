using System.Collections.Generic;
using GeometryConverter.DAL.Bases;
using GeometryConverter.DAL.Collections;

namespace MaterialManager.BLL
{
    public class MaterialFinder
    {
        public static List<string> ReturnMaterials(ElementCollection collection)
        {
            List<string> materials = new List<string>();
            foreach (Element element in collection.Elements)
            {
                if (!materials.Contains(element.Material))
                    materials.Add(element.Material);
            }
            return materials;
        }
    }
}
