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
        /// Gets a player by their name
        /// </summary>
        /// <param name="name">Name of player</param>
        /// <returns>Player or null if not found</returns>
        public MUser GetMUserByMPlayer(MPlayer mPlayer)
        {
            return MUsers.Instance.GetById(mPlayer.MUser.Id);
        }

        /// <summary>
        /// Gets a player by their network id
        /// </summary>
        /// <param name="netId">Network id</param>
        /// <returns>Player or null if not found</returns>
        public MUser GetMUserByNetId(int netId)
        {
            return MUsers.Instance.GetByNetId(netId);
        }

        /// <summary>
        /// Gets a collection of online MUsers
        /// </summary>
        /// <returns>Collection of MUser's</returns>
        public ICollection<MUser> GetOnlineMUsers()
        {
            return MUsers.Instance.GetOnlineMUsers();
        }

        /// <summary>
        /// Gets a MUser from a citizen
        /// </summary>
        /// <param name="player">Citizen of player</param>
        /// <returns>MUser or null if not found</returns>
        public MUser GetMUserByPlayer(Player player)
        {
            return MUsers.Instance.GetByPlayer(player);
        }
    }
}
