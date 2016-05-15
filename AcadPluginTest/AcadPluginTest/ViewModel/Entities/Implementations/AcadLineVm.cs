using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadLineVm : ViewModelBase, IAcadObject
    {
        public AcadLineVm(ObjectId id, Line line)
        {
            Id = id;
            Name ="Линия";
            IsModidified = false;
            AcadObjectType = ObjectType.Line;
            StartCoordinate = line.StartPoint.ToIsoCoordinates();
            EndCoordinate = line.EndPoint.ToIsoCoordinates();
        }


        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool IsModidified { get; set; }
        public ObjectType AcadObjectType { get; set; }

        public IsoCoordinate StartCoordinate { get; set; }
        public IsoCoordinate EndCoordinate { get; set; }
    }
}
