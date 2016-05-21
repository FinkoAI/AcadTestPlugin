using AcadPluginTest.ViewModel.Entities.Implementations.Base;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    /// <summary>
    /// ViewModel для объекта линия
    /// </summary>
    public class AcadLineVm : BaseAcadGeometryObject
    {
        #region Fields
        #endregion

        #region Properties
        
        /// <summary>
        /// Координаты начала
        /// </summary>
        public IsoCoordinate StartCoordinate { get; set; }
        
        /// <summary>
        /// Координаты конца
        /// </summary>
        public IsoCoordinate EndCoordinate { get; set; }
        
        #endregion

    }
}
