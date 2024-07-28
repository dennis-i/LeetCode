namespace LCode;

public class WhenTesting_FindFirstAndLastPositionOfElementInSortedArray
{

    [Theory]
    [InlineData(new[] { 3, 4 }, new[] { 5, 7, 7, 8, 8, 10 }, 8)]
    [InlineData(new[] { -1, -1 }, new[] { 5, 7, 7, 8, 8, 10 }, 6)]
    [InlineData(new[] { 0, 0 }, new[] { 1 }, 1)]
    [InlineData(new[] { -1, -1 }, new int[0], 0)]
    public void TestIt(int[] exepected, int[] nums, int target)
    {
        Assert.Equal(exepected, SearchRange(nums, target));
    }

    public int[] SearchRange(int[] nums, int target)
    {

        int[] result = [-1, -1];

        int low = 0;
        int hight = nums.Length - 1;

        while (low <= hight)
        {
            int mid = low + ((hight - low) >> 1);

            if (nums[mid] < target)
                low = mid + 1;
            else if (nums[mid] > target)
                hight = mid - 1;
            else
            {
                int midl = mid;
                int midr = mid;
                while (midl >= 0)
                {
                    if (nums[midl] != target)
                        break;
                    midl--;
                }
                while (midr < nums.Length)
                {
                    if (nums[midr] != target)
                        break;
                    midr++;
                }
                result[0] = midl + 1;
                result[1] = midr - 1;
                break;
            }
        }
        return result;
    }
}