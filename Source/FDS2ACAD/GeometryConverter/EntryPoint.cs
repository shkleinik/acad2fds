using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Interop.Common;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using GeometryConverter;
using GeometryConverter.DAL;
using GeometryConverter.DAL.Collections;

[assembly: CommandClass(typeof(EntryPoint))]

namespace GeometryConverter
{
    public class EntryPoint
    {
        [CommandMethod("FDS")]
        static public void Test()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Transaction tr = db.TransactionManager.StartTransaction();

            using (tr)
            {
                try
                {
                    // Prompt for selection of a solid to be traversed
                    PromptEntityOptions prEntOpt = new PromptEntityOptions("\nSelect a 3D solid:");
                    prEntOpt.SetRejectMessage("\nMust be a 3D solid.");
                    prEntOpt.AddAllowedClass(typeof(Solid3d), true);

                    PromptEntityResult prEntRes = ed.GetEntity(prEntOpt);

                    Solid3d sol = (Solid3d)tr.GetObject(prEntRes.ObjectId, OpenMode.ForRead);

                    
                    Acad3DSolid oSol = (Acad3DSolid)sol.AcadObject;
                    ed.WriteMessage("\nSolid type: {0}", oSol.SolidType);
                    //ElementCollection result = SolidOperator.Analyze(sol);
                    //ed.WriteMessage("\nElement count: {0}", result.Elements.Count);
                }
                catch (System.Exception ex)
                {
                    ed.WriteMessage("\nException during analysis: {0}", ex.Message);
                }

            }
        }
    }
}
