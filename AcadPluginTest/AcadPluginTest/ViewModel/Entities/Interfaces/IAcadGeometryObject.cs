using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    public interface IAcadGeometryObject : IAcadObject
    {
        double Thickness { get; set; }
    }
}
