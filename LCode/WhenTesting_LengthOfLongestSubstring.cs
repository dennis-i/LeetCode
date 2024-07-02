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

        var span = s.AsSpan();

        int index = 0;
        int longest = 0;


        while (index < span.Length - 1)
        {
            var curr = span[index++];
            int idx2 = index;

            while (idx2 < span.Length)
            {

                var next = span[idx2++];

                int charCnt = idx2 - index;

                if (curr == next)
                {
                    if (longest < charCnt)
                        longest = charCnt;
                    break;
                }
            }
        }

        return longest == 0 ? span.Length : longest;
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