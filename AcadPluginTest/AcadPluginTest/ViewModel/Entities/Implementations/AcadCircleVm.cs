using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.ViewModel.Entities.Implementations.Base;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadCircleVm : BaseAcadGeometryObject
    {
        #region Private fields

        private double _radius;

        #endregion

        #region Properties

        /// <summary>
        /// Радиус окружности
        /// </summary>
        public double Radius
        {
            get { return _radius; }
            set
            {
                if (_radius != value)
                    IsModified = true;
                Set(() => Radius, ref _radius, value);

            }
        }

        public IsoCoordinate CenterCoordinate { get; set; }

        #endregion

    }
}
