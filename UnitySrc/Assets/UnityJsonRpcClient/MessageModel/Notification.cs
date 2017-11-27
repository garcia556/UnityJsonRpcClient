using UnityEngine;

namespace UnityJsonRpcClient
{
	[System.Serializable]	
	public class Notification<T> : NotificationBase
	{
		public T @params;

		internal Notification(string method, T parameters) : base(method)
		{
			this.@params = parameters;
		}
	}

	[System.Serializable]
	public class NotificationBase : MessageBase
	{
		public string method;

		public NotificationBase(string method)
		{
			this.method = method;
		}
	}
}
