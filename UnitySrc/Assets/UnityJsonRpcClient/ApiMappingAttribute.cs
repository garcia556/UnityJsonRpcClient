using System;

namespace UnityJsonRpcClient
{
	[System.AttributeUsage(System.AttributeTargets.Class)]  
	public class ApiMappingAttribute : System.Attribute  
	{  
		private string method;
		public string Method { get { return this.method; } }
		public Type Request = typeof(Empty);
		public Type Response = typeof(Empty);

		public ApiMappingAttribute(string method)
		{
			this.method = method;
		}
	}
}
