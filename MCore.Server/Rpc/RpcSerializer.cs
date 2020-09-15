using MCore.Base;
using MCore.Base.Rpc;
using Newtonsoft.Json;

namespace MCore.Server.Rpc {

    /// <summary>
    /// Server's rpc serializer
    /// </summary>
    public class RpcSerializer : IRpcSerializer {

		public string Serialize(object obj) => JsonConvert.SerializeObject(obj);

		public T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);
	}
}
