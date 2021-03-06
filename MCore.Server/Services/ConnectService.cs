﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using MCore.Base.Rpc;
using MCore.Server.Entity;
using MCore.Server.Entity.Memory;
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
            Rpc.Client.Event(RpcEvents.ClientConnect).On(HandlePlayerConnecting);
        }

        /// <summary>
        /// Does not do much
        /// </summary>
        public override void Initialize() {
            // Empty
        }

        //Function that gets called when player has connected
        private void HandlePlayerConnecting([FromSource]Player player)
        {
            try
            {
                MUser mUser = MCoreServer.Instance.GetMUserByPlayer(player);
                MemoryEntity.Instance.ForceSave();
                if (int.TryParse(player.Handle, out int netId))
                {
                    mUser.netId = netId;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }



    }
}
