namespace LCode;

public class WhenTesting_SearchInRotatedSortedArray
{
    [Theory]
    [InlineData(4, new[] { 4, 5, 6, 7, 0, 1, 2 }, 0)]
    [InlineData(0, new[] { 0, 1, 2, 4, 5, 6, 7 }, 0)]
    [InlineData(-1, new[] { 4, 5, 6, 7, 0, 1, 2 }, 3)]
    [InlineData(-1, new[] { 1 }, 0)]
    [InlineData(0, new[] { 1 }, 1)]
    [InlineData(1, new[] { 1, 3 }, 3)]
    [InlineData(-1, new[] { 1, 3 }, 2)]
    [InlineData(0, new[] { 3, 1 }, 3)]
    [InlineData(1, new[] { 3, 5, 1 }, 5)]
    public void TestIt(int expected, int[] nums, int target)
    {
        Assert.Equal(expected, Search(nums, target));
    }

    public int Search(int[] nums, int target)
    {
        int low = 0;
        int high = nums.Length - 1;

        if (low == high)
            return target == nums[0] ? 0 : -1;

        while (low <= high)
        {

            if (nums[high] < target)
                high--;
            else if (nums[low] > target)
                low++;


            else
            {
                int mid = low + (high - low) / 2;
                if (target > nums[mid])
                {
                    low++;
                }
                else if (target < nums[mid])
                {
                    high--;
                }
                else
                {
                    return mid;
                }
            }

        }

        return -1;
    }
}