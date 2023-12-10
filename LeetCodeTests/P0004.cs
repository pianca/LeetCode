
namespace LeetCodeTests;

public class P0004
{
	[Theory]
	[InlineData(new int[] { 1, 3 }, new int[] { 2 }, 2.0)]
	[InlineData(new int[] { 1, 2 }, new int[] { 3, 4 }, 2.5)]
	public void MedianOfTwoSortedArrays(int[] nums1, int[] nums2, double expected)
	{
		var s = new Solution();
		var output = s.FindMedianSortedArrays(nums1, nums2);
		Assert.Equal(expected, output);
	}

	class Solution
	{
		public double FindMedianSortedArrays(int[] nums1, int[] nums2)
		{
			if (nums1.Length == 0)
				return Median(nums2);
			if (nums2.Length == 0)
				return Median(nums1);

			int[] merged = new int[nums1.Length + nums2.Length];

			bool oneWins;
			int idx1 = 0;
			int idx2 = 0;
			int idxMerged = 0;
			int? v1 = null;
			int? v2 = null;
			while (idxMerged < merged.Length)
			{
				if (v1.HasValue == false)
					v1 = idx1 >= nums1.Length ? null : nums1[idx1];

				if (v2.HasValue == false)
					v2 = idx2 >= nums2.Length ? null : nums2[idx2];

				if (v1.HasValue == false)
					oneWins = false;
				else if (v2.HasValue == false)
					oneWins = true;
				else
				{
					if (v1 > v2)
						oneWins = false;
					else
						oneWins = true;
				}

				if (oneWins)
				{
					merged[idxMerged++] = v1.Value;
					v1 = null;
					idx1++;
				}
				else
				{
					merged[idxMerged++] = v2.Value;
					v2 = null;
					idx2++;
				}
			}

			return Median(merged);
		}

		private double Median(int[] nums)
		{
			if ((nums.Length % 2) > 0)
				return nums[nums.Length / 2];

			double a = nums[nums.Length / 2 - 1];
			double b = nums[nums.Length / 2];
			return (a + b) / 2.0;
		}
	}
}