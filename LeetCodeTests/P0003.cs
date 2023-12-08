namespace LeetCodeTests;

public class P0003
{
	[Theory]
	[InlineData("abcabcbb", 3)]
	[InlineData("bbbbb", 1)]
	[InlineData("pwwkew", 3)]
	public void LongestSubstringWithoutRepeatingCharacters(string input, int expected)
	{
		var s = new Solution();
		var output = s.LengthOfLongestSubstring(input);
		Assert.Equal(expected, output);
	}

	class Solution
	{
		public int LengthOfLongestSubstring(string s)
		{
			if (s.Length == 0)
				return 0;
			if (s.Length == 1)
				return 1;

			int startIdx = 0;
			var bestLen = 1;
			var charsSeen = new Dictionary<char, int>();

			while (startIdx < (s.Length - 1))
			{
				charsSeen.Clear();

				var c = s[startIdx];
				charsSeen[c]=startIdx;
				int len = 1;

				int idx = startIdx + 1;
				while (idx < s.Length)
				{
					c = s[idx];

					if (charsSeen.ContainsKey(c))
					{
						bestLen = len > bestLen ? len : bestLen;
						startIdx = charsSeen[c] + 1;
						break;
					}

					charsSeen[c] = idx;
					len++;
					idx++;

					if (idx >= s.Length)
					{
						bestLen = len > bestLen ? len : bestLen;
						return bestLen;
					}
				}
			}

			return bestLen;
		}
	}
}