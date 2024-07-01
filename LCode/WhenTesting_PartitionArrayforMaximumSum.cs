namespace LCode;


//TODO
public class WhenTesting_PartitionArrayforMaximumSum
{
    [Theory]
    [InlineData(84, new[] { 1, 15, 7, 9, 2, 5, 10 }, 3)]
    public void TestIt(int expected, int[] arr, int k)
    {
        Assert.Equal(expected, MaxSumAfterPartitioning(arr, k));
    }


    [Theory]
    [InlineData(new[] { 15, 10, 9 }, new[] { 1, 15, 7, 9, 2, 5, 10 }, 3)]
    public void TestMaxK(int[] expected, int[] arr, int k)
    {
        Assert.Equal(expected, MaxElements(arr, k));
    }


    public int MaxSumAfterPartitioning(int[] arr, int k)
    {


        int[] maxElements = MaxElements(arr, k);

        return 0;
    }

    private int[] MaxElements(int[] arr, int k)
    {
        int[] res = new int[k];
        Array.Fill(res, Int32.MinValue);

        for (int j = 0; j < res.Length; ++j)
        {
            bool maxFound = j != 0;
            for (int i = 0; i < arr.Length; ++i)
            {
                if (maxFound)
                {
                    if (arr[i] > res[j] && arr[i] < res[j - 1])
                        res[j] = arr[i];
                }
                else
                {
                    if (arr[i] > res[j])
                        res[j] = arr[i];
                }
            }
        }

        return res;
    }
}