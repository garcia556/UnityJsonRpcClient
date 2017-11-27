using System.Collections;

namespace UnityJsonRpcClient
{
	// REQ: with parameters
	// RES: some response expected
	public class JsonRpcCallBase<TReq, TRes>
	{
		protected JsonRpcService client;
		protected virtual JsonRpcCallType type { get { return JsonRpcCallType.Unknown; } }

		protected string method;
		protected TReq parameters;
		protected TRes result;
		protected JsonRpcException error = null;

		public TRes Result { get { return this.result; } }
		public JsonRpcException Error { get { return this.error; } }
		public bool isError { get { return this.error != null; } }

		internal JsonRpcCallBase(JsonRpcService client, string method, TReq parameters)
		{
			this.client = client;
			this.method = method;
			this.parameters = parameters;
		}

		public IEnumerator Send()
		{
			var output = new JsonRpcCallOutput();
			yield return this.client.Send<TReq, TRes>(this.type, this.method, this.parameters, output);
			this.error = output.error;
			this.result = (TRes)output.result;
		}
	}
}
