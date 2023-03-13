using Avalonia.Controls.Shapes;
using HellHades.ArtifactExtractor;
using HellHades.ArtifactExtractor.Models;
using HellHades.ArtifactExtractor.OptimizerV3;
using HellHades.ArtifactExtractor.RaidReader;
using HellHades.ArtifactExtractor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RaidCatalog.Logic.Dtos;
using RaidCatalog.Logic.Services;
using RaidCatalog.Logic.ViewModels;
using RaidCatalog.Logic.WebServices;
using RaidCatalog.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RaidCatalog {
    public partial class MainWindow : Window {

        private IConfiguration Configuration;

        private MainService MainService;
        private SettingsService SettingsService = new SettingsService();
        private FilterService FilterService = new FilterService();
        private LoaderService LoaderService = null;
        private ArtifactWebService ArtifactWebService = new ArtifactWebService();
        public int UserId = 1;

        public MainViewModel Model {
            get => (MainViewModel)this.DataContext;
            set => this.DataContext = value;
        }

        public MainWindow() {
            InitializeComponent();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            this.Configuration = builder.Build();

            this.MainService = new MainService(this.Configuration);

            this.Model = new MainViewModel();

            SettingsService.Load(this.Model, Properties.Settings.Default);
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            ServiceCollection services = new ServiceCollection();
            Startup.ConfigureServices(services, this.Configuration, new string[0]);
            var serviceProvider = services.BuildServiceProvider();
            Startup.AddEventHandlers(serviceProvider);
            var provider = serviceProvider.GetService<RaidDataProvider>();
            var reader = serviceProvider.GetService<RaidReader>();
            var heroTypeService = serviceProvider.GetService<IHeroTypeService>();

            LoaderService = new LoaderService(this.Configuration, provider, reader, heroTypeService);

            this.Model.ArtifactsLoading = true;
            await LoaderService.LoadAsync(this.Model);
            await MainService.UpdateArtifactWrappersAsync(this.Model);
            await MainService.UpdateHeroWrappersAsync(this.Model);
            this.Model.ArtifactsLoading = false;

            this.Model.FavoritesLoading = true;
            SettingsService.LoadVisibleHeroes(this.Model, Properties.Settings.Default);
            var result = await ArtifactWebService.GetArtsAsync(UserId);
            if (result.Success) {
                await MainService.ApplyFavoritesAsync(result.Data, this.Model);
            } else {
                System.Windows.MessageBox.Show(result.Message);
            }
            this.Model.FavoritesLoading = false;

            //Heroes_Click(null,null);
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e) {
            this.Model.ArtifactsLoading = true;
            await LoaderService.LoadAsync(this.Model);
            await MainService.UpdateArtifactWrappersAsync(this.Model);
            await MainService.UpdateHeroWrappersAsync(this.Model);
            this.Model.ArtifactsLoading = false;

            //---------------------------------------
            //foreach(var wrapper in this.Model.SetWrappers) {
            //    foreach(var artifact in wrapper.Artifacts) {
            //        artifact.
            //    }
            //}

            this.Model.FavoritesLoading = true;
            SettingsService.LoadVisibleHeroes(this.Model, Properties.Settings.Default);
            var result = await ArtifactWebService.GetArtsAsync(UserId);
            if (result.Success) {
                await MainService.ApplyFavoritesAsync(result.Data, this.Model);
            } else {
                System.Windows.MessageBox.Show(result.Message);
            }
            this.Model.FavoritesLoading = false;
        }

        private async void ArtifactKindFilter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            FilterService.SetArtifactKindFilter((ArtifactKind)((FrameworkElement)sender).Tag, this.Model);
            await MainService.UpdateArtifactWrappersAsync(this.Model);
        }

        private async void ArtifactLevelFilter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e) {
            FilterService.SetArtifactLevelFilter((int)((FrameworkElement)sender).Tag, this.Model);
            await MainService.UpdateArtifactWrappersAsync(this.Model);
        }

        private async void EquippedFilter_Click(object sender, RoutedEventArgs e) {
            await MainService.UpdateArtifactWrappersAsync(this.Model);
        }

        private async void NoEquippedFilter_Click(object sender, RoutedEventArgs e) {
            await MainService.UpdateArtifactWrappersAsync(this.Model);
        }

        private async void HeroFavoriteFilter_Click(object sender, RoutedEventArgs e) {
            await MainService.UpdateArtifactWrappersAsync(this.Model);
            Model.UpdateFavorites();
        }

        private async void RoleFavoriteFilter_Click(object sender, RoutedEventArgs e) {
            await MainService.UpdateArtifactWrappersAsync(this.Model);
            Model.UpdateFavorites();
        }

        private async void AscendFavoriteFilter_Click(object sender, RoutedEventArgs e) {
            await MainService.UpdateArtifactWrappersAsync(this.Model);
            Model.UpdateFavorites();
        }

        private async void LowFavoriteFilter_Click(object sender, RoutedEventArgs e) {
            await MainService.UpdateArtifactWrappersAsync(this.Model);
            Model.UpdateFavorites();
        }

        private void Window_Closed(object sender, EventArgs e) {
            SettingsService.Save(this.Model, Properties.Settings.Default);
        }

        private void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            if (!e.Handled) {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        #region [ Selection ]

        private void ArtifactListView_GotFocus(object sender, RoutedEventArgs e) {
            ((UIElement)sender).UpdateLayout();
            ((ListView)sender).SelectedItem = null;
        }

        #endregion [ Selection ]

        #region [ Settings ]

        private void Heroes_Click(object sender, RoutedEventArgs e) {
            var win = new HeroesWindow(this.Model.AllHeroes);
            win.ShowDialog();
            SettingsService.SaveVisibleHeroes(this.Model, Properties.Settings.Default);
        }

        private void SaveJson_Click(object sender, RoutedEventArgs args) {
            try {
                var dataJson = JsonConvert.SerializeObject(this.Model.Data);
                var heroTypesJson = JsonConvert.SerializeObject(this.Model.HeroTypes);
                File.WriteAllTextAsync("DATA.json", dataJson);
                File.WriteAllTextAsync("HERO_TYPES.json", heroTypesJson);
                System.Windows.MessageBox.Show("Saved");
            } catch (Exception e) {
                System.Windows.MessageBox.Show(e.Message);
            }
        }

        #endregion [ Settings ]

        private async void Selected_Hero(ArtifactWrapper art, HeroWrapper hero, SetWrapper set) {
            if (Model.Selection == null || Model.SelectionChanging == true || Model.FavoritesLoading == true) return;

            Model.FavoritesLoading = true;
            Model.ErrorMessage = null;

            if (art.WebId != null) {
                if (hero != null || set != null) {
                    // update
                    try {
                        var webResult = await ArtifactWebService.UpdateArtAsync(UserId, new WebArtDto() {
                            Id = (int)art.WebId,
                            UserId = UserId,
                            ArtId = art.Artifact.Id,
                            HeroId = (int?)hero?.Hero?.Id,
                            Tag = (byte?)set?.Set,
                            Ascend = art.Ascend,
                            Low = art.Low,
                            Comment = art.Comment,
                            Order = art.Order
                        });
                        if (webResult.Success) {
                            art.WebId = webResult.Data?.Id;
                            art.Hero = hero;
                            art.Set = set;
                        } else {
                            Model.ErrorMessage = webResult.Message;
                        }
                    } catch {
                    }
                } else {
                    // delete
                    try {
                        var webResult = await ArtifactWebService.DeleteArtAsync(UserId, art.Artifact.Id);
                        if (webResult.Success) {
                            art.WebId = null;
                            art.Hero = null;
                            art.Set = null;
                            this.MainService.RemoveFavorites(Model, art);
                        } else {
                            Model.ErrorMessage = webResult.Message;
                        }
                    } catch {
                    }
                }

            } else {
                if (hero != null || set != null) {
                    // insert
                    try {
                        var webResult = await ArtifactWebService.AddArtAsync(UserId, new WebArtDto() {
                            UserId = UserId,
                            ArtId = art.Artifact.Id,
                            HeroId = (int?)hero?.Hero?.Id,
                            Tag = (byte?)set?.Set,
                            Ascend = art.Ascend,
                            Low = art.Low,
                            Comment = art.Comment,
                            Order = art.Order
                        });
                        if (webResult.Success) {
                            art.WebId = webResult.Data.Id;
                            art.Hero = hero;
                            art.Set = set;
                            this.MainService.AddFavorites(Model, art);
                        } else {
                            Model.ErrorMessage = webResult.Message;
                        }
                    } catch {
                    }
                } else {
                    // delete
                    try {
                        var webResult = await ArtifactWebService.DeleteArtAsync(UserId, art.Artifact.Id);
                        if (webResult.Success) {
                            art.WebId = null;
                            art.Hero = null;
                            art.Set = null;
                            this.MainService.RemoveFavorites(Model, art);
                        } else {
                            Model.ErrorMessage = webResult.Message;
                        }
                    } catch {
                    }
                }
            }
            Model.UpdateFavorites();

            Model.FavoritesLoading = false;
        }

        private void SelectHero_Click(object sender, RoutedEventArgs e) {
            var dlg = new HeroesSelectorWindow(Model.AllHeroes.Where(e => e.Visible).ToList(), Model.Selection.Hero);
            if (dlg.ShowDialog() == true) {
                Selected_Hero(Model.Selection, dlg.Model.SelectedHero, Model.Selection.Set);
            }
        }

        private void ClearHero_Click(object sender, RoutedEventArgs e) {
            Selected_Hero(Model.Selection, null, Model.Selection.Set);
        }

        private void SelectSet_Click(object sender, RoutedEventArgs e) {
            var dlg = new SetSelectorWindow(Model.Selection.Set);
            if (dlg.ShowDialog() == true) {
                Selected_Hero(Model.Selection, Model.Selection.Hero, dlg.Model.SelectedSet);
            }
        }

        private void ClearSet_Click(object sender, RoutedEventArgs e) {
            var dlg = new SetSelectorWindow(Model.Selection.Set);
            Selected_Hero(Model.Selection, Model.Selection.Hero, null);
        }

        private async void Check_Click(object sender, RoutedEventArgs e) {

            var selection = Model.Selection;
            if (selection == null) return;
            #region [ single request + delay ]
            var localTask = task = Task.Delay(1000);
            await task;
            if (localTask != task) {
                return;
            }
            #endregion [ single request + delay ]

            if (selection.Ascend == false && selection.Low == false && selection.Hero == null && selection.Tag == null) {
                var result = await ArtifactWebService.DeleteArtAsync(UserId, selection.Artifact.Id);
                this.MainService.RemoveFavorites(Model, selection);
            } else {
                if (selection.WebId == null) {
                    var webResult = await ArtifactWebService.AddArtAsync(UserId, new WebArtDto() {
                        UserId = UserId,
                        ArtId = selection.Artifact.Id,
                        Ascend = selection.Ascend,
                        Low = selection.Low,
                    });
                    if (webResult.Success) {
                        selection.WebId = webResult.Data.Id;
                        this.MainService.AddFavorites(Model, selection);
                    } else {
                        Model.ErrorMessage = webResult.Message;
                    }
                } else {
                    var result = await ArtifactWebService.UpdateArtAsync(UserId, new WebArtDto() {
                        Id = selection.Artifact.Id,
                        UserId = UserId,
                        ArtId = selection.Artifact.Id,
                        HeroId = selection.Hero?.Hero?.Id,
                        Order = selection.Order,
                        Comment = selection.Comment,
                        Ascend = selection.Ascend,
                        Low = selection.Low,
                        Tag = selection.Tag,
                    });
                }
            }
            Model.UpdateFavorites();
        }

        Task task;
        private async void Comment_KeyUp(object sender, KeyEventArgs e) {
            var selection = Model.Selection;
            if (selection == null) return;
            #region [ single request + delay ]
            var localTask = task = Task.Delay(1000);
            await task;
            if (localTask != task) {
                return;
            }
            #endregion [ single request + delay ]

            var result = await ArtifactWebService.UpdateArtAsync(UserId, new WebArtDto() {
                Id = selection.Artifact.Id,
                UserId = UserId,
                ArtId = selection.Artifact.Id,
                HeroId = selection.Hero?.Hero?.Id,
                Order = selection.Order,
                Comment = selection.Comment,
                Ascend = selection.Ascend,
                Low = selection.Low,
                Tag = selection.Tag,
            });
        }

        private async void Rating_KeyUp(object sender, KeyEventArgs e) {
            var selection = Model.Selection;
            if (selection == null) return;
            #region [ single request + delay ]
            var localTask = task = Task.Delay(1000);
            await task;
            if (localTask != task) {
                return;
            }
            #endregion [ single request + delay ]

            var result = await ArtifactWebService.UpdateArtAsync(UserId, new WebArtDto() {
                Id = selection.Artifact.Id,
                UserId = UserId,
                ArtId = selection.Artifact.Id,
                HeroId = selection.Hero?.Hero?.Id,
                Order = selection.Order,
                Comment = selection.Comment,
                Ascend = selection.Ascend,
                Low = selection.Low,
                Tag = selection.Tag,
            });
            this.MainService.UpdateOrderFavorits(this.Model);
        }
    }
}
