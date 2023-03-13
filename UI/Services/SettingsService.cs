using Newtonsoft.Json;
using RaidCatalog.Logic.ViewModels;
using RaidCatalog.Properties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaidCatalog.Services {
    internal class SettingsService {
        public void Load(MainViewModel model, Settings settings) {
            model.HeroFavoriteFilter = settings.HeroFavoriteFilter;
            model.RoleFavoriteFilter = settings.RoleFavoriteFilter;
            model.AscendFavoriteFilter = settings.AscendFavoriteFilter;
            model.LowFavoriteFilter = settings.LowFavoriteFilter;

            model.WeaponFilter = settings.WeaponFilter;
            model.HelmetFilter = settings.HelmetFilter;
            model.ShieldFilter = settings.ShieldFilter;
            model.GlovesFilter = settings.GlovesFilter;
            model.ChestFilter = settings.ChestFilter;
            model.BootsFilter = settings.BootsFilter;
            model.RingFilter = settings.RingFilter;
            model.CloakFilter = settings.CloakFilter;
            model.BannerFilter = settings.BannerFilter;

            model.Level9Filter = settings.Level9Filter;
            model.Level10Filter = settings.Level10Filter;
            model.Level11Filter = settings.Level11Filter;
            model.Level12Filter = settings.Level12Filter;
            model.Level13Filter = settings.Level13Filter;
            model.Level14Filter = settings.Level14Filter;
            model.Level15Filter = settings.Level15Filter;
            model.Level16Filter = settings.Level16Filter;

            model.NoEquippedFilter = settings.NoEquippedFilter;
            model.EquippedFilter = settings.EquippedFilter;

            model.GlyphRatingFilter = settings.GlyphRatingFilter;
            model.GlyphAtkFilter = settings.GlyphAtkFilter;
            model.GlyphDefFilter = settings.GlyphDefFilter;
            model.GlyphHpFilter = settings.GlyphHpFilter;
            model.GlyphAtkPFilter = settings.GlyphAtkPFilter;
            model.GlyphDefPFilter = settings.GlyphDefPFilter;
            model.GlyphHpPFilter = settings.GlyphHpPFilter;
            model.GlyphAccFilter = settings.GlyphAccFilter;
            model.GlyphResFilter = settings.GlyphResFilter;
            model.GlyphSpdFilter = settings.GlyphSpdFilter;
        }

        public void Save(MainViewModel model, Settings settings) {
            settings.HeroFavoriteFilter = model.HeroFavoriteFilter;
            settings.RoleFavoriteFilter = model.RoleFavoriteFilter;
            settings.AscendFavoriteFilter = model.AscendFavoriteFilter;
            settings.LowFavoriteFilter = model.LowFavoriteFilter;

            settings.WeaponFilter = model.WeaponFilter;
            settings.HelmetFilter = model.HelmetFilter;
            settings.ShieldFilter = model.ShieldFilter;
            settings.GlovesFilter = model.GlovesFilter;
            settings.ChestFilter = model.ChestFilter;
            settings.BootsFilter = model.BootsFilter;
            settings.RingFilter = model.RingFilter;
            settings.CloakFilter = model.CloakFilter;
            settings.BannerFilter = model.BannerFilter;

            settings.Level9Filter = model.Level9Filter;
            settings.Level10Filter = model.Level10Filter;
            settings.Level11Filter = model.Level11Filter;
            settings.Level12Filter = model.Level12Filter;
            settings.Level13Filter = model.Level13Filter;
            settings.Level14Filter = model.Level14Filter;
            settings.Level15Filter = model.Level15Filter;
            settings.Level16Filter = model.Level16Filter;

            settings.NoEquippedFilter = model.NoEquippedFilter;
            settings.EquippedFilter = model.EquippedFilter;

            settings.GlyphRatingFilter = model.GlyphRatingFilter;
            settings.GlyphAtkFilter = model.GlyphAtkFilter;
            settings.GlyphDefFilter = model.GlyphDefFilter;
            settings.GlyphHpFilter = model.GlyphHpFilter;
            settings.GlyphAtkPFilter = model.GlyphAtkPFilter;
            settings.GlyphDefPFilter = model.GlyphDefPFilter;
            settings.GlyphHpPFilter = model.GlyphHpPFilter;
            settings.GlyphAccFilter = model.GlyphAccFilter;
            settings.GlyphResFilter = model.GlyphResFilter;
            settings.GlyphSpdFilter = model.GlyphSpdFilter;

            settings.Save();
        }

        public void SaveVisibleHeroes(MainViewModel model, Settings settings) {
            var ids = model.AllHeroes.Where(e => e.Visible).Select(e => e.Hero.Id).ToList();
            settings.VisibleHeroes = JsonConvert.SerializeObject(ids);
            settings.Save();
        }

        public void LoadVisibleHeroes(MainViewModel model, Settings settings) {
            try {
                var ids = JsonConvert.DeserializeObject<List<int>>(settings.VisibleHeroes);
                if (ids == null) return;
                foreach (var id in ids) {
                    var hero = model.AllHeroes.FirstOrDefault(e => e.Hero.Id == id);
                    if (hero != null) {
                        hero.Visible = true;
                    }
                }
            } catch (Exception e) {
                MainViewModel.Instance.ErrorMessage = e.Message;
            }
        }
    }
}
