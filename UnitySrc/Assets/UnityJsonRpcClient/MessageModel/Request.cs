namespace UnityJsonRpcClient
{
	[System.Serializable]	
	public class Request<T> : RequestBase
    {
		public T @params;

		public Request(string id, string method, T parameters) : base(id, method)
		{
			this.@params = parameters;
		}
    }

	[System.Serializable]	
	public class RequestBase : NotificationBase
	{
		public string id;

		public RequestBase(string id, string method) : base(method)
		{
			this.id = id;
		}
	}
}
