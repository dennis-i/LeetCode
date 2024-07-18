namespace LCode;

public class WhenTesting_ValidParentheses
{
    [Theory]
    [InlineData(true, "()")]
    [InlineData(true, "()[]{}")]
    [InlineData(false, "(]")]
    [InlineData(false, "]")]
    [InlineData(false, "(")]
    public void TestIt(bool expected, string s)
    {
        Assert.Equal(expected, IsValid(s));
    }

    public bool IsValid(string s)
    {
        const string closing = ")]}";
        var span = s.AsSpan();

        var stack = new Stack<char>();
        while (!span.IsEmpty)
        {
            if (span[0] == '(')
                stack.Push(')');
            else if (span[0] == '[')
                stack.Push(']');
            else if (span[0] == '{')
                stack.Push('}');

            else if (closing.Contains(span[0]))
            {
                if (stack.Count == 0)
                    return false;
                var c = stack.Pop();
                if (c != span[0])
                    return false;
            }
            span = span.Slice(1);
        }

        return stack.Count == 0;
    }
}
