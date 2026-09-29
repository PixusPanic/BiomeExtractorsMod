using BiomeExtractorsMod.Common.Database;
using BiomeExtractorsMod.Common.Hooks;
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using static BiomeExtractorsMod.Common.Database.BiomeExtractionSystem;

namespace BiomeExtractorsMod.CrossMod.Confection.Common.Database;

[ExtendsFromMod(modName)]
public class ConfectionExtractionSystem : ExtractionSystemExtension
{
    public static ConfectionExtractionSystem Instance => ModContent.GetInstance<ConfectionExtractionSystem>();

    public const string modName = "TheConfectionRebirth";

    #region IDs
    public static readonly string confection_forest = "confection_forest";
    public static readonly string confection_desert = "confection_desert";
    public static readonly string confection_snow = "confection_snow";

    public static readonly string neapolinite_ore_forest = "neopolinite_bars_forest";
    public static readonly string neapolinite_ore_desert = "neopolinite_bars_desert";
    public static readonly string neapolinite_ore_snow = "neopolinite_bars_snow";

    public static readonly string ug_confection = "ug_confection";
    public static readonly string ug_confection_desert = "ug_confection_desert";
    public static readonly string ug_confection_snow = "ug_confection_snow";

    public static readonly string ug_neapolinite_ore = "ug_neopolinite_bars";
    public static readonly string ug_neapolinite_ore_desert = "ug_neopolinite_bars_desert";
    public static readonly string ug_neapolinite_ore_snow = "ug_neopolinite_bars_snow";

    public static readonly string confection_sky = "confection_sky";
    public static readonly string confection_spc = "confection_spc";
    #endregion

    #region Checks
    // BLOCK LISTS
    public static readonly List<ushort> confection_forest_blocks = [ModContent.Find<ModTile>(modName, "CookieBlock").Type,
        ModContent.Find<ModTile>(modName, "CreamGrass").Type, ModContent.Find<ModTile>(modName, "CreamGrassMowed").Type, ModContent.Find<ModTile>(modName, "Creamstone").Type,
    ];

    public static readonly List<ushort> confection_desert_blocks = [ModContent.Find<ModTile>(modName, "Creamsand").Type,
        ModContent.Find<ModTile>(modName, "Creamsandstone").Type, ModContent.Find<ModTile>(modName, "HardendedCreamsand").Type,
    ];

    public static readonly List<ushort> confection_snow_blocks = [ModContent.Find<ModTile>(modName, "CreamBlock").Type,
        ModContent.Find<ModTile>(modName, "BlueIce").Type,
    ];

    // BLOCKS
    //public static readonly Func<List<ushort>, Predicate<ScanData>> hallow125 = tiles => scan => scan.Tiles(tiles) - scan.Tiles(crimsonBlocks) - scan.Tiles(corruptBlocks) >= 125;

    #endregion

    #region Database Setup
    private static string LocalizeAs(string suffix) => BiomeExtractionSystem.LocalizeAs(suffix);

    public override void LoadDatabase()
    {
        SetupConfection();
        SetupHallowedExtra();
        SetupCrimsonExtra();
    }

    private static void SetupConfection()
    {
        SetupConfectionBase();
        SetupConfectionDesert();
        SetupConfectionSnow();

        SetupConfectionSky();
    }

