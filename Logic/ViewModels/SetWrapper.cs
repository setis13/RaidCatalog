using HellHades.ArtifactExtractor.Models;
using System.Windows.Media.Imaging;

namespace RaidCatalog.Logic.ViewModels {
    public class SetWrapper : BaseViewModel {

        public SetWrapper(ArtifactSet set) {
            this.Set = set;
        }

        public ArtifactSet Set { get; }

        public BitmapImage Image { get; set; }
    }
}
