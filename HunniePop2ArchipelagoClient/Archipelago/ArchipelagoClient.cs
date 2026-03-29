using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using BepInEx;
using HunniePop2ArchipelagoClient.HuniePop2.Gameplay;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HunniePop2ArchipelagoClient.Archipelago
{
    public class ArchipelagoClient
    {
        public const string APVersion = "0.5.0";
        private const string game = "Hunie Pop 2";
        public int[] expectedworld = [3, 0, 1];

        public static bool Authenticated;
        private bool attemptingConnection;

        public static ArchipelagoData ServerData = new();
        public static ArchipelagoSession session;

        public static Dictionary<long, ScoutedItemInfo> shopdict = null;
        public static ArchipelageItemList alist = new ArchipelageItemList();
        public static int totalloc = 0;
        public static int totalitem = 0;
        public bool slotstate = false;

        public string worldversion = "";

        /// <summary>
        /// call to connect to an Archipelago session. Connection info should already be set up on ServerData
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            if (Authenticated || attemptingConnection) return;

            try
            {
                session = ArchipelagoSessionFactory.CreateSession(ServerData.Uri.Trim());
                SetupSession();
            }
            catch (Exception e)
            {
                HuniePop2Archipelago.BepinLogger.LogError(e);
            }

            TryConnect();
        }

        /// <summary>
        /// add handlers for Archipelago events
        /// </summary>
        private void SetupSession()
        {
            session.MessageLog.OnMessageReceived += message => ArchipelagoConsole.LogArchMessage(message);
            session.Items.ItemReceived += OnItemReceived;
            session.Socket.ErrorReceived += OnSessionErrorReceived;
            session.Socket.SocketClosed += OnSessionSocketClosed;
        }

        /// <summary>
        /// attempt to connect to the server with our connection info
        /// </summary>
        private void TryConnect()
        {
            try
            {
                // it's safe to thread this function call but unity notoriously hates threading so do not use excessively
                ThreadPool.QueueUserWorkItem(
                    _ => HandleConnectResult(
                        session.TryConnectAndLogin(
                            game,
                            ServerData.SlotName,
                            ItemsHandlingFlags.AllItems, // TODO make sure to change this line
                            new Version(APVersion),
                            password: ServerData.Password,
                            requestSlotData: true // ServerData.NeedSlotData
                        )));
            }
            catch (Exception e)
            {
                HuniePop2Archipelago.BepinLogger.LogError(e);
                HandleConnectResult(new LoginFailure(e.ToString()));
                attemptingConnection = false;
            }
        }

        /// <summary>
        /// handle the connection result and do things
        /// </summary>
        /// <param name="result"></param>
        private void HandleConnectResult(LoginResult result)
        {
            string outText;
            if (result.Successful)
            {
                var success = (LoginSuccessful)result;

                ServerData.SetupSession(success.SlotData, session.RoomState.Seed);
                Authenticated = true;

                var t = JsonConvert.DeserializeObject<int[]>(ServerData.slotData["world_version"].ToString()); 

                HuniePop2Archipelago.BepinLogger.LogMessage($"CONNECTED TO SERVER | CLIENT V{HuniePop2Archipelago.PluginVersion}, SERVER V{t[0]}.{t[1]}.{t[2]}");
                worldversion = $"{t[0]}.{t[1]}.{t[2]}";

                if (t[0] != expectedworld[0] || t[1] != expectedworld[1] || t[2] != expectedworld[2])
                {
                    ArchipelagoConsole.LogError($"APWORLD VERSION ERROR\nEXPECTED: V{expectedworld[0]}.{expectedworld[1]}.{expectedworld[2]} GOT V{t[0]}.{t[1]}.{t[2]}");
                }

                alist = new ArchipelageItemList();

                foreach (ItemInfo item in session.Items.AllItemsReceived)
                {
                    alist.add(item);
                }

                buildshoplocations(Convert.ToInt32(ArchipelagoClient.ServerData.slotData["number_shop_items"]));
                totalitem = Convert.ToInt32(ServerData.slotData["total_items"]);
                totalloc = Convert.ToInt32(ServerData.slotData["total_locations"]);

                slotstate = session.DataStorage[Scope.Slot, "slotsetup"];

                outText = $"Successfully connected to {ServerData.Uri} as {ServerData.SlotName}!";

                ServerData.gamedata = JsonConvert.DeserializeObject < Dictionary<string, Dictionary<string, int>>>(ServerData.slotData["gamedata"].ToString());

                string alists = session.DataStorage[Scope.Slot, "archdata"];
                if (alists.IsNullOrWhiteSpace())
                {
                    HuniePop2Archipelago.BepinLogger.LogMessage("SERVER ARCHDATA = NULL");
                    alist.seed = session.RoomState.Seed;
                }
                else
                {
                    ArchipelageItemList alist2 = JsonConvert.DeserializeObject<ArchipelageItemList>(alists);
                    HuniePop2Archipelago.BepinLogger.LogMessage("SERVER ARCHDATA:");
                    HuniePop2Archipelago.BepinLogger.LogMessage(alists.ToString());
                    if (alist2.seed != "")
                    {
                        alist.merge(alist2.list);
                    }
                    else
                    {
                        alist.seed = session.RoomState.Seed;
                    }
                }
            }
            else
            {
                var failure = (LoginFailure)result;
                outText = $"Failed to connect to {ServerData.Uri} as {ServerData.SlotName}.";
                outText = failure.Errors.Aggregate(outText, (current, error) => current + $"\n    {error}");

                HuniePop2Archipelago.BepinLogger.LogError(outText);

                Authenticated = false;
                Disconnect();
            }

            ArchipelagoConsole.LogMessage(outText);
            attemptingConnection = false;
        }

        /// <summary>
        /// something we wrong or we need to properly disconnect from the server. cleanup and re null our session
        /// </summary>
        private void Disconnect()
        {
            HuniePop2Archipelago.BepinLogger.LogDebug("disconnecting from server...");
            session?.Socket.DisconnectAsync();
            session = null;
            Authenticated = false;
        }

        public void SendMessage(string message)
        {
            if ( message.StartsWith("$"))
            {
                processcode(message);
                return;
            }
            session.Socket.SendPacketAsync(new SayPacket { Text = message });
        }

        /// <summary>
        /// we received an item so reward it here
        /// </summary>
        /// <param name="helper">item helper which we can grab our item from</param>
        private void OnItemReceived(ReceivedItemsHelper helper)
        {
            var receivedItem = helper.DequeueItem();

            alist.add(receivedItem);

            if (helper.Index < ServerData.Index) return;

            ServerData.Index++;
        }

        public static void sendloc(int loc, bool t)
        {
            session.Locations.CompleteLocationChecks(loc);
        }

        public static void sendloc(string set,int offset)
        {
            HuniePop2Archipelago.BepinLogger.LogMessage($"SENDING LOCATION: SET:{set}, OFFSET:{offset}");
            sendloc(Convert.ToInt32(ServerData.slotData[set]) + offset, true);
        }

        public static ScoutedItemInfo getshopitem(int loc)
        {
            if (!Authenticated) { return null; }
            long key = Convert.ToInt32(ServerData.slotData["shop_loc_start"]) + loc;
            if (shopdict.ContainsKey(key))
            {
                return shopdict[key];
            }
            return null;
        }

        public void buildshoplocations(int num)
        {
            long[] shopids = new long[num];
            int shopstart = Convert.ToInt32(ServerData.slotData["shop_loc_start"]);
            for (int i = 0; i < shopids.Length; i++) { shopids[i] = shopstart + i; }


            Task<Dictionary<long, ScoutedItemInfo>> scoutedInfoTask = Task.Run(async () => await session.Locations.ScoutLocationsAsync(shopids));
            if (scoutedInfoTask.IsFaulted)
            {
                ArchipelagoConsole.LogMessage("ERROR:"+scoutedInfoTask.Exception.GetBaseException().Message);
                return;
            }
            shopdict = scoutedInfoTask.Result;

        }

        public static string seed()
        {
            return session.RoomState.Seed;
        }

        public static List<long> completeloc()
        {
            return session.Locations.AllLocationsChecked.ToList();
        }

        public static bool locdone(long flag)
        {
            return session.Locations.AllLocationsChecked.Contains(flag);
        }

        public static string itemidtoname(long flag)
        {
            return session.Items.GetItemName(flag);
        }

        public static void resetlist()
        {
            ArchipelagoConsole.LogMessage("RESETING RECIEVED ITEMS");
            ArchipelageItemList newlist = new ArchipelageItemList();
            int i = 0;

            foreach (ItemInfo item in session.Items.AllItemsReceived)
            {
                ArchipelagoConsole.LogMessage($"NAME:{item.ItemName} ID:{item.ItemId} RECIEVED");
                newlist.add(item);
                i++;
            }
            alist = newlist;
            ArchipelagoConsole.LogMessage("ITEM RESET COMPLETE, RESET "+i.ToString()+" ITEMS");
        }

        public static void complete()
        {
            var statusUpdatePacket = new StatusUpdatePacket();
            statusUpdatePacket.Status = ArchipelagoClientState.ClientGoal;
            session.Socket.SendPacket(statusUpdatePacket);
        }

        /// <summary>
        /// something went wrong with our socket connection
        /// </summary>
        /// <param name="e">thrown exception from our socket</param>
        /// <param name="message">message received from the server</param>
        private void OnSessionErrorReceived(Exception e, string message)
        {
            HuniePop2Archipelago.BepinLogger.LogError(e);
            ArchipelagoConsole.LogMessage(message);
        }

        /// <summary>
        /// something went wrong closing our connection. disconnect and clean up
        /// </summary>
        /// <param name="reason"></param>
        private void OnSessionSocketClosed(string reason)
        {
            HuniePop2Archipelago.BepinLogger.LogError($"Connection to Archipelago lost: {reason}");
            Disconnect();
        }

        public static void processcode(string code)
        {
            switch (code)
            {
                case "$debug":
                    ArchipelagoConsole.LogMessage("setting debug flag");
                    ArchipelagoConsole.debug = true;
                    break;
                case "$resync":
                    ArchipelagoConsole.LogMessage("Resyncing Items");
                    session.Socket.SendPacket(new SyncPacket());
                    break;
                case "$resetitems":
                    ArchipelagoConsole.LogMessage("RESETING SENT ITEM LIST ALL ITEMS WILL BE REPROCESSED");
                    session.DataStorage[Scope.Slot, "archdata"] = "";
                    resetlist();
                    break;
                case "$resetsave":
                    ArchipelagoConsole.LogMessage("RESETING SAVE FILE");
                    session.DataStorage[Scope.Slot, "savefile"] = "";
                    session.DataStorage[Scope.Slot, "slotsetup"] = false;
                    break;
                case "$resetgame":
                    ArchipelagoConsole.LogMessage("RESETTING SAVFILE AND ITEMS RECIEVED");
                    session.DataStorage[Scope.Slot, "archdata"] = "";
                    session.DataStorage[Scope.Slot, "savefile"] = "";
                    session.DataStorage[Scope.Slot, "slotsetup"] = false;
                    resetlist();
                    break;
                case "$voidfiller":
                    ArchipelagoConsole.LogMessage("REMOVING UNPROCESSED FILLER ITEMS FROM BEING PROCESSED");

                    DepartLocation.filler_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["filler_item_start"]);
                    DepartLocation.arch_item_start ??= Convert.ToInt32(ArchipelagoClient.ServerData.slotData["arch_item_start"]);

                    foreach (ArchipelagoItem i in alist.list)
                    {
                        if (i.Id > DepartLocation.filler_item_start && i.Id <= DepartLocation.arch_item_start)
                        {
                            i.processed = true;
                        }
                    }
                    ArchipelagoClient.session.DataStorage[Scope.Slot, "archdata"] = JsonConvert.SerializeObject(ArchipelagoClient.alist);
                    break;
                case "$debugsave":
                    string playerfile = ArchipelagoClient.session.DataStorage[Scope.Slot, $"savefile"];

                    ArchipelagoConsole.LogMessage($"------------PLAYER FILE START -----------");
                    ArchipelagoConsole.LogMessage($"{playerfile}");
                    ArchipelagoConsole.LogMessage($"------------PLAYER FILE END -----------");
                    break;
                case "$debugserverarchdata":
                    string adata = ArchipelagoClient.session.DataStorage[Scope.Slot, $"archdata"];

                    ArchipelagoConsole.LogMessage($"------------SERVER ARCHDATA START -----------");
                    ArchipelagoConsole.LogMessage($"{adata}");
                    ArchipelagoConsole.LogMessage($"------------SERVER ARCHDATA END -----------");
                    break;                
                case "$debugarchdata":
                    ArchipelagoConsole.LogMessage($"------------ARCHDATA START -----------");
                    ArchipelagoConsole.LogMessage($"{alist.ToString()}");
                    ArchipelagoConsole.LogMessage($"------------ARCHDATA END -----------");
                    break;
                case "$ripdata":
                    ArchipelagoConsole.LogMessage("RIPPING ALL DATA");

                    ArchipelagoConsole.LogMessage("-------------GIRLS--------------");
                    foreach (var g in Game.Data.Girls.GetAll())
                    {
                        ArchipelagoConsole.LogMessage("-----------------------------------");
                        ArchipelagoConsole.LogMessage($"Girl ID: {g.id}");
                        ArchipelagoConsole.LogMessage($"Girl Name: {g.girlName}");
                        ArchipelagoConsole.LogMessage($"fav affection: {g.favoriteAffectionType}");
                        ArchipelagoConsole.LogMessage($"dis affection: {g.leastFavoriteAffectionType}");
                        ArchipelagoConsole.LogMessage($"----------SHOES---------");
                        foreach (var s in g.shoesItemDefs)
                        {
                            ArchipelagoConsole.LogMessage($"shoe id: {s.id} |shoe name: {s.itemName}");
                        }
                        ArchipelagoConsole.LogMessage($"----------UNIQUES---------");
                        foreach (var s in g.uniqueItemDefs)
                        {
                            ArchipelagoConsole.LogMessage($"unique id: {s.id} |unique name: {s.itemName}");
                        }
                        ArchipelagoConsole.LogMessage($"----------BAGGAGE---------");
                        foreach (var s in g.baggageItemDefs)
                        {
                            ArchipelagoConsole.LogMessage($"baggabe id: {s.id} |baggage name: {s.itemName}");
                        }

                        ArchipelagoConsole.LogMessage("-----------------------------------");
                    }
                    ArchipelagoConsole.LogMessage("-------------PAIRS--------------");
                    foreach (var p in Game.Data.GirlPairs.GetAll())
                    {
                        ArchipelagoConsole.LogMessage("-----------------------------------");
                        ArchipelagoConsole.LogMessage($"Pair ID: {p.id}");
                        ArchipelagoConsole.LogMessage($"Pair Name: {p.name}");
                        ArchipelagoConsole.LogMessage($"Girl 1 ID: {p.girlDefinitionOne.id} |Girl 1 Name: {p.girlDefinitionOne.girlName}");
                        ArchipelagoConsole.LogMessage($"Girl 2 ID: {p.girlDefinitionTwo.id} |Girl 2 Name: {p.girlDefinitionTwo.girlName}");
                        ArchipelagoConsole.LogMessage("-----------------------------------");
                    }
                    ArchipelagoConsole.LogMessage("-------------ITEMS--------------");
                    foreach (var i in Game.Data.Items.GetAll())
                    {
                        ArchipelagoConsole.LogMessage("-----------------------------------");
                        ArchipelagoConsole.LogMessage($"Item ID: {i.id}");
                        ArchipelagoConsole.LogMessage($"Item Name: {i.name}");
                        ArchipelagoConsole.LogMessage($"Item type: {i.itemType}");
                        ArchipelagoConsole.LogMessage("-----------------------------------");
                    }

                    break;
                default:
                    break;
            }
        }
    }
}