/*
 * TODO : !!!  Problems !!!:
 * 1) The algorithm can be enhanced if we can find the way how to make NULL ALL referenced to 
 * specific memory set. Sample:
 * 
 *           var myObj = new Object();
 *           var obj1 = myObj;
 *           var obj2 = myObj;
 *           var obj3 = myObj;
 *           var obj4 = myObj;
 *           // . . .
 *           var objN = myObj;
 *           myObj = null; // objects 1..N still refers to the specified memory set.
 *   
 * 2) We should solve out how to find size of "initial element" or find another way how to get valueable elements
 * 3) AutoCad Architecture object model is not the same as simple AutoCad. We should find some solutions for this issue.
 * 4*) Make burnable all planes of burner solid (not required but desirable).
 * 5)Resolve all issues with rounding.
 * 6) Materials, materials . . . 
 * 7) Try to optimize again, while number of initial elements is greater, than optimezed (just if have time).
 * 8 :)  Go to play basketball (optional, desireable).
 * 9) Try to save AutoCad Architecture drawings in other Acad format(or something similar) and run calculation.
 */
namespace GeometryConverter.Optimization
{
    using System.Collections.Generic;
    using Bases;
    using Helpers;
    using System;


    public class LevelOptimizer
    {
        #region Fields

        private readonly List<Element> initialElements;
        private readonly List<Direction> usedDirections = new List<Direction>();
        private List<Element> optimizedElements;
        private readonly int directionsNumber;

        #endregion

        #region Constructors

        public LevelOptimizer(List<Element> valueableElements)
        {
            directionsNumber = EnumHelper.GetEnumElementsNumber(EnumHelper.Direction);
            initialElements = valueableElements.Clone();
            ElementHelper.SetNeighbourhoodRelations(initialElements);
        }

        #endregion

        #region Internal implementation

        /// <summary>
        /// Optimizes collection of many equal elements into collection of several unequal ones
        /// </summary>
        /// <returns>Optimezed collection</returns>
        public List<Element> Optimize()
        {
            if (initialElements.Count < 1)
                return new List<Element>();

            if (optimizedElements != null)
                return optimizedElements;

            optimizedElements = new List<Element>();

            do
            {
                var freeElement = GetMostFreeElement();

                var stage1d = CalculateStage(freeElement);
                var stage2d = CalculateStage(stage1d);
                var stage3d = CalculateStage(stage2d);
                optimizedElements.Add(stage3d.ToElement());

                initialElements.SetNullAndRemoveElements(stage3d);
                usedDirections.Clear();

            } while (initialElements.FindAll(e => e != null).Count > 0);

            return optimizedElements;
        }

        private List<Element> CalculateStage(Element probe)
        {
            return CalculateStage(new List<Element> { probe });
        }

        /// <summary>
        /// Caclculate stage of optimization for temporary collection
        /// </summary>
        /// <param name="probe">Probe</param>
        /// <returns>Level</returns>
        private List<Element> CalculateStage(List<Element> probe)
        {
            return GetMaxDepthCollection(probe);
        }

        private Element GetMostFreeElement()
        {
            var maxEmpty = int.MinValue;
            var idxFreeElement = int.MinValue;

            for (var i = 0; i < initialElements.Count; i++)
            {
                if (initialElements[i] == null)
                    continue;

                var nullElements = Array.FindAll(initialElements[i].Neighbours, (el => el == null)).Length;

                if (maxEmpty < nullElements)
                    idxFreeElement = i;
            }

            return initialElements[idxFreeElement];
        }

        // Todo : break this big ugly method to smaller
        private int[] GetLevelsInAllDirections(IList<Element> elements)
        {
            var levels = new int[directionsNumber];
            var levelNeighbours = 0;
            var currentIndices = GetCurrentIndices(elements);
            var allElementsHasNeighbourInDirection = true;
            var neighboursInDirection = new List<Element>();

            // go in all directions
            for (var i = 0; i < levels.Length; i++)
            {
                if (usedDirections.Contains((Direction)i))
                    continue;

                // check if all elements in direction have neighbours
                while (allElementsHasNeighbourInDirection)
                {
                    foreach (var index in currentIndices)
                    {
                        if (initialElements[index].Neighbours[i] == null)
                            break;

                        var neighbour = initialElements[index].Neighbours[i];
                        neighboursInDirection.Add(neighbour);
                    }

                    if (neighboursInDirection.Count == currentIndices.Length)
                    {
                        levelNeighbours++;
                        currentIndices = GetCurrentIndices(neighboursInDirection);
                        neighboursInDirection.Clear();
                    }
                    else
                    {
                        allElementsHasNeighbourInDirection = false;
                    }
                }

                levels[i] = levelNeighbours;
                currentIndices = GetCurrentIndices(elements);
                levelNeighbours = 0;
                allElementsHasNeighbourInDirection = true;
            }

            return levels;
        }

        private static int[] GetCurrentIndices(IList<Element> elements)
        {
            var currentIndices = new int[elements.Count];

            for (var i = 0; i < elements.Count; i++)
            {
                currentIndices[i] = (int)elements[i].Index;
            }

            return currentIndices;
        }

        private static Direction GetDirectionOfMaxDepth(int[] levels)
        {
            var max = int.MinValue;
            var idxMax = int.MinValue;

            for (var i = 0; i < levels.Length; i++)
            {
                if (max > levels[i])
                    continue;

                max = levels[i];
                idxMax = i;
            }

            return (Direction)idxMax;
        }

        private static List<Element> GetMaxDepthCollectionInDirection(List<Element> elements, Direction direction, int depth)
        {
            var maxDepthCollection = new List<Element>();

            var current = elements;
            maxDepthCollection.AddRange(current);

            for (var i = 0; i < depth; i++)
            {
                var nextLevel = new List<Element>();

                foreach (var element in current)
                {
                    var neighbour = element.Neighbours[(int)direction];
                    nextLevel.Add(neighbour);
                }

                maxDepthCollection.AddRange(nextLevel);
                current = nextLevel;
            }

            return maxDepthCollection;
        }

        private List<Element> GetMaxDepthCollection(List<Element> elements)
        {
            var levelsDepthes = GetLevelsInAllDirections(elements);
            var maxDepthDirection = GetDirectionOfMaxDepth(levelsDepthes);
            usedDirections.Add(maxDepthDirection);
            usedDirections.Add(GetInverseDirection(maxDepthDirection));

            return GetMaxDepthCollectionInDirection(elements, maxDepthDirection, levelsDepthes[(int)maxDepthDirection]);
        }

        private static Direction GetInverseDirection(Direction direction)
        {
            Direction inverseDirection;

            switch (direction)
            {
                case Direction.Top:
                    inverseDirection = Direction.Bottom;
                    break;
                case Direction.Bottom:
                    inverseDirection = Direction.Top;
                    break;
                case Direction.Left:
                    inverseDirection = Direction.Right;
                    break;
                case Direction.Right:
                    inverseDirection = Direction.Left;
                    break;
                case Direction.Front:
                    inverseDirection = Direction.Back;
                    break;
                case Direction.Back:
                    inverseDirection = Direction.Front;
                    break;
                default:
                    throw new NotSupportedException("This direction is not supported!");
            }

            return inverseDirection;
        }

        #endregion
    }
}