using CitizenFX.Core;
using CitizenFX.Core.Native;
using MCore.Base.Rpc;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCore.Client {

    /// <summary>
    /// Main class for the client-side of core
    /// </summary>
    public class MCoreClient : BaseScript {

        private bool firstTick = false;

        public MCoreClient() {
            // Subscribe to ticking
            Tick += OnTick;
        }

        /// <summary>
        /// Called when the client is first loaded
        /// </summary>
        private void OnLoad() {
            if (playerConnected())
            {
                Debug.WriteLine(RpcEvents.ClientConnect);
                try
                {
                    TriggerServerEvent(RpcEvents.ClientConnect);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }
            else
            {
                OnLoad();
            }
        }

        private bool playerConnected()
        {
            return API.NetworkIsPlayerConnected(this.LocalPlayer.Handle);
        }

        /// <summary>
        /// Called when the client/game ticks
        /// </summary>
        /// <returns>Task responsible for ticking</returns>
        public async Task OnTick() {
            if (!firstTick) {
                OnLoad();
                this.firstTick = true;
            }

            // Guarantee async
            await Delay(100);
        }

    }
}
