using System;
using System.Text.Json;
namespace etherscan_test.Helpers
{
	public static class StringHelper
	{
		public static string PrettyPrintObject(object obj)
		{
			return JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
		}
	}
}

