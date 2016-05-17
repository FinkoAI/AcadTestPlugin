using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using AcadPluginTest.Enums;

namespace AcadPluginTest.Content.Converters
{
    /// <summary>
    /// Получает изображение в зависимости от типа объекта
    /// </summary>
    public class ObjectTypeToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path;
            var item = value is ObjectType ? (ObjectType) value : ObjectType.Unknown;

            switch (item)
            {
                case ObjectType.Circle:
                    path = "/Content/Images/circle_50px.png";
                    break;
                case ObjectType.Layer:
                    path = "/Content/Images/dots_50px.png";
                    break;
                case ObjectType.Point:
                    path = "/Content/Images/dots_50px.png";
                    break;
                case ObjectType.Line:
                    path = "/Content/Images/line_50px.png";
                    break;
                case ObjectType.Unknown:
                    path = "/Content/Images/unknown.png";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new BitmapImage(new Uri("/AcadPluginTest;component" + path, UriKind.Relative));
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
