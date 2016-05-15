using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadCircleVm : ViewModelBase, IAcadObject
    {
        public AcadCircleVm(ObjectId id, Circle circle)
        {
            var center = circle.Center.ToIsoCoordinates();

            Id = id;
            Name = "Окружность";
            IsModidified = false;
            AcadObjectType = ObjectType.Circle;
            CenterCoordinate = center;
            Radius = circle.Radius;
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public bool IsModidified { get; set; }
        public ObjectType AcadObjectType { get; set; }

        public IsoCoordinate CenterCoordinate { get; set; }
        public double Radius { get; set; }

    }
}
