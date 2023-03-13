using HellHades.ArtifactExtractor.Models;
using System.Collections.ObjectModel;

namespace RaidCatalog.Logic.ViewModels {
    public class MainViewModel : BaseViewModel {

        public static MainViewModel Instance { get; private set; }

        public MainViewModel() {
            Instance = this;
        }

        #region [ Favorite Filters ]

        private bool heroFavoriteFilter;
        private bool roleFavoriteFilter;
        private bool ascendFavoriteFilter;
        private bool lowFavoriteFilter;

        public bool HeroFavoriteFilter {
            get => heroFavoriteFilter;
            set {
                heroFavoriteFilter = value;
                OnPropertyChanged();
            }
        }
        public bool RoleFavoriteFilter {
            get => roleFavoriteFilter;
            set {
                roleFavoriteFilter = value;
                OnPropertyChanged();
            }
        }
        public bool AscendFavoriteFilter {
            get => ascendFavoriteFilter;
            set {
                ascendFavoriteFilter = value;
                OnPropertyChanged();
            }
        }
        public bool LowFavoriteFilter {
            get => lowFavoriteFilter;
            set {
                lowFavoriteFilter = value;
                OnPropertyChanged();
            }
        }

        #endregion [ Favorite Filters ]

        #region [ Gliph Filters ]

        private int glyphRatingFilter;
        private bool glyphAtkFilter;
        private bool glyphDefFilter;
        private bool glyphHpFilter;
        private bool glyphAtkPFilter;
        private bool glyphDefPFilter;
        private bool glyphHpPFilter;
        private bool glyphAccFilter;
        private bool glyphResFilter;
        private bool glyphSpdFilter;

