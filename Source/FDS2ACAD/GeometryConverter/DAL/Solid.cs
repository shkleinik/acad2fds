using System;
using Autodesk.AutoCAD.BoundaryRepresentation;
using Autodesk.AutoCAD.DatabaseServices;
using GeometryConverter.BLL.Bases;
using BrFace = Autodesk.AutoCAD.BoundaryRepresentation.Face;

namespace GeometryConverter.DAL
{
    static class Solid
    {
        public static Brep Brep;

        static void GetBrepData(Entity solid)
        {
        }

        /// <summary>
        /// Returns minimal and maximal Bases point
        /// </summary>
        /// <param name="solid">ACAD solid</param>
        /// <returns>Array of 2 elements where [0] is Max and [1] is Min</returns>
        public static BasePoint[] GetMinMaxPoint(Entity solid)
        {
            BasePoint[] result = new BasePoint[2];
            result[0] = new BasePoint(double.MaxValue, double.MaxValue, double.MaxValue);
            result[1] = new BasePoint(double.MinValue, double.MinValue, double.MinValue);

            using (new Brep(solid))
            {
                int cmpCnt = 0;
                foreach (Complex cmp in Brep.Complexes)
                {
                    int shlCnt = 0;
                    foreach (Shell shl in cmp.Shells)
                    {
                        int fceCnt = 0;
                        foreach (BrFace fce in shl.Faces)
                        {
                            int lpCnt = 0;
                            foreach (BoundaryLoop lp in fce.Loops)
                            {
                                int edgCnt = 0;
                                foreach (Edge edg in lp.Edges)
                                {
                                    BasePoint tmp = BLL.Helpers.PointBridge.ConvertToBasePoint(edg.Vertex1.Point);
                                    if (tmp.IsGreater(result[1]))
                                        result[1] = tmp;
                                    if (tmp.IsLower(result[0]))
                                        result[0] = tmp;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
