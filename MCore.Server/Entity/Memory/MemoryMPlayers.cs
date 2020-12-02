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
        protected IDictionary<string, MPlayer> MPlayers = new Dictionary<string, MPlayer>();

        /// <inheritdoc />
        public override void Clean()
        {

        }

        /// <inheritdoc />
        public override ICollection<MPlayer> GetAllMPlayers()
        {
            return MPlayers.Values;
        }

        public override IDictionary<string, MPlayer> GetAllMPlayersDictionary()
        {
            return MPlayers;
        }

        /// <inheritdoc />
        public override ICollection<MPlayer> GetOnlineMPlayers()
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

        public override int MPlayerCount()
        {
            return GetAllMPlayers().Count();
        }

        public override int MPlayerOnlineCount()
        {
            return GetOnlineMPlayers().Count();
        }

        /// <inheritdoc />
        public override MPlayer GetById(string id)
        {
            // Try and get the player from the dictionary
            if (MPlayers.TryGetValue(id, out MPlayer player)) return player;
            return null;
        }

        /// <inheritdoc />
        public override MPlayer GetBySteamId(string id)
        {
            return this.GetById(id);
        }

        /// <inheritdoc />
        public override MPlayer GetByNetId(int id)
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
    }
}
