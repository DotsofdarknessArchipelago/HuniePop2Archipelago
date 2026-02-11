using HarmonyLib;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace HunniePop2ArchipelagoClient.HuniePop2.Girls
{
    [HarmonyPatch]
    public class Questions
    {

        /// <summary>
        /// sends relevent location when learning girls details
        /// </summary>
        [HarmonyPatch(typeof(PlayerFileGirl), "LearnFavAnswer")]
        [HarmonyPostfix]
        public static void questioncheck(QuestionDefinition questionDef, bool __result, PlayerFileGirl __instance)
        {
            if (__result == false) { return; }
            //if questions arent in logic skip checking
            if (Game.Persistence.playerFile.GetFlagValue("questions_skiped") == 0)
            {
                if (Game.Persistence.playerFile.GetFlagValue("question:" + __instance.girlDefinition.id + ":" + questionDef.id) != 1)
                {
                    //Archipelago.ArchipelagoClient.sendloc(69420144 + (__instance.girlDefinition.id - 1) * 20 + questionDef.id);
                    Archipelago.ArchipelagoClient.sendloc($"{__instance.girlDefinition.girlName.ToLower()}_questions_loc_start", questionDef.id);
                    Game.Persistence.playerFile.SetFlagValue("question:" + __instance.girlDefinition.id + ":" + questionDef.id, 1);
                }
            }
        }

        /// <summary>
        /// overwrite the question list when a girl is asking what their favruote thing is since the nomal logic for this is terrible to get unanswered questions
        /// </summary>
        [HarmonyPatch(typeof(TalkManager), "TalkStep")]
        [HarmonyPrefix]
        public static bool question(TalkManager __instance, ref int ____talkStepIndex, ref List<QuestionDefinition> ____questionPool, ref UiDoll ____targetDoll)
        {
            //make sure we are getting asked about their favroute thing and that we are in the begining
            if (__instance.talkType == TalkWithType.FAVORITE_QUESTION)
            {
                if (____talkStepIndex == 0)
                {
                    //clear the question pool since we are replacing it anyways
                    ____questionPool.Clear();


                    List<QuestionDefinition> badpool = new List<QuestionDefinition>();
                    List<QuestionDefinition> goodpool = new List<QuestionDefinition>();

                    List<QuestionDefinition> questionlist = Game.Data.Questions.GetAll();

                    foreach (QuestionDefinition question in questionlist)
                    {
                        //add the question to the good pool if it hasnt been answered yet otherwise put it in the bad pool
                        if (Game.Persistence.playerFile.GetFlagValue("question:" + ____targetDoll.girlDefinition.id + ":" + question.id) != 1)
                        {
                            goodpool.Add(question);
                        }
                        else
                        {
                            badpool.Add(question);
                        }
                    }

                    //choose 4 questions to be asked pulling from the good pool untill its empty
                    for (int j = 1; j < 4; j++)
                    {
                        if (goodpool.Count > 0)
                        {
                            int index = UnityEngine.Random.Range(0, goodpool.Count);
                            ____questionPool.Add(goodpool[index]);
                            goodpool.RemoveAt(index);
                        }
                        else
                        {
                            int index = UnityEngine.Random.Range(0, badpool.Count);
                            ____questionPool.Add(badpool[index]);
                            badpool.RemoveAt(index);
                        }
                    }
                }
            }
            return true;
        }

        [HarmonyPatch(typeof(TalkManager), "TalkWith")]
        [HarmonyPrefix]
        public static bool questi3on(
            int dollIndex,
            TalkManager __instance,
            ref GirlPairDefinition ____girlPair,
            ref PlayerFileGirlPair ____fileGirlPair,
            ref bool ____altGirl,
            ref PuzzleStatusGirl ____statusGirl,
            ref PuzzleStatusGirl ____oppositeStatusGirl,
            ref UiDoll ____targetDoll,
            ref UiDoll ____oppositeDoll,
            ref PlayerFileGirl ____fileGirl,
            ref PlayerFileGirl ____oppositeFileGirl,
            ref TalkWithType ____talkType,
            ref bool ____isTalking,
            ref int ____talkStepIndex
            )
        {
            if (Game.Session.Location.currentGirlPair == null)
            {
                return false;
            }
            ____girlPair = Game.Session.Location.currentGirlPair;
            ____fileGirlPair = Game.Persistence.playerFile.GetPlayerFileGirlPair(____girlPair);
            ____altGirl = dollIndex > 0;
            ____statusGirl = Game.Session.Puzzle.puzzleStatus.GetStatusGirl(____altGirl);
            ____oppositeStatusGirl = Game.Session.Puzzle.puzzleStatus.GetStatusGirl(!____altGirl);
            ____targetDoll = Game.Session.gameCanvas.GetDoll(____altGirl);
            ____oppositeDoll = Game.Session.gameCanvas.GetDoll(!____altGirl);
            ____fileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(____targetDoll.girlDefinition);
            ____oppositeFileGirl = Game.Persistence.playerFile.GetPlayerFileGirl(____oppositeDoll.girlDefinition);
            Game.Session.Puzzle.puzzleStatus.SetGirlFocus(____altGirl);
            if (____statusGirl.stamina >= 2)
            {
                bool flag = false;
                if (____targetDoll.girlDefinition == __instance.telepathGirlDefinition)
                {
                    List<GirlDefinition> allBySpecial = Game.Data.Girls.GetAllBySpecial(false);
                    for (int i = 0; i < allBySpecial.Count; i++)
                    {
                        if (!Game.Persistence.playerFile.GetPlayerFileGirl(allBySpecial[i]).playerMet)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                ____talkType = ((!flag) ? (MathUtils.RandomBool() ? TalkWithType.HER_QUESTION : TalkWithType.FAVORITE_QUESTION) : TalkWithType.FAVORITE_QUESTION);
                //if (____statusGirl.playerFileGirl.learnedBaggage.Count < ____statusGirl.girlDefinition.baggageItemDefs.Count && ____statusGirl.playerFileGirl.relationshipPoints >= __instance.baggageThresholdsPoint[Mathf.Clamp(____statusGirl.playerFileGirl.learnedBaggage.Count, 0, __instance.baggageThresholdsPoint.Length - 1)])
                //{
                //    ____talkType = TalkWithType.BAGGAGE_CONVO;
                //}
                ____isTalking = true;
                ____talkStepIndex = -1;
                //if (____talkType != TalkWithType.BAGGAGE_CONVO)
                //{
                //    Game.Persistence.playerFile.relationshipPoints += 2;
                //    ____statusGirl.playerFileGirl.relationshipPoints += 2;
                //}
                Game.Session.Puzzle.puzzleStatus.AddResourceValue(PuzzleResourceType.STAMINA, -2, ____altGirl);
                if (Game.Session.Puzzle.puzzleStatus.movesRemaining < Game.Session.Puzzle.puzzleStatus.maxMovesRemaining)
                {
                    Game.Session.Puzzle.puzzleStatus.AddResourceValue(PuzzleResourceType.MOVES, 1, ____altGirl);
                    TokenDefinition byResourceType = Game.Data.Tokens.GetByResourceType(PuzzleResourceType.MOVES, PuzzleAffectionType.TALENT);
                    Object.Instantiate<EnergyTrailBehavior>(__instance.energyTrailPrefab).Init(EnergyTrailFormat.START_AND_END, byResourceType.energyDefinition, null, ____targetDoll, "+1 " + byResourceType.resourceName);
                    Game.Manager.Audio.Play(AudioCategory.SOUND, __instance.sfxTalkReward, ____targetDoll.pauseDefinition);
                }
                Game.Session.Puzzle.puzzleStatus.CheckChanges();
                Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Session.Gift.sfxResourceFlourish, ____targetDoll.pauseDefinition);
                if (Game.Manager.Windows.IsWindowActive(null, true, false))
                {
                    Game.Manager.Windows.HideWindow();
                }


                MethodInfo dynMethod = __instance.GetType().GetMethod("TalkStep", BindingFlags.NonPublic | BindingFlags.Instance);
                dynMethod.Invoke(__instance, []);

                //__instance.TalkStep();
                return false;
            }
            Game.Manager.Audio.Play(AudioCategory.SOUND, Game.Manager.Ui.sfxReject, ____targetDoll.pauseDefinition);
            if (Game.Manager.Windows.IsWindowActive(null, true, false))
            {
                Game.Manager.Windows.ShowWindow(Game.Session.Location.actionBubblesWindow, true);
                Game.Manager.Windows.HideWindow();
            }
            return false;
        }



        //IL CODE

        /// <summary>
        /// nop the logic for setting the question list
        /// </summary>
        [HarmonyPatch(typeof(TalkManager), "TalkStep")]
        [HarmonyILManipulator]
        public static void questionil(ILContext ctx, MethodBase orig)
        {
            for (int i = 0; i < ctx.Instrs.Count; i++)
            {
                //skip the first 20 instrcts since what we want is not in them
                if (i < 20)
                {
                    continue;
                }

                //find where the question pool logic begins then nop it all
                if (ctx.Instrs[i].OpCode == OpCodes.Ldarg_0
                    && ctx.Instrs[i - 1].OpCode == OpCodes.Br
                    && ctx.Instrs[i - 2].OpCode == OpCodes.Callvirt
                    && ctx.Instrs[i - 3].OpCode == OpCodes.Newobj
                    && ctx.Instrs[i - 4].OpCode == OpCodes.Ldftn
                    && ctx.Instrs[i - 5].OpCode == OpCodes.Ldarg_0
                    && ctx.Instrs[i - 6].OpCode == OpCodes.Ldfld
                    && ctx.Instrs[i - 7].OpCode == OpCodes.Ldarg_0
                    && ctx.Instrs[i - 8].OpCode == OpCodes.Callvirt
                    && ctx.Instrs[i - 9].OpCode == OpCodes.Ldc_I4_1)
                {
                    for (int j = 0; j < 103; j++)
                    {
                        ctx.Instrs[i + j].OpCode = OpCodes.Nop;
                    }
                }
            }
        }
    }
}