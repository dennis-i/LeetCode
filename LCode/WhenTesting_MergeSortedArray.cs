namespace LCode;

public class WhenTesting_MergeSortedArray
{
    [Theory]
    [InlineData(new[] { 1, 2, 2, 3, 5, 6 }, new[] { 1, 2, 3, 0, 0, 0 }, 3, new[] { 2, 5, 6 }, 3)]
    [InlineData(new[] { 1 }, new[] { 0 }, 0, new[] { 1 }, 1)]
    [InlineData(new[] { 1 }, new[] { 1 }, 1, new int[0], 0)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 4, 5, 6, 0, 0, 0 }, 3, new[] { 1, 2, 3 }, 3)]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 4, 5, 6, 0 }, 5, new[] { 3 }, 1)]
    [InlineData(new[] { -1, 0, 0, 1, 1, 1, 2, 3, 3 }, new[] { 0, 0, 3, 0, 0, 0, 0, 0, 0 }, 3, new[] { -1, 1, 1, 1, 2, 3 }, 6)]
    [InlineData(new[] { -1, -1, 0, 0, 1, 1, 2, 2, 3 }, new[] { -1, 0, 1, 1, 0, 0, 0, 0, 0 }, 4, new[] { -1, 0, 2, 2, 3 }, 5)]
    public void TestIt(int[] expected, int[] nums1, int m, int[] nums2, int n)
    {
        Merge(nums1, m, nums2, n);
        Assert.Equal(expected, nums1);
    }

    public void Merge(int[] nums1, int m, int[] nums2, int n)
    {
        if (n == 0)
            return;
        if (m == 0)
        {
            nums2.AsSpan().CopyTo(nums1);
            return;
        }

        void makeSpot(int[] arr, int spotIdx)
        {
            arr.AsSpan(spotIdx, arr.Length - spotIdx - 1).CopyTo(arr.AsSpan(spotIdx + 1));
        }


        for (int i = 0; i < n; ++i)
        {

            if (nums2[i] < nums1[i])
            {
                makeSpot(nums1, i);

                nums1[i] = nums2[i];
            }
            else if (nums2[i] >= nums1[m + i - 1])
            {
                nums1[m + i] = nums2[i];
            }
            else
            {
                int l = i;
                int r = i + 1;

                while (l < nums1.Length - 1 && nums2[i] >= nums1[l])
                {
                    ++l;
                }

                makeSpot(nums1, l);
                nums1[l] = nums2[i];
            }







        }


    }
}