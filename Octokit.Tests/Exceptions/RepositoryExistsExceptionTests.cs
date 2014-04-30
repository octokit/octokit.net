using Xunit;

namespace Octokit.Tests.Exceptions
{
    public class RepositoryExistsExceptionTests
    {
        public class TheMessageProperty
        {
            [Fact]
            public void WhenOwnerIsNullDoNotMentionInMessage()
            {
                var exception = new RepositoryExistsException(
                    null,
                    "some-repo",
                    false,
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.Equal("There is already a repository named 'some-repo' for the current account.", exception.Message);
            }

            [Fact]
            public void WhenOwnerIsNotNullMentionInMessage()
            {
                var exception = new RepositoryExistsException(
                    "some-org",
                    "some-repo",
                    true,
                    GitHubClient.GitHubDotComUrl,
                    new ApiValidationException());

                Assert.Equal("There is already a repository named 'some-repo' in the organization 'some-org'.", exception.Message);
            }
        }
    }
}
