namespace LCode;

public class WhenTesting_FindMedianSortedArrays
{
    [Theory]
    [InlineData(2.0, new[] { 1, 3 }, new[] { 2 })]
    [InlineData(2.5, new[] { 1, 2 }, new[] { 3, 4 })]
    public void TestIt(double expected, int[] nums1, int[] nums2)
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
            else
            {
                l.Add(n1);
                ++i;
            }

        }

        return l;
    }
}