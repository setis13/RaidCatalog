using HellHades.ArtifactExtractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidCatalog.Logic.ViewModels {
    public class HeroesViewModel : BaseViewModel {
        private HeroWrapper selectedHero;

        public HeroWrapper SelectedHero {
            get => selectedHero;
            set {
                selectedHero = value;
                OnPropertyChanged();
            }
        }

        public List<HeroWrapper> Heroes { get; set; }

        public HeroesViewModel(List<HeroWrapper> heroes) {
            this.Heroes = heroes
                .Where(e => e.HeroType.Rarity >= Rarity.Epic)
                .OrderBy(e => e.HeroType.Fraction)
                .ThenBy(e => e.HeroType.Rarity)
                .ThenBy(e => e.HeroType.Name)
                .ToList();
        }
    }
}
