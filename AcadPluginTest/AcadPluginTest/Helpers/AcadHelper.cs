using System;
using System.Collections.Generic;
using System.Linq;
using AcadPluginTest.Enums;
using AcadPluginTest.Factories;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Internal;
using AcadApplication = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace AcadPluginTest.Helpers
{
    public static class AcadHelper
    {
        #region Public methods

        /// <summary>
        /// Возвращает список ViewModel'ей слоёв
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static List<ILayerObject> GetLayerVms(Document doc)
        {
            var database = doc.Database;

            var acadLayers = GetLayers(database);
            var layerVmList = new List<ILayerObject>();

            foreach (var layerRecord in acadLayers)
            {
                var layerObjectIds = GetLayerObjectIds(layerRecord.Name, doc);
                var layerObjects = GetAllObjectsByIdList(layerObjectIds, database);

                var layerVm = AcadObjectVmFactory.GetAcadObjectVm(layerRecord, layerObjects, database.LayerZero);

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
            ReDrawScreen(doc);
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

        /// <summary>
        /// Получает элементы дерева в виде плоского списка
        /// </summary>
        /// <param name="treeLayers"></param>
        /// <returns></returns>
        public static List<IAcadObject> GetFlatElementsTree(List<ILayerObject> treeLayers)
        {
            var modifiedObjects = treeLayers.Cast<IAcadObject>().ToList();
            modifiedObjects.AddRange(treeLayers.SelectMany(x => x.Objects));

            return modifiedObjects;
        }

        /// <summary>
        /// Проверяет наличие всех объектов дерева
        /// </summary>
        /// <param name="document"></param>
        /// <param name="layersList"></param>
        /// <returns></returns>
        public static bool IsAllObjectsExist(Document document, IEnumerable<ILayerObject> layersList)
        {
            var result = true;

            using (var tr = document.Database.TransactionManager.StartOpenCloseTransaction())
            {
                try
                {
                    foreach (var layer in layersList)
                    {
                        var layerRecord = tr.GetObject(layer.Id, OpenMode.ForRead) as LayerTableRecord;

                        if (layerRecord == null)
                            return false;

                        if (layer.Objects.Any(acadObject => tr.GetObject(acadObject.Id, OpenMode.ForRead) == null))
                        {
                            return false;
                        }

                    }
                }
                catch (Exception)
                {
                    result = false;
                }
                finally
                {
                    tr.Commit();
                }
            }

            return result;
        }

        /// <summary>
        /// Сохраняет изменения внесённые пользователем
        /// </summary>
        /// <param name="document"></param>
        /// <param name="modifiedObjects"></param>
        /// <returns></returns>
        public static bool SaveModifiedObjects(Document document, List<IAcadObject> modifiedObjects)
        {
            DeselectAllDrawingObjects(document);

            var result = true;
            
            using (document.LockDocument())
            {
                using (var tr = document.Database.TransactionManager.StartOpenCloseTransaction())
                {
                    try
                    {
                        foreach (var modifiedObject in modifiedObjects)
                        {
                            switch (modifiedObject.AcadObjectType)
                            {
                                case ObjectType.Point:
                                    var pointObject = tr.GetTypedObject<DBPoint>(modifiedObject.Id, OpenMode.ForWrite);
                                    var pointEntity = modifiedObject as AcadPointVm;
                                    UpdateAcadObject(ref pointObject, pointEntity);
                                    break;
                                case ObjectType.Layer:
                                    var layerObject = tr.GetTypedObject<LayerTableRecord>(modifiedObject.Id, OpenMode.ForWrite);
                                    var layerEntity = modifiedObject as AcadLayerVm;
                                    UpdateAcadObject(ref layerObject, layerEntity);
                                    break;
                                case ObjectType.Line:
                                    var lineObject = tr.GetTypedObject<Line>(modifiedObject.Id, OpenMode.ForWrite);
                                    var lineEntity = modifiedObject as AcadLineVm;
                                    UpdateAcadObject(ref lineObject, lineEntity);
                                    break;
                                case ObjectType.Circle:
                                    var circleObject = tr.GetTypedObject<Circle>(modifiedObject.Id, OpenMode.ForWrite);
                                    var circleEntity = modifiedObject as AcadCircleVm;
                                    UpdateAcadObject(ref circleObject, circleEntity);
                                    break;
                            }

                            modifiedObject.IsModified = false;
                        }


                        tr.Commit();
                    }
                    catch (Exception)
                    {
                        result = false;
                    }
                }
            }

            ReDrawScreen(document);
            return result;
        }

        #endregion

        #region Private methods

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
        private static List<IAcadGeometryObject> GetAllObjectsByIdList(List<ObjectId> objectIds, Database db)
        {
            var result = new List<IAcadGeometryObject>();
            if (objectIds == null)
                return result;

            using (var tr = db.TransactionManager.StartOpenCloseTransaction())
            {
                foreach (var objectId in objectIds.Where(x => x.ObjectClass.DxfName.AsObjectType() != ObjectType.Unknown))
                {
                    switch (objectId.ObjectClass.DxfName.AsObjectType())
                    {
                        case ObjectType.Line:
                            var line = tr.GetTypedObject<Line>(objectId, OpenMode.ForRead);
                            result.Add(AcadObjectVmFactory.GetAcadObjectVm(line));
                            break;
                        case ObjectType.Point:
                            var point = tr.GetTypedObject<DBPoint>(objectId, OpenMode.ForRead);
                            result.Add(AcadObjectVmFactory.GetAcadObjectVm(point));
                            break;
                        case ObjectType.Circle:
                            var circle = tr.GetTypedObject<Circle>(objectId, OpenMode.ForRead);
                            result.Add(AcadObjectVmFactory.GetAcadObjectVm(circle));
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
        /// Обновляет объект типа слой
        /// </summary>
        /// <param name="acadObject"></param>
        /// <param name="entity"></param>
        private static void UpdateAcadObject(ref LayerTableRecord acadObject, AcadLayerVm entity)
        {
            acadObject.Color = entity.Color.ToAcadColor();
            if (!entity.IsNotZeroLayer)
                acadObject.Name = entity.Name;
            acadObject.IsOff = entity.IsHidden;
        }

        /// <summary>
        /// Обновляет объект типа точка
        /// </summary>
        /// <param name="acadObject"></param>
        /// <param name="entity"></param>
        private static void UpdateAcadObject(ref DBPoint acadObject, AcadPointVm entity)
        {
            acadObject.Thickness = entity.Thickness;
            acadObject.Position = entity.Coordinate.ToPoint3D();
        }

        /// <summary>
        /// Обновляет объект типа окружность
        /// </summary>
        /// <param name="acadObject"></param>
        /// <param name="entity"></param>
        private static void UpdateAcadObject(ref Circle acadObject, AcadCircleVm entity)
        {
            acadObject.Radius = entity.Radius;
            acadObject.Thickness = entity.Thickness;
            acadObject.Center = entity.CenterCoordinate.ToPoint3D();
        }

        /// <summary>
        /// Обновляет объекти типа линия
        /// </summary>
        /// <param name="acadObject"></param>
        /// <param name="entity"></param>
        private static void UpdateAcadObject(ref Line acadObject, AcadLineVm entity)
        {
            acadObject.Thickness = entity.Thickness;
            acadObject.StartPoint = entity.StartCoordinate.ToPoint3D();
            acadObject.EndPoint = entity.EndCoordinate.ToPoint3D();
        }

        private static void ReDrawScreen(Document doc)
        {
            AcadApplication.UpdateScreen();
            doc.Editor.UpdateScreen();
            doc.Editor.Regen();
        }

        #endregion
    }
}
