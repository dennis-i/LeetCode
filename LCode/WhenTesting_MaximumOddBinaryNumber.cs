namespace LCode;

public class WhenTesting_MaximumOddBinaryNumber
{

    [Theory]
    [InlineData("001", "010")]
    [InlineData("1001", "0101")]
    [InlineData("001", "100")]
    public void TestIt(string exepected, string s)
    {
        Assert.Equal(exepected, MaximumOddBinaryNumber(s));
    }


    public string MaximumOddBinaryNumber(string s)
    {
        var span = s.AsSpan().ToArray().AsSpan();
        if (span.Length == 1)
            return span.ToString();
        int oneIdx = span.Length - 2;
        while (span[^1] == '0')
        {
            if (span[oneIdx] == '1')
                (span[oneIdx], span[^1]) = (span[^1], span[oneIdx]);
            --oneIdx;
        }


        int l = 0;
        int r = span.Length - 2;
        while (l < r)
        {
            if (span[l] == '1')
                ++l;
            else if (span[r] == '0')
                --r;
            else
            {
                (span[l], span[r]) = (span[r], span[l]);
                ++l;
                --r;
            }
        }
        return span.ToString();
    }
}