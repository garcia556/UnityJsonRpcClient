namespace UnityJsonRpcClient
{
	// REQ: with parameters
	// RES: empty response expected
	public class JsonRpcCallRequestReq<T> : JsonRpcCallBase<T, Empty>
	{
		protected override JsonRpcCallType type { get { return JsonRpcCallType.Request; } }
		private new Empty result;
		private new Empty Result { get { return Empty.New; } }

		internal JsonRpcCallRequestReq(JsonRpcService client, string method, T parameters) : base(client, method, parameters) { }
	}
}
