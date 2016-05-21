namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    /// <summary>
    /// Интерфейс для геометрических объектов
    /// </summary>
    public interface IAcadGeometryObject : IAcadObject
    {
        double Thickness { get; set; }
    }
}
