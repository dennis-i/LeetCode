namespace LCode;

//TODO
public class WhenTesting_NextPermutation
{
    [Theory]
    [InlineData(new[] { 1, 3, 2 }, new[] { 1, 2, 3 })]
    [InlineData(new[] { 1, 2, 3 }, new[] { 3, 2, 1 })]
    [InlineData(new[] { 1, 5, 1 }, new[] { 1, 1, 5 })]
    [InlineData(new[] { 2, 1, 3 }, new[] { 1, 3, 2 })]
    //[InlineData(new[] { 2, 1, 3 }, new[] { 1, 5, 8, 4, 7, 6, 5, 3, 1 })]
    public void TestIt(int[] expected, int[] nums)
    {
        NextPermutation(nums);
        Assert.Equal(expected, nums);
    }

    public void NextPermutation(int[] nums)
    {
        int idx = nums.Length - 1;
        while (idx > 1)
        {
            if (nums[idx] > nums[idx - 1])
                break;
            idx--;
        }

        int n = nums[idx - 1];
        int swapIdx = idx;
        for (int i = idx; i < nums.Length - 1; i++)
        {
            if (n > nums[i])
                break;
            swapIdx++;
        }

        (nums[swapIdx], nums[idx - 1]) = (nums[idx - 1], nums[swapIdx]);

        int l = idx;
        int r = nums.Length - 1;
        while (l < r)
        {
            (nums[l], nums[r]) = (nums[r], nums[l]);
            r--;
            l++;
        }

    }
}