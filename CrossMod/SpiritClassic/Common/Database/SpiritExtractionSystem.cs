using System;
using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.Common.Hooks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;

/*namespace BiomeExtractorsMod.CrossMod.SpiritClassic.Common.Database;

[ExtendsFromMod("SpiritMod")]
public class SpiritExtractionSystem : ExtractionSystemExtension
{
    public static SpiritExtractionSystem Instance => ModContent.GetInstance<SpiritExtractionSystem>();
    
    #region IDs
    public static readonly string briar = "briar";
    public static readonly string briar_night = "briar_night";
    public static readonly string briar_hm = "briar_hm";
    public static readonly string ug_briar = "ug_briar";
    public static readonly string ug_briar_hm = "ug_briar_hm";
    #endregion
    
    #region Checks
    // BLOCK LISTS
    public static readonly ushort[] briar_blocks = [(ushort)ModContent.TileType<BriarGrass>(),
        (ushort)ModContent.TileType<ReachGrassTile>(), (ushort)ModContent.TileType<BarkTileTile>(),
        (ushort)ModContent.TileType<LivingBriarWood>()
    ];
    
    // PROGRESSION
    private static readonly Predicate<ScanData> nightTime = scan => !Main.dayTime;
    
    // BLOCKS
    static readonly Predicate<ScanData> briar200 = scan => scan.Tiles(briar_blocks) >= 200;
    
    #endregion

    #region Database setup
    private static string LocalizeAs(string suffix) => BiomeExtractionSystem.LocalizeAs(suffix);
    
    public override void LoadDatabase()
    {
        //SetupBriar();
    }
    // TODO
    private void SetupBriar()
    {
        BES.AddPool(briar, 350, LocalizeAs(briar));
        BES.AddPool(ug_briar, 550, LocalizeAs(ug_briar));
        
        BES.AddPoolRequirements(briar, briar200);
        BES.AddPoolRequirements(ug_briar, belowSurfaceLayer, briar200);
        BES.AddPoolRequirements(briar_night, nightTime, briar200);
        
        BES.AddItemInPool(briar, ItemID.None, 64);
        BES.AddItemInPool(briar, ItemID.DirtBlock, 14);
        BES.AddItemInPool(briar, (short)ModContent.ItemType<BriarGrassSeeds>(), 5);
        BES.AddItemInPool(briar, new ItemEntry((short)ModContent.ItemType<AncientBark>(), 1, 3), 32);
        BES.AddItemInPool(briar, (short)ModContent.ItemType<BlubbyItem>(), 3);
        BES.AddItemInPool(briar, (short)ModContent.ItemType<BriarInchwormItem>(), 3);
        BES.AddItemInPool(briar, (short)ModContent.ItemType<BriarmothItem>(), 3);
        BES.AddItemInPool(briar, (short)ModContent.ItemType<ReachFishingCatch>(), 2);
        
        BES.AddItemInPool(briar_night, (short)ModContent.ItemType<EnchantedLeaf>(), 8);
    }
    #endregion
}*/