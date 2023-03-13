using HellHades.ArtifactExtractor.Models;

namespace RaidCatalog.Logic.ViewModels {
    public class ArtifactViewModel : BaseViewModel {
        private int level;
        private bool isActivated;

        public int Id { get; set; }
        public int Level {
            get => level;
            set {
                level = value;
                OnPropertyChanged();
            }
        }
        public bool IsActivated {
            get => isActivated;
            set { 
                isActivated = value;
                OnPropertyChanged();
            }
        }
        public ArtifactSet Set { get; set; }
        public Grade Rank { get; set; }
        public Rarity Rarity { get; set; }
        public ArtifactKind Kind { get; set; }
        public ArtifactBonus PrimaryBonus { get; set; }
        public List<ArtifactBonus> SecondaryBonuses { get; set; } = new List<ArtifactBonus>();
        public HeroFraction RequiredFraction { get; set; }
        public int? RerollsCount { get; set; }
        public int? AscendLevel { get; set; }
        public ArtifactBonus? AscendBonus { get; set; }
    }
}
