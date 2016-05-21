using AcadPluginTest.ViewModel.Entities.Interfaces;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    /// <summary>
    /// Класс для хранения координат объектов
    /// </summary>
    public class IsoCoordinate : ObservableObject, IIsoCoordinate
    {
        #region Constructors
        public IsoCoordinate(double x, double y, double z)
        {
            XCoordinate = x;
            YCoordinate = y;
            ZCoordinate = z;
        }
        #endregion

        #region Fields
        
        private double _xCoordinate;
        private double _yCoordinate;
        private double _zCoordinate;

        #endregion

        #region Properties

        public event CoordinateChangedEventHandler CoordianteChangedEvent;

        public delegate void CoordinateChangedEventHandler();

        /// <summary>
        /// Координата по ОСИ X
        /// </summary>
        public double XCoordinate
        {
            get { return _xCoordinate; }
            set
            {
                Set(() => XCoordinate, ref _xCoordinate, value);

                if (CoordianteChangedEvent != null)
                    CoordianteChangedEvent();

            }
        }

        /// <summary>
        /// Координата по ОСИ Y
        /// </summary>
        public double YCoordinate
        {
            get { return _yCoordinate; }
            set
            {
                Set(() => YCoordinate, ref _yCoordinate, value);
                
                if (CoordianteChangedEvent != null)
                    CoordianteChangedEvent();
            }
        }

        /// <summary>
        /// Координата по ОСИ Z
        /// </summary>
        public double ZCoordinate
        {
            get { return _zCoordinate; }
            set
            {
                Set(() => ZCoordinate, ref _zCoordinate, value);

                if (CoordianteChangedEvent != null)
                    CoordianteChangedEvent();
            }
        }

        #endregion
        
    }
}