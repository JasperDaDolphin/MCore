using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace MCore.Server.Entity.Memory
{

    /// <summary>
    /// A memory implementation of MPlayers
    /// </summary>
    public abstract class MemoryMPlayers : MPlayers
    {

        // A dictionary (unique id -> player) of all loaded players
        protected IDictionary<string, MPlayer> Players = new Dictionary<string, MPlayer>();

        /// <inheritdoc />
        public override void Clean()
        {

        }

        /// <inheritdoc />
        public override ICollection<MPlayer> GetAllPlayers()
        {
            return Players.Values;
        }

        /// <inheritdoc />
        public override ICollection<MPlayer> GetOnlinePlayers()
        {
            // Pre-define a collection we will add to
            ICollection<MPlayer> col = new List<MPlayer>();

            // Loop through all online players and get em!
            foreach (Player online in MCoreServer.Instance.OnlinePlayers)
            {
                col.Add(this.GetByPlayer(online));
            }
            return col;
        }

        /// <inheritdoc />
        public override MPlayer GetById(string id)
        {
            // Try and get the player from the dictionary
            if (Players.TryGetValue(id, out MPlayer player)) return player;

            return this.GeneratePlayer(id);
        }

        /// <inheritdoc />
        public override MPlayer GetBySteamId(string id)
        {
            return this.GetById(id);
        }

        /// <inheritdoc />
        public override MPlayer GetByNetworkId(int id)
        {
            Player player = new PlayerList()[id];
            if (player != null) return this.GetByPlayer(player);
            return null;
        }

        /// <inheritdoc />
        public override MPlayer GetByName(string name)
        {
            return this.GetByPlayer(MCoreServer.Instance.OnlinePlayers[name]);
        }

        /// <inheritdoc />
        public override MPlayer GetByPlayer(Player player)
        {
            return this.GetBySteamId(player.Identifiers[IdentifierType.STEAM]);
        }

        /// <summary>
        /// Generates a MPlayer
        /// </summary>
        /// <param name="id">Player's unique id</param>
        /// <returns>Generated player</returns>
        public abstract MPlayer GeneratePlayer(string id);
    }
}
