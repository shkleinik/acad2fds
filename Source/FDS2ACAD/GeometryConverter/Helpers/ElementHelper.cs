namespace GeometryConverter.Helpers
{
    using System.Collections.Generic;
    using Bases;

    public static class ElementHelper
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
                        Elements[i].SetReferenceIfNeighbour(Elements[j]);
                }
            }
        }

        /// <summary>
        /// Removes elements from the collection
        /// </summary>
        /// <param name="set">Elements to remove</param>
        /// <param name="subSet"></param>
        public static void SetNullAndRemoveElements(this List<Element> set, List<Element> subSet)
        {

            #region Set subset in set null and remove
            for (var i = 0; i < set.Count; i++)
            {
                if (set[i] == null)
                    continue;

                for (var j = 0; j < subSet.Count; j++)
                {
                    if (set[i].Index != subSet[j].Index) 
                        continue;

                    set[i] = null;
                    break;
                }
            }

            // set.RemoveAll(e => e == null); 

            #endregion


            #region Clear relationships

            for (var i = 0; i < set.Count; i++)
            {
                if(set[i] == null)
                    continue;

                for (var j = 0; j < set[i].Neighbours.Length; j++)
                {
                    foreach (var element in subSet)
                    {
                        if (set[i].Neighbours[j] != null)
                            if (element.Index == set[i].Neighbours[j].Index)
                                set[i].Neighbours[j] = null;
                    }
                }
            } 

            #endregion

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