        public int GlyphRatingFilter {
            get => glyphRatingFilter;
            set {
                glyphRatingFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphAtkFilter {
            get => glyphAtkFilter;
            set {
                glyphAtkFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphDefFilter {
            get => glyphDefFilter;
            set {
                glyphDefFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphHpFilter {
            get => glyphHpFilter;
            set {
                glyphHpFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphAtkPFilter {
            get => glyphAtkPFilter;
            set {
                glyphAtkPFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphDefPFilter {
            get => glyphDefPFilter;
            set {
                glyphDefPFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphHpPFilter {
            get => glyphHpPFilter;
            set {
                glyphHpPFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphAccFilter {
            get => glyphAccFilter;
            set {
                glyphAccFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphResFilter {
            get => glyphResFilter;
            set {
                glyphResFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlyphSpdFilter {
            get => glyphSpdFilter;
            set {
                glyphSpdFilter = value;
                OnPropertyChanged();
            }
        }

        #endregion [ Gliph Filters ]

        #region [ Slot Filters ]

        private bool weaponFilter;
        private bool helmetFilter;
        private bool shieldFilter;
        private bool glovesFilter;
        private bool chestFilter;
        private bool bootsFilter;
        private bool ringFilter;
        private bool cloakFilter;
        private bool bannerFilter;

        public bool WeaponFilter {
            get => weaponFilter;
            set {
                weaponFilter = value;
                OnPropertyChanged();
            }
        }
        public bool HelmetFilter {
            get => helmetFilter;
            set {
                helmetFilter = value;
                OnPropertyChanged();
            }
        }
        public bool ShieldFilter {
            get => shieldFilter;
            set {
                shieldFilter = value;
                OnPropertyChanged();
            }
        }
        public bool GlovesFilter {
            get => glovesFilter;
            set {
                glovesFilter = value;
                OnPropertyChanged();
            }
        }
        public bool ChestFilter {
            get => chestFilter;
            set {
                chestFilter = value;
                OnPropertyChanged();
            }
        }
        public bool BootsFilter {
            get => bootsFilter;
            set {
                bootsFilter = value;
                OnPropertyChanged();
            }
        }
        public bool RingFilter {
            get => ringFilter;
            set {
                ringFilter = value;
                OnPropertyChanged();
            }
        }
        public bool CloakFilter {
            get => cloakFilter;
            set {
                cloakFilter = value;
                OnPropertyChanged();
            }
        }
        public bool BannerFilter {
            get => bannerFilter;
            set {
                bannerFilter = value;
                OnPropertyChanged();
            }
        }

        #endregion [ Slot Filters ]

        #region [ Level Filters ]

        private bool level9Filter;
        private bool level10Filter;
        private bool level11Filter;
        private bool level12Filter;
        private bool level13Filter;
        private bool level14Filter;
        private bool level15Filter;
        private bool level16Filter;

        public bool Level9Filter {
            get => level9Filter;
            set {
                level9Filter = value;
                OnPropertyChanged();
            }
        }
        public bool Level10Filter {
            get => level10Filter;
            set {
                level10Filter = value;
                OnPropertyChanged();
            }
        }
        public bool Level11Filter {
            get => level11Filter;
            set {
                level11Filter = value;
                OnPropertyChanged();
            }
        }
        public bool Level12Filter {
            get => level12Filter;
            set {
                level12Filter = value;
                OnPropertyChanged();
            }
        }
        public bool Level13Filter {
            get => level13Filter;
            set {
                level13Filter = value;
                OnPropertyChanged();
            }
        }
        public bool Level14Filter {
            get => level14Filter;
            set {
                level14Filter = value;
                OnPropertyChanged();
            }
        }
        public bool Level15Filter {
            get => level15Filter;
            set {
                level15Filter = value;
                OnPropertyChanged();
            }
        }
        public bool Level16Filter {
            get => level16Filter;
            set {
                level16Filter = value;
                OnPropertyChanged();
            }
        }

        #endregion [ Level Filters ]

        #region [ Equipped Filters ]

        private bool equippedFilter;
        private bool noEquippedFilter;

        public bool EquippedFilter {
            get => equippedFilter;
            set {
                equippedFilter = value;
                OnPropertyChanged();
            }
        }
        public bool NoEquippedFilter {
            get => noEquippedFilter;
            set {
                noEquippedFilter = value;
                OnPropertyChanged();
            }
        }

        #endregion [ Equipped Filters ]

        #region [ Loading ]

        public bool artifactsLoading;
        public bool favoritesLoading;

        public bool ArtifactsLoading {
            get => artifactsLoading;
            set {
                artifactsLoading = value;
                OnPropertyChanged();
            }
        }
        public bool FavoritesLoading {
            get => favoritesLoading;
            set {
                favoritesLoading = value;
                OnPropertyChanged();
            }
        }

        #endregion [ Loading ]

        private ArtifactWrapper selection;
        public ArtifactWrapper Selection {
            get => selection;
            set {
                selection = value;
                SelectionChanging = true;
                OnPropertyChanged();
                SelectionChanging = false;
            }
        }
        public bool SelectionChanging { get; private set; }

        #region [ Errors ]

        private string errorMessage;

        public string ErrorMessage {
            get => errorMessage;
            set {
                errorMessage = value;
                OnPropertyChanged();
            }
        }

        #endregion [ Errors ]

        public List<SetWrapper> Sets { get; set; }
        public List<HeroType> HeroTypes { get; set; }
        public List<HeroWrapper> AllHeroes { get; set; }
        public List<ArtifactWrapper> AllArtifacts { get; set; }

        private ObservableCollection<ArtifactWrapper> favorites = new ObservableCollection<ArtifactWrapper>();
        public ObservableCollection<ArtifactWrapper> Favorites {
            get => favorites;
            set {
                favorites = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FavoritesView));
            }
        }
        public List<ArtifactWrapper> FavoritesView {
            get {
                return Favorites.Where(e => (!HeroFavoriteFilter && !RoleFavoriteFilter && !AscendFavoriteFilter && !LowFavoriteFilter) ||
                    (HeroFavoriteFilter && e.Hero != null && !e.Low & !e.Ascend) ||
                    (RoleFavoriteFilter && e.Tag != null && !e.Low & !e.Ascend) ||
                    (AscendFavoriteFilter && e.Ascend) ||
                    (LowFavoriteFilter && e.Low)
                ).ToList();
            }
        }
        public void UpdateFavorites() {
            OnPropertyChanged(nameof(FavoritesView));
        }

        private ObservableCollection<ArtifactSetWrapper> setWrappers;
        private int countAll;
        private int count1011;
        private int countAccessorialAll;
        private int countAccessorial1011;
        private int upgradesCount;
        private int equippedCount;
        private int equippedCount1011;
        private int equippedCount1215;

        public ObservableCollection<ArtifactSetWrapper> SetWrappers {
            get => setWrappers;
            set {
                setWrappers = value;
                OnPropertyChanged();
            }
        }

        public RaidData Data { get; internal set; }

        public int CountAll {
            get => countAll;
            set {
                countAll = value;
                OnPropertyChanged();
            }
        }
        public int Count1011 {
            get => count1011;
            set {
                count1011 = value;
                OnPropertyChanged();
            }
        }
        public int CountAccessorialAll {
            get => countAccessorialAll;
            set {
                countAccessorialAll = value;
                OnPropertyChanged();
            }
        }
        public int CountAccessorial1011 {
            get => countAccessorial1011;
            set {
                countAccessorial1011 = value;
                OnPropertyChanged();
            }
        }
        public int UpgradesCount {
            get => upgradesCount;
            set {
                upgradesCount = value;
                OnPropertyChanged();
            }
        }
        public int EquippedCount {
            get => equippedCount;
            set {
                equippedCount = value;
                OnPropertyChanged();
            }
        }
        public int EquippedCount1011 {
            get => equippedCount1011;
            set {
                equippedCount1011 = value;
                OnPropertyChanged();
            }
        }
        public int EquippedCount1215 {
            get => equippedCount1215;
            set {
                equippedCount1215 = value;
                OnPropertyChanged();
            }
        }
    }
}
