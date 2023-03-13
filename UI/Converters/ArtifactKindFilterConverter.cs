using HellHades.ArtifactExtractor.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RaidCatalog.Converters {
    public class ArtifactKindFilterConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if ((bool)value == false) {
                switch ((ArtifactKind)parameter) {
                    case ArtifactKind.Helmet:
                        return "/Resources/Images/Filters/Helmet.png";
                    case ArtifactKind.Chest:
                        return "/Resources/Images/Filters/Chest.png";
                    case ArtifactKind.Gloves:
                        return "/Resources/Images/Filters/Gloves.png";
                    case ArtifactKind.Boots:
                        return "/Resources/Images/Filters/Boots.png";
                    case ArtifactKind.Weapon:
                        return "/Resources/Images/Filters/Weapon.png";
                    case ArtifactKind.Shield:
                        return "/Resources/Images/Filters/Shield.png";
                    case ArtifactKind.Ring:
                        return "/Resources/Images/Filters/Ring.png";
                    case ArtifactKind.Cloak:
                        return "/Resources/Images/Filters/Cloak.png";
                    case ArtifactKind.Banner:
                        return "/Resources/Images/Filters/Banner.png";
                }
            } else {
                switch ((ArtifactKind)parameter) {
                    case ArtifactKind.Helmet:
                        return "/Resources/Images/Filters/Helmet_On.png";
                    case ArtifactKind.Chest:
                        return "/Resources/Images/Filters/Chest_On.png";
                    case ArtifactKind.Gloves:
                        return "/Resources/Images/Filters/Gloves_On.png";
                    case ArtifactKind.Boots:
                        return "/Resources/Images/Filters/Boots_On.png";
                    case ArtifactKind.Weapon:
                        return "/Resources/Images/Filters/Weapon_On.png";
                    case ArtifactKind.Shield:
                        return "/Resources/Images/Filters/Shield_On.png";
                    case ArtifactKind.Ring:
                        return "/Resources/Images/Filters/Ring_On.png";
                    case ArtifactKind.Cloak:
                        return "/Resources/Images/Filters/Cloak_On.png";
                    case ArtifactKind.Banner:
                        return "/Resources/Images/Filters/Banner_On.png";
                }
            }
            throw new Exception("ArtifactKindFilterConverter [1]");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
