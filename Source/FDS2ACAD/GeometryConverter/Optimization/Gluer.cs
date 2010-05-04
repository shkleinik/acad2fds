namespace GeometryConverter.Optimization
{
    using System;
    using System.Collections.Generic;
    using Bases;

    public class Gluer
    {
        #region Fields

        private readonly List<Element> elements;
        private readonly List<Element> columns;
        private List<int> layerIndices;

        #endregion


        #region Constructors

        public Gluer(List<Element> elements)
        {
            this.elements = elements;
            columns = new List<Element>();
            SetNeighbourhoodRelations();
            PopulateHorizontalNeighboursIndices(elements[0]);
        } 

        #endregion

        public List<Element> GetGluedElements()
        {
            var result = new List<Element>();

            foreach (var i in layerIndices)
            {
                columns.Add(GetColumnFromElement(elements[i]));
            }

            return result;
        }

        private Element GetColumnFromElement(Element element)
        {
            var column = (Element)element.Clone();

            int topNeighbours = 0;
            int bottomNeighbours = 0;
            int? current = element.NeighbourTop;

            if (current != null)
                while (elements[(int)current].NeighbourTop != null)
                {
                    topNeighbours++;
                }

            if (current != null)
                while (elements[(int)current].NeighbourBottom != null)
                {
                    bottomNeighbours++;
                }

            column.NeighbourTop = null;
            column.NeighbourBottom = null;

            column.ZLength = element.ZLength * (1 + topNeighbours + bottomNeighbours);

            return column;
        }

        private void SetNeighbourhoodRelations()
        {
            for (var i = 0; i < elements.Count; i++)
            {
                for (var j = 0; j < elements.Count; j++)
                {
                    if (i != j)
                        elements[i].DefinePositionIfNeighbour(elements[j]);
                }
            }
        }

        private void PopulateHorizontalNeighboursIndices(Element element)
        {
            layerIndices = new List<int>();
            var stack = new List<Element>();

            // Start point
            var startElement = element;  // ADD
            stack.Add(startElement);

            do
            {
                for (var i = 0; i < stack.Count; i++)
                {
                    i = stack.Count - 1;

                    if (stack[i].Index != null)
                        layerIndices.Add((int)stack[i].Index);
                    else
                        throw new ArgumentNullException("element");
                    
                    for (var j = 2; j < 6; j++)
                    {
                        var neighbour = GetNeighbourElement(stack[i], j);

                        if (neighbour != null)
                            stack.Add(neighbour);
                    }

                    stack.RemoveAt(i);
                }
            }
            while (stack.Count > 0);
        }

        private Element GetNeighbourElement(Element center, int direction)
        {
            if(center.Neighbours[direction] == null)
                return null;

            var index = (int)center.Neighbours[direction];

            return !layerIndices.Contains(index) ? elements[index] : null;
        }
    }
}