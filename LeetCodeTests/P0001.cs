namespace LeetCodeTests;

public class P0001
{
    [Theory]
    [InlineData(new int[] { 2, 7, 11, 15 }, 9, new int[] { 0, 1 })]
    public void TwoSum(int[] nums, int target, int[] expected)
    {
        var s = new Solution();
        var output = s.TwoSum(nums, target);
        Assert.True(Enumerable.SequenceEqual(expected, output));
    }

    class Solution
    {
        public int[] TwoSum(int[] nums, int target)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < nums.Length; j++)
                {
                    if (i == j)
                        continue;

                    if (target == (nums[i] + nums[j]))
                        return new int[] { i, j };
                }
            }

            throw new ArgumentException();
        }
    }
}