using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.Common.Hooks;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;

namespace BiomeExtractorsMod.CrossMod.Arbour.Common.Database;

[ExtendsFromMod(modName)]
public class ArbourExtractionSystem : ExtractionSystemExtension
{
    public static ArbourExtractionSystem Instance => ModContent.GetInstance<ArbourExtractionSystem>();

    public const string modName = "Arbour";

    #region IDs
    public static readonly string arbor = "arbor";
    public static readonly string arbor_spc = "arbor_spc";
    #endregion

    #region Checks
    // BLOCK LISTS
    public static ushort[] arbor_blocks = [ModContent.Find<ModTile>(modName, "ArborGrass").Type, 
        ModContent.Find<ModTile>(modName, "ArborVines").Type, ModContent.Find<ModTile>(modName, "ArborLeaf").Type
    ];
    
    // BLOCKS
    static readonly Predicate<ScanData> arbor200 = scan => scan.Tiles(arbor_blocks) >= 200;
    #endregion
    
    #region Database Setup
    private static string LocalizeAs(string suffix) => BiomeExtractionSystem.LocalizeAs(suffix);

    public override void LoadDatabase()
    {
        /*if (ModContent.TryFind(modName, "ArborGrass", out ModTile arborGrass))
            arbor_blocks.Append(arborGrass.Type);*/

        SetupArbor();  
    }

    private static void SetupArbor()
    {
        BES.AddPool(arbor, 5500, true, LocalizeAs(arbor));
        BES.AddPool(arbor_spc, 5500, true, LocalizeAs(arbor));

        BES.AddPoolParent(arbor, sky);
        BES.AddPoolParent(arbor_spc, space);

        BES.AddPoolBiomeChecks(arbor, inMainWorld, arbor200);
        BES.AddPoolBiomeChecks(arbor_spc, inMainWorld, arbor200);

        BES.AddItemInPool(arbor, ItemID.None, 75);
        
        // TERRAIN
        BES.SafelyGetModItem(arbor, modName, "ArborGrassSeeds", 16);

        // MATERIALS
        //BES.AddItemInPool(arbor, new ItemEntry((short)ModContent.ItemType<BirchWoodBlock>(), 1, 3), 120);
        BES.SafelyGetModItemEntry(arbor, modName, "BirchWoodBlock", 1, 3, 120);
        BES.AddItemInPool(arbor, ItemID.Acorn, 10);

        BES.AliasItemPool(arbor_spc, arbor);
    }
    #endregion
}