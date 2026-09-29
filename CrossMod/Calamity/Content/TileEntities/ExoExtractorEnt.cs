using BiomeExtractorsMod.Content.TileEntities;
using BiomeExtractorsMod.Common.Database;
using Terraria.Localization;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;
using BiomeExtractorsMod.CrossMod.Calamity.Content.Tiles;

namespace BiomeExtractorsMod.CrossMod.Calamity.Content.TileEntities
{
    [ExtendsFromMod("CalamityMod")]
    [JITWhenModsEnabled("CalamityMod")]
    internal class ExoExtractorEnt : BiomeExtractorEnt
    {
        protected internal override string LocalName => Language.GetTextValue($"{BiomeExtractorsMod.LocExtractorSuffix("Exo")}");
        protected internal override int TileType => ModContent.TileType<ExoExtractorTile>();
        protected internal override ExtractionTier ExtractionTier => Instance.GetTier(ExtractionTiers.EXO, true);
    }
}
