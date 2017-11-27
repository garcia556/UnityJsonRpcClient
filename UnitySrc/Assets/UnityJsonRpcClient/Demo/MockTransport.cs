using System;
using System.Collections;
using UnityEngine;
using JsonRpcTransportException = UnityJsonRpcClient.JsonRpcTransportException;

namespace UnityJsonRpcClientDemo
{
	public class MockTransportService : UnityJsonRpcClient.IReqRepTransport
	{
		public void Initialize(string resource) { }

		public IEnumerator Notification(string data, Action<JsonRpcTransportException> callback)
		{
			return this.NotificationInternal(data, callback);
		}

		private IEnumerator NotificationInternal(string data, Action<JsonRpcTransportException> callback)
		{
			var req = JsonUtility.FromJson<UnityJsonRpcClient.NotificationBase>(data);
			switch (req.method)
			{
				case "NotifyStr":
					var req1 = JsonUtility.FromJson<UnityJsonRpcClient.Request<string>>(data);
					MockServerApi.NotifyStr(req1.@params);
					break;
				case "Notify":
					MockServerApi.Notify();
					break;
			}

			yield break;
		}

		public IEnumerator Request(string data, Action<JsonRpcTransportException, string> callback)
		{
			return this.RequestInternal(data, callback);
		}

		private IEnumerator RequestInternal(string data, Action<JsonRpcTransportException, string> callback)
		{
			if (callback == null)
				yield break;

			var req = JsonUtility.FromJson<UnityJsonRpcClient.NotificationBase>(data);
			switch (req.method)
			{
				case "Add":
					var req1 = JsonUtility.FromJson<UnityJsonRpcClient.Request<MockServerApi.AddReq>>(data);
					var res1 = new UnityJsonRpcClient.Response<MockServerApi.AddRes>();
					res1.result = MockServerApi.Add(req1.@params);
					callback.Invoke(null, JsonUtility.ToJson(res1));
					break;
				case "GetString":
					var res2 = new UnityJsonRpcClient.Response<string>();
					res2.result = MockServerApi.GetString();
					callback.Invoke(null, JsonUtility.ToJson(res2));
					break;
			}

			yield break;
		}
	}
}
