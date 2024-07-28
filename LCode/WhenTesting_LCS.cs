using System.Diagnostics;

namespace LCode;

public class WhenTesting_LCS
{
    [Theory]
    [InlineData("first hello the world", " world the word fist and fit")]
    public void TestIt(string a, string b)
    {

        int n = Lcs(a, 0, b, 0);
        Debug.WriteLine("lcs = {0}", n);
    }


    private int Lcs(string a, int idxa, string b, int idxb) => Lcs(a, idxa, b, idxb, new Dictionary<(int, int), int>());

    private int Lcs(string a, int idxa, string b, int idxb, Dictionary<ValueTuple<int, int>, int> memo)
    {
        if (idxa == a.Length || idxb == b.Length)
            return 0;

        if (memo.ContainsKey((idxa, idxb)))
            return memo[(idxa, idxb)];

        int n1 = Lcs(a, idxa + 1, b, idxb, memo);
        int n2 = Lcs(a, idxa, b, idxb + 1, memo);

        int res = (a[idxa] == b[idxb]) ? 1 : 0;



        res = Math.Max(n1, n2) + res;
        memo.Add((idxa, idxb), res);
        return res;
    }
}