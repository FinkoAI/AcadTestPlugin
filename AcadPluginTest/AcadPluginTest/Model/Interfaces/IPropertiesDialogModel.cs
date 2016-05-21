using System.Collections.Generic;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.ApplicationServices;

namespace AcadPluginTest.Model.Interfaces
{
    /// <summary>
    /// Интерфейс модели панели редактирования примитивов
    /// </summary>
    public interface IPropertiesDialogModel
    {
        Document Document { get; }
        bool TrySaveChanges(List<ILayerObject> layersList);
        bool IsDataValid(IEnumerable<ILayerObject> layersList);
        IEnumerable<ILayerObject> GetLayersData();
    }
}
