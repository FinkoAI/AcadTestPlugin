

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    public interface ILayerObject : IAcadObject
    {
        Color Color { get; set; }
        bool IsHidden { get; set; }

        ObservableCollection<IAcadGeometryObject> Objects { get; set; }
    }
}
