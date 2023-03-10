using System;
namespace etherscan_test.Helpers
{
	public static class NumberHelper
	{
		public static string DecimalToHex(UInt64 number)
		{
			return $"0x{number.ToString("X")}";
		}

		public static UInt64 HexToDecimal(string number)
		{
			// Ignore the '0x' portion of the number
			return Convert.ToUInt64(number.Substring(2), 16);
		}
	}
}

