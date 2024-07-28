namespace LCode;

public class WhenTesting_GenerateParentheses
{
    [Theory]
    [InlineData(new[] { "()" }, 1)]
    [InlineData(new[] { "(())", "()()" }, 2)]
    public void TestIt(string[] expected, int n)
    {
        Assert.Equal(expected, GenerateParenthesis(n));
    }

    public IList<string> GenerateParenthesis(int n)
    {
        var result = new List<string>();


        return result;
    }
}