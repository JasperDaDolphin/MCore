using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCore.Server.Entity;
using MCore.Server.Command;
using MCore.Server.Storage;
using MCore.Server.Services;
using MCore.Base.Services;
using MCore.Server.Entity.Memory;
/// <summary>
/// Base namespace
/// </summary>
namespace MCore.Server
{

    /// <summary>
    /// Main class for the server-side of core
    /// </summary>
    public partial class MCoreServer : BaseScript {

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
        public PlayerList OnlinePlayers => base. Players;

        /// <summary>
        /// A dictionary of all event handlers
        /// </summary>
        public new EventHandlerDictionary EventHandlers => base.EventHandlers;

        /// <summary>
        /// Registry for server's services
        /// </summary>
        public readonly ServiceRegistry Services = new ServiceRegistry();

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
            MemoryEntity.Instance.Load();

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

            // Guarantee async
            await Delay(100);
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
