﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RaidCatalog.Converters {
    public class InvertedBooleanToVisibilityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            return value.Equals(false) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
