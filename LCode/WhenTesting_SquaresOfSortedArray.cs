namespace LCode;

public class WhenTesting_SquaresOfSortedArray
{
    [Theory]
    [InlineData(new[] { 0, 1, 9, 16, 100 }, new[] { -4, -1, 0, 3, 10 })]
    [InlineData(new[] { 4, 9, 9, 49, 121 }, new[] { -7, -3, 2, 3, 11 })]
    [InlineData(new[] {49}, new[] { -7 })]
    public void TestIt(int[] expected, int[] nums)
    {
        Assert.Equal(expected, SortedSquares(nums));
    }

    public int[] SortedSquares(int[] nums)
    {

        int pow(int val) => val * val;
        int abs(int v) => Math.Abs(v);

        int[] result = new int[nums.Length];

        int r = nums.Length - 1;
        int l = 0;

        int dstIdx = r;

        while (l <= r)
        {
            if (abs(nums[l]) > abs(nums[r]))
            {
                result[dstIdx] = pow(nums[l]);
                ++l;
            }
            else
            {
                result[dstIdx] = pow(nums[r]);
                --r;
            }
            dstIdx--;

        }

        return result;
    }
}