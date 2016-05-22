using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace AcadPluginTest.Content.Converters
{
    /// <summary>
    /// Получает цвет отображения объекта в дереве в зависимости от наличия ошибок
    /// </summary>
    public class IsValidToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isValid = (bool) value;

            var color = !isValid ? Color.FromRgb(255, 0, 0) : Color.FromRgb(0, 0, 0);

            return new SolidColorBrush(color);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
