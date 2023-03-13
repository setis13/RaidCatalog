using HellHades.ArtifactExtractor.Models;
using HellHades.ArtifactExtractor.RaidReader;
using Microsoft.Extensions.Configuration;
using RaidCatalog.Logic.Converters;
using RaidCatalog.Logic.Dtos;
using RaidCatalog.Logic.ViewModels;
using RaidCatalog.Logic.WebServices;
using RaidCatalog.Logic.WebServices.Models;
using System.Collections.ObjectModel;
using System.Printing;
using System.Windows.Media.TextFormatting;

namespace RaidCatalog.Logic.Services {
    public class MainService {

        private IConfiguration Configuration;
        private ImageWebService ImageWebService;

        public MainService(IConfiguration configuration) {
            Configuration = configuration;
            ImageWebService = new ImageWebService(configuration);
        }

        public async Task UpdateArtifactWrappersAsync(MainViewModel model) {
            var wrappers = await GetFilteredArtifactWrappersAsync(model);
            if (model.SetWrappers == null) {
                model.SetWrappers = new ObservableCollection<ArtifactSetWrapper>();
                foreach (ArtifactSetWrapper wrapper in wrappers) {
                    model.SetWrappers.Add(wrapper);
                }
            } else {
                this.ApplyArtifactWrappers(model.SetWrappers, wrappers);
            }
            if (model.Sets == null) {
                model.Sets = new List<SetWrapper>();
                foreach (var set in Enum.GetValues<ArtifactSet>()) {
                    var img = ImageWebService.GetSetImage(set);
                    if (img != null) {
                        model.Sets.Add(new SetWrapper(set) {
                            Image = img
                        });
                    }
                }
            }
            var equipedArtifacts = model.AllArtifacts.Where(e => e.Artifact.IsActivated && e.Hero != null && e.Hero.Hero.InStorage == false);
            var noEquipedArtifacts = model.AllArtifacts.Where(e => e.Artifact.IsActivated == false);

            model.CountAll = noEquipedArtifacts.Count(e => e.Artifact.Kind <= ArtifactKind.Shield);
            model.Count1011 = noEquipedArtifacts.Count(e => e.Artifact.Kind >= ArtifactKind.Shield && (e.Artifact.Level == 10 || e.Artifact.Level == 11));
            model.CountAccessorialAll = noEquipedArtifacts.Count(e => e.Artifact.Kind >= ArtifactKind.Ring);
            model.CountAccessorial1011 = noEquipedArtifacts.Count(e => e.Artifact.Kind >= ArtifactKind.Ring && (e.Artifact.Level == 10 || e.Artifact.Level == 11));
            model.UpgradesCount = equipedArtifacts.Where(e => e.Artifact.Level >= 10 && e.Artifact.Level <= 15).Sum(e => 16 - e.Artifact.Level);
            model.EquippedCount = equipedArtifacts.Count(e => (e.Artifact.Level >= 10 && e.Artifact.Level <= 15));
            model.EquippedCount1011 = equipedArtifacts.Count(e => (e.Artifact.Level == 10 || e.Artifact.Level == 11));
            model.EquippedCount1215 = equipedArtifacts.Count(e => (e.Artifact.Level >= 12 && e.Artifact.Level <= 15));
        }

        public async Task UpdateHeroWrappersAsync(MainViewModel model) {
            foreach (var wrapper in model.AllHeroes) {
                wrapper.Image = await ImageWebService.GetHeroImageAsync(wrapper.HeroType.Fraction, wrapper.HeroType.Rarity, wrapper.HeroType.Name);
            }
        }

        private void ApplyArtifactWrappers(IList<ArtifactSetWrapper> destination, IList<ArtifactSetWrapper> source) {
            // removes sets
            foreach (var setWrapper in destination.Where(d => source.Any(s => s.Group == d.Group) == false).ToList()) {
                destination.Remove(setWrapper);
            }
            // adds sets
            foreach (var setWrapper in source.Where(s => destination.Any(d => d.Group == s.Group) == false).ToList()) {
                var index = this.GetInsertIndex(setWrapper, destination);
                destination.Insert(index, setWrapper);
            }
            foreach (var destinationWrapper in destination) {
                var sourceWrapper = source.First(s => s.Group == destinationWrapper.Group);
                // removes artifacts
                foreach (var artifact in destinationWrapper.Artifacts
                    .Where(d => sourceWrapper.Artifacts.Any(s => s.Artifact.Id == d.Artifact.Id) == false).ToList()) {
                    destinationWrapper.Artifacts.Remove(artifact);
                }
                // removes different level artifacts
                foreach (var artifact in destinationWrapper.Artifacts
                    .Where(d => sourceWrapper.Artifacts.Any(s => s.Artifact.Id == d.Artifact.Id && s.Artifact.Level != d.Artifact.Level) == true).ToList()) {
                    destinationWrapper.Artifacts.Remove(artifact);
                }
                // adds artifacts
                foreach (var artifact in sourceWrapper
                    .Artifacts.Where(s => destinationWrapper.Artifacts.Any(d => d.Artifact.Id == s.Artifact.Id) == false).ToList()) {
                    var index = this.GetInsertIndex(artifact, destinationWrapper.Artifacts);
                    destinationWrapper.Artifacts.Insert(index, artifact);
                }
            }
        }

