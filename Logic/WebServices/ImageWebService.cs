using System.Diagnostics;
using System.Net;
using System.Text;
using RaidCatalog.Logic.Extensions;
using RaidCatalog.Logic.Helpers;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Extensions.Configuration;
using HellHades.ArtifactExtractor.Models;
using Path = System.IO.Path;
using System.Windows.Markup;
using RaidCatalog.Logic.Converters;
using HellHades.ArtifactExtractor.ViewModels;
using System.Xml.Linq;
using System.Net.NetworkInformation;
using System.Windows.Shapes;

namespace RaidCatalog.Logic.WebServices {
    public class ImageWebService {

        private string HeroesImagePath;
        private string ArtifactsImagePath;
        private string BaseUrl = "https://raidoptimiser.hellhades.com/assets";

        Dictionary<string, BitmapImage> BitmapImageCache = new Dictionary<string, BitmapImage>();

        public ImageWebService(IConfiguration configuration) {
            this.HeroesImagePath = configuration.GetSection("HeroesImagePath").Value;
            this.ArtifactsImagePath = configuration.GetSection("ArtifactsImagePath").Value;

            if (Directory.Exists(this.HeroesImagePath) == false) {
                Directory.CreateDirectory(this.HeroesImagePath);
            }
            if (Directory.Exists(ArtifactsImagePath) == false) {
                Directory.CreateDirectory(ArtifactsImagePath);
            }
        }

        //public async Task<BitmapImage> GetHeroImageAsync(HeroFraction fraction, Rarity rarity, string name) {
        //    name = name.OnlyLetters();
        //    var path = HeroesImagePath;
        //    path = Path.Combine(path, fraction);
        //    if (Directory.Exists(path) == false) {
        //        Directory.CreateDirectory(path);
        //    }
        //    path = Path.Combine(path, rarity);
        //    if (Directory.Exists(path) == false) {
        //        Directory.CreateDirectory(path);
        //    }
        //    path = Path.Combine(path, name + ".png");
        //    if (File.Exists(path) == false) {
        //        var data = await RequestHeroImageAsync(fraction, rarity, name);
        //        File.WriteAllBytes(path, data);
        //    }
        //    return new BitmapImage(new Uri(path));
        //}

        private BitmapImage GetDefaultImage(ArtifactKind kind) {
            switch (kind) {
                case ArtifactKind.Helmet:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Helmet.png"));
                case ArtifactKind.Chest:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Chest.png"));
                case ArtifactKind.Gloves:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Gloves.png"));
                case ArtifactKind.Boots:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Boots.png"));
                case ArtifactKind.Weapon:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Weapon.png"));
                case ArtifactKind.Shield:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Shield.png"));
                case ArtifactKind.Ring:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Ring.png"));
                case ArtifactKind.Cloak:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Cloak.png"));
                case ArtifactKind.Banner:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Filters/Banner.png"));
                default:
                    throw new Exception($"Undefined Kind '{kind}'");
            }
        }

        private BitmapImage GetDefaultImage(HeroFraction fraction) {
            switch (fraction) {
                case HeroFraction.None:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/vault.png"));
                case HeroFraction.BannerLords:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/BannerLords.png"));
                case HeroFraction.HighElves:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/HighElves.png"));
                case HeroFraction.SacredOrder:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/SacredOrder.png"));
                case HeroFraction.CovenOfMagi:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/CovenOfMagi.png"));
                case HeroFraction.OgrynTribes:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/OgrynTribes.png"));
                case HeroFraction.LizardMen:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/LizardMen.png"));
                case HeroFraction.Skinwalkers:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/Skinwalkers.png"));
                case HeroFraction.Orcs:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/Orcs.png"));
                case HeroFraction.Demonspawn:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/Skinwalkers.png"));
                case HeroFraction.UndeadHordes:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/UndeadHordes.png"));
                case HeroFraction.DarkElves:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/DarkElves.png"));
                case HeroFraction.KnightsRevenant:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/KnightsRevenant.png"));
                case HeroFraction.Barbarians:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/Barbarians.png"));
                case HeroFraction.SylvanWatchers:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/vault.png"));
                case HeroFraction.Shadowkin:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/shadowkin.png"));
                case HeroFraction.Dwarves:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/Dwarves.png"));
                default:
                    return new BitmapImage(new Uri("pack://application:,,,/Resources/Images/Fractions/vault.png"));
            }
        }

        private bool CheckImage(byte[] binary) {
            return !Encoding.UTF8.GetString(binary).StartsWith(@"<!DOCTYPE html>");
        }

        private void GetPathParts(ArtifactSet set, ArtifactKind kind, HeroFraction fraction,
            out string directoryStr, out string setStr, out string kindStr, out string extStr) {

            switch (kind) {
                case ArtifactKind.Banner:
                    kindStr = "flag";
                    directoryStr = "AccessoriesIcons";
                    extStr = "png";
                    break;
                case ArtifactKind.Cloak:
                    kindStr = "pendant";
                    directoryStr = "AccessoriesIcons";
                    extStr = "png";
                    break;
                case ArtifactKind.Ring:
                    kindStr = "ring";
                    directoryStr = "AccessoriesIcons";
                    extStr = "png";
                    break;
                default:
                    kindStr = kind.ToString();
                    directoryStr = "ArtifactsIcons";
                    extStr = "jpg";
                    break;
            }
            if (set == ArtifactSet.ChangeHitType || set == ArtifactSet.ShieldAccessory) {
                setStr = ArtifactSetConverter.Convert(set).OnlyLetters();
            } else {
                setStr = fraction != HeroFraction.None ? fraction.ToString() : ArtifactSetConverter.Convert(set).OnlyLetters();
            }
        }

