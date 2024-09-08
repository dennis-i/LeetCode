using System.Security.Cryptography;

namespace LCode;

public class WhenTrainingForFbCafeteria
{


    [Fact]
    public void Test()
    {
        PriorityQueue<int, int> queue = new();


        for (int i = 0; i < 100; ++i)
        {
            int n = Random.Shared.Next(0, 100);
            queue.Enqueue(n, n);
        }
    }


    [Theory]
    [InlineData(3, 10, 1, 2, new long[] { 2, 6 })]
    [InlineData(1, 15, 2, 3, new long[] { 11, 6, 14 })]
    public void TestIt(long expected, long N, long K, int M, long[] S)
    {
        Assert.Equal(expected, GetMaxAdditionalDinersCount(N, K, M, S));
    }

    public long GetMaxAdditionalDinersCount(long N, long K, int M, long[] S)
    {
        Array.Sort(S);


        bool inRange(long val, long mid, long space) => val >= mid - space && val <= mid + space;


        long seat = 1;
        int existsIdx = 0;
        long cnt = 0;
        while (seat <= N)
        {
            if (existsIdx >= S.Length)
            {
                cnt++;
                seat += K + 1;
            }
            else if (inRange(seat, S[existsIdx], K))
            {
                seat = 1 + S[existsIdx] + K;
                existsIdx++;
            }
            else
            {
                cnt++;
                seat += K + 1;
            }
        }
        return cnt;
    }

}