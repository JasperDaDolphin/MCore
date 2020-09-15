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
    /// Represents all players
    /// </summary>
    public abstract class MPlayers
    {

        public static MPlayers Instance { get; } = GetPlayersImpl();

        /// <summary>
        /// Gets the implementation to use for the players
        /// </summary>
        /// <returns>Implementation of players</returns>
        private static MPlayers GetPlayersImpl()
        {
            return new MySqlMPlayers();
        }

        /// <summary>
        /// Cleans the loaded players
        /// </summary>
        public abstract void Clean();

        /// <summary>
        /// Gets a collection of all loaded players in memory
        /// </summary>
        /// <returns>Collection of loaded players</returns>
        public abstract ICollection<MPlayer> GetAllPlayers();

        /// <summary>
        /// Gets a collection of all online players
        /// </summary>
        /// <returns>Collection of online players</returns>
        public abstract ICollection<MPlayer> GetOnlinePlayers();

        /// <summary>
        /// Gets a player from their uniquely generated key by MCore
        /// </summary>
        /// <param name="id">Unique key</param>
        /// <returns></returns>
        public abstract MPlayer GetById(string id);

        /// <summary>
        /// Gets a player from a steam id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract MPlayer GetBySteamId(string id);

        /// <summary>
        /// Gets a player from their network id
        /// </summary>
        /// <param name="id">Network id</param>
        /// <returns></returns>
        public abstract MPlayer GetByNetworkId(int id);

        /// <summary>
        /// Gets a player from their name
        /// </summary>
        /// <param name="name">Name to use</param>
        /// <returns></returns>
        public abstract MPlayer GetByName(string name);

        /// <summary>
        /// Gets a player from a CitizenX's Player
        /// </summary>
        /// <param name="player">CitizenX player</param>
        /// <returns></returns>
        public abstract MPlayer GetByPlayer(Player player);

        /// <summary>
        /// Loads players into memory
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// Force saves players
        /// </summary>
        public abstract void ForceSave();

        /// <summary>
        /// Force saves players async
        /// </summary>
        public abstract Task ForceSaveAsync();

    }
}
