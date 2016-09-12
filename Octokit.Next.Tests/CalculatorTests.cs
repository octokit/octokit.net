namespace Octokit.Next.Tests
{
    using Xunit;

    public class CalculatorTests
    {
        private readonly Calculator _sut;

        public CalculatorTests()
        {
            _sut = new Calculator();
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1, 2, 3)]
        [InlineData(2, 1, 3)]
        [InlineData(-1, 1, 0)]
        public void AddingTwoIntegers_ShouldReturnExpectedResult(int x, int y, int expected)
        {
            Assert.Equal(expected, _sut.Add(x, y));
        }
    }
}
