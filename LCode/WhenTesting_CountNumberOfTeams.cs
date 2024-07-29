using System.Diagnostics;

namespace LCode;

public class WhenTesting_CountNumberOfTeams
{

    [Theory]
    [InlineData(3, new[] { 2, 5, 3, 4, 1 })]
    [InlineData(0, new[] { 2, 1, 3 })]
    [InlineData(4, new[] { 1, 2, 3, 4 })]
    [InlineData(3, new[] { 3, 6, 7, 5, 1 })]
    [InlineData(25, new[] { 4, 7, 9, 5, 10, 8, 2, 1, 6 })]
    public void TestIt(int expected, int[] ratings)
    {
        Assert.Equal(expected, NumTeams(ratings));
    }

    public int NumTeams(int[] rating)
    {
        int cnt = 0;

        int Find(int[] nums, int i, int j, int k) => FindM(nums, i, j, k, new Dictionary<ValueTuple<int, int, int>, int>());

        int FindM(int[] nums, int i, int j, int k, Dictionary<ValueTuple<int, int, int>, int> memo)
        {


            if (i >= j || j >= k)
                return 0;

            if (i >= nums.Length || j >= nums.Length || k >= nums.Length)
                return 0;

            if (memo.TryGetValue((i, j, k), out var value))
                return value;


            int n1 = FindM(nums, i + 1, j, k, memo);
            int n2 = FindM(nums, i, j + 1, k, memo);
            int n3 = FindM(nums, i, j, k + 1, memo);



            int res = (nums[i] < nums[j] && nums[j] < nums[k]) ||
                      (nums[i] > nums[j] && nums[j] > nums[k]) ? 1 : 0;

            if (res == 1)
                Debug.WriteLine($"[{cnt++}]({nums[i]},{nums[j]},{nums[k]})");

            res += n1;
            res += n2;
            res += n3;

            //res = Math.Max(Math.Max(n1, n2), n3) + res;
            memo.Add((i, j, k), res);

            return res;
        }
        return Find(rating, 0,1, 2);
    }
}