using System;
using System.Text.Json.Nodes;

namespace etherscan_test.Helpers
{
	public static class JsonHelper
	{
		public static string BuildRequestBody(string method, string param)
		{
			return $"{{\"jsonrpc\": \"2.0\", \"method\": \"{method}\", \"params\": {param}, \"id\": 0}}";
		}
	}
}

