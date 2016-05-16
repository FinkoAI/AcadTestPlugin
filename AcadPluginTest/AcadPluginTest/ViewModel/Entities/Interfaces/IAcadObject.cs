using AcadPluginTest.Enums;
using Autodesk.AutoCAD.DatabaseServices;

namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    public interface IAcadObject
    {
        ObjectId Id { get; set; }
        string Name { get; set; }
        bool IsModified { get; set; }
        ObjectType AcadObjectType { get; set; }
    }
}
