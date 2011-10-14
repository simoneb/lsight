using System;
using System.Globalization;
using System.Windows.Data;
using lsight.Model;

namespace lsight.Converters
{
    public class OffsetConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return LogOffset.Parse(value.ToString());
        }
    }
}