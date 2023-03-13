using HellHades.ArtifactExtractor.Models;

namespace RaidCatalog.Logic.Converters {
    public static class ArtifactKindToOrderConverter {
        public static int Convert(ArtifactKind kind) {
            switch (kind) {
                case ArtifactKind.Weapon:
                    return 1;
                case ArtifactKind.Helmet:
                    return 2;
                case ArtifactKind.Shield:
                    return 3;
                case ArtifactKind.Gloves:
                    return 4;
                case ArtifactKind.Chest:
                    return 5;
                case ArtifactKind.Boots:
                    return 6;
                case ArtifactKind.Ring:
                    return 7;
                case ArtifactKind.Cloak:
                    return 8;
                case ArtifactKind.Banner:
                    return 9;
                default:
                    return 10;
            }
        }
    }
}
