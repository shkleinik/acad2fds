namespace GeometryConverter.Optimization
{
    using System.Collections.Generic;
    using Bases;
    using Collections;
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
            initialElements = valueableElements;
            ElementManager.SetNeighbourhoodRelations(initialElements);
        }

        #endregion

        #region Internal implementation

        /// <summary>
        /// Optimizes collection of many equal elements into collection of several unequal ones
        /// </summary>
        /// <returns>Optimezed collection</returns>
        public List<Element> Optimize()
        {
            if(initialElements.Count < 1)
                return new List<Element>();

            var probe = initialElements.Clone();

            if (optimizedElements != null)
                return optimizedElements;

            optimizedElements = new List<Element>();

            List<Element> stage1d;
            List<Element> stage2d;
            List<Element> stage3d;

            do
            {
                var freeElement = GetMostFreeElement();

                stage1d = CalculateStage(freeElement);
                stage2d = CalculateStage(stage1d);
                stage3d = CalculateStage(stage2d);

                return stage3d;

                probe.RemoveElements(stage3d);
                usedDirections.Clear();

                optimizedElements.Add(stage3d.ToElement());
            } while (probe.Count > 0);

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

        /// <summary>
        /// Defines whether is an available level for the collection in the direction
        /// </summary>
        /// <param name="elementCollection">Collection</param>
        /// <param name="direction">Direction</param>
        /// <param name="levelRate">Current rate of level</param>
        /// <returns></returns>
        private bool LevelExistsInDirection(List<Element> elementCollection, int direction, int levelRate)
        {
            bool result = false;
            var sampleCollection = GetTmpCollection(elementCollection, direction, levelRate);

            for (int j = 0; j < levelRate + 1; j++)
                for (int i = 0; i < elementCollection.Count; i++)
                {
                    if (sampleCollection[i + levelRate].Neighbours[direction] != null)
                    {
                        //index = (int)elementCollection.Elements[i].Neighbours[direction];
                        result = true;
                        continue;
                    }
                    result = false;
                    break;
                }
            return result;
        }

        private static bool LevelExistsInDirection(IList<Element> elementCollection, int direction)
        {
            bool result = false;
            for (int i = 0; i < elementCollection.Count; i++)
            {
                if (elementCollection[i].Neighbours[direction] != null)
                {
                    //index = (int)elementCollection.Elements[i].Neighbours[direction];
                    result = true;
                    continue;
                }
                result = false;
                break;
            }
            return result;
        }

        private List<Element> GetTmpCollection(List<Element> elementCollection, int direction, int levelRate)
        {
            var result = new List<Element>();
            result.AddRange(elementCollection);

            var tmpCollection = elementCollection.Clone();

            for (int i = 0; i < levelRate + 1; i++)
            {
                if (!LevelExistsInDirection(tmpCollection, direction))
                    break;
                for (int j = 0; j < tmpCollection.Count; j++)
                {
                    tmpCollection[j] = initialElements[(int)tmpCollection[j].Neighbours[direction]];
                }

                result.AddRange(tmpCollection);
            }
            return result;
        }

        /// <summary>
        /// Adds level to the collection in appropriate direction
        /// </summary>
        /// <param name="elementCollection">Collection to be grown</param>
        /// <param name="direction">Direction</param>
        /// <param name="addition">Addition for output</param>
        private static void AddLevel(IList<Element> elementCollection, int direction, out List<Element> addition)
        {
            addition = new List<Element>();

            for (var i = 0; i < elementCollection.Count; i++)
            {
                var elementIndex = elementCollection[i].Neighbours[direction];
                addition.Add(elementCollection[(int)elementIndex]);
            }
        }

        private Element GetMostFreeElement()
        {
            var maxEmpty = int.MinValue;
            var idxFreeElement = int.MinValue;

            for (var i = 0; i < initialElements.Count; i++)
            {
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

                        var neighbourIdx = (int)initialElements[index].Neighbours[i];
                        neighboursInDirection.Add(initialElements[neighbourIdx]);
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

        private List<Element> GetMaxDepthCollectionInDirection(List<Element> elements, Direction direction, int depth)
        {
            var oneDcollection = new List<Element>();

            var current = elements;
            oneDcollection.AddRange(current);

            for (var i = 0; i < depth; i++)
            {
                var nextLevel = new List<Element>();

                foreach (var element in current)
                {
                    var neighbourIdx = (int)element.Neighbours[(int)direction];
                    nextLevel.Add(initialElements[neighbourIdx]);
                }

                oneDcollection.AddRange(nextLevel);
                current = nextLevel;
            }

            return oneDcollection;
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