using AcadPluginTest.Enums;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Implementations.Base;
using Autodesk.AutoCAD.DatabaseServices;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadPointVm : BaseAcadGeometryObject
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        

        public IsoCoordinate Coordinate { get; set; }
    }
}
