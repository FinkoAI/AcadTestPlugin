using AcadPluginTest.Enums;
using AcadPluginTest.Helpers;
using AcadPluginTest.Model.Implementations;
using AcadPluginTest.ViewModel.Entities.Implementations.Base;
using AcadPluginTest.ViewModel.Entities.Interfaces;
using Autodesk.AutoCAD.DatabaseServices;
using GalaSoft.MvvmLight;

namespace AcadPluginTest.ViewModel.Entities.Implementations
{
    public class AcadLineVm : BaseAcadGeometryObject
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        

        public IsoCoordinate StartCoordinate { get; set; }
        public IsoCoordinate EndCoordinate { get; set; }
    }
}