        private async Task<List<ArtifactSetWrapper>> GetFilteredArtifactWrappersAsync(MainViewModel model) {
            var resultWrappers = new List<ArtifactSetWrapper>();
            foreach (ArtifactWrapper artifact in model.AllArtifacts
                                                    .OrderBy(e => ArtifactSetToOrderConverter.Convert(e.Artifact.Set, e.Artifact.RequiredFraction))
                                                    .ThenBy(e => ArtifactKindToOrderConverter.Convert(e.Artifact.Kind))
                                                    .ThenByDescending(e => e.Artifact.Level)) {
                if (this.CheckFilter(artifact, model) == false) continue;
                ArtifactSetWrapper wrapper = resultWrappers.FirstOrDefault(e => e.Group == artifact.Group);
                if (wrapper == null) {
                    resultWrappers.Add(wrapper = new ArtifactSetWrapper(artifact.Artifact.Set, artifact.Artifact.RequiredFraction));
                }
                artifact.Image = await ImageWebService.GetArtifactImageAsync(artifact.Artifact.Set, artifact.Artifact.Kind, artifact.Artifact.RequiredFraction);
                wrapper.Artifacts.Add(artifact);
            }
            return resultWrappers;
        }

        /// <summary>
        ///     Gets Inserting Index </summary>
        /// <param name="setWrapper">Inserting</param>
        /// <param name="setWrappers">Ordered List</param>
        private int GetInsertIndex(ArtifactSetWrapper setWrapper, IList<ArtifactSetWrapper> setWrappers) {
            var tmp = setWrappers.FirstOrDefault(e => e.Group > setWrapper.Group);
            if (tmp == null) return setWrappers.Count;
            return setWrappers.IndexOf(tmp);
        }

        /// <summary>
        ///     Gets Inserting Index </summary>
        /// <param name="wrapper">Inserting</param>
        /// <param name="wrappers">Ordered List</param>
        private int GetInsertIndex(ArtifactWrapper wrapper, IList<ArtifactWrapper> wrappers) {
            var tmp = wrappers.FirstOrDefault(e =>
                (ArtifactKindToOrderConverter.Convert(e.Artifact.Kind) * 100) + e.Artifact.Level >
                (ArtifactKindToOrderConverter.Convert(wrapper.Artifact.Kind) * 100) + wrapper.Artifact.Level);
            if (tmp == null) return wrappers.Count;
            return wrappers.IndexOf(tmp);
        }

        private bool CheckFilter(ArtifactWrapper artifact, MainViewModel model) {
            return this.CheckFilterByKind(artifact.Artifact, model) && this.CheckFilterByLevel(artifact.Artifact, model) && this.CheckFilterByEquipped(artifact, model);
        }

        private bool CheckFilterByEquipped(ArtifactWrapper artifact, MainViewModel model) {
            if (model.EquippedFilter != model.NoEquippedFilter) {
                if (model.EquippedFilter && artifact.Hero != null && artifact.Hero.Hero.InStorage == false) {
                    return true;
                }
                if (model.NoEquippedFilter && artifact.Hero == null) {
                    return true;
                }
                return false;
            } else {
                return true;
            }
        }

