using System;
using System.Text;

namespace LCode;

public class UnitTest1
{
    [Theory]
    [InlineData(2, "aab")]
    [InlineData(3, "abcabcbb")]
    [InlineData(1, "bbbbb")]
    [InlineData(3, "pwwkew")]
    [InlineData(1, " ")]
    [InlineData(2, "au")]

    public void Test1(int expected, string str)
    {
        Assert.Equal(expected, LengthOfLongestSubstring(str));
    }


    [Theory]
    [InlineData(2.0, new[] { 1, 3 }, new[] { 2 })]
    [InlineData(2.5, new[] { 1, 2 }, new[] { 3, 4 })]
    public void Test2(double expected, int[] nums1, int[] nums2)
    {
        var res = FindMedianSortedArrays(nums1, nums2);
        Assert.Equal(expected, res);
    }

    [Theory]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 3 }, new[] { 2 })]
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 1, 3 }, new[] { 2, 4 })]
    [InlineData(new[] { 1, 2, 3, 6, 7, 8 }, new[] { 1, 3, 8 }, new[] { 2, 6, 7 })]
    [InlineData(new[] { 2, 6, 7, 8 }, new[] { 8 }, new[] { 2, 6, 7 })]
    [InlineData(new[] { 0, 2, 6, 7, 100 }, new[] { 0, 100 }, new[] { 2, 6, 7 })]
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 1, 2 }, new[] { 3, 4 })]
    [InlineData(new[] { 1, 2, 3, 4 }, new[] { 3, 4 }, new[] { 1, 2 })]
    [InlineData(new[] { 0, 0, 0, 0 }, new[] { 0, 0 }, new[] { 0, 0 })]
    public void TestArrayMerge(int[] expected, int[] nums1, int[] nums2)
    {
        var res = Merge(nums1, nums2);
        Assert.Equal(expected, res);
    }


    //Input: s = "PAYPALISHIRING", numRows = 4
    //Output: "PINALSIGYAHRPI"

    [Theory]
    [InlineData("PAHNAPLSIIGYIR", "PAYPALISHIRING", 3)]
    [InlineData("PINALSIGYAHRPI", "PAYPALISHIRING", 4)]
    [InlineData("A", "A", 1)]
    public void TestConvert(string expected, string inStr, int numRows)
    {
        Assert.Equal(expected, Convert(inStr, numRows));
    }


    public string Convert(string s, int numRows)
    {
        var span = s.AsSpan();
        int processed = 0;
        int sizeToGo = span.Length;
        var l = new List<char[]>();

        int colCnt = 0;
        while (processed < sizeToGo)
        {


            char[] col = new char[numRows];
            Array.Fill<char>(col, ' ');


            int colIdx = numRows == 1 ? 0 : colCnt % (numRows - 1);

            bool goDiag = colIdx != 0;
            if (!goDiag)
            {

                int numCharsToSlice = Math.Min(numRows, sizeToGo - processed);
                var rowSpan = span.Slice(0, numCharsToSlice);
                rowSpan.CopyTo(col);
                processed += numCharsToSlice;
                span = span.Slice(numCharsToSlice);
            }
            else
            {
                col[numRows - 1 - colIdx] = span[0];
                span = span.Slice(1);
                processed++;
            }
            l.Add(col);

            ++colCnt;
        }

        var sb = new StringBuilder(l.Count * numRows);
        for (int i = 0; i < numRows; ++i)
        {
            foreach (var row in l)
            {
                if (row[i] != ' ')
                    sb.Append(row[i]);
            }

        }

        return sb.ToString();

    }



    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        if (nums1.Length == 0 && nums2.Length == 0)
            return 0.0;
        if (nums1.Length == 0)
            return Median(nums2);
        if (nums2.Length == 0)
            return Median(nums1);


        var merged = Merge(nums1, nums2);
        return Median(merged);

    }

    private static IReadOnlyList<int> Merge(int[] nums1, int[] nums2)
    {
        int totalSize = nums1.Length + nums2.Length;

        var l = new List<int>(totalSize);


        int i = 1;
        int j = 0;
        while (l.Count < totalSize)
        {
            int n1 = i > nums1.Length ? int.MaxValue : nums1[i - 1];

            int n2 = i >= nums1.Length ? int.MaxValue : nums1[i];

            int n3 = j >= nums2.Length ? int.MaxValue : nums2[j];



            if (n3 < n1)
            {
                l.Add(n3);
                ++j;
            }

            else if ((n3 < n2 && n3 > n1) || (n3 == n1))
            {
                l.Add(n1);
                l.Add(n3);
                ++j;
                ++i;
            }
            //else if (n3 == n1)
            //{
            //    l.Add(n1);
            //    l.Add(n3);
            //    ++i;
            //    ++j;
            //}
            else
            {
                l.Add(n1);
                ++i;
            }

        }

        return l;
    }

    private static double Median(IReadOnlyList<int> nums)
    {
        var size = nums.Count;
        if (1 == size)
            return nums[0];

        int idx = size >> 1;
        if ((size & 1) == 0)
        {
            return (nums[idx - 1] + nums[idx]) / 2.0;
        }

        return nums[idx];
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