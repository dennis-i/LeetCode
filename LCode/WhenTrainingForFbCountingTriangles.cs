using System.Diagnostics;

namespace LCode;

public class WhenTrainingForFbCountingTriangles
{

    [Fact]
    public void TestIt()
    {
        int[][] arr = [[2, 2, 3], [3, 2, 2], [2, 5, 6]];
        Assert.Equal(2, CountDistinctTriangles(arr));


        arr = [[8, 4, 6], [100, 101, 102], [84, 93, 173]];
        Assert.Equal(3, CountDistinctTriangles(arr));

        arr = [[5, 8, 9], [5, 9, 8], [9, 5, 8], [9, 8, 5], [8, 9, 5], [8, 5, 9]];
        Assert.Equal(1, CountDistinctTriangles(arr));
    }

    private static int CountDistinctTriangles(int[][] arr)
    {

        var map = new Dictionary<(int, int, int), int>();


        foreach (var tr in arr)
        {
            var key = ToKey(tr);
            if (!map.ContainsKey(key))
                map.Add(key, 0);
            map[key]++;

        }
        return map.Count;
    }

    private static ValueTuple<int, int, int> ToKey(int[] tr)
    {
        ArrSort(tr);
        return (tr[0], tr[1], tr[2]);
    }



    private static void PrintArray(int[] array, string name = "")
    {
        Debug.Write($"Array:{name}\n");
        Debug.Write("[");
        for (int i = 0; i < array.Length; ++i)
        {
            Debug.Write($"{array[i]}");
            if (i < array.Length - 1)
                Debug.Write(",");
        }
        Debug.Write("]\n");
    }

    private static void ArrSort(int[] arr)
    {

        Span<int> mergeSort(Span<int> src)
        {
            if (src.Length < 2)
                return src;

            int mid = src.Length >> 1;
            Span<int> left = src.Slice(0, mid);
            Span<int> right = src.Slice(mid);

            left = mergeSort(left);
            right = mergeSort(right);

            int[] result = new int[left.Length + right.Length];

            int l = 0;
            int r = 0;
            int dst = 0;
            while (l < left.Length && r < right.Length)
            {
                if (left[l] < right[r])
                {
                    result[dst] = left[l];
                    ++l;
                }
                else
                {
                    result[dst] = right[r];
                    ++r;
                }

                ++dst;
            }

            void complete(Span<int> srcArr, Span<int> dstArr, int srcIdx, ref int dstIdx)
            {
                while (srcIdx < srcArr.Length)
                {
                    dstArr[dstIdx++] = srcArr[srcIdx++];
                }
            }

            complete(left, result, l, ref dst);
            complete(right, result, r, ref dst);


            return result;

        }

        var m = mergeSort(arr);
        m.CopyTo(arr);
    }
}