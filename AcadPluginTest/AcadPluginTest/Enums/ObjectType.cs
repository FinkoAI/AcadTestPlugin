using System.ComponentModel;

namespace AcadPluginTest.Enums
{
    /// <summary>
    /// Перечисление типов объектов
    /// Соответствует свойству ObjectId.ObjectClass.DxfName
    /// </summary>
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
