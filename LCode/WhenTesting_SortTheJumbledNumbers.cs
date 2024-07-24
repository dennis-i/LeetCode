namespace LCode;

public class WhenTesting_SortTheJumbledNumbers
{

    [Theory]
    [InlineData(new[] { 338, 38, 991 }, new[] { 8, 9, 4, 0, 2, 1, 3, 5, 7, 6 }, new[] { 991, 338, 38 })]
    [InlineData(new[] { 123, 456, 789 }, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }, new[] { 789, 456, 123 })]
    [InlineData(new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 }, new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 })]
    public void TestIt(int[] expected, int[] mapping, int[] nums)
    {
        Assert.Equal(expected, SortJumbled(mapping, nums));
    }

    public int[] SortJumbled(int[] mapping, int[] nums)
    {

        int map(ReadOnlySpan<int> tbl, int num)
        {
            if (num < 10)
                return tbl[num];
            int res = 0;
            int div = 10;
            while (num > 0)
            {
                int dig = num % 10;

                dig = tbl[dig];
                res += dig * (div / 10);

                num /= 10;
                div *= 10;
            }

            return res;
        }


      
        var tmp = new ValueTuple<int, int>[nums.Length];

        for (int i = 0; i < nums.Length; ++i)
        {
            tmp[i] = (nums[i], map(mapping, nums[i]));
        }

        return tmp.OrderBy(x => x.Item2).Select(x => x.Item1).ToArray();

    }
}