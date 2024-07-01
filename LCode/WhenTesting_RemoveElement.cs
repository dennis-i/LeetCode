namespace LCode;

public class WhenTesting_RemoveElement
{
    [Theory]
    [InlineData(new[] { 2, 2 }, new[] { 3, 2, 2, 3 }, 3)]
    [InlineData(new[] { 2, 2 }, new[] { 3, 3, 2, 2, 3 }, 3)]
    [InlineData(new[] { 0, 1, 4, 0, 3 }, new[] { 0, 1, 2, 2, 3, 0, 4, 2 }, 2)]
    [InlineData(new[] { 0 }, new[] { 0 }, 2)]
    [InlineData(new int[0], new[] { 0 }, 0)]
    [InlineData(new int[0], new int[0], 2)]

    public void TestIt(int[] expected, int[] nums, int val)
    {
        int l = RemoveElement(nums, val);
        Assert.Equal(expected.Length, l);
        for (int i = 0; i < l; i++)
            Assert.Equal(expected[i], nums[i]);

    }


    public int RemoveElement(int[] nums, int val)
    {


        for (int i = 0; i < nums.Length; ++i)
        {
            if (nums[i] == val)
            {
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    if (nums[j] != val)
                        (nums[i], nums[j]) = (nums[j], nums[i]);
                }
            }
        }

        for (int i = 0; i < nums.Length; ++i)
        {
            if (nums[i] == val) return i;
        }

        return nums.Length;
    }
}