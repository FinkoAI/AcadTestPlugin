using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadPointVm : ViewModelBase, IAcadObject
    {
        public AcadPointVm(ObjectId id, DBPoint point)
        {
            var coordinate = point.Position.ToIsoCoordinates();

            Id = id;
            Name = "Точка";
            IsModidified = false;
            AcadObjectType = ObjectType.Point;

            Coordinate = coordinate;
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool IsModidified { get; set; }
        public ObjectType AcadObjectType { get; set; }

        public IsoCoordinate Coordinate { get; set; }
    }
}
