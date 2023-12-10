
namespace LeetCodeTests;

public class P0005
{
	[Theory]
	[InlineData("a", new[] { "a" })]
	[InlineData("babad", new[] { "bab", "aba" })]
	[InlineData("cbbd", new[] { "bb" })]
	public void TwoSum(string input, string[] expected)
	{
		var s = new Solution();
		var output = s.LongestPalindrome(input);
		Assert.Contains(output, expected);
	}

	class Solution
	{
		public string LongestPalindrome(string s)
		{
			var data = s.AsSpan();
			for (int ssLength = s.Length; ssLength >= 1; ssLength--)
			{
				int maxOffset = s.Length - ssLength;
				for (int start = 0; start <= maxOffset; start++)
				{
					var ss = data.Slice(start, ssLength);
					if (IsSubString(ref ss))
						return new string(ss);
				}
			}
			throw new ArgumentException("");
		}

		private bool IsSubString(ref ReadOnlySpan<char> ss)
		{
			var halfLength = ss.Length / 2;
			var leftOffset = halfLength - 1;
			var rightOffset = ss.Length % 2 == 0 ? halfLength : halfLength + 1;
			for (int i = 0; i < halfLength; i++)
			{
				if (ss[leftOffset - i] != ss[rightOffset + i])
					return false;
			}
			return true;
		}

		/// <summary>
		/// optimized version
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public string LongestPalindromeOptimized(string s)
		{
			var data = s.AsSpan();
			for (int ssLength = s.Length; ssLength >= 1; ssLength--)
			{
				if (ssLength == 1)
					return new string(data.Slice(0, 1));

				int maxOffset = s.Length - ssLength;
				for (int start = 0; start <= maxOffset; start++)
				{
					var ss = data.Slice(start, ssLength);

					var halfLength = ss.Length / 2;
					var leftOffset = halfLength - 1;
					var rightOffset = ss.Length % 2 == 0 ? halfLength : halfLength + 1;
					int i = 0;
					while (i < halfLength && ss[leftOffset - i] == ss[rightOffset + i])
					{
						i++;
						if (i == halfLength)
							return new string(ss);
					}
				}
			}
			return "";
		}
	}
}