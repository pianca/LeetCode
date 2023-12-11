using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace LeetCodeTests;

public class P0007
{
	[Theory]
	[InlineData(123, 321)]
	[InlineData(-123, -321)]
	[InlineData(120, 021)]
	[InlineData(-2147483412, -2143847412)]
	public void TwoSum(int num, int expected)
	{
		var s = new Solution();
		var output = s.Reverse(num);
		Assert.Equal(expected, output);
	}

	class Solution
	{
		public int Reverse(int x)
		{
			int max = int.MaxValue / 10;
			int min = int.MinValue / 10;

			int num = x;
			int output = 0;

			while (num != 0)
			{
				var quotient = num / 10;
				var toProcess = quotient * 10;
				var digit = num - toProcess;
				output = output * 10 + digit;

				if (quotient == 0)
					return output;

				if (output > max || output < min)
					return 0;

				num = quotient;
			}

			return 0;
		}
	}
}