        private void GetPathParts(HeroFraction fraction, Rarity rarity, ref string name, out string directoryStr, out string fractionStr, out string rarityStr) {
            directoryStr = "ChampionsIcons";
            fractionStr = fraction.ToString();
            rarityStr = rarity.ToString();
            name = name.OnlyLetters();
        }

        public async Task<BitmapImage> GetArtifactImageAsync(ArtifactSet set, ArtifactKind kind, HeroFraction fraction) {
            // generages path
            this.GetPathParts(set, kind, fraction, out string directoryStr, out string setStr, out string kindStr, out string extStr);
            var path = Path.Combine(ArtifactsImagePath, $"{setStr}_{kindStr}.{extStr}");
            if (Path.IsPathRooted(path) == false) {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }
            try {
                // checks cache
                if (BitmapImageCache.ContainsKey(path)) {
                    return BitmapImageCache[path];
                }
                // checks file
                if (File.Exists(path) == false) {
                    // downloads file
                    byte[] binary = await RequestArtifactImageAsync(directoryStr, setStr, kindStr, extStr);
                    // checks html
                    if (CheckImage(binary) == false) {
                        var img = this.GetDefaultImage(kind);
                        // writes cahce
                        if (BitmapImageCache.ContainsKey(path) == false) {
                            BitmapImageCache[path] = img;
                        }
                        return img;
                    }
                    // writes image file
                    await File.WriteAllBytesAsync(path, binary);
                }
                // writes cahce
                if (BitmapImageCache.ContainsKey(path) == false) {
                    BitmapImageCache[path] = new BitmapImage(new Uri(path));
                }
                // returns image obj
                return BitmapImageCache[path];
            } catch (Exception e) {
                ViewModels.MainViewModel.Instance.ErrorMessage = e.Message;
                return this.GetDefaultImage(kind);
            }
        }

        public async Task<BitmapImage> GetHeroImageAsync(HeroFraction fraction, Rarity rarity, string name) {
            // generages path
            this.GetPathParts(fraction, rarity, ref name, out string directoryStr, out string fractionStr, out string rarityStr);
            var path = Path.Combine(HeroesImagePath, fractionStr, rarityStr, $"{name}.png");
            if (Path.IsPathRooted(path) == false) {
                path = Path.Combine(Environment.CurrentDirectory, path);
            }
            if (Directory.Exists(Path.GetDirectoryName(path)) == false) {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            try {
                // checks cache
                if (BitmapImageCache.ContainsKey(path)) {
                    return BitmapImageCache[path];
                }
                // checks file
                if (File.Exists(path) == false) {
                    // downloads file
                    byte[] binary = await RequestHeroImageAsync(directoryStr, fractionStr, rarityStr, name);
                    // checks html
                    if (CheckImage(binary) == false) {
                        var img = this.GetDefaultImage(fraction);
                        // writes cahce
                        if (BitmapImageCache.ContainsKey(path) == false) {
                            BitmapImageCache[path] = img;
                        }
                        return img;
                    }
                    // writes image file
                    await File.WriteAllBytesAsync(path, binary);
                }
                // writes cahce
                if (BitmapImageCache.ContainsKey(path) == false) {
                    BitmapImageCache[path] = new BitmapImage(new Uri(path));
                }
                // returns image obj
                return BitmapImageCache[path];
            } catch (Exception e) {
                ViewModels.MainViewModel.Instance.ErrorMessage = e.Message;
                return this.GetDefaultImage(fraction);
            }
        }

        public BitmapImage GetSetImage(ArtifactSet set) {
            try {
                return new BitmapImage(new Uri($"pack://application:,,,/Resources/Images/Sets/{ArtifactSetConverter.Convert(set)}.png"));
            } catch (Exception) {
                return null;
            }
        }

        // https://raidoptimiser.hellhades.com/assets/ChampionsIcons/Dwarves/Legendary/Hurndig.png
        public async Task<byte[]> RequestHeroImageAsync(string directory, string fraction, string rarity, string name) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                $"{BaseUrl}/{directory}/{fraction}/{rarity}/{name}.png");
            request.Method = "GET";
            using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                return await BinaryHelper.ReadFullyAsync(response.GetResponseStream());
            }
        }

        // https://raidoptimiser.hellhades.com/assets/ArtifactsIcons/SwiftParry_Shield.jpg
        // https://raidoptimiser.hellhades.com/assets/AccessoriesIcons/Demonspawn_flag.png
        // https://raidoptimiser.hellhades.com/assets/ArtifactsIcons/Offense_Weapon.jpg
        public async Task<byte[]> RequestArtifactImageAsync(string directory, string set, string kind, string ext) {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                $"{BaseUrl}/{directory}/{set}_{kind}.{ext}");
            request.Method = "GET";
            using (var response = (HttpWebResponse)await request.GetResponseAsync()) {
                return await BinaryHelper.ReadFullyAsync(response.GetResponseStream());
            }
        }
    }
}
