using AcadPluginTest.Model.Interfaces;
using Autodesk.AutoCAD.MacroRecorder;

namespace AcadPluginTest.Model.Implementations
{
    public class IsoCoordinate : IIsoCoordinate
    {
        public IsoCoordinate(double x, double y, double z)
        {
            XCoordinate = x;
            YCoordinate = y;
            ZCoordinate = z;
        }

        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public double ZCoordinate { get; set; }
    }
}