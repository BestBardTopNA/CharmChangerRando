﻿using System.Collections.Generic;

namespace CharmChangerRando
{
    public class CharmRelations
    {
        public static Dictionary<string, (bool positive, string reference)> Relations = new()
        {
            { "grubsongDamageSoul", (true, null) },
            { "grubsongDamageSoulCombo", (true, null) },
            { "regularInvulnerability", (true, null) },
            { "regularRecoil", (false, null) },
            { "stalwartShellInvulnerability", (true, "regularInvulnerability") },
            { "stalwartShellRecoil", (false, "regularRecoil") },
            { "baldurShellKnockbackMult", (false, null) },
            { "baldurShellBlocks", (true, null) },
            { "furyOfTheFallenJonis", (true, null) },
            { "furyOfTheFallenHealth", (true, null) },
            { "furyOfTheFallenScaling", (true, null) },
            { "regularFocusTime", (false, null) },
            { "quickFocusFocusTime", (false, "regularFocusTime") },
            { "deepFocusHealingTimeScale", (false, null) },
            { "regularFocusHealing", (true, null) },
            { "deepFocusHealing", (true, "regularFocusHealing") },
            { "lifebloodHeartLifeblood", (true, null) },
            { "lifebloodCoreLifeblood", (true, null) },
            { "defendersCrestDiscount", (true, null) },
            { "defendersCrestCloudFrequency", (true, null) },
            { "defendersCrestDamageRate", (false, null) },
            { "flukenestDamage", (true, null) },
            { "flukenestShamanStoneDamage", (true, "flukenestDamage") },
            { "flukenestVSFlukes", (true, null) },
            { "flukenestSSFlukes", (true, "flukenestVSFlukes") },
            { "flukenestFlukeSizeMin", (true, null) },
            { "flukenestFlukeSizeMax", (true, "flukenestFlukeSizeMin") },
            { "flukenestShamanStoneFlukeSizeMin", (true, "flukenestFlukeSizeMin") },
            { "flukenestShamanStoneFlukeSizeMax", (true, "flukenestShamanStoneFlukeSizeMin") },
            { "flukenestDefendersCrestDamageRate", (false, "defendersCrestDamageRate") },
            { "flukenestDefendersCrestShamanStoneDamageRate", (false, "flukenestDefendersCrestDamageRate") },
            { "thornsOfAgonyDamageMultiplier", (true, null) },
            { "longnailMarkOfPrideWallSlash", (false, null) },
            { "longnailMarkOfPrideScale", (true, "markOfPrideScale") },
            { "markOfPrideScale", (true, "longnailScale") },
            { "longnailScale", (true, null) },
            { "heavyBlowWallSlash", (false, "regularSlashRecoil") },
            { "heavyBlowCycloneSlash", (true, "regularCycloneSlashRecoil") },
            { "regularSlashRecoil", (true, null) },
            { "regularGreatSlashRecoil", (true, null) },
            { "regularCycloneSlashRecoil", (true, null) },
            { "heavyBlowSlashRecoil", (true, "regularSlashRecoil") },
            { "heavyBlowGreatSlashRecoil", (true, "regularGreatSlashRecoil") },
            { "heavyBlowCycloneSlashRecoil", (true, "regularCycloneSlashRecoil") },
            { "heavyBlowStagger", (true, null) },
            { "heavyBlowStaggerCombo", (true, null) },
            { "SharpShadowDamageMultiplier", (true, null) },
            { "SharpShadowDashmasterDamageIncrease", (true, null) },
            { "SharpShadowDashSpeed", (true, null) },
            { "sporeShroomDamageResetsCooldown", (true, null) },
            { "sporeShroomCooldown", (false, null) },
            { "sporeShroomCloudDuration", (true, null) },
            { "sporeShroomDamageRate", (false, null) },
            { "sporeShroomDefendersCrestDamageRate", (false, "sporeShroomDamageRate") },
            { "regularVSSizeScaleX", (true, null) },
            { "regularVSSizeScaleY", (true, null) },
            { "regularSSSizeScaleX", (true, "regularVSSizeScaleX") },
            { "regularSSSizeScaleY", (true, "regularVSSizeScaleY") },
            { "shamanStoneVSSizeScaleX", (true, "regularVSSizeScaleX") },
            { "shamanStoneVSSizeScaleY", (true, "regularVSSizeScaleY") },
            { "shamanStoneSSSizeScaleX", (true, "shamanStoneVSSizeScaleX") },
            { "shamanStoneSSSizeScaleY", (true, "shamanStoneVSSizeScaleY") },
            { "regularVSDamage", (true, null) },
            { "regularSSDamage", (true, "regularVSDamage") },
            { "regularHWDamage", (true, null) },
            { "regularASDamage", (true, "regularHWDamage") },
            { "regularDiveDamage", (true, null) },
            { "regularDDiveDamage", (true, null) },
            { "regularDDarkDamageL", (true, null) },
            { "regularDDarkDamageR", (true, null) },
            { "regularDDarkDamageMega", (true, null) },
            { "shamanStoneVSDamage", (true, "regularVSDamage") },
            { "shamanStoneSSDamage", (true, "regularSSDamage") },
            { "shamanStoneHWDamage", (true, "regularHWDamage") },
            { "shamanStoneASDamage", (true, "regularASDamage") },
            { "shamanStoneDiveDamage", (true, "regularDiveDamage") },
            { "shamanStoneDDiveDamage", (true, "regularDDiveDamage") },
            { "shamanStoneDDarkDamageL", (true, "regularDDarkDamageL") },
            { "shamanStoneDDarkDamageR", (true, "regularDDarkDamageR") },
            { "shamanStoneDDarkDamageMega", (true, "regularDDarkDamageMega") },
            { "regularSoul", (true, null) },
            { "regularReservesSoul", (true, null) },
            { "soulCatcherSoul", (true, "regularSoul") },
            { "soulCatcherReservesSoul", (true, "regularReservesSoul") },
            { "soulEaterSoul", (true, "soulCatcherSoul") },
            { "soulEaterReservesSoul", (true, "soulCatcherReservesSoul") },
            { "glowingWombSpawnRate", (false, null) },
            { "glowingWombSpawnCost", (false, null) },
            { "glowingWombSpawnTotal", (true, null) },
            { "glowingWombDamage", (true, null) },
            { "glowingWombFuryOfTheFallenDamage", (true, "glowingWombDamage") },
            { "glowingWombDefendersCrestDamage", (true, "glowingWombDamage") },
            { "glowingWombDefendersCrestDamageRate", (false, null) },
            { "fragileCharmsBreak", (false, null) },
            { "greedGeoIncrease", (true, null) },
            { "strengthDamageIncrease", (true, null) },
            { "regularChargeTime", (false, null) },
            { "nailmastersGloryChargeTime", (false, "regularChargeTime") },
            { "jonisBlessingScaling", (true, null) },
            { "shapeOfUnnSpeed", (true, null) },
            { "shapeOfUnnQuickFocusSpeed", (true, "shapeOfUnnSpeed") },
            { "hivebloodTimer", (false, null) },
            { "hivebloodJonisTimer", (false, "hivebloodTimer") },
            { "regularDreamSoul", (true, null) },
            { "dreamWielderSoulGain", (true, "regularDreamSoul") },
            { "dreamWielderEssenceChanceLow", (false, "dreamWielderEssenceChanceHigh") },
            { "dreamWielderEssenceChanceHigh", (false, null) },
            { "dashmasterDownwardDash", (true, null) },
            { "regularDashCooldown", (false, null) },
            { "dashmasterDashCooldown", (false, "regularDashCooldown") },
            { "regularAttackCooldown", (false, null) },
            { "quickSlashAttackCooldown", (false, "regularAttackCooldown") },
            { "regularSpellCost", (false, null) },
            { "spellTwisterSpellCost", (false, "regularSpellCost") },
            { "grubberflysElegyJoniBeamDamageBool", (false, null) },
            { "grubberflysElegyDamageScale", (true, null) },
            { "grubberflysElegyFuryOfTheFallenScaling", (true, "grubberflysElegyDamageScale") },
            { "grubberflysElegyMarkOfPrideScale", (true, null) },
            { "kingsoulSoulGain", (true, null) },
            { "kingsoulSoulTime", (false, null) },
            { "regularWalkSpeed", (true, null) },
            { "regularSpeed", (true, null) },
            { "sprintmasterSpeed", (true, "regularSpeed") },
            { "sprintmasterSpeedCombo", (true, "sprintmasterSpeed") },
            { "dreamshieldNoise", (false, null) },
            { "dreamshieldDamageScale", (true, null) },
            { "dreamshieldReformationTime", (false, null) },
            { "dreamshieldSizeScale", (true, null) },
            { "dreamshieldDreamWielderSizeScale", (true, "dreamshieldSizeScale") },
            { "dreamshieldSpeed", (true, null) },
            { "dreamshieldFocusSpeed", (true, "dreamshieldSpeed") },
            { "weaversongCount", (true, null) },
            { "weaversongDamage", (true, null) },
            { "weaversongSpeedMin", (true, null) },
            { "weaversongSpeedMax", (true, "weaversongSpeedMin") },
            { "weaversongSpeedSprintmaster", (true, null) },
            { "weaversongGrubsongSoul", (true, null) },
            { "grimmchildDamage2", (true, null) },
            { "grimmchildDamage3", (true, "grimmchildDamage2") },
            { "grimmchildDamage4", (true, "grimmchildDamage3") },
            { "grimmchildAttackTimer", (false, null) },
            { "carefreeMelodyChancetrue", (true, null) },
            { "carefreeMelodyChance2", (true, "carefreeMelodyChancetrue") },
            { "carefreeMelodyChance3", (true, "carefreeMelodyChance2") },
            { "carefreeMelodyChance4", (true, "carefreeMelodyChance3") },
            { "carefreeMelodyChance5", (true, "carefreeMelodyChance4") },
            { "carefreeMelodyChance6", (true, "carefreeMelodyChance5") },
            { "carefreeMelodyChance7", (true, "carefreeMelodyChance6") }
        };
    }
}
