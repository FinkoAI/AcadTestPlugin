using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    /// <summary>
    /// Интерфейс для объекта слоя
    /// </summary>
    public interface ILayerObject : IAcadObject
    {
        Color Color { get; set; }
        bool IsHidden { get; set; }
        bool IsNotZeroLayer { get; set; }

        ObservableCollection<IAcadGeometryObject> Objects { get; set; }
    }
}
