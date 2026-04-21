using HarmonyLib;
using HunniePop2ArchipelagoClient.Archipelago;
using System;

namespace HunniePop2ArchipelagoClient.HuniePop2.Gameplay
{
    [HarmonyPatch]
    public class Traits
    {

        /// <summary>
        /// overwrite the caculation for affection lv for each type
        /// </summary>
        /// <returns>RETURNS FALSE TO SKIP ORIGINAL METHOD</returns>
        [HarmonyPatch(typeof(PlayerFile), "GetAffectionLevelExp")]
        [HarmonyPrefix]
        public static bool affectionexp(PuzzleAffectionType affectionType, bool ofLevel, PlayerFile __instance, ref int __result)
        {
            if (!ArchipelagoClient.Authenticated) { return true; }
            
            DepartLocation.token_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["token_item_start"]);

            if (DepartLocation.token_item_start == null) return true;

            int exp = 0;

            //sets the base flag id based on the affection type
            int flag = (int)DepartLocation.token_item_start;
            if (affectionType == PuzzleAffectionType.ROMANCE) { flag += 2; }
            else if (affectionType == PuzzleAffectionType.FLIRTATION) { flag += 3; }
            else if (affectionType == PuzzleAffectionType.SEXUALITY) { flag += 4; }
            else { flag += 1; }

            //checks the recieved item list based on the flag if the item has been found and adds 6 to the total exp
            exp = 6 * ArchipelagoClient.alist.coutnitem(flag);

            if (ofLevel) { __result = 0; }
            else { __result = exp; }
            return false;
        }

        /// <summary>
        /// overwrite the caculation for passion lv
        /// </summary>
        /// <returns>RETURNS FALSE TO SKIP ORIGINAL METHOD</returns>
        [HarmonyPatch(typeof(PlayerFile), "GetPassionLevelExp")]
        [HarmonyPrefix]
        public static bool passionexp(bool ofLevel, PlayerFile __instance, ref int __result)
        {
            DepartLocation.token_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["token_item_start"]);

            if (DepartLocation.token_item_start == null) return true;

            //checks the recieved item list based on the flag value and adds 6 exp if the item has been recieved
            int exp = 0;
            int flag = (int)DepartLocation.token_item_start + 5;
            exp = 6 * ArchipelagoClient.alist.coutnitem(flag);

            if (ofLevel) { __result = 0; }
            else { __result = exp; }
            return false;
        }

        /// <summary>
        /// overwrite the caculation for style lv
        /// </summary>
        /// <returns>RETURNS FALSE TO SKIP ORIGINAL METHOD</returns>
        [HarmonyPatch(typeof(PlayerFile), "GetStyleLevelExp")]
        [HarmonyPrefix]
        public static bool styleexp(bool ofLevel, PlayerFile __instance, ref int __result)
        {
            DepartLocation.token_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["token_item_start"]);

            if (DepartLocation.token_item_start == null) return true;

            //checks the recieved item list based on the flag value and adds 6 exp if the item has been recieved
            int exp = 0;
            int flag = (int)DepartLocation.token_item_start + 6;
            exp = 6 * ArchipelagoClient.alist.coutnitem(flag);

            if (ofLevel) { __result = 0; }
            else { __result = exp; }
            return false;
        }
    }
}