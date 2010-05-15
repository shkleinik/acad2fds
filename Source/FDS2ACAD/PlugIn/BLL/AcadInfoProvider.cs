namespace Fds2AcadPlugin.BLL
{
    using System;
    using System.Collections.Generic;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;

    public static class AcadInfoProvider
    {
        public static List<Entity> AskUserToSelectSolids()
        {
            // Note: automate doc opening during debug
//#if DEBUG
//            var doc = new DefaultFactory().CreateDocumentManager().Open(@"C:\Users\walash\Desktop\Test.dwg");
//            new DefaultFactory().CreateDocumentManager().MdiActiveDocument = doc;
//#endif

            var document = new DefaultFactory().CreateDocumentManager().MdiActiveDocument;
            var database = document.Database;
            var editor = document.Editor;
            var transaction = database.TransactionManager.StartTransaction();
            var selectionOptions = new PromptSelectionOptions { MessageForAdding = "Select solids to analyze: " };
            var promptSelectionResult = editor.GetSelection(selectionOptions);

            if (promptSelectionResult.Status != PromptStatus.OK)
            {
                transaction.Commit();
                transaction.Dispose();
                return new List<Entity>();
            }
            
            var solids = new List<Entity>();
            var objectIds = promptSelectionResult.Value.GetObjectIds();

            foreach (var objectId in objectIds)
            {
                var dbObject = transaction.GetObject(objectId, OpenMode.ForRead);
                /// if (dbObject.GetType() == typeof(Solid3d) ) // || dbObject.GetType() == typeof(ImpCurve)
                    solids.Add((Entity)dbObject);

                // else if(dbObject.GetType().ToString() == "Autodesk.AutoCAD.DatabaseServices.ImpCurve")
                   // solids.Add((Entity)dbObject);
                
            }

            transaction.Commit();
            transaction.Dispose();

            return solids;
        }

        public static string GetDocumentName()
        {
            var pathToOpenDocument = new DefaultFactory().CreateDocumentManager().MdiActiveDocument.Name;

            char[] separator = { '\\' };
            string[] directories = pathToOpenDocument.Split(separator);
            string filename = directories[directories.Length - 1];
            filename = filename.Remove(filename.IndexOf("."));
            return filename;
        }

        public static string GetPathToPluginDirectory()
        {
            return string.Format(Constants.PluginFileSystemLocationPattern,
                                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)
                                );
        }
    }
}