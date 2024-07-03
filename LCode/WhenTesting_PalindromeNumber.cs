namespace LCode;

public class WhenTesting_PalindromeNumber
{

    [Theory]
    [InlineData(true, 121)]
    [InlineData(true, 0)]
    [InlineData(false, -121)]
    [InlineData(false, 10)]
    [InlineData(true, 1001)]

    public void TestIt(bool expected, int number)
    {
        Assert.Equal(expected, IsPalindrome(number));
    }

    public bool IsPalindrome(int x)
    {
        if (x < 0)
            return false;

        var l = new List<int>(10);
        int n = x;
        while (n > 0)
        {
            int t = n % 10;
            l.Add(t);
            n /= 10;
        }

        if (l.Count > 1 && l[0] == 0)
            return false;

        for (int i = 0; i < l.Count >> 1; ++i)
        {
            int left = l[i];
            int right = l[^(i + 1)];
            if (left == right)
                continue;

            return false;
        }
        return true;
    }
}