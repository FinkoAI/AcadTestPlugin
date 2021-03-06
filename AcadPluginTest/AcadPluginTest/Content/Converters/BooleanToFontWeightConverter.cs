﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AcadPluginTest.Content.Converters
{
    /// <summary>
    /// Выбирает гарнитуру шрифта в зависимости от наличия изменей в объекте
    /// </summary>
    class BooleanToFontWeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isModified = (bool) value;

            return isModified ? FontWeights.Bold : FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
