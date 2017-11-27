using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityJsonRpcClient
{
	[System.Serializable]
	public class Empty
	{
		public static Empty New
		{
			get { return new Empty(); }
		}
	}

	public class JsonRpcService
	{
		public const string ERR_MSG_EMPTY_TRANSPORT_RESPONSE = "Transport returned empty response";
		
		public static bool RequestInProgress = false;

		private static int Id = 0;

		private IReqRepTransport transport;
		private bool parallelRequestsAllowed;

		public JsonRpcService(IReqRepTransport transport, bool parallelRequestsAllowed)
		{
			this.transport = transport;
			this.parallelRequestsAllowed = parallelRequestsAllowed;
		}

#region API
		public JsonRpcCallNotification Notification(string method)
		{
			return new JsonRpcCallNotification(this, method);
		}

		public JsonRpcCallNotificationReq<T> Notification<T>(string method, T parameters)
		{
			return new JsonRpcCallNotificationReq<T>(this, method, parameters);
		}

		public JsonRpcCallRequest Request(string method)
		{
			return new JsonRpcCallRequest(this, method);
		}

		public JsonRpcCallRequestReq<T> Request<T>(string method, T parameters)
		{
			return new JsonRpcCallRequestReq<T>(this, method, parameters);
		}

		public JsonRpcCallRequestRes<T> Request<T>(string method)
		{
			return new JsonRpcCallRequestRes<T>(this, method);
		}

		public JsonRpcCallRequestReqRes<TReq, TRes> Request<TReq, TRes>(string method, TReq parameters)
		{
			return new JsonRpcCallRequestReqRes<TReq, TRes>(this, method, parameters);
		}
#endregion

		private IEnumerator SendNotification<TReq>(string method, TReq parameters, JsonRpcCallOutput output)
		{
			Notification<TReq> request = new Notification<TReq>(method, parameters);

			string requestTransport = JsonUtility.ToJson(request);

			Exception error = null;
			yield return this.transport.Notification(requestTransport, (JsonRpcTransportException err) => { error = err; });
			if (error == null)
				yield break;
			output.error = new JsonRpcException(error.Message);
		}

		private IEnumerator SendRequest<TReq, TRes>(string method, TReq parameters, JsonRpcCallOutput output)
		{
			string id = (++Id).ToString();

			Request<TReq> request = new Request<TReq>(id, method, parameters);
			Response<TRes> response = null;

			string requestTransport = JsonUtility.ToJson(request);

			Exception error = null;
			string responseTransport = null;
			yield return this.transport.Request(requestTransport, (JsonRpcTransportException err, string res) => { error = err; responseTransport = res; });
			if (string.IsNullOrEmpty(responseTransport))
				error = new JsonRpcException(ERR_MSG_EMPTY_TRANSPORT_RESPONSE);

			if (error != null)
			{
				output.error = (JsonRpcException)error;
				yield break;
			}

			response = JsonUtility.FromJson<Response<TRes>>(responseTransport);
			if (response.error.code != Int32.MinValue)
			{
				output.error = (JsonRpcException)error;
				yield break;
			}

			output.result = response.result;
		}

		internal IEnumerator Send<TReq, TRes>(JsonRpcCallType type, string method, TReq parameters, JsonRpcCallOutput output)
		{
			if (!this.parallelRequestsAllowed)
				while (JsonRpcService.RequestInProgress)
					yield return new WaitForEndOfFrame();
			
			JsonRpcService.RequestInProgress = true;

			if (type == JsonRpcCallType.Notification)
				yield return this.SendNotification<TReq>(method, (TReq)parameters, output);
			else
			if (type == JsonRpcCallType.Request)
				yield return this.SendRequest<TReq, TRes>(method, (TReq)parameters, output);

			JsonRpcService.RequestInProgress = false;
		}
	}
}
