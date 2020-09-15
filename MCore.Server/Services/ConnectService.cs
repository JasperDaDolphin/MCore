using CitizenFX.Core;
using CitizenFX.Core.Native;
using MCore.Server.Entity;
using MCore.Server.Rpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Server.Services {

    /// <summary>
    /// A service for connecting players
    /// </summary>
    public class ConnectService : ServerService {

        /// <summary>
        /// Constructs a connect service
        /// </summary>
        public ConnectService() {
            Rpc.Client.Event("playerSpawned").On(HandlePlayerConnecting);
        }

        /// <summary>
        /// Does not do much
        /// </summary>
        public override void Initialize() {
            // Empty
        }

        // Function that gets called when player has connected
        //private void HandlePlayerConnecting([FromSource]Player player, string playerName, dynamic setKickReason, dynamic deferrals) {
        //    try {
        //        MPlayer MPlayer = MCoreServer.Instance.GetPlayer(player);
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.WriteLine(e.ToString());
        //    }
        //}

        private void HandlePlayerConnecting([FromSource]Player player)
        {
            try
            {
                MPlayer MPlayer = MCoreServer.Instance.GetPlayer(player);
                if(int.TryParse(player.Handle, out int id))
                {
                    MPlayer.NetworkId = id;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }

    }
}
