using System.ComponentModel;
using AcadPluginTest.Enums;
using Autodesk.AutoCAD.DatabaseServices;

namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    /// <summary>
    /// Интерфейс для всех объектов
    /// </summary>
    public interface IAcadObject : IDataErrorInfo
    {
        ObjectId Id { get; set; }
        string Name { get; set; }
        bool IsModified { get; set; }
        bool IsSelected { get; set; }
        bool IsValid { get; }
        ObjectType AcadObjectType { get; set; }
    }
}
