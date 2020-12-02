using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace MCore.Server.Entity.Memory
{
    /// <summary>
    /// A memory implementation of MUsers
    /// </summary>
    public abstract class MemoryMUsers : MUsers
    {
        // A dictionary (unique id -> user) of all loaded users
        protected IDictionary<string, MUser> MUsers = new Dictionary<string, MUser>();

        /// <inheritdoc />
        public override void Clean()
        {

        }

        /// <inheritdoc />
        public override ICollection<MUser> GetAllMUsers()
        {
            return MUsers.Values;
        }

        /// <inheritdoc />
        public override IDictionary<string, MUser> GetAllMUsersDictionary()
        {
            return MUsers;
        }

        /// <inheritdoc />
        public override ICollection<MUser> GetOnlineMUsers()
        {
            // Pre-define a collection we will add to
            ICollection<MUser> col = new List<MUser>();

            // Loop through all online users and get em!
            foreach (Player online in MCoreServer.Instance.OnlinePlayers)
            {
                col.Add(this.GetByPlayer(online));
            }
            return col;
        }

        public override int MUserCount()
        {
            return GetAllMUsers().Count();
        }

        public override int MUserOnlineCount()
        {
            return GetOnlineMUsers().Count();
        }

        /// <inheritdoc />
        public override MUser GetById(string id)
        {
            // Try and get the user from the dictionary
            if (MUsers.TryGetValue(id, out MUser user)) return user;

            return null;
        }

        /// <inheritdoc />
        public override MUser GetBySteamId(string steamId)
        {
            foreach (MUser user in GetAllMUsers())
                if (user.SteamId == steamId) return user;
            return null;
        }

        public override MUser GetByDiscordId(string discordId)
        {
            foreach (MUser user in GetAllMUsers())
                if (user.DiscordId == discordId) return user;
            return null;
        }

        public override MUser GetByLicenseId(string licenseId)
        {
            foreach (MUser user in GetAllMUsers())
                if (user.LicenseId == licenseId) return user;
            return null;
        }

        /// <inheritdoc />
        public override MUser GetByNetId(int netId)
        {
            Player player = new PlayerList()[netId];
            if (player != null) return this.GetByPlayer(player);
            return null;
        }

        public override MUser GetByName(string name)
        {
            Player player = new PlayerList()[name];
            if (player != null) return this.GetByPlayer(player);
            return null;
        }

        /// <inheritdoc />
        public override MUser GetByPlayer(Player player)
        {
            MUser mUser = this.GetBySteamId(player.Identifiers[IdentifierType.STEAM]);

            if (mUser == null)
            {
                return GenerateMUser(player);
            }
            return mUser;
        }

        /// <summary>
        /// Generates a MUser
        /// </summary>
        /// <param name="id">User's unique id</param>
        /// <returns>Generated user</returns>
        public abstract MUser GenerateMUser(Player player);
    }
}
