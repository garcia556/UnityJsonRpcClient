namespace UnityJsonRpcClient
{
	// REQ: no request parameters
	// RES: some response expected
	public class JsonRpcCallRequestRes<T> : JsonRpcCallBase<Empty, T>
	{
		protected override JsonRpcCallType type { get { return JsonRpcCallType.Request; } }
		private new Empty parameters;

		internal JsonRpcCallRequestRes(JsonRpcService client, string method) : base(client, method, Empty.New) { }
	}
}
