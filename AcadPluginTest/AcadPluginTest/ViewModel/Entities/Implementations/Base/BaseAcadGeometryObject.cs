using AcadPluginTest.ViewModel.Entities.Interfaces;

namespace AcadPluginTest.ViewModel.Entities.Implementations.Base
{
    /// <summary>
    /// Базовый класс для геометрических объектов чертежа(линия, точка, окружность)
    /// </summary>
    public class BaseAcadGeometryObject : BaseAcadObject, IAcadGeometryObject
    {
        #region Fields

        private double _thisckness;

        #endregion

        #region Properties

        /// <summary>
        /// Высота 3D
        /// </summary>
        public double Thickness
        {
            get { return _thisckness; }
            set
            {
                if (_thisckness != value)
                    SetModified();

                Set(() => Thickness, ref _thisckness, value);
            }
        }

        #endregion
        
        
    }
}
