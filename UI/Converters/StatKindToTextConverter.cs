using HellHades.ArtifactExtractor.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RaidCatalog.Converters {
    public class StatKindToTextConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch ((StatKind)value) {
                case StatKind.Health:
                    return "Здр";
                case StatKind.Attack:
                    return "Атк";
                case StatKind.Defense:
                    return "Зщт";
                case StatKind.Speed:
                    return "Скр";
                case StatKind.Resistance:
                    return "Сопр";
                case StatKind.Accuracy:
                    return "Метк";
                case StatKind.CriticalChance:
                    return "Крит.ш";
                case StatKind.CriticalDamage:
                    return "Крит.ур";
                default:
                    return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
