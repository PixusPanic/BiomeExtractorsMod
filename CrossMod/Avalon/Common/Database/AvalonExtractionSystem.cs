using System;
using System.Collections.Generic;
using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.Common.Hooks;
using Terraria.ID;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;

/*namespace BiomeExtractorsMod.CrossMod.Avalon.Common.Database;

[ExtendsFromMod("Avalon")]
public class AvalonExtractionSystem : ExtractionSystemExtension
{
    public static AvalonExtractionSystem Instance => ModContent.GetInstance<AvalonExtractionSystem>();

    #region IDs
    public static readonly string ug_bact_prime = "ug_bact_prime";
    public static readonly string ug_skeletron = "ug_skeletron";
    public static readonly string dungeon_o = "dungeon_o";
    public static readonly string dungeon_y = "dungeon_y";
    public static readonly string dungeon_p = "dungeon_p";
    public static readonly string ug_dst_beak = "ug_dst_beak";
    public static readonly string ug_mechs = "ug_mechs";

    // subelements
    public static readonly string post_skeletron = "post_skeletron";
    public static readonly string post_desert_beak = "post_desert_beak";
    #endregion

    #region Checks
    // BLOCK LISTS
    //public static readonly ushort[] contagion_blocks = [];

    #region Dungeon
    public static readonly List<ushort> dungeonAvalonBricks = [TileID.BlueDungeonBrick, TileID.GreenDungeonBrick, TileID.PinkDungeonBrick, (ushort)ModContent.TileType<global::Avalon.Tiles.OrangeBrick>(), (ushort)ModContent.TileType<global::Avalon.Tiles.YellowBrick>(), (ushort)ModContent.TileType<global::Avalon.Tiles.PurpleBrick>()];
    public static readonly List<ushort> dungeonAvalonWalls = [WallID.BlueDungeonUnsafe, WallID.BlueDungeonSlabUnsafe, WallID.BlueDungeonTileUnsafe, WallID.GreenDungeonUnsafe, WallID.GreenDungeonSlabUnsafe, WallID.GreenDungeonTileUnsafe, WallID.PinkDungeonUnsafe, WallID.PinkDungeonSlabUnsafe, WallID.PinkDungeonTileUnsafe,
        (ushort)ModContent.WallType<OrangeBrickUnsafe>(), (ushort)ModContent.WallType<OrangeSlabUnsafe>(), (ushort)ModContent.WallType<OrangeTiledUnsafe>(), (ushort)ModContent.WallType<YellowBrickUnsafe>(), (ushort)ModContent.WallType<YellowSlabWallUnsafe>(), (ushort)ModContent.WallType<YellowTiledWallUnsafe>(), (ushort)ModContent.WallType<PurpleBrickUnsafe>(), (ushort)ModContent.WallType<PurpleSlabWallUnsafe>(), (ushort)ModContent.WallType<PurpleTiledWallUnsafe>()];
    
    public static readonly List<ushort> dungeonWallsOrange = [(ushort)ModContent.WallType<OrangeBrickUnsafe>(), (ushort)ModContent.WallType<OrangeSlabUnsafe>(), (ushort)ModContent.WallType<OrangeTiledUnsafe>()];
    public static readonly List<ushort> dungeonWallsYellow = [(ushort)ModContent.WallType<YellowBrickUnsafe>(), (ushort)ModContent.WallType<YellowSlabWallUnsafe>(), (ushort)ModContent.WallType<YellowTiledWallUnsafe>()];
    public static readonly List<ushort> dungeonWallsPurple = [(ushort)ModContent.WallType<PurpleBrickUnsafe>(), (ushort)ModContent.WallType<PurpleSlabWallUnsafe>(), (ushort)ModContent.WallType<PurpleTiledWallUnsafe>()];
    #endregion
    
    //TIERS
    //static readonly Predicate<ScanData> demonic = scan => scan.MinTier(ExtractionTiers.DEMONIC);
    //static readonly Predicate<ScanData> infernal = scan => scan.MinTier(ExtractionTiers.INFERNAL);
    
    // PROGRESSION
    public static readonly Predicate<ScanData> postBacteriumPrime =
        scan => ModContent.GetInstance<DownedBossSystem>().DownedBacteriumPrime;
    public static readonly Predicate<ScanData> postDesertBeak =
        scan => ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak;
    
    public static readonly Predicate<ScanData> dungeonAvalon250 = scan => scan.Tiles(dungeonAvalonBricks) >= 250;
    public static readonly Predicate<ScanData> dungeon_o250 = scan => scan.Tiles((ushort)ModContent.TileType<global::Avalon.Tiles.OrangeBrick>()) >= 250;
    public static readonly Predicate<ScanData> dungeon_y250 = scan => scan.Tiles((ushort)ModContent.TileType<global::Avalon.Tiles.YellowBrick>()) >= 250;
    public static readonly Predicate<ScanData> dungeon_p250 = scan => scan.Tiles((ushort)ModContent.TileType<global::Avalon.Tiles.PurpleBrick>()) >= 250;
    
    // WALLS
    public static readonly Predicate<ScanData> dungeonAvalon_bg = scan => scan.ValidWalls(dungeonAvalonWalls);
    public static readonly Predicate<ScanData> dungeon_bg_o = scan => scan.ValidWalls(dungeonWallsOrange);
    public static readonly Predicate<ScanData> dungeon_bg_y = scan => scan.ValidWalls(dungeonWallsYellow);
    public static readonly Predicate<ScanData> dungeon_bg_p = scan => scan.ValidWalls(dungeonWallsPurple);
    #endregion
    
    #region Misc
    /// I need to do this since it's not a static variable
    private static bool desertBeak = ModContent.GetInstance<DownedBossSystem>().DownedDesertBeak;

    public static readonly Condition DownedDesertBeak = new Condition("Drops.DownedDB", () => desertBeak);
    
    private static Condition Create(string key, Func<bool> predicate)
    {
        return new Condition(Language.GetText("Mods.Avalon.Condition." + key), predicate);
    }
    #endregion
    
    #region Database setup
    public override void LoadDatabase()
    {
        ExpandVanillaPools();
        //SetupContagion();
    }

    private static void ExpandVanillaPools()
    {
        ExpandUnderground();
        ExpandDungeon();
    }

    private static void ExpandUnderground()
    {
        // PRE-HARDMODE MINERALS
        BES.AddItemInPool(underground, (short)ModContent.ItemType<BronzeOre>(), 14);
        BES.AddItemInPool(underground, (short)ModContent.ItemType<NickelOre>(), 12);
        BES.AddItemInPool(underground, (short)ModContent.ItemType<ZincOre>(), 11);
        BES.AddItemInPool(underground, (short)ModContent.ItemType<BismuthOre>(), 10);
        
        BES.AddItemInPool(underground, (short)ModContent.ItemType<Heartstone>(), 7);
        BES.AddItemInPool(underground, (short)ModContent.ItemType<Starstone>(), 7);
        BES.AddItemInPool(underground, (short)ModContent.ItemType<Boltstone>(), 7);
        
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<BronzeOre>(), 14);
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<NickelOre>(), 12);
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<ZincOre>(), 11);
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<BismuthOre>(), 10);
        
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<Heartstone>(), 7);
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<Starstone>(), 7);
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<Boltstone>(), 7);
        
        // GEMS
        BES.AddItemInPool(underground, (short)ModContent.ItemType<Peridot>(), 5);
        BES.AddItemInPool(underground, (short)ModContent.ItemType<Tourmaline>(), 5);
        BES.AddItemInPool(underground, (short)ModContent.ItemType<Zircon>(), 5);
        
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<Peridot>(), 5);
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<Tourmaline>(), 5);
        BES.AddItemInPool(caverns, (short)ModContent.ItemType<Zircon>(), 5);
        
        // HARDMODE MINERALS
        BES.AddItemInPool(hm_ores, (short)ModContent.ItemType<DurataniumOre>(), 20);
        BES.AddItemInPool(hm_ores, (short)ModContent.ItemType<NaquadahOre>(), 18);
        BES.AddItemInPool(hm_ores, (short)ModContent.ItemType<TroxiniumOre>(), 16);
        
        // POST-SKELETRON
        BES.AddPool(ug_skeletron, 75);

        BES.AddPoolParent(ug_skeletron, underground, SubLocalizeAs(post_skeletron));

        BES.AddPoolAccessRequirements(ug_skeletron, postSkeletron);

        BES.AddItemInPool(ug_skeletron, (short)ModContent.ItemType<Sulphur>(), 9);
        
        // POST-DESERT BEAK
        BES.AddPool(ug_dst_beak, 100);

        BES.AddPoolParent(ug_dst_beak, underground, SubLocalizeAs(post_desert_beak));

        BES.AddPoolVisibilityRequirements(ug_dst_beak, demonic);

        BES.AddPoolBiomeChecks(ug_dst_beak, inMainWorld, belowSurfaceLayer);

        BES.AddPoolAccessRequirements(ug_dst_beak, postDesertBeak);

        BES.AddItemInPool(ug_dst_beak, (short)ModContent.ItemType<RhodiumOre>(), 8);
        BES.AddItemInPool(ug_dst_beak, (short)ModContent.ItemType<OsmiumOre>(), 8);
        BES.AddItemInPool(ug_dst_beak, (short)ModContent.ItemType<IridiumOre>(), 8);
        
        // HALLOWED ORE
        /*BES.AddPool(ug_mechs, 126, [steampunk, postMechs], true);
        
        BES.AddPoolRequirements(ug_mechs, inMainWorld, purity100, belowSurfaceLayer);

        BES.AddItemInPool(ug_mechs, (short)ModContent.ItemType<HallowedOre>(), 5);
    }
    
    private static void ExpandDungeon()
    {
        // TODO
        /*BES.RemovePool(dungeon);
        BES.RemovePool(ectoplasm);
        
        BES.AddPool(dungeon, 2000, [demonic, postSkeletron], LocalizeAs(dungeon));
        BES.AddPool(ectoplasm, 2000, [cyber, postPlantera]);
        BES.AddPoolRequirements(dungeon, inMainWorld, dungeonAvalon250, belowSurfaceLayer, dungeonAvalon_bg);
        BES.AddPoolRequirements(ectoplasm, inMainWorld, dungeonAvalon250, belowSurfaceLayer, dungeonAvalon_bg);
        
        BES.AddPool(dungeon_o, 2000, [demonic, postSkeletron]);
        BES.AddPool(dungeon_y, 2000, [demonic, postSkeletron]);
        BES.AddPool(dungeon_p, 2000, [demonic, postSkeletron]);
        
        BES.AddPoolRequirements(dungeon_o, inMainWorld, dungeon_o250, belowSurfaceLayer, dungeon_bg_o);
        BES.AddPoolRequirements(dungeon_y, inMainWorld, dungeon_y250, belowSurfaceLayer, dungeon_bg_y);
        BES.AddPoolRequirements(dungeon_p, inMainWorld, dungeon_p250, belowSurfaceLayer, dungeon_bg_p);
        
        BES.AddItemInPool(dungeon_o, (short)ModContent.ItemType<OrangeBrick>(), 6);
        BES.AddItemInPool(dungeon_y, (short)ModContent.ItemType<YellowBrick>(), 6);
        BES.AddItemInPool(dungeon_p, (short)ModContent.ItemType<PurpleBrick>(), 6);
        
        BES.AddItemInPool(dungeon, ItemID.None, 48);
        BES.AddItemInPool(dungeon, ItemID.Spike, 3);
        BES.AddItemInPool(dungeon, (short)ModContent.ItemType<PoisonSpike>(), 2);
        BES.AddItemInPool(dungeon, ItemID.Bone, 18);
        BES.AddItemInPool(dungeon, ItemID.GoldenKey, 1);
        BES.AddItemInPool(ectoplasm, ItemID.Ectoplasm, 5);
    }
    
    private void SetupContagion()
    {
        // TODO
    }
    #endregion
}*/