        private bool CheckFilterByLevel(ArtifactViewModel artifact, MainViewModel model) {
            if (model.Level9Filter == true ||
                model.Level10Filter == true ||
                model.Level11Filter == true ||
                model.Level12Filter == true ||
                model.Level13Filter == true ||
                model.Level14Filter == true ||
                model.Level15Filter == true ||
                model.Level16Filter == true) {
                if (model.Level10Filter && artifact.Level == 10) {
                    return true;
                }
                if (model.Level11Filter && artifact.Level == 11) {
                    return true;
                }
                if (model.Level12Filter && artifact.Level == 12) {
                    return true;
                }
                if (model.Level13Filter && artifact.Level == 13) {
                    return true;
                }
                if (model.Level14Filter && artifact.Level == 14) {
                    return true;
                }
                if (model.Level15Filter && artifact.Level == 15) {
                    return true;
                }
                if (model.Level16Filter && artifact.Level == 16) {
                    return true;
                }
                return false;
            } else {
                return true;
            }
        }

        private bool CheckFilterByKind(ArtifactViewModel artifact, MainViewModel model) {
            if (model.WeaponFilter == true ||
                model.HelmetFilter == true ||
                model.ShieldFilter == true ||
                model.GlovesFilter == true ||
                model.ChestFilter == true ||
                model.BootsFilter == true ||
                model.RingFilter == true ||
                model.CloakFilter == true ||
                model.BannerFilter == true) {
                if (model.WeaponFilter && artifact.Kind == ArtifactKind.Weapon) {
                    return true;
                }
                if (model.HelmetFilter && artifact.Kind == ArtifactKind.Helmet) {
                    return true;
                }
                if (model.ShieldFilter && artifact.Kind == ArtifactKind.Shield) {
                    return true;
                }
                if (model.GlovesFilter && artifact.Kind == ArtifactKind.Gloves) {
                    return true;
                }
                if (model.ChestFilter && artifact.Kind == ArtifactKind.Chest) {
                    return true;
                }
                if (model.BootsFilter && artifact.Kind == ArtifactKind.Boots) {
                    return true;
                }
                if (model.RingFilter && artifact.Kind == ArtifactKind.Ring) {
                    return true;
                }
                if (model.CloakFilter && artifact.Kind == ArtifactKind.Cloak) {
                    return true;
                }
                if (model.BannerFilter && artifact.Kind == ArtifactKind.Banner) {
                    return true;
                }
                return false;
            } else {
                return true;
            }
        }

        public void UpdateSetArtifactWrappers(MainViewModel model) {

        }

        public async Task ApplyFavoritesAsync(WebArtDto[] dtos, MainViewModel model) {
            model.Favorites.Clear();
            foreach (var dto in dtos) {
                var artifactWrapper = model.AllArtifacts.FirstOrDefault(e => e.Artifact.Id == dto.ArtId);
                if (artifactWrapper != null) {
                    if (artifactWrapper.Image == null) {
                        artifactWrapper.Image = await ImageWebService.GetArtifactImageAsync(
                            artifactWrapper.Artifact.Set,
                            artifactWrapper.Artifact.Kind,
                            artifactWrapper.Artifact.RequiredFraction);
                    }
                    if (dto.HeroId != null) {
                        var heroWrapper = model.AllHeroes.FirstOrDefault(e => e.Hero.Id == dto.HeroId);
                        if (heroWrapper != null) {
                            artifactWrapper.Hero = heroWrapper;
                        }
                    }
                    if (dto.Tag != null) {
                        var setWrapper = model.Sets.FirstOrDefault(e => (int)e.Set == dto.Tag);
                        if (setWrapper != null) {
                            artifactWrapper.Set = setWrapper;
                        }
                    }
                    artifactWrapper.WebId = dto.Id;
                    artifactWrapper.Order = dto.Order;
                    artifactWrapper.Comment = dto.Comment;
                    artifactWrapper.Low = dto.Low;
                    artifactWrapper.Ascend = dto.Ascend;
                    artifactWrapper.Tag = dto.Tag;
                    model.Favorites.Add(artifactWrapper);
                }
            }
            UpdateOrderFavorits(model);
        }

        public void RemoveFavorites(MainViewModel model, ArtifactWrapper artifact) {
            if (model.Favorites.Contains(artifact)) {
                model.Favorites.Remove(artifact);
            }
            UpdateOrderFavorits(model);
        }
        public void AddFavorites(MainViewModel model, ArtifactWrapper artifact) {
            if (model.Favorites.Contains(artifact) == false) {
                model.Favorites.Add(artifact);
            }
            UpdateOrderFavorits(model);
        }

        public void UpdateOrderFavorits(MainViewModel model) {
            model.Favorites = new ObservableCollection<ArtifactWrapper>(model.Favorites.OrderByDescending(e => e.Order).ThenByDescending(e => e.Artifact.Level));
        }
    }
}
