namespace LCode;

public class WhenTesting_ReverceInteger
{
    [Theory]
    [InlineData(321, 123)]
    [InlineData(-321, -123)]
    [InlineData(21, 120)]
    [InlineData(0, int.MaxValue)]
    [InlineData(0, 1534236469)]
    public void TestIt(int expected, int x)
    {
        Assert.Equal(expected, Reverse(x));
    }
    public int Reverse(int x)
    {
        if (x == 0)
            return 0;

        int n = x;
        var l = new List<int>();
        int powCnt = 0;
        while (n != 0)
        {
            int t = n % 10;
            n /= 10;
            l.Add(t);
            powCnt++;
        }

        int res = 0;
        int mult = (int)Math.Pow(10, powCnt - 1);
        for (int i = 0; i < l.Count; ++i)
        {
            int m = l[i] * mult;
            if (m / mult != l[i])
                return 0;
            res += m;
            mult /= 10;
        }

        return res;
    }

}