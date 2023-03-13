using HellHades.ArtifactExtractor;
using HellHades.ArtifactExtractor.Models;
using HellHades.ArtifactExtractor.OptimizerV3;
using HellHades.ArtifactExtractor.RaidReader;
using HellHades.ArtifactExtractor.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RaidCatalog.Logic.ViewModels;
using RaidCatalog.Logic.WebServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RaidCatalog.Logic.Services {
    public class LoaderService {
        private IConfiguration Configuration;
        private RaidDataProvider RaidDataProvider;
        private RaidReader RaidReader;
        private IHeroTypeService HeroTypeService;

        public LoaderService(IConfiguration configuration, RaidDataProvider raidDataProvider, RaidReader raidReader, IHeroTypeService heroTypeService) {
            this.Configuration = configuration;
            this.RaidDataProvider = raidDataProvider;
            this.RaidReader = raidReader;
            this.HeroTypeService = heroTypeService;
        }

        public async Task LoadAsync(MainViewModel model) {
#if DEBUG
            List<HeroType> heroTypes = await Task.Factory.StartNew(() => {
                var json = File.ReadAllText(@"D:\Develop\RaidCatalog\DATA\HERO_TYPES.json");
                return JsonConvert.DeserializeObject<List<HeroType>>(json);
            });
            model.HeroTypes = heroTypes;

            RaidData data = await Task.Factory.StartNew(() => {
                var json = File.ReadAllText(@"D:\Develop\RaidCatalog\DATA\DATA.json");
                return JsonConvert.DeserializeObject<RaidData>(json);
            });
#else
            var heroTypes = await HeroTypeService.GetHeroTypesAsync(CancellationToken.None);
            RaidData data = RaidReader.LoadData();
#endif
            model.AllHeroes = data.Heroes.Select(e => new HeroWrapper(e, heroTypes.First(t => t.Id == e.TypeId))).ToList();
            model.AllArtifacts = data.Artifacts.Select(e => new ArtifactWrapper(e)).ToList();

            //  Sets heroes
            foreach (var hero in model.AllHeroes) {
                if (hero.Hero.Artifacts != null) {
                    foreach (int artifactId in hero.Hero.Artifacts) {
                        model.AllArtifacts.First(e => e.Artifact.Id == artifactId).Hero = hero;
                    }
                }
            }

            model.Data = data;
        }
    }
}
