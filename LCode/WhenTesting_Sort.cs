
using System.Diagnostics;

namespace LCode;

public class WhenTesting_Sort
{
    [Theory]
    [InlineData(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 100 }, new[] { 100, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 })]
    public void TestIt(int[] expected, int[] nums)
    {
        Sort(nums);
        Assert.Equal(expected, nums);
    }


    [Theory]
    //[InlineData(10)]
    //[InlineData(20)]
    //[InlineData(30)]
    //[InlineData(40)]
    //[InlineData(50)]
    //[InlineData(70)]
    [InlineData(120)]
    public void TestIt2(int arrLen)
    {
        int[] nums1 = new int[arrLen];
        int[] nums2 = new int[arrLen];
        int[] expected = new int[arrLen];
        for (int i = 0; i < arrLen; ++i)
        {
            nums1[i] = arrLen - i;
            expected[i] = i + 1;
        }


        var sw = new Stopwatch();
        sw.Start();
        Array.Sort(nums2);
       
        sw.Stop();
        Debug.WriteLine($"sort 2 took:{sw.Elapsed.TotalNanoseconds} ns");
        sw.Restart();
        Sort(nums1);
        sw.Stop();
        Debug.WriteLine($"sort 1 took:{sw.Elapsed.TotalNanoseconds} ns");
        Assert.Equal(expected, nums1);
    }

    private void Sort(int[] nums)
    {
        _numIt = 0;
        Sort(nums, 73);
        //Sort(nums, 23);
        Sort(nums, 17);
        //Sort(nums, 11);
        Sort(nums, 7);
        //Sort(nums, 5);
        Sort(nums, 3);
        Sort(nums, 2);
    }
    private static int _numIt = 0;
    private void Sort(int[] nums, int step)
    {

        int cnt = 0;
        while (cnt < nums.Length - step)
        {
            var l = nums.AsSpan(cnt, step);
            var r = nums.AsSpan(nums.Length - step - cnt, step);
            ++cnt;

            void order(Span<int> span)
            {
                if (span[0] > span[^1])
                    (span[0], span[^1]) = (span[^1], span[0]);
                _numIt++;
            }
            order(l);
            order(r);
        }
    }





}


