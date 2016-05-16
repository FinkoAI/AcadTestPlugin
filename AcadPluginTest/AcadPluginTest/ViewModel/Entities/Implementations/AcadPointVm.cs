using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadPointVm : ViewModelBase, IAcadGeometryObject
    {
        public AcadPointVm(ObjectId id, DBPoint point)
        {
            var coordinate = point.Position.ToIsoCoordinates();

            Id = id;
            IsModified = false;
            AcadObjectType = ObjectType.Point;
            Thickness = point.Thickness;
            Coordinate = coordinate;

            Name = string.Format("Точка {0}", Coordinate.ToCoordinateString());
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool IsModified { get; set; }
        public ObjectType AcadObjectType { get; set; }

        public IsoCoordinate Coordinate { get; set; }
        public double Thickness { get; set; }
    }
}
