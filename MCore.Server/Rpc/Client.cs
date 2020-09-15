using CitizenFX.Core;

namespace MCore.Server.Rpc
{
	public static class Client {

		public static ServerRpcRequest Event(string @event) {
			Debug.WriteLine(@event);
			return new ServerRpcRequest(@event, new RpcHandler(), new RpcTrigger(), new RpcSerializer());
		}
	}
}
