using System.Diagnostics;
using System.Text;

namespace LCode;

public class WhenTesting_SubstringWithConcatenationOfAllWords
{

    [Theory]
    [InlineData(new[] { 0, 9 }, "barfoothefoobarman", new[] { "foo", "bar" })]
    [InlineData(new[] { 0, 3, 6 }, "foobarfoobar", new[] { "foo", "bar" })]
    [InlineData(new int[0], "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "word" })]
    [InlineData(new int[] { 6, 9, 12 }, "barfoofoobarthefoobarman", new[] { "bar", "foo", "the" })]
    [InlineData(new int[] { 8 }, "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "good" })]
    [InlineData(new int[] { 13 }, "lingmindraboofooowingdingbarrwingmonkeypoundcake", new[] { "fooo", "barr", "wing", "ding", "wing" })]
    [InlineData(new int[] { 1, 3 }, "abaababbaba", new[] { "ab", "ba", "ab", "ba" })]
    [InlineData(new int[] { 1, 3 }, "abaababbaba", new[] { "dhvf", "sind", "ffsl", "yekr", "zwzq", "kpeo", "cila", "tfty", "modg", "ztjg", "ybty", "heqg", "cpwo", "gdcj", "lnle", "sefg", "vimw", "bxcb" })]
    public void TestIt(int[] expected, string s, string[] words)
    {
        var res = FindSubstring(s, words);
        Assert.Equal(expected.Length, res.Count);
        for (int i = 0; i < expected.Length; i++)
            Assert.Contains(expected[i], res);

    }



    [Theory]
    [InlineData(new[] { "12", "21" }, new[] { 1, 2 })]
    [InlineData(new[] { "12", "21" }, new[] { 1, 2, 3 })]
    [InlineData(new[] { "12", "21" }, new[] { 1, 2, 3, 4 })]
    [InlineData(new[] { "12", "21" }, new[] { 1, 2, 3, 4, 5 })]
    public void FindPermutations(string[] expected, int[] nums)
    {


        int Factotrial(int n)
        {
            int sum = 1;
            for (int i = 1; i <= n; ++i)
                sum *= i;
            return sum;
        }

        int f = Factotrial(nums.Length);
        var tbl = FindPerm(nums);
        Assert.Equal(f, tbl.Count);

        var sb = new StringBuilder();
        foreach (var row in tbl)
        {
            foreach (var r in row)
            {
                sb.AppendFormat("{0} ", r);
            }

            sb.AppendLine();
        }

        string dump = sb.ToString();
    }



    private List<List<int>> FindPerm(int[] nums)
    {
        List<List<int>> result = new();
        BackTrack(result, nums, 0, nums.Length);
        return result;
    }
    private void BackTrack(List<List<int>> result, int[] array, int start, int end)
    {
        if (start == end)
        {
            result.Add([.. array]);
        }
        else
        {
            for (int i = start; i < end; i++)
            {
                (array[start], array[i]) = (array[i], array[start]);
                BackTrack(result, array, start + 1, end);
                (array[start], array[i]) = (array[i], array[start]);
            }
        }

    }

    private IReadOnlyList<string> ConcantinatedWords(string[] words)
    {
        var hash = new HashSet<string>();
        int[] idxs = new int[words.Length];
        for (int i = 0; i < idxs.Length; ++i)
            idxs[i] = i;

        var permIdx = FindPerm(idxs);
        foreach (var item in permIdx)
        {
            var sb = new StringBuilder();
            foreach (var idx in item)
            {
                sb.Append(words[idx]);
            }
            hash.Add(sb.ToString());
        }
        return hash.ToArray();
    }

    private int FindMatch(Span<char> s, Span<char> pattern, int singleWordLen, int fromIdx = 0)
    {
        if (s.Length <= fromIdx)
            return -1;


        s = s.Slice(fromIdx);

        if (s.Length < pattern.Length)
            return -1;

        var tmpptrn = pattern;
        var tmps = s;

        int res = -1;
        int idx = 0;
        int cnt = 0;
        while (!tmpptrn.IsEmpty && !tmps.IsEmpty)
        {
            if (tmps[0] == tmpptrn[0])
            {
                if (res == -1)
                    res = idx;
                tmpptrn = tmpptrn.Slice(1);
            }
            else if (res != -1)
            {
                cnt++;
                tmps = s.Slice(/*singleWordLen **/ cnt);
                res = -1;
                tmpptrn = pattern;
                idx = s.Length - tmps.Length;
                continue;

            }
            tmps = tmps.Slice(1);
            idx++;
        }

        return tmpptrn.IsEmpty ? res + fromIdx : -1;
    }

    public IList<int> FindSubstring(string s, string[] words)
    {
        var permutations = ConcantinatedWords(words);
        var res = new List<int>();

        foreach (var permutation in permutations)
        {
            
            var idx = FindMatch(s.ToCharArray(), permutation.ToCharArray(), words[0].Length);

            while (idx != -1)
            {
                res.Add(idx);
                idx = FindMatch(s.ToCharArray(), permutation.ToCharArray(), words[0].Length, idx + 1);
            }
        }

        return res;
    }
}