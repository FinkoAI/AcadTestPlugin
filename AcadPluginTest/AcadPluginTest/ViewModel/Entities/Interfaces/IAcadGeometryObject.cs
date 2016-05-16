using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    interface IAcadGeometryObject : IAcadObject
    {
        double Thickness { get; set; }
    }
}
