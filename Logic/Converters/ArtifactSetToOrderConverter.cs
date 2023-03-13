using HellHades.ArtifactExtractor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidCatalog.Logic.Converters {
    public static class ArtifactSetToOrderConverter {
        public static int Convert(ArtifactSet set, HeroFraction requiredFraction) {
            if (set != ArtifactSet.None && 
                set != ArtifactSet.IgnoreCooldown &&
                set != ArtifactSet.RemoveDebuff &&
                set != ArtifactSet.ShieldAccessory &&
                set != ArtifactSet.ChangeHitType &&
                set != ArtifactSet.CounterattackAccessory) {
                return (int)set;
            }
            return (int)requiredFraction + 1000;
        }
    }
}
