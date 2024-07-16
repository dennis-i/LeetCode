namespace LCode;

public class WhenTesting_LengthOfLongestSubstring
{
    [Theory]
    [InlineData(2, "aab")]
    [InlineData(3, "abcabcbb")]
    [InlineData(1, "bbbbb")]
    [InlineData(3, "pwwkew")]
    [InlineData(1, " ")]
    [InlineData(2, "au")]

    public void TestIt(int expected, string str)
    {
        Assert.Equal(expected, LengthOfLongestSubstring(str));
    }


    public int LengthOfLongestSubstring(string s)
    {

        var hash = new HashSet<char>();
        var span = s.AsSpan();

        int idx1 = 0;
        int idx2 = 0;

        int max = 0;
        while (idx1 < span.Length && idx2 < span.Length)
        {
            if (!hash.Contains(span[idx1]))
            {
                hash.Add(span[idx1]);
                ++idx1;
            }
            else
            {
                hash.Remove(span[idx2]);
                ++idx2;
            }

            max = Math.Max(hash.Count, max);
        }

        return max;
    }


    public int[] TwoSum(int[] nums, int target)
    {

        var l = new List<int>(nums);

        for (int i = 0; i < nums.Length; ++i)
        {
            int n = nums[i];
            int x = target - n;
            int idx = l.IndexOf(x);
            if (idx != -1 && idx != i)
                return [i, idx];

        }

        throw new Exception("Not found");

    }
}