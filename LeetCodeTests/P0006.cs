namespace LeetCodeTests;

public class P0006
{
	[Theory]
	[InlineData("PAYPALISHIRING", 3, "PAHNAPLSIIGYIR")]
	[InlineData("PAYPALISHIRING", 4, "PINALSIGYAHRPI")]
	[InlineData("A", 1, "A")]
	public void TwoSum(string input, int numRows, string expected)
	{
		var s = new Solution();
		var output = s.Convert(input, numRows);
		Assert.Equal(expected, output);
	}

	class Solution
	{
		public string Convert(string s, int numRows)
		{
			if (numRows == 1)
				return s;

			Span<char> res = stackalloc char[s.Length];
			int resIdx = 0;
			int nr = numRows - 1;
			for (int r = 0; r < numRows; r++)
			{
				int c = 0;
				int sIdx = c * 2 + r;
				while (sIdx < s.Length)
				{
					var m = (c % nr);
					bool toPush = m == 0 || (nr - r) == m;
					if (toPush)
					{
						res[resIdx++] = s[sIdx];
						if (resIdx == s.Length)
							return new string(res);
					}

					c++;
					sIdx = c * 2 + r;
				}
			}

			return "";
		}


		public string ConvertSlow(string s, int numRows)
		{
			if (numRows == 1)
				return s;

			List<char>[] rows = new List<char>[numRows];
			int numElements = s.Length / numRows + 1;
			for (int r = 0; r < numRows; r++)
			{
				rows[r] = new List<char>(numElements);
			}

			int rowIdx = 0;
			bool incr = true;
			for (int i = 0; i < s.Length; i++)
			{
				rows[rowIdx].Add(s[i]);

				if (incr)
				{
					if (rowIdx == (numRows - 1))
					{
						incr = false;
					}
				}
				else
				{
					if (rowIdx == 0)
					{
						incr = true;
					}
				}

				rowIdx = incr ? rowIdx + 1 : rowIdx - 1;
			}

			rowIdx = 0;
			char[] res = new char[s.Length];
			foreach (var row in rows)
				foreach (char c in row)
					res[rowIdx++] = c;

			return new string(res);

			throw new ArgumentException();
		}
	}
}