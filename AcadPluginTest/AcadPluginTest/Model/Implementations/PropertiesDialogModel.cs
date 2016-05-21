using System.Collections.Generic;
using System.Linq;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Interfaces;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;

namespace AcadPluginTest.Model.Implementations
{
    /// <summary>
    /// Модель для взаимодействия с документом (получение, сохранение данных)
    /// </summary>
    public class PropertiesDialogModel : IPropertiesDialogModel
    {
        private readonly Document _document;

        public Document Document
        {
            get { return _document; }
        }

        public PropertiesDialogModel(Document document)
        {
            _document = document;
        }

        /// <summary>
        /// Пытается сохранить данные
        /// </summary>
        /// <param name="layersList">Дерево объектов</param>
        /// <returns>Возвращает true - если изменеия сохранились, в противном случае false</returns>
        public bool TrySaveChanges(List<ILayerObject> layersList)
        {
            var modifiedObjects = AcadHelper.GetFlatElementsTree(layersList).Where(x => x.IsModified).ToList();
            return AcadHelper.SaveModifiedObjects(_document, modifiedObjects);
        }

        /// <summary>
        /// Проверяет данные на валидность
        /// </summary>
        /// <param name="layersList">Дерево объектов</param>
        /// <returns>Вернёт false, если отсутствует хотя бы один объект из дерева</returns>
        public bool IsDataValid(IEnumerable<ILayerObject> layersList)
        {
            return AcadHelper.IsAllObjectsExist(_document, layersList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ILayerObject> GetLayersData()
        {
            return AcadHelper.GetLayerVms(_document);
        }
    }
}
