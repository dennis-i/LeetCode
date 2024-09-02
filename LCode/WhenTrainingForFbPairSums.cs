namespace LCode;

public class WhenTrainingForFbPairSums
{

    [Fact]
    public void TestIt()
    {


        Assert.Equal(2, numberOfWays([1, 2, 3, 4, 3], 6));
        Assert.Equal(4, numberOfWays([1, 5, 3, 3, 3], 6));

        int[] longArr = Enumerable.Range(0, 10000000).Select(i => i).ToArray();
        Assert.Equal(4, numberOfWays(longArr, 6));
    }

    private static int numberOfWays(int[] arr, int k)
    {

        return withHash(arr, k);
    }

    private static int withHash(int[] arr, int k)
    {
        int res = 0;
        Dictionary<int, int> hash = new();
        for (int i = 0; i < arr.Length; ++i)
        {
            int diff = k - arr[i];
            if (!hash.ContainsKey(diff))
                hash.Add(diff, 0);

            hash[diff]++;
        }

        foreach (var element in arr)
        {
            if (hash.ContainsKey(element))
            {
                res += hash[element];
            }
        }

        return res >> 1;
    }

    private static int brutForce(int[] arr, int k)
    {
        int res = 0;
        for (int i = 0; i < arr.Length; ++i)
        {
            for (int j = 0; j < arr.Length; ++j)
            {
                if (i != j)
                {
                    if (arr[i] + arr[j] == k)
                        ++res;
                }
            }
        }
        return res >> 1;
    }
}