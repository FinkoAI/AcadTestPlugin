using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadLineVm : ViewModelBase, IAcadGeometryObject
    {
        public AcadLineVm(ObjectId id, Line line)
        {
            Id = id;
            IsModified = false;
            AcadObjectType = ObjectType.Line;
            StartCoordinate = line.StartPoint.ToIsoCoordinates();
            EndCoordinate = line.EndPoint.ToIsoCoordinates();
            Thickness = line.Thickness;

            Name = string.Format("Линия {0} => {1}", StartCoordinate.ToCoordinateString(), EndCoordinate.ToCoordinateString());
        }


        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool IsModified { get; set; }
        public ObjectType AcadObjectType { get; set; }

        public IsoCoordinate StartCoordinate { get; set; }
        public IsoCoordinate EndCoordinate { get; set; }
        public double Thickness { get; set; }
    }
}
