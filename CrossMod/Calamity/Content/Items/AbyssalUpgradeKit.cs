using BiomeExtractorsMod.Content.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;
using CalamityMod.Items.Placeables.Abyss;
using BiomeExtractorsMod.CrossMod.Calamity.Content.Tiles;

namespace BiomeExtractorsMod.CrossMod.Calamity.Content.Items
{
    [ExtendsFromMod("CalamityMod")]
    [JITWhenModsEnabled("CalamityMod")]
    internal class AbyssalUpgradeKit : ExtractorModificationKit
    {
        protected override int[] TargetTiles => [ModContent.TileType<ThermoresistantExtractorTile>()];

        protected override int ResultTile => ModContent.TileType<AbyssalExtractorTile>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<PureGreen>();
            Item.value = Item.buyPrice(gold: 20); // sell at 4
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ReaperTooth>(), 5)
                .AddIngredient(ModContent.ItemType<Voidstone>(), 6)
                .AddIngredient(ModContent.ItemType<Necroplasm>(), 6)
                .AddTile(ModContent.TileType<VoidCondenser>())
                .Register();
        }
    }
}
