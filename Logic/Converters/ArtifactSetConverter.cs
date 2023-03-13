using HellHades.ArtifactExtractor.Models;
using RaidCatalog.Logic.Extensions;

namespace RaidCatalog.Logic.Converters {
    public static class ArtifactSetConverter {
        public static string Convert(ArtifactSet set) {
            switch (set) {
                case ArtifactSet.None:
                    return "None";
                case ArtifactSet.Hp:
                    return "Life";
                case ArtifactSet.AttackPower:
                    return "Offense";
                case ArtifactSet.Defense:
                    return "Defense";
                case ArtifactSet.AttackSpeed:
                    return "Speed";
                case ArtifactSet.CriticalChance:
                    return "Critical_Rate";
                case ArtifactSet.CriticalDamage:
                    return "CriticalDamage";
                case ArtifactSet.Accuracy:
                    return "Accuracy";
                case ArtifactSet.Resistance:
                    return "Resistance";
                case ArtifactSet.LifeDrain:
                    return "Lifesteal";
                case ArtifactSet.DamageIncreaseOnHpDecrease:
                    return "Fury";
                case ArtifactSet.SleepChance:
                    return "Daze";
                case ArtifactSet.BlockHealChance:
                    return "Cursed";
                case ArtifactSet.FreezeRateOnDamageReceived:
                    return "Frost";
                case ArtifactSet.Stamina:
                    return "Frenzy";
                case ArtifactSet.Heal:
                    return "Regeneration";
                case ArtifactSet.BlockDebuff:
                    return "Immunity";
                case ArtifactSet.Shield:
                    return "Shield";
                case ArtifactSet.GetExtraTurn:
                    return "Relentless";
                case ArtifactSet.IgnoreDefense:
                    return "Savage";
                case ArtifactSet.DecreaseMaxHp:
                    return "Destroy";
                case ArtifactSet.StunChance:
                    return "Stun";
                case ArtifactSet.DotRate:
                    return "Toxic";
                case ArtifactSet.ProvokeChance:
                    return "Provoke";
                case ArtifactSet.Counterattack:
                    return "Retaliation";
                case ArtifactSet.CounterattackOnCrit:
                    return "Avenging";
                case ArtifactSet.AoeDamageDecrease:
                    return "Stalwart";
                case ArtifactSet.CooldownReductionChance:
                    return "Reflex";
                case ArtifactSet.CriticalHealMultiplier:
                    return "Curing";
                case ArtifactSet.AttackPowerAndIgnoreDefense:
                    return "Cruel";
                case ArtifactSet.HpAndHeal:
                    return "Immortal";
                case ArtifactSet.ShieldAndAttackPower:
                    return "DivineOffense";
                case ArtifactSet.ShieldAndCriticalChance:
                    return "DivineCriticalRate";
                case ArtifactSet.ShieldAndHp:
                    return "DivineLife";
                case ArtifactSet.ShieldAndSpeed:
                    return "DivineSpeed";
                case ArtifactSet.UnkillableAndSpdAndCrDmg:
                    return "SwiftParry";
                //case ArtifactSet.BlockReflectDebuffAndHpAndDef:
                //    return "Offense"; // ??
                case ArtifactSet.HpAndDefence:
                    return "Resilience";
                case ArtifactSet.AccuracyAndSpeed:
                    return "Perception";
                case ArtifactSet.CritDmgAndTransformWeekIntoCritHit:
                    return "Affinitybreaker";
                case ArtifactSet.ResistanceAndBlockDebuff:
                    return "StoneSkin";
                case ArtifactSet.AttackAndCritRate:
                    return "Fatal";
                //case ArtifactSet.FreezeResistAndRate:
                //    return "";
                case ArtifactSet.CritRateAndLifeDrain:
                    return "Bloodthirst";
                case ArtifactSet.PassiveShareDamageAndHeal:
                    return "Guardian";
                case ArtifactSet.ResistAndDef:
                    return "Fortitude";
                //case ArtifactSet.CritRateAndIgnoreDefMultiplier:
                //    return "";
                case ArtifactSet.Protection:
                    return "Protection";
                case ArtifactSet.StoneSkin:
                    return "StoneSkin";
                case ArtifactSet.Killstroke:
                    return "Killstroke";
                //case ArtifactSet.Instinct:
                //    return "";
                //case ArtifactSet.Bolster:
                //    return "";
                //case ArtifactSet.Defiant:
                //    return "";
                //case ArtifactSet.IgnoreCooldown:
                //    return "";
                //case ArtifactSet.RemoveDebuff:
                //    return "";
                case ArtifactSet.ShieldAccessory:
                    return "ShieldAccessory";
                case ArtifactSet.ChangeHitType:
                    return "ChangeHitType";
                case ArtifactSet.CounterattackAccessory:
                    return "CounterattackAccessory";
                default:
                    return set.ToString().OnlyLetters();
            }

        }
    }
}
