using HellHades.ArtifactExtractor.Models;
using RaidCatalog.Logic.Converters;
using System.Collections.ObjectModel;

namespace RaidCatalog.Logic.ViewModels {
    public class ArtifactSetWrapper {

        public int Group { get; }
        public ArtifactSet Set { get; }
        public HeroFraction Fraction { get; }
        public string Name => Fraction != HeroFraction.None ? Fraction.ToString() : Set.ToString();


        public ArtifactSetWrapper(ArtifactSet set, HeroFraction fraction) {
            this.Group = ArtifactSetToOrderConverter.Convert(set, fraction);
            this.Set = set;
            this.Fraction = fraction;
        }

        public ObservableCollection<ArtifactWrapper> Artifacts { get; set; } = new ObservableCollection<ArtifactWrapper>();
    }
}
