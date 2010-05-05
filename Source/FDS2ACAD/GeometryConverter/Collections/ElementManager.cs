namespace GeometryConverter.Collections
{
    using System.Collections.Generic;
    using Bases;

    public static class ElementManager
    {
        /// <summary>
        /// Absolute MUST to perform before any computation and element analysis
        /// </summary>
        public static void SetNeighbourhoodRelations(List<Element> Elements)
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                for (var j = 0; j < Elements.Count; j++)
                {
                    if (i != j)
                        Elements[i].DefinePositionIfNeighbour(Elements[j]);
                }
            }
        }

        /// <summary>
        /// Removes elements from the collection
        /// </summary>
        /// <param name="elementCollection">Elements to remove</param>
        /// <param name="elementsToRemove"></param>
        public static void RemoveElements(this List<Element> elementCollection, List<Element> elementsToRemove)
        {
            foreach (var element in elementsToRemove)
            {
                elementCollection.RemoveAll(e => e.Index == element.Index);
            }
        }

        /// <summary>
        /// Clones collection
        /// </summary>
        /// <returns>Copy of the collection</returns>
        public static List<Element> Clone(this List<Element> elements)
        {
            var result = new List<Element>();

            for (var i = 0; i < elements.Count; i++)
            {
                result.Add((Element)elements[i].Clone());
            }

            return result;
        }
    }
}