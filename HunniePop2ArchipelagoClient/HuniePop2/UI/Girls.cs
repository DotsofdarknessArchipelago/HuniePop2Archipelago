using HarmonyLib;
using HunniePop2ArchipelagoClient.Archipelago;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace HunniePop2ArchipelagoClient.HuniePop2.UI
{
    [HarmonyPatch]
    public class Girls
    {
        [HarmonyPatch(typeof(UiAppLevelPlate), "Populate", [typeof(PlayerFileGirl)])]
        [HarmonyPrefix]
        public static bool giftovoverwrite(
            PlayerFileGirl playerFileGirl, 
            UiAppLevelPlate __instance,
            ref PlayerFileGirl ____playerFileGirl,
            ref int ____level,
            ref string ____replacementNum
            )
        {
            if (__instance.levelPlateType == LevelPlateType.AFFECTION)
            {
                return false;
            }
            ____playerFileGirl = playerFileGirl;
            ____level = 1;
            ____replacementNum = "";
            int num = 4;
            LevelPlateType levelPlateType = __instance.levelPlateType;
            if (levelPlateType != LevelPlateType.PASSION)
            {
                if (levelPlateType == LevelPlateType.STYLE)
                {
                    ____level = ____playerFileGirl.receivedShoes.Count;
                }
            }
            else
            {
                ____level = ____playerFileGirl.receivedUniques.Count;
            }
            __instance.valueLabelPro.text = ____level.ToString() + "/" + num.ToString();

            return false;
        }


        [HarmonyPatch(typeof(UiAppDisplaySlot), "Populate", [typeof(ItemDefinition), typeof(bool)])]
        [HarmonyPrefix]
        public static bool giftslotoverwrite(ItemDefinition itemDef,ref bool disable, UiAppDisplaySlot __instance, ref ItemDefinition ____itemDefinition, ref PlayerFileGirl ____playerFileGirl)
        {
            disable = false;
            if (itemDef == null) return true;
            if (itemDef.itemType != ItemType.UNIQUE_GIFT && itemDef.itemType != ItemType.SHOES) return true;


            ____itemDefinition = itemDef;
            __instance.itemSlot.Populate(____itemDefinition);

            __instance.button.Enable();

            switch (itemDef.itemType)
            {
                case ItemType.SHOES:
                    if (____playerFileGirl.HasShoes(itemDef))
                    {
                        __instance.itemSlot.slotBgCanvasGroup.alpha = 1f;
                        return false;
                    }
                    else
                    {
                        __instance.itemSlot.slotBgCanvasGroup.alpha = 0.4f;
                        return false;
                    }
                case ItemType.UNIQUE_GIFT:
                    if (____playerFileGirl.HasUnique(itemDef))
                    {
                        __instance.itemSlot.slotBgCanvasGroup.alpha = 1f;
                        return false;
                    }
                    else
                    {
                        __instance.itemSlot.slotBgCanvasGroup.alpha = 0.4f;
                        return false;
                    }
            }
            return false;
        }

        [HarmonyPatch(typeof(UiAppDisplaySlot), "Populate", [typeof(PlayerFileGirl)])]
        [HarmonyPrefix]
        public static bool giftslotoverwrite2(PlayerFileGirl playerFileGirl, UiAppDisplaySlot __instance, ref PlayerFileGirl ____playerFileGirl)
        {
            if (__instance.itemType != ItemType.UNIQUE_GIFT && __instance.itemType != ItemType.SHOES) return true;

            ____playerFileGirl = playerFileGirl;
            ItemDefinition itemDefinition = null;
            switch (__instance.itemType)
            {
                case ItemType.SHOES:
                        itemDefinition = ____playerFileGirl.girlDefinition.shoesItemDefs[__instance.slotIndex];
                    break;
                case ItemType.UNIQUE_GIFT:
                        itemDefinition = ____playerFileGirl.girlDefinition.uniqueItemDefs[__instance.slotIndex];
                    break;
            }
            __instance.Populate(itemDefinition, false);
            return false;
        }

        [HarmonyPatch(typeof(UiCellphoneInventorySlot), "Start")]
        [HarmonyPostfix]
        public static void giftslotoverwrite2(UiCellphoneInventorySlot __instance, ref bool ____locked, ref PlayerFileGirl ____playerFileGirl)
        {
            if (____playerFileGirl != null)
            {
                ____locked = false;
            }
            __instance.Refresh();
        }

        [HarmonyPatch(typeof(CursorBehavior), "Update")]
        [HarmonyPrefix]
        public static void drapoverwrite(CursorBehavior __instance, ref bool ____active)
        {
            if (__instance.image.sprite == null) return;
            if (__instance.image.sprite == HuniePop2Archipelago.archicon)
            {
                __instance.image.rectTransform.sizeDelta = new Vector2(80, 80);
                __instance.rectTransform.sizeDelta = new Vector2(80, 80);
            }
            return;
        }
    }
}
