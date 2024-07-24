namespace LCode;

public class WhenTesting_Minimum_LengthOfStringAfterDeletingSimilarEnds
{
    [Theory]
    [InlineData(2, "ca")]
    [InlineData(0, "cabaabac")]
    [InlineData(3, "aabccabba")]
    [InlineData(0, "abbbbbbbbbbbbbbbbbbba")]
    [InlineData(1, "bbbbbbbbbbbbbbbbbbbbbbbbbbbabbbbbbbbbbbbbbbccbcbcbccbbabbb")]
    public void TestIt(int expected, string s)
    {
        Assert.Equal(expected, MinimumLength(s));
    }

    public int MinimumLength(string s)
    {
        var span = s.AsSpan();

        int l = 0;
        int r = span.Length - 1;

        char c = '\0';
        while (l < r)
        {
            if (s[l] == s[r])
            {
                c = s[l];
                l++;
                r--;
                span = span.Length > 1 ? span.Slice(1, span.Length - 2) : span.Slice(1);
            }
            else if (s[r] == c)
            {
                r--;
                span = span.Slice(0, span.Length - 1);
            }
            else if (s[l] == c)
            {
                l++;
                span = span.Slice(1);
            }
            else
                break;
        }
        return span.Length;
    }
}