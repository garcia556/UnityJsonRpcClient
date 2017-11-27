using System;
using UnityEngine;

namespace UnityJsonRpcClientDemo
{
	public static class MockServerApi
	{
		private static void Log(string message)
		{
			Debug.LogFormat("MockServerApi: {0}", message);
		}

//////////////////////////////////////////////////////////////////////

		[System.Serializable] public class AddReq { public int a; public int b; }
		[System.Serializable] public class AddRes { public int sum; }
		public static AddRes Add(AddReq req)
		{
			Log(string.Format("Add called: {0} + {1}", req.a, req.b));
			return new AddRes() { sum = req.a + req.b };
		}

		public static string GetString()
		{
			Log("GetString called");
			return "some string constant";
		}

		public static void NotifyStr(string data)
		{
			Log(string.Format("NotifyStr called: {0}", data));
		}

		public static void Notify()
		{
			Log("Notify called");
		}
	}
}
