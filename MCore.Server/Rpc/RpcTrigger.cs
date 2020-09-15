using CitizenFX.Core;
using MCore.Base;
using MCore.Base.Rpc;

namespace MCore.Server.Rpc {

    /// <summary>
    /// Server's rpc trigger
    /// </summary>
    public class RpcTrigger : IRpcTrigger {

		public void Fire(RpcMessage message) {
			if (message.Target != null) {
				message.Target.TriggerEvent(message.Event, message.Build());
			} else {
				BaseScript.TriggerClientEvent(message.Event, message.Build());
			}
		}
	}
}
