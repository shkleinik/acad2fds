namespace Fds2AcadPlugin.BLL
{
    using System.Collections.Generic;
    using System.IO;
    using Autodesk.AutoCAD.DatabaseServices;
    using Autodesk.AutoCAD.EditorInput;

    public static class AcadInfoProvider
    {
        public static List<Entity> AskUserToSelectSolids()
        {
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

                if (dbObject.GetType() == typeof(Solid3d))
                    solids.Add((Entity)dbObject);
            }

            transaction.Commit();
            transaction.Dispose();

            return solids;
        }

        public static IEnumerable<Entity> AllSolidsFromCurrentDrawing()
        {
            var objectIds = new DefaultFactory().CreateDocumentManager().MdiActiveDocument.Editor.SelectAll().Value.GetObjectIds();
            var transaction = new DefaultFactory().CreateDocumentManager().MdiActiveDocument.Database.TransactionManager.StartTransaction();

            foreach (var objectId in objectIds)
            {
                var dbObject = transaction.GetObject(objectId, OpenMode.ForRead);

                if (dbObject.GetType() == typeof(Solid3d))
                    yield return (Entity)dbObject;
            }
            
        }

        public static string GetDocumentName()
        {
            var pathToOpenDocument = new DefaultFactory().CreateDocumentManager().MdiActiveDocument.Name;

            return Path.GetFileNameWithoutExtension(pathToOpenDocument);
        }
    }
}