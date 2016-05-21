using AcadPluginTest.ViewModel.Entities.Implementations.Base;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    /// <summary>
    /// ViewModel для объета типа точка
    /// </summary>
    public class AcadPointVm : BaseAcadGeometryObject
    {
        #region Fields
        #endregion

        #region Properties

        /// <summary>
        /// Координаты точки
        /// </summary>
        public IsoCoordinate Coordinate { get; set; }

        #endregion
    }
}
