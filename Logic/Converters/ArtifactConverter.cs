using HellHades.ArtifactExtractor.Models;
using RaidCatalog.Logic.ViewModels;

namespace RaidCatalog.Logic.Converters {
    public static class ArtifactConverter {
        public static ArtifactViewModel Convert(Artifact source) {
            return new ArtifactViewModel() {
                Id = source.Id,
                Level = source.Level,
                IsActivated = source.IsActivated,
                Set = source.Set,
                Rank = source.Rank,
                Rarity = source.Rarity,
                Kind = source.Kind,
                PrimaryBonus = source.PrimaryBonus,
                SecondaryBonuses = source.SecondaryBonuses,
                RequiredFraction = source.RequiredFraction,
                RerollsCount = source.RerollsCount,
                AscendLevel = source.AscendLevel,
                AscendBonus = source.AscendBonus,
            };
        }
    }
}
