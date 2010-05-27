namespace MaterialManager.BLL
{
    using System.Collections.Generic;
    using GeometryConverter.Bases;

    public class MaterialFinder
    {
        public static List<string> ReturnMaterials(List<Element> elements)
        {
            var materials = new List<string>();
            foreach (var element in elements)
            {
                if (!materials.Contains(element.Material))
                    materials.Add(element.Material);
            }
            return materials;
        }
    }
}
