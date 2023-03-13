using System;
using System.Globalization;
using System.Windows.Data;

namespace RaidCatalog.Converters {
    public class PercentConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return (double)value == 1.0f ? "MAX" : (int)(100 * (double)value) + "%";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
