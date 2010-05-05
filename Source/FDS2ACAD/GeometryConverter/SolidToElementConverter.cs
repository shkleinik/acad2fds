namespace GeometryConverter
{
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using Autodesk.AutoCAD.DatabaseServices;
    using Collections;
    using Helpers;
    using Element = Bases.Element;

    public class SolidToElementConverter : BaseSolidToElementConverter
    {
        #region Fields

        private List<Element> _valueableCollection;

        #endregion

        #region Properties

        public List<Element> UsefulElementCollectionProvider
        {
            get
            {
                _valueableCollection = GetValuableElements(Factorize(AllElements));
                ElementManager.SetNeighbourhoodRelations(_valueableCollection);

                // return _valueableCollection;
                // return new ElementManager();
                return Optimize(_valueableCollection);

                //comment if have uncommented previous:
                //return GetValuableElements(Factorize(_fullCollection));
            }
        }

        #endregion

        #region Constructors

        public SolidToElementConverter(Solid3d solid)
            : base(new List<Solid3d> { solid }, DefaultFactor)
        {
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="solids">Array of selected solids</param>
        public SolidToElementConverter(List<Solid3d> solids)
            :base(solids, DefaultFactor)
        {
        }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="solids">Array of selected solids</param>
        /// <param name="factor">Unit factor</param>
        public SolidToElementConverter(List<Solid3d> solids, double factor)
            :base(solids, factor)
        {
        }

        #endregion

        #region Internal implementation

        /// <summary>
        /// Set factor field for each element of Collection
        /// </summary>
        /// <param name="collection">Unfactorized collection</param>
        /// <returns>Factorized collection</returns>
        private List<Element> Factorize(List<Element> collection)
        {
            foreach (var element in collection)
            {
                element.Factor = _factor;
            }
            return collection;
        }

        /// <summary>
        /// Provides collection of valuable elements that belongs to the Solid from all elements
        /// </summary>
        /// <returns>Collection of valuable elements</returns>
        private List<Element> GetValuableElements(IList<Element> elements)
        {
            var result = new List<Element>();
            for (var i = 0; i < elements.Count; i++)
            {
                foreach (var solid in _solids)
                {
                    var material = solid.Material;
                    var brep = new Brep(solid);
                    PointContainment containment;
                    brep.GetPointContainment(elements[i].Center.Unfactorize(_factor).ConverToAcadPoint(), out containment);

                    if (containment != PointContainment.Inside)
                        continue;

                    elements[i].Material = material;
                    result.Add(elements[i]);
                }
            }

            return result;
        }
        #endregion

        #region Collection optimization

        /// <summary>
        /// Optimizes collection of many equal elements into collection of several unequal ones
        /// </summary>
        /// <param name="elements">Unoptimized collection</param>
        /// <returns>Optimezed collection</returns>
        private List<Element>  Optimize(List<Element> elements)
        {
            var probe = elements.Clone();
            var result = new List<Element>();
            List<Element>  stage1d;
            List<Element>  stage2d;
            List<Element>  stage3d;

            do
            {
                var localProbe =  new List<Element>{probe[0]};/*SelectFirstElement()*/
                stage1d = CalculateStage(localProbe);
                stage2d = CalculateStage(stage1d);
                stage3d = CalculateStage(stage2d);

                probe.RemoveElements(stage3d);
                //probe.Elements.;
                
                result.Add(stage3d.ToElement());
            } while (probe.Count > 0);
            return result;
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

            for (var i = 0; i < EnumHelper.GetEnumElementsNumber(EnumHelper.Direction); i++)
            {
                while (LevelExistsInDirection(probe, i, levelRateTmp))
                    //todo: resolve problem with infinitive neighbour index reference
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

        private static bool LevelExistsInDirection(List<Element> elementCollection, int direction)
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
                    tmpCollection[j] = _valueableCollection[(int)tmpCollection[j].Neighbours[direction]];
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
        private static void AddLevel(List<Element> elementCollection, int direction, out List<Element> addition)
        {
            addition = new List<Element>();

            for (var i = 0; i < elementCollection.Count; i++)
            {
                var elementIndex = elementCollection[i].Neighbours[direction];
                addition.Add(elementCollection[(int)elementIndex]);
            }
        }

        #endregion
    }
}