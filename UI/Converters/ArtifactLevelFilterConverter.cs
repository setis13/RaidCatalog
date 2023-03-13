using HellHades.ArtifactExtractor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RaidCatalog.Converters {
    public class ArtifactLevelFilterConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if ((bool)value == false) {
                switch ((int)parameter) {
                    case 9:
                        return "/Resources/Images/Filters/Level9.png";
                    case 10:
                        return "/Resources/Images/Filters/Level10.png";
                    case 11:
                        return "/Resources/Images/Filters/Level11.png";
                    case 12:
                        return "/Resources/Images/Filters/Level12.png";
                    case 13:
                        return "/Resources/Images/Filters/Level13.png";
                    case 14:
                        return "/Resources/Images/Filters/Level14.png";
                    case 15:
                        return "/Resources/Images/Filters/Level15.png";
                    case 16:
                        return "/Resources/Images/Filters/Level16.png";
                }
            } else {
                switch ((int)parameter) {
                    case 9:
                        return "/Resources/Images/Filters/Level9_On.png";
                    case 10:
                        return "/Resources/Images/Filters/Level10_On.png";
                    case 11:
                        return "/Resources/Images/Filters/Level11_On.png";
                    case 12:
                        return "/Resources/Images/Filters/Level12_On.png";
                    case 13:
                        return "/Resources/Images/Filters/Level13_On.png";
                    case 14:
                        return "/Resources/Images/Filters/Level14_On.png";
                    case 15:
                        return "/Resources/Images/Filters/Level15_On.png";
                    case 16:
                        return "/Resources/Images/Filters/Level16_On.png";
                }
            }
            throw new Exception("ArtifactKindLevelConverter [1]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
