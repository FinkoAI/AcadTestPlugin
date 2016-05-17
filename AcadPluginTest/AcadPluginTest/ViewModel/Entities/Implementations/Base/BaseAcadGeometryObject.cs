using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcadPluginTest.ViewModel.Entities.Interfaces;

namespace AcadPluginTest.ViewModel.Entities.Implementations.Base
{
    public class BaseAcadGeometryObject : BaseAcadObject, IAcadGeometryObject
    {
        #region Fields

        private double _thisckness;

        #endregion

        #region Properties

        #endregion
        /// <summary>
        /// Высота 3D
        /// </summary>
        public double Thickness
        {
            get { return _thisckness; }
            set { Set(() => Thickness, ref _thisckness, value); }
        }
    }
}
