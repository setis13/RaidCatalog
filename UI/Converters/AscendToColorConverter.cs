using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RaidCatalog.Converters {
    public class AscendToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (Object.Equals(value, true)) {
                return Brushes.Red;
            } else {
                return Brushes.Yellow;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
