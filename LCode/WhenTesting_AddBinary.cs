using System.Text;

namespace LCode;


public class WhenTesting_AddBinary
{

    [Theory]
    [InlineData("0", "0", "0")]
    [InlineData("0", "0000", "00000")]
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


        (char carryOut, char result) BitAdd(char x, char y, char carryIn)
        {
            return (carryIn, x, y) switch
            {
                ('0', '0', '0') => ('0', '0'),
                ('0', '0', '1') => ('0', '1'),
                ('0', '1', '0') => ('0', '1'),
                ('0', '1', '1') => ('1', '0'),
                ('1', '0', '0') => ('0', '1'),
                ('1', '0', '1') => ('1', '0'),
                ('1', '1', '0') => ('1', '0'),
                ('1', '1', '1') => ('1', '1'),

                _ => throw new ArgumentOutOfRangeException()
            };
        }

        var spanA = a.AsSpan();
        var spanB = b.AsSpan();

        int maxLen = Math.Max(spanA.Length, spanB.Length);

        char[] arrA = new char[maxLen];
        char[] arrB = new char[maxLen];

        Array.Fill(arrA, '0');
        Array.Fill(arrB, '0');

        spanA.CopyTo(arrA.AsSpan(arrA.Length - spanA.Length));
        spanB.CopyTo(arrB.AsSpan(arrB.Length - spanB.Length));

        List<char> res = new(maxLen + 1);
        char carryIn = '0';

        for (int i = 0; i < arrA.Length; ++i)
        {
            var x = arrA[^(i + 1)];
            var y = arrB[^(i + 1)];

            var t = BitAdd(x, y, carryIn);
            carryIn = t.carryOut;
            res.Add(t.result);
        }

        res.Add(carryIn);

        bool oneFound = false;
        var sb = new StringBuilder(res.Count);
        for (int i = 0; i < res.Count; ++i)
        {
            var dig = res[^(i + 1)];
            if (dig == '1')
                oneFound = true;

            if (oneFound || i == res.Count - 1)
                sb.Append(dig);
        }
        return sb.ToString();
    }

}