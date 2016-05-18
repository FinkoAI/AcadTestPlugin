using System.Collections.Generic;
using System.Collections.ObjectModel;
using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.ViewModel.Entities.Implementations;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;

namespace AcadPluginTest.Factories
{
    /// <summary>
    /// Фабрика для генерации ViewModel'ей объектов автокада
    /// </summary>
    public static class AcadObjectVmFactory
    {
        /// <summary>
        /// Формирует ViewModel для окружности
        /// </summary>
        /// <param name="circle"></param>
        /// <returns></returns>
        public static IAcadGeometryObject GetAcadObjectVm(Circle circle)
        {
            var center = circle.Center.ToIsoCoordinates();

            var vm = new AcadCircleVm
            {
                Id = circle.ObjectId,
                Name = string.Format("Окружность {0}", center.ToCoordinateString()),
                AcadObjectType = ObjectType.Circle,
                CenterCoordinate = center,
                Radius = circle.Radius,
                Thickness = circle.Thickness
            };

            vm.CenterCoordinate.CoordianteChangedEvent += vm.SetModified;
            vm.IsModified = false;

            return vm;
        }

        /// <summary>
        /// Формирует ViewModel для линии
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static IAcadGeometryObject GetAcadObjectVm(Line line)
        {
            var startPoint = line.StartPoint.ToIsoCoordinates();
            var endPoint = line.EndPoint.ToIsoCoordinates();

            var vm = new AcadLineVm
            {
                Id = line.ObjectId,
                Name = string.Format("Линия {0} => {1}", startPoint.ToCoordinateString(), endPoint.ToCoordinateString()),
                AcadObjectType = ObjectType.Line,
                StartCoordinate = startPoint,
                EndCoordinate = endPoint,
                Thickness = line.Thickness
            };

            vm.StartCoordinate.CoordianteChangedEvent += vm.SetModified;
            vm.EndCoordinate.CoordianteChangedEvent += vm.SetModified;
            vm.IsModified = false;

            return vm;
        }

        /// <summary>
        /// Формирует ViewModel для точки
        /// </summary>
        /// <param name="acadPoint"></param>
        /// <returns></returns>
        public static IAcadGeometryObject GetAcadObjectVm(DBPoint acadPoint)
        {
            var point = acadPoint.Position.ToIsoCoordinates();

            var vm = new AcadPointVm
            {
                Id = acadPoint.ObjectId,
                Name = string.Format("Точка {0}", point.ToCoordinateString()),
                AcadObjectType = ObjectType.Point,
                Coordinate = point,
                Thickness = acadPoint.Thickness
            };

            vm.Coordinate.CoordianteChangedEvent += vm.SetModified;
            vm.IsModified = false;

            return vm;
        }

        /// <summary>
        /// Формирует ViewModel для слоя с его объектами
        /// </summary>
        /// <param name="acadLayer"></param>
        /// <param name="acadGeometryObjects"></param>
        /// <returns></returns>
        public static IAcadObject GetAcadObjectVm(LayerTableRecord acadLayer, IEnumerable<IAcadGeometryObject> acadGeometryObjects)
        {
            var vm = new AcadLayerVm
            {
                Id = acadLayer.ObjectId,
                Name = acadLayer.Name,
                Color = acadLayer.Color.ToSystemColor(),
                AcadObjectType = ObjectType.Layer,
                IsHidden = acadLayer.IsOff,
                Objects = new ObservableCollection<IAcadGeometryObject>(acadGeometryObjects)
            };

            vm.IsModified = false;

            return vm;
        }
    }
}
