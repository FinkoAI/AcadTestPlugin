namespace AcadPluginTest.ViewModel.Entities.Interfaces
{
    /// <summary>
    /// Интерфейс для класса хранения координат
    /// </summary>
    public interface IIsoCoordinate
    {
        double XCoordinate { get; set; }
        double YCoordinate { get; set; }
        double ZCoordinate { get; set; }
    }
}
