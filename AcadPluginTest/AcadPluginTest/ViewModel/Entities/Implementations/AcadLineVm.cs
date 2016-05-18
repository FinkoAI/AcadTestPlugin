using AcadPluginTest.ViewModel.Entities.Implementations.Base;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadLineVm : BaseAcadGeometryObject
    {
        #region Fields
        #endregion

        #region Properties
        
        public IsoCoordinate StartCoordinate { get; set; }
        public IsoCoordinate EndCoordinate { get; set; }
        
        #endregion

    }
}
