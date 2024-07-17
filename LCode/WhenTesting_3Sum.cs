namespace LCode;


using Tripples = IList<IList<int>>;

public class WhenTesting_3Sum
{

    [Theory]
    [InlineData(new[] { "-1, -1, 2", "-1, 0, 1" }, new[] { -1, 0, 1, 2, -1, -4 })]
    [InlineData(new string[0], new[] { 0, 1, 1 })]
    [InlineData(new[] { "0,0,0" }, new[] { 0, 0, 0 })]
    [InlineData(new[] { "-2,0,2", "-2,1,1" }, new[] { -2, 0, 1, 1, 2 })]
    public void TestIt(string[] expectedStrs, int[] nums)
    {
        Tripples expected = ToArrays(expectedStrs);


        var res = ThreeSum(nums);
        Assert.Equal(expected.Count, res.Count);
        for (int i = 0; i < expected.Count; i++)
        {
            Assert.Contains(expected[i], res);
        }

    }

    private Tripples ToArrays(string[] expectedStrs)
    {
        var result = new int[expectedStrs.Length][];

        for (int i = 0; i < expectedStrs.Length; ++i)
        {
            var s = expectedStrs[i].Split(',', StringSplitOptions.RemoveEmptyEntries);
            result[i] = new int[s.Length];
            for (int j = 0; j < s.Length; ++j)
            {
                result[i][j] = int.Parse(s[j]);
            }

        }

        return result;
    }


    public IList<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);

        var hash = new HashSet<ValueTuple<int, int, int>>();
        for (int i = 0; i < nums.Length - 2; ++i)
        {
            if (nums[i] > 0)
                break;

            int p1 = i + 1;
            int p2 = nums.Length - 1;

            while (p1 < p2)
            {
                int n = nums[i] + nums[p1] + nums[p2];
                if (n < 0)
                    ++p1;
                else if (n > 0)
                    --p2;
                else
                {
                    hash.Add((nums[i], nums[p1], nums[p2]));
                    ++p1;
                }
            }
        }
        var result = new List<IList<int>>();
        foreach (var item in hash)
        {
            result.Add(new[] { item.Item1, item.Item2, item.Item3 });
        }
        return result;
    }

}