using Octokit;
using System.Threading.Tasks;
using Xunit;

public class SimpleJsonTests
{
    [Theory]
    [InlineData("\"abc\"", "abc")]
    [InlineData(" \"abc\" ", "abc")]
    [InlineData("\" abc \" ", " abc ")]
    [InlineData("\"abc\\\"def\"", "abc\"def")]
    [InlineData("\"abc\\r\\ndef\"", "abc\r\ndef")]
    public async Task ParseStringSuccess(string input, string expected)
    {
        int index = 0;
        bool success = true;

        string actual = SimpleJson.ParseString(input.ToCharArray(), ref index, ref success);

        Assert.True(success);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("\"abc")]
    public async Task ParseStringIncomplete(string input)
    {
        int index = 0;
        bool success = true;

        string actual = SimpleJson.ParseString(input.ToCharArray(), ref index, ref success);

        Assert.False(success);
        Assert.Null(actual);
    }
}
