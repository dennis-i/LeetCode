namespace LCode;

public class WhenTestingThreeConsecutiveOdds
{


    [Theory]
    [InlineData(false, new[] { 2, 6, 4, 1 })]
    [InlineData(false, new[] { 1 })]
    [InlineData(true, new[] { 1, 2, 34, 3, 4, 5, 7, 23, 12 })]
    public void TestIt(bool expected, int[] arr)
    {
        Assert.Equal(expected, ThreeConsecutiveOdds(arr));
    }



    public bool ThreeConsecutiveOdds(int[] arr)
    {
        const int numConsElements = 3;

        var span = arr.AsSpan();
       
        while (!span.IsEmpty)
        {
            int sliceLen = Math.Min(span.Length, numConsElements);
            var elements = span.Slice(0, sliceLen);
            span = span.Slice(1);
            if (elements.Length < numConsElements)
                return false;

            int val = 1;

            foreach (var element  in elements)
            {
                val &= element;
            }

            if (val == 1)
                return true;

        }
        return false;
    }




}