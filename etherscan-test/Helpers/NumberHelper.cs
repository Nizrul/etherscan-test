using System;
namespace etherscan_test.Helpers
{
	public static class NumberHelper
	{
		public static string DecimalToHex(int number)
		{
			return $"0x{number.ToString("X")}";
		}

		public static int HexToDecimal(string number)
		{
			// Ignore the '0x' portion of the number
			return Convert.ToInt32(number.Substring(2), 16);
		}
	}
}

