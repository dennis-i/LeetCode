namespace LCode;

public class WhenTesting_TrappingRainWater
{
    [Theory]
    [InlineData(6, new[] { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 })]
    [InlineData(9, new[] { 4, 2, 0, 3, 2, 5 })]
    [InlineData(0, new[] { 0 })]
    [InlineData(1, new[] { 4, 2, 3 })]
    [InlineData(23, new[] { 5, 5, 1, 7, 1, 1, 5, 2, 7, 6 })]
    [InlineData(1, new[] { 4, 9, 4, 5, 3, 2 })]
    public void TestIt(int expected, int[] nums)
    {
        Assert.Equal(expected, Trap(nums));

    }

    public int Trap(int[] height)
    {
        int Count(Span<int> span)
        {
            int l = 0;
            int r = span.Length - 1;

            while (l < r)
            {
                if (span[l] > 0 && span[r] > 0)
                    break;

                if (span[l] == 0)
                    ++l;
                else if (span[r] == 0)
                    --r;
            }

            if (l == r)
                return 0;

            span = span.Slice(l, r - l + 1);
            int res = 0;
            int min = Int32.MaxValue;


            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] > 0 && span[i] < min)
                    min = span[i];

            }


            for (int i = 0; i < span.Length; i++)
            {
                if (span[i] == 0)
                    res += min;
                else
                    span[i] -= min;
            }

            if (span.Length > 1)
                res += Count(span);
            return res;
        }


        return Count(height.AsSpan());
    }
}