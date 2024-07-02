

namespace LCode;

public class WhenTesting_RotateArray
{
    [Theory]
    [InlineData(new[] { 7, 1, 2, 3, 4, 5, 6 }, new[] { 1, 2, 3, 4, 5, 6, 7 }, 1)]
    [InlineData(new[] { 5, 6, 7, 1, 2, 3, 4 }, new[] { 1, 2, 3, 4, 5, 6, 7 }, 3)]
    [InlineData(new[] { 3, 99, -1, -100 }, new[] { -1, -100, 3, 99 }, 2)]
    [InlineData(new[] { 1, 2, 3 }, new[] { 1, 2, 3 }, 3)]
    [InlineData(new[] { 2, 1 }, new[] { 1, 2 }, 1)]
    [InlineData(new[] { 2, 1 }, new[] { 1, 2 }, 3)]
    public void TestIt(int[] expected, int[] nums, int k)
    {
        RotateFast(nums, k);
        Assert.Equal(expected, nums);
    }

    public void Rotate(int[] nums, int k)
    {
        if (nums.Length == 1) return;

        int k1 = k % nums.Length;

        var span = nums.AsSpan();

        while (k1 > 0)
        {
            int last = span[^1];
            var s0 = span.Slice(0, span.Length - 1);
            var s1 = span.Slice(1);

            s0.CopyTo(s1);
            span[0] = last;
            k1--;
        }
    }

    public void RotateFast(int[] nums, int k)
    {
        if (nums.Length == 1) return;

        int k1 = k % nums.Length;

        int[] tmp = new int[k1];
        var span = nums.AsSpan();

        var right = span.Slice(span.Length - k1);
        var left = span.Slice(0, span.Length - k1);

        right.CopyTo(tmp);
        left.CopyTo(span.Slice(k1));
        tmp.CopyTo(span);
    }
}