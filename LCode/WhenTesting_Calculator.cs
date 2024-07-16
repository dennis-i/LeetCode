namespace LCode;

public class WhenTesting_Calculator
{

    [Theory]
    [InlineData(5, "3+2")]
    [InlineData(9, "3+2*3")]
    [InlineData(7, "3+20/5")]
    [InlineData(5, "(5+20)/5")]
    public void TestIt(int expected, string expr)
    {

        Assert.Equal(expected, Caclulate(expr));

    }

    [Theory]
    [InlineData(new[] { "1" }, "  1")]
    [InlineData(new[] { "1", "+", "2" }, "  1 + 2")]
    [InlineData(new[] { "(", "1", "+", "2", ")", "*", "8" }, "  (1 + 2)*8")]
    [InlineData(new[] { "1", "+", "2", "*", "4" }, "  1 + 2 * 4")]
    [InlineData(new[] { "10", "/", "2", "*", "4" }, "  10 / 2 * 4")]
    public void TestLexer(string[] expected, string expr)
    {
        var lexer = new Lexer(expr);
        Assert.Equal(expected, lexer.Tokens);
    }


    [Theory]

    [InlineData(24, "(1 + 2)*8")]

    public void TestParser(int expected, string expr)
    {
        var lexer = new Lexer(expr);
        var parser = new Parser(lexer);
        Assert.Equal(expected, parser.Calculate());

    }



    private int Caclulate(string expr)
    {
        var lexer = new Lexer(expr);
        var parser = new Parser(lexer);
        return parser.Calculate();
    }
}




abstract class Operation
{
    public int Operand { get; set; }
    public Operation Next { get; set; }
    public abstract void Update(ref int value);
}



class Start : Operation
{
    public override void Update(ref int value)
    {
        if (Next != null) Next.Update(ref value);
    }
}

class Add : Operation
{
    public override void Update(ref int value)
    {

        if (Next != null) Next.Update(ref value);
        value += Operand;
    }
}

class Mult : Operation
{
    public override void Update(ref int value)
    {

        if (Next != null) Next.Update(ref value);
        value *= Operand;
    }
}

class Div : Operation
{
    public override void Update(ref int value)
    {

        if (Next != null) Next.Update(ref value);
        value /= Operand;
    }
}


class Value : Operation
{
    public override void Update(ref int value)
    {
        value = Operand;
    }
}

internal class Parser
{
    public Parser(Lexer lexer)
    {
        _lexer = lexer;
    }

    private readonly Lexer _lexer;

    public int Calculate()
    {


        int sv = 1;

        Dictionary<int, List<string>> ordermap = new();

        const string parens = "()";


        var l = new List<string>();


        for (int i = 0; i < _lexer.Tokens.Count; ++i)
        {
            var symbol = _lexer.Tokens[i];
            if (symbol == "(")
                sv++;
            else if (symbol == ")")
                sv--;

            if (!ordermap.ContainsKey(sv))
                ordermap.Add(sv, []);


            if (!parens.Contains(symbol))
                ordermap[sv].Add(symbol);
        }


        bool isNumber(string n) => int.TryParse(n, out _);
        bool isSign(string n) => n is "+" or "-" or "*" or "/";


        Operation create(string sign)
        {
            return sign switch
            {
                "+" => new Add(),
                "*" => new Mult(),
                "/" => new Div(),
                _ => throw new ArgumentOutOfRangeException(nameof(sign), sign, null)
            };
        }

        void helper(Span<string> span, Operation operation)
        {

            

            if (!span.IsEmpty)
            {
                if (span.Length > 1)
                {
                    int value;
                    if (isSign(span[0]))
                    {
                        operation.Next = create(span[0]);
                        if (!int.TryParse(span[1], out value))
                            throw new Exception($"unhandled symbol:{span[1]}");
                        operation.Next.Operand = value;
                    }
                    else
                    {
                        if (!int.TryParse(span[0], out value))
                            throw new Exception($"unhandled symbol:{span[0]}");

                        operation.Next = create(span[1]);
                        operation.Next.Operand = value;
                    }
                    helper(span.Slice(2), operation.Next);
                }
                else
                {

                    if (int.TryParse(span[0], out var value))
                        operation.Next = new Value { Operand = value };
                }
            }

        }

        //var l = new List<Operation>();

        int n = default;
        while (ordermap.Count > 0)
        {
            var operation = new Start();

            int max = ordermap.Keys.Max();

            var expr = ordermap[max];
            helper(expr.ToArray(), operation);
            //l.Add(operation);
            operation.Update(ref n);
            ordermap.Remove(max);

        }

        //foreach (var e in l)
        //{
        //    e.Update(ref n);
        //}

        return n;
    }
}

internal class Lexer
{
    public Lexer(string expr)
    {
        _expr = expr;
        Chop();
    }

    private readonly string _expr;
    private readonly List<string> _tokens = new();

    public IReadOnlyList<string> Tokens => _tokens;

    private void Chop()
    {

        HashSet<char> operators = ['+', '-', '*', '/', '(', ')'];


        var expr = _expr.AsSpan();


        _tokens.Clear();


        while (!expr.IsEmpty)
        {

            expr = expr.TrimStart();

            if (operators.Contains(expr[0]))
            {

                _tokens.Add(expr.Slice(0, 1).ToString());
                expr = expr.Slice(1);
            }
            else if (char.IsDigit(expr[0]))
            {
                int i = 0;
                while (i < expr.Length && char.IsDigit(expr[i]))
                {
                    ++i;
                }

                _tokens.Add(expr.Slice(0, i).ToString());
                expr = expr.Slice(i);
            }
            else
            {
                throw new Exception($"Unhandled symbol:{expr[0]}");
            }
        }



    }
}