using System;
using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.Common.Hooks;
using Terraria.ID;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;

namespace BiomeExtractorsMod.CrossMod.Verdant.Common.Database;

[ExtendsFromMod(modName)]
public class VerdantExtractionSystem : ExtractionSystemExtension
{
    public static VerdantExtractionSystem Instance => ModContent.GetInstance<VerdantExtractionSystem>();

    public const string modName = "Verdant";

    #region IDs
    public static readonly string verdant = "verdant";
    //public static readonly string ug_verdant = "ug_verdant";
    public static readonly string verdant_hm = "verdant_hm";
    #endregion
    
    #region Checks
    // BLOCK LISTS
    //TODO - When Verdant makes these classes public, make this (ushort)ModContent.TileType<ModTile>()
    public static readonly ushort[] verdant_blocks = [ModContent.Find<ModTile>(modName, "LushSoil").Type, 
        ModContent.Find<ModTile>(modName, "EmbeddedAquamarine").Type, ModContent.Find<ModTile>(modName, "VerdantGrassLeaves").Type,
        ModContent.Find<ModTile>(modName, "VerdantLeaves").Type, ModContent.Find<ModTile>(modName, "LivingLushWood").Type
    ];
    
    // BLOCKS
    static readonly Predicate<ScanData> verdant250 = scan => scan.Tiles(verdant_blocks) >= 250;
        
    #endregion
    
    #region Database Setup
    private static string LocalizeAs(string suffix) => BiomeExtractionSystem.LocalizeAs(suffix);

    public override void LoadDatabase()
    {
        SetupVerdant();
        
        BES.SafelyGetModItem(underground, modName, "AquarmarineItem", 4);
        BES.SafelyGetModItem(caverns, modName, "AquarmarineItem", 4);
    }

    private static void SetupVerdant()
    {
        BES.AddPool(verdant, 1750, LocalizeAs(verdant));
        //BES.AddPool(ug_verdant, 1750, LocalizeAs(ug_verdant));
        BES.AddPool(verdant_hm, 2050, true);
        
        BES.AddPoolBiomeChecks(verdant, inMainWorld, verdant250);
        //BES.AddPoolBiomeChecks(ug_verdant, inMainWorld, belowSurfaceLayer, verdant250);
        BES.AddPoolBiomeChecks(verdant_hm, inMainWorld, verdant250);

        BES.AddItemInPool(verdant, ItemID.None, 75);
        
        // TERRAIN
        BES.SafelyGetModItem(verdant, modName, "LushSoilBlock", 26);
        //BES.AddItemInPool(verdant, (short)ModContent.ItemType<VerdantWoodBlock>(), 16);
        BES.SafelyGetModItem(verdant_hm, modName, "MysteriaWood", 14);

        // MINERALS
        BES.SafelyGetModItem(verdant, modName, "AquarmarineItem", 8);

        // CRITTERS AND BAITS
        BES.SafelyGetModItem(verdant, modName, "FlotieItem", 6);
        BES.SafelyGetModItem(verdant, modName, "FlotinyItem", 8);
        BES.SafelyGetModItem(verdant, modName, "BulbSnail", 7);
        
        BES.SafelyGetModItem(verdant, modName, "AxolotlItem", 4);
        BES.SafelyGetModItem(verdant, modName, "BulbboxJellyItem", 3);
        BES.SafelyGetModItem(verdant, modName, "MossCarpItem", 3);
        BES.SafelyGetModItem(verdant, modName, "PoolwormItem", 5);
        
        // MISC
        BES.SafelyGetModItem(verdant, modName, "LushLeaf", 22);

        //BES.AliasItemPool(ug_verdant, verdant);
    }
    #endregion
}