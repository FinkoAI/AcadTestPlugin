using System.Collections.Generic;
using System.Collections.ObjectModel;
using AcadPluginTest.Enums;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadLayerVm : ViewModelBase, IAcadObject
    {
        public AcadLayerVm(LayerTableRecord layer, List<IAcadObject> acadObjects)
        {
            Id = layer.ObjectId;
            Name = layer.Name;
            Color = layer.Color;
            AcadObjectType = ObjectType.Layer;
            IsModidified = false;
            IsHidden = layer.IsHidden;

            Objects = new ObservableCollection<IAcadObject>(acadObjects);
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool IsModidified { get; set; }
        public ObjectType AcadObjectType { get; set; }

        public Color Color { get; set; }
        public bool IsHidden { get; set; }
        public ObservableCollection<IAcadObject> Objects { get; set; }
    }
}
