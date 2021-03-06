﻿using AcadPluginTest.ViewModel.Entities.Implementations.Base;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    /// <summary>
    /// ViewModel для объекта типа окружность
    /// </summary>
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
                    SetModified();
                Set(() => Radius, ref _radius, value);

            }
        }

        /// <summary>
        /// Координаты центра окружности
        /// </summary>
        public IsoCoordinate CenterCoordinate { get; set; }

        #endregion

    }
}
