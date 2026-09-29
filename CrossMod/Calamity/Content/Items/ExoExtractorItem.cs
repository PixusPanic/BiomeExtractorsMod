using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.Content.Items;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using BiomeExtractorsMod.CrossMod.Calamity.Common;
using BiomeExtractorsMod.CrossMod.Calamity.Content.Tiles;

namespace BiomeExtractorsMod.CrossMod.Calamity.Content.Items
{
    [ExtendsFromMod("CalamityMod")]
    [JITWhenModsEnabled("CalamityMod")]
    internal class ExoExtractorItem : BiomeExtractorItem
    {
        protected internal override int TileId => ModContent.TileType<ExoExtractorTile>();
        protected internal override ExtractorUpgradeKit UpgradeItemToCraftThis => ModContent.GetInstance<ExoUpgradeKit>();

        public override void SetDefaults()
        {
            base.SetDefaults();
            BiomeExtractionSystem.Instance.AddTier(ExtractionTiers.EXO, $"{BiomeExtractorsMod.LocArticles}.Exo", $"{BiomeExtractorsMod.LocTiers}.Exo", delegate { return CalamityConfigs.Instance.ExoExtractorRate; }, delegate { return CalamityConfigs.Instance.ExoExtractorChance; }, delegate { return CalamityConfigs.Instance.ExoExtractorAmount; }, delegate { return Mod.Assets.Request<Texture2D>("Calamity/Content/MapIcons/ExoExtractorIcon"); });
            Item.rare = ModContent.RarityType<DarkOrange>();
            Item.value = Item.buyPrice(platinum: 1); // sell at 20
        }
    }
}
