using Newtonsoft.Json.Linq;

using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LeetCodeTests;

public class P0010
{
	[Theory]
	[InlineData("aa", "a", false)]
	[InlineData("aa", "a*", true)]
	[InlineData("ab", ".*", true)]
	[InlineData("mississippi", "mis*is*ip*.", true)]
	[InlineData("ab", ".*c", false)]
	[InlineData("aaa", "aaaa", false)]
	[InlineData("aaa", "a*a", true)]
	[InlineData("aaa", "ab*ac*a", true)]
	[InlineData("a", ".*..a*", false)]
	public void IsMatch(string s, string p, bool expected)
	{
		Solution solution = new Solution();
		Assert.Equal(expected, solution.IsMatch(s, p));
	}

	class Solution
	{
		static readonly StringBuilder sb = new StringBuilder(20);
		public bool IsMatch(string s, string p)
		{
			ReadOnlySpan<char> input = s.AsSpan();
			List<IPattern> patternList = GetPatterns(p.AsSpan());
			HashSet<string> matchingOptions = new(20) { "" };
			for (int idx = 0; idx < patternList.Count; idx++)
			{
				IPattern pattern = patternList[idx]!;
				switch (pattern)
				{
					case PatternChar:
						matchingOptions = (pattern as PatternChar).AppendToMatchingOptions(matchingOptions, pattern as PatternChar);
						break;
					case PatternMultiple:
						matchingOptions = (pattern as PatternMultiple).AppendToMatchingOptions(matchingOptions, input, pattern as PatternMultiple, patternList);
						break;
				}
				var matchFound = KeepValidMatchingOptions(matchingOptions, input, IsMatchingAllPattern(pattern, patternList));
				if (matchFound)
					return true;
			}

			return false;
		}

		interface IPattern { int Idx { get; } }

		record PatternChar(char Value, int Idx) : IPattern
		{
			internal HashSet<string> AppendToMatchingOptions(HashSet<string> matchingOptions, PatternChar pattern /*InputString input*/)
			{
				HashSet<string> newMatchingOptions = new HashSet<string>(20);
				foreach (var old in matchingOptions)
				{
					newMatchingOptions.Add(old + pattern.Value);
				}
				return newMatchingOptions;
			}
		}
		record PatternMultiple(char Value, int Idx) : IPattern
		{
			internal HashSet<string> AppendToMatchingOptions(HashSet<string> matchingOptions, ReadOnlySpan<char> input, PatternMultiple pattern, List<IPattern> patternList)
			{
				HashSet<string> newMatchingOptions = new HashSet<string>(20);
				var oldMatchingOptionsMinLen = matchingOptions.Any() ? matchingOptions.Min(x => x.Length) : 0;
				var patternStringLen = input.Length - oldMatchingOptionsMinLen - pattern.RemainingPatternChars(patternList);
				var patternStringOptions = CreateMatchingStringOptions(patternStringLen, pattern.Value);
				foreach (var old in matchingOptions)
				{
					var endIdx = patternStringLen - (old.Length - oldMatchingOptionsMinLen) + 1;
					for (int idx = 0; idx < endIdx; idx++)
					{
						newMatchingOptions.Add(old + patternStringOptions[idx]);
					}
				}
				return newMatchingOptions;
			}

			private int RemainingPatternChars(List<IPattern> patternList)
			{
				return patternList.Count(x => x.Idx > Idx && x is PatternChar);
			}
		}
		private static string[] CreateMatchingStringOptions(int maxLen, char value)
		{
			var arrayLen = maxLen + 1;
			var newOptions = new string[arrayLen];
			sb.Clear();
			for (int i = 0; i < arrayLen; i++)
			{
				newOptions[i] = sb.ToString();
				sb.Append(value);
			}
			return newOptions;
		}
		private bool KeepValidMatchingOptions(HashSet<string> matchingOptions, ReadOnlySpan<char> input, bool isLastPatternElement)
		{
			foreach (var matchingOption in matchingOptions)
			{
				bool isMatch = IsPartialOrFullMatch(input, matchingOption);
				if (isMatch)
				{
					if (isLastPatternElement && IsMatchingAllString(matchingOption, input))
						return true;
				}
				else
				{
					matchingOptions.Remove(matchingOption);
				}
			}
			return false;
		}
		private bool IsPartialOrFullMatch(ReadOnlySpan<char> inputString, string matchingOption)
		{
			if (matchingOption.Length > inputString.Length)
				return false;

			var o = matchingOption.AsSpan();
			for (int idx = 0; idx < matchingOption.Length; idx++)
			{
				if (IsCharMatch(inputString[idx], o[idx]) == false)
					return false;
			}
			return true;
		}
		private bool IsMatchingAllString(string k, ReadOnlySpan<char> s) => k.Length == s.Length;
		private bool IsMatchingAllPattern(IPattern pattern, List<IPattern> patternList) => (pattern.Idx + 1) == patternList.Count;
		private bool IsCharMatch(char i, char p)
		{
			if (p == '.')
				return true;
			return i == p;
		}
		private List<IPattern> GetPatterns(ReadOnlySpan<char> pattern)
		{
			var count = 0;
			List<IPattern> list = new List<IPattern>();
			for (int i = 0; i < pattern.Length; i++)
			{
				var c = pattern[i];

				if ((i + 1) < pattern.Length)
				{
					var n = pattern[i + 1];
					if (n == '*')
					{
						i++;
						list.Add(new PatternMultiple(c, count++));
						continue;
					}
				}

				list.Add(new PatternChar(c, count++));
			}

			return list;
		}
	}
}