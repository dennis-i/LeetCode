namespace LCode;

public class WhenTesting_LetterCombinationsOfPhoneNumber
{

    [Theory]
    [InlineData(new string[0], "")]
    [InlineData(new[] { "a", "b", "c" }, "2")]
    [InlineData(new[] { "a", "b", "c" }, "2345")]
    [InlineData(new[] { "ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf" }, "23")]
    public void TestIt(string[] expected, string digits)
    {
        Assert.Equal(expected, LetterCombinations(digits));
    }

    public IList<string> LetterCombinations(string digits)
    {
        var phoneKeyMap = new Dictionary<char, char[]>()
        {
            { '2', ['a', 'b', 'c'] },
            { '3', ['d', 'e', 'f'] },
            { '4', ['g', 'h', 'i'] },
            { '5', ['j', 'k', 'l'] },
            { '6', ['m', 'n', 'o'] },
            { '7', ['p', 'q', 'r', 's'] },
            { '8', ['t', 'u', 'v'] },
            { '9', ['w', 'x', 'y', 'z'] },
        };

        var span = digits.AsSpan();
        HashSet<string> res = new();

        for (int i = 0; i < span.Length; i++)
        {
            if (phoneKeyMap.ContainsKey(span[i]))
            {
                res = i == 0 ? Combine(phoneKeyMap[span[i]]) : Combine(res, phoneKeyMap[span[i]]);
            }
        }
        return res.ToList();
    }

    HashSet<string> Combine(ReadOnlySpan<char> a)
    {
        var result = new HashSet<string>();
        for (int i = 0; i < a.Length; ++i)
        {
            result.Add($"{a[i]}");
        }
        return result;
    }

    HashSet<string> Combine(HashSet<string> a, ReadOnlySpan<char> b)
    {
        var result = new HashSet<string>();
        foreach (var elem in a)
        {
            for (int j = 0; j < b.Length; ++j)
            {
                result.Add($"{elem}{b[j]}");
            }
        }
        return result;
    }

}