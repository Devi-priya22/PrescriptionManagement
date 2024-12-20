using System;
using System.Globalization;
using System.Windows.Data;

namespace PrescriptionManagement.Converter
{
    internal class Coverterr : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
