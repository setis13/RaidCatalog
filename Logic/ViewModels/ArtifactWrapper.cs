using HellHades.ArtifactExtractor.Models;
using RaidCatalog.Logic.Converters;
using System;
using System.Windows.Media.Imaging;

namespace RaidCatalog.Logic.ViewModels {
    public class ArtifactWrapper : BaseViewModel {

        public ArtifactWrapper(Artifact artifact) {
            this.Artifact = ArtifactConverter.Convert(artifact);
            this.Group = ArtifactSetToOrderConverter.Convert(artifact.Set, artifact.RequiredFraction);
        }

        private SetWrapper set;
        private HeroWrapper hero;
        private bool visible = true;
        private bool low;
        private bool ascend;

        public int Group { get; }
        public ArtifactViewModel Artifact { get; }

        public BitmapImage Image { get; set; }

        public int? WebId { get; set; }
        public short Order { get; set; }
        public string Comment { get; set; }
        public bool Ascend {
            get => ascend;
            set {
                ascend = value;
                OnPropertyChanged();
            }
        }
        public bool Low {
            get => low;
            set {
                low = value;
                OnPropertyChanged();
            }
        }
        public byte? Tag { get; set; }


        public bool Visible {
            get => visible; 
            set {
                visible = value;
                OnPropertyChanged();
            }
        }

        public SetWrapper Set {
            get => set;
            set {
                set = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HeroSet));
            }
        }
        public HeroWrapper Hero {
            get => hero;
            set {
                hero = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HeroSet));
            }
        }
        public bool HeroSet => Hero != null || Set != null;

        public override string ToString() {
            return $"Kind:{Artifact.Kind}\nSet:{Artifact.Set}\nFraction{Artifact.RequiredFraction}\nId:{Artifact.Id}";
        }
    }
}
