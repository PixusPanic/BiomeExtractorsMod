using BiomeExtractorsMod.Content.TileEntities;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;
using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.CrossMod.Calamity.Content.Tiles;

namespace BiomeExtractorsMod.CrossMod.Calamity.Content.TileEntities
{
    [ExtendsFromMod("CalamityMod")]
    [JITWhenModsEnabled("CalamityMod")]
    internal class SpectralExtractorEnt : BiomeExtractorEnt
    {
        protected internal override int TileType => ModContent.TileType<SpectralExtractorTile>();
        protected internal override ExtractionTier ExtractionTier => Instance.GetTier(ExtractionTiers.SPECTRAL, true);
    }
}
