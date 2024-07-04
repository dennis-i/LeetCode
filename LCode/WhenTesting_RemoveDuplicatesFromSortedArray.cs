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


    [Theory]
    [InlineData(new[] { 1, 0 }, new[] { 0, 1 }, 0)]
    [InlineData(new[] { 0, 1, 2, 3, 4, 5 }, new[] { 0, 5, 1, 2, 3, 4 }, 1)]
    public void TestMoveToEnd(int[] expected, int[] nums, int fromIdx)
    {
        //MovedBehindBigger(fromIdx, nums);
        Assert.Equal(expected, nums);
    }


    public int RemoveDuplicates(int[] nums)
    {
        if (nums.Length == 1) return 1;

        int max = int.MinValue;

        for (int i = 1; i < nums.Length; ++i)
        {
            int n1 = nums[i - 1];
            int n2 = nums[i];

            if (n2 > max)
                max = n2;

            if (n1 >= n2)
            {
                if (!MovedBehindBigger(i, nums))
                    break;
                --i;
            }
        }

        for (int i = 0; i < nums.Length; ++i)
        {
            if (nums[i] == max)
                return i + 1;
        }

        return 0;
    }

    public bool MovedBehindBigger(int fromIndex, int[] array)
    {
        int idx = fromIndex;
        while (idx + 1 < array.Length)
        {
            (array[idx + 1], array[idx]) = (array[idx], array[idx + 1]);
            if (array[idx + 1] < array[idx])
                return true;
            ++idx;
        }

        return false;
    }

}