namespace LeetCodeTests;

public class P0008
{
	[Theory]
	[InlineData("  +  413", 0)]
	[InlineData("-5-", -5)]
	[InlineData("+-12", 0)]
	[InlineData("42", 42)]
	[InlineData("   -42", -42)]
	[InlineData("4193 with words", 4193)]
	[InlineData("word and 93", 0)]
	[InlineData("-91283472332", -2147483648)]
	public void MyAtoi(string input, int expected)
	{
		var s = new Solution();
		var output = s.MyAtoi(input);
		Assert.Equal(expected, output);
	}

	class Solution
	{
		const uint TEN = 10;
		public int MyAtoi(string s)
		{
			bool digitRead = false;
			int sign = +1;
			Int64 result = 0;
			bool signRead = false;
			uint plsuMax = int.MaxValue;
			uint minusMax = plsuMax + 1;

			foreach (char c in s)
			{
				switch (c)
				{
					case ' ':
						if (signRead && digitRead == false)
							return 0;
						if (digitRead)
							return (int)result * sign;
						break;
					case '+':
						if (signRead && digitRead==false)
							return 0;
						signRead = true;
						if (digitRead)
							return (int)result * sign;
						break;
					case '-':
						if (signRead && digitRead == false)
							return 0;
						signRead = true;
						if (digitRead)
							return (int)result * sign;
						sign = -1;
						break;
					case '0':
						digitRead = true;
						result = result * TEN;
						break;
					case '1':
						digitRead = true;
						result = result * TEN + 1u;
						break;
					case '2':
						digitRead = true;
						result = result * TEN + 2u;
						break;
					case '3':
						digitRead = true;
						result = result * TEN + 3u;
						break;
					case '4':
						digitRead = true;
						result = result * TEN + 4u;
						break;
					case '5':
						digitRead = true;
						result = result * TEN + 5u;
						break;
					case '6':
						digitRead = true;
						result = result * TEN + 6u;
						break;
					case '7':
						digitRead = true;
						result = result * TEN + 7u;
						break;
					case '8':
						digitRead = true;
						result = result * TEN + 8u;
						break;
					case '9':
						digitRead = true;
						result = result * TEN + 9u;
						break;

					default:
						if (digitRead)
							return (int)result * sign;
						else
							return 0;
				}

				if (sign == 1)
				{
					if (result > plsuMax)
						return int.MaxValue;
				}
				else
				{
					if (result > minusMax)
						return int.MinValue;
				}
			}

			if (sign == 1)
			{
				if (result > plsuMax)
					return int.MaxValue;
			}
			else
			{
				if (result > minusMax)
					return int.MinValue;
			}

			return (int)result * sign;
		}
	}
}