namespace LCode;

public class WhenTrainingForFbContiguousSubarrays
{

  

    [Theory]
    [InlineData(new[] { 1, 3, 1, 5, 1 }, new[] { 3, 4, 1, 6, 2 })]
    [InlineData(new[] { 1, 2, 6, 1, 3, 1 }, new[] { 2, 4, 7, 1, 5, 3 })]
    public void TestIt(int[] expected, int[] array)
    {
        Assert.Equal(expected, CountSubarrays(array));
    }



    private int[] CountSubarrays(int[] arr)
    {


        int SubArrayAtIndex(int[] array, int index, int num)
        {
            int l = index - 1;
            int r = index + 1;
            int sum = 1;
            while (l >= 0 && array[l] < num)
            {
                --l;
                sum++;
            }

            while (r < array.Length && array[r] < num)
            {
                ++r;
                sum++;
            }

            return sum;
        }

        var result = new int[arr.Length];
        for (int i = 0; i < arr.Length; ++i)
        {
            int num = arr[i];
            int nArr = SubArrayAtIndex(arr, i, num);
            result[i] = nArr;
        }


        return result;
    }
}