namespace LCode;

public class WhenTesting_SortAnArray
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 5 }, new[] { 5, 2, 3, 1 })]
    public void TestIt(int[] expected, int[] nums)
    {
        Assert.Equal(expected, SortArray(nums));
    }

    public int[] SortArray(int[] nums)
    {

        var res = MergeSort(nums);


        return res;
    }


    private int[] MergeSort(Span<int> arr)
    {

        Index idx = Index.End;
        if (arr.Length < 2) return arr.ToArray();

        int mid = arr.Length >> 1;
        var left = MergeSort(arr.Slice(0, mid));
        var right = MergeSort(arr.Slice(mid, arr.Length - mid));

        return Merge(left, right);
    }

    private int[] Merge(Span<int> left, Span<int> right)
    {
        var arr = new int[left.Length + right.Length];

        int l = 0;
        int r = 0;
        int i = 0;
        while (l < left.Length && r < right.Length)
        {
            if (left[l] < right[r])
            {
                arr[i] = left[l];
                l++;
            }
            else
            {
                arr[i] = right[r];
                r++;
            }

            i++;
        }

        while (l < left.Length)
        {
            arr[i] = left[l];
            l++;
            i++;
        }

        while (r < right.Length)
        {
            arr[i] = right[r];
            r++;
            i++;
        }

        return arr;
    }
}