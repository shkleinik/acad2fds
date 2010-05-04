namespace GeometryConverter.Collections
{
    using System.Collections.Generic;
    using Bases;

    public class ElementCollection
    {
        public List<Element> Elements;

        #region Constructors

        public ElementCollection()
        {
            Elements = new List<Element>();
        }

        public ElementCollection(List<Element> elements)
        {
            Elements = elements;
        }

        public ElementCollection(Element element)
        {
            Elements = new List<Element> { element };
        }

        #endregion

        /// <summary>
        /// Converts collection to single element
        /// </summary>
        /// <returns>Element</returns>
        public Element ToElement()
        {
            var centerMin = new BasePoint(double.MaxValue, double.MaxValue, double.MaxValue);
            var centerMax = new BasePoint(double.MinValue, double.MinValue, double.MinValue);
            foreach (var element in Elements)
            {
                if (element.Center.IsGreater(centerMax))
                    centerMax = element.Center;
                if (element.Center.IsLower(centerMin))
                    centerMin = element.Center;
            }
            double centerX = (centerMax.X + centerMin.X) / 2;
            double centerY = (centerMax.Y + centerMin.Y) / 2;
            double centerZ = (centerMax.Z + centerMin.Z) / 2;
            var center = new BasePoint(centerX, centerY, centerZ);
            double lengthX = centerMax.X - centerMin.X + Elements[0].XLength;
            double lengthY = centerMax.Y - centerMin.Y + Elements[0].YLength;
            double lengthZ = centerMax.Z - centerMin.Z + Elements[0].ZLength;
            // Todo : decide if it is necessary to set in this constructor index
            var result = new Element(center, lengthX, lengthY, lengthZ);
            return result;
        }

        /// <summary>
        /// Absolute MUST to perform before any computation and element analysis
        /// </summary>
        public ElementCollection SetNeighbourhoodRelations()
        {
            // // Note : Set indeces on initialization
            // for (var i = 0; i < Elements.Count; i++)
            // {
            //     Elements[i].Index = i;
            // }

            for (var i = 0; i < Elements.Count; i++)
            {
                for (var j = 0; j < Elements.Count; j++)
                {
                    if (i != j)
                        Elements[i].DefinePositionIfNeighbour(Elements[j]);
                }
            }

            return this;
        }

        /// <summary>
        /// Adds collection to collection
        /// </summary>
        /// <param name="collection">Addition</param>
        public void AddCollection(ElementCollection collection)
        {
            foreach (Element element in collection.Elements)
            {
                Elements.Add(element);
            }
        }

        /// <summary>
        /// Removes elements from the collection
        /// </summary>
        /// <param name="elementCollection">Elements to remove</param>
        public void RemoveElements(ElementCollection elementCollection)
        {
            for (var i = 0; i < elementCollection.Elements.Count; i++)
            {
                Elements[(int)elementCollection.Elements[i].Index] = null;
            }
        }

        /// <summary>
        /// Clones collection
        /// </summary>
        /// <returns>Copy of the collection</returns>
        public ElementCollection Clone()
        {
            var result = new ElementCollection();

            for (var i = 0; i < Elements.Count; i++)
            {
                result.Elements.Add((Element)Elements[i].Clone());
            }

            return result;
        }

        /// <summary>
        /// Return length of the collection
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            var length = 0;
            for (var i = 0; i < Elements.Count; i++)
            {
                if (Elements[i] != null)
                    length++;
            }
            return length;
        }

        /// <summary>
        /// Retuns first element which is not a null yet
        /// </summary>
        /// <returns>Element</returns>
        public Element SelectFirstElement()
        {
            for (var i = 0; i < Elements.Count; i++)
            {
                if (Elements[i] != null)
                    return Elements[i];
            }

            return null;
        }
    }
}