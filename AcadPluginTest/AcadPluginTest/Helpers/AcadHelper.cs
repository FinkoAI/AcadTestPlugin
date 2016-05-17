using System;
using System.Collections.Generic;
using System.Linq;
using AcadPluginTest.Enums;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;

namespace AcadPluginTest.Helpers
{
    public static class AcadHelper
    {
        /// <summary>
        /// Возвращает список всех слоёв документа
        /// </summary>
        /// <param name="db">БД документа</param>
        /// <returns></returns>
        private static List<LayerTableRecord> GetLayers(Database db)
        {
            List<LayerTableRecord> result;

            using (var tr = db.TransactionManager.StartOpenCloseTransaction())
            {
                var layerTable = tr.GetObject(db.LayerTableId, OpenMode.ForRead) as LayerTable;

                if (layerTable == null) 
                    return null;

                result = new List<LayerTableRecord>();
                foreach (var recordId in layerTable)
                {
                    var layerRecord = tr.GetObject(recordId, OpenMode.ForRead) as LayerTableRecord;
                    result.Add(layerRecord);
                }

                tr.Commit();
            }

            return result;
        }

        /// <summary>
        /// Возвращает список всех Id объектов по имени слоя
        /// </summary>
        /// <param name="layerName"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static List<ObjectId> GetLayerObjectIds(string layerName, Document doc)
        {
            var editor = doc.Editor;

            var typedValues =
                new[]
                {
                    new TypedValue(
                        (int) DxfCode.LayerName,
                        layerName
                        )
                };

            var selectionFilter = new SelectionFilter(typedValues);

            var selectionResult = editor.SelectAll(selectionFilter);

            if (selectionResult.Status == PromptStatus.OK)
            {
                var result = selectionResult.Value.GetObjectIds();
                return result.ToList();
            }

            return null;
        }

        /// <summary>
        /// Возвращает список всех объектов на основе переданных Id
        /// </summary>
        /// <param name="objectIds"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private static List<IAcadObject> GetAllObjectsByIdList(List<ObjectId> objectIds, Database db)
        {
            var result = new List<IAcadObject>();
            
            using (var tr = db.TransactionManager.StartOpenCloseTransaction())
            {
                foreach (var objectId in objectIds.Where(x => x.ObjectClass.DxfName.AsObjectType() != ObjectType.Unknown))
                {
                    switch (objectId.ObjectClass.DxfName.AsObjectType())
                    {
                        case ObjectType.Line:
                            var line = tr.GetTypedObject<Line>(objectId, OpenMode.ForRead);
                            result.Add(new AcadLineVm(objectId, line));
                            break;
                        case ObjectType.Point:
                            var point = tr.GetTypedObject<DBPoint>(objectId, OpenMode.ForRead);
                            result.Add(new AcadPointVm(objectId, point));
                            break;
                        case ObjectType.Circle:
                            var circle = tr.GetTypedObject<Circle>(objectId, OpenMode.ForRead);
                            result.Add(new AcadCircleVm(objectId, circle));
                            break;
                        case ObjectType.Unknown:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                tr.Commit();
            }

            return result;
        }

        /// <summary>
        /// Возвращает список ViewModel'ей слоёв
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<AcadLayerVm> GetLayerVms(Document doc)
        {
            var database = doc.Database;

            var acadLayers = GetLayers(database);
            var layerVmList = new List<AcadLayerVm>();

            foreach (var layerRecord in acadLayers)
            {
                var layerObjectIds = GetLayerObjectIds(layerRecord.Name, doc);
                var layerObjects = GetAllObjectsByIdList(layerObjectIds, database);

                var layerVm = new AcadLayerVm(layerRecord, layerObjects);

                layerVmList.Add(layerVm);
            }

            return layerVmList;
        }

        /// <summary>
        /// Выделяет объекты на чертеже на основе переданного списка Id
        /// </summary>
        /// <param name="objectIds"></param>
        /// <param name="doc"></param>
        public static void SelectDrawingObjects(IEnumerable<ObjectId> objectIds, Document doc)
        {
            Utils.SelectObjects(objectIds.ToArray());
            doc.Editor.UpdateScreen();
            doc.Editor.Regen();
        }

        /// <summary>
        /// Снимает выделение у всех объектов чертежа 
        /// </summary>
        /// <param name="document"></param>
        public static void DeselectAllDrawingObjects(Document document)
        {
            var editor = document.Editor;
            var newIds = new ObjectId[0];
            editor.SetImpliedSelection(newIds);
        }
    }
}
