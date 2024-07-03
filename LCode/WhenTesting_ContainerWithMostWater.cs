namespace LCode;

public class WhenTesting_ContainerWithMostWater
{
    [Theory]
    [InlineData(49, new[] { 1, 8, 6, 2, 5, 4, 8, 3, 7 })]
    [InlineData(1, new[] { 1, 1 })]
    [InlineData(0, new[] { 0, 0 })]
    [InlineData(0, new[] { 0, 10 })]
    [InlineData(20, new[] { 0, 10, 0, 20 })]
    [InlineData(17, new[] { 2, 3, 4, 5, 18, 17, 6 })]
    public void TestIt(int expected, int[] height)
    {
        Assert.Equal(expected, MaxArea(height));
    }

    public int MaxArea(int[] height)
    {

        int Min(int a, int b) => Math.Min(a, b);

        int CalcArea(ReadOnlySpan<int> span) => (Min(span[0], span[^1]) * (span.Length - 1));


        var span = height.AsSpan();
        if (span.Length == 2)
            return Min(span[0], span[1]);

        int max = int.MinValue;

        while (span.Length > 1)
        {

            var left = span;
            var right = span;

            int area = CalcArea(right);
            if (area > max)
                max = area;


            left = left.Slice(0, left.Length - 1);
            right = right.Slice(1);

            int maxH = max / right.Length;
            if (span[0] > maxH || span[^1] > maxH)
            {

                while (right.Length > 1)
                {
                    area = Math.Max(CalcArea(left), CalcArea(right));
                    if (area > max)
                        max = area;

                    left = left.Slice(0, left.Length - 1);
                    right = right.Slice(1);
                }
            }

            span = span.Slice(1, span.Length - 1);
        }
        return max;
    }
}