using System;

namespace UnityJsonRpcClient
{
	public class JsonRpcException : Exception
	{
		public JsonRpcException(string message) : base(message) { }
	}
}
