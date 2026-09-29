using BiomeExtractorsMod.Content.Items;
using BiomeExtractorsMod.Content.Tiles;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityMod.Items.Placeables.Abyss;
using BiomeExtractorsMod.CrossMod.Calamity.Content.Tiles;

namespace BiomeExtractorsMod.CrossMod.Calamity.Content.Items
{
    [ExtendsFromMod("CalamityMod")]
    [JITWhenModsEnabled("CalamityMod")]
    internal class SulphuricUpgradeKit : ExtractorModificationKit
    {
        protected override int[] TargetTiles => [ModContent.TileType<BiomeExtractorTileDemonic>()];

        protected override int ResultTile => ModContent.TileType<SulphuricExtractorTile>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.SetShopValues(ItemRarityColor.Orange3, Item.buyPrice(gold: 7)); // sell at 1.4
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 5)
                .AddIngredient(ModContent.ItemType<SulphurousShale>(), 12)
                .AddTile(TileID.TinkerersWorkbench)
                .Register();
        }
    }
}
