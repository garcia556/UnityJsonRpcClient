namespace UnityJsonRpcClient
{
	// REQ: no request parameters
	// RES: empty response expected
	public class JsonRpcCallRequest : JsonRpcCallBase<Empty, Empty>
	{
		protected override JsonRpcCallType type { get { return JsonRpcCallType.Request; } }
		private new Empty parameters;
		private new Empty result;
		private new Empty Result { get { return Empty.New; } }

		internal JsonRpcCallRequest(JsonRpcService client, string method) : base(client, method, Empty.New) { }
	}
}
