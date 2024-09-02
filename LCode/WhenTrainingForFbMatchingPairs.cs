namespace LCode;

public class WhenTrainingForFbMatchingPairs
{

    [Theory]
    [InlineData(4, "abcd", "adcb")]
    [InlineData(0, "abcd", "efhg")]
    [InlineData(1, "mno", "mno")]
    [InlineData(63, "mnasdasfdasfasfasfasfasfasfasfasfasfasfasfrgdfhfyjfghfgjfgjfgoj", "mnasdasfdasfasfasfasfasfasfasfasfasfasfasfrgdfhfyjfghfgjfgjfgjo")]
    public void TestIt(int expected, string s, string t)
    {
        Assert.Equal(expected, matchingPairs(s, t));
    }

    private static int matchingPairs(string s, string t)
    {




        int SwapNCnt(string s1, string t2, int idx1, int idx2, Dictionary<(int, int), int> memo)
        {

            if (idx1 == idx2)
                return 0;


            var key = (idx1, idx2);
            if (memo.ContainsKey(key))
                return memo[key];


            int n1 = SwapNCnt(s1, t2, idx1 + 1, idx2, memo);
            int n2 = SwapNCnt(s1, t2, idx1, idx2 - 1, memo);

            var ch = s1.ToCharArray();
            (ch[idx1], ch[idx2]) = (ch[idx2], ch[idx1]);

            int sum = 0;
            for (int i = 0; i < s1.Length; ++i)
            {
                if (ch[i] == t2[i])
                    ++sum;
            }

            var result = Math.Max(sum, Math.Max(n1, n2));
            memo.Add(key, result);
            return result;
        }

        int res = SwapNCnt(s, t, 0, s.Length - 1, new Dictionary<(int, int), int>());
        return res;
    }
}