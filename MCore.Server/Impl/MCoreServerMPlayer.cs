using CitizenFX.Core;
using MCore.Server.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server
{
    public partial class MCoreServer
    {
        /// <summary>
        /// Gets a player from a citizen
        /// </summary>
        /// <param name="player">Citizen of player</param>
        /// <returns>Player or null if not found</returns>
        public MPlayer GetMPlayerByPlayer(Player player)
        {
            return MPlayers.Instance.GetByPlayer(player);
        }

        /// <summary>
        /// Gets a player by their network id
        /// </summary>
        /// <param name="netId">Network id</param>
        /// <returns>Player or null if not found</returns>
        public MPlayer GetMPlayerBynetId(int netId)
        {
            return MPlayers.Instance.GetByNetId(netId);
        }

        /// <summary>
        /// Gets a player by their name
        /// </summary>
        /// <param name="name">Name of player</param>
        /// <returns>Player or null if not found</returns>
        public MPlayer GetMPlayerByName(string name)
        {
            return MPlayers.Instance.GetByName(name);
        }

        /// <summary>
        /// Gets a collection of online players
        /// </summary>
        /// <returns></returns>
        public ICollection<MPlayer> GetOnlineMPlayers()
        {
            return MPlayers.Instance.GetOnlineMPlayers();
        }

        public MPlayer GenerateMPlayer(MUser mUser)
        {
            return MPlayers.Instance.GenerateMPlayer(mUser);
        }
    }
}
