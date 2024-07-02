namespace LCode;

public class WhenTesting_LongestPalindromicSubstring
{


    [Theory]
    [InlineData("aba", "babad")]
    [InlineData("bb", "cbbd")]
    [InlineData("abcdedcba", "abcdedcba")]
    [InlineData("1234567654321", "abcdedcba1234567654321")]
    [InlineData("bb", "casdbb")]
    [InlineData("a", "a")]
    [InlineData("a", "ab")]
    public void TestIt(string expected, string s)
    {
        Assert.Equal(expected, LongestPalindrome(s));
    }



    //abcdedcba1234567654321
    //         1234567654321abcdedcba
    public string LongestPalindrome(string s)
    {
        if (s.Length == 1) return s;

        var span = s.AsSpan();

        Dictionary<int, string> map = new();
        var centers = PaliCenters(span);

        foreach (var center in centers)
        {

            int lidx = center.Item1 - 1;
            int ridx = center.Item2 + 1;
            while (lidx >= 0 && ridx < span.Length)
            {
                if (span[lidx] != span[ridx])
                    break;

                lidx--;
                ridx++;

            }

            lidx++;

            var p = span.Slice(lidx, ridx - lidx);
            map[p.Length] = new string(p);

        }

        if (map.Count == 0)
            return new string(span.Slice(0,1));

        return map[map.Keys.Max()];
    }

    private IReadOnlyList<(int, int)> PaliCenters(ReadOnlySpan<char> s)
    {
        var centers = new List<(int, int)>();
        for (int i = 0; i < s.Length - 1; i++)
        {
            //xyx
            if (i < s.Length - 2 && s[i] == s[i + 2])
                centers.Add((i + 1, i + 1));
            //xx
            if (s[i] == s[i + 1])
                centers.Add((i, i + 1));
        }
        return centers;
    }






}
