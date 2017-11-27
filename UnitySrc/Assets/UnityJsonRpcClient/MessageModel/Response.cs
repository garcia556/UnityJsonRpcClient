namespace UnityJsonRpcClient
{
	[System.Serializable]
    public class Response<T> : ResponseBase
    {
		public T result;
    }

	[System.Serializable]
	public class ResponseBase : MessageBase
	{
		public string id;
		public Error error;
	}
}
