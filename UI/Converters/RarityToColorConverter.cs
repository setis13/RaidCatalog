using HellHades.ArtifactExtractor.Models;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RaidCatalog.Converters {
    public class RarityToColorConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch ((Rarity)value) {
                case Rarity.Common:
                    return new SolidColorBrush(Colors.Gray);
                case Rarity.Uncommon:
                    return new SolidColorBrush(Colors.Green);
                case Rarity.Rare:
                    return new SolidColorBrush(Colors.DodgerBlue);
                case Rarity.Epic:
                    return new SolidColorBrush(Colors.Violet);
                case Rarity.Legendary:
                    return new SolidColorBrush(Colors.Orange);
                case Rarity.Mythical:
                    return new SolidColorBrush(Colors.Red);
                default:
                    return new SolidColorBrush(Colors.White);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
