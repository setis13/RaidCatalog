using HellHades.ArtifactExtractor.Models;
using RaidCatalog.Logic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaidCatalog.Logic.Services {
    public class FilterService {
        public bool SetArtifactKindFilter(ArtifactKind artifactKind, MainViewModel model) {
            switch (artifactKind) {
                case ArtifactKind.Helmet:
                    return model.HelmetFilter = !model.HelmetFilter;
                case ArtifactKind.Chest:
                    return model.ChestFilter = !model.ChestFilter;
                case ArtifactKind.Gloves:
                    return model.GlovesFilter = !model.GlovesFilter;
                case ArtifactKind.Boots:
                    return model.BootsFilter = !model.BootsFilter;
                case ArtifactKind.Weapon:
                    return model.WeaponFilter = !model.WeaponFilter;
                case ArtifactKind.Shield:
                    return model.ShieldFilter = !model.ShieldFilter;
                case ArtifactKind.Ring:
                    return model.RingFilter = !model.RingFilter;
                case ArtifactKind.Cloak:
                    return model.CloakFilter = !model.CloakFilter;
                case ArtifactKind.Banner:
                    return model.BannerFilter = !model.BannerFilter;
                default:
                    return default;
            }
        }
        public bool SetArtifactLevelFilter(int artifactlevel, MainViewModel model) {
            switch (artifactlevel) {
                case 9:
                    return model.Level9Filter = !model.Level9Filter;
                case 10:
                    return model.Level10Filter = !model.Level10Filter;
                case 11:
                    return model.Level11Filter = !model.Level11Filter;
                case 12:
                    return model.Level12Filter = !model.Level12Filter;
                case 13:
                    return model.Level13Filter = !model.Level13Filter;
                case 14:
                    return model.Level14Filter = !model.Level14Filter;
                case 15:
                    return model.Level15Filter = !model.Level15Filter;
                case 16:
                    return model.Level16Filter = !model.Level16Filter;
                default:
                    return default;
            }
        }
    }
}
