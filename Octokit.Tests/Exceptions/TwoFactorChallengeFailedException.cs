using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class TwoFactorChallengeFailedExceptionTests
    {
        public class TheConstructor
        {
            [Fact]
            public void SetsDefaultMessage()
            {
                var exception = new TwoFactorChallengeFailedException();

                Assert.Equal("The two-factor authentication code supplied is not correct", exception.Message);
            }
        }
    }
}
