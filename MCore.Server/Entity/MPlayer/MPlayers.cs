using CitizenFX.Core;
using MCore.Server.Entity.Memory.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Entity
{
    /// <summary>
    /// Represents all MPlayers
    /// </summary>
    public abstract class MPlayers : Entity
    {
        public static MPlayers Instance { get; } = GetPlayersImpl();

        /// <summary>
        /// Gets the implementation to use for the MPlayers
        /// </summary>
        /// <returns>Implementation of MPlayers</returns>
        private static MPlayers GetPlayersImpl()
        {
            return new MySqlMPlayers();
        }

        /// <summary>
        /// Cleans the loaded MPlayers
        /// </summary>
        public abstract void Clean();

        /// <summary>
        /// Gets a collection of all loaded MPlayers in memory
        /// </summary>
        /// <returns>Collection of loaded MPlayers</returns>
        public abstract ICollection<MPlayer> GetAllMPlayers();

        /// <summary>
        /// Gets a MPlayers Dictionary
        /// </summary>
        /// <returns>Dictionary of MPlayers</returns>
        public abstract IDictionary<string, MPlayer> GetAllMPlayersDictionary();

        /// <summary>
        /// Gets a collection of all online MPlayers
        /// </summary>
        /// <returns>Collection of online MPlayers</returns>
        public abstract ICollection<MPlayer> GetOnlineMPlayers();

        /// <summary>
        /// MPlayer Count 
        /// </summary>
        public abstract int MPlayerCount();

        /// <summary>
        /// MPlayer Count Online
        /// </summary>
        public abstract int MPlayerOnlineCount();

        /// <summary>
        /// Gets a MPlayer from their uniquely generated key by MCore
        /// </summary>
        /// <param name="id">Unique key</param>
        /// <returns>MPlayer</returns>
        public abstract MPlayer GetById(string id);

        /// <summary>
        /// Gets a MPlayer from a steam id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MPlayer</returns>
        public abstract MPlayer GetBySteamId(string id);

        /// <summary>
        /// Gets a MPlayer from their network id
        /// </summary>
        /// <param name="id">Network id</param>
        /// <returns>MPlayer</returns>
        public abstract MPlayer GetByNetId(int id);

        /// <summary>
        /// Gets a MPlayer from their name
        /// </summary>
        /// <param name="name">Name to use</param>
        /// <returns>MPlayer</returns>
        public abstract MPlayer GetByName(string name);

        /// <summary>
        /// Gets a MPlayer from a CitizenX's Player
        /// </summary>
        /// <param name="player">CitizenX Player</param>
        /// <returns>MPlayer</returns>
        public abstract MPlayer GetByPlayer(Player player);

        public abstract MPlayer GenerateMPlayer(MUser mUser);
    }
}
