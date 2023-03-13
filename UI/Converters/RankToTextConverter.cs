using HellHades.ArtifactExtractor.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace RaidCatalog.Converters {
    public class RankToTextConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            switch ((Grade)value) {
                case Grade.OneStar:
                    return "1★";
                case Grade.TwoStars:
                    return "2★";
                case Grade.ThreeStars:
                    return "3★";
                case Grade.FourStars:
                    return "4★";
                case Grade.FiveStars:
                    return "5★";
                case Grade.SixStars:
                    return "6★";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
