namespace RaidCatalog.Logic.ViewModels {
    public class SetsViewModel : BaseViewModel {
        private SetWrapper selectedSet;

        public SetWrapper SelectedSet {
            get => selectedSet;
            set {
                selectedSet = value;
                OnPropertyChanged();
            }
        }

        public List<SetWrapper> Sets { get; set; }

        public SetsViewModel(List<SetWrapper> sets) {
            this.Sets = sets;
        }
    }
}
