namespace UnityJsonRpcClient
{
	// REQ: with parameters
	// RES: no response from server expected
	public class JsonRpcCallNotificationReq<T> : JsonRpcCallBase<T, Empty>
	{
		protected override JsonRpcCallType type { get { return JsonRpcCallType.Notification; } }
		private new Empty result;
		private new Empty Result { get { return Empty.New; } }
		private new JsonRpcException Error { get { return this.error; } }
		private new bool isError { get { return this.error != null; } }

		internal JsonRpcCallNotificationReq(JsonRpcService client, string method, T parameters) : base(client, method, parameters) { }
	}
}
