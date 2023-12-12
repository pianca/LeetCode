namespace LeetCodeTests;

public class P0009
{
	[Theory]
	[InlineData(121, true)]
	[InlineData(-121, false)]
	[InlineData(10, false)]
	public void PalindromeNumber(int num, bool expected)
	{
		var s = new Solution();
		var output = s.PalindromeNumber(num);
		Assert.Equal(expected, output);
	}

	class Solution
	{
		public bool PalindromeNumber(int x)
		{
			var s = x.ToString().AsSpan();
			for (int i = 0; i < s.Length / 2; i++)
			{
				if (s[i] != s[s.Length - 1 - i])
					return false;
			}
			return true;
		}
	}
}