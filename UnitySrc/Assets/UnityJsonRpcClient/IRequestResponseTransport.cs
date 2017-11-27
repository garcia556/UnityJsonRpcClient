using System;
using System.Collections;

namespace UnityJsonRpcClient
{
	public interface IReqRepTransport
	{
		void Initialize(string resource);
		IEnumerator Notification(string data, Action<JsonRpcTransportException> callback);
		IEnumerator Request(string data, Action<JsonRpcTransportException, string> callback);
	}
}
