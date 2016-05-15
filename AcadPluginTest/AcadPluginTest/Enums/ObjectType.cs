using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcadPluginTest.Enums
{
    public enum ObjectType
    {
        [Description("POINT")]
        Point,
        
        [Description("LAYER")]
        Layer,

        [Description("LINE")]
        Line,

        [Description("CIRCLE")]
        Circle,

        Unknown
    }
}
