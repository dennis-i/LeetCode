using System.Text;

namespace LCode;

public class WhenTrainingForFbRotationalCipher
{

    [Theory]
    [InlineData("Cheud-726?", "Zebra-493?", 3)]
    [InlineData("nopqrstuvwxyzABCDEFGHIJKLM9012345678", "abcdefghijklmNOPQRSTUVWXYZ0123456789", 39)]
    public void TestIt(string expected, string input, int rotationFactor)
    {
        Assert.Equal(expected, rotationalCipher(input, rotationFactor));
    }



    private static string rotationalCipher(String input, int rotationFactor)
    {

        int letLen = 'z' - 'a' + 1;


        char rotate(char c, int by)
        {
            if (char.IsAsciiLetterLower(c))
                return (char)((c - 'a' + by) % letLen + 'a');
            if (char.IsAsciiLetterUpper(c))
                return (char)((c - 'A' + by) % letLen + 'A');
            if (char.IsAsciiDigit(c))
                return (char)((c - 0x30 + by) % 10 + 0x30);

            return c;
        }

        var sb = new StringBuilder(input.Length);
        for (int i = 0; i < input.Length; ++i)
        {
            sb.Append(rotate(input[i], rotationFactor));
        }

        return sb.ToString();

    }
}