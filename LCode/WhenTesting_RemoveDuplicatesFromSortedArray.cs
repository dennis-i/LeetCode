namespace LCode;

public class WhenTesting_RemoveDuplicatesFromSortedArray
{

    [Theory]
    [InlineData(new[] { 0, 1, 2, 3, 4, }, new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 })]
    [InlineData(new[] { 1, 2 }, new[] { 1, 1, 2 })]
    [InlineData(new[] { 5 }, new[] { 5 })]
    [InlineData(new[] { 7 }, new[] { 7, 7, 7, 7, 7 })]
    [InlineData(new[] { 1, 2 }, new[] { 1, 2, 2 })]
    public void TestIt(int[] expected, int[] nums)
    {
        int k = RemoveDuplicates(nums);
        Assert.Equal(expected.Length, k);
        for (int i = 0; i < k; i++)
            Assert.Equal(expected[i], nums[i]);
    }


    public int RemoveDuplicates(int[] nums)
    {
        if (nums.Length == 1) return 1;


        int idx1 = 0;
        int idx2 = 1;

        while (idx2 < nums.Length)
        {
            if (nums[idx1] == nums[^1])
                break;

            if (nums[idx1] == nums[idx2])
            {
                for (int i = idx2 + 1; i < nums.Length; ++i)
                {
                    nums[i - 1] = nums[i];
                }
            }
            else
            {
                ++idx1;
                ++idx2;
            }
        }

        return idx1 + 1;
    }

}