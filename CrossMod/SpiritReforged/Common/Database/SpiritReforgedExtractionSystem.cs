using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.Common.Hooks;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;

namespace BiomeExtractorsMod.CrossMod.SpiritReforged.Common.Database;

[ExtendsFromMod(modName)]
public class SpiritReforgedExtractionSystem : ExtractionSystemExtension
{
    public static SpiritReforgedExtractionSystem Instance => ModContent.GetInstance<SpiritReforgedExtractionSystem>();

    public const string modName = "SpiritReforged";

    #region IDs
    public static readonly string savanna = "savanna";
    public static readonly string savanna_night = "savanna_night";
    
    public static readonly string corrupt_savanna = "corrupt_savanna";
    public static readonly string crimson_savanna = "crimson_savanna";
    public static readonly string hallowed_savanna = "hallowed_savanna";

    public static readonly string salt_flats = "salt_flats";
    #endregion

    #region Checks
    // BLOCK LISTS
    public static readonly ushort[] savanna_blocks = [ModContent.Find<ModTile>(modName, "SavannaDirt").Type,
        ModContent.Find<ModTile>(modName, "SavannaGrass").Type, TileID.Sand, TileID.HardenedSand, TileID.Sandstone];

    public static readonly ushort[] salt_flat_blocks = [ModContent.Find<ModTile>(modName, "SaltBlockReflective").Type, ModContent.Find<ModTile>(modName, "SaltBlockDull").Type];
    
    // PROGRESSION
    private static readonly Predicate<ScanData> nightTime = scan => !Main.dayTime;
    
    // BLOCKS
    static readonly Predicate<ScanData> savanna250 = scan => scan.Tiles(savanna_blocks) >= 250;
    static readonly Predicate<ScanData> salt_flats250 = scan => scan.Tiles(salt_flat_blocks) >= 250;
    #endregion

    #region Database setup
    private static string LocalizeAs(string suffix) => BiomeExtractionSystem.LocalizeAs(suffix); 
    
    public override void LoadDatabase()
    {
        SetupSavanna();
        SetupSaltFlats();
        //SetupCorruptSavanna();
        //SetupCrimsonSavanna();
        //SetupHallowedSavanna();
    }

    private void SetupSavanna()
    {
        BES.AddPool(savanna, 650, LocalizeAs(savanna));
        
        BES.AddPoolBiomeChecks(savanna, inMainWorld, surfaceLayer, savanna250);
        //BES.AddPoolRequirements(savanna_night, inMainWorld, surfaceLayer, nightTime, savanna250);
        
        BES.AddItemInPool(savanna, ItemID.None, 64);
        // TERRAIN:18
        BES.SafelyGetModItem(savanna, modName, "SavannaDirt", 72);
        BES.AddItemInPool(savanna, ItemID.HardenedSand, 18);
        BES.AddItemInPool(savanna, ItemID.SandBlock, 20);
        BES.AddItemInPool(savanna, ItemID.Sandstone, 10);
        BES.AddItemInPool(savanna, ItemID.ClayBlock, 7);
        // MATERIALS:40
        BES.SafelyGetModItemEntry(savanna, modName, "Drywood", 1, 3, 120);
        BES.AddItemInPool(savanna, ItemID.Acorn, 20);
        // VEGETATION:11
        BES.SafelyGetModItem(savanna, modName, "SavannaGrassSeeds", 16);
        //BES.AddItemInPool(savanna, ItemID.Daybloom, 25);
        //BES.AddItemInPool(savanna, ItemID.Mushroom, 25);
        // COLORS: 7
        BES.AddItemInPool(savanna, ItemID.SkyBlueFlower, 21);
    }

    private void SetupSaltFlats()
    {
        BES.AddPool(salt_flats, 1750, LocalizeAs(salt_flats));

        BES.AddPoolBiomeChecks(salt_flats, inMainWorld, surfaceLayer, salt_flats250);

        BES.AddItemInPool(salt_flats, ItemID.None, 64);

        BES.SafelyGetModItem(salt_flats, modName, "SaltBlockReflective", 48);
        BES.SafelyGetModItem(salt_flats, modName, "SaltBlockDull", 72);

        BES.SafelyGetModItemEntry(salt_flats, modName, "Drywood", 1, 2, 160);
        BES.AddItemInPool(salt_flats, ItemID.Acorn, 5);
        
        BES.SafelyGetModItem(salt_flats, modName, "BrineShrimp", 4);
    }

    #endregion
}