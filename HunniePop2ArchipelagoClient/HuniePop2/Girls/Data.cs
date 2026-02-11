using HarmonyLib;
using HunniePop2ArchipelagoClient.Archipelago;
using System;
using System.Collections.Generic;
using System.Text;

namespace HunniePop2ArchipelagoClient.HuniePop2.Girls
{
    [HarmonyPatch]
    public class Data
    {

        public static Dictionary<int, GirlDefinition> girldata;


        [HarmonyPatch(typeof(GirlData), "Get")]
        [HarmonyPrefix]
        public static bool girldataoverwite1(int id, ref GirlDefinition __result, ref Dictionary<int, GirlDefinition> ____definitions)
        {
            if (girldata == null && ArchipelagoClient.ServerData.gamedata == null) { return true; }

            if (girldata == null)
            {
                girldata = new Dictionary<int, GirlDefinition>();

                for (int i = 0; i < ____definitions.Count; i++)
                {
                    var tempdef = ____definitions[i+1];

                    if (tempdef.specialCharacter) { girldata.Add(tempdef.id, tempdef); continue; }

                    tempdef.favoriteAffectionType = (PuzzleAffectionType)ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["favtoken"];
                    tempdef.leastFavoriteAffectionType = (PuzzleAffectionType)ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["distoken"];

                    tempdef.baggageItemDefs = [
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["baggage1"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["baggage2"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["baggage3"]),
                        ];

                    tempdef.shoesItemDefs = [
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe1"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe2"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe3"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe4"]),
                        ];

                    tempdef.uniqueItemDefs = [
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique1"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique2"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique3"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique4"]),
                        ];

                    girldata.Add(tempdef.id, tempdef);
                }
            }

            if (girldata.ContainsKey(id)) { __result = girldata[id]; }
            else { __result = null; }

            return false;
        }


        [HarmonyPatch(typeof(GirlData), "GetAll")]
        [HarmonyPrefix]
        public static bool girldataoverwite2(ref List<GirlDefinition> __result, ref Dictionary<int, GirlDefinition> ____definitions)
        {
            if (girldata == null && ArchipelagoClient.ServerData.gamedata == null) { return true; }

            if (girldata == null)
            {
                for (int i = 0; i < ____definitions.Count; i++)
                {
                    var tempdef = ____definitions[i+1];

                    if (tempdef.specialCharacter) { girldata.Add(tempdef.id, tempdef); continue; }

                    tempdef.favoriteAffectionType = (PuzzleAffectionType)ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["favtoken"];
                    tempdef.leastFavoriteAffectionType = (PuzzleAffectionType)ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["distoken"];

                    tempdef.baggageItemDefs = [
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["baggage1"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["baggage2"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["baggage3"]),
                        ];

                    tempdef.shoesItemDefs = [
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe1"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe2"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe3"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["shoe4"]),
                        ];

                    tempdef.uniqueItemDefs = [
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique1"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique2"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique3"]),
                        Game.Data.Items.Get(ArchipelagoClient.ServerData.gamedata[tempdef.girlName.ToLower()]["unique4"]),
                        ];

                    girldata.Add(tempdef.id, tempdef);
                }
            }

            __result = ListUtils.DictionaryValuesToList<int, GirlDefinition>(girldata);
            return false;
        }

    }
}
