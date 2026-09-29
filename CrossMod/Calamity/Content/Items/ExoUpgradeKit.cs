using BiomeExtractorsMod.Content.Items;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.ModLoader;
using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.CrossMod.Calamity.Content.Tiles;

namespace BiomeExtractorsMod.CrossMod.Calamity.Content.Items
{
    [ExtendsFromMod("CalamityMod")]
    [JITWhenModsEnabled("CalamityMod")]
    internal class ExoUpgradeKit : ExtractorUpgradeKit
    {
        protected override int Tier => ExtractionTiers.EXO;
        protected override int ResultTile => ModContent.TileType<ExoExtractorTile>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            Item.rare = ModContent.RarityType<DarkOrange>();
            Item.value = Item.buyPrice(gold: 25); // sell at 5
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ExoPrism>(), 5)
                .AddIngredient(ModContent.GetInstance<VoltageRegulationSystem>())
                .AddIngredient(ModContent.GetInstance<LongRangedSensorArray>())
                .AddIngredient(ModContent.GetInstance<AuricQuantumCoolingCell>())
                .AddTile(ModContent.TileType<DraedonsForge>())
                .Register();
        }
    }
}
