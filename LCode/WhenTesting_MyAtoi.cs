namespace LCode;

public class WhenTesting_MyAtoi
{

    [Theory]
    [InlineData(42, "42")]
    [InlineData(-42, "  -042")]
    [InlineData(5, "5-1")]
    [InlineData(0, "0-1")]
    [InlineData(0, "words and 987")]
    [InlineData(987, " 987 757")]
    [InlineData(0, "")]
    [InlineData(-2147483648, "-91283472332")]
    [InlineData(-1, "-000000000000000000000000000000000000000000000000001")]
    [InlineData(2147483647, "21474836460")]
    [InlineData(2147483647, "20000000000000000000")]
    public void TestIt(int expected, string s)
    {
        Assert.Equal(expected, MyAtoi(s));
    }

    public int MyAtoi(string s)
    {

        bool isNonZero(char c) => c > 0x30 && c < 0x3a;
        bool isZero(char c) => c == 0x30;
        bool isMinus(char c) => c == 0x2d;
        bool isPlus(char c) => c == 0x2b;
        bool isSpace(char c) => c == 0x20;

        var span = s.AsSpan();
        var list = new List<int>(20);
        bool isNegative = false;
        bool signFound = false;

        bool leadingZeros = true;


        while (!span.IsEmpty)
        {
            char c = span[0];
            span = span.Slice(1);

            if (isZero(c))
            {
                if (!leadingZeros)
                    list.Add(0);
                signFound = true;
                continue;
            }

            if (isNonZero(c))
            {
                list.Add(c - 0x30);
                leadingZeros = false;
                signFound = true;
                continue;
            }

            if (isSpace(c) && !signFound)
                continue;

            if (isPlus(c) && !signFound)
            {
                signFound = true;
                continue;
            }

            if (isMinus(c) && !signFound)
            {
                isNegative = signFound = true;
                continue;
            }

            break;
        }

        if (list.Count == 0)
            return 0;

        if (list.Count > 10)
            return (isNegative) ? int.MinValue : int.MaxValue;

        long mult = (long)Math.Pow(10, list.Count - 1);
        long res = 0;
        for (int i = 0; i < list.Count; ++i)
        {
            long n = list[i] * mult;
            res += n;
            mult /= 10;

        }

        if (isNegative)
            res = -res;


        if (res < int.MinValue)
            return int.MinValue;
        if (res > int.MaxValue)
            return int.MaxValue;
        return (int)res;
    }
}