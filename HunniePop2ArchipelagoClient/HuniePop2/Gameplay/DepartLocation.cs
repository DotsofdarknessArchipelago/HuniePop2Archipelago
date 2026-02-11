using Archipelago.MultiClient.Net.Enums;
using HarmonyLib;
using HunniePop2ArchipelagoClient.Archipelago;
using HunniePop2ArchipelagoClient.HuniePop2.Girls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HunniePop2ArchipelagoClient.HuniePop2.Gameplay
{
    [HarmonyPatch]
    public class DepartLocation
    {

        /// <summary>
        /// stuff to do when moving locations
        /// - check/process the item recieved list
        /// - check that randomiser completion goal has been reached
        /// - overwirite baggage
        /// - overwrite finder slots with new generated finder slots
        /// </summary>
        [HarmonyPatch(typeof(LocationManager), "Depart")]
        [HarmonyPostfix]
        public static void processarch()
        {
            PlayerFile file = Game.Persistence.playerFile;
            //check goal completion for beating nymphojin
            if (file.storyProgress >= 12)
            {
                ArchipelagoClient.complete();
            }

            //overwite girls baggage since need to be able to give them all gifts without problem
            //possibly unessiary since its done on the read for girls data but just to make sure it here also
            //for (int i = 0; i < file.girls.Count; i++)
            //{
            //
            //    //fill all baggages slots with temp baggage if not all slots have baggage in them
            //    int id = file.girls[i].girlDefinition.id;
            //    if (file.girls[i].girlDefinition.baggageItemDefs.Count != 3)
            //    {
            //        List<ItemDefinition> newlist = new List<ItemDefinition>();
            //        newlist.Add(Baggage.baggagestuff());
            //        newlist.Add(Baggage.baggagestuff());
            //        newlist.Add(Baggage.baggagestuff());
            //        file.girls[i].girlDefinition.baggageItemDefs = newlist;
            //    }
            //
            //    //check if the 1st baggage item has been obtained and put it in the 1st slot otherwise put a temp baggage instead
            //    if (ArchipelagoClient.alist.hasitem(69420189 + ((id - 1) * 3)) && file.girls[i].girlDefinition.baggageItemDefs[0] != Game.Data.Items.Get(((id - 1) * 3) + 93))
            //    {
            //        file.girls[i].girlDefinition.baggageItemDefs[0] = Game.Data.Items.Get(((id - 1) * 3) + 93);
            //    }
            //    else if (file.girls[i].girlDefinition.baggageItemDefs[0] != Baggage.baggagestuff() && !ArchipelagoClient.alist.hasitem(69420189 + ((id - 1) * 3)))
            //    {
            //        file.girls[i].girlDefinition.baggageItemDefs[0] = Baggage.baggagestuff();
            //    }
            //
            //    //check if the 2nd baggage item has been obtained and put it in the 1st slot otherwise put a temp baggage instead
            //    if (ArchipelagoClient.alist.hasitem(69420190 + ((id - 1) * 3)) && file.girls[i].girlDefinition.baggageItemDefs[1] != Game.Data.Items.Get(((id - 1) * 3) + 94))
            //    {
            //        file.girls[i].girlDefinition.baggageItemDefs[1] = Game.Data.Items.Get(((id - 1) * 3) + 94);
            //    }
            //    else if (file.girls[i].girlDefinition.baggageItemDefs[1] != Baggage.baggagestuff() && !ArchipelagoClient.alist.hasitem(69420190 + ((id - 1) * 3)))
            //    {
            //        file.girls[i].girlDefinition.baggageItemDefs[1] = Baggage.baggagestuff();
            //    }
            //
            //    //check if the 3rd baggage item has been obtained and put it in the 1st slot otherwise put a temp baggage instead
            //    if (ArchipelagoClient.alist.hasitem(69420191 + ((id - 1) * 3)) && file.girls[i].girlDefinition.baggageItemDefs[2] != Game.Data.Items.Get(((id - 1) * 3) + 95))
            //    {
            //        file.girls[i].girlDefinition.baggageItemDefs[2] = Game.Data.Items.Get(((id - 1) * 3) + 95);
            //    }
            //    else if (file.girls[i].girlDefinition.baggageItemDefs[2] != Baggage.baggagestuff() && !ArchipelagoClient.alist.hasitem(69420191 + ((id - 1) * 3)))
            //    {
            //        file.girls[i].girlDefinition.baggageItemDefs[2] = Baggage.baggagestuff();
            //    }
            //}

            //check/process recieved item list
            if (ArchipelagoClient.alist.needprocessing())
            {
                archflagprocess(file);
            }

            if (ArchipelagoClient.alist.seed != ArchipelagoClient.session.RoomState.Seed) { ArchipelagoClient.alist.seed = ArchipelagoClient.session.RoomState.Seed; }

            //save current archipelago data to file
            ArchipelagoClient.session.DataStorage[Scope.Slot, "savefile"] = JsonConvert.SerializeObject(Game.Persistence.playerData.files[4].WriteData());
            ArchipelagoClient.session.DataStorage[Scope.Slot, "archdata"] = JsonConvert.SerializeObject(ArchipelagoClient.alist);

            //generate new finder and overwrite the finder slots with it since normal finder logic isnt that great when playing this
            //TODO overwite the finder UI so dont hae to do this?
            file.finderSlots = Finder.genfinder(file);

        }


        public static int? fairy_wings_item_start;
        public static int? token_item_start;
        public static int? girl_unlock_item_start;
        public static int? pair_unlock_item_start;
        public static int? gift_unique_item_start;
        public static int? gift_shoe_item_start;
        public static int? lola_baggage_item_start;
        public static int? jessie_baggage_item_start;
        public static int? lillian_baggage_item_start;
        public static int? zoey_baggage_item_start;
        public static int? sarah_baggage_item_start;
        public static int? lailani_baggage_item_start;
        public static int? candace_baggage_item_start;
        public static int? nora_baggage_item_start;
        public static int? brooke_baggage_item_start;
        public static int? ashley_baggage_item_start;
        public static int? abia_baggage_item_start;
        public static int? polly_baggage_item_start;
        public static int? outfits_item_start;
        public static int? filler_item_start;
        public static int? arch_item_start;


        /// <summary>
        /// method to process recieved archipelago items
        /// </summary>
        public static void archflagprocess(PlayerFile file)
        {
            fairy_wings_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["fairy_wings_item_start"]);
            token_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["token_item_start"]);
            girl_unlock_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["girl_unlock_item_start"]);
            pair_unlock_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["pair_unlock_item_start"]);
            gift_unique_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["gift_unique_item_start"]);
            gift_shoe_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["gift_shoe_item_start"]);
            lola_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["lola_baggage_item_start"]);
            jessie_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["jessie_baggage_item_start"]);
            lillian_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["lillian_baggage_item_start"]);
            zoey_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["zoey_baggage_item_start"]);
            sarah_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["sarah_baggage_item_start"]);
            lailani_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["lailani_baggage_item_start"]);
            candace_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["candace_baggage_item_start"]);
            nora_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["nora_baggage_item_start"]);
            brooke_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["brooke_baggage_item_start"]);
            ashley_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["ashley_baggage_item_start"]);
            abia_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["abia_baggage_item_start"]);
            polly_baggage_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["polly_baggage_item_start"]);
            outfits_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["outfits_item_start"]);
            filler_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["filler_item_start"]);
            arch_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["arch_item_start"]);




            ArchipelagoConsole.LogMessage("<color=yellow>PROCESSING ITEMS</color>");
            //ArchipelagoConsole.LogMessage(ArchipelagoClient.itemstoprocess.Dequeue().ToString());
            //itterate over entire list
            for (int i = 0; i < ArchipelagoClient.alist.list.Count; i++)
            {
                //if the item has already been processed continue with next item
                if (ArchipelagoClient.alist.list[i].processed) { continue; }

                ArchipelagoConsole.debugLogMessage("PROCESSING ITEM ID: " + ArchipelagoClient.alist.list[i].Id + " FROM PLAYER: " + ArchipelagoClient.alist.list[i].PlayerName + " FROM LOC:" + ArchipelagoClient.alist.list[i].LocationId);

                //if item id is between 69420000 and 69420025 process item as Fairy Wings
                if (ArchipelagoClient.alist.list[i].Id > fairy_wings_item_start && ArchipelagoClient.alist.list[i].Id <= token_item_start)
                {
                    //get girl pair based on the id-69420000 to get the girl id
                    GirlPairDefinition def = Game.Data.GirlPairs.Get((int)ArchipelagoClient.alist.list[i].Id - (int)fairy_wings_item_start);
                    //add girl pair to list of completed girl pairs or if pair is already in the list output warning
                    if (!file.completedGirlPairs.Contains(def))
                    {
                        file.completedGirlPairs.Add(def);
                        ArchipelagoConsole.LogMessage("<color=green>" + def.name + " WING ITEM PROCESSED</color>");
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                    else
                    {
                        ArchipelagoConsole.LogMessage("<color=orange>" + def.name + " WING ITEM ALREADY PROCESSED</color>");
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                }
                //if item id is between 69420024 and 69420057 process item as Token Power Level Up
                else if (ArchipelagoClient.alist.list[i].Id > token_item_start && ArchipelagoClient.alist.list[i].Id <= girl_unlock_item_start)
                {
                    //just say the item is prcessed as the token power up is handled elsewhere
                    ArchipelagoConsole.LogMessage("<color=green>TOKEN LV-UP PROCESSED</color>");
                    ArchipelagoClient.alist.list[i].processed = true;
                }
                //if item id is between 69420056 and 69420069 process item as Girl Unlock
                else if (ArchipelagoClient.alist.list[i].Id > girl_unlock_item_start && ArchipelagoClient.alist.list[i].Id <= pair_unlock_item_start)
                {
                    //unlock girl by setting playermet to true
                    GirlDefinition def = Game.Data.Girls.Get((int)ArchipelagoClient.alist.list[i].Id - (int)girl_unlock_item_start);
                    file.GetPlayerFileGirl(def).playerMet = true;
                    ArchipelagoClient.alist.list[i].processed = true;
                    ArchipelagoConsole.LogMessage("<color=green>" + def.girlName + " IS UNLOCKED</color>");

                }
                //if item id is between 69420056 and 69420069 process item as Pair Unlock
                else if (ArchipelagoClient.alist.list[i].Id > pair_unlock_item_start && ArchipelagoClient.alist.list[i].Id <= gift_unique_item_start)
                {
                    //unlock pair by setting RelationshipType from UNKNOWN to COMPATIABLE
                    GirlPairDefinition def = Game.Data.GirlPairs.Get((int)ArchipelagoClient.alist.list[i].Id - (int)pair_unlock_item_start);
                    PlayerFileGirlPair pair = file.GetPlayerFileGirlPair(def);
                    if (pair.relationshipType == GirlPairRelationshipType.UNKNOWN)
                    {
                        pair.relationshipType = GirlPairRelationshipType.COMPATIBLE;
                        file.metGirlPairs.Add(def);
                        ArchipelagoConsole.LogMessage("<color=green>" + def.name + " UNLOCKED PAIR</color>");
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                    else
                    {
                        ArchipelagoConsole.LogMessage("<color=orange>" + def.name + " PAIR ALREADY PROCESSED</color>");
                        ArchipelagoClient.alist.list[i].processed = true;

                    }
                }
                //if item id is between 69420092 and 69420141 process item as Unique Gift Unlock
                else if (ArchipelagoClient.alist.list[i].Id > gift_unique_item_start && ArchipelagoClient.alist.list[i].Id <= gift_shoe_item_start)
                {
                    //check if the players inventory is full if not add the relevient unique item
                    if (!file.IsInventoryFull())
                    {
                        ItemDefinition def = Game.Data.Items.Get(IDs.uniqueoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - gift_unique_item_start)));
                        file.AddInventoryItem(def);
                        ArchipelagoConsole.LogMessage("<color=green>" + def.itemName + " UNIQUE GIFT OBTAINED AND CAN NOW BE FOUND IN THE SHOP</color>");
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                    else
                    {
                        ArchipelagoConsole.LogMessage("<color=red>INVENTORY FULL COUDNT PROCESS ITEM</color>");
                    }
                }
                //if item id is between 69420140 and 69420189 process item as Shoe Gift Unlock
                else if (ArchipelagoClient.alist.list[i].Id > gift_shoe_item_start && ArchipelagoClient.alist.list[i].Id <= lola_baggage_item_start)
                {
                    //check if the players inventory is full if not add the relevient shoe item
                    if (!file.IsInventoryFull())
                    {
                        ItemDefinition def = Game.Data.Items.Get(IDs.shoeoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - gift_shoe_item_start)));
                        file.AddInventoryItem(def);
                        ArchipelagoConsole.LogMessage("<color=green>" + def.itemName + " SHOES GIFT OBTAINED AND CAN NOW BE FOUND IN THE SHOP</color>");
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                    else
                    {
                        ArchipelagoConsole.LogMessage("<color=red>INVENTORY FULL COUDNT PROCESS ITEM</color>");
                    }
                }
                //if item id is between 69420188 and 69420225 process item as Baggage Unlock
                else if (ArchipelagoClient.alist.list[i].Id > lola_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= outfits_item_start)
                {
                    //overwrite the custom baggage with the regular baggage
                    GirlDefinition def;
                    ItemDefinition bagdef;
                    if (ArchipelagoClient.alist.list[i].Id > lola_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= jessie_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(1);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - lola_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > jessie_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= lillian_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(2);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - jessie_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > lillian_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= zoey_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(3);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - lillian_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > zoey_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= sarah_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(4);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - zoey_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > sarah_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= lailani_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(5);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - sarah_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > lailani_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= candace_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(6);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - lailani_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > candace_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= nora_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(7);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - candace_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > nora_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= brooke_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(8);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - nora_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > brooke_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= ashley_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(9);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - brooke_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > ashley_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= abia_baggage_item_start ) 
                    { 
                        def = Game.Data.Girls.Get(10);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - ashley_baggage_item_start)));
                    }
                    else if (ArchipelagoClient.alist.list[i].Id > abia_baggage_item_start && ArchipelagoClient.alist.list[i].Id <= polly_baggage_item_start) 
                    { 
                        def = Game.Data.Girls.Get(11);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - abia_baggage_item_start)));
                    }
                    else 
                    { 
                        def = Game.Data.Girls.Get(12);
                        bagdef = Game.Data.Items.Get(IDs.baggageoffsettoid((int)(ArchipelagoClient.alist.list[i].Id - polly_baggage_item_start)));
                    }

                    //file.GetPlayerFileGirl(def).girlDefinition.baggageItemDefs[(((int)ArchipelagoClient.alist.list[i].Id - 69420189) % 3)] = bagdef;
                    file.GetPlayerFileGirl(def).LearnBaggage(bagdef);
                    ArchipelagoConsole.LogMessage("<color=green>" + def.girlName + " OBTAINED NEW BAGGAGGE</color>");
                    ArchipelagoClient.alist.list[i].processed = true;

                }
                //if item id is between 69420224 and 69420345 process item as Outfit Unlock
                else if (ArchipelagoClient.alist.list[i].Id > outfits_item_start && ArchipelagoClient.alist.list[i].Id <= filler_item_start)
                {
                    //add relevant outfit/style to the relivent girl
                    int u = (int)(ArchipelagoClient.alist.list[i].Id - outfits_item_start -1);
                    int girlid = (u / 10) + 1;
                    int styleid = u % 10;

                    GirlDefinition def = Game.Data.Girls.Get(girlid);
                    if (!file.GetPlayerFileGirl(Game.Data.Girls.Get(girlid)).unlockedOutfits.Contains(styleid))
                    {
                        ArchipelagoConsole.LogMessage("<color=green>OBTAINED " + file.GetPlayerFileGirl(Game.Data.Girls.Get(girlid)).girlDefinition.girlName + " OUTFIT #" + (styleid + 1) + "</color>");
                        file.GetPlayerFileGirl(Game.Data.Girls.Get(girlid)).unlockedOutfits.Add(styleid);
                        file.GetPlayerFileGirl(Game.Data.Girls.Get(girlid)).unlockedHairstyles.Add(styleid);
                    }
                    ArchipelagoClient.alist.list[i].processed = true;
                }
                //if item id is between 69420334 and 69420422 process item as Filler Items Unlock
                else //if (ArchipelagoClient.alist.list[i].Id > filler_item_start && ArchipelagoClient.alist.list[i].Id <= arch_item_start)
                {
                    //if item id is 69420345 do nothing as its a nothing item
                    if ((int)ArchipelagoClient.alist.list[i].Id == (arch_item_start +1))
                    {
                        //ArchipelagoConsole.LogMessage("nothing");
                        ArchipelagoConsole.LogMessage("<color=green>OBTAINED NOTHING</color>");
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                    //if item id is 69420421 and a random amount of token/seeds to the player
                    else if ((int)ArchipelagoClient.alist.list[i].Id == (arch_item_start + 1))
                    {
                        //ArchipelagoConsole.LogMessage("tokens");

                        int b = UnityEngine.Random.Range(0, 20);
                        int g = UnityEngine.Random.Range(0, 20);
                        int o = UnityEngine.Random.Range(0, 20);
                        int r = UnityEngine.Random.Range(0, 20);

                        file.AddFruitCount(PuzzleAffectionType.TALENT, b);
                        file.AddFruitCount(PuzzleAffectionType.FLIRTATION, g);
                        file.AddFruitCount(PuzzleAffectionType.ROMANCE, o);
                        file.AddFruitCount(PuzzleAffectionType.SEXUALITY, r);
                        ArchipelagoConsole.LogMessage("<color=green>OBTAINED SEEDS: </color><color=blue>" + b.ToString() + " Blue</color>, <color=green>" + g.ToString() + " Green</color>, <color=orange>" + o.ToString() + " Orange</color>, <color=red>" + r.ToString() + " Red</color>");
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                    //skip these arch items
                    else if ((int)ArchipelagoClient.alist.list[i].Id == (arch_item_start + 3) || (int)ArchipelagoClient.alist.list[i].Id == (arch_item_start + 4))
                    {
                        ArchipelagoClient.alist.list[i].processed = true;
                    }
                    //otherwise add the relivent item to the players inventory if not full
                    else
                    {
                        if (!file.IsInventoryFull())
                        {
                            ArchipelagoConsole.LogMessage(Game.Data.Items.Get(IDs.filleroffsettoid((int)(ArchipelagoClient.alist.list[i].Id - filler_item_start))).name);
                            //file.AddInventoryItem(Game.Data.Items.Get(Util.itemflagtoid((int)ArchipelagoClient.alist.list[i].item.ItemId)));

                            for (int k = 0; k < 35; k++)
                            {
                                PlayerFileInventorySlot playerFileInventorySlot = file.GetPlayerFileInventorySlot(k);
                                if (playerFileInventorySlot.itemDefinition == null)
                                {
                                    playerFileInventorySlot.itemDefinition = Game.Data.Items.Get(IDs.filleroffsettoid((int)(ArchipelagoClient.alist.list[i].Id - filler_item_start)));
                                    playerFileInventorySlot.daytimeStamp = 0;
                                    break;
                                }
                            }

                            ArchipelagoConsole.LogMessage("<color=green>OBTAINED " + Game.Data.Items.Get(IDs.filleroffsettoid((int)(ArchipelagoClient.alist.list[i].Id - filler_item_start))).itemName + " ITEM</color>");
                            ArchipelagoClient.alist.list[i].processed = true;
                        }
                        else
                        {
                            ArchipelagoConsole.LogMessage("<color=red>INVENTORY FULL COUDNT PROCESS ITEM</color>");
                        }
                    }
                }

            }
        }
    }
}