    private static void SetupConfectionBase()
    {
        BES.AddPool(confection_forest, 100, LocalizeAs(confection_forest));
        BES.AddPool(neapolinite_ore_forest, 100);
        BES.AddPool(ug_confection, 1100, LocalizeAs(ug_confection));
        BES.AddPool(ug_neapolinite_ore, 1100);

        BES.AddPoolParent(neapolinite_ore_forest, confection_forest, SubLocalizeAs(post_mechs));
        BES.AddPoolParent(ug_neapolinite_ore, ug_confection, SubLocalizeAs(post_mechs));

        BES.AddPoolVisibilityRequirements(confection_forest, steampunk);
        BES.AddPoolVisibilityRequirements(neapolinite_ore_forest, steampunk);
        BES.AddPoolVisibilityRequirements(ug_confection, steampunk);
        BES.AddPoolVisibilityRequirements(ug_neapolinite_ore, steampunk);

        BES.AddPoolAccessRequirements(confection_forest, hardmodeOnly);
        BES.AddPoolAccessRequirements(neapolinite_ore_forest, postMechs);
        BES.AddPoolAccessRequirements(ug_confection, hardmodeOnly);
        BES.AddPoolAccessRequirements(ug_neapolinite_ore, postMechs);

        /*BES.AddPoolWorldChecks(ug_confection, notremix);
        BES.AddPoolAccessRequirements(ug_neapolinite_ore, notremix);*/

        BES.AddPoolBiomeChecks(confection_forest, inMainWorld, hallow125.Invoke(confection_forest_blocks));
        BES.AddPoolBiomeChecks(neapolinite_ore_forest, inMainWorld, hallow125.Invoke(confection_forest_blocks));
        BES.AddPoolBiomeChecks(confection_forest, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_forest_blocks));
        BES.AddPoolBiomeChecks(ug_neapolinite_ore, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_forest_blocks));

        BES.AddItemInPool(confection_forest, ItemID.None, 72);
        BES.SafelyGetModItem(confection_forest, modName, "CookieBlock", 26);
        BES.SafelyGetModItem(confection_forest, modName, "Creamstone", 7);
        //BES.SafelyGetModItem(confection_forest, ItemID.MudBlock, 8);
        BES.SafelyGetModItem(confection_forest, modName, "Sprinkles", 15);
        BES.SafelyGetModItem(confection_forest, modName, "CookieDough", 5);
        BES.SafelyGetModItem(confection_forest, modName, "SherbetBricks", 10);
        BES.AddItemInPool(confection_forest, ItemID.Gel, 11);
        BES.SafelyGetModItemEntry(confection_forest, modName, "CreamWood", 1, 3, 50);
        BES.SafelyGetModItem(confection_forest, modName, "CreamBeans", 7);
        BES.SafelyGetModItem(confection_forest, modName, "Birdnana", 1);
        BES.SafelyGetModItem(confection_forest, modName, "ChocolateBunn", 1);
        BES.SafelyGetModItem(confection_forest, modName, "ChocolateFrog", 1);
        BES.SafelyGetModItem(confection_forest, modName, "CherryBug", 2);
        BES.SafelyGetModItem(confection_forest, modName, "GrumbleBee", 2);
        BES.SafelyGetModItem(confection_forest, modName, "GummyWorm", 2);
        BES.SafelyGetModItem(confection_forest, modName, "Pip", 1);
        BES.SafelyGetModItem(confection_forest, modName, "RoyalCherryBug", 1);
        BES.SafelyGetModItem(confection_forest, modName, "PastryBlock", 5);
        //BES.SafelyGetModItem(neapolinite_bars_forest, (short)ModContent.ItemType<NeapoliniteBar>(), 1);
        BES.SafelyGetModItem(neapolinite_ore_forest, modName, "NeapoliniteOre", 5);
        /*BES.AliasItemPool(confection_forest_remix, confection_forest);
        BES.AliasItemPool(confection_bars_forest_remix, confection_bars_forest);*/

        BES.AddItemInPool(ug_confection, ItemID.None, 70);
        BES.SafelyGetModItem(ug_confection, modName, "CookieBlock", 20);
        BES.SafelyGetModItem(ug_confection, modName, "Creamstone", 6);
        BES.SafelyGetModItem(ug_confection, modName, "SoulofDelight", 24);
        BES.SafelyGetModItem(ug_confection, modName, "Saccharite", 20);
        //BES.AddItemInPool(ug_neapolinite_bars, (short)ModContent.ItemType<NeapoliniteBar>(), 1);
        BES.SafelyGetModItem(ug_neapolinite_ore, modName, "NeapoliniteOre", 5);
        /*BES.AliasItemPool(ug_confection_caverns_remix, ug_confection);
        BES.AliasItemPool(ug_confection_bars_remix, ug_confection_bars);*/
    }

    private static void SetupConfectionDesert()
    {
        BES.AddPool(confection_desert, 100, LocalizeAs(confection_desert));
        BES.AddPool(neapolinite_ore_desert, 100);
        BES.AddPool(ug_confection_desert, 1100, LocalizeAs(ug_confection_desert));
        BES.AddPool(ug_neapolinite_ore_desert, 1100);

        BES.AddPoolParent(neapolinite_ore_desert, confection_desert, SubLocalizeAs(post_mechs));
        BES.AddPoolParent(ug_neapolinite_ore_desert, ug_confection_desert, SubLocalizeAs(post_mechs));

        BES.AddPoolVisibilityRequirements(confection_desert, steampunk);
        BES.AddPoolVisibilityRequirements(neapolinite_ore_desert, steampunk);
        BES.AddPoolVisibilityRequirements(ug_confection, steampunk);
        BES.AddPoolVisibilityRequirements(ug_neapolinite_ore, steampunk);

        BES.AddPoolAccessRequirements(confection_forest, hardmodeOnly);
        BES.AddPoolAccessRequirements(neapolinite_ore_forest, postMechs);
        BES.AddPoolAccessRequirements(ug_confection_desert, hardmodeOnly);
        BES.AddPoolAccessRequirements(ug_neapolinite_ore_desert, postMechs);

        /*BES.AddPoolWorldChecks(ug_confection, notremix);
        BES.AddPoolAccessRequirements(ug_neapolinite_ore, notremix);*/

        BES.AddPoolBiomeChecks(confection_desert, inMainWorld, hallow125.Invoke(confection_desert_blocks));
        BES.AddPoolBiomeChecks(neapolinite_ore_desert, inMainWorld, hallow125.Invoke(confection_desert_blocks));
        BES.AddPoolBiomeChecks(confection_desert, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_desert_blocks));
        BES.AddPoolBiomeChecks(ug_neapolinite_ore_desert, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_desert_blocks));

        BES.SafelyGetModItem(confection_desert, modName, "Creamsand", 41);
        BES.AddItemInPool(confection_desert, ItemID.Cactus, 25);
        BES.SafelyGetModItem(confection_desert, modName, "Sprinkles", 15);
        BES.SafelyGetModItem(confection_desert, modName, "CookieDough", 5);
        BES.SafelyGetModItem(confection_desert, modName, "SherbetBricks", 10);
        BES.AddItemInPool(confection_desert, ItemID.LightShard, 15);
        BES.AddItemInPool(confection_desert, ItemID.Waterleaf, 25);
        BES.AddItemInPool(confection_desert, ItemID.PinkPricklyPear, 16);
        BES.AddItemInPool(confection_desert, ItemID.Scorpion, 4);
        BES.AddItemInPool(confection_desert, ItemID.BlackScorpion, 3);
        BES.SafelyGetModItem(neapolinite_ore_desert, modName, "NeapoliniteOre>", 5);
        /*BES.AliasItemPool(confection_desert_remix, confection_desert);
        BES.AliasItemPool(confection_bars_desert_remix, confection_bars_desert);*/

        BES.SafelyGetModItem(ug_confection_desert, modName, "Creamsand", 8);
        BES.SafelyGetModItem(ug_confection_desert, modName, "HardenedCreamsand", 8);
        BES.SafelyGetModItem(ug_confection_desert, modName, "Creamsandstone", 8);
        BES.SafelyGetModItem(ug_confection_desert, modName, "SoulofDelight", 18);
        BES.SafelyGetModItem(ug_confection_desert, modName, "Saccharite", 20);
        BES.AddItemInPool(ug_confection_desert, ItemID.LightShard, 18);
        BES.SafelyGetModItem(ug_neapolinite_ore_desert, modName, "NeapoliniteOre", 5);
        /*BES.AliasItemPool(ug_confection_desert_remix, ug_confection_desert);
        BES.AliasItemPool(ug_neapolinite_ore_desert_remix, ug_neapolinite_ore_desert);*/
    }

    private static void SetupConfectionSnow()
    {
        /*BES.AddPool(confection_snow, 100, [steampunk, hardmodeOnly], LocalizeAs(confection_snow));
        BES.AddPool(neapolinite_ore_snow, 100, [steampunk, postMechs]);
        BES.AddPool(ug_confection_snow, 1100, ExtractionTiers.STEAMPUNK, LocalizeAs(ug_confection_snow));
        BES.AddPool(ug_neapolinite_ore_snow, 1100, [steampunk, postMechs]);

        BES.AddPoolRequirements(confection_snow, inMainWorld, hallow125.Invoke(confection_snow_blocks));
        BES.AddPoolRequirements(neapolinite_ore_snow, inMainWorld, hallow125.Invoke(confection_snow_blocks));
        BES.AddPoolRequirements(ug_confection_snow, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_snow_blocks), notremix);
        BES.AddPoolRequirements(ug_neapolinite_ore_snow, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_snow_blocks), notremix);*/

        BES.AddPool(confection_snow, 100, LocalizeAs(confection_snow));
        BES.AddPool(neapolinite_ore_snow, 100);
        BES.AddPool(ug_confection_snow, 1100, LocalizeAs(ug_confection_snow));
        BES.AddPool(ug_neapolinite_ore_snow, 1100);

        BES.AddPoolParent(neapolinite_ore_snow, confection_forest, SubLocalizeAs(post_mechs));
        BES.AddPoolParent(ug_neapolinite_ore_snow, ug_confection_snow, SubLocalizeAs(post_mechs));

        BES.AddPoolVisibilityRequirements(confection_snow, steampunk);
        BES.AddPoolVisibilityRequirements(neapolinite_ore_snow, steampunk);
        BES.AddPoolVisibilityRequirements(ug_confection_snow, steampunk);
        BES.AddPoolVisibilityRequirements(ug_neapolinite_ore_snow, steampunk);

        BES.AddPoolAccessRequirements(confection_snow, hardmodeOnly);
        BES.AddPoolAccessRequirements(neapolinite_ore_snow, postMechs);
        BES.AddPoolAccessRequirements(ug_confection_snow, hardmodeOnly);
        BES.AddPoolAccessRequirements(ug_neapolinite_ore_snow, postMechs);

        /*BES.AddPoolWorldChecks(ug_confection, notremix);
        BES.AddPoolAccessRequirements(ug_neapolinite_ore, notremix);*/

        BES.AddPoolBiomeChecks(confection_snow, inMainWorld, hallow125.Invoke(confection_snow_blocks));
        BES.AddPoolBiomeChecks(neapolinite_ore_snow, inMainWorld, hallow125.Invoke(confection_snow_blocks));
        BES.AddPoolBiomeChecks(confection_snow, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_snow_blocks));
        BES.AddPoolBiomeChecks(ug_neapolinite_ore_snow, inMainWorld, belowSurfaceLayer, hallow125.Invoke(confection_snow_blocks));

        BES.AddItemInPool(confection_snow, ItemID.None, 33);
        BES.SafelyGetModItem(confection_snow, modName, "CreamBlock", 27);
        BES.SafelyGetModItem(confection_snow, modName, "BlueIce", 14);
        BES.SafelyGetModItem(confection_snow, modName, "Sprinkles", 15);
        BES.SafelyGetModItem(confection_snow, modName, "CookieDough", 5);
        BES.SafelyGetModItem(confection_snow, modName, "SherbetBricks", 10);
        BES.AddItemInPool(confection_snow, new ItemEntry(ItemID.BorealWood, 1, 3), 17);
        BES.AddItemInPool(confection_snow, ItemID.Shiverthorn, 25);
        BES.SafelyGetModItem(neapolinite_ore_snow, modName, "NeapoliniteOre", 5);
        /*BES.AliasItemPool(confection_snow_remix, confection_snow);
        BES.AliasItemPool(confection_bars_snow_remix, confection_snow);*/

        BES.AddItemInPool(ug_confection_snow, ItemID.None, 69);
        BES.SafelyGetModItem(ug_confection_snow, modName, "CreamBlock", 12);
        BES.SafelyGetModItem(ug_confection_snow, modName, "BlueIce", 12);
        BES.SafelyGetModItem(ug_confection_snow, modName, "SoulofDelight", 25);
        BES.SafelyGetModItem(ug_confection_snow, modName, "Saccharite", 20);
        BES.SafelyGetModItem(ug_neapolinite_ore_snow, modName, "NeapoliniteOre", 5);
        /*BES.AliasItemPool(ug_confection_snow_remix, ug_confection_snow);
        BES.AliasItemPool(ug_neapolinite_ore_snow_remix, ug_confection_snow);*/
    }

    private static void SetupConfectionSky()
    {
        BES.AddPool(confection_sky, 125, true);
        BES.AddPool(confection_spc, 125, true);

        BES.AddPoolParent(confection_sky, sky, SubLocalizeAs(in_hardmode));
        BES.AddPoolParent(confection_spc, space, SubLocalizeAs(in_hardmode));

        BES.AddPoolVisibilityRequirements(confection_sky, steampunk);
        BES.AddPoolVisibilityRequirements(confection_spc, steampunk);

        BES.AddPoolAccessRequirements(confection_sky, hardmodeOnly);
        BES.AddPoolAccessRequirements(confection_spc, hardmodeOnly);

        BES.AddPoolBiomeChecks(confection_sky, inMainWorld, skyLayer, hallow125.Invoke(confection_forest_blocks));
        BES.AddPoolBiomeChecks(confection_spc, inMainWorld, spaceLayer, hallow125.Invoke(confection_forest_blocks));

        BES.AliasItemPool(confection_forest, confection_sky);

        BES.SafelyGetModItem(confection_sky, modName, "PinkFairyFloss", 4);
        BES.SafelyGetModItem(confection_sky, modName, "PurpleFairyFloss", 4);
        BES.SafelyGetModItem(confection_sky, modName, "BlueFairyFloss", 4);

        BES.AliasItemPool(confection_sky, confection_spc);
    }

    private void SetupHallowedExtra()
    {
        BES.SafelyGetModItem(hallowed_forest, modName, "ShellBlock", 5);

        BES.RemoveItemFromPool(hallowed_bars_forest, ItemID.HallowedBar);
        BES.SafelyGetModItem(hallowed_bars_forest, modName, "HallowedOre", 5);

        BES.RemoveItemFromPool(hallowed_bars_desert, ItemID.HallowedBar);
        BES.SafelyGetModItem(hallowed_bars_desert, modName, "HallowedOre", 5);

        //BES.RemoveItemFromPool(hallowed_bars_snow, ItemID.HallowedBar); //For some reason this doesn't work
        BES.SafelyGetModItem(hallowed_bars_snow, modName, "HallowedOre", 5);
    }

    private void SetupCrimsonExtra()
    {
        BES.RemoveItemFromPool(ug_crimson_caverns_hm, ItemID.SoulofNight);
        BES.SafelyGetModItem(ug_crimson_caverns_hm, modName, "SoulofSpite", 20);

        BES.RemoveItemFromPool(ug_crimson_desert_hm, ItemID.SoulofNight);
        BES.SafelyGetModItem(ug_crimson_desert_hm, modName, "SoulofSpite", 16);

        BES.RemoveItemFromPool(ug_crimson_snow_hm, ItemID.SoulofNight);
        BES.SafelyGetModItem(ug_crimson_snow_hm, modName, "SoulofSpite", 20);
    }
    #endregion
}