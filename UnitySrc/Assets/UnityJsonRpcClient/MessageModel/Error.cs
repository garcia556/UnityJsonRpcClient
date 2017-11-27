using UnityEngine;

namespace UnityJsonRpcClient
{
	[System.Serializable]
    public class Error
    {
		public int code = int.MinValue;
		public string message;
    }
}
