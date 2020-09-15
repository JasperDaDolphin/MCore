using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCore.Server.Entity;
using MCore.Server.Command;
using MCore.Server.Storage;
using MCore.Server.Services;
using MCore.Base.Services;
/// <summary>
/// Base namespace
/// </summary>
namespace MCore.Server
{

    /// <summary>
    /// Main class for the server-side of core
    /// </summary>
    public class MCoreServer : BaseScript {

        private bool firstTick = false;

        /// <summary>
        /// Single instance of main class
        /// </summary>
        public static MCoreServer Instance { get; private set; }

        /// <summary>
        /// Database context
        /// </summary>
        public static DB Db { get; private set; }

        /// <summary>
        /// A list of all online players
        /// </summary>
        public PlayerList OnlinePlayers => base.Players;

        /// <summary>
        /// A dictionary of all event handlers
        /// </summary>
        public new EventHandlerDictionary EventHandlers => base.EventHandlers;

        /// <summary>
        /// Registry for server's services
        /// </summary>
        public readonly ServiceRegistry Services = new ServiceRegistry();

        public int TickCounter = 0;

        /// <summary>
        /// Script load
        /// </summary>
        public MCoreServer() {
            // Setting singleton instance and subscribing to ticking
            Instance = this;
            Tick += OnTick;

            // Setup database and create it
            Db = new DB();
            Db.Database.CreateIfNotExists();

            // Load players
            MPlayers.Instance.Load();

            // Register services
            Services.Add(new CommandService());
            Services.Add(new ConnectService());
            Services.Initialize();
        }

        /// <summary>
        /// Called when the server is first loaded
        /// </summary>
        private void OnLoad()
        {
           
        }

        /// <summary>
        /// Called when server ticks
        /// </summary>
        /// <returns>Task responsible for ticking</returns>
        private async Task OnTick() {
            if (!firstTick)
            {
                OnLoad();
                this.firstTick = true;
            }

            //if (this.TickCounter % 10 == 0) UpdatePlayersHandle();
            //TickCounter++;

            // Guarantee async
            await Delay(100);
        }

        private void UpdatePlayersHandle()
        {
            foreach (Player player in OnlinePlayers)
            {
                MPlayer mPlayer = this.GetPlayer(player);
                if (int.TryParse(player.Handle, out int networkId))
                {
                    mPlayer.NetworkId = networkId;
                }
            }
        }


        /// <summary>
        /// Broadcasts a message to all online players
        /// </summary>
        /// <param name="color">Color of prefix</param>
        /// <param name="prefix">Prefix of message</param>
        /// <param name="message">Message to broadcast</param>
        public void BroadcastMessage(int[] color, string prefix, string message) {
            // -1 triggers event for all connected clients
            TriggerClientEvent("chatMessage", prefix, color, new string[] { message });
        }

        /// <summary>
        /// Gets a player from a citizen
        /// </summary>
        /// <param name="player">Citizen of player</param>
        /// <returns>Player or null if not found</returns>
        public MPlayer GetPlayer(Player player) {
            return MPlayers.Instance.GetByPlayer(player);
        }

        /// <summary>
        /// Gets a player by their steamId
        /// </summary>
        /// <param name="id">Id of player</param>
        /// <returns>Player or null if not found</returns>
        public MPlayer GetPlayerBySteamId(string steamId) {
            return MPlayers.Instance.GetBySteamId(steamId);
        }

        /// <summary>
        /// Gets a player by their network id
        /// </summary>
        /// <param name="netId">Network id</param>
        /// <returns>Player or null if not found</returns>
        public MPlayer GetPlayerByNetworkId(int netId) {
            return MPlayers.Instance.GetByNetworkId(netId);
        }

        /// <summary>
        /// Gets a player by their name
        /// </summary>
        /// <param name="name">Name of player</param>
        /// <returns>Player or null if not found</returns>
        public MPlayer GetPlayerByName(string name) {
            return MPlayers.Instance.GetByName(name);
        }

        /// <summary>
        /// Gets a collection of online players
        /// </summary>
        /// <returns></returns>
        public ICollection<MPlayer> GetOnlinePlayers() {
            return MPlayers.Instance.GetOnlinePlayers();
        }

        /// <summary>
        /// Gets the console sender
        /// </summary>
        /// <returns>The console sender</returns>
        public ConsoleSender GetConsoleSender() {
            return new ConsoleSender();
        }

        /// <summary>
        /// Logs a message
        /// </summary>
        /// <param name="message">Message to log</param>
        public static void Log(string message) => Debug.Write($"{DateTime.Now:s} {message}{Environment.NewLine}");

    }
}
