using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Implementations.Base;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadCircleVm :  BaseAcadGeometryObject
    {
        #region Private fields

        
        private double _radius;
        private double _thickness;

        #endregion

        #region Properties

        /// <summary>
        /// Радиус окружности
        /// </summary>
        public double Radius
        {
            get { return _radius; }
            set { Set(() => Radius, ref _radius, value); }
        }

        /// <summary>
        /// Высота 3D
        /// </summary>
        public double Thickness
        {
            get { return _thickness; }
            set { Set(() => Thickness, ref _thickness, value); }
        }

        public ObjectType AcadObjectType { get; set; }

        public IsoCoordinate CenterCoordinate { get; set; }

        #endregion

    }
}
