namespace LCode;

public class WhenTrainingForFbSlowSums
{
    [Theory]
    [InlineData(26, new[] { 4, 2, 1, 3 })]
    [InlineData(88, new[] { 2, 3, 9, 8, 4 })]
    [InlineData(0, new int[0])]
    public void TestIt(int expected, int[] array)
    {
        Assert.Equal(expected, GetTotalTime(array));

    }

    int GetTotalTime(int[] arr)
    {
        Array.Sort(arr);
        int pen = 0;
        var span = arr.AsSpan();

        while (span.Length > 1)
        {
            span[^2] += span[^1];
            pen += span[^2];
            span = span.Slice(0, span.Length - 1);
        }

        return pen;
    }
}