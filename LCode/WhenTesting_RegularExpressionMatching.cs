namespace LCode;

//TODO
public class WhenTesting_RegularExpressionMatching
{
    [Theory]
    [InlineData(false, "aa", "a")]
    [InlineData(true, "aa", "a*")]
    [InlineData(true, "ab", ".*")]
    [InlineData(true, "abbbz", ".*")]
    [InlineData(false, "abcd", "ab*")]
    [InlineData(true, "aab", "c*a*b")]
    public void TestIt(bool expected, string s, string p)
    {
        Assert.Equal(expected, IsMatch(s, p));
    }

    public bool IsMatch(string s, string p)
    {
        var span = s.AsSpan();
        var pspan = p.AsSpan();
        var l = new List<char>();
        int j = 0;
        bool wildStarted = false;
        for (int i = 0; i < span.Length; ++i)
        {
            if (j == pspan.Length)
                return false;

            var ch = span[i];
            var pch = pspan[j];

            if (wildStarted)
            {
                l.Add(l[^1]);
            }
            else if (ch == pch )
            {
                l.Add(ch);
                j++;
            }

            else if (pch == '.')
            {
                l.Add(ch);
                j++;
            }
               

            else if (pch == '*')
            {
                wildStarted=true;
            }
                

           
        }
        return true;
    }

}