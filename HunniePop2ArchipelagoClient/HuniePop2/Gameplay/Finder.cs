using HarmonyLib;
using HunniePop2ArchipelagoClient.Archipelago;
using System;
using System.Collections.Generic;

namespace HunniePop2ArchipelagoClient.HuniePop2.Gameplay
{
    [HarmonyPatch]
    public class Finder
    {

        /// <summary>
        /// clears all te finder slots tehn generates them using our logic
        /// TODO probally colapse the external method if replacing the UI dosent pan out
        /// </summary>
        [HarmonyPatch(typeof(PlayerFile), "PopulateFinderSlots")]
        [HarmonyPrefix]
        public static bool finder(PlayerFile __instance)
        {
            for (int i = 0; i < __instance.finderSlots.Count; i++)
            {
                __instance.finderSlots[i].Clear();
            }

            __instance.finderSlots = genfinder(__instance);

            return false;
        }


        /// <summary>
        /// Generate and overwirite the finder slots
        /// </summary>
        public static List<PlayerFileFinderSlot> genfinder(PlayerFile file)
        {

            List<GirlPairDefinition> pripair = new List<GirlPairDefinition>();
            List<GirlPairDefinition> pair = new List<GirlPairDefinition>();

            //iterate over the number of girl pairs
            for (int i = 0; i < file.girlPairs.Count; i++)
            {
                //if girl pair is not the standard gameplay pairs skip over them
                if (file.girlPairs[i].girlPairDefinition.girlDefinitionOne.id >= 13 || file.girlPairs[i].girlPairDefinition.girlDefinitionTwo.id >= 13) { continue; }
                //check if the pair is able to be met yet
                if (file.girlPairs[i].relationshipType != GirlPairRelationshipType.UNKNOWN)
                {
                    //check if each girl can be met yet
                    if (file.GetPlayerFileGirl(file.girlPairs[i].girlPairDefinition.girlDefinitionOne).playerMet && file.GetPlayerFileGirl(file.girlPairs[i].girlPairDefinition.girlDefinitionTwo).playerMet)
                    {
                        if (file.girlPairs[i].relationshipType == GirlPairRelationshipType.COMPATIBLE || file.girlPairs[i].relationshipType == GirlPairRelationshipType.ATTRACTED)
                        {
                            pripair.Add(file.girlPairs[i].girlPairDefinition);
                            continue;
                        }

                        bool t = false;

                        var g1 = file.GetPlayerFileGirl(file.girlPairs[i].girlPairDefinition.girlDefinitionOne);
                        if (Game.Persistence.playerFile.GetFlagValue(g1.girlDefinition.id.ToString() + ":" + g1.outfitIndex.ToString()) == -1)
                        {
                            pripair.Add(file.girlPairs[i].girlPairDefinition);
                            continue;
                        }
                        foreach (var ui in g1.girlDefinition.uniqueItemDefs)
                        {
                            if (!g1.HasUnique(ui) && file.IsItemInInventory(ui, false))
                            {
                                pripair.Add(file.girlPairs[i].girlPairDefinition);
                                t = true;
                                break;
                            }
                        }
                        if (t) continue;
                        foreach (var ui in g1.girlDefinition.shoesItemDefs)
                        {
                            if (!g1.HasShoes(ui) && file.IsItemInInventory(ui, false))
                            {
                                pripair.Add(file.girlPairs[i].girlPairDefinition);
                                t = true;
                                break;
                            }
                        }
                        if (t) continue;

                        var g2 = file.GetPlayerFileGirl(file.girlPairs[i].girlPairDefinition.girlDefinitionTwo);
                        if (Game.Persistence.playerFile.GetFlagValue(g2.girlDefinition.id.ToString() + ":" + g2.outfitIndex.ToString()) == -1)
                        {
                            pripair.Add(file.girlPairs[i].girlPairDefinition);
                            continue;
                        }
                        foreach (var ui in g2.girlDefinition.uniqueItemDefs)
                        {
                            if (!g2.HasUnique(ui) && file.IsItemInInventory(ui, false))
                            {
                                pripair.Add(file.girlPairs[i].girlPairDefinition);
                                t = true;
                                break;
                            }
                        }
                        if (t) continue;
                        foreach (var ui in g2.girlDefinition.shoesItemDefs)
                        {
                            if (!g2.HasShoes(ui) && file.IsItemInInventory(ui, false))
                            {
                                pripair.Add(file.girlPairs[i].girlPairDefinition);
                                t = true;
                                break;
                            }
                        }
                        if (t) continue;


                        //add to list
                        pair.Add(file.girlPairs[i].girlPairDefinition);
                    }
                }
            }


            int initalpaircount = pair.Count;

            List<LocationDefinition> areas = new List<LocationDefinition>();
            //itterate over locations for pairs to be found natually
            for (int j = 1; j < 9; j++)
            {
                LocationDefinition a = Game.Data.Locations.Get(j);
                if (file.locationDefinition != a)
                {
                    areas.Add(a);
                }
            }

            List<PlayerFileFinderSlot> finder = new List<PlayerFileFinderSlot>();

            for (int i = 0; i < 8; i++)
            {
                //if we run out of pairs avaliable or locations return
                if ((pair.Count + pripair.Count) == 0 || areas.Count == 0) { break; }
                //get a random pair and location index from list
                if (pripair.Count > 0)
                {
                    int p;
                    if (pripair.Count == 1) { p = 0; } else { p = UnityEngine.Random.Range(0, pripair.Count); }
                    int a = UnityEngine.Random.Range(0, areas.Count);

                    //make sure that the index is valid
                    p = Math.Min(p, pripair.Count - 1);
                    a = Math.Min(a, areas.Count - 1);

                    //generate a new PlayerFileFinderSlot based on index generated
                    PlayerFileFinderSlot findSlot = new PlayerFileFinderSlot();
                    findSlot.locationDefinition = areas[a];
                    findSlot.girlPairDefinition = pripair[p];
                    //randomise if the girls are flipped or not
                    if ((UnityEngine.Random.Range(0, 100) % 2) == 0)
                    {
                        findSlot.sidesFlipped = false;
                    }
                    else
                    {
                        findSlot.sidesFlipped = true;
                    }

                    //add finder slot to list and remove pair and locations from their list
                    finder.Add(findSlot);
                    areas.RemoveAt(a);
                    pripair.RemoveAt(p);
                }
                else if (pair.Count > 0)
                {
                    int p;
                    if (pair.Count == 1) { p = 0; } else { p = UnityEngine.Random.Range(0, pair.Count); }
                    int a = UnityEngine.Random.Range(0, areas.Count);

                    //make sure that the index is valid
                    p = Math.Min(p, pair.Count - 1);
                    a = Math.Min(a, areas.Count - 1);

                    //generate a new PlayerFileFinderSlot based on index generated
                    PlayerFileFinderSlot findSlot = new PlayerFileFinderSlot();
                    findSlot.locationDefinition = areas[a];
                    findSlot.girlPairDefinition = pair[p];
                    //randomise if the girls are flipped or not
                    if ((UnityEngine.Random.Range(0, 100) % 2) == 0)
                    {
                        findSlot.sidesFlipped = false;
                    }
                    else
                    {
                        findSlot.sidesFlipped = true;
                    }

                    //add finder slot to list and remove pair and locations from their list
                    finder.Add(findSlot);
                    areas.RemoveAt(a);
                    pair.RemoveAt(p);
                }
            }
            //return finder list
            return finder;
        }

    }
}