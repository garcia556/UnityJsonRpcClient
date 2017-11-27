using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityJsonRpcClientDemo
{
	public class Bootstrap : MonoBehaviour
	{
#region Data model
		[System.Serializable]
		public class DataRequest { public string fromClient; }

		[System.Serializable]
		public class DataResponse { public string fromServer; }
#endregion

		void Awake()
		{
			this.StartCoroutine(this.AwakeWorker());
		}

		private void ProcessResult(UnityJsonRpcClient.JsonRpcTransportException error, int result)
		{
			if (error != null)
			{
				Debug.LogWarningFormat("Error: {0}", error.Message);
				return;
			}

			Debug.LogFormat("Callback: {0}", result);
		}

		private static void Log(string message)
		{
			Debug.LogFormat("Client: {0}", message);
		}

		private IEnumerator AwakeWorker()
		{
			UnityJsonRpcClient.JsonRpcService client = new UnityJsonRpcClient.JsonRpcService(new MockTransportService(), false);

			var req1 = client.Request<MockServerApi.AddReq, MockServerApi.AddRes>("Add", new MockServerApi.AddReq() { a = 2, b = 3 });
			yield return req1.Send();
			Log(string.Format("Add: {0}", req1.Result.sum));

			var req2 = client.Request<string>("GetString");
			yield return req2.Send();
			Log(string.Format("GetString: {0}", req2.Result));

			var req3 = client.Notification<string>("NotifyStr", "data from client");
			yield return req3.Send();
			Log("NotifyStr called");

			var req4 = client.Notification("Notify");
			yield return req4.Send();
			Log("Notify called");

			yield break;
		}
	}
}
