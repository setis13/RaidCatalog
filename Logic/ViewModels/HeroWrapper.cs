using HellHades.ArtifactExtractor.Models;
using System.Windows.Media.Imaging;

namespace RaidCatalog.Logic.ViewModels {
    public class HeroWrapper : BaseViewModel {

        public HeroWrapper(Hero hero, HeroType heroType) {
            this.Hero = hero;
            this.HeroType = heroType;
        }

        public Hero Hero { get; }
        public HeroType HeroType { get; }

        public BitmapImage Image { get; set; }

        private bool visible;

        public bool Visible {
            get => visible; set {
                visible = value;
                OnPropertyChanged();
            }
        }
    }
}
