namespace UnityJsonRpcClient
{
	// REQ: no request parameters
	// RES: some response expected
	public class JsonRpcCallRequestReqRes<TReq, TRes> : JsonRpcCallBase<TReq, TRes>
	{
		protected override JsonRpcCallType type { get { return JsonRpcCallType.Request; } }

		internal JsonRpcCallRequestReqRes(JsonRpcService client, string method, TReq parameters) : base(client, method, parameters) { }
	}
}
