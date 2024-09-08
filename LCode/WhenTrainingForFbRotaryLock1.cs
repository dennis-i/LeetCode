namespace LCode;

public class WhenTrainingForFbRotaryLock
{


    [Theory]
    [InlineData(2, 3, 3, new[] { 1, 2, 3 })]
    [InlineData(11, 10, 4, new[] { 9, 4, 4, 8 })]
    public void TestOneLock(long expected, int n, int m, int[] c)
    {
        Assert.Equal(expected, getMinCodeEntryTimeWithSingleLock(n, m, c));

    }


    [Theory]
    [InlineData(2, 3, 3, new[] { 1, 2, 3 })]
    [InlineData(6, 10, 4, new[] { 9, 4, 4, 8 })]
    [InlineData(1, 10, 1, new[] { 2 })]
    [InlineData(0, 10, 1, new[] { 1 })]
    public void TestTwoLocks(long expected, int n, int m, int[] c)
    {
        Assert.Equal(expected, getMinCodeEntryTimeWithTwoLocks(n, m, c));

    }

    public long getMinCodeEntryTimeWithSingleLock(int N, int M, int[] C)
    {
        int curr = 1;




        int countSteps(bool cw, int current, int desired, int n, Dictionary<(bool, int, int), int> memo)
        {
            if (current > n) current = 1;
            else if (current <= 0) current = n;

            if (current == desired)
                return 0;

            current = cw ? current + 1 : current - 1;
            var key = (cw, current, desired);
            if (memo.ContainsKey(key))
                return memo[key];

            int res = 1 + countSteps(cw, current, desired, n, memo);
            memo.Add(key, res);
            return res;

        }



        long res = 0;
        var memo = new Dictionary<(bool, int, int), int>();
        foreach (var code in C)
        {
            int n1 = countSteps(true, curr, code, N, memo);
            int n2 = countSteps(false, curr, code, N, memo);
            res += Math.Min(n1, n2);
            curr = code;
        }

        return res;
    }

    public long getMinCodeEntryTimeWithTwoLocks(int N, int M, int[] C)
    {


        int countSteps(bool cw, int current, int desired, int n, Dictionary<(bool, int, int), int> memo)
        {
            if (current > n) current = 1;
            else if (current == 0) current = n;

            if (current == desired)
                return 0;

            current = cw ? current + 1 : current - 1;
            var key = (cw, current, desired);
            if (memo.ContainsKey(key))
                return memo[key];

            int res = 1 + countSteps(cw, current, desired, n, memo);
            memo.Add(key, res);
            return res;

        }

        int curr1 = 1;
        int curr2 = 1;

        long res = 0;
        var memo = new Dictionary<(bool, int, int), int>();
        foreach (var code in C)
        {

            int n1 = Math.Min(countSteps(true, curr1, code, N, memo), countSteps(false, curr1, code, N, memo));
            int n2 = Math.Min(countSteps(true, curr2, code, N, memo), countSteps(false, curr2, code, N, memo));

            if (n1 < n2)
            {
                curr1 = code;
                res += n1;
            }
            else
            {
                curr2 = code;
                res += n2;
            }

        }

        return res;
    }
}
