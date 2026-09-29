using BiomeExtractorsMod.Content.Tiles;
using BiomeExtractorsMod.CrossMod.Calamity.Content.TileEntities;

namespace BiomeExtractorsMod.CrossMod.Calamity.Content.Tiles
{
    internal abstract class BiomeExtractorTileAbyss : BiomeExtractorTile
    {
        protected override int GetAnimationFrame(int type, int i, int j)
        {
            bool found = TileUtils.TryGetTileEntityAs(i, j, out BiomeExtractorEntAbyss entity);
            if (!found || entity.PressureLock)
            {
                return IdleFrame;
            }
            return base.GetAnimationFrame(type, i, j);
        }
    }
}
