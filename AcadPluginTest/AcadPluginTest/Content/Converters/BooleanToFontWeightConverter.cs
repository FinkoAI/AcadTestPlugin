using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AcadPluginTest.Content.Converters
{
    class BooleanToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hasModified = (bool) value;

            return hasModified ? FontWeights.Bold : FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
