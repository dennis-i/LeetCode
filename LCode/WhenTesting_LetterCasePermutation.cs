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

        char invertCase(char ch) => char.IsUpper(ch) ? char.ToLower(ch) : char.ToUpper(ch);


        var span = s.AsSpan();
        List<int> strIdxs = new(span.Length);

        for (int i = 0; i < span.Length; ++i)
        {
            if (char.IsLetter(span[i]))
                strIdxs.Add(i);
        }

        if (strIdxs.Count == 0)
            return [s];


        var result = new HashSet<string>();


        char[] arr = new char[span.Length];

        for (int i = 0; i < strIdxs.Count; ++i)
        {
            span.CopyTo(arr);
            result.Add(new string(arr));
            int strIdx1 = strIdxs[i];
            arr[strIdx1] = invertCase(arr[strIdx1]);
            result.Add(new string(arr));

            for (int j = i; j < strIdxs.Count; ++j)
            {
               
                int strIdx2 = strIdxs[j];
                span.CopyTo(arr);
                arr[strIdx1] = invertCase(arr[strIdx1]);
                result.Add(new string(arr));
                span.CopyTo(arr);
                arr[strIdx2] = invertCase(arr[strIdx2]);
                result.Add(new string(arr));
              
            }

        }

        return result.ToList();
    }
}