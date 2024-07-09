namespace LCode;

public class WhenTesting_LetterCasePermutation
{
    [Theory]
    [InlineData(new[] { "a1b2", "a1B2", "A1b2", "A1B2" }, "a1b2")]
    [InlineData(new[] { "3z4", "3Z4" }, "3z4")]
    [InlineData(new[] { "mqe", "mqE", "mQe", "mQE", "Mqe", "MqE", "MQe", "MQE" }, "mQe")]
    public void TestIt(string[] expected, string s)
    {
        var perm = LetterCasePermutation(s);

        Assert.Equal(expected.Length, perm.Count);
        for (int i = 0; i < expected.Length; i++)
        {
            Assert.Contains(expected[i], perm);
        }

    }

    public IList<string> LetterCasePermutation(string s)
    {

        char SetCase(char ch, bool upper) => upper ? char.ToUpper(ch) : char.ToLower(ch);

        var span = s.AsSpan();

        var bitMapper = new Dictionary<int, int>();
        int cnt = 0;
        for (int i = 0; i < span.Length; ++i)
        {
            if (char.IsLetter(span[i]))
                bitMapper[cnt++] = i;
        }
        if (bitMapper.Count == 0)
            return [s];

        var result = new List<string>();
        int numPerm = (int)Math.Pow(2, bitMapper.Count);
        for (int i = 0; i < numPerm; ++i)
        {

            char[] arr = new char[span.Length];
            span.CopyTo(arr);
            foreach (var element in bitMapper)
            {
                int bitIdx = element.Key;
                int chIdx = element.Value;

                bool isUpper = (i & (1 << bitIdx)) > 0;
                arr[chIdx] = SetCase(arr[chIdx], isUpper);
            }

            result.Add(new string(arr));
        }

        return result;
    }
}