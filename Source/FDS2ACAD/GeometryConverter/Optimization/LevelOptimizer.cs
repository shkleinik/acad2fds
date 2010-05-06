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
        private List<Element> optimizedElements;
        private int dirNum;

        #endregion

        #region Constructors

        public LevelOptimizer(List<Element> valueableElements)
        {
            dirNum = EnumHelper.GetEnumElementsNumber(EnumHelper.Direction);
            this.initialElements = valueableElements;
            // Todo : Need factorize?
            ElementManager.SetNeighbourhoodRelations(this.initialElements);
        }

        #endregion

        #region Internal implementation

        /// <summary>
        /// Optimizes collection of many equal elements into collection of several unequal ones
        /// </summary>
        /// <returns>Optimezed collection</returns>
        public List<Element> Optimize()
        {
            var probe = initialElements.Clone();

            if (optimizedElements != null)
                return optimizedElements;

            optimizedElements = new List<Element>();

            List<Element> stage1d;
            List<Element> stage2d;
            List<Element> stage3d;

            do
            {
                var localProbe = GetCornerElement();

                return CalculateStage(localProbe);

                stage1d = CalculateStage(localProbe);
                stage2d = CalculateStage(stage1d);
                stage3d = CalculateStage(stage2d);

                probe.RemoveElements(stage3d);

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
            List<Element> result = probe.Clone();

            var levelRate = 0;
            var levelRateTmp = 0;
            var bestDirection = 0;

            var maxDepthCollection = new List<Element>();
            for (var i = 0; i < probe.Count; i++)
            {
                maxDepthCollection = GetMaxDepthCollection(probe[0]);
            }


            return maxDepthCollection;

            for (var i = 0; i < dirNum; i++)
            {
                while (LevelExistsInDirection(probe, i, levelRateTmp))
                // Todo: resolve problem with infinitive neighbour index reference
                {
                    levelRateTmp++;
                }
                if (levelRateTmp > levelRate)
                {
                    bestDirection = i;
                    levelRate = levelRateTmp;
                    levelRateTmp = 0;
                }
            }

            List<Element> addition;
            for (var i = 0; i < levelRate; i++)
            {
                AddLevel(probe, bestDirection, out addition);
                result.AddRange(addition);
            }



            //(LevelExistsInDirection(probe, bestDirection))
            //{    
            //    AddLevel(probe, bestDirection, out addition);
            //    res   ult.AddCollection(addition);
            //}

            return result;
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

        private Element GetCornerElement()
        {
            var maxEmpty = int.MinValue;
            var idxCornerElement = int.MinValue;

            for (var i = 0; i < initialElements.Count; i++)
            {
                var nullElements = Array.FindAll(initialElements[i].Neighbours, (el => el == null)).Length;

                if (maxEmpty < nullElements)
                    idxCornerElement = i;
            }

            return initialElements[idxCornerElement];
        }

        private int[] GetLevelsInAllDirections(Element element)
        {
            var levels = new int[dirNum];
            var levelNeighbours = 0;
            var current = element.Index;

            for (var i = 0; i < levels.Length; i++)
            {
                while (initialElements[(int)current].Neighbours[i] != null)
                {
                    levelNeighbours++;
                    current = initialElements[(int)current].Neighbours[i];
                }

                levels[i] = levelNeighbours;
                current = element.Index;
                levelNeighbours = 0;
            }

            return levels;
        }

        private Direction GetDirectionOfMaxDepth(int[] levels)
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

        private List<Element> Get1dCollection(Element element, Direction direction, int depth)
        {
            var oneDcollection = new List<Element>();

            var current = element;
            oneDcollection.Add(current);

            for (var i = 0; i < depth; i++)
            {
                current = initialElements[(int)current.Neighbours[(int)direction]];
                oneDcollection.Add(current);
            }

            return oneDcollection;
        }

        private List<Element> GetMaxDepthCollection(Element element)
        {
            var levelsDepthes = GetLevelsInAllDirections(element);
            var maxDepthDirection = GetDirectionOfMaxDepth(levelsDepthes);

            return Get1dCollection(element, maxDepthDirection, levelsDepthes[(int)maxDepthDirection]);
        }

        #endregion
    }
}