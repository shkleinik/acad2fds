namespace GeometryConverter
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.BoundaryRepresentation;
    using Autodesk.AutoCAD.DatabaseServices;
    using Bases;
    using Collections;
    using Helpers;
    using Element = Bases.Element;

    public class SolidToElementConverter : BaseSolidToElementConverter
    {
        #region Fields

        private ElementCollection _valueableCollection;

        #endregion

        #region Properties

        public ElementCollection UsefulElementCollectionProvider
        {
            get
            {
                //uncomment to OPTIMIZE:
                _valueableCollection = GetValuableElements(Factorize(AllElements));
                _valueableCollection.SetNeighbourhoodRelations();

                // return _valueableCollection;
                // return new ElementCollection();
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
        /// <param name="input">All elements collection</param>
        /// <param name="input">Input</param>
        /// <returns>Collection of valuable elements</returns>
        private ElementCollection GetValuableElements(IList<Element> elements)
        {
            var result = new ElementCollection();
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
                    result.Elements.Add(elements[i]);
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
        private ElementCollection Optimize(ElementCollection elements)
        {
            var probe = elements.Clone();
            var result = new ElementCollection();
            ElementCollection stage1d;
            ElementCollection stage2d;
            ElementCollection stage3d;

            do
            {
                var localProbe = new ElementCollection(probe.SelectFirstElement());
                stage1d = CalculateStage(localProbe);
                stage2d = CalculateStage(stage1d);
                stage3d = CalculateStage(stage2d);
                probe.RemoveElements(stage3d);
                result.Elements.Add(stage3d.ToElement());
            } while (probe.SelectFirstElement() != null);
            return result;
        }

        /// <summary>
        /// Caclculate stage of optimization for temporary collection
        /// </summary>
        /// <param name="probe">Probe</param>
        /// <returns>Level</returns>
        private ElementCollection CalculateStage(ElementCollection probe)
        {
            ElementCollection result = probe.Clone();

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

            ElementCollection addition;
            for (var i = 0; i < levelRate; i++)
            {
                AddLevel(probe, bestDirection, out addition);
                result.AddCollection(addition);
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
        private bool LevelExistsInDirection(ElementCollection elementCollection, int direction, int levelRate)
        {
            bool result = false;
            var sampleCollection = GetTmpCollection(elementCollection, direction, levelRate);

            for (int j = 0; j < levelRate + 1; j++)
                for (int i = 0; i < elementCollection.Elements.Count; i++)
                {
                    if (sampleCollection.Elements[i + levelRate].Neighbours[direction] != null)
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

        private static bool LevelExistsInDirection(ElementCollection elementCollection, int direction)
        {
            bool result = false;
            for (int i = 0; i < elementCollection.Elements.Count; i++)
            {
                if (elementCollection.Elements[i].Neighbours[direction] != null)
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

        private ElementCollection GetTmpCollection(ElementCollection elementCollection, int direction, int levelRate)
        {
            var result = new ElementCollection();
            result.AddCollection(elementCollection);

            var tmpCollection = elementCollection.Clone();

            for (int i = 0; i < levelRate + 1; i++)
            {
                if (!LevelExistsInDirection(tmpCollection, direction))
                    break;
                for (int j = 0; j < tmpCollection.Elements.Count; j++)
                {
                    tmpCollection.Elements[j] = _valueableCollection.Elements[(int)tmpCollection.Elements[j].Neighbours[direction]];
                }
                result.AddCollection(tmpCollection);
            }
            return result;
        }

        /// <summary>
        /// Adds level to the collection in appropriate direction
        /// </summary>
        /// <param name="elementCollection">Collection to be grown</param>
        /// <param name="direction">Direction</param>
        /// <param name="addition">Addition for output</param>
        private static void AddLevel(ElementCollection elementCollection, int direction, out ElementCollection addition)
        {
            addition = new ElementCollection();

            for (var i = 0; i < elementCollection.Elements.Count; i++)
            {
                var elementIndex = elementCollection.Elements[i].Neighbours[direction];
                addition.Elements.Add(elementCollection.Elements[(int)elementIndex]);
            }
        }

        #endregion
    }
}