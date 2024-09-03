using System.Diagnostics;

namespace LCode;

public class WhenTrainingForFbBalancedSplit
{
    [Theory]
    [InlineData(true, new[] { 1, 5, 7, 1 })]
    [InlineData(false, new[] { 12, 7, 6, 7, 6 })]
    [InlineData(false, new[] { 12, 7, 6, 7, 6, 12, 7, 6, 7, 6, 12, 7, 6, 7, 6, 12, 7, 6, 7, 6, 12, 7, 6, 7, 6 })]
    public void TestIt(bool expected, int[] array)
    {
        Assert.Equal(expected, BalancedSplitExists(array));

    }

    [Theory]
    [InlineData(new[] { 1, 1, 5, 7 }, new[] { 1, 5, 7, 1 })]
    public void TestSort(int[] expected, int[] array)
    {
        Assert.Equal(expected, Sort(array).ToArray());

        const int bigArraySize = Int32.MaxValue - 100;
        int[] bigArray = Enumerable.Range(0, bigArraySize).Select(i => bigArraySize - i).ToArray();

        //PrintArray<int>(bigArray, "unsorted");
        var sorted = Sort(bigArray);
        //PrintArray<int>(sorted, "sorted");
    }

    private static bool BalancedSplitExists(int[] arr)
    {
        Sort(arr)
            .CopyTo(arr);

        PrintArray<int>(arr, "input array");
        int r = arr.Length - 1;
        while (r > 0)
        {
            while (r > 0 && arr[r] == arr[r - 1])
            {
                --r;
            }

            var left = arr.AsSpan(0, r);
            var right = arr.AsSpan(r);
            PrintArray<int>(left, "left array");
            PrintArray<int>(right, "right array");

            if (ArraySum(left) == ArraySum(right))
                return true;
            --r;
        }

        return false;
    }

    private static int ArraySum(ReadOnlySpan<int> arr)
    {
        int sum = 0;
        foreach (var element in arr)
        {
            sum += element;
        }
        return sum;
    }

    private static void PrintArray<T>(ReadOnlySpan<T> array, string name = "")
    {
        int last = array.Length - 1;
        Debug.Write($"Array:{name}\n");
        Debug.Write("[");
        for (int i = 0; i < array.Length; ++i)
        {
            Debug.Write($"{array[i]}");
            if (i != last)
                Debug.Write(",");
        }
        Debug.Write("]\n\n\n");
    }

    private static Span<int> Sort(Span<int> array)
    {
        if (array.Length < 2)
            return array;

        int mid = array.Length >> 1;
        var left = Sort(array.Slice(0, mid));
        var right = Sort(array.Slice(mid));

        int[] result = new int[left.Length + right.Length];

        int l = 0;
        int r = 0;
        int i = 0;
        while (l < left.Length && r < right.Length)
        {
            result[i++] = left[l] < right[r] ? left[l++] : right[r++];
        }

        void doRest(ReadOnlySpan<int> src, Span<int> dst, int srcIdx, ref int dstIdx)
        {
            while (srcIdx < src.Length)
            {
                dst[dstIdx++] = src[srcIdx++];
            }
        }
        doRest(left, result, l, ref i);
        doRest(right, result, r, ref i);


        return result;
    }
}