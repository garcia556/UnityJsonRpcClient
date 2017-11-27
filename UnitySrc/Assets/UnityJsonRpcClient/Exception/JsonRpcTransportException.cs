using System;

namespace UnityJsonRpcClient
{
	public class JsonRpcTransportException : JsonRpcException
	{
		public JsonRpcTransportException(string message) : base(message) { }
	}
}
