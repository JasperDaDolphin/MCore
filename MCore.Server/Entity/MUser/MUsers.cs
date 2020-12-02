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
    /// Represents all MUsers
    /// </summary>
    public abstract class MUsers : Entity
    {
        public static MUsers Instance { get; } = GetUsersImpl();

        /// <summary>
        /// Gets the implementation to use for the MUsers
        /// </summary>
        /// <returns>Implementation of MUsers</returns>
        private static MUsers GetUsersImpl()
        {
            return new MySqlMUsers();
        }

        /// <summary>
        /// Cleans the loaded MUsers
        /// </summary>
        public abstract void Clean();

        /// <summary>
        /// Gets a collection of all loaded MUsers in memory
        /// </summary>
        /// <returns>Collection of loaded MUsers</returns>
        public abstract ICollection<MUser> GetAllMUsers();

        /// <summary>
        /// Gets a MUsers Dictionary
        /// </summary>
        /// <returns>Dictionary of MUsers</returns>
        public abstract IDictionary<string, MUser> GetAllMUsersDictionary();

        /// <summary>
        /// Gets a collection of all online MUsers
        /// </summary>
        /// <returns>Collection of online MUsers</returns>
        public abstract ICollection<MUser> GetOnlineMUsers();

        /// <summary>
        /// MUser Count 
        /// </summary>
        public abstract int MUserCount();

        /// <summary>
        /// MUser Count Online
        /// </summary>
        public abstract int MUserOnlineCount();

        /// <summary>
        /// Gets a MUser from their uniquely generated key by MCore
        /// </summary>
        /// <param name="id">Unique key</param>
        /// <returns>MUser</returns>
        public abstract MUser GetById(string id);

        /// <summary>
        /// Gets a MUser from a steam id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MUser</returns>
        public abstract MUser GetBySteamId(string steamId);

        /// <summary>
        /// Gets a MUser from a discord id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MUser</returns>
        public abstract MUser GetByDiscordId(string discordId);

        /// <summary>
        /// Gets a user from a license id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MUser</returns>
        public abstract MUser GetByLicenseId(string licenseId);

        /// <summary>
        /// Gets a user from their network id
        /// </summary>
        /// <param name="id">Network id</param>
        /// <returns>MUser</returns>
        public abstract MUser GetByNetId(int netId);

        /// <summary>
        /// Gets a MUser from their name
        /// </summary>
        /// <param name="name">Name to use</param>
        /// <returns>MUser</returns>
        public abstract MUser GetByName(string name);

        /// <summary>
        /// Gets a MUser from a CitizenX's Player
        /// </summary>
        /// <param name="user">CitizenX Player</param>
        /// <returns>MUser</returns>
        public abstract MUser GetByPlayer(Player player);
    }
}
