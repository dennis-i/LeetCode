using System.Text;

namespace LCode;

public class WhenTesting_AddBinary
{

    [Theory]
    [InlineData("100", "11", "1")]
    [InlineData("10101", "1010", "1011")]
    [InlineData("110110", "100", "110010")]
    [InlineData("110111101100010011000101110110100000011101000101011001000011011000001100011110011010010011000000000",
        "10100000100100110110010000010101111011011001101110111111111101000000101111001110001111100001101",
        "110101001011101110001111100110001010100001101011101010000011011011001011101111001100000011011110011")]
    public void TestIt(string expected, string a, string b)
    {
        Assert.Equal(expected, AddBinary(a, b));
    }


    public string AddBinary(string a, string b)
    {
        int n1 = BinToInt(a);
        int n2 = BinToInt(b);

        return IntToBin(n2 + n1);
    }

    private string IntToBin(int val)
    {
        if (0 == val)
            return "0";
        Dictionary<int, string> hex2bin = new()
        {
            {0x0,"0000"},
            {0x1,"0001"},
            {0x2,"0010"},
            {0x3,"0011"},
            {0x4,"0100"},
            {0x5,"0101"},
            {0x6,"0110"},
            {0x7,"0111"},
            {0x8,"1000"},
            {0x9,"1001"},
            {0xa,"1010"},
            {0xb,"1011"},
            {0xc,"1100"},
            {0xd,"1101"},
            {0xe,"1110"},
            {0xf,"1111"}
        };

        var l = new List<string>();
        while (val > 0)
        {
            var n = val & 0xf;
            l.Add(hex2bin[n]);
            val >>= 4;
        }

        var sb = new StringBuilder();
        for (int i = 0; i < l.Count; ++i)
            sb.Append(l[^(i + 1)]);

        return sb.ToString().TrimStart('0');
    }

    private int BinToInt(string s)
    {

        Dictionary<string, int> bin2hex = new()
         {
             {"0000",0x0},
             {"0001",0x1},
             {"0010",0x2},
             {"0011",0x3},
             {"0100",0x4},
             {"0101",0x5},
             {"0110",0x6},
             {"0111",0x7},
             {"1000",0x8},
             {"1001",0x9},
             {"1010",0xa},
             {"1011",0xb},
             {"1100",0xc},
             {"1101",0xd},
             {"1110",0xe},
             {"1111",0xf}
         };


        var span = s.AsSpan();

        int alingLen = ((span.Length + 3) >> 2) << 2;
        char[] align = new char[alingLen];
        Array.Fill(align, '0');
        span.CopyTo(align.AsSpan(alingLen - span.Length));

        int res = 0;
        int numHexDigits = alingLen >> 2;
        for (int i = 0; i < numHexDigits; ++i)
        {
            int offset = i << 2;
            var spanDig = align.AsSpan(offset, 4);
            int d = bin2hex[new string(spanDig)];
            res |= (d << ((numHexDigits - 1 - i) << 2));

        }
        return res;
    }
}