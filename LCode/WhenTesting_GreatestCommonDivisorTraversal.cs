namespace LCode;

//TODO
public class WhenTesting_GreatestCommonDivisorTraversal
{
    [Theory]
    [InlineData(true, new[] { 2, 3, 6 })]
    [InlineData(false, new[] { 3, 9, 5 })]
    [InlineData(true, new[] { 4, 3, 12, 8 })]
    [InlineData(false, new[] { 10, 40, 30, 30, 42, 13, 35, 15, 13, 30, 33, 30, 30, 35, 42, 42, 42, 28, 15, 4, 35, 44, 21, 42, 35, 15, 22, 10, 5, 30, 42 })]
    public void TestIt(bool expected, int[] nums)
    {
        Assert.Equal(expected, CanTraverseAllPairs(nums));
    }




    [Theory]
    [InlineData(3, 3, 3)]
    [InlineData(2, 2, 4)]
    [InlineData(2, 8, 6)]
    [InlineData(5, 10, 15)]
    [InlineData(50, 100, 150)]
    [InlineData(1, 7, 4)]
    public void TestGcd(int expected, int a, int b)
    {
        Assert.Equal(expected, Gcd(a, b));
    }



    [Fact]
    public void TestIdxPairs()
    {

        var expected = new[] { (0, 1) };
        var arr = new[] { 1, 2 };
        Assert.Equal(expected, IndexPairs(arr));

        expected = new[] { (0, 1), (0, 2), (1, 2) };
        arr = new[] { 1, 2, 3 };
        Assert.Equal(expected, IndexPairs(arr));
    }

    public bool CanTraverseAllPairs(int[] nums)
    {
        if (nums.Length < 2) return false;

        bool[] visited = new bool[nums.Length];

        var pairs = IndexPairs(nums);


        foreach (var pair in pairs)
        {
            if (1 != Gcd(nums[pair.Item1], nums[pair.Item2]))
                visited[pair.Item1] = visited[pair.Item2] = true;
        }
        return visited.All(x => x);
    }


    private static IReadOnlyList<ValueTuple<int, int>> IndexPairs(int[] nums)
    {

        var l = new List<ValueTuple<int, int>>(nums.Length * nums.Length);
        for (int i = 0; i < nums.Length; ++i)
        {
            for (int j = i + 1; j < nums.Length; ++j)
            {
                l.Add((i, j));
            }
        }
        return l;
    }

    private static int Gcd(int a, int b)
    {
        if (a == b) return a;

        int large = Math.Max(a, b);
        int small = Math.Min(a, b);
        if (large % small == 0) return small;


        int m = small >> 1;
        
        while (m > 1)
        {

            if (large % m == 0 && small % m == 0)
                return m;
            --m;
        }

        return 1;
    }

}