using System.Text;

namespace LCode;

public class WhenTesting_SubstringWithConcatenationOfAllWords
{

    [Theory]
    [InlineData(new[] { 0, 9 }, "barfoothefoobarman", new[] { "foo", "bar" })]
    [InlineData(new int[0], "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "word" })]
    [InlineData(new int[] { 6, 9, 12 }, "barfoofoobarthefoobarman", new[] { "bar", "foo", "the" })]
    [InlineData(new int[] { 8 }, "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "good" })]
    [InlineData(new int[] { 13 }, "lingmindraboofooowingdingbarrwingmonkeypoundcake", new[] { "fooo", "barr", "wing", "ding", "wing" })]
    public void TestIt(int[] expected, string s, string[] words)
    {
        var res = FindSubstring(s, words);
        Assert.Equal(expected.Length, res.Count);
        for (int i = 0; i < expected.Length; i++)
            Assert.Contains(expected[i], res);

    }


    [Theory]
    [InlineData(new[] { "foobar", "barfoo" }, new[] { "foo", "bar" })]
    [InlineData(new[] { "abcdef", "abefcd", "cdabef", "cdefab", "efabcd", "efcdab" }, new[] { "ab", "cd", "ef" })]
    public void TestConcats(string[] expected, string[] words)
    {
        Assert.Equal(expected, ConcantinatedWords(words));
    }


    [Theory]

    [InlineData("fooowingdingbarrwing", new[] { "fooo", "barr", "wing", "ding", "wing" })]
    public void AfterConcatContains(string expected, string[] words)
    {
        Assert.Contains(expected, ConcantinatedWords(words));
    }



    [Theory]
    [InlineData(new[] { "a" }, new[] { "a", "b" }, 1)]
    [InlineData(new[] { "a", "b", "d" }, new[] { "a", "b", "c", "d" }, 2)]

    public void TestRemoveIdx(string[] expected, string[] words, int idx)
    {
        Assert.Equal(expected, ExcludeIndex(words, idx).ToArray());
    }


    [Theory]
    [InlineData(new[] { "b", "a" }, new[] { "a", "b" })]
    [InlineData(new[] { "b", "c", "d", "a" }, new[] { "a", "b", "c", "d" })]

    public void TestRotateOne(string[] expected, string[] words)
    {
        Assert.Equal(expected, RotateLeftOne(words).ToArray());
    }


    private IReadOnlyList<string> ConcantinatedWords(string[] words)
    {

        string Combine(string s, ReadOnlySpan<string> toAdd, bool reverce = false)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append(s);

            for (int i = 0; i < toAdd.Length; ++i)
            {
                if (reverce)
                    sb.Append(toAdd[^(i + 1)]);
                else
                    sb.Append(toAdd[i]);
            }
            return sb.ToString();
        }

        var hash = new HashSet<string>();

        for (int i = 0; i < words.Length; ++i)
        {
            string head = words[i];

            var excl = ExcludeIndex(words, i);
            for (int j = 0; j < excl.Length; ++j)
            {
                var str = Combine(head, excl, false);
                hash.Add(str);
                str = Combine(head, excl, true);
                hash.Add(str);
                excl = RotateLeftOne(excl);
            }
        }



        return hash.ToArray();
    }


    private ReadOnlySpan<string> RotateLeftOne(ReadOnlySpan<string> src)
    {
        string[] res = new string[src.Length];
        string tmp = src[0];
        src.Slice(1).CopyTo(res);
        res[^1] = tmp;
        return res;
    }

    private ReadOnlySpan<string> ExcludeIndex(ReadOnlySpan<string> src, int index)
    {
        var res = new string[src.Length - 1];
        src.Slice(0, index).CopyTo(res);
        src.Slice(index + 1).CopyTo(res.AsSpan(index));
        return res;

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
                tmps = s.Slice(singleWordLen * cnt);
                res = -1;
                tmpptrn = pattern;
                idx = s.Length - tmps.Length;
                continue;

            }
            tmps = tmps.Slice(1);
            idx++;
        }

        return tmpptrn.IsEmpty ? res : -1;
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