using System.Diagnostics;

namespace LCode;

//TODO
public class WhenTesting_RegularExpressionMatching
{
    [Theory]
    [InlineData(false, "aa", "a")]
    [InlineData(false, "aa", "aaa")]
    [InlineData(true, "abc", "abc")]
    [InlineData(true, "abc", ".bc")]
    [InlineData(true, "aa", "a*")]
    [InlineData(true, "ab", ".*")]
    [InlineData(true, "abbbz", ".*")]
    [InlineData(false, "abcd", "ab*")]
    [InlineData(true, "aab", "c*a*b")]
    [InlineData(true, "a", "b*a")]
    [InlineData(true, "a", "a")]
    [InlineData(false, "b", "a")]
    [InlineData(true, "a", ".")]
    [InlineData(false, "ab", ".*c")]
    [InlineData(true, "aaa", "ab*a*c*a")]
    [InlineData(false, "aaa", "aaaa")]
    [InlineData(false, "aaba", "ab*a*c*a")]
    [InlineData(true, "aaca", "ab*a*c*a")]
    public void TestIt(bool expected, string s, string p)
    {
        Assert.Equal(expected, IsMatch(s, p));
    }



    public bool IsMatch(string s, string p)
    {
        var patternSpan = p.AsSpan();
        var strSpan = s.AsSpan();



        bool match = false;
        char previous = '\0';


        var canReduce = new Dictionary<char, int>();


        while (!strSpan.IsEmpty)
        {



            var sch = strSpan[0];
            var pch = patternSpan[0];


            if (!canReduce.ContainsKey(sch))
                canReduce.Add(sch, 0);

            if (pch == '*')
            {
                if (previous != '\0' && previous != '.')
                    canReduce[previous]++;
                pch = previous;
            }

            if (pch == '.')
                pch = sch;


            if (pch == sch)
            {
                match = true;
                strSpan = strSpan.Slice(1);

                if (!(patternSpan.Length == 1 && patternSpan[0] == '*'))
                    if (!patternSpan.IsEmpty)
                    {
                        previous = patternSpan[0];
                        patternSpan = patternSpan.Slice(1);
                    }

            }
            else if (patternSpan.Length > 1 && patternSpan[0] == '*' && patternSpan[1] == sch)
            {
                match = true;
                strSpan = strSpan.Slice(1);
                canReduce[previous]--;
            }

            else if (patternSpan.Length > 1 && patternSpan[1] == '*')
            {
                previous = '\0';
                patternSpan = patternSpan.Slice(2);
            }
            else
            {
                strSpan = strSpan.Slice(1);
                previous = patternSpan[0];
                if (!patternSpan.IsEmpty)
                    patternSpan = patternSpan.Slice(1);

                return false;
            }


            if (patternSpan.IsEmpty && !strSpan.IsEmpty)
                return false;

            while (strSpan.IsEmpty && patternSpan.Length > 1 && patternSpan[1] == '*')
            {
                patternSpan = patternSpan.Slice(2);
            }

            while (strSpan.IsEmpty && !patternSpan.IsEmpty)
            {
                char k = patternSpan[0];
                if (k == '*')
                {
                    patternSpan = patternSpan.Slice(1);
                    continue;
                }
                if (!canReduce.TryGetValue(k, out var n))
                    return false;

                bool ok = n > 0;
                while (n > 0 && ok)
                {
                    if (patternSpan.IsEmpty)
                        return ok;

                    ok = patternSpan[0] == k;
                    patternSpan = patternSpan.Slice(1);

                    --n;
                }
                return ok;


            }

            if (strSpan.IsEmpty && !patternSpan.IsEmpty)
                return false;

        }



        return match;
    }

}