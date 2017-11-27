using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace UnityJsonRpcClient
{
	public class HttpTransport : IReqRepTransport
	{
		public const int HTTP_STATUS_CODE_OK = 200;
		public const string ERR_MSG_HTTP_STATUS_CODE_NON_OK = "Non-OK HTTP response code received";

		private string url;

		public void Initialize(string resource)
		{
			this.url = resource;
		}

		public IEnumerator Notification(string data, Action<JsonRpcTransportException> callback)
		{
			return this.RequestInternal(data, (JsonRpcTransportException err, string res) => { callback.Invoke(err); });
		}

		public IEnumerator Request(string data, System.Action<JsonRpcTransportException, string> callback)
		{
			return this.RequestInternal(data, callback);
		}

		private IEnumerator RequestInternal(string data, Action<JsonRpcTransportException, string> callback)
		{
			var www = UnityWebRequest.Post(this.url, data);
			yield return www.Send();

			if (www.isError)
			{
				callback.Invoke(new JsonRpcTransportException(www.error), null);
				yield break;
			}

			if (www.responseCode != HTTP_STATUS_CODE_OK)
			{
				callback.Invoke(new JsonRpcTransportException(ERR_MSG_HTTP_STATUS_CODE_NON_OK), null);
				yield break;
			}

			callback.Invoke(null, www.downloadHandler.text);
		}
	}
}
