namespace UnityJsonRpcClient
{
	// REQ: no parameters
	// RES: no response from server expected
	public class JsonRpcCallNotification : JsonRpcCallBase<Empty, Empty>
	{
		protected override JsonRpcCallType type { get { return JsonRpcCallType.Notification; } }
		private new Empty parameters;
		private new Empty result;
		private new Empty Result { get { return Empty.New; } }
		private new JsonRpcException Error { get { return this.error; } }
		private new bool isError { get { return this.error != null; } }

		internal JsonRpcCallNotification(JsonRpcService client, string method) : base(client, method, Empty.New) { }
	}
}
