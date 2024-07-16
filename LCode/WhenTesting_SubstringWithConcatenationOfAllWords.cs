namespace LCode;

public class WhenTesting_SubstringWithConcatenationOfAllWords
{

    [Theory]
    [InlineData(new[] { 0, 9 }, "barfoothefoobarman", new[] { "foo", "bar" })]
    [InlineData(new[] { 0, 3, 6 }, "foobarfoobar", new[] { "foo", "bar" })]
    [InlineData(new int[0], "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "word" })]
    [InlineData(new[] { 6, 9, 12 }, "barfoofoobarthefoobarman", new[] { "bar", "foo", "the" })]
    [InlineData(new[] { 8 }, "wordgoodgoodgoodbestword", new[] { "word", "good", "best", "good" })]
    [InlineData(new[] { 13 }, "lingmindraboofooowingdingbarrwingmonkeypoundcake", new[] { "fooo", "barr", "wing", "ding", "wing" })]
    [InlineData(new[] { 1, 3 }, "abaababbaba", new[] { "ab", "ba", "ab", "ba" })]
    [InlineData(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, "aaaaaaaaaaaaaa", new[] { "aa", "aa" })]


    public void TestIt(int[] expected, string s, string[] words)
    {
        var res = FindSubstring(s, words);
        Assert.Equal(expected.Length, res.Count);
        for (int i = 0; i < expected.Length; i++)
            Assert.Contains(expected[i], res);

    }

    [Fact]
    public void TestFromFileData()
    {
        const string dataFile = @"SubstringWithConcatenationOfAllWords.txt";
        var lines = File.ReadAllLines(dataFile);

        string s = String.Empty;
        string[] words = [];

        foreach (var line in lines)
        {
            if (line.Contains("[String]"))
            {
                s = line.Replace("[String]", "");
            }
            else if (line.Contains("[Words]"))
            {
                words = line.Replace("[Words]", "")
                  .Replace("[", "")
                  .Replace("]", "").Split(',', StringSplitOptions.RemoveEmptyEntries);
            }
        }
        var res = FindSubstring(s, words);

    }



    private int FindSubstringInternal(in string s, in Dictionary<string, int> testMap, in int wordLen, in int startIdx)
    {

        var map = new Dictionary<string, int>(testMap);

        bool firstFound = false;
        int result = -1;
        int chIdx = 0;
        var span = s.AsSpan(startIdx);
        var span2 = s.AsSpan(startIdx);


        while (span2.Length >= wordLen)
        {

            var key = new string(span2.Slice(0, wordLen));
            if (map.ContainsKey(key) && map[key] > 0)
            {

                if (!firstFound)
                {
                    firstFound = true;
                    result = span.Length - span2.Length + startIdx;
                }

                span2 = span2.Slice(wordLen);

                map[key]--;

                if (IsMatchFound())
                    break;
            }
            else
            {
                chIdx++;
                if (firstFound)
                {
                    map = testMap.ToDictionary();
                    span2 = s.AsSpan(startIdx + chIdx);
                }
                else
                {
                    span2 = span2.Slice(1);
                }
                firstFound = false;
            }
        }

        return IsMatchFound() ? result : -1;


        bool IsMatchFound()
        {
            foreach (var n in map)
            {
                if (n.Value > 0)
                    return false;
            }
            return true;
        }
    }
    public IList<int> FindSubstring(string s, string[] words)
    {
        Dictionary<string, int> CreateMap()
        {
            var map = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (!map.ContainsKey(word))
                    map.Add(word, 0);

                map[word]++;
            }

            return map;
        }



        var testMap = CreateMap();

        var res = new List<int>(s.Length);

        int wordLen = words[0].Length;


        int idx = FindSubstringInternal(s, testMap, wordLen, 0);

        while (idx != -1)
        {
            res.Add(idx);
            idx = FindSubstringInternal(s, testMap, wordLen, idx + 1);
        }


        return res;
    }

}