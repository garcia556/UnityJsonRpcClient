using UnityEngine;

namespace UnityJsonRpcClient
{
	[System.Serializable]
	public abstract class MessageBase
	{
		public string jsonrpc = "2.0";
	}
}
