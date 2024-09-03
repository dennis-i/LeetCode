namespace LCode;

public class WhenTrainingForFbBalanceBrackets
{
    [Theory]
    [InlineData(true, "{[()]}")]
    [InlineData(true, "{}()")]
    [InlineData(false, "{(})")]
    [InlineData(false, ")")]
    public void TestIt(bool expected, string brackets)
    {
        Assert.Equal(expected, IsBalanced(brackets));
    }

    private static bool IsBalanced(string s)
    {

        var stack = new Stack<char>();
        var map = new Dictionary<char, char>()
        {
            { '(', ')' },
            { '{', '}' },
            { '[', ']' },
        };

        for (int i = 0; i < s.Length; ++i)
        {
            var c = s[i];
            if (map.ContainsKey(c))
            {
                stack.Push(map[c]);
            }
            else
            {
                if (stack.Count == 0)
                    return false;
                if (stack.Peek() != c)
                    return false;
                stack.Pop();
            }
        }
        return true;
    }
}