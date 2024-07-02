using System.Text;

namespace LCode;

public class WhenTesting_ConvertingZigzag
{

    [Theory]
    [InlineData("PAHNAPLSIIGYIR", "PAYPALISHIRING", 3)]
    [InlineData("PINALSIGYAHRPI", "PAYPALISHIRING", 4)]
    [InlineData("A", "A", 1)]
    public void TestConvert(string expected, string inStr, int numRows)
    {
        Assert.Equal(expected, Convert(inStr, numRows));
    }


    public string Convert(string s, int numRows)
    {
        var span = s.AsSpan();
        int processed = 0;
        int sizeToGo = span.Length;
        var l = new List<char[]>();

        int colCnt = 0;
        while (processed < sizeToGo)
        {


            char[] col = new char[numRows];
            Array.Fill<char>(col, ' ');


            int colIdx = numRows == 1 ? 0 : colCnt % (numRows - 1);

            bool goDiag = colIdx != 0;
            if (!goDiag)
            {

                int numCharsToSlice = Math.Min(numRows, sizeToGo - processed);
                var rowSpan = span.Slice(0, numCharsToSlice);
                rowSpan.CopyTo(col);
                processed += numCharsToSlice;
                span = span.Slice(numCharsToSlice);
            }
            else
            {
                col[numRows - 1 - colIdx] = span[0];
                span = span.Slice(1);
                processed++;
            }
            l.Add(col);

            ++colCnt;
        }

        var sb = new StringBuilder(l.Count * numRows);
        for (int i = 0; i < numRows; ++i)
        {
            foreach (var row in l)
            {
                if (row[i] != ' ')
                    sb.Append(row[i]);
            }

        }
        return sb.ToString();

    }
}