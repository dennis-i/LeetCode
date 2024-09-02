namespace LCode;

public class WhenTrainingForFbChangeInAForeignCurrency
{

    [Theory]
    [InlineData(false, new[] { 5, 10, 25, 100, 200 }, 94)]
    [InlineData(true, new[] { 4, 17, 29 }, 75)]
    [InlineData(true, new[] { 10, 20, 30, 40 }, 751010000)]
    public void TestIt(bool expected, int[] denominations, int targetMoney)
    {
        Assert.Equal(expected, canGetExactChange(targetMoney, denominations, new Dictionary<int, bool>()));
    }



    private static bool canGetExactChange(int targetMoney, int[] denominations, Dictionary<int, bool> memo)
    {
        if (memo.ContainsKey(targetMoney))
            return memo[targetMoney];

        if (targetMoney < 0)
        {
            memo.Add(targetMoney, false);
            return false;
        }

        if (targetMoney == 0)
            return true;




        foreach (var denomination in denominations)
        {
            int rem = targetMoney - denomination;
            if (canGetExactChange(rem, denominations, memo))
            {
                memo.Add(targetMoney, true);
                return true;
            }

        }
        memo.Add(targetMoney, false);
        return false;
